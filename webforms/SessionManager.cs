using System;
using System.Collections.Generic;
using System.Reflection;
using System.Data;
using System.Web.SessionState;
using System.Web.UI;
using System.Web;

/// <summary>
/// Summary description for SessionManager
/// </summary>
public static class SessionManager
{
	static SessionManager()
	{
	}

    

    /// <summary>
    /// Pozor, nekontroluje zda je sc správné a nepodvrhnuté, ani nevrací -1 
    /// Nikdy nevrací null, null může vrátit pouze v jeho proměnných
    /// Cokoliv změníš v této metodě musíš změnit i v té s parametrem Page
    /// </summary>
    /// <param name="page"></param>
    /// <returns></returns>
    public static LoginedUser GetLoginedUser(HttpContext page)
    {
        LoginedUser vr = new global::LoginedUser();
        // Žel všechny stránky nemají masterPage, proto musím kontrolovat na null
        if (page != null)
        {
            object l = page.Session["login"];
            //object uid = page.Session["userId"];
            object sc = page.Session["sc"];
            if (l != null && sc != null && l is string && sc is string)// && d != null)
            {
                vr.login = (string)l;
                //vr.userId = (int)uid;
                vr.sc = (string)sc;
            }
            return vr;
        }
        return vr;
    }

    /// <summary>
    /// Nikdy nevrací null, null může vrátit pouze v jeho proměnných
    /// Cokoliv změníš v této metodě musíš změnit i v parametru HttpContext
    /// </summary>
    /// <param name="page"></param>
    /// <returns></returns>
    public static LoginedUser GetLoginedUser(SunamoMasterPage page)
    {
        LoginedUser vr = new global::LoginedUser();
        // Žel všechny stránky nemají masterPage, proto musím kontrolovat na null
        if (page != null)
        {
            object l = page.Session["login"];
            //object uid = page.Session["userId"];
            object sc = page.Session["sc"];
            if (l != null && sc != null && l is string  && sc is string)// && d != null)
            {
                vr.login = (string)l;
                //vr.userId = (int)uid;
                vr.sc = (string)sc;
            }
            else
            {
                // Pokud nezískám informace ze session, pokusím se je získat z cookies
                LoginCookie lc = SunamoMasterPage.GetLoginCookie(page.Page.Request);
                if (lc != null)
                {
                    vr.login = lc.login;
                    vr.sc = lc.sc;
                }
            }
            return vr;
        }
        return vr;
    }

    static void regenerateId(int userID)
    {
        HttpContext Context = HttpContext.Current;
        System.Web.SessionState.SessionIDManager manager = new System.Web.SessionState.SessionIDManager();
        string oldId = manager.GetSessionID(Context);
        string newId = manager.CreateSessionID(Context);
        SaveNewSessionIDToSession(userID, oldId, newId);
    }

    public static void SaveNewSessionIDToSession(int userID, string oldId, string newId)
    {
        HttpContext Context = HttpContext.Current;
        System.Web.SessionState.SessionIDManager manager = new System.Web.SessionState.SessionIDManager();
        bool isAdd = false, isRedir = false;
        try
        {
            manager.SaveSessionID(Context, newId, out isRedir, out isAdd);
            HttpApplication ctx = (HttpApplication)HttpContext.Current.ApplicationInstance;
            HttpModuleCollection mods = ctx.Modules;
            System.Web.SessionState.SessionStateModule ssm = (SessionStateModule)mods.Get("Session");
            System.Reflection.FieldInfo[] fields = ssm.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
            SessionStateStoreProviderBase store = null;
            System.Reflection.FieldInfo rqIdField = null, rqLockIdField = null, rqStateNotFoundField = null;
            foreach (System.Reflection.FieldInfo field in fields)
            {
                if (field.Name.Equals("_store")) store = (SessionStateStoreProviderBase)field.GetValue(ssm);
                if (field.Name.Equals("_rqId")) rqIdField = field;
                if (field.Name.Equals("_rqLockId")) rqLockIdField = field;
                if (field.Name.Equals("_rqSessionStateNotFound")) rqStateNotFoundField = field;
            }
            object lockId = rqLockIdField.GetValue(ssm);
            if ((lockId != null) && (oldId != null)) store.ReleaseItemExclusive(Context, oldId, lockId);
            rqStateNotFoundField.SetValue(ssm, true);
            rqIdField.SetValue(ssm, newId);
        }
        catch (Exception)
        {
            TableRowSessions3.SetSessionID(userID, oldId);
        }
        
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="login"></param>
    /// <param name="fce"></param>
    public static void LoginUser(SunamoMasterPage page, string login, int userId, string sc)
    {
        ////DebugLogger.Instance.Write("Přihlašuji uživatele " + login + " se sc " + sc);
        SaveNewSessionIDToSession(userId, page.Session.SessionID, sc);
        //regenerateId();
        page.Session.Add("login", login);
        page.Session.Add("userId", userId);
        page.Session.Add("sc", sc);
    }

    /// <summary>
    /// Experimentální podpora pro funkčnost metody .ID() i s objektem HttpContext, ne jen SunamoPage
    /// </summary>
    /// <param name="page"></param>
    /// <param name="login"></param>
    /// <param name="userId"></param>
    /// <param name="sc"></param>
    public static void LoginUser(HttpContext page, string login, int userId, string sc)
    {
        ////DebugLogger.Instance.Write("Přihlašuji uživatele dpo objektu HttpContext " + login + " se sc " + sc);
        SaveNewSessionIDToSession(userId, page.Session.SessionID, sc);
        //regenerateId();
        page.Session.Add("login", login);
        page.Session.Add("userId", userId);
        page.Session.Add("sc", sc);

    }

    /// <summary>
    /// 
    /// </summary>
    public static void LogoutUser(SunamoMasterPage page)
    {
        ////DebugLogger.Instance.Write("Odhlašuji uživatele " + page.Session["login"] + " se sc " + page.Session["sc"]);
        page.Session.Remove("login");
        page.Session.Remove("userId");
        page.Session.Remove("sc");
        page.Session.Abandon();
    }

    public static string GetValue(SunamoMasterPage page, string nameOfValue)
    {
        object val = page.Session[nameOfValue];
        if (val == null)
        {
            return "";
        }
        return val.ToString();
    }

    #region MyRegion
    /// <summary>
    /// Nikdy nevrací null, null může vrátit pouze v jeho proměnných
    /// </summary>
    /// <param name="page"></param>
    /// <returns></returns>
    public static LoginedUser GetLoginedUser(Page page)
    {
        return GetLoginedUser(CastToSMP(page));
    }

    private static SunamoMasterPage CastToSMP(Page page)
    {
        return (SunamoMasterPage)page.Master;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="login"></param>
    /// <param name="fce"></param>
    public static void LoginUser(Page page, string login, int userId, string sc)
    {
        LoginUser(CastToSMP(page), login, userId, sc);
    }

    /// <summary>
    /// 
    /// </summary>
    public static void LogoutUser(Page page)
    {
        LogoutUser(CastToSMP(page));
    }

    public static string GetValue(Page page, string nameOfValue)
    {
        return GetValue(CastToSMP(page), nameOfValue);
    } 
    #endregion

    #region Outcomment
    #endregion
}

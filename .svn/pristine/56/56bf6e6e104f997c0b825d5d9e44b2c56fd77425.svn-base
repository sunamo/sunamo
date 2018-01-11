using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
public  class SunamoMasterPage : System.Web.UI.MasterPage
{
    #region Práce s cookies a přihlašováním, pokud se něco změní zde, musíš to projevit i do SunamoPage
    /// <summary>
    /// Do názvu cookie A2 zadej například ThisApp.Name pro cookii, která obsahuje přihlašovací údaje
    /// </summary>
    /// <param name="Page"></param>
    /// <param name="nameOfCookie"></param>
    /// <returns></returns>
    public static HttpCookie GetExistsOrNew(HttpRequest req, string nameOfCookie)
    {
        HttpCookieCollection col = null;
        if (req == null)
        {
            col = HttpContext.Current.Request.Cookies;
        }
        else
        {
            col = req.Cookies;
        }

        string Mja = nameOfCookie;

        // Musí se to získavat pomocí metody Get, ne pomocí indexeru
        HttpCookie vr = col[Mja];

        if (vr == null)
        {
            vr = col.Get(Mja);
        }
        if (vr == null)
        {
            vr = new HttpCookie(Mja);
            foreach (string item in col.AllKeys)
            {
                vr.Values[item] = col[item].Value;
            }
        }
        return vr;
    }

    public static LoginCookie GetLoginCookie(HttpRequest req)
    {
        string sc = ReadPermanentCookieSingleValue(req, CookieNames.sCzSc);
        string login = ReadPermanentCookieSingleValue(req, CookieNames.sCzLogin);
        string idUser = ReadPermanentCookieSingleValue(req, CookieNames.sCzIdUser);
        if (login != null && sc != null && idUser != null)
        {
            if (!login.Contains("ASP.NET_SessionId=") && !sc.Contains("ASP.NET_SessionId=") && !idUser.Contains("ASP.NET_SessionId="))
            {
                int idUserI = -1;
                if (int.TryParse(idUser, out idUserI))
                {
                    LoginCookie vr = new LoginCookie();
                    vr.idUser = idUserI;
                    vr.login = login;
                    vr.sc = sc;
                    return vr;
                }
                else
                {
                    LoginCookie vr = new LoginCookie();
                    vr.login = login;
                    vr.sc = sc;
                    return vr;
                }
            }
        }

        return null;
    }

    public static string ReadPermanentCookieSingleValue(HttpRequest req, string nameOfCookie)
    {
        HttpCookie myCookie = GetExistsOrNew(req, nameOfCookie);
        if (nameOfCookie == CookieNames.sCzSc)
        {
            return ConvertRot12.From(myCookie.Value);
        }
        return myCookie.Value;
    }

    public string[] ReadPermanentCookie(string nameOfCookie, params string[] keys)
    {
        HttpCookie myCookie = null;
        myCookie = GetExistsOrNew(Page.Request, nameOfCookie);
        if (myCookie == null)
        {
            return new string[0];
        }

        string[] vr = new string[keys.Length];
        //ok - cookie is found.
        for (int i = 0; i < keys.Length; i++)
        {
            //Gracefully check if the cookie has the key-value as expected.
            string o = myCookie.Values[keys[i]];
            if (!string.IsNullOrEmpty(o))
            {
                if (o == null)
                {
                    vr[i] = null;
                }
                else
                {
                    if (keys[i] == "sc")
                    {
                        vr[i] = ConvertRot12.From(o);
                    }
                    else
                    {
                        vr[i] = o;
                    }
                }
            }
            else
            {
                vr[i] = null;
            }
        }
        return vr;
    }

    /// <summary>
    /// Pokud budeš chtít použít tuto metodu třeba v MasterPage, musíš ji zkopírovat! Žádné vkládání do statických tříd.
    /// Pokud chci zapsat null, nestačí poslat pouze null, musí to být string null
    /// </summary>
    /// <param name="args"></param>
    public void WritePernamentCookie(string nameOfCookie, params string[] args)
    {
        #region Nový kód podle SunamoPage
        #region NEFUNGUJE
        #endregion

        #region Možná funguje ale ...
        #endregion

        //create a cookie
        HttpCookie myCookie = null;
        if (!this.Request.IsLocal)
        {
            myCookie = new HttpCookie(nameOfCookie);
        }
        else
        {
            myCookie = new HttpCookie("localhost." + nameOfCookie);
        }

        //Add key-values in the cookie
        for (int i = 0; i < args.Length; i++)
        {
            #region MyRegion
            #endregion
            string fd = args[i];
            string df = args[++i];
            if (df == null)
            {
                df = "";
            }
            if (fd == "sc")
            {
                df = ConvertRot12.To(df);
            }
            myCookie.Values.Add(fd, df);    
        }

        //set cookie expiry date-time. Made it to last for next 12 hours.
        myCookie.Expires = DateTime.Now.AddYears(1);

        //Most important, write the cookie to client.
        Response.Cookies.Add(myCookie); 
        #endregion
    }

    public void WritePernamentCookieSingleValue(string nameOfCookie, string value)
    {
        HttpCookie myCookie = new HttpCookie(nameOfCookie);
        if (nameOfCookie == CookieNames.sCzSc)
        {
            value = ConvertRot12.To(value);
        }
        myCookie.Value = value;
        myCookie.Expires = DateTime.Now.AddYears(1);
        Response.Cookies.Add(myCookie);
    }
    
    /// <summary>
    /// Tato metoda musí být volána ještě předtím než se odhlásím ze session
    /// </summary>
    public void LogoutUser(string web)
    {
        #region Nový kód podle SunamoPage
            Logout();
        #endregion
    }

    /// <summary>
    /// Tato metoda musí být volána ještě předtím než se odhlásím ze session
    /// </summary>
    public void Logout()
    {
            RemoveCookieSingleValue(CookieNames.sCzIdUser);
            RemoveCookieSingleValue(CookieNames.sCzSc);
                RemoveCookieSingleValue(CookieNames.sCzLogin);
    }

    public void RemoveCookieSingleValue(string nameOfCookie)
    {
        WritePernamentCookieSingleValue(nameOfCookie, null);
    }

    public void RemoveCookie(string nameOfCookie, params string[] keysToDelete)
    {
        #region Novější verze pomocí SunamoPage
        string[] d = new string[keysToDelete.Length *2];

        for (int i = 0; i < keysToDelete.Length; i++)
        {
            d[i] = keysToDelete[i];
            d[++i] = null;
        }
        WritePernamentCookie(nameOfCookie, d);
        #endregion
    }
    /// <summary>
    /// Tato metoda se může volat pouze po přihlášení, nikoliv při načtení každé stránky
    /// </summary>
    /// <param name="login"></param>
    /// <param name="idUser"></param>
    /// <param name="sc"></param>
    /// <param name="autoLogin"></param>
    /// <param name="rememberUser"></param>
    public void DoLogin(string login, int idUser, string sc, bool autoLogin, bool rememberUser)
    {
        TableRowSessions3.SetSessionID(idUser, sc);
        //string[] args = null;
        if (autoLogin)
        {
            WritePernamentCookieSingleValue(CookieNames.sCzLogin, login);
            WritePernamentCookieSingleValue(CookieNames.sCzIdUser, idUser.ToString());
            WritePernamentCookieSingleValue(CookieNames.sCzSc, sc);
        }
        else
        {
            if (rememberUser)
            {
                WritePernamentCookieSingleValue(CookieNames.sCzLogin, login);
            }
            else
            {
                //WritePernamentCookie(ThisApp.Name, "login", null);
            }
        }
    }
    #endregion

    #region Working with cookies - Permanently commented
    #endregion
}

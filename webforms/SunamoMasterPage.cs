using System;
using System.Web;
using System.Web.UI;

public  class SunamoMasterPage : System.Web.UI.MasterPage
{
    /// <summary>
    /// To A2 is passed ThisApp.Name
    /// </summary>
    /// <param name="Page"></param>
    /// <param name="nameOfCookie"></param>
    /// <returns></returns>
    public static HttpCookie GetExistsOrNew(HttpRequest request, string nameOfCookie)
    {
        HttpCookieCollection collection = null;
        if (request == null)
        {
            collection = HttpContext.Current.Request.Cookies;
        }
        else
        {
            collection = request.Cookies;
        }

        HttpCookie cookie = collection[nameOfCookie];

        if (cookie == null)
        {
            cookie = collection.Get(nameOfCookie);
        }
        if (cookie == null)
        {
            cookie = new HttpCookie(nameOfCookie);
            foreach (string item in collection.AllKeys)
            {
                cookie.Values[item] = collection[item].Value;
            }
        }
        return cookie;
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
        HttpCookie cookie = null;
        cookie = GetExistsOrNew(Page.Request, nameOfCookie);
        if (cookie == null)
        {
            return new string[0];
        }

        string[] result = new string[keys.Length];
        // ok - cookie is found.
        for (int i = 0; i < keys.Length; i++)
        {
            string o = cookie.Values[keys[i]];
            if (!string.IsNullOrEmpty(o))
            {
                if (o == null)
                {
                    result[i] = null;
                }
                else
                {
                    if (keys[i] == "sc")
                    {
                        result[i] = ConvertRot12.From(o);
                    }
                    else
                    {
                        result[i] = o;
                    }
                }
            }
            else
            {
                result[i] = null;
            }
        }
        return result;
    }

    /// <summary>
    /// Pokud chci zapsat null, nestačí poslat pouze null, musí to být string null
    /// </summary>
    /// <param name="args"></param>
    public void WritePernamentCookie(string nameOfCookie, params string[] args)
    {
        // create a cookie
        HttpCookie cookie = null;
        if (!this.Request.IsLocal)
        {
            cookie = new HttpCookie(nameOfCookie);
        }
        else
        {
            cookie = new HttpCookie("localhost." + nameOfCookie);
        }

        //Add key-values in the cookie
        for (int i = 0; i < args.Length; i++)
        {
            string key = args[i];
            string value = args[++i];
            if (value == null)
            {
                value = "";
            }
            if (key == "sc")
            {
                value = ConvertRot12.To(value);
            }
            cookie.Values.Add(key, value);    
        }

        cookie.Expires = DateTime.Now.AddYears(1);

        Response.Cookies.Add(cookie); 
    }

    public void WritePernamentCookieSingleValue(string nameOfCookie, string value)
    {
        HttpCookie cookie = new HttpCookie(nameOfCookie);
        if (nameOfCookie == CookieNames.sCzSc)
        {
            value = ConvertRot12.To(value);
        }
        cookie.Value = value;
        cookie.Expires = DateTime.Now.AddYears(1);
        Response.Cookies.Add(cookie);
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
        string[] d = new string[keysToDelete.Length *2];

        for (int i = 0; i < keysToDelete.Length; i++)
        {
            d[i] = keysToDelete[i];
            d[++i] = null;
        }
        WritePernamentCookie(nameOfCookie, d);
    }

    public void DoLogin(string login, int idUser, string sc, bool autoLogin, bool rememberUser)
    {
        TableRowSessions3.SetSessionID(idUser, sc);
        
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
        }
    }
}

using sunamo;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web;

public static class SunamoPageHelper
{
    #region Lst
    #region Navždy zakomentováno
    #endregion
    public static string WebTitle(MySites sa, HttpRequest Request, MySites ms)
    {
        return " - " + MasterPageHelper.GetNameOfWeb(sa, Request, ms);
    }

    public static string DescriptionOfSite(byte sda)
    {
        string desc;
        if (sda == (byte)MySitesShort.Sda)
        {
            desc = "Mládež, Sbor, Církev, Křesťanství, Bůh, Ježíš, Duch svatý, Zábava, Hry, Soutěže, Fotogalerie, Download, Odkazy, Kontakty, Kalendář, Vzkazník, Modlitební prosby";
        }
        else if (sda == (byte)MySitesShort.Geo)
        {
            desc = "Geocaching je hra na pomezí sportu a turistiky, která spočívá v použití navigačního systému GPS při hledání skryté schránky nazývané cache (v češtině psáno i keš), o níž jsou známy její zeměpisné souřadnice (v systému WGS 84). Při hledání se používají turistické přijímače GPS. Člověk zabývající se geocachingem bývá označován slovem geocacher, česky též geokačer nebo prostě kačer. Po objevení cache, zapsání se do logbooku a případné výměně obsahu ji nálezce opět uschová a zamaskuje.";
        }
        else if (sda == (byte)MySitesShort.Nope)
        {
            desc = "Stránky člověka s nickem sunamo. Texty písní, Aplikace pro Windows a mnoho dalšího. Dále pak listingy některých kešek z geocaching.com, Zkracovač odkazů, Fotogalerie.";
        }
        else
        {
            throw new Exception("Neimplementovaná větev v metodě SunamoPage.DescriptionOfSite()");
        }

        return desc;
    }

    #region Zakomentováno dočasně
    /// <summary>
    /// Naplňuji ji v static SunamoPage() na hodnoty jako login.aspx, logout.aspx a další stránky, které by se neměli zapisovat do DB
    /// </summary>
    public static List<string> returnUrls = new List<string>();

    static SunamoPageHelper()
    {
        returnUrls.Add("login.aspx");
        returnUrls.Add("logout.aspx");
    }

    #endregion

    /// <summary>
    /// Pokud ještě nebyla IP adresa zjištěna, zjistím ji 
    /// Pokud IP adresa nebude mít 4 čísla byte nebo bude moje, vrátím null
    /// </summary>
    /// <returns></returns>
    public static byte[] IsIpAddressRight(SunamoPage sp)
    {
        if (sp.ipAddress == null)
        {
            sp.ipAddress = HttpRequestHelper.GetUserIP(sp.Request);
        }
        if (sp.ipAddress == null)
        {
            return null;
        }
        else
        {
            return IsIpAddressRight(sp.ipAddress);
        }
        
    }


    #endregion

    public static string GetFooterHtml(MySites ms)
    {
        HtmlGenerator sb = new HtmlGenerator();
        sb.WriteRaw("(c) 2012 - 2017 ");
        sb.WriteTagWithAttr("a", "href", "mailto:radek.jancik@sunamo.cz");
        sb.WriteRaw("radek.jancik@sunamo.cz");
        sb.TerminateTag("a");

        sb.WriteRaw(" - ");

        //sb.WriteTagWithAttr("a", "href", "https://www.microsoft.com/cs-cz/store/apps/geocaching-tool/9nblggh5jqtz");
        //sb.WriteRaw("Moje Windows 10 aplikace pro GeoCaching");
        //sb.TerminateTag("a");

        //sb.WriteRaw(" - ");

        sb.WriteTagWithAttr("a", "href", "http://jepsano.net");
        sb.WriteRaw("CZ Blog");
        sb.TerminateTag("a");

        

        sb.WriteRaw(" All rights reserved");

        return sb.ToString();
    }

    /// <summary>
    /// Pro routed pages
    /// </summary>
    /// <param name="rightUp"></param>
    public static void TransferToLogin(SunamoPage sp, string rightUp)
    {
        sp.WriteToDebugWithTime(rightUp + "Me/Login.aspx?ReturnUrl=" + UH.UrlEncode(sp.Request.Url.ToString()));
    }

    /// <summary>
    /// Přesměruje na OffRegLogSys.aspx když je vypnuté přihlašovací a registrační systém.
    /// </summary>
    /// <param name="page"></param>
    public static bool RedirectIfOffRegLogSys(SunamoPage sp)
    {
        //Request.Headers.Add("Location", "Default.aspx");
        if (!GeneralLayer.AllowedRegLogSys)
        {
            string na = "~/OffRegLogSys.aspx";
            sp.Server.Transfer(na);
            //Server.Transfer(na);
            return true;
        }
        return false;
    }

    /// <summary>
    /// Přesměruje, pokud uživatel není sunamo
    /// Pokud vrátí True, musí vrátit volající metoda bezodkladně řízení metody
    /// Přesměruje na Default.aspx nebo na předchozí stránku
    /// </summary>
    /// <param name="page"></param>
    public static bool RedirectIfNotAllowed(SunamoPage sp)
    {
        if (!MasterPageHelper.ReplaceWithCorrectFunction(sp))
        {
            string na = "~/Default.aspx";
            #region Toto jsem musel zakomentovat, aby mi když bych se chtěl odhlásit z Widgets/RemoveComments.aspx mě to nepřesměrovalo na odhlašovací stránka a tato stránka zase na Wid/RemoveComments.aspx a takhle pořád dokola
            #endregion
            sp.WriteToDebugWithTime(na);
            //Server.Transfer(na);
            sp.Response.End();
            return true;
        }
        return false;
    }

    public static void FillSC(SunamoPage sp)
    {
        LoginedUser lu = SessionManager.GetLoginedUser(sp);

        int id = lu.ID(sp);
        if (id != -1)
        {
            sp.scLoginedUser = lu.sc;
        }

    }

    /// <summary>
    /// Musí být volána na každé stránce kde se nějak pracuje s přihlášením. Je to kvůli úspoře výkonu, že ji volám takto
    /// Po zavolání této metody již nemusíš kontrolovat zda sc není null/SE nebo zda souhlasí SC, protože toto všechno dělá tato metoda, resp. metoda LoginedUser.ID(sp) kterou volá
    /// </summary>
    public static void FillLoginVariables(SunamoPage sp)
    {
        LoginedUser lu = SessionManager.GetLoginedUser(sp);

        int id = lu.ID(sp);
        if (id != -1)
        {
            sp.nameLoginedUser = lu.login;
            sp.idLoginedUser = id;
            sp.scLoginedUser = lu.sc;
        }
    }

    /// <summary>
    /// Získává z databáze a id uživatele ověřeného pomocí sc, naprosto spolehlivé.
    /// Uloží do proměnné SunamoPage.nameLoginedUser
    /// </summary>
    /// <param name="sp"></param>
    public static void FillNameUser(SunamoPage sp)
    {
        LoginedUser lu = SessionManager.GetLoginedUser(sp);

        int id = lu.ID(sp);
        if (id != -1)
        {
            sp.nameLoginedUser = GeneralCells.LoginOfUser(id); //lu.login;
        }
    }
    /// <summary>
    /// Pokud IP adresa nebude mít 4 čísla byte, vrátím null
    /// </summary>
    /// <param name="ipAddress"></param>
    /// <returns></returns>
    private static byte[] IsIpAddressRight(IPAddress ipAddress)
    {
        

        byte[] b = ipAddress.GetAddressBytes();
        if (b == null)
        {
            return null;
        }
        if (b.Length != 4)
        {
            return null;
        }
        else
        {
        }
        return b;
    }
}

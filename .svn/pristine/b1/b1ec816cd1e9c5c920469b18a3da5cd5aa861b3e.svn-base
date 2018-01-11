using sunamo;
using sunamo.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI.HtmlControls;

public class SunamoCzMetroUIHelper
{
    public static void SetHtmlMetroUpperBarV3(SunamoMasterPage smp, HtmlGenericControl horniLista, MySites domain)
    {
        SunamoPage p = (SunamoPage)smp.Page;
        SetHtmlMetroUpperBarV3(p, horniLista, domain);
    }
    private static void WriteLiWithAnchor(HtmlGenerator hgUlSettings, string link, string text)
    {
        hgUlSettings.WriteTag("li");
        hgUlSettings.WriteTagWithAttr("a", "href", link);
        hgUlSettings.WriteRaw(text);
        hgUlSettings.TerminateTag("a");
        hgUlSettings.TerminateTag("li");
    }



    /// <summary>
    /// A2 je například ChromeDevicesCompare, tedy doména 3. úrovně
    /// </summary>
    /// <param name="sp"></param>
    /// <param name="domain"></param>
    /// <returns></returns>
    private static string GetLiOfUpperBarMetro(SunamoPage sp, MySites domain)
    {
        StringBuilder sb = new StringBuilder();
        string text = "";
        text = domain.ToString();
        sb.Append("<li>");
        string host = sp.Request.Url.Host.Replace("www.", "");
        WriteTagWithAttrs(sb, "a", text, "href", web.UH.GetWebUri2(sp, text, host == Consts.localhost, host));

        sb.Append("</li>");
        return sb.ToString();
    }

    static void WriteTagWithAttrs(StringBuilder sb, string nazevTagu, string text, params string[] atrrs)
    {
        sb.AppendFormat("<{0} ", nazevTagu);
        for (int i = 0; i < atrrs.Length; i++)
        {
            sb.AppendFormat("{0}=\"{1}\" ", atrrs[i], atrrs[++i]);
        }
        sb.Append(">");

        sb.Append(text);
        sb.AppendFormat("</{0}>", nazevTagu);
    }

    public static void SetHtmlMetroUpperBarV3(SunamoPage p, HtmlGenericControl horniLista, MySites domain)
    {
        HttpRequest Request = p.Request;
        LoginCookie lu = SunamoMasterPage.GetLoginCookie(Request);
        int luid = -1;
        if (lu != null)
        {
            luid = lu.id;
        }
        bool logined = !(lu == null || luid == -1);

        HtmlGenerator hg = new HtmlGenerator();
        // fixed-top
        hg.WriteTagWithAttr("ul", "class", "app-bar");
        hg.WriteTagWithAttr("li", "style", "list-style-type: none;");
        hg.WriteTagWithAttr("ul", "class", "app-bar-menu");
        hg.WriteTagWithAttr("li", "class", "active-container");

        hg.WriteTagWith2Attrs("a", "href", "#", "class", "dropdown-toggle");
        hg.WriteTagWithAttr("span", "class", "mif-menu");
        hg.TerminateTag("span");
        hg.TerminateTag("a");

        string aActualWebInnerHtml = domain.ToString();
        if (domain == MySites.Nope)
        {
            aActualWebInnerHtml = "sunamo.cz";
        }

        #region MyRegion
        hg.WriteTagWithAttrs("ul", "class", "d-menu", "data-role", "dropdown", "style", "display: none;");
        foreach (MySites item in Enum.GetValues(typeof(MySites)))
        {

            if (item == MySites.BibleServer)
            {
                continue;
            }
            // 2
            if (item == MySites.Kocicky)
            {
                continue;
            }
            if (item == MySites.Nope)
            {
                continue;
            }
            if (item == MySites.ThunderBrigade)
            {
                continue;
            }
            // 11
            if (item == MySites.CasdMladez)
            {
                continue;
            }
            // 12
            if (item == MySites.Shortener)
            {
                continue;
            }
            // 13
            if (item == MySites.Shared)
            {
                continue;
            }
            // 14
            if (item == MySites.Eurostrip)
            {
                continue;
            }
            // 15
            if (item == MySites.Widgets)
            {
                continue;
            }
            if (item == MySites.WindowsStoreApps)
            {
                continue;
            }
            // 18
            if (item == MySites.MediaServis)
            {
                continue;
            }
            //255
            if (item == MySites.None)
            {
                continue;
            }
            hg.WriteRaw(GetLiOfUpperBarMetro(p, item));
        }
        #endregion
        hg.TerminateTag("ul");
        hg.TerminateTag("li");
        hg.TerminateTag("ul");

        hg.WriteTagWithAttrs("a", "class", "app-bar-element", "href", "javascript:goToDomain();", "title", "Jdi na rozcestník všech webů");
        hg.WriteTagWithAttr("span", "class", "mif-home");
        hg.TerminateTag("span");
        hg.TerminateTag("a");

        hg.WriteTagWithAttrs("a", "class", "app-bar-element", "href", "javascript:goToWeb();", "title", "Jdi na hlavní stránku " + aActualWebInnerHtml);
        hg.WriteTagWithAttr("span", "class", "mif-history");
        hg.TerminateTag("span");
        hg.TerminateTag("a");

        if (logined)
        {
            hg.WriteTagWithAttrs("a", "class", "app-bar-element", "href", "javascript:sendPageToCloseHuman('" + p.Request.Url.ToString() + "');", "title", "Doporučit tuto stránku blízkému");
            hg.WriteTagWithAttr("span", "class", "mif-mail");
            hg.TerminateTag("span");
            hg.TerminateTag("a");
        }

        if (luid == 1)
        {
            hg.WriteTagWith2Attrs("a", "class", "app-bar-element", "href", "javascript:showDebugInfo('" + p.Request.Url.Host + "', '" + p.Request.Url.ToString() + "');");
            hg.WriteTagWithAttr("span", "class", "mif-bug");
            hg.TerminateTag("span");
            hg.TerminateTag("a");
        }



        #region MyRegion
        string linkLogin = MeUri.LoginWithReturnUrl(p);
        #endregion



        hg.WriteTagWithAttr("div", "class", "place-right");

        string buttonUserOnClick = "";

        if (logined)
        {
            buttonUserOnClick = "javascript:location.replace('http://" + Request.Url.Host + "/Me/User.aspx?un=" + lu.login + "');";
        }
        else
        {
            buttonUserOnClick = "javascript:location.replace('" + linkLogin + "');";
        }

        if (logined)
        {
            //hg.WriteTagWithAttr("span", "class", "app-bar-element");
            hg.WriteTagWith2Attrs("a", "class", "app-bar-element", "href", buttonUserOnClick);
            if (!string.IsNullOrEmpty(lu.login))
            {
                //hg.WriteTagWithAttr("a", "class", "app-bar-element");
                hg.WriteNonPairTagWithAttrs("img", "src", GravatarHelper.GetGravatarUri(Request, lu.login), "alt", lu.login, "width", "28", "height", "28");
                //hg.TerminateTag("a");
            }
            hg.WriteRaw(" " + lu.login);


            hg.TerminateTag("a");
        }
        else
        {
            hg.WriteTagWith2Attrs("a", "class", "app-bar-element", "href", buttonUserOnClick);
            hg.WriteRaw("Nepřihlášený uživatel");
            hg.TerminateTag("a");
        }
        if (logined)
        {
            string aSignOutHRef = "";
            // Nemůžu tu mít Request.Url.Host protože jsem se musel vždy odhlašovat 2x, jen když tu je fixně www.webelieve.cz tak to funguje dobře.
            if (Request.Url.Host == "localhost")
            {
                aSignOutHRef = GetAnchorWithReturnUri("http://localhost/Me/Logout.aspx", Request);
            }
            else
            {
                aSignOutHRef = GetAnchorWithReturnUri(Consts.HttpWwwCzSlash + "Me/Logout.aspx", Request);
            }

            hg.WriteTagWithAttrs("a", "class", "app-bar-element", "href", aSignOutHRef, "Title", "Odhlásit");
            hg.WriteTagWithAttr("span", "class", "mif-lock");
            hg.TerminateTag("span");
            hg.TerminateTag("a");



        }
        #region MyRegion
        #endregion
        hg.TerminateTag("div");
        hg.TerminateTag("li");
        hg.TerminateTag("ul");
        //hg.TerminateTag("div");
        horniLista.InnerHtml = hg.ToString();
    }

    public static string GetAnchorWithReturnUri(string p, HttpRequest Request)
    {
        return GetAnchorWithReturnUri(p, Request, true);
    }

    private static string GetAnchorWithReturnUri(string p, HttpRequest Request, bool encode)
    {
        string s = null;
        if (encode)
        {
            s = sunamo.UH.UrlEncode(Request.Url.ToString());
        }
        else
        {
            s = Request.Url.ToString();
        }
        return p + "?ReturnUrl=" + s;
    }
}

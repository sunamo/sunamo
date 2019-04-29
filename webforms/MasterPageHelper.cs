﻿using System.Web.UI.HtmlControls;
using System;

using System.Drawing;
using System.Web.UI;
using System.Collections.Generic;
using System.Text;
using System.Web;
using sunamo;
/// <summary>
/// Výhoda toho že to mám v swf sestavení je to, že to mohu používat i bez webové služby a tím šetřit server ale holt v každém webu budu mít muset aktuální swf assembly
/// </summary>
public class MasterPageHelper
{
    /// <summary>
    /// A2 zda se má zahrnout jqueryslidemenu.js nebo jqueryslidemenuRoot.js(+css) metodou page.stylesAndScripts.Append() místo přímého vložení metodou JavascriptInjection.RegisterClientScriptExternal()
    /// </summary>
    /// <param name="page"></param>
    /// <param name="template"></param>
    /// <param name="writeGaTrackingCode"></param>
    public static void WriteGeneralCode(SunamoPage page, bool template, bool writeGaTrackingCode)
    {
        
        bool isRoot = MasterPageHelper.IsRoot(page);
        WriteGeneralCodeBase(page, writeGaTrackingCode);
        string pridat = "";
        if (!isRoot)
        {
            pridat += AllStrings.dds;
        }
    }

    /// <summary>
    /// A2 zda se má zahrnout jqueryslidemenu.js nebo jqueryslidemenuRoot.js(+css) metodou page.stylesAndScripts.Append() místo přímého vložení metodou JavascriptInjection.RegisterClientScriptExternal()
    /// </summary>
    /// <param name="page"></param>
    /// <param name="template"></param>
    /// <param name="writeGaTrackingCode"></param>
    public static void WriteGeneralCodeRoute(SunamoPage page, bool template, bool writeGaTrackingCode)
    {
        //SunamoPage sp = SunamoMasterPage.CastToSunamoPage(page;
        bool isRoot =MasterPageHelper.IsRoot(page);
        WriteGeneralCodeBase(page, writeGaTrackingCode);
        StringBuilder pridat = new StringBuilder();
        int pocet = SH.OccurencesOfStringIn(page.Request.FilePath, AllStrings.slash);
        for (int i = 1; i < pocet; i++)
        {
            pridat.Append(AllStrings.dds);
        }

        if (pocet > 2)
        {
            pocet -= 2;
            WriteRightCountUpForScripts(page.Header.Controls, pocet);
        }
      
    }

    /// <summary>
    /// A1 je počet argumentů stránky.
    /// Používá se pouze u Route stránek.
    /// </summary>
    /// <param name="page"></param>
    /// <param name="pocet"></param>
    public static void WriteRightCountUpForScripts(ControlCollection page, int pocet)
    {
            for (int i = 0; i < pocet; i++)
            {
                foreach (var item in page)
                {
                    if (item is LiteralControl)
                    {
                        LiteralControl sc = item as LiteralControl;
                        if (sc.Text.Contains("<script ") && !sc.Text.Contains("src=\"http"))
                        {
                            sc.Text = sc.Text.Replace("src=\"", "src=\AllStrings.dds);
                        }

                    }
                }
            }
        
    }

    public static SunamoMasterPage GetSmp(SunamoPage page)
    {
        Page p = page;
        if (page.Master == null)
        {
            p = page.Page;
        }
        return  (SunamoMasterPage)p.Master;
    }

    /// <summary>
    /// Vrátí null, jen pokud bude login null nebo WhiteSpaces
    /// </summary>
    /// <returns></returns>
    public static LoginCookie GetLoginCookie(SunamoPage p)
    {
        return SunamoMasterPage.GetLoginCookie(p.Request);
    }

    public static void AddFavicon(SunamoPage page, MySites ms)
    {
        if (page.Header != null)
        {
            var myHtmlLink = new HtmlLink { Href = "http://" + page.Request.Url.Host + "/img/" + ms.ToString() + "/favicon.ico" };
            myHtmlLink.Attributes.Add("rel", "shortcut icon");
            myHtmlLink.Attributes.Add("type", "image/x-icon");
            page.Header.Controls.AddAt(0, myHtmlLink);
            /*
             * 
             */
        }

    }

    public static bool IsRoot(SunamoPage page)
    {
        //return HttpContext.Current.Request.Path.IndexOf(AllChars.slash, 1) == -1;
        return page.Request.Path.IndexOf(AllChars.slash, 1) == -1;
    }

    public static string GetNameOfWeb(MySites sa, HttpRequest Request)
    {
        if (sa == MySites.Nope)
        {
            return Request.Url.Host;
        }
        else
        {
            if (sa == MySites.None)
            {
                return SH.ReplaceOnceIfStartedWith(Request.Url.Host, "www.", "");
            }
            else
            {
                return sa.ToString().ToLower() + AllStrings.dot + SH.ReplaceOnceIfStartedWith(Request.Url.Host, "www.", "");
            }
        }
    }

    protected void LogoutUser(SunamoPage p, string web)
    {
        GetSmp(p).Logout();
    }

    private static void WriteGeneralCodeBase(SunamoPage page, bool writeGaTrackingCode)
    {
        if (writeGaTrackingCode)
        {
            
        }
    }

    /// <summary>
    /// Volá ji pouze metoda RedirectIfNotAllowed, kterou volej na začátku každé administratorské stránky.
    /// Jinak tuto metodu nevolej. 
    /// Vrací true když je uživatel sunamo, jinak F.
    /// </summary>
    /// <param name="page"></param>
    /// <returns></returns>
    public static bool ReplaceWithCorrectFunction(SunamoPage page)
    {
        LoginedUser lu = SessionManager.GetLoginedUser(page);
        if (lu.ID(SunamoMasterPage.CastToSunamoPage(page)) == 1)
        {
            return true;
        }
        return false;
    }

}

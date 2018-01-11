using System.Collections.Generic;
using System.Text;
using System.Web.UI;
public static class JavaScriptInjectionResponse
{
    /// <summary>
    /// Pokud bude A1 null, nebude se zapisovat do stránky
    /// </summary>
    /// <param name="page"></param>
    /// <param name="src"></param>
    public static string RegisterClientScriptInnerHtml(Page page, string src)
    {
        HtmlGenerator hg = new HtmlGenerator();
        hg.WriteTagWithAttr("script", "type", "text/javascript");
        hg.WriteRaw(src);
        hg.TerminateTag("script");

        string vr = hg.ToString();
        if (page != null)
        {
            page.Response.Write(vr);
        }
        return vr;
    }

    /// <summary>
    /// Pokud bude A1 null, nebude se zapisovat do stránky
    /// Vrací prázdný řetězec, pokud A1 != null
    /// Do A2 se dává výstup metody GetRightUpRoot()
    /// </summary>
    /// <param name="page"></param>
    /// <param name="getRightUpRoot"></param>
    /// <param name="p1"></param>
    public static string InjectExternalScript(Page page, string getRightUpRoot, params string[] p1)
    {
        List<HtmlGenerator> list = new List<HtmlGenerator>(p1.Length);
        foreach (var item in p1)
        {
            HtmlGenerator hg = new HtmlGenerator();
            if (item.EndsWith(".js"))
            {
                hg.WriteTagWith2Attrs("script", "type", "text/javascript", "src", getRightUpRoot + item);
            }   
            else
            {
                hg.WriteTagWithAttr("script", "src", getRightUpRoot + item);
            }
            hg.TerminateTag("script");
            list.Add(hg);
            
        }
        StringBuilder sb = new StringBuilder();
        if (page == null)
        {
            foreach (var item in list)
            {
                sb.Append(item.ToString());
            }
        }
        else
        {
            foreach (var item in list)
            {
                page.Response.Write(item.ToString());
            }
        }
        return sb.ToString();
    }

    public static string InjectInternalScript(Page page, string javaScript)
    {
        return RegisterClientScriptInnerHtml(page, javaScript);
    }

    /// <summary>
    /// Pokud bude A1 null, nebude se zapisovat do stránky
    /// </summary>
    /// <param name="page"></param>
    /// <param name="p1"></param>
    public static string InjectExternalScriptUri(Page page, params string[] p1)
    {
        List<HtmlGenerator> list = new List<HtmlGenerator>(p1.Length);
        foreach (var item in p1)
        {
            HtmlGenerator hg = new HtmlGenerator();
            if (item.EndsWith(".js"))
            {
                hg.WriteTagWith2Attrs("script", "type", "text/javascript", "src", item);
            }
            else
            {
                hg.WriteTagWithAttr("script", "src", item);
            }
            hg.TerminateTag("script");
            list.Add(hg);
        }

        StringBuilder sb = new StringBuilder();
        if (page == null)
        {
            foreach (var item in list)
            {
                sb.Append(item.ToString());
            }
        }
        else
        {
            foreach (var item in list)
            {
                page.Response.Write(item.ToString());
            }
        }
        return sb.ToString();
    }

}

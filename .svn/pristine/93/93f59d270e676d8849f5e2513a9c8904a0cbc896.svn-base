using System.Collections.Generic;
using System.Text;
using System.Web.UI;
public static class StyleInjectionResponse
{
    /// <summary>
    /// Pokud bude A1 null, nebude se zapisovat do stránky
    /// V případě že bude A2 URI dej do getRightUpRoot prázdný řetězec
    /// </summary>
    /// <param name="page"></param>
    /// <param name="src"></param>
    /// <returns></returns>
    public static string RegisterClientStyleExternal(Page page, string getRightUpRoot, string item)
    {
            HtmlGenerator hg = new HtmlGenerator();
            if (item.EndsWith(".css"))
            {
                hg.WriteTagWithAttrs("style", "type", "text/css", "href", getRightUpRoot + item, "rel", "stylesheet");
            }
            else
            {
                hg.WriteTagWith2Attrs("style", "href", getRightUpRoot + item, "rel", "stylesheet");
            }
            hg.TerminateTag("style");
            string vr = hg.ToString();
            if (page == null)
            {
                return vr;
            }
            else
            {
                page.Response.Write(vr);
            }
            return vr;
        
    }

    /// <summary>
    /// Pokud bude A1 null, nebude se zapisovat do stránky
    /// Pokud A1 != null, return empty string
    /// 
    /// </summary>
    /// <param name="page"></param>
    /// <param name="getRightUpRoot"></param>
    /// <param name="p1"></param>
    /// <returns></returns>
    public static string InjectExternalStyle(Page page, string getRightUpRoot, params string[] p1)
    {
        List<HtmlGenerator> list = new List<HtmlGenerator>(p1.Length);
        foreach (var item in p1)
        {
            HtmlGenerator hg = new HtmlGenerator();
            if (item.EndsWith(".css"))
            {
                hg.WriteTagWithAttrs("style", "type", "text/css", "href", getRightUpRoot + item, "rel", "stylesheet");
            }
            else
            {
                hg.WriteTagWith2Attrs("style", "href", getRightUpRoot + item, "rel", "stylesheet");
            }
            hg.TerminateTag("style");
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

    /// <summary>
    /// Pokud bude A1 null, nebude se zapisovat do stránky
    /// Pokud A1 nebude null, vrátí Empty String
    /// </summary>
    /// <param name="page"></param>
    /// <param name="p1"></param>
    /// <returns></returns>
    public static string InjectExternalStyleUri(Page page, params string[] p1)
    {
        List<HtmlGenerator> list = new List<HtmlGenerator>(p1.Length);
        foreach (var item in p1)
        {
            HtmlGenerator hg = new HtmlGenerator();
            if (item.EndsWith(".css"))
            {
                hg.WriteTagWithAttrs("style", "type", "text/css", "href", item, "rel", "stylesheet");
            }
            else
            {
                hg.WriteTagWith2Attrs("style", "href", item, "rel", "stylesheet");
            }
            hg.TerminateTag("style");
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

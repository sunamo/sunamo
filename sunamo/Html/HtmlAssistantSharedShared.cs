using HtmlAgilityPack;
using sunamo.Html;
using sunamo.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

public partial class HtmlAssistant
{
    /// <summary>
    /// return se if wont be found
    /// return (null) Consts.nulled when attr exists without value (input readonly atc.)
    /// </summary>
    /// <param name="p"></param>
    /// <param name="divMain"></param>
    /// <param name="_trim"></param>
    public static string GetValueOfAttribute(string p, HtmlNode divMain, bool _trim = false)
    {
        object o = divMain.Attributes[p];
        if (o != null)
        {
            string st = ((HtmlAttribute)o).Value;
            if (_trim)
            {
                st = st.Trim();
            }

            if (st == string.Empty)
            {
                return Consts.nulled;
            }

            return st;
        }

        return string.Empty;
    }

    public static string TrimInnerHtml(string value)
    {
        HtmlDocument hd = HtmlAgilityHelper.CreateHtmlDocument();
        hd.LoadHtml(value);
        foreach (var item in hd.DocumentNode.DescendantsAndSelf())
        {
            if (item.NodeType == HtmlNodeType.Element)
            {
                item.InnerHtml = item.InnerHtml.Trim();
            }
        }
        return hd.DocumentNode.OuterHtml;
    }
}
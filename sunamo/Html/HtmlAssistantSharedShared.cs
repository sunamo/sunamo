using HtmlAgilityPack;
using sunamo.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

public partial class HtmlAssistant
{
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
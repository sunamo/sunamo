using HtmlAgilityPack;
using sunamo.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

public partial class HtmlAssistant
{
    static Type type = typeof(HtmlAssistant);

    public static string InnerTextDecodeTrim(HtmlNode n)
    {
        var r = n.InnerText.Trim();
        r = SH.ReplaceWhiteSpacesWithoutSpaces(r, AllStrings.space);
        r = HttpUtility.HtmlDecode(r);
        r = SH.ReplaceAllDoubleSpaceToSingle(r);
        return r;
    }
    public static string InnerText(HtmlNode item, bool recursive, string tag)
    {
        var node = HtmlAgilityHelper.Node(item, recursive, tag);
        if (node == null)
        {
            return string.Empty;
        }
        return node.InnerText;
    }

    public static string InnerHtml(HtmlNode item, bool recursive, string tag)
    {
        var node = HtmlAgilityHelper.Node(item, recursive, tag);
        if (node == null)
        {
            return string.Empty;
        }
        return node.InnerHtml;
    }

    public static Dictionary<string, string> GetAttributesPairs(string s)
    {
        if (!s.Contains("<"))
        {
            s = "<img " + s + "/>";
        }

        Dictionary<string, string> result = new Dictionary<string, string>();

        HtmlNode node = HtmlNode.CreateNode(s);
        foreach (var item in node.Attributes)
        {
            result.Add(item.Name, item.Value);
        }

        return result;
    }


}
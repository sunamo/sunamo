using HtmlAgilityPack;
using sunamo.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

/// <summary>
/// HtmlHelperText - for methods which NOT operate on HtmlAgiityHelper! 
/// HtmlAgilityHelper - getting new nodes
/// HtmlAssistant - Only for methods which operate on HtmlAgiityHelper! 
/// </summary>
public partial class HtmlAssistant
{
    public static List<string> SplitByBr(string input)
    {
        return SplitByTag(input, "br");
    }

    public static List< string> SplitByTag(string input, string d)
    {
        var ih = input;
        ih = HtmlHelper.ReplaceHtmlNonPairTagsWithXmlValid(ih);
        var lines = SH.Split(ih, HtmlTagTemplates.br);
        return lines;
    }

    public static void SetAttribute(HtmlNode node, string atr, string hod)
    {
        object o = null;
        while (true)
        {
            o = node.Attributes.FirstOrDefault(a => a.Name == atr);
            if (o != null)
            {
                node.Attributes.Remove((HtmlAttribute)o);
            }
            else
            {
                break;
            }
        }

        node.Attributes.Add(atr, hod);
    }

    public static string InnerText(HtmlNode node, bool recursive, string tag, string attr, string attrValue)
    {
        var node2 = HtmlAgilityHelper.NodeWithAttr(node, true, tag, attr, attrValue);
        if (node2 != null)
        {
            return HtmlAssistant.HtmlDecode(node2.InnerText.Trim());
        }

        return string.Empty;
    }

    private static string HtmlDecode(string v)
    {
        return HttpUtility.HtmlDecode(v);
    }
}
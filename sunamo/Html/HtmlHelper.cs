using HtmlAgilityPack;
using sunamo.Constants;
using sunamo.Html;
using sunamo.Values;
using sunamo.Xml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;


public static partial class HtmlHelper
{
    static Type type = typeof(HtmlHelper);
    public static string ToXmlFinal(string xml)
    {
        xml = HtmlHelper.ReplaceHtmlNonPairTagsWithXmlValid(xml);
        xml = XH.RemoveXmlDeclaration(xml);
        return "<?xml version=\"1.0\" encoding=\"utf-8\" ?>" + HtmlHelper.ReplaceHtmlNonPairTagsWithXmlValid(XH.RemoveXmlDeclaration(xml.Replace("<?xml version=\"1.0\" encoding=\"iso-8859-2\" />", "").Replace("<?xml version=\"1.0\" encoding=\"utf-8\" />", "").Replace("<?xml version=\"1.0\" encoding=\"UTF-8\" />", "")));
    }

    public static void DeleteAttributesFromAllNodes(List<HtmlNode> nodes)
    {
        foreach (var node in nodes)
        {
            for (int i = node.Attributes.Count - 1; i >= 0; i--)
            {
                node.Attributes.RemoveAt(i);
            }
        }
    }

    /// <summary>
    /// Již volá ReplaceHtmlNonPairTagsWithXmlValid
    /// </summary>
    /// <param name="xml"></param>
    /// <param name="odstranitXmlDeklaraci"></param>
    
    public static List<HtmlNode> ReturnTagsWithContainsClassRek(HtmlNode htmlNode, string tag, string t)
    {
        List<HtmlNode> vr = new List<HtmlNode>();

        RecursiveReturnTagsWithContainsAttrWithSplittedElement(vr, htmlNode, tag, "class", t, AllStrings.space);
        return vr;
    }
}
using sunamo.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class WikipediaHelper
{
    public static List<string> ParseList(string html)
    {
        var hd = HtmlAgilityHelper.CreateHtmlDocument();

        hd.LoadHtml(html);

        var mwParserOutputNode = HtmlAgilityHelper.NodeWithAttr(hd.DocumentNode, true, "*", "class", "mw-parser-output");

        var subNodes = HtmlAgilityHelper.NodesWithAttr(mwParserOutputNode, false, "*", "class", "div-col columns column-width");

        List<string> result = new List<string>();

        foreach (var item in subNodes)
        {
            var anchors = HtmlAgilityHelper.Nodes(item, true, "a");

            foreach (var anchor in anchors)
            {
                result.Add(anchor.InnerText.Trim());
            }
        }

        return result;
    }
}


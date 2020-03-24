using sunamo;
using sunamo.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class WikipediaHelper
{
    public static string HtmlEntitiesList()
    {
        var c = string.Empty;
        //c = TF.ReadFile(@"d:\_Test\sunamo\shared\WikipediaHelper\ParseTable.html");


        var tables = WikipediaHelper.ParseTable(c, "Character", "Names");

        var table = tables.First();

        List<string> chars = table.ColumnValues("Character", true, false);
        List<string> names = table.ColumnValues("Names", true, true);

        return CSharpHelper.GetDictionaryValuesFromTwoList<string, string>(2, "a", names, chars, ",");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="html"></param>
    /// <param name="columns"></param>
    /// <returns></returns>
    public static List<HtmlTableParser> ParseTable(string html, params string[] columns)
    {
        List<HtmlTableParser> htmls = new List<HtmlTableParser>();
        var hd = HtmlAgilityHelper.CreateHtmlDocument();
        hd.LoadHtml(html);

        //var mwParserOutputNode = HtmlAgilityHelper.NodeWithAttr(hd.DocumentNode, true, "*", "class", "mw-parser-output");

        var subNodes = HtmlAgilityHelper.NodesWhichContainsInAttr(hd.DocumentNode, true, "*", "class", "wikitable");

        List<string> result = new List<string>();

        foreach (var item in subNodes)
        {
            var theads = HtmlAgilityHelper.Nodes(item, true, "th");

            var headers = new List<string>(theads.Count);
            foreach (var th in theads)
            {
                headers.Add(th.InnerText.Trim());
            }

            bool rightTable = true;

            foreach (var item2 in columns)
            {
                if (!headers.Contains(item2))
                {
                    rightTable = false;
                }
            }

            if (rightTable)
            {
                HtmlTableParser tp = new HtmlTableParser(item, false);

                htmls.Add(tp);
            }
        }

        return htmls;
    }

    /// <summary>
    /// Dont know for what page it was used
    /// I try to find with several page
    /// </summary>
    /// <param name="html"></param>
    /// <returns></returns>
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
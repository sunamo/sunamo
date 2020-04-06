using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sunamo.Html;

public partial class HtmlHelperText{ 
public static string ReplacePairTag(string input, string tag, string forWhat)
    {
        input = input.Replace("<" + tag + ">", "<" + forWhat + ">");
        input = input.Replace("<" + tag  + " ", "<" + forWhat+ " ");
        input = input.Replace("</" + tag + ">", "</" + forWhat + ">");
        return input;
    }

public static string InsertMissingEndingTags(string s, string tag)
    {
        StringBuilder text = new StringBuilder(s);

        var start = SH.ReturnOccurencesOfString(s, "<" + tag);
        var endingTag = "</" + tag + ">";
        var ends = SH.ReturnOccurencesOfString(s, endingTag);

        var startC = start.Count;
        var endsC = ends.Count;

        if (start.Count > ends.Count)
        {
            // In keys are start, in value end. If end isnt, then -1
            Dictionary<int, int> se = new Dictionary<int, int>();

            for (int i = start.Count - 1; i >= 0; i--)
            {
                var startActual = start[i];

                var endDx = -1;
                if (ends.Count != 0)
                {
                    endDx = ends.Count - 1;
                }
                var endActual = -1;
                if (endDx != -1)
                {
                    endActual = ends[endDx];
                }
                if (startActual > endActual)
                {
                    se.Add(startActual, -1);
                }
                else
                {
                    se.Add(startActual, endActual);
                    ends.RemoveAt(endDx);
                }
            }

            foreach (var item in se)
            {
                if (item.Value == -1)
                {
                    var dexEndOfStart = s.IndexOf(AllChars.gt, item.Key);

                    var space = s.IndexOf(AllChars.space, dexEndOfStart);

                    if (space != -1)
                    {

                        text.Insert(space, endingTag);
                    }
                }
            }
        }

        return text.ToString();
    }

    public static List<string> CreateH2FromNumberedList(List<string> lines)
    {
        CA.Trim(lines);

        for (int i = 0; i < lines.Count; i++)
        {
            if (SH.IsNumbered(lines[i]))
            {
                lines[i] = WrapWith(lines[i], "h2");
            }
        }

        return lines;
    }

    public static List<string> GetAllEquvivalentsOfNonPairingTag(string v)
    {
        return CA.ToListString("<" + v + ">", "<" + v + " />", "<"+v+"/>");
    }

/// <summary>
        /// A2 only name like p, title etc.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="v"></param>
        private static string WrapWith(string s, string p)
        {
            return AllStrings.lt + p + AllStrings.gt + s + "</" + p + AllStrings.gt;
        }

public static string RemoveAllNodes(string v)
    {
        var hd = HtmlAgilityHelper.CreateHtmlDocument();
        hd.LoadHtml(v);

        var nodes = hd.DocumentNode.Descendants().ToList();
        for (int i = 0; i < nodes.Count(); i++)
        {


            var node = nodes[i];

            if (node.NodeType != HtmlAgilityPack.HtmlNodeType.Text)
            {
                if (node.ParentNode.NodeType != HtmlAgilityPack.HtmlNodeType.Document)
                {
                    node.ParentNode.Remove();
                }
                else
                {
                    node.Remove();
                }
            }
        }

        return hd.DocumentNode.OuterHtml;
    }
}
using System;
using System.Collections.Generic;
using System.Text;
using HtmlAgilityPack;
using System.Linq;
using sunamo.Constants;


namespace sunamo.Html
{
    /// <summary>
    /// HtmlHelperText - for methods which NOT operate on HtmlAgiityHelper! 
    /// HtmlAgilityHelper - getting new nodes
    /// HtmlAssistant - Only for methods which operate on HtmlAgiityHelper! 
    /// </summary>
    public class HtmlAgilityHelper
    {
        /// <summary>
        /// Dříve bylo false ale to byla hloupost
        /// </summary>
        public static bool _trimTexts = true;
        public const string textNode = "#text";

        #region Helpers
        /// <summary>
        /// remove #text but keep everything else
        /// </summary>
        /// <param name="htmlNodeCollection"></param>
        
        public static string ReplacePlainUriForAnchors(HtmlDocument hd, string input)
        {
            /*
             * Kurví se mi to tady, přidává se na konec </installedapp></installedapp></installedapp></string></string>. 
             * Zde jsem ani po krokování neobjevil kde to vzniká, čímž bude to nejnodušší odstranit při formátu
             */

            input = HtmlAgilityHelper.WrapIntoTagIfNot(input);
            hd.LoadHtml(input);
            List<HtmlNode> textNodes = HtmlAgilityHelper.TextNodes(hd.DocumentNode, "a");
            for (int i = textNodes.Count - 1; i >= 0; i--)
            {
                var item = textNodes[i];
                if (CA.IsEqualToAnyElement<string>( item.ParentNode.Name, "pre"))
                {
                    continue;
                }
                var d = SH.SplitByWhiteSpaces(item.InnerText);
                bool changed = CA.ChangeContent(d, RegexHelper.IsUri, HtmlGenerator2.Anchor);

                item.InnerHtml = string.Empty;
                InsertGroup(item, d);

                //item.ParentNode.ReplaceChild(CreateNode(item.InnerHtml), item);

                // must be last because use ParentNode above
                //item.ParentNode.RemoveChild(item);

                //new HtmlNode(HtmlNodeType.Element, hd, 0);

                //    var ret = item.ParentNode.ReplaceChild(newNode, item);
                //newNode.ParentNode.InsertAfter(HtmlNode.CreateNode(d[1]), newNode);
                //int x = 0;
                //}
            }

            string output = hd.DocumentNode.OuterHtml;



            return output;
        }

        public static string WrapIntoTagIfNot(string input, string tag = HtmlTags.div)
        {
            input = input.Trim();
            if (input[0] != AllChars.lt)
            {
                input = WrapIntoTag(tag, input);
            }

            return input;
        }

        private static string WrapIntoTag(string div, string input)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(AllChars.lt);
            sb.Append(div);
            sb.Append(AllChars.gt);

            sb.Append(input);

            sb.Append(AllChars.lt + string.Empty + AllChars.slash);
            sb.Append(div);
            sb.Append(AllChars.gt);

            return sb.ToString();
        }

        public static void InsertGroup(HtmlNode insertAfter, List<string> list)
        {
            foreach (var item in list)
            {
                insertAfter.InnerHtml += SH.WrapWith(item, AllChars.space);
                //insertAfter = insertAfter.ParentNode.InsertAfter(CreateNode(item), insertAfter);
            }
            insertAfter.InnerHtml = SH.ReplaceAllDoubleSpaceToSingle(insertAfter.InnerHtml).Trim();
        }

        public static HtmlNode CreateNode(string html)
        {
            if (!RegexHelper.rHtmlTag.IsMatch(html))
            {
                html = SH.WrapWith(html, AllChars.space);
            }
            return HtmlNode.CreateNode(html);
        }

        private static List<HtmlNode> TextNodes(HtmlNode node, params string[] dontHaveAsParentTag)
        {
            /*
             * I tried https://www.nuget.org/p/ because <a href=\"https://jepsano.net/\">https://jepsano.net/</a> another text https://www.nuget.org/p/ divide into:
             * I tried https://www.nuget.org/p/ because
             * <a href=\"https://jepsano.net/\">
             * https://jepsano.net/ with parent a
             * another text https://www.nuget.org/p/ 
             * 
             */

            List<HtmlNode> vr = new List<HtmlNode>();
            List<HtmlNode> allNodes = new List<HtmlNode>();
            RecursiveReturnTags(allNodes, node, true, false, AllStrings.asterisk);
            foreach (var item in allNodes)
            {
                if (item.Name == textNode)
                {
                    if (!CA.IsEqualToAnyElement<string>(item.ParentNode.Name, dontHaveAsParentTag))
                    {
                        vr.Add(item);
                    }
                }
            }
            return vr;
        }
    }
}

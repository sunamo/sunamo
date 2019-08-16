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
        public static bool _trimTexts = false;

        #region Helpers
        public static List<HtmlNode> TrimTexts(HtmlNodeCollection htmlNodeCollection)
        {
            if (!_trimTexts)
            {
                return htmlNodeCollection.ToList();
            }
            List<HtmlNode> vr = new List<HtmlNode>();
            foreach (var item in htmlNodeCollection)
            {
                if (item.Name != "#text")
                {
                    vr.Add(item);
                }
            }
            return vr;
        }

        public static List<HtmlNode> TrimComments(List<HtmlNode> n)
        {
            List<HtmlNode> vr = new List<HtmlNode>();
            bool startWith = false;
            bool endsWith = false;
            foreach (var item in n)
            {
                startWith = false;
                endsWith = false;

                var html = item.InnerHtml.Trim();
                endsWith = html.Contains(SunamoCzAdminConsts.endHtmlComment);
                startWith = html.Contains(SunamoCzAdminConsts.startHtmlComment);

                if (startWith && endsWith) //item.NodeType == HtmlNodeType.Comment)
                {
                    
                }
                else if(true)
                {
                    

                    if (html == string.Empty)
                    {
                        continue;
                    }

                     endsWith = html.Contains(SunamoCzAdminConsts.endAspxComment);
                     startWith = html.Contains(SunamoCzAdminConsts.startAspxComment);
                    if (startWith || endsWith )
                    {
                        if (startWith && endsWith)
                        {
                            continue;
                        }
                        else
                        {
                            
                        }
                    }

                    if (html.StartsWith("<%"))
                    {
                        continue;
                    }

                    //var hd = HtmlAgilityHelper.CreateHtmlDocument();
                    //hd.LoadHtml(html);
                    int count = item.ChildNodes.Count;
                    var textCount = TrimTexts(item.ChildNodes).Count;

                    if (textCount == count && html == string.Empty)
                    {
                        continue;
                    }
                    //if (textCount != 0)
                    //{
                    //    continue;
                    //}

                        vr.Add(item);
                    
                }
               
            }
            return vr;
        }

        public static List<HtmlNode> TrimTexts(List<HtmlNode> c2)
        {
            if (!_trimTexts)
            {
                return c2;
            }
            List<HtmlNode> vr = new List<HtmlNode>();
            foreach (var item in c2)
            {
                if (item.Name != "#text")
                {
                    vr.Add(item);
                }
            }
            return vr;
        }

        /// <summary>
        /// Do A2 se může zadat *
        /// </summary>
        /// <param name="hn"></param>
        /// <param name="tag"></param>
        /// <returns></returns>
        private static bool HasTagName(HtmlNode hn, string tag)
        {
            if (tag == AllStrings.asterisk)
            {
                return true;
            }
            return hn.Name == tag;
        }

        private static bool HasTagAttr(HtmlNode item, string atribut, string hodnotaAtributu, bool enoughIsContainsAttribute)
        {
            if (hodnotaAtributu == AllStrings.asterisk)
            {
                return true;
            }
            bool contains = false;
            var attrValue = HtmlHelper.GetValueOfAttribute(atribut, item);

            if (enoughIsContainsAttribute)
            {
                contains = attrValue.Contains(hodnotaAtributu);
            }
            else
            {
                contains = attrValue == hodnotaAtributu;
            }
            return contains;
        }

        

        /// <summary>
        /// It's calling by others
        /// Do A5 se může vložit *
        /// </summary>
        /// <param name="vr"></param>
        /// <param name="html"></param>
        /// <param name="p"></param>
        private static void RecursiveReturnTags(List<HtmlNode> vr, HtmlNode html, bool recursive, bool single, string p)
        {
            foreach (HtmlNode item in html.ChildNodes)
            {
                if (HasTagName(item, p))
                {
                    //RecursiveReturnTags(vr, item, p);

                    vr.Add(item);

                    if (single)
                    {
                        return;
                    }

                    if (recursive)
                    {
                        RecursiveReturnTags(vr, item, recursive, single, p);
                    }
                }
                else
                {
                    if (recursive)
                    {
                        RecursiveReturnTags(vr, item, recursive, single, p);
                    }
                }
            }
        }

        public static List<HtmlNode> Nodes(HtmlNode node, bool recursive, bool single, string tag)
        {
            List<HtmlNode> vr = new List<HtmlNode>();
            RecursiveReturnTags(vr, node, recursive, false, tag);
            vr = TrimTexts(vr);
            return vr;
        }

        public static List<HtmlNode> NodesWithAttrWorker(HtmlNode node, bool recursive, string tag, string atribut, string hodnotaAtributu, bool enoughIsContainsAttribute)
        {
            List<HtmlNode> vr = new List<HtmlNode>();

            RecursiveReturnTagsWithContainsAttr(vr, node, recursive, tag, atribut, hodnotaAtributu, enoughIsContainsAttribute);
            vr = TrimTexts(vr);
            return vr;
        }

        public static HtmlDocument CreateHtmlDocument()
        {
            HtmlDocument hd = new HtmlDocument();
            hd.OptionOutputOriginalCase = true;
            // false - i přesto mi tag ukončený na / převede na </Page>
            hd.OptionAutoCloseOnEnd = false;
            hd.OptionOutputAsXml = false;
            hd.OptionFixNestedTags = false;
            hd.OptionCheckSyntax = false;
            return hd;
        }

        /// <summary>
        /// Do A3 se může zadat * pro vrácení všech tagů
        /// </summary>
        /// <param name="vr"></param>
        /// <param name="htmlNode"></param>
        /// <param name="p"></param>
        /// <param name="atribut"></param>
        /// <param name="hodnotaAtributu"></param>
        public static void RecursiveReturnTagsWithContainsAttr(List<HtmlNode> vr, HtmlNode htmlNode, bool recursively, string p, string atribut, string hodnotaAtributu, bool enoughIsContainsAttribute)
        {
            foreach (HtmlNode item in htmlNode.ChildNodes)
            {
                string attrValue = HtmlHelper.GetValueOfAttribute(atribut, item);

                if (HasTagName(item, p))
                {
                    if (HasTagAttr(item, atribut, hodnotaAtributu, enoughIsContainsAttribute))
                    {
                        vr.Add(item);
                    }

                    if (recursively)
                    {
                        RecursiveReturnTagsWithContainsAttr(vr, item, recursively, p, atribut, hodnotaAtributu, enoughIsContainsAttribute);
                    }
                }
                else
                {
                    if (recursively)
                    {
                        RecursiveReturnTagsWithContainsAttr(vr, item, recursively, p, atribut, hodnotaAtributu, enoughIsContainsAttribute);
                    }
                }
            }
        }
        #endregion

        #region 1 Node
        public static HtmlNode Node(HtmlNode node, bool recursive, string tag)
        {
            return CA.FirstOrNull<HtmlNode>(Nodes(node, recursive, true, tag));
        }
        #endregion

        #region 2 Nodes
        public static List<HtmlNode> Nodes(HtmlNode node, bool recursive, string tag)
        {
            return Nodes(node, recursive, false, tag);
        }
        #endregion

        #region 3 NodeWithAttr
        public static HtmlNode NodeWithAttr(HtmlNode node, bool recursive, string tag, string attr, string attrValue)
        {
            return NodesWithAttrWorker(node, recursive, tag, attr, attrValue, false).FirstOrDefault();
        }
        #endregion

        #region 4 NodesWithAttr
        public static List<HtmlNode> NodesWithAttr(HtmlNode node, bool recursive, string tag, string attr, string attrValue, bool contains = false)
        {
            return NodesWithAttrWorker(node, recursive, tag, attr, attrValue, contains);
        }
        #endregion

        #region 5 NodesWhichContainsInAttr
        /// <summary>
        /// A6 - whether is sufficient only contains
        /// </summary>
        /// <param name="node"></param>
        /// <param name="recursive"></param>
        /// <param name="tag"></param>
        /// <param name="attr"></param>
        /// <param name="attrValue"></param>
        /// <param name="contains"></param>
        /// <returns></returns>
        public static List<HtmlNode> NodesWhichContainsInAttr(HtmlNode node, bool recursive, string tag, string attr, string attrValue)
        {
            return NodesWithAttrWorker(node, recursive, tag, attr, attrValue, true);
        }
        #endregion

        public static string ReplacePlainUriForAnchors(string input)
        {
            HtmlDocument hd = CreateHtmlDocument();

            return ReplacePlainUriForAnchors(hd, input);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
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
                if (item.ParentNode.Name == "pre")
                {
                    continue;
                }
                var d = SH.Split(item.InnerText, AllStrings.space);
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
            insertAfter.InnerHtml = SH.DoubleSpacesToSingle(insertAfter.InnerHtml).Trim();
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
             * I tried https://www.nuget.org/p/ because <a href=\"http://jepsano.net/\">http://jepsano.net/</a> another text https://www.nuget.org/p/ divide into:
             * I tried https://www.nuget.org/p/ because
             * <a href=\"http://jepsano.net/\">
             * http://jepsano.net/ with parent a
             * another text https://www.nuget.org/p/ 
             * 
             */

            List<HtmlNode> vr = new List<HtmlNode>();
            List<HtmlNode> allNodes = new List<HtmlNode>();
            RecursiveReturnTags(allNodes, node, true, false, AllStrings.asterisk);
            foreach (var item in allNodes)
            {
                if (item.Name == "#text")
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

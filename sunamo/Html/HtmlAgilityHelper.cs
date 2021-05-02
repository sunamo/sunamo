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
        public static List<HtmlNode> TrimTexts(HtmlNodeCollection htmlNodeCollection)
        {
            if (!_trimTexts)
            {
                return htmlNodeCollection.ToList();
            }
            List<HtmlNode> vr = new List<HtmlNode>();
            foreach (var item in htmlNodeCollection)
            {
                if (item.Name != textNode)
                {
                    vr.Add(item);
                }
            }
            return vr;
        }

        public static HtmlNode FindAncestorParentNode(HtmlNode item, string v)
        {
            while (item != null)
            {
                if (item.Name == v)
                {
                    return item;
                }
                item = item.ParentNode;
            }
            return null;
        }

        public static bool HasAncestorParentNode(HtmlNode item, string v)
        {
            while (item != null)
            {
                if (item.Name == v)
                {
                    return true;
                }
                item = item.ParentNode;
            }
            return false;
        }

        /// <summary>
        /// remove #text but not #comment
        /// </summary>
        /// <param name="c2"></param>
        public static List<HtmlNode> TrimTexts(List<HtmlNode> c2)
        {
            return TrimTexts(c2, true, false);
        }


        /// <summary>
        /// A2 =remove #text
        /// A3 = remove #comment
        /// </summary>
        /// <param name="c2"></param>
        /// <param name="texts"></param>
        /// <param name="comments"></param>
        public static List<HtmlNode> TrimTexts(List<HtmlNode> c2, bool texts, bool comments = false)
        {
            if (!_trimTexts)
            {
                return c2;
            }
            List<HtmlNode> vr = new List<HtmlNode>();
            bool add = true;
            foreach (var item in c2)
            {
                add = true;
                if (texts)
                {
                    if (item.Name == textNode)
                    {
                        add = false;
                    }
                }
                if (comments)
                {
                    if (item.Name == "#comment")
                    {
                        add = false;
                    }
                }

                if (add)
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
            bool toTranslate = true;

            foreach (var item in n)
            {
                startWith = false;
                endsWith = false;
                toTranslate = true;



                var html = item.InnerHtml.Trim();
                // contains whole html comment
                endsWith = html.Contains(AspxConsts.endHtmlComment);
                startWith = html.Contains(AspxConsts.startHtmlComment);

                if (startWith && endsWith) //item.NodeType == HtmlNodeType.Comment)
                {
                    toTranslate = false;
                }
                else if (true)
                {
                    if (html == string.Empty)
                    {
                        continue;
                    }

                    endsWith = html.Contains(AspxConsts.endAspxComment);
                    startWith = html.Contains(AspxConsts.startAspxComment);
                    if (startWith || endsWith)
                    {
                        if (startWith && endsWith)
                        {
                            // contains whole aspx comment
                            toTranslate = false;
                        }
                        else
                        {

                        }
                    }

                    if (!toTranslate)
                    {
                        continue;
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



        /// <summary>
        /// Do A2 se může zadat *
        /// </summary>
        /// <param name="hn"></param>
        /// <param name="tag"></param>
        private static bool HasTagName(HtmlNode hn, string tag)
        {
            if (tag == AllStrings.asterisk)
            {
                return true;
            }
            return hn.Name == tag;
        }

        private static bool HasTagAttr(HtmlNode item, string atribut, string hodnotaAtributu, bool enoughIsContainsAttribute, bool searchAsSingleString)
        {
            if (hodnotaAtributu == AllStrings.asterisk)
            {
                return true;
            }

            bool contains = false;
            var attrValue = HtmlHelper.GetValueOfAttribute(atribut, item);

            if (enoughIsContainsAttribute)
            {
                if (searchAsSingleString)
                {
                    contains = attrValue.Contains(hodnotaAtributu);
                }
                else
                {
                    bool cont = true;
                    var p = SH.Split(hodnotaAtributu, AllStrings.space);
                    foreach (var item2 in p)
                    {
                        if (!attrValue.Contains(item2))
                        {
                            cont = false;
                            break;
                        }
                    }

                    contains = cont;
                }
            }
            else
            {
                contains = attrValue == hodnotaAtributu;
            }

            return contains;
        }



        /// <summary>
        /// A4 = if add one, return. Like Node vs Nodes
        /// It's calling by others
        /// Do A5 se může vložit *
        /// </summary>
        /// <param name="vr"></param>
        /// <param name="html"></param>
        /// <param name="p"></param>
        public static void RecursiveReturnTags(List<HtmlNode> vr, HtmlNode html, bool recursive, bool single, string p)
        {

            if (html == null)
            {
                return;
            }

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
            if (tag != textNode)
            {
                vr = TrimTexts(vr);
            }

            return vr;
        }

        public static List<HtmlNode> NodesWithAttrWorker(HtmlNode node, bool recursive, string tag, string atribut, string hodnotaAtributu, bool enoughIsContainsAttribute, bool searchAsSingleString = true)
        {
            List<HtmlNode> vr = new List<HtmlNode>();

            RecursiveReturnTagsWithContainsAttr(vr, node, recursive, tag, atribut, hodnotaAtributu, enoughIsContainsAttribute, searchAsSingleString);
            if (tag != textNode)
            {
                vr = TrimTexts(vr);
            }

            return vr;
        }

        public static HtmlDocument CreateHtmlDocument(CreateHtmlDocumentInitData d = null)
        {
            HtmlDocument hd = new HtmlDocument();

            hd.OptionOutputOriginalCase = true;
            // false - i přesto mi tag ukončený na / převede na </Page>. Musí se ještě tagy jež nechci ukončovat vymazat z HtmlAgilityPack.HtmlNode.ElementsFlags.Remove("form"); před načetním XML https://html-agility-pack.net/knowledge-base/7104652/htmlagilitypack-close-form-tag-automatically
            hd.OptionAutoCloseOnEnd = false;
            hd.OptionOutputAsXml = false;
            hd.OptionFixNestedTags = false;
            //when OptionCheckSyntax = false, raise NullReferenceException in Load/LoadHtml
            //hd.OptionCheckSyntax = false;
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
        public static void RecursiveReturnTagsWithContainsAttr(List<HtmlNode> vr, HtmlNode htmlNode, bool recursively, string p, string atribut, string hodnotaAtributu, bool enoughIsContainsAttribute, bool searchAsSingleString = true)
        {
            if (htmlNode == null)
            {
                return;
            }

            foreach (HtmlNode item in htmlNode.ChildNodes)
            {
                string attrValue = HtmlHelper.GetValueOfAttribute(atribut, item);

                if (HasTagName(item, p))
                {
                    if (HasTagAttr(item, atribut, hodnotaAtributu, enoughIsContainsAttribute, searchAsSingleString))
                    {
                        vr.Add(item);
                    }

                    if (recursively)
                    {
                        RecursiveReturnTagsWithContainsAttr(vr, item, recursively, p, atribut, hodnotaAtributu, enoughIsContainsAttribute, searchAsSingleString);
                    }
                }
                else
                {
                    if (recursively)
                    {
                        RecursiveReturnTagsWithContainsAttr(vr, item, recursively, p, atribut, hodnotaAtributu, enoughIsContainsAttribute, searchAsSingleString);
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
        /// <summary>
        /// Return null if not found
        /// </summary>
        /// <param name="node"></param>
        /// <param name="recursive"></param>
        /// <param name="tag"></param>
        /// <param name="attr"></param>
        /// <param name="attrValue"></param>
        /// <param name="contains"></param>
        /// <returns></returns>
        public static HtmlNode NodeWithAttr(HtmlNode node, bool recursive, string tag, string attr, string attrValue, bool contains = false)
        {
            return NodesWithAttrWorker(node, recursive, tag, attr, attrValue, contains).FirstOrDefault();
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
        public static List<HtmlNode> NodesWhichContainsInAttr(HtmlNode node, bool recursive, string tag, string attr, string attrValue, bool searchAsSingleString = true)
        {
            return NodesWithAttrWorker(node, recursive, tag, attr, attrValue, true, searchAsSingleString);
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
                if (CA.IsEqualToAnyElement<string>(item.ParentNode.Name, "pre"))
                {
                    continue;
                }
                var d = SH.SplitByWhiteSpaces(item.InnerText);
                bool changed = CA.ChangeContent(null, d, RegexHelper.IsUri, HtmlGenerator2.Anchor);

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
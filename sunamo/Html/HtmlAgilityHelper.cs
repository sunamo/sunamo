using System;
using System.Collections.Generic;
using System.Text;
using HtmlAgilityPack;
using System.Linq;

namespace sunamo.Html
{
    public class HtmlAgilityHelper
    {
        #region Helpers
        public static List<HtmlNode> TrimTexts(HtmlNodeCollection htmlNodeCollection)
        {
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

        public static List<HtmlNode> TrimTexts(List<HtmlNode> c2)
        {
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
            if (tag == "*")
            {
                return true;
            }
            return hn.Name == tag;
        }

        private static bool HasTagAttr(HtmlNode item, string atribut, string hodnotaAtributu, bool enoughIsContainsAttribute)
        {
            if (hodnotaAtributu == "*")
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
        /// Do A3 se může vložit *
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

        public static List<HtmlNode> NodesWithAttrWorker(HtmlNode node, bool recursive, string tag, string atribut, string hodnotaAtributu, bool enoughIsContainsAttribute )
        {
            List<HtmlNode> vr = new List<HtmlNode>();

            RecursiveReturnTagsWithContainsAttr(vr, node, recursive, tag, atribut, hodnotaAtributu, enoughIsContainsAttribute);
            vr = TrimTexts(vr);
            return vr;
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
    }
}

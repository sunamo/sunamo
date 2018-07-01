using System;
using System.Collections.Generic;
using System.Text;
using HtmlAgilityPack;
using System.Linq;

namespace sunamo.Html
{
    public class HtmlAgilityHelper
    {
        #region 1 Node
        public static HtmlNode Node(HtmlNode node, bool recursive, string tag)
        {
            //return Nodes(node, recursive, tag).FirstOrNu();
            return null;
            
        }
        #endregion

        #region 2 Nodes
        public static List<HtmlNode> Nodes(HtmlNode node, bool recursive, string tag)
        {
            return NodesWithAttr(node, recursive, tag, null, null, false);
        }
        #endregion

        #region 3 NodeWithAttr
        public static HtmlNode NodeWithAttr(HtmlNode node, bool recursive, string tag, string attr, string attrValue)
        {
            return HtmlHelper.ReturnTagsWithContainsAttrRek(node, tag, attr, attrValue, false, recursive).FirstOrDefault();
        }
        #endregion

        #region 4 NodesWithAttr
        public static List<HtmlNode> NodesWithAttr(HtmlNode node, bool recursive, string tag, string attr, string attrValue, bool contains = false) 
        {
            return HtmlHelper.ReturnTagsWithContainsAttrRek(node, tag, attr, attrValue, false, recursive);
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
            return HtmlHelper.ReturnTagsWithContainsAttrRek(node, tag, attr, attrValue, true, recursive);
        }
        #endregion
    }
}

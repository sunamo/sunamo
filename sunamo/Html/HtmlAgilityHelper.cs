using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;

namespace sunamo.Html
{
    /// <summary>
    /// Is directed pure by tag nesting and hiearchy
    /// Contains only methods which working with HtmlAgility - others are in Html5Helper
    /// Completely wrapper of chaotic HtmlHelper
    /// </summary>
    public class HtmlAgilityHelper
    {

        public static List<HtmlNode> Nodes(HtmlNode root, bool recursively, params string[] p)
        {
            if (recursively)
            {
                return HtmlHelper.ReturnAllTags(root, p);
            }
            return HtmlHelper.ReturnTags(root, p);

            /*
             * 
             * 
             * ReturnTagsRek
ReturnAllTagsImg
RecursiveReturnTags
RecursiveReturnTagsWithAttr - return result in A1
ReturnAllTags



             */
        }

        public static List<HtmlNode> NodesWithAttr(HtmlNode root, bool recursively, string tagName, string attrName, string attrValue, bool includeSubelements = false)
        {
            List<HtmlNode> result = null;

            if (recursively)
            {
                return HtmlHelper.ReturnTagsWithAttrRek(root, tagName, attrName, attrValue, includeSubelements);
            }
            return HtmlHelper.GetTagsOfAtribute(root, tagName, attrName, attrValue);

            /*
             * ReturnTagsWithAttrRek2
             * ReturnTagsWithContainsClassRek
             */
        }

        public static List<HtmlNode> NodesWhichContainsInAttr(HtmlNode root, bool recursively, string tagName, string attrName, string attrValue)
        {
            if (recursively)
            {
                return HtmlHelper.ReturnTagsWithContainsAttrRek(root, tagName, attrName, attrValue);
            }
            return HtmlHelper.ReturnTagsWithContainsAttr(root, tagName, attrName, attrValue);
        }

        public static HtmlNode Node(HtmlNode root, bool recursively, string tagName)
        {
            if (recursively)
            {
                return HtmlHelper.ReturnTagRek(root, tagName);
            }
            /*
             * GetTag
             */
            return HtmlHelper.ReturnTag(root, tagName);
            
        }

        public static HtmlNode NodeWithAttr(HtmlNode root, bool recursively, string tag, string attr, string value)
        {
            if (recursively)
            {
                return HtmlHelper.ReturnTagWithAttrRek(root, tag, attr, value);
            }
            return HtmlHelper.GetTagOfAtribute(root, tag, attr, value);
            /*
             * GetTagOfAtributeRek
             * GetTagOfAtribute
             */
        }

        
    }
}

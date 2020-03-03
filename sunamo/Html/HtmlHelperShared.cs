using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using HtmlAgilityPack;
using sunamo.Constants;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using sunamo.Values;
using sunamo.Html;
using System.Web;

public static partial class HtmlHelper
{
    /// <summary>
    /// Problematic with auto translate
    /// </summary>
    /// <param name="vstup"></param>
    
    public static HtmlNode ReturnTag(HtmlNode body, string nazevTagu)
    {
        //List<HtmlNode> html = new List<HtmlNode>();
        foreach (HtmlNode item in body.ChildNodes)
        {
            if (item.Name == nazevTagu)
            {
                return item;
            }
        }
        return null;
    }

    /// <summary>
    /// Replace A2 by A3
    /// </summary>
    /// <param name="parentNode"></param>
    /// <param name="o2"></param>
    /// <param name="nc"></param>
    public static void ReplaceChildNodeByOuterHtml(HtmlNode parentNode, string o2, HtmlNode nc)
    {
        for (int i = 0; i < parentNode.ChildNodes.Count; i++)
        {
            var item = parentNode.ChildNodes[i];
            if (item.OuterHtml == o2)
            {
                // First is new, Second is old!!!
                parentNode.ReplaceChild(nc,item);
                break;
            }
        }
    }
}
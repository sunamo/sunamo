using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;


    public static partial class HtmlHelper
    {
    public static string ConvertTextToHtml(string p)
    {
        p = p.Replace(Environment.NewLine, "<br />");
        return p;
    }

public static string PrepareToAttribute(string title)
        {
            return title.Replace('\"', '\'');
        }

    /// <summary>
    /// Pokud bude nalezen alespoň jeden tag, vrátí ho, pokud žádný, GN
    /// </summary>
    /// <param name="htmlNode"></param>
    /// <param name="tag"></param>
    /// <param name="attr"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static HtmlNode ReturnTagWithAttr(HtmlNode htmlNode, string tag, string attr, string value)
    {
        List<HtmlNode> vr = new List<HtmlNode>();
        RecursiveReturnTagWithAttr(vr, htmlNode, tag, attr, value);
        if (vr.Count > 0)
        {
            return vr[0];
        }
        return null;
    }

    public static string ReplaceAllFontCase(string r)
        {
            string za = "<br />";
            r = r.Replace("<BR />", za);
            r = r.Replace("<bR />", za);
            r = r.Replace("<Br />", za);

            r = r.Replace("<br/>", za);
            r = r.Replace("<BR/>", za);
            r = r.Replace("<bR/>", za);
            r = r.Replace("<Br/>", za);

            r = r.Replace("<br>", za);
            r = r.Replace("<BR>", za);
            r = r.Replace("<bR>", za);
            r = r.Replace("<Br>", za);
            return r;
        }

public static string ClearSpaces(string dd)
        {
            return dd.Replace("&nbsp;", " ").Replace("  ", " ");
        }

private static void RecursiveReturnTagWithAttr(List<HtmlNode> vr, HtmlNode htmlNode, string tag, string attr, string value)
        {
            foreach (HtmlNode item in htmlNode.ChildNodes)
            {
                if (item.Name == tag && HtmlHelper.GetValueOfAttribute(attr, item) == value)
                {
                    //RecursiveReturnTagWithAttr(vr, item, tag, attr, value);
                    vr.Add(item);
                    return;
                }
                else
                {
                    RecursiveReturnTagWithAttr(vr, item, tag, attr, value);
                }
            }
        }

public static string GetValueOfAttribute(string p, HtmlNode divMain, bool trim = false)
    {
        return HtmlAssistant.GetValueOfAttribute(p, divMain, trim);
    }
}
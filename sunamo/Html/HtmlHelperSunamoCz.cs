using sunamo;
using sunamo.Html;
using System;
using System.Collections.Generic;
using System.Linq;


namespace sunamo.Html
{
    public class HtmlHelperSunamoCz
    {
        public static string ConvertTextToHtmlWithAnchors(string p)
        {
            string[] d = SH.SplitNone(HtmlHelper.ConvertTextToHtml(p), ' ');
            for (int i = 0; i < d.Length; i++)
            {
                if (d[i].StartsWith("http://") || d[i].StartsWith("https://"))
                {
                    d[i] = HtmlGenerator2.AnchorWithHttp(d[i]);
                }
            }
            return SH.Join(' ', d);
        }
    }
}
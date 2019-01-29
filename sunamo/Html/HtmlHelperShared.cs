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
}


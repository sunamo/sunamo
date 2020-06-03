public partial class HtmlHelperSunamoCz{ 
public static string ConvertTextToHtmlWithAnchors(string p)
    {
        p = HtmlHelper.ConvertTextToHtml(p);
        p = p.Replace("<", " <");
        var d = SH.SplitAndKeepDelimiters(p, CA.ToList<char>( AllChars.space, AllChars.lt, AllChars.gt));
        for (int i = 0; i < d.Length(); i++)
        {
            if (d[i].StartsWith("https://") || d[i].StartsWith("https" + ":" + "//"))
            {
                var res = d[i];
                 res = HtmlGenerator2.AnchorWithHttp(res);
                d[i] = res;
            }
        }

        return SH.Join("", d);
    }
}
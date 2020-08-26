public partial class HtmlHelperSunamoCz{ 
public static string ConvertTextToHtmlWithAnchors(string p)
    {
        p = HtmlHelper.ConvertTextToHtml(p);
        p = p.Replace("<", " <");
        var d = SH.SplitAndKeepDelimiters(p, CA.ToList<char>( AllChars.space, AllChars.lt, AllChars.gt));
        for (int i = 0; i < d.Length(); i++)
        {
            var item = d[i].Trim();
            if (item.StartsWith("https://") || item.StartsWith("https://") || item.StartsWith("www."))
            {
                var res = item;
                res = HtmlGenerator2.AnchorWithHttp(res);
                d[i] = AllStrings.space + res + AllStrings.space;
            }
        }

        return SH.Join("", d);
    }
}
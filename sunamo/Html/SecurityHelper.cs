public static class SecurityHelper
{

    public static string TreatHtmlCode(string r)
    {
        r = RemoveJsAttributesFromEveryNode(r);
        r = SH.ReplaceAll2(r, " ", "  ");
        r = RegexHelper.rHtmlScript.Replace(r, "");
        r = RegexHelper.rHtmlComment.Replace(r, "");
        
        return r;
    }

    public static string RemoveJsAttributesFromEveryNode(string html)
    {
        var document = new HtmlAgilityPack.HtmlDocument();
        document.LoadHtml(html);
        foreach (var eachNode in document.DocumentNode.SelectNodes("//*"))
        {
            foreach (var item in eachNode.Attributes)
            {
                if (item.Name.ToLower().StartsWith("on"))
                {
                    item.Remove();
                }
                else if (item.Value.ToLower().Trim().StartsWith("javascript:"))
                {
                    item.Remove();
                }
            }
        }
        html = document.DocumentNode.OuterHtml;
        return html;
    }
}

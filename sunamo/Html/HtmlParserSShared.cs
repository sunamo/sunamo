using HtmlAgilityPack;
using sunamo.Html;
using System.IO;
using System.Net;

public static partial class HtmlDocumentS{
    private static string s_html2 = null;
    public static HtmlNode LoadHtml(string html)
    {
        HtmlDocument hd = HtmlAgilityHelper.CreateHtmlDocument();
        //hd.Encoding = Encoding.UTF8;
        html = WebUtility.HtmlDecode(html);
        s_html2 = html;
        //HtmlHelper.ToXml(html)
        hd.LoadHtml(html);
        return hd.DocumentNode;
    }
}
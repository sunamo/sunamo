using HtmlAgilityPack;
using System.IO;
using System.Net;

public static class HtmlParserS
{
    static string html2 = null;

    public static HtmlNode Load(string path)
    {
        HtmlDocument hd = new HtmlDocument();
        //hd.Encoding = Encoding.UTF8;
        html2 = File.ReadAllText(path);
        html2 = WebUtility.HtmlDecode(html2);
        //string html =HtmlHelper.ToXml(); 
        hd.LoadHtml(html2);
        return hd.DocumentNode;
    }
    
    public static HtmlNode LoadHtml(string html)
    {
        HtmlDocument hd = new HtmlDocument();
        //hd.Encoding = Encoding.UTF8;
        html = WebUtility.HtmlDecode(html);
        html2 = html;
        //HtmlHelper.ToXml(html)
        hd.LoadHtml(html);
        return hd.DocumentNode;
    }
}

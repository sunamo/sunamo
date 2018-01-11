using HtmlAgilityPack;
using sunamo;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Xml;

public class HtmlParser
{
    HtmlDocument hd = new HtmlDocument();
    string html = null;

    public void Load(string path)
    {
        //hd.Encoding = Encoding.UTF8;
        html = File.ReadAllText(path);
        html = WebUtility.HtmlDecode(html);
        //string html =HtmlHelper.ToXml(); 
        hd.LoadHtml(html);
    }
    
    public void LoadHtml(string html)
    {
        //hd.Encoding = Encoding.UTF8;
        html = WebUtility.HtmlDecode(html);
        this.html = html;
        //HtmlHelper.ToXml(html)
        hd.LoadHtml(html);
    }

    public HtmlNode DocumentNode
    {
        get
        {
            return hd.DocumentNode;
        }
    }

    public string ToXml()
    {
        //return html;
        StringWriter sw = new StringWriter();
        XmlWriter tw = XmlWriter.Create(sw);
        DocumentNode.WriteTo(tw);
        sw.Flush();
        //sw.Close();
        sw.Dispose();

        return sw.ToString().Replace("<?xml version=\"1.0\" encoding=\"iso-8859-2\"?>", "");
    }
}

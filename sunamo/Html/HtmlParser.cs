using HtmlAgilityPack;
using sunamo;
using sunamo.Html;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Xml;

/// <summary>
/// Is 2, never use HtmlDocument!!! have too many methods. 
/// </summary>
public class HtmlDocument2
{
    private HtmlDocument _hd = HtmlAgilityHelper.CreateHtmlDocument();
    private string _html = null;

    public void Load(string path)
    {
        //hd.Encoding = Encoding.UTF8;
        _html = File.ReadAllText(path);
        _html = WebUtility.HtmlDecode(_html);
        //string html =HtmlHelper.ToXml(); 
        _hd.LoadHtml(_html);
    }

    public void LoadHtml(string html)
    {
        //hd.Encoding = Encoding.UTF8;
        html = WebUtility.HtmlDecode(html);
        _html = html;
        //HtmlHelper.ToXml(html)
        _hd.LoadHtml(html);
    }

    public HtmlNode DocumentNode
    {
        get
        {
            return _hd.DocumentNode;
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

    #region Without HtmlAgility
    #region ToXml
    public string ToXmlFinal(string xml)
    {
        return HtmlHelper.ToXmlFinal(xml);
    }

    /// <summary>
    /// Již volá ReplaceHtmlNonPairTagsWithXmlValid
    /// </summary>
    /// <param name="xml"></param>
    /// <param name="odstranitXmlDeklaraci"></param>
    
    public List<HtmlNode> ReturnTagsWithContainsClassRek(HtmlNode htmlNode, string tag, string t)
    {
        return HtmlHelper.ReturnTagsWithContainsClassRek(htmlNode, tag, t);
    }
    #endregion
}

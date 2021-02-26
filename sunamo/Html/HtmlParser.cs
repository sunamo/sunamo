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
        _html = TF.ReadAllText(path);
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
    public string ToXml(string xml, bool odstranitXmlDeklaraci)
    {
        return HtmlHelper.ToXml(xml, odstranitXmlDeklaraci);
    }

    /// <summary>
    /// Již volá RemoveXmlDeclaration i ReplaceHtmlNonPairTagsWithXmlValid
    /// </summary>
    /// <param name="xml"></param>
    public string ToXml(string xml)
    {
        return HtmlHelper.ToXml(xml);
    }
    #endregion

    public string ClearSpaces(string dd)
    {
        return HtmlHelper.ClearSpaces(dd);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="html"></param>
    /// <param name="nameTag"></param>
    public string TrimOpenAndEndTags(string html, string nameTag)
    {
        return HtmlHelper.TrimOpenAndEndTags(html, nameTag);
    }

    public string ConvertTextToHtml(string p)
    {
        return HtmlHelper.ConvertTextToHtml(p);
    }

    public string PrepareToAttribute(string title)
    {
        return HtmlHelper.PrepareToAttribute(title);
    }

    /// <summary>
    /// Před zavoláním této metody musí být v A1 převedeny bílé znaky na mezery - pouze tak budou označeny všechny výskyty daných slov
    /// </summary>
    /// <param name="celyObsah"></param>
    /// <param name="maxPocetPismenNaVetu"></param>
    /// <param name="pocetVet"></param>
    /// <param name="hledaneSlova"></param>
    public string HighlightingWords(string celyObsah, int maxPocetPismenNaVetu, int pocetVet, List<string> hledaneSlova)
    {
        return HtmlHelper.HighlightingWords(celyObsah, maxPocetPismenNaVetu, pocetVet, hledaneSlova);
    }

    public string ReplaceAllFontCase(string r)
    {
        return HtmlHelper.ReplaceAllFontCase(r);
    }

    public string ReplaceHtmlNonPairTagsWithXmlValid(string vstup)
    {
        return HtmlHelper.ReplaceHtmlNonPairTagsWithXmlValid(vstup);
    }

    #region Strip

    /// <summary>
    /// Nahradí každý text <*> za SE. Vnitřní ne-xml obsah nechá být.
    /// </summary>
    /// <param name="p"></param>
    public string StripAllTags(string p)
    {
        return HtmlHelper.StripAllTags(p);
    }

    public string StripAllTags(string p, string replaceFor)
    {
        return HtmlHelper.StripAllTags(p, replaceFor);
    }

    /// <summary>
    /// Strip all tags and return only 
    /// </summary>
    /// <param name="d"></param>
    public List<string> StripAllTagsList(string d)
    {
        return HtmlHelper.StripAllTagsList(d);
    }

    /// <summary>
    /// Nahradí každý text <*> za mezeru. Vnitřní ne-xml obsah nechá být.
    /// </summary>
    /// <param name="p"></param>
    public string StripAllTagsSpace(string p)
    {
        return HtmlHelper.StripAllTagsSpace(p);
    }

    /// <summary>
    /// Jen volá metodu StripAllTags
    /// Nahradí každý text <*> za SE. Vnitřní ne-xml obsah nechá být.
    /// </summary>
    /// <param name="p"></param>
    public string RemoveAllTags(string p)
    {
        return HtmlHelper.RemoveAllTags(p);
    }
    #endregion
    #endregion

    #region Set
    /// <summary>
    /// 
    /// </summary>
    /// <param name="node"></param>
    /// <param name="atr"></param>
    /// <param name="hod"></param>
    public void SetAttribute(HtmlNode node, string atr, string hod)
    {
        HtmlHelper.SetAttribute(node, atr, hod);
    }
    #endregion

    #region Get

    public string GetValueOfAttribute(string p, HtmlNode divMain, bool _trim = false)
    {
        return HtmlHelper.GetValueOfAttribute(p, divMain, _trim);
    }
    #endregion

    #region Check name and value


    public bool HasTagAttrContains(HtmlNode htmlNode, string delimiter, string attr, string value)
    {
        return HtmlHelper.HasTagAttrContains(htmlNode, delimiter, attr, value);
    }
    #endregion

    #region Helper methods

    public HtmlNode TrimNode(HtmlNode hn2)
    {
        return HtmlHelper.TrimNode(hn2);
    }

    #region Text nodes
    public List<HtmlNode> GetWithoutTextNodes(HtmlNode htmlNode)
    {
        return HtmlHelper.GetWithoutTextNodes(htmlNode);
    }

    public List<HtmlNode> TrimTexts(HtmlNodeCollection htmlNodeCollection)
    {
        return HtmlHelper.TrimTexts(htmlNodeCollection);
    }

    /// <summary>
    /// Used in ParseChromeAPIs. 
    /// </summary>
    /// <param name="api_reference"></param>
    public List<HtmlNode> ReturnTags(HtmlNode api_reference)
    {
        return null;
    }

    public List<HtmlNode> TrimTexts(List<HtmlNode> c2)
    {
        return HtmlHelper.TrimTexts(c2);
    }
    #endregion

    public bool HasChildTag(HtmlNode spanInHeader, string v)
    {
        return HtmlHelper.HasChildTag(spanInHeader, v);
    }



    #region Apply
    /// <summary>
    /// Nehodí se na vrácení obsahu celé stránky
    /// A1 je zdrojový kód celé stránky
    /// 
    /// </summary>
    /// <param name="s"></param>
    /// <param name="p"></param>
    /// <param name="ssh"></param>
    /// <param name="value"></param>
    public string ReturnApplyToAllTags(string s, string p, EditHtmlWidthHandler ssh, string value)
    {
        return HtmlHelper.ReturnApplyToAllTags(s, p, ssh, value);
    }

    #endregion





    public Dictionary<string, string> GetValuesOfStyle(HtmlNode item)
    {
        return HtmlHelper.GetValuesOfStyle(item);
    }




    #endregion


    #region 1 Node
    public HtmlNode GetTag(HtmlNode cacheAuthorNode, string p)
    {
        return HtmlHelper.GetTag(cacheAuthorNode, p);
    }

    /// <summary>
    /// Prochází děti A1 a pokud některý má název A2, G
    /// Vrátí null pokud se takový tag nepodaří najít
    /// </summary>
    /// <param name="body"></param>
    /// <param name="nazevTagu"></param>
    public HtmlNode ReturnTag(HtmlNode body, string nazevTagu)
    {
        return HtmlHelper.ReturnTag(body, nazevTagu);
    }

    public HtmlNode ReturnTagRek(HtmlNode hn, string nameTag)
    {
        return HtmlHelper.ReturnTagRek(hn, nameTag);
    }
    #endregion

    #region 2 Nodes - recursive



    /// <summary>
    /// Vratilo 15 namisto 10
    /// Používá metodu RecursiveReturnTags
    /// Do A2 se může vložit *
    /// </summary>
    /// <param name="htmlNode"></param>
    /// <param name="tag"></param>
    public List<HtmlNode> ReturnTagsRek(HtmlNode htmlNode, string tag)
    {
        return HtmlHelper.ReturnTagsRek(htmlNode, tag);
    }

    /// <summary>
    /// Vratilo 0 misto 10
    /// A1 je uzel který se bude rekurzivně porovnávat
    /// A2 je název tagu(div, a, atd.) které chci vrátit.
    /// </summary>
    /// <param name="htmlNode"></param>
    /// <param name="p"></param>
    public List<HtmlNode> ReturnAllTags(HtmlNode htmlNode, params string[] p)
    {
        return HtmlHelper.ReturnAllTags(htmlNode, p);
    }

    /// <summary>
    /// Return 0 instead of 10
    /// If tag is A2, don't apply recursive on that
    /// A2 je název tagu, napříkald img
    /// </summary>
    /// <param name="html"></param>
    /// <param name="p"></param>
    public List<HtmlNode> ReturnAllTagsImg(HtmlNode html, string p)
    {
        return HtmlHelper.ReturnAllTagsImg(html, p);
    }
    #endregion

    #region 2 Nodes - no recursive
    /// <summary>
    /// Do A2 se může vložit * ale nemělo by to moc smysl
    /// </summary>
    /// <param name="parent"></param>
    /// <param name="tag"></param>
    public List<HtmlNode> ReturnTags(HtmlNode parent, string tag)
    {
        return HtmlHelper.ReturnTags(parent, tag);
    }
    #endregion

    #region 3 NodeWithAttr
    public HtmlNode GetTagOfAtributeRek(HtmlNode hn, string nameTag, string nameAtr, string valueOfAtr)
    {
        return HtmlHelper.GetTagOfAtributeRek(hn, nameTag, nameAtr, valueOfAtr);
    }

    public HtmlNode GetTagOfAtribute(HtmlNode hn, string nameTag, string nameAtr, string valueOfAtr)
    {
        return HtmlHelper.GetTagOfAtribute(hn, nameTag, nameAtr, valueOfAtr);
    }

    /// <summary>
    /// Pokud bude nalezen alespoň jeden tag, vrátí ho, pokud žádný, GN
    /// </summary>
    /// <param name="htmlNode"></param>
    /// <param name="tag"></param>
    /// <param name="attr"></param>
    /// <param name="value"></param>
    public HtmlNode ReturnTagWithAttr(HtmlNode htmlNode, string tag, string attr, string value)
    {
        return HtmlHelper.ReturnTagWithAttr(htmlNode, tag, attr, value);
    }


    #endregion



    #region 4 NodesWithAttr


    #region Only call other method
    /// <summary>
    /// Do A2 se může zadat * pro získaní všech tagů
    /// Do A4 se může vložit * pro vrácení tagů s hledaným atributem s jakoukoliv hodnotou
    /// </summary>
    /// <param name="htmlNode"></param>
    /// <param name="tag"></param>
    /// <param name="atribut"></param>
    /// <param name="hodnotaAtributu"></param>
    public List<HtmlNode> ReturnTagsWithAttrRek(HtmlNode htmlNode, string tag, string atribut, string hodnotaAtributu)
    {
        return HtmlHelper.ReturnTagsWithAttrRek(htmlNode, tag, atribut, hodnotaAtributu);
    }

    /// <summary>
    /// G null když tag nebude nalezen
    /// </summary>
    /// <param name="htmlNode"></param>
    /// <param name="tag"></param>
    /// <param name="attr"></param>
    /// <param name="value"></param>
    public HtmlNode ReturnTagWithAttrRek(HtmlNode htmlNode, string tag, string attr, string value)
    {
        return HtmlHelper.ReturnTagWithAttrRek(htmlNode, tag, attr, value);
    }
    #endregion

    /// <summary>
    /// Return 0 instead of 10
    /// Originally from HtmlDocument
    /// </summary>
    /// <param name="htmlNode"></param>
    /// <param name="tagName"></param>
    /// <param name="attrName"></param>
    /// <param name="attrValue"></param>
    public List<HtmlNode> ReturnTagsWithAttrRek2(HtmlNode htmlNode, string tagName, string attrName, string attrValue)
    {
        return HtmlHelper.ReturnTagsWithAttrRek2(htmlNode, tagName, attrName, attrValue);
    }



    #region No recursive
    /// <summary>
    /// 
    /// </summary>
    /// <param name="hn"></param>
    /// <param name="nameTag"></param>
    /// <param name="nameAtr"></param>
    /// <param name="valueOfAtr"></param>
    public List<HtmlNode> GetTagsOfAtribute(HtmlNode hn, string nameTag, string nameAtr, string valueOfAtr)
    {
        return HtmlHelper.GetTagsOfAtribute(hn, nameAtr, nameAtr, valueOfAtr);
    }
    #endregion

    #endregion

    #region 5 NodesWhichContainsInAttr


    /// <summary>
    /// Do A3 se může zadat * pro vrácení všech tagů
    /// </summary>
    /// <param name="vr"></param>
    /// <param name="htmlNode"></param>
    /// <param name="p"></param>
    /// <param name="atribut"></param>
    /// <param name="hodnotaAtributu"></param>
    public void RecursiveReturnTagsWithContainsAttr(List<HtmlNode> vr, HtmlNode htmlNode, string p, string atribut, string hodnotaAtributu, bool contains, bool recursively)
    {
        HtmlHelper.RecursiveReturnTagsWithContainsAttr(vr, htmlNode, p, atribut, hodnotaAtributu, contains, recursively);
    }

    /// <summary>
    /// Do A2 se může zadat * pro získaní všech tagů
    /// </summary>
    /// <param name="htmlNode"></param>
    /// <param name="tag"></param>
    /// <param name="atribut"></param>
    /// <param name="hodnotaAtributu"></param>
    public List<HtmlNode> ReturnTagsWithContainsAttrRek(HtmlNode htmlNode, string tag, string atribut, string hodnotaAtributu)
    {
        return HtmlHelper.ReturnTagsWithContainsAttrRek(htmlNode, tag, atribut, hodnotaAtributu);
    }

    public List<HtmlNode> ReturnTagsWithContainsAttrRek(HtmlNode htmlNode, string tag, string atribut, string hodnotaAtributu, bool contains, bool recursively)
    {
        return HtmlHelper.ReturnTagsWithContainsAttrRek(htmlNode, tag, atribut, hodnotaAtributu, contains, recursively);
    }

    /// <summary>
    /// Do A2 se může zadat * pro získaní všech tagů
    /// </summary>
    /// <param name="htmlNode"></param>
    /// <param name="tag"></param>
    /// <param name="atribut"></param>
    /// <param name="hodnotaAtributu"></param>
    public List<HtmlNode> ReturnTagsWithContainsClassRek(HtmlNode htmlNode, string tag, string t)
    {
        return HtmlHelper.ReturnTagsWithContainsClassRek(htmlNode, tag, t);
    }
    #endregion
}
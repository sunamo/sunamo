using HtmlAgilityPack;
using sunamo.Constants;
using sunamo.Html;
using sunamo.Xml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;


    public static partial class HtmlHelper
    {
        public static string ToXmlFinal(string xml)
        {
            xml = HtmlHelper.ReplaceHtmlNonPairTagsWithXmlValid(xml);
            xml = XH.RemoveXmlDeclaration(xml);
            return "<?xml version=\"1.0\" encoding=\"utf-8\" ?>" + HtmlHelper.ReplaceHtmlNonPairTagsWithXmlValid(XH.RemoveXmlDeclaration(xml.Replace("<?xml version=\"1.0\" encoding=\"iso-8859-2\" />", "").Replace("<?xml version=\"1.0\" encoding=\"utf-8\" />", "").Replace("<?xml version=\"1.0\" encoding=\"UTF-8\" />", "")));
        }

        /// <summary>
        /// Již volá ReplaceHtmlNonPairTagsWithXmlValid
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="odstranitXmlDeklaraci"></param>
        /// <returns></returns>
        public static string ToXml(string xml, bool odstranitXmlDeklaraci)
        {
            HtmlDocument doc = HtmlAgilityHelper.CreateHtmlDocument();
            //doc.Encoding = Encoding.UTF8;
            doc.LoadHtml(xml);
            StringWriter sw = new StringWriter();
            XmlWriter tw = XmlWriter.Create(sw);
            doc.DocumentNode.WriteTo(tw);
            tw.Flush();
            sw.Flush();
            string vr = sw.ToString();
            if (odstranitXmlDeklaraci)
            {
                vr = XH.RemoveXmlDeclaration(vr);
            }
            vr = HtmlHelper.ReplaceHtmlNonPairTagsWithXmlValid(vr);
            return vr;
        }

        /// <summary>
        /// Již volá RemoveXmlDeclaration i ReplaceHtmlNonPairTagsWithXmlValid
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static string ToXml(string xml)
        {
            return ToXml(xml, true);
        }



        public static string ReplaceHtmlNonPairTagsWithXmlValid(string vstup)
        {
            List<string> jizNahrazeno = new List<string>();
            var pol = CA.ToEnumerable("br", "hr", "img", "meta", "input", "iframe");
            MatchCollection mc = Regex.Matches(vstup, "<(?:\"[^\"]*\"['\"]*|'[^']*'['\"]*|[^'\">])+>");
            List<string> col = new List<string>(pol);

            //<(?:"[^"]*"['"]*|'[^']*'['"]*|[^'">])+>
            foreach (Match item in mc)
            {
                string d = item.Value.Replace(" >", ">");
                string tag = "";
                if (item.Value.Contains(" "))
                {
                    tag = SH.GetFirstPartByLocation(item.Value, ' ');
                }
                else
                {
                    tag = d.Replace("/", "").Replace(">", "");
                }
                tag = tag.TrimStart('<').Trim().ToLower();
                if (col.Contains(tag))
                {
                    if (!item.Value.Contains("/>"))
                    {
                        if (!jizNahrazeno.Contains(item.Value))
                        {
                            jizNahrazeno.Add(item.Value);
                            string nc = item.Value.Substring(0, item.Value.Length - 1) + " />";
                            vstup = vstup.Replace(item.Value, nc);
                        }
                    }
                }


            }
            return vstup;
        }

        /// <summary>
        /// Strip all tags and return only 
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static List<string> StripAllTagsList(string d)
        {
            string replaced = StripAllTags(d, AllStrings.doubleSpace);
            return CA.ToListString(SH.Split(replaced, AllStrings.doubleSpace));
        }

        /// <summary>
        /// Nahradí každý text <*> za mezeru. Vnitřní ne-xml obsah nechá být.
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static string StripAllTagsSpace(string p)
        {
            return Regex.Replace(p, @"<[^>]*>", " ");
        }

        /// <summary>
        /// Jen volá metodu StripAllTags
        /// Nahradí každý text <*> za SE. Vnitřní ne-xml obsah nechá být.
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static string RemoveAllTags(string p)
        {
            return StripAllTags(p);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <param name="atr"></param>
        /// <param name="hod"></param>
        public static void SetAttribute(HtmlNode node, string atr, string hod)
        {

        object o = null;
            while (true)
            {
                o = node.Attributes.FirstOrDefault(a => a.Name == atr);
                if (o != null)
                {
                    node.Attributes.Remove((HtmlAttribute)o);
                }
                else
                {
                    break;
                }
            }
            node.Attributes.Add(atr, hod);
        }


    private static bool HasTagAttr(HtmlNode item, string atribut, string hodnotaAtributu, bool enoughIsContainsAttribute)
        {
            if (hodnotaAtributu == "*")
            {
                return true;
            }
            bool contains = false;
            var attrValue = HtmlHelper.GetValueOfAttribute(atribut, item) ;
            if (enoughIsContainsAttribute)
            {
                contains = attrValue.Contains(hodnotaAtributu);
            }
            else
            {
                contains = attrValue == hodnotaAtributu;
            }
            return contains;
        }

    /// <summary>
    /// Used in ParseChromeAPIs. Searched everywhere, DNF
    /// </summary>
    /// <param name="item2"></param>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <param name="v3"></param>
    /// <returns></returns>
    public static HtmlNode ReturnTagOfAtribute(HtmlNode item2, string v1, string v2, string v3)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Do A2 se může zadat *
    /// </summary>
    /// <param name="hn"></param>
    /// <param name="tag"></param>
    /// <returns></returns>
    private static bool HasTagName(HtmlNode hn, string tag)
        {
            if (tag == "*")
            {
                return true;
            }
            return hn.Name == tag;
        }

        public static bool HasTagAttrContains(HtmlNode htmlNode, string delimiter, string attr, string value)
        {
            string attrValue = HtmlHelper.GetValueOfAttribute(attr, htmlNode);
            var spl = SH.Split(attrValue, delimiter);
            return spl.Contains(value);
        }

    /// <summary>
    /// Have also override with List<HtmlNode>
    /// </summary>
    /// <param name="htmlNodeCollection"></param>
    /// <returns></returns>
    public static List<HtmlNode> TrimTexts(HtmlNodeCollection htmlNodeCollection)
        {
            List<HtmlNode> vr = new List<HtmlNode>();
            foreach (var item in htmlNodeCollection)
            {
                if (item.Name != "#text")
                {
                    vr.Add(item);
                }
            }
            return vr;
        }

        public static List<HtmlNode> TrimTexts(List<HtmlNode> c2)
        {
            List<HtmlNode> vr = new List<HtmlNode>();
            foreach (var item in c2)
            {
                if (item.Name != "#text")
                {
                    vr.Add(item);
                }
            }
            return vr;
        }

        public static bool HasChildTag(HtmlNode spanInHeader, string v)
        {
            return ReturnTags(spanInHeader, v).Count != 0;
        }

    /// <summary>
    /// Used in ParseChromeAPIs. Nowhere is executed
    /// </summary>
    /// <param name="dd2"></param>
    /// <param name="v"></param>
    /// <returns></returns>
    public static string ReturnInnerTextOfTagsRek(HtmlNode dd2, string v)
    {
        throw new NotImplementedException();
    }



    /// <summary>
    /// Nehodí se na vrácení obsahu celé stránky
    /// A1 je zdrojový kód celé stránky
    /// 
    /// </summary>
    /// <param name="s"></param>
    /// <param name="p"></param>
    /// <param name="ssh"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string ReturnApplyToAllTags(string s, string p, EditHtmlWidthHandler ssh, string value)
        {
            List<HtmlNode> vr = new List<HtmlNode>();
            HtmlDocument doc = HtmlAgilityHelper.CreateHtmlDocument();
            //hd.Encoding = Encoding.UTF8;
            doc.LoadHtml(s);
            HtmlNode htmlNode = doc.DocumentNode;
            RecursiveApplyToAllTags(vr, ref htmlNode, p, ssh, value);
            return htmlNode.OuterHtml;
            ;
        }

        /// <summary>
        /// A1 je kolekce uzlů na které jsem zavolal A4
        /// A2 je referencovaný uzel, do kterého se změny přímo projevují
        /// A3 je název tagu který se hledá(div, a, atd.)
        /// A4 je samotná metoda která bude provádět změny
        /// A5 je volitelný parametr do A4
        /// </summary>
        /// <param name="vr"></param>
        /// <param name="html"></param>
        /// <param name="p"></param>
        /// <param name="ssh"></param>
        /// <param name="value"></param>
        private static void RecursiveApplyToAllTags(List<HtmlNode> vr, ref HtmlNode html, string p, EditHtmlWidthHandler ssh, string value)
        {
            for (int i = 0; i < html.ChildNodes.Count; i++)
            {
                HtmlNode item = html.ChildNodes[i];
                if (item.Name == p)
                {
                    RecursiveApplyToAllTags(vr, ref item, p, ssh, value);
                    if (!vr.Contains(item))
                    {
                        vr.Add(item);

                        string d = ssh.Invoke(ref item, value);
                    }
                }
                else
                {
                    RecursiveApplyToAllTags(vr, ref item, p, ssh, value);
                }
            }
        }

        

        

        public static Dictionary<string, string> GetValuesOfStyle(HtmlNode item)
        {
            Dictionary<string, string> vr = new Dictionary<string, string>();
            string at = GetValueOfAttribute("style", item);
            if (at.Contains(";"))
            {
                var d = SH.Split(at, ";");
                foreach (string item2 in d)
                {
                    if (item2.Contains(":"))
                    {
                        var r = SH.SplitNone(item2, ":");
                        vr.Add(r[0].Trim().ToLower(), r[1].Trim().ToLower());
                    }
                }
            }
            return vr;
        }

        

        


        public static HtmlNode GetTag(HtmlNode cacheAuthorNode, string p)
        {
            foreach (HtmlNode item in cacheAuthorNode.ChildNodes)
            {
                if (item.OriginalName == p)
                {
                    return item;
                }
            }
            return null;
        }

    public static HtmlNode ReturnNextSibling(HtmlNode h4Callback, string v)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Prochází děti A1 a pokud některý má název A2, G
    /// Vrátí null pokud se takový tag nepodaří najít
    /// </summary>
    /// <param name="body"></param>
    /// <param name="nazevTagu"></param>
    /// <returns></returns>
    public static HtmlNode ReturnTag(HtmlNode body, string nazevTagu)
        {
            //List<HtmlNode> html = new List<HtmlNode>();
            foreach (HtmlNode item in body.ChildNodes)
            {
                if (item.Name == nazevTagu)
                {
                    return item;
                }
            }
            return null;
        }

        public static HtmlNode ReturnTagRek(HtmlNode hn, string nameOfTag)
        {
            hn = HtmlHelper.TrimNode(hn);
            foreach (HtmlNode var in hn.ChildNodes)
            {
                HtmlNode hn2 = var;//.FirstChild;
                foreach (HtmlNode item2 in var.ChildNodes)
                {
                    if (item2.Name == nameOfTag)
                    {
                        return item2;
                    }
                    HtmlNode hn3 = ReturnTagRek(item2, nameOfTag);
                    if (hn3 != null)
                    {
                        return hn3;
                    }
                }
                if (hn2.Name == nameOfTag)
                {
                    return hn2;
                }
            }
            return null;
        }


    /// <summary>
    /// Method with just single parameter are used in ParseChromeAPIs
    /// </summary>
    /// <param name="dlOuter"></param>
    /// <returns></returns>
    public static List<HtmlNode> ReturnTags(HtmlNode dlOuter)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Rekurzivně volá metodu RecursiveReturnAllTags
    /// </summary>
    /// <param name="vr"></param>
    /// <param name="html"></param>
    /// <param name="p"></param>
    private static void RecursiveReturnAllTags(List<HtmlNode> vr, HtmlNode html, params string[] p)
        {
            foreach (HtmlNode item in html.ChildNodes)
            {
                bool contains = false;

                if (p.Length == 1)
                {
                    if (item.Name == p[0])
                    {
                        contains = true;
                    }
                }
                else
                {
                    foreach (var t in p)
                    {
                        if (item.Name == t)
                        {
                            contains = true;
                        }
                    }
                }

                if (contains)
                {
                    RecursiveReturnAllTags(vr, item, p);
                    if (!vr.Contains(item))
                    {
                        vr.Add(item);
                    }
                }
                else
                {
                    RecursiveReturnAllTags(vr, item, p);
                }
            }
        }

        /// <summary>
        /// It's calling by others
        /// Do A3 se může vložit *
        /// </summary>
        /// <param name="vr"></param>
        /// <param name="html"></param>
        /// <param name="p"></param>
        private static void RecursiveReturnTags(List<HtmlNode> vr, HtmlNode html, string p)
        {
            foreach (HtmlNode item in html.ChildNodes)
            {
                if (HasTagName(item, p))
                {
                    //RecursiveReturnTags(vr, item, p);
                   
                        vr.Add(item);
                    
                    RecursiveReturnTags(vr, item, p);
                }
                else
                {
                    RecursiveReturnTags(vr, item, p);
                }
            }
        }

        /// <summary>
        /// Vratilo 15 namisto 10
        /// Používá metodu RecursiveReturnTags
        /// Do A2 se může vložit *
        /// </summary>
        /// <param name="htmlNode"></param>
        /// <param name="tag"></param>
        /// <returns></returns>
        public static List<HtmlNode> ReturnTagsRek(HtmlNode htmlNode, string tag)
        {
            List<HtmlNode> vr = new List<HtmlNode>();
            RecursiveReturnTags(vr, htmlNode, tag);
            vr = TrimTexts(vr);
            return vr;
        }

        /// <summary>
        /// Vratilo 0 misto 10
        /// A1 je uzel který se bude rekurzivně porovnávat
        /// A2 je název tagu(div, a, atd.) které chci vrátit.
        /// </summary>
        /// <param name="htmlNode"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public static List<HtmlNode> ReturnAllTags(HtmlNode htmlNode, params string[] p)
        {
            List<HtmlNode> vr = new List<HtmlNode>();
            RecursiveReturnAllTags(vr, htmlNode, p);
            return vr;
        }

        /// <summary>
        /// Return 0 instead of 10
        /// If tag is A2, don't apply recursive on that
        /// A2 je název tagu, napříkald img
        /// </summary>
        /// <param name="html"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public static List<HtmlNode> ReturnAllTagsImg(HtmlNode html, string p)
        {
            List<HtmlNode> vr = new List<HtmlNode>();
            foreach (HtmlNode item in html.ChildNodes)
            {
                if (item.Name == p)
                {
                    HtmlNode node = item.ParentNode;
                    if (node != null)
                    {
                        vr.Add(item);
                    }
                }
                else
                {
                    vr.AddRange(ReturnAllTags(item, p));
                }
            }
            return vr;
        }

        /// <summary>
        /// Do A2 se může vložit * ale nemělo by to moc smysl
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="tag"></param>
        /// <returns></returns>
        public static List<HtmlNode> ReturnTags(HtmlNode parent, string tag)
        {
            List<HtmlNode> vr = new List<HtmlNode>();

            foreach (var item in parent.ChildNodes)
            {
                if (HasTagName(item, tag))
                {
                    vr.Add(item);
                }

            }

            return vr;
        }

        public static HtmlNode GetTagOfAtribute(HtmlNode hn, string nameOfTag, string nameOfAtr, string valueOfAtr)
        {
            hn = HtmlHelper.TrimNode(hn);
            foreach (HtmlNode var in hn.ChildNodes)
            {
                //var.InnerHtml = var.InnerHtml.Trim();
                HtmlNode hn2 = var;//.FirstChild;
                if (hn2.Name == nameOfTag)
                {
                    if (HtmlHelper.GetValueOfAttribute(nameOfAtr, hn2) == valueOfAtr)
                    {
                        return hn2;
                    }
                    foreach (HtmlNode var2 in hn2.ChildNodes)
                    {
                        if (HtmlHelper.GetValueOfAttribute(nameOfAtr, var2) == valueOfAtr)
                        {
                            return var2;
                        }
                    }

                    //}
                }
            }
            return null;
        }






        /// <summary>
        /// Do A2 se může zadat * pro získaní všech tagů
        /// Do A4 se může vložit * pro vrácení tagů s hledaným atributem s jakoukoliv hodnotou
        /// </summary>
        /// <param name="htmlNode"></param>
        /// <param name="tag"></param>
        /// <param name="atribut"></param>
        /// <param name="hodnotaAtributu"></param>
        /// <returns></returns>
        public static List<HtmlNode> ReturnTagsWithAttrRek(HtmlNode htmlNode, string tag, string atribut, string hodnotaAtributu)
        {
            List<HtmlNode> vr = new List<HtmlNode>();

            RecursiveReturnTagsWithAttr(vr, htmlNode, tag, atribut, hodnotaAtributu);
            return vr;
        }

        /// <summary>
        /// G null když tag nebude nalezen
        /// </summary>
        /// <param name="htmlNode"></param>
        /// <param name="tag"></param>
        /// <param name="attr"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static HtmlNode ReturnTagWithAttrRek(HtmlNode htmlNode, string tag, string attr, string value)
        {
            return ReturnTagWithAttr(htmlNode, tag, attr, value);
        }

        /// <summary>
        /// Return 0 instead of 10
        /// Originally from HtmlDocument
        /// </summary>
        /// <param name="htmlNode"></param>
        /// <param name="tagName"></param>
        /// <param name="attrName"></param>
        /// <param name="attrValue"></param>
        /// <returns></returns>
        public static List<HtmlNode> ReturnTagsWithAttrRek2(HtmlNode htmlNode, string tagName, string attrName, string attrValue)
        {
            List<HtmlNode> node = new List<HtmlNode>();
            RecursiveReturnAllTags(node, htmlNode, tagName);
            for (int i = node.Count - 1; i >= 0; i--)
            {
                if (GetValueOfAttribute(attrName, node[i]) != attrValue)
                {
                    node.RemoveAt(i);
                }
            }
            return node;
        }

        /// <summary>
        /// Do A3 se může zadat * pro vrácení všech tagů
        /// 
        /// Do A5 se může vložit * pro vrácení tagů s hledaným atributem s jakoukoliv hodnotou
        /// </summary>
        /// <param name="vr"></param>
        /// <param name="htmlNode"></param>
        /// <param name="p"></param>
        /// <param name="atribut"></param>
        /// <param name="hodnotaAtributu"></param>
        private static void RecursiveReturnTagsWithAttr(List<HtmlNode> vr, HtmlNode htmlNode, string p, string atribut, string hodnotaAtributu)
        {
            foreach (HtmlNode item in htmlNode.ChildNodes)
            {
                if (HasTagName(item, p))
                {
                    if (HasTagAttr(item, atribut, hodnotaAtributu, false))
                    {
                        //RecursiveReturnTagsWithAttr(vr, item, p);
                        if (!vr.Contains(item))
                        {
                            vr.Add(item);
                        }
                    }
                }
                else
                {
                    RecursiveReturnTagsWithAttr(vr, item, p, atribut, hodnotaAtributu);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hn"></param>
        /// <param name="nameOfTag"></param>
        /// <param name="nameOfAtr"></param>
        /// <param name="valueOfAtr"></param>
        /// <returns></returns>
        public static List<HtmlNode> GetTagsOfAtribute(HtmlNode hn, string nameOfTag, string nameOfAtr, string valueOfAtr)
        {
            List<HtmlNode> vr = new List<HtmlNode>();
            foreach (HtmlNode var in hn.ChildNodes)
            {
                if (var.Name == nameOfTag)
                {
                    if (HtmlHelper.GetValueOfAttribute(nameOfAtr, var) == valueOfAtr)
                    {
                        vr.Add(var);
                    }
                }
            }
            return vr;
        }


        private static void RecursiveReturnTagsWithContainsAttr(List<HtmlNode> vr, HtmlNode node, string p, string atribut, string hodnotaAtributu)
            {
            RecursiveReturnTagsWithContainsAttr(vr, node, p, atribut, hodnotaAtributu, true, true);
        }

        /// <summary>
        /// Do A3 se může zadat * pro vrácení všech tagů
        /// </summary>
        /// <param name="vr"></param>
        /// <param name="htmlNode"></param>
        /// <param name="p"></param>
        /// <param name="atribut"></param>
        /// <param name="hodnotaAtributu"></param>
        public static void RecursiveReturnTagsWithContainsAttr(List<HtmlNode> vr, HtmlNode htmlNode, string p, string atribut, string hodnotaAtributu, bool contains, bool recursively)
        {
            foreach (HtmlNode item in htmlNode.ChildNodes)
            {
                string attrValue = HtmlHelper.GetValueOfAttribute(atribut, item);
                if (contains)
                {
                    contains = attrValue.Contains(hodnotaAtributu);
                }
                else
                {
                    contains = attrValue == hodnotaAtributu;
                }
                if (HasTagName(item, p) && contains)
                {
                    //RecursiveReturnTagsWithContainsAttr(vr, item, p);
                    if (!vr.Contains(item))
                    {
                        vr.Add(item);
                    }
                }
                else
                {
                    if (recursively)
                    {
                        RecursiveReturnTagsWithContainsAttr(vr, item, p, atribut, hodnotaAtributu, contains, recursively);
                    }
                }
            }
        }

        /// <summary>
        /// Do A3 se může zadat * pro vrácení všech tagů
        /// </summary>
        /// <param name="vr"></param>
        /// <param name="htmlNode"></param>
        /// <param name="p"></param>
        /// <param name="atribut"></param>
        /// <param name="hodnotaAtributu"></param>
        private static void RecursiveReturnTagsWithContainsAttrWithSplittedElement(List<HtmlNode> vr, HtmlNode htmlNode, string p, string atribut, string hodnotaAtributu, string delimiter)
        {

            foreach (HtmlNode item in htmlNode.ChildNodes)
            {
                if (HasTagName(item, p) && HasTagAttrContains(item, delimiter, atribut, hodnotaAtributu))
                {
                    //RecursiveReturnTagsWithContainsAttrWithSplittedElement(vr, item, p, atribut, hodnotaAtributu, delimiter);
                    if (!vr.Contains(item))
                    {
                        vr.Add(item);
                    }
                }
                else
                {
                    RecursiveReturnTagsWithContainsAttrWithSplittedElement(vr, item, p, atribut, hodnotaAtributu, delimiter);
                }
            }
        }

        /// <summary>
        /// Do A2 se může zadat * pro získaní všech tagů
        /// </summary>
        /// <param name="htmlNode"></param>
        /// <param name="tag"></param>
        /// <param name="atribut"></param>
        /// <param name="hodnotaAtributu"></param>
        /// <returns></returns>
        public static List<HtmlNode> ReturnTagsWithContainsAttrRek(HtmlNode htmlNode, string tag, string atribut, string hodnotaAtributu)
        {
            List<HtmlNode> vr = new List<HtmlNode>();

            RecursiveReturnTagsWithContainsAttr(vr, htmlNode, tag, atribut, hodnotaAtributu);
            return vr;
        }

        public static List<HtmlNode> ReturnTagsWithContainsAttrRek(HtmlNode htmlNode, string tag, string atribut, string hodnotaAtributu, bool contains, bool recursively)
        {
            List<HtmlNode> vr = new List<HtmlNode>();

            RecursiveReturnTagsWithContainsAttr(vr, htmlNode, tag, atribut, hodnotaAtributu, contains, recursively);
            return vr;
        }

        /// <summary>
        /// Do A2 se může zadat * pro získaní všech tagů
        /// </summary>
        /// <param name="htmlNode"></param>
        /// <param name="tag"></param>
        /// <param name="atribut"></param>
        /// <param name="hodnotaAtributu"></param>
        /// <returns></returns>
        public static List<HtmlNode> ReturnTagsWithContainsClassRek(HtmlNode htmlNode, string tag, string t)
        {
            List<HtmlNode> vr = new List<HtmlNode>();

            RecursiveReturnTagsWithContainsAttrWithSplittedElement(vr, htmlNode, tag, "class", t, " ");
            return vr;
        }
    }
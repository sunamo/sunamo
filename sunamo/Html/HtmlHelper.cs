using HtmlAgilityPack;
using sunamo.Xml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace sunamo.Html
{
    public static class HtmlHelper
    {
        public static string ToXmlFinal(string xml)
        {
            xml = sunamo.Html.HtmlHelper.ReplaceHtmlNonPairTagsWithXmlValid(xml);
            xml = XH.RemoveXmlDeclaration(xml);
            return "<?xml version=\"1.0\" encoding=\"utf-8\" ?>" + sunamo.Html.HtmlHelper.ReplaceHtmlNonPairTagsWithXmlValid(XH.RemoveXmlDeclaration(xml.Replace("<?xml version=\"1.0\" encoding=\"iso-8859-2\" />", "").Replace("<?xml version=\"1.0\" encoding=\"utf-8\" />", "").Replace("<?xml version=\"1.0\" encoding=\"UTF-8\" />", "")));
        }

        /// <summary>
        /// Již volá ReplaceHtmlNonPairTagsWithXmlValid
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="odstranitXmlDeklaraci"></param>
        /// <returns></returns>
        public static string ToXml(string xml, bool odstranitXmlDeklaraci)
        {
            HtmlDocument doc = new HtmlDocument();
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
            vr = sunamo.Html.HtmlHelper.ReplaceHtmlNonPairTagsWithXmlValid(vr);
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



        /// <summary>
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
                    if (!vr.Contains(item))
                    {
                        vr.Add(item);
                    }
                }
                else
                {
                    RecursiveReturnTags(vr, item, p);
                }
            }
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

        public static bool HasChildTag(HtmlNode spanInHeader, string v)
        {
            return ReturnTags(spanInHeader, v).Count != 0;
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
                    if (HasTagAttr(item, atribut, hodnotaAtributu))
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

        private static bool HasTagAttr(HtmlNode item, string atribut, string hodnotaAtributu)
        {
            if (hodnotaAtributu == "*")
            {
                return true;
            }
            return HtmlHelper.GetValueOfAttribute(atribut, item) == hodnotaAtributu;
        }

        /// <summary>
        /// Do A3 se může zadat * pro vrácení všech tagů
        /// </summary>
        /// <param name="vr"></param>
        /// <param name="htmlNode"></param>
        /// <param name="p"></param>
        /// <param name="atribut"></param>
        /// <param name="hodnotaAtributu"></param>
        private static void RecursiveReturnTagsWithContainsAttr(List<HtmlNode> vr, HtmlNode htmlNode, string p, string atribut, string hodnotaAtributu)
        {

            foreach (HtmlNode item in htmlNode.ChildNodes)
            {
                if (HasTagName(item, p) && HtmlHelper.GetValueOfAttribute(atribut, item).Contains(hodnotaAtributu))
                {
                    //RecursiveReturnTagsWithContainsAttr(vr, item, p);
                    if (!vr.Contains(item))
                    {
                        vr.Add(item);
                    }
                }
                else
                {
                    RecursiveReturnTagsWithContainsAttr(vr, item, p, atribut, hodnotaAtributu);
                }
            }
        }

        public static bool HasTagAttrContains(HtmlNode htmlNode, string delimiter, string attr, string value)
        {
            string attrValue = HtmlHelper.GetValueOfAttribute(attr, htmlNode);
            string[] spl = SH.Split(attrValue, delimiter);
            return spl.Contains(value);
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
            HtmlDocument doc = new HtmlDocument();
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
        /// Originally from HtmlParser
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
        /// Pokud bude nalezen alespoň jeden tag, vrátí ho, pokud žádný, GN
        /// </summary>
        /// <param name="htmlNode"></param>
        /// <param name="tag"></param>
        /// <param name="attr"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static HtmlNode ReturnTagWithAttr(HtmlNode htmlNode, string tag, string attr, string value)
        {
            List<HtmlNode> vr = new List<HtmlNode>();
            RecursiveReturnTagWithAttr(vr, htmlNode, tag, attr, value);
            if (vr.Count > 0)
            {
                return vr[0];
            }
            return null;
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

        public static string PrepareToAttribute(string title)
        {
            return title.Replace('\"', '\'');
        }

        /*
                 * -----
    from: 60
    to: 67
    word: aplikace
    veta: -1
    -----
    -----
    from: 81
    to: 89
    word: zobrazuje
    veta: -1
    -----
    -----
    from: 188
    to: 196
    word: zobrazuje
    veta: -1
    -----
    -----
    from: 217
    to: 221
    word: ještě
    veta: -1
    -----
    -----
    from: 251
    to: 259
    word: zobrazuje
    veta: -1
    -----
    -----
    from: 340
    to: 349
    word: angličtinu
    veta: -1
    -----
                 */

        /// <summary>
        /// Před zavoláním této metody musí být v A1 převedeny bílé znaky na mezery - pouze tak budou označeny všechny výskyty daných slov
        /// </summary>
        /// <param name="celyObsah"></param>
        /// <param name="maxPocetPismenNaVetu"></param>
        /// <param name="pocetVet"></param>
        /// <param name="hledaneSlova"></param>
        /// <returns></returns>
        public static string HighlightingWords(string celyObsah, int maxPocetPismenNaVetu, int pocetVet, string[] hledaneSlova)
        {
            hledaneSlova = CA.ToLower(hledaneSlova);
            celyObsah = celyObsah.Trim();
            List<FromToWord> ftw = SH.ReturnOccurencesOfStringFromToWord(celyObsah, hledaneSlova);
            if (ftw.Count > 0)
            {

                List<List<FromToWord>> dd = new List<List<FromToWord>>();
                List<FromToWord> fromtw = new List<FromToWord>();
                fromtw.Add(ftw[0]);
                int indexDDNaKteryVkladat = 0;
                int indexFromNaposledyVlozeneho = ftw[0].from;
                dd.Add(fromtw);

                for (int i = 1; i < ftw.Count; i++)
                {
                    var item = ftw[i];
                    if (item.to - indexFromNaposledyVlozeneho < maxPocetPismenNaVetu)
                    {
                        dd[indexDDNaKteryVkladat].Add(item);
                    }
                    else
                    {
                        List<FromToWord> ftw2 = new List<FromToWord>();
                        ftw2.Add(item);
                        dd.Add(ftw2);
                        if (dd.Count == pocetVet)
                        {
                            break;
                        }
                        indexDDNaKteryVkladat++;
                    }
                    indexFromNaposledyVlozeneho = item.from;
                }

                // Teď vypočtu pro každou kolekci v DD prostřední prvek a od toho vezmu vždy znaky nalevo i napravo
                StringBuilder final = new StringBuilder();
                foreach (var item in dd)
                {
                    int stred = 0;
                    if (item.Count % 2 == 0)
                    {
                        // Zjistím 2 prostřední slova
                        int from = item[item.Count / 2].from;
                        int to = 0;
                        if (item.Count != 2)
                        {
                            to = item[item.Count / 2 + 1].to;
                        }
                        else
                        {
                            to = item[item.Count / 2].to;
                        }

                        stred = (from + (to - from) / 2);
                    }
                    else if (item.Count == 1)
                    {
                        stred = (item[0].from + (item[0].to - item[0].from) / 2);
                    }
                    else
                    {
                        stred = item.Count / 2;
                        stred++;
                        stred = (item[stred].from + (item[stred].to - item[stred].from) / 2);
                    }

                    int naKazdeStrane = maxPocetPismenNaVetu / 2;

                    string veta = SH.XCharsBeforeAndAfterWholeWords(SH.ReplaceAll(celyObsah, " ", CA.ToListString(AllChars.whiteSpacesChars).ToArray()), stred, naKazdeStrane);

                    // Teď zvýrazním nalezené slova
                    string[] slova = SH.SplitBySpaceAndPunctuationCharsLeave(veta);
                    StringBuilder vetaSeZvyraznenimiCastmi = new StringBuilder();
                    foreach (var item2 in slova)
                    {
                        bool jeToHledaneSlovo = false;
                        string i2l = item2.ToLower();
                        foreach (var item3 in hledaneSlova)
                        {
                            if (i2l == item3)
                            {
                                jeToHledaneSlovo = true;
                            }
                        }

                        if (jeToHledaneSlovo)
                        {
                            vetaSeZvyraznenimiCastmi.Append("<b>" + item2 + "</b>");
                        }
                        else
                        {
                            vetaSeZvyraznenimiCastmi.Append(item2);
                        }
                    }
                    final.Append(vetaSeZvyraznenimiCastmi.ToString() + " ... ");
                }
                return final.ToString();
            }
            else
            {
                return SH.ShortForLettersCountThreeDotsReverse(celyObsah, pocetVet * maxPocetPismenNaVetu);
            }

        }

        public static string ReplaceAllFontCase(string r)
        {
            string za = "<br />";
            r = r.Replace("<BR />", za);
            r = r.Replace("<bR />", za);
            r = r.Replace("<Br />", za);

            r = r.Replace("<br/>", za);
            r = r.Replace("<BR/>", za);
            r = r.Replace("<bR/>", za);
            r = r.Replace("<Br/>", za);

            r = r.Replace("<br>", za);
            r = r.Replace("<BR>", za);
            r = r.Replace("<bR>", za);
            r = r.Replace("<Br>", za);
            return r;
        }

        

        #region Z swf HtmlParser
        private static void RecursiveReturnTagWithAttr(List<HtmlNode> vr, HtmlNode htmlNode, string tag, string attr, string value)
        {
            foreach (HtmlNode item in htmlNode.ChildNodes)
            {
                if (item.Name == tag && HtmlHelper.GetValueOfAttribute(attr, item) == value)
                {
                    //RecursiveReturnTagWithAttr(vr, item, tag, attr, value);
                    vr.Add(item);
                    return;
                }
                else
                {
                    RecursiveReturnTagWithAttr(vr, item, tag, attr, value);
                }
            }
        }

        
        #endregion



        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <param name="atr"></param>
        /// <param name="hod"></param>
        public static void SetAttribute(HtmlNode node, string atr, string hod)
        {
            object o = node.Attributes[atr];
            if (o != null)
            {
                node.Attributes.Remove(node.Attributes[atr]);
            }
            node.Attributes.Add(atr, hod);
        }

        /// <summary>
        /// Vrátí SE pokud nenalezne, nebo bude prázdný. nevrací null
        /// </summary>
        /// <param name="p"></param>
        /// <param name="divMain"></param>
        /// <returns></returns>
        public static string GetValueOfAttribute(string p, HtmlNode divMain)
        {
            return GetValueOfAttribute(p, divMain, false);
        }

        public static string GetValueOfAttribute(string p, HtmlNode divMain, bool trim)
        {
            object o = divMain.Attributes[p];
            if (o != null)
            {
                string st = ((HtmlAttribute)o).Value;
                if (trim)
                {
                    st = st.Trim();
                }
                return st;
            }
            return "";
        }

        public static string ReplaceHtmlNonPairTagsWithXmlValid(string vstup)
        {
            List<string> jizNahrazeno = new List<string>();
            var pol = CA.ToEnumerable("br", "hr", "img", "meta", "input", "iframe");
            MatchCollection mc = Regex.Matches(vstup, "<(?:\"[^\"]*\"['\"]*|'[^']*'['\"]*|[^'\">])+>");
            List<string> col = new List<string>( pol);

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





        public static Dictionary<string, string> GetValuesOfStyle(HtmlNode item)
        {
            Dictionary<string, string> vr = new Dictionary<string, string>();
            string at = GetValueOfAttribute("style", item);
            if (at.Contains(";"))
            {
                string[] d = SH.Split(at, ";");
                foreach (string item2 in d)
                {
                    if (item2.Contains(":"))
                    {
                        string[] r = SH.SplitNone(item2, ":");
                        vr.Add(r[0].Trim().ToLower(), r[1].Trim().ToLower());
                    }
                }
            }
            return vr;
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
        /// Nahradí každý text <*> za SE. Vnitřní ne-xml obsah nechá být.
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static string StripAllTags(string p)
        {
            return Regex.Replace(p, @"<[^>]*>", String.Empty);
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


        public static HtmlNode GetTagOfAtributeRek(HtmlNode hn, string nameOfTag, string nameOfAtr, string valueOfAtr)
        {
            hn = HtmlHelper.TrimNode(hn);
            foreach (HtmlNode var in hn.ChildNodes)
            {
                //var.InnerHtml = var.InnerHtml.Trim();
                HtmlNode hn2 = var;//.FirstChild;
                foreach (HtmlNode item2 in var.ChildNodes)
                {
                    if (HtmlHelper.GetValueOfAttribute(nameOfAtr, item2) == valueOfAtr)
                    {
                        return item2;
                    }
                    HtmlNode hn3 = GetTagOfAtributeRek(item2, nameOfTag, nameOfAtr, valueOfAtr);
                    if (hn3 != null)
                    {
                        return hn3;
                    }
                }
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
        /// 
        /// </summary>
        /// <param name="html"></param>
        /// <param name="nameOfTag"></param>
        /// <returns></returns>
        public static string TrimOpenAndEndTags(string html, string nameOfTag)
        {
            html = html.Replace("<" + nameOfTag + ">", "");
            html = html.Replace("</" + nameOfTag + ">", "");
            return html;
        }



        public static HtmlNode TrimNode(HtmlNode hn2)
        {
            if (hn2.FirstChild == null)
            {
                return hn2;
            }
            if (string.IsNullOrWhiteSpace(hn2.FirstChild.InnerHtml))
            {
                return hn2;
            }
            hn2.InnerHtml = hn2.InnerHtml.Trim();
            hn2.FirstChild.InnerHtml = hn2.FirstChild.InnerHtml.Trim();
            hn2.InnerHtml = hn2.InnerHtml.Trim();
            return hn2;
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



        public static string ClearSpaces(string dd)
        {
            return dd.Replace("&nbsp;", " ").Replace("  ", " ");
        }

        public static string ConvertTextToHtml(string p)
        {
            p = p.Replace(Environment.NewLine, "<br />");
            return p;
        }

        public static List<HtmlNode> GetWithoutTextNodes(HtmlNode htmlNode)
        {
            List<HtmlNode> vr = new List<HtmlNode>();
            foreach (HtmlNode item in htmlNode.ChildNodes)
            {
                string dd = item.ToString();
                if (dd != "HtmlAgilityPack.HtmlTextNode")
                {
                    vr.Add(item);
                }
            }
            return vr;
        }

        #region Vrácení všech tagů v elementu s ohledem jen na název tagu

        #region Using RecursiveReturnTags method
        /// <summary>
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
            return vr;
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
        #endregion



        /// <summary>
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
        #endregion


    }
}

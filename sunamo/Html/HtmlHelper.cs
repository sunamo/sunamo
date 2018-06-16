using HtmlAgilityPack;
using sunamo.Constants;
using sunamo.Xml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace sunamo.Html
{
    public static class HtmlHelper
    {
        const string textNodeToString = "HtmlAgilityPack.HtmlTextNode";

        #region private HtmlAgility helper methods
        /// <summary>
        /// Rekurzivně volá metodu RecursiveReturnAllTags
        /// </summary>
        /// <param name="vr"></param>
        /// <param name="html"></param>
        /// <param name="tagsName"></param>
        private static void RecursiveReturnAllTags(List<HtmlNode> vr, HtmlNode html, params string[] tagsName)
        {
            bool defaultContains = false;
            if (tagsName[0] == AllStrings.asterisk)
            {
                defaultContains = true;
            }

            bool contains = false;
            foreach (HtmlNode item in html.ChildNodes)
            {
                if (item is HtmlTextNode)
                {
                    continue;
                }
                contains = defaultContains;

                if (!contains)
                {
                    if (tagsName.Length == 1)
                    {
                        if (item.Name == tagsName[0])
                        {
                            contains = true;
                        }
                    }
                    else
                    {
                        foreach (var t in tagsName)
                        {
                            if (item.Name == t)
                            {
                                contains = true;
                            }
                        }
                    }
                }

                if (contains)
                {
                    RecursiveReturnAllTags(vr, item, tagsName);
                    if (!vr.Contains(item))
                    {
                        vr.Add(item);
                    }
                }
                else
                {
                    RecursiveReturnAllTags(vr, item, tagsName);
                }
            }
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

        static bool IsHtmlNode(HtmlNode node)
        {
            if (node is HtmlTextNode)
            {
                return false;
            }
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        /// <summary>
        /// Do A2 se může zadat *
        /// </summary>
        /// <param name="hn"></param>
        /// <param name="tag"></param>
        /// <returns></returns>
        private static bool HasTagName(HtmlNode hn, params string[] tag)
        {
            if (hn is HtmlTextNode)
            {
                return false;
            }
            if (hn.ToString() == textNodeToString)
            {
                return false;
            }
            if (CA.Contains("*", tag))
            {
                return true;
            }
            return tag.FirstOrDefault(t => t == hn.Name) != null;
        }

        private static bool HasTagAttr(HtmlNode item, string atribut, string hodnotaAtributu, bool enoughContains = false)
        {
            
            var value = HtmlHelper.GetValueOfAttribute(atribut, item);
            if (value != null)
            {
                if (hodnotaAtributu == AllStrings.asterisk)
                {
                    return true;
                }

                if (enoughContains)
                {
                    return value.Contains(hodnotaAtributu);
                }
                return value == hodnotaAtributu;
            }
            return false;
        }
        #endregion

        #region public HtmlAgility Helper methods
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
        /// Trim InnerHtml and FirstChild.InnerHtml
        /// </summary>
        /// <param name="hn2"></param>
        /// <returns></returns>
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

        public static List<HtmlNode> GetWithoutTextNodes(HtmlNode htmlNode)
        {
            List<HtmlNode> vr = new List<HtmlNode>();
            foreach (HtmlNode item in htmlNode.ChildNodes)
            {
                
                if (!IsHtmlNode(item))
                {
                    vr.Add(item);
                }
            }
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
        #endregion

        #region Write document
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
        #endregion

        #region private HtmlAgility


        private static void RecursiveReturnTagWithAttr(List<HtmlNode> vr, HtmlNode htmlNode, string tag, string attr, string value)
        {
            foreach (HtmlNode item in htmlNode.ChildNodes)
            {
                if (HasTagName(item, tag) && HasTagAttr(item, attr, value))
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
                if (HasTagName(item, p) && HasTagAttr(item, atribut, hodnotaAtributu, true))
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
                if (HasTagName(item, p) && HasTagAttr(item, atribut, hodnotaAtributu, true))
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
        /// Do A3 se může zadat * pro vrácení všech tagů
        /// 
        /// Do A5 se může vložit * pro vrácení tagů s hledaným atributem s jakoukoliv hodnotou
        /// </summary>
        /// <param name="vr"></param>
        /// <param name="htmlNode"></param>
        /// <param name="p"></param>
        /// <param name="atribut"></param>
        /// <param name="hodnotaAtributu"></param>
        private static void RecursiveReturnTagsWithAttr(List<HtmlNode> vr, HtmlNode htmlNode, string p, string atribut, string hodnotaAtributu, bool includeSubelements = false)
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

                    if (includeSubelements)
                    {
                        RecursiveReturnTagsWithAttr(vr, item, p, atribut, hodnotaAtributu, includeSubelements);
                    }
                }
                else
                {
                    RecursiveReturnTagsWithAttr(vr, item, p, atribut, hodnotaAtributu, includeSubelements);
                }
            }
        }


        #endregion

        #region Public agility non nodes
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
        /// Vrátí SE bude prázdný. jinak null
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
            return null;
        }
        #endregion

        #region public HtmlAgility node
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
                if (HasTagName(item, nazevTagu))
                {
                    return item;
                }
            }
            return null;
        }

        /// <summary>
        /// Do A2 se může vložit * ale nemělo by to moc smysl
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="tag"></param>
        /// <returns></returns>
        public static List<HtmlNode> ReturnTags(HtmlNode parent, params string[] tag)
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
        /// When won't working, try ReturnTagsWithAttrRek2
        /// Do A2 se může zadat * pro získaní všech tagů
        /// Do A4 se může vložit * pro vrácení tagů s hledaným atributem s jakoukoliv hodnotou
        /// </summary>
        /// <param name="htmlNode"></param>
        /// <param name="tag"></param>
        /// <param name="atribut"></param>
        /// <param name="hodnotaAtributu"></param>
        /// <returns></returns>
        public static List<HtmlNode> ReturnTagsWithAttrRek(HtmlNode htmlNode, string tag, string atribut, string hodnotaAtributu, bool includeSubelements = false)
        {
            List<HtmlNode> vr = new List<HtmlNode>();

            RecursiveReturnTagsWithAttr(vr, htmlNode, tag, atribut, hodnotaAtributu, includeSubelements);
            return vr;
        }

        /// <summary>
        /// When won't working, try ReturnTagsWithAttrRek
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

        public static HtmlNode ReturnTagRek(HtmlNode hn, string nameOfTag)
        {
            hn = HtmlHelper.TrimNode(hn);
            foreach (HtmlNode var in hn.ChildNodes)
            {
                
                    if (HasTagName(var, nameOfTag))
                    {
                        return var;
                    }
                
                    HtmlNode node = ReturnTagRek(var, nameOfTag);
                if (node != null)
                {
                    return node;
                }
            }
            return null;
        }

        /// <summary>
        /// POZOR, FUNGUJE JAKO REKURZIVNI
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
                if (HasTagName(var, nameOfTag))
                {
                    if (HasTagAttr(var, HtmlAttrs.classAttr, valueOfAtr))
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
                }
            }
            return null;
        }

        public static HtmlNode GetTagOfAtribute(HtmlNode hn, string nameOfTag, string nameOfAtr, string valueOfAtr, bool twoLevel = true)
        {
            hn = HtmlHelper.TrimNode(hn);
            foreach (HtmlNode hn2 in hn.ChildNodes)
            {
               
                if (HasTagName(hn2, nameOfTag))
                {
                    if (HasTagAttr(hn2, nameOfAtr, valueOfAtr))
                    {
                        return hn2;
                    }
                    if (twoLevel)
                    {
                        return GetTagOfAtribute(hn, nameOfTag, nameOfAtr, valueOfAtr, false);
                    }
                }
            }
            return null;
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

        
        #endregion

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

        /// <summary>
        /// A1 je uzel který se bude rekurzivně porovnávat
        /// A2 je název tagu(div, a, atd.) které chci vrátit.
        /// Return really all tags as recursive!!!
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
        /// 
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

        /// <summary>
        /// Do A2 se může zadat * pro získaní všech tagů
        /// </summary>
        /// <param name="htmlNode"></param>
        /// <param name="tag"></param>
        /// <param name="atribut"></param>
        /// <param name="hodnotaAtributu"></param>
        /// <returns></returns>
        public static List<HtmlNode> ReturnTagsWithContainsAttr(HtmlNode hn, string nameOfTag, string nameOfAtr, string valueOfAtr)
        {
            List<HtmlNode> vr = new List<HtmlNode>();
            foreach (HtmlNode var in hn.ChildNodes)
            {
                if (HasTagName(var, nameOfTag))
                {
                    if (HasTagAttr(var, nameOfAtr, valueOfAtr, true))
                    {
                        vr.Add(var);
                    }
                }
            }
            return vr;
        }

        #endregion

        #region Html5Helper
        public static string ToXmlFinal(string xml)
        {
            return Html5Helper.ToXmlFinal(xml);
        }

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
            return Html5Helper.HighlightingWords(celyObsah, maxPocetPismenNaVetu, pocetVet, hledaneSlova);
        }

        public static string ReplaceHtmlNonPairTagsWithXmlValid(string vstup)
        {
            return Html5Helper.ReplaceHtmlNonPairTagsWithXmlValid(vstup);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="html"></param>
        /// <param name="nameOfTag"></param>
        /// <returns></returns>
        public static string TrimOpenAndEndTags(string html, string nameOfTag)
        {
            return Html5Helper.TrimOpenAndEndTags(html, nameOfTag);
        }

        public static string ReplaceAllFontCase(string r)
        {
            return Html5Helper.ReplaceAllFontCase(r);
        }

        public static string PrepareToAttribute(string title)
        {
            return Html5Helper.PrepareToAttribute(title);
        }

        /// <summary>
        /// Jen volá metodu StripAllTags
        /// Nahradí každý text <*> za SE. Vnitřní ne-xml obsah nechá být.
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static string RemoveAllTags(string p)
        {
            return Html5Helper.RemoveAllTags(p);
        }

        /// <summary>
        /// Nahradí každý text <*> za SE. Vnitřní ne-xml obsah nechá být.
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static string StripAllTags(string p)
        {
            return Html5Helper.StripAllTags(p);
        }

        /// <summary>
        /// Nahradí každý text <*> za mezeru. Vnitřní ne-xml obsah nechá být.
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static string StripAllTagsSpace(string p)
        {
            return Html5Helper.StripAllTagsSpace(p);
        }

        public static List<string> StripAllTags(string p, string delimiter)
        {
            return Html5Helper.StripAllTags(p, delimiter);
        }

        public static List<string> StripAllTagsList(string p)
        {
            return Html5Helper.StripAllTagsList(p);
        }

        public static string ClearSpaces(string dd)
        {
            return Html5Helper.ClearSpaces(dd);
        }

        public static string ConvertTextToHtml(string p)
        {
            return Html5Helper.ConvertTextToHtml(p);
        }
        #endregion
    }
}

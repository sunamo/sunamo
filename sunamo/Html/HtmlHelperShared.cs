

using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using HtmlAgilityPack;
using sunamo.Constants;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

public static partial class HtmlHelper
    {
    public static string ConvertTextToHtml(string p)
    {
        p = p.Replace(Environment.NewLine, "<br />");
        return p;
    }

public static string PrepareToAttribute(string title)
        {
            return title.Replace(AllChars.qm, AllChars.bs);
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

public static string ClearSpaces(string dd)
        {
            return dd.Replace("&nbsp;", AllStrings.space).Replace(AllStrings.doubleSpace, AllStrings.space);
        }

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

public static string GetValueOfAttribute(string p, HtmlNode divMain, bool trim = false)
    {
        return HtmlAssistant.GetValueOfAttribute(p, divMain, trim);
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

/// <summary>
        /// 
        /// </summary>
        /// <param name="html"></param>
        /// <param name="nameOfTag"></param>
        /// <returns></returns>
        public static string TrimOpenAndEndTags(string html, string nameOfTag)
        {
            html = html.Replace(AllStrings.lt + nameOfTag + AllStrings.gt, "");
            html = html.Replace("</" + nameOfTag + AllStrings.gt, "");
            return html;
        }

/// <summary>
        /// Před zavoláním této metody musí být v A1 převedeny bílé znaky na mezery - pouze tak budou označeny všechny výskyty daných slov
        /// </summary>
        /// <param name="celyObsah"></param>
        /// <param name="maxPocetPismenNaVetu"></param>
        /// <param name="pocetVet"></param>
        /// <param name="hledaneSlova"></param>
        /// <returns></returns>
        public static string HighlightingWords(string celyObsah, int maxPocetPismenNaVetu, int pocetVet, List<string> hledaneSlova)
        {
            hledaneSlova = CA.ToLower(hledaneSlova);
            celyObsah = celyObsah.Trim();
            List<FromToWord> ftw = SH.ReturnOccurencesOfStringFromToWord(celyObsah, hledaneSlova.ToArray());
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

                    string veta = SH.XCharsBeforeAndAfterWholeWords(SH.ReplaceAll(celyObsah, AllStrings.space, CA.ToListString(AllChars.whiteSpacesChars).ToArray()), stred, naKazdeStrane);

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

    public static string ConvertHtmlToText(string h)
    {
        h = SH.ReplaceAll(h, string.Empty, "<br>", "<br />", "<br/>");
        return h;
    }

    /// <summary>
    /// Nahradí každý text <*> za SE. Vnitřní ne-xml obsah nechá být.
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    public static string StripAllTags(string p)
        {
            return StripAllTags(p, AllStrings.doubleSpace);
        }
public static string StripAllTags(string p, string replaceFor)
        {
            return Regex.Replace(p, @"<[^>]*>", replaceFor);
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
}
using sunamo.Xml;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace sunamo.Html
{
    public class Html5Helper
    {
        #region Move to Html5Helper
        public static string ToXmlFinal(string xml)
        {
            xml = sunamo.Html.HtmlHelper.ReplaceHtmlNonPairTagsWithXmlValid(xml);
            xml = XH.RemoveXmlDeclaration(xml);
            return "<?xml version=\"1.0\" encoding=\"utf-8\" ?>" + sunamo.Html.HtmlHelper.ReplaceHtmlNonPairTagsWithXmlValid(XH.RemoveXmlDeclaration(xml.Replace("<?xml version=\"1.0\" encoding=\"iso-8859-2\" />", "").Replace("<?xml version=\"1.0\" encoding=\"utf-8\" />", "").Replace("<?xml version=\"1.0\" encoding=\"UTF-8\" />", "")));
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

        public static string PrepareToAttribute(string title)
        {
            return title.Replace('\"', '\'');
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

        public static List<string> StripAllTags(string p, string delimiter)
        {
            List<string> d = StripAllTagsList(p);
            return d;
        }

        public static List<string> StripAllTagsList(string p)
        {
            char notContained = SH.CharWhichIsNotContained(p);
            var d = SH.SplitList(Regex.Replace(p, @"<[^>]*>", notContained.ToString()), notContained);
            return d;
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
        #endregion
    }
}

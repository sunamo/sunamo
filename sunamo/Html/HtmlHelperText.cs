using sunamo.Constants;
using sunamo.Enums;
using sunamo.Html;
using sunamo.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;


    /// <summary>
    /// HtmlHelperText - for methods which NOT operate on HtmlAgiityHelper! 
    /// HtmlAgilityHelper - getting new nodes
    /// HtmlAssistant - Only for methods which operate on HtmlAgiityHelper! 
    /// </summary>
    public partial class HtmlHelperText
    {
        private static Type type = typeof(HtmlHelperText);

        public static bool ContainsTag(string s)
    {
        foreach (var item in AllHtmlTags.WithLeftArrow)
        {
            if (s.Contains(item))
            {
                return true;
            }
        }
        return false;
    }

        /// <summary>
        /// Get type of tag (paired ended, paired not ended, non paired)
        /// </summary>
        /// <param name="tag"></param>
        public static HtmlTagSyntax GetSyntax(ref string tag)
        {
            ThrowExceptions.InvalidParameter(Exc.GetStackTrace(),type, "GetSyntax", (string)tag, "tag");

            tag = SH.GetToFirst((string)tag, AllStrings.space);
            tag = tag.Trim().TrimStart(AllChars.lt).TrimEnd(AllChars.gt).ToLower();

            if (AllLists.HtmlNonPairTags.Contains((string)tag))
            {
                return HtmlTagSyntax.NonPairingNotEnded;
            }
            tag = tag.TrimEnd(AllChars.slash);
            if (AllLists.HtmlNonPairTags.Contains((string)tag))
            {
                return HtmlTagSyntax.NonPairingEnded;
            }
            if (tag[tag.Length - 1] == AllChars.slash)
            {
                return HtmlTagSyntax.End;
            }
            return HtmlTagSyntax.Start;
        }


        public static string TrimInnerOfEncodedHtml(string value)
        {
            value = SH.ReplaceAll(value, "&" + "gt" + ";", "&" + "gt" + "; ");
            value = SH.ReplaceAll(value, "&" + "lt" + ";", " &" + "lt" + ";");
            return value;
        }

        public static bool IsHtmlEntity(string i)
        {
            i = i.TrimStart('&').TrimEnd(';');
            return AllLists.htmlEntities.Contains(i);
        }

    public static List<string> GetContentOfTags(string text, string pre)
        {
            List<string> result = new List<string>();
            string start = $"<{pre}";
            string end = $"</{pre}>";
            int dex = text.IndexOf(start);
            while (dex != -1)
            {
                int dexEndLetter = text.IndexOf(AllChars.gt, dex);

                int dex2 = text.IndexOf(start, dex + start.Length);
                int dexEnd = text.IndexOf(end, dex);

                if (dex2 != -1)
                {
                    if (dexEnd > dex2)
                    {
                        ThrowExceptions.Custom(Exc.GetStackTrace(), type, Exc.CallingMethod(),$"Another starting tag is before ending <{pre}>");
                    }
                }

                result.Add(SH.GetTextBetweenTwoChars(text, dexEndLetter, dexEnd).Trim());

                dex = text.IndexOf(start, dexEnd);
            }

            return result;
        }

        public static bool IsCssDeclarationName(string decl)
        {
            if (AllLists.allCssKeys.Contains(decl))
            {
                return true;
            }
            return false;
        }

        public static string ConvertTextToHtml(string text)
        {
            var lines = SH.GetLines(text);

            CA.RemoveStringsEmpty2(lines);

            var endP = "</p>";

            CA.ChangeContent(lines, AddIntoParagraph);

            var result = SH.JoinNL(lines);
            result = SH.ReplaceAll(result, endP + AllStrings.cr + AllStrings.nl, endP);
            return result;
        }

    private static string AddIntoParagraph(string s)
        {
            const string spaceDash = " -";

            if (s.Contains(spaceDash))
            {
                s = "<b>" + s;

                s = s.Replace(spaceDash, "</b>" + spaceDash);
            }

            //string s2 = string.Empty;
            if (s[0] == AllChars.lt)
            {
                var tag = HtmlHelperText.GetFirstTag(s).ToLower();

                if (AllLists.PairingTagsDontWrapToParagraph.Contains(tag))
                {
                    return s;
                }
                if (tag.StartsWith(AllStrings.slash))
                {
                    if (AllLists.PairingTagsDontWrapToParagraph.Contains(tag.Substring(1)))
                    {
                        return s;
                    }
                }

                //s2 = s.Substring(1);
            }
            return WrapWith(s, "p");
        }

        private static string GetFirstTag(string s)
        {
            var between = SH.GetTextBetween(s, AllStrings.lt, AllStrings.gt);

            if (between.Contains(AllStrings.space))
            {
                return SH.GetToFirst(between, AllStrings.space);
            }
            return between;
        }
    }
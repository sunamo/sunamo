using sunamo.Constants;
using sunamo.Enums;
using sunamo.Values;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace sunamo.Html
{
    public class HtmlHelperText
    {
        static Type type = typeof(HtmlHelperText);

        public static HtmlTagSyntax GetSyntax(ref string tag)
        {
            ThrowExceptions.InvalidParameter(type, "GetSyntax", (string)tag, "tag");

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
            if (tag[tag.Length -1] == AllChars.slash)
            {
                return HtmlTagSyntax.End;
            }
            return HtmlTagSyntax.Start;
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
                        throw new Exception($"Another starting tag is before ending <{pre}>");
                    }
                }

                result.Add(SH.GetTextBetweenTwoChars(text, dexEndLetter, dexEnd  ).Trim());

                dex = text.IndexOf(start, dexEnd );
            }

            return result;
        }
    }
}

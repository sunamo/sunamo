using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sunamo
{
    public class SqlServerHelper
    {
        /// <summary>
        /// Tato hodnota byla založena aby používal všude v DB konzistentní datovou hodnotu, klidně může mít i hodnotu DT.MaxValue když to tak má být
        /// </summary>
        public static readonly DateTime DateTimeMinVal = new DateTime(1900, 1, 1);
        public static readonly DateTime DateTimeMaxVal = new DateTime(2079, 6, 6);
        static List<char> availableCharsInVarCharWithoutDiacriticLetters = new List<char>(new char[] { ' ', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '-', '.', ',', ';', '!', '–', '—', '…', '„', '“', '‚', '‘', '»', '«', '’', '\'', '(', ')', '[', ']', '{', '}', '”', '~', '°', '+', '@', '#', '$', '%', '^', '&', '=', '_', 'ˇ', '¨', '¤', '÷', '×', '˝', '/', '\\' });

        public static string ConvertToVarChar(string maybeUnicode)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in maybeUnicode)
            {
                if (availableCharsInVarCharWithoutDiacriticLetters.Contains(item) || SH.diacritic.IndexOf(item) != -1)
                {
                    sb.Append(item);
                }
                else
                {
                    sb.Append(SH.TextWithoutDiacritic(item.ToString()));
                }
            }
            return SH.ReplaceAll(sb.ToString(), " ", "  ");
        }

        public static string ConvertToVarCharPathOrFileName(string maybeUnicode)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in maybeUnicode)
            {
                if (availableCharsInVarCharWithoutDiacriticLetters.Contains(item) || SH.diacritic.IndexOf(item) != -1)
                {
                    sb.Append(item);
                }
            }
            string vr = SH.ReplaceAll(sb.ToString(), " ", "  ").Trim();
            vr = SH.ReplaceAll(vr, "\\", " \\");
            vr = SH.ReplaceAll(vr, "\\", "\\ ");
            vr = SH.ReplaceAll(vr, "/", "/ ");
            vr = SH.ReplaceAll(vr, "/", " /");
            return vr;
        }
    }
}

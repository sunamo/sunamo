using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public partial class SqlServerHelper
{
    /// <summary>
    /// Tato hodnota byla založena aby používal všude v DB konzistentní datovou hodnotu, klidně může mít i hodnotu DT.MaxValue když to tak má být
    /// </summary>
    public static readonly DateTime DateTimeMinVal = new DateTime(1900, 1, 1);
    public static readonly DateTime DateTimeMaxVal = new DateTime(2079, 6, 6);
    public static List<char> s_availableCharsInVarCharWithoutDiacriticLetters = new List<char>(new char[] { AllChars.space, 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', AllChars.dash, AllChars.dot, AllChars.comma, AllChars.sc, AllChars.exclamation, '\u2013', '\u2014', '\u2026', '\u201E', '\u201C', '\u201A', '\u2018', '\u00BB', '\u00AB', '\u2019', AllChars.bs, AllChars.lb, AllChars.rb, AllChars.lsf, AllChars.rsf, AllChars.cbl, AllChars.cbr, '\u201D', '~', '\u00B0', AllChars.plus, '@', '#', '$', AllChars.modulo, '^', '&', '=', AllChars.us, '\u02C7', '\u00A8', '\u00A4', '\u00F7', '\u00D7', '\u02DD', AllChars.slash, AllChars.bs, AllChars.lt, AllChars.gt });
    public static string ConvertToVarCharPathOrFileName(string maybeUnicode)
    {
        StringBuilder sb = new StringBuilder();
        foreach (var item in maybeUnicode)
        {
            if (s_availableCharsInVarCharWithoutDiacriticLetters.Contains(item) || SH.diacritic.IndexOf(item) != -1)
            {
                sb.Append(item);
            }
        }

        string vr = SH.ReplaceAll(sb.ToString(), AllStrings.space, AllStrings.doubleSpace).Trim();
        vr = SH.ReplaceAll(vr, AllStrings.bs, " \\");
        vr = SH.ReplaceAll(vr, AllStrings.bs, "\\ ");
        vr = SH.ReplaceAll(vr, AllStrings.slash, "/ ");
        vr = SH.ReplaceAll(vr, AllStrings.slash, " /");
        return vr;
    }
}
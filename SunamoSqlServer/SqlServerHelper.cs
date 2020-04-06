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
    public static List<char> s_availableCharsInVarCharWithoutDiacriticLetters = null;
    public static List<char> allowedInPassword = CA.ToList<char>('!', '@', '#', '$', '%', '^', '&', '*', '?', '_', '~');

    public static bool IsNull(object o)
    {
        return o == null || o == DBNull.Value;
    }

    static SqlServerHelper()
    {
            s_availableCharsInVarCharWithoutDiacriticLetters = new List<char>(new char[] { AllChars.colon, AllChars.space,  AllChars.dash, AllChars.dot, AllChars.comma, AllChars.sc, AllChars.exclamation, AllChars.bs, AllChars.lb, AllChars.rb, AllChars.lsf, AllChars.rsf, AllChars.cbl, AllChars.cbr , AllChars.plus, AllChars.modulo, AllChars.us,  AllChars.slash, AllChars.bs, AllChars.lt, AllChars.gt, AllChars.apostrophe });
        s_availableCharsInVarCharWithoutDiacriticLetters.AddRange(AllChars.lowerChars);
        s_availableCharsInVarCharWithoutDiacriticLetters.AddRange(AllChars.upperChars);
        s_availableCharsInVarCharWithoutDiacriticLetters.AddRange(AllChars.numericChars);
        s_availableCharsInVarCharWithoutDiacriticLetters.AddRange(new char[] { '\u2013', '\u2014', '\u2026', '\u201E', '\u201C', '\u201A', '\u2018', '\u00BB', '\u00AB', '\u2019', '\u201D', '\u00B0', '\u02C7', '\u00A8', '\u00A4', '\u00F7', '\u00D7', '\u02DD' });
        s_availableCharsInVarCharWithoutDiacriticLetters.AddRange(new char[] { '~', '@', '#', '$', '^', '&', '=', '|' });


        s_availableCharsInVarCharWithoutDiacriticLetters.AddRange(allowedInPassword);
        CA.RemoveDuplicitiesList<char>(s_availableCharsInVarCharWithoutDiacriticLetters);
    }
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

   public static Tuple<int, int> UnnormalizeNumber(int serie)
    {
        const int increaseAbout = 1000; 

        int l = int.MinValue;
        int h = l + increaseAbout;

        for (int i = 0; i < serie; i++)
        {
            l += increaseAbout;
            h += increaseAbout;
        }

        Tuple<int, int> d = new Tuple<int, int>(l, h);
        return d;
    }
}
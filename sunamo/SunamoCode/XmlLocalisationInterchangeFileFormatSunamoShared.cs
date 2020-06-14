using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
/// <summary>
/// Must be directly in standard, not SunamoCode.apps because SunamoCode.apps is derive from standard and I need class XmlLocalisationInterchangeFileFormat in standard
/// </summary>
public partial  class XmlLocalisationInterchangeFileFormatSunamo{
    static Type type = typeof(XmlLocalisationInterchangeFileFormatSunamo);
    public const string cs = "const string ";
    const string eqBs = " = \"";
    public static string SunamoStringsDot = "SunamoStrings.";
    public static string GetConstsFromLine(string d4)
    {
        return SH.GetTextBetween(d4, cs, eqBs, false);
    }


    public static Langs GetLangFromFilename(string s)
    {
        return XmlLocalisationInterchangeFileFormatXlf.GetLangFromFilename(s);
    }

    /// <summary>
    /// sess.i18n(
    /// </summary>
    public const string RLDataEn = "RLData.en[";
    public const string RLDataEn2 = "RLDataEn[";
    public const string SessI18n = "sess.i18n(";
    public const string XlfKeysDot = "XlfKeys.";

    public static List<string> removeSessI18nIfLineContains = CA.ToList<string>("MSStoredProceduresI");

    public static string RemoveSessI18nIfLineContains(string c)
    {
        return RemoveSessI18nIfLineContainsWorker(c, removeSessI18nIfLineContains.ToArray());
    }

    public static string RemoveSessI18nIfLineContainsWorker(string c, params string[] lineCont)
    {
        var l = SH.GetLines(c);
        bool cont = false;
        for (int i = l.Count - 1; i >= 0; i--)
        {
            var line = l[i];
            cont = false;
            foreach (var item in lineCont)
            {
                if (line.Contains(item))
                {
                    cont = true;
                    break;
                }
            }

            if (cont)
            {
                l[i] = RemoveAllSessI18n(l[i]);
            }
        }

        return SH.JoinNL(l);
    }

    public static string RemoveAllSessI18n(string c)
    {
        var sb = new StringBuilder(c);

        var sessI18n = XmlLocalisationInterchangeFileFormatSunamo.SessI18n;

        var occ = SH.ReturnOccurencesOfString(c, sessI18n);
        var ending = new List<int>(occ.Count);
        foreach (var item in occ)
        {
            ending.Add(c.IndexOf(AllChars.rb, item));
        }

        var l = sessI18n.Length;

        for (int i = occ.Count - 1; i >= 0; i--)
        {
            sb = sb.Remove(ending[i], 1);
            sb = sb.Remove(occ[i], l);
        }

        return sb.ToString();
    }

    /// <summary>
    /// return code for getting from RLData.en
    /// </summary>
    /// <param name="key2"></param>
    public static string TextFromRLData(string pathOrExt, string key2)
    {
        var ext = FS.GetExtension(pathOrExt);
        ext = SH.PrefixIfNotStartedWith(ext, ".");
        if (ext == AllExtensions.cs)
        {
            return RLDataEn + XlfKeysDot + key2 + "]";
        }
        else if (ext == AllExtensions.ts)
        {
            return "su.en(\"" + key2 + "\")";
        }
        ThrowExceptions.NotImplementedCase(Exc.GetStackTrace(), type, Exc.CallingMethod(), ext);
        return null;
    }

   
}
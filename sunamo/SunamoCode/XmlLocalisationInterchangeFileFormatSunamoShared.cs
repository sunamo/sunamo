using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
/// <summary>
/// Must be directly in standard, not SunamoCode.apps because SunamoCode.apps is derive from standard and I need class XmlLocalisationInterchangeFileFormat in standard
/// </summary>
public partial class XmlLocalisationInterchangeFileFormatSunamo
{
    public static string pathXlfKeys = @"D:\Documents\Visual Studio 2017\Projects\sunamo\sunamo\Constants\XlfKeys.cs";
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
    public const string RLDataEn = SunamoNotTranslateAble.RLDataEn;
    public const string RLDataCs = SunamoNotTranslateAble.RLDataCs;
    public const string RLDataEn2 = SunamoNotTranslateAble.RLDataEn2;
    public const string SessI18n = SunamoNotTranslateAble.SessI18n;
    public const string XlfKeysDot = SunamoNotTranslateAble.XlfKeysDot;

    /// <summary>
    /// XmlLocalisationInterchangeFileFormatSunamo.removeSessI18nIfLineContains
    /// </summary>
    public static List<string> removeSessI18nIfLineContains = CA.ToList<string>("MSStoredProceduresI");

    /// <summary>
    /// Before is possible use ReplaceRlDataToSessionI18n
    /// </summary>
    /// <param name="c"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Before is possible use ReplaceRlDataToSessionI18n
    /// </summary>
    /// <param name="c"></param>
    /// <returns></returns>
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
            return SessI18n + XlfKeysDot + key2 + ")";
        }
        else if (ext == AllExtensions.ts)
        {
            return "su.en(\"" + key2 + "\")";
        }
        ThrowExceptions.NotImplementedCase(Exc.GetStackTrace(), type, Exc.CallingMethod(), ext);
        return null;
    }


}
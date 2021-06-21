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
    public static string pathXlfKeys = @"E:\Documents\vs\Projects\sunamo\sunamo\Constants\XlfKeys.cs";
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
    /// SunamoPageHelperSunamo.i18n(
    /// </summary>
    public const string RLDataEn = SunamoNotTranslateAble.RLDataEn;
    public const string RLDataCs = SunamoNotTranslateAble.RLDataCs;
    public const string RLDataEn2 = SunamoNotTranslateAble.RLDataEn2;
    public const string SessI18n = SunamoNotTranslateAble.SessI18n;
    public const string XlfKeysDot = SunamoNotTranslateAble.XlfKeysDot;
    public const string SessI18nShort = SunamoNotTranslateAble.SessI18nShort;






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
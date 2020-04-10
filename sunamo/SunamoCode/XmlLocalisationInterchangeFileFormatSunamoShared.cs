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

    

    /// <summary>
    /// A1 can be full path
    /// </summary>
    /// <param name="s"></param>
    public static Langs GetLangFromFilename(string s)
    {
        s = FS.GetFileNameWithoutExtension(s);
        var parts = SH.Split(s, AllChars.dot);
        string last = parts[parts.Count - 1].ToLower();
        if (last.StartsWith("cs"))
        {
            return Langs.cs;
        }

        return Langs.en;
    }

    /// <summary>
    /// RLData.en[
    /// </summary>
    public const string RLDataEn = "RLData.en[";
    public const string XlfKeysDot = "XlfKeys.";

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
/// <summary>
/// Must be directly in standard, not SunamoCode.apps because SunamoCode.apps is derive from standard and I need class XmlLocalisationInterchangeFileFormat in standard
/// </summary>
public static  class XmlLocalisationInterchangeFileFormatSunamo{ 
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
}
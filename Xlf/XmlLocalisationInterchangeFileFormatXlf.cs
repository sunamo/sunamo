using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SunamoExceptions;
using Xlf;

public class XmlLocalisationInterchangeFileFormatXlf
{
    /// <summary>
    /// A1 can be full path
    /// </summary>
    /// <param name="s"></param>
    public static Langs GetLangFromFilename(string s)
    {
        s = Path.GetFileNameWithoutExtension(s);
        List<string> parts = null;
        if (s.Contains(AllStrings.lowbar))
        {
            parts = SH.Split(s, AllChars.lowbar);
        }
        else
        {
            parts = SH.Split(s, AllChars.dot, AllChars.dash);
        }
        string beforeLast = parts[parts.Count - 2].ToLower();
        if (beforeLast.StartsWith("cs"))
        {
            return Langs.cs;
        }

        return Langs.en;
    }
}
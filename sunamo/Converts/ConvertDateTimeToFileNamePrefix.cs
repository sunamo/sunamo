using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class ConvertDateTimeToFileNamePrefix
{
    private static char s_delimiter = AllChars.lowbar;

    /// <summary>
    /// Převede z data na název souboru bez přípony
    /// Pokud A1 bude obsahovat delimiter(teď _), budou nahrazeny za mezeru. Je to na Začátku, stačí při parsování použít metodu SH.SplitToPartsFromEnd
    /// </summary>
    public static string ToConvention(string prefix, DateTime dt, bool time)
    {
        //prefix = SH.ReplaceAll(prefix, AllStrings.space, AllStrings.lowbar);
        return prefix + s_delimiter + DTHelper.DateTimeToFileName(dt, time);
    }

    /// <summary>
    /// Převede z názvu souboru na datum
    /// Automaticky rozpozná poslední čas z A1
    /// </summary>
    /// <param name="fnwoe"></param>
    public static DateTime? FromConvention(string fnwoe, bool time)
    {
        string prefix = "";
        return DTHelper.FileNameToDateTimePrefix(fnwoe, time, out prefix);
    }
}
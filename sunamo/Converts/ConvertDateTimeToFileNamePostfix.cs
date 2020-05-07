using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class ConvertDateTimeToFileNamePostfix
{
    private static char s_delimiter = AllChars.lowbar;

    /// <summary>
    /// Převede z data na název souboru bez přípony
    /// Pokud A1 bude obsahovat delimiter(teď _), nebudou nahrazeny za mezeru. Je to na konci, stačí při parsování použít metodu SH.SplitToParts
    /// </summary>
    public static string ToConvention(string postfix, DateTime dt, bool time)
    {
        //postfix = SH.ReplaceAll(postfix, AllStrings.space, AllStrings.lowbar);
        return DTHelper.DateTimeToFileName(dt, time) + s_delimiter + postfix;
    }

    /// <summary>
    /// POUžívá se pokud nechceš zjistit postfix, pokud chceš, použij normálně metodu DTHelper.FileNameToDateTimePostfix
    /// Převede z názvu souboru na datum
    /// Automaticky rozpozná poslední čas z A1
    /// </summary>
    /// <param name="fnwoe"></param>
    public static DateTime? FromConvention(string fnwoe, bool time)
    {
        string postfix = "";
        return DTHelper.FileNameToDateTimePostfix(fnwoe, time, out postfix);
    }
}
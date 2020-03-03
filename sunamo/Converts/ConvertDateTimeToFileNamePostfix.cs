using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class ConvertDateTimeToFileNamePostfix
{
    private static char s_delimiter = AllChars.us;

    /// <summary>
    /// Převede z data na název souboru bez přípony
    /// Pokud A1 bude obsahovat delimiter(teď _), nebudou nahrazeny za mezeru. Je to na konci, stačí při parsování použít metodu SH.SplitToParts
    /// </summary>
    
    public static DateTime? FromConvention(string fnwoe, bool time)
    {
        string postfix = "";
        return DTHelper.FileNameToDateTimePostfix(fnwoe, time, out postfix);
    }
}


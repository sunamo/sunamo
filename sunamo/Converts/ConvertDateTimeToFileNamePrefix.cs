using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    public class ConvertDateTimeToFileNamePrefix
    {
        static char delimiter = '_';

    /// <summary>
    /// Převede z data na název souboru bez přípony
    /// Pokud A1 bude obsahovat delimiter(teď _), budou nahrazeny za mezeru. Je to na Začátku, stačí při parsování použít metodu SH.SplitToPartsFromEnd
    /// </summary>
    /// <returns></returns>
    public static string ToConvention(string prefix, DateTime dt, bool time)
        {
        //prefix = SH.ReplaceAll(prefix, " ", "_");
            return prefix + delimiter + DTHelper.DateTimeToFileName(dt, time);
        }

        /// <summary>
        /// Převede z názvu souboru na datum
        /// Automaticky rozpozná poslední čas z A1
        /// </summary>
        /// <param name="fnwoe"></param>
        /// <returns></returns>
        public static DateTime? FromConvention(string fnwoe, bool time)
        {
            string prefix = "";
            return DTHelper.FileNameToDateTimePrefix(fnwoe, time, out prefix);
        }
    }


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    public class ConvertDateTimeToFileNamePostfix
    {
        static char delimiter = '_';

        /// <summary>
        /// Převede z data na název souboru bez přípony
        /// Pokud A1 bude obsahovat delimiter(teď _), nebudou nahrazeny za mezeru. Je to na konci, stačí při parsování použít metodu SH.SplitToParts
        /// </summary>
        /// <returns></returns>
        public static string ToConvention(string postfix, DateTime dt, bool time)
        {
            //postfix = SH.ReplaceAll(postfix, " ", "_");
            return DTHelper.DateTimeToFileName(dt, time) + delimiter + postfix;
        }

        /// <summary>
        /// POUžívá se pokud nechceš zjistit postfix, pokud chceš, použij normálně metodu DTHelper.FileNameToDateTimePostfix
        /// Převede z názvu souboru na datum
        /// Automaticky rozpozná poslední čas z A1
        /// </summary>
        /// <param name="fnwoe"></param>
        /// <returns></returns>
        public static DateTime? FromConvention(string fnwoe, bool time)
        {
            string postfix = "";
            return DTHelper.FileNameToDateTimePostfix(fnwoe, time, out postfix);
        }
    }


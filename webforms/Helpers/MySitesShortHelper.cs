using sunamo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace webforms.Helpers
{
    public class MySitesShortHelper
    {
        public static MySitesShort GetMss(string table)
        {
            bool pok = false;
            return GetMss(table, ref pok);
        }

        /// <summary>
        /// A2 whether was founded exactly match
        /// </summary>
        /// <param name="table"></param>
        /// <param name="pok"></param>
        /// <returns></returns>
        public static MySitesShort GetMss(string table, ref bool pok)
        {
            MySitesShort mss = MySitesShort.Nope;

            pok = false;
            //List<string> tabulkyNope = webforms.MSStoredProceduresI.ci.SelectGetAllTablesInDBStartedWith(MySitesShort.Nope);
            var tabulkyNope = Reflection.GetConsts(typeof(Tables)).Select(d => d.Name).Where(r => !r.Contains(AllChars.us));

            if (table.Length > 3 && table.Contains(AllChars.us))
            {
                if (Enum.TryParse<MySitesShort>(table.Substring(0, 3), false, out mss))
                {
                    if (table[3] == '_')
                    {
                        pok = true;
                    }
                }
                else
                {
                    // keep 8 Nope
                }
            }
            else
            {
                // keep 8 Nope
            }
            return mss;
        }

        public static MySitesShort GetMySitesShortFromTableName(string table)
        {
            MySitesShort mss = MySitesShort.Nope;
            if (Enum.TryParse<MySitesShort>(table.Substring(0, 3), false, out mss))
            {
                return mss;
            }
            else
            {
                return MySitesShort.None;
            }
        }
    }
}

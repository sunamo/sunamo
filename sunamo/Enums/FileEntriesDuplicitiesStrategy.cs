using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sunamo.Enums
{
    /// <summary>
    /// Jak se má program zachovat pokud nalezne stejně pojmmenovanou složku nebo soubor
    /// </summary>
    public enum FileEntriesDuplicitiesStrategy
    {
        /// <summary>
        /// Přidá sérii k souboru (1)
        /// </summary>
        Serie,
        /// <summary>
        /// Přidá čas k názvu souboru
        /// </summary>
        Time
    }
}

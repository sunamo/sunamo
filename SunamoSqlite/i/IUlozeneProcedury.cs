using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;

namespace DocArch.SqLite
{
    interface IUlozeneProcedury
    {
        SQLiteCommand InsertRowTypeEnumIfNotExists(string tabulka, string nazev);
        SQLiteCommand DeleteTableIfExists(string nazevTabulky);
    }
}

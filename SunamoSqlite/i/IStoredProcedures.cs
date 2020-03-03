using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;

    public interface IStoredProcedures
    {
        SQLiteCommand InsertRowTypeEnumIfNotExists(string tabulka, string nazev);
        SQLiteCommand DeleteTableIfExists(string nazevTabulky);
    }
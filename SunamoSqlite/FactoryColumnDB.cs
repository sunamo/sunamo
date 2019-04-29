using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;


public class FactoryColumnDB : IFactoryColumnDB<SloupecDB, TypeAffinity>
{
    public SloupecDB CreateInstance(TypeAffinity typ, string nazev, Signed signed, bool canBeNull, bool mustBeUnique, string referencesTable, string referencesColumn, bool primaryKey)
    {
        SloupecDB column = new SloupecDB();
        
        
        bool isNewId = false;
        column.typ = typ; // ConvertSqlDbType.ToSqlDbType(typ, out isNewId);
        column.isNewId = isNewId;
        column.Name = nazev;
        column._signed = signed;
        column.canBeNull = canBeNull;
        column.mustBeUnique = mustBeUnique;
        column.referencesTable = referencesTable;
        column.referencesColumn = referencesColumn;
        column.primaryKey = primaryKey;

        return column;
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class MSFactoryColumnDB : IFactoryColumnDB<MSSloupecDB, SqlDbType2>
{
    public static IFactoryColumnDB<MSSloupecDB, SqlDbType2> Instance = new MSFactoryColumnDB();

    private MSFactoryColumnDB() {
    }

    public MSSloupecDB CreateInstance(SqlDbType2 typ, string nazev, Signed signed, bool canBeNull, bool mustBeUnique, string referencesTable, string referencesColumn, bool primaryKey)
    {
        MSSloupecDB column = new MSSloupecDB();
        bool isNewId = false;

        if (nazev.Trim() == "Uri")
        {

        }
        else
        {

        }

        column.typ = typ; //ConvertSqlDbType.ToSqlDbType(typ, out isNewId);
        
        if (column.Type == SqlDbType2.NChar || column.Type == SqlDbType2.NVarChar)
        {
            column.IsUnicode = true;
        }
        else if (column.Type == SqlDbType2.Char || column.Type == SqlDbType2.VarChar)
        {
            column.IsUnicode = false;
        }

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
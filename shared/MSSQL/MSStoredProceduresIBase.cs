using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using sunamo;
using sunamo.Values;
public partial class MSStoredProceduresIBase : SqlServerHelper
{


    protected MSStoredProceduresIBase()
    {
    }

    public DataTable DeleteAllSmallerThanWithOutput(string TableName, string sloupceJezVratit, string nameColumnSmallerThan, object valueColumnSmallerThan, AB[] whereIs, AB[] whereIsNot)
    {
        AB[] whereSmallerThan = CA.ToArrayT<AB>(AB.Get(nameColumnSmallerThan, valueColumnSmallerThan));
        string whereS = GeneratorMsSql.CombinedWhere(whereIs, whereIsNot, null, whereSmallerThan);
        SqlCommand comm = new SqlCommand("DELETE FROM " + TableName + GeneratorMsSql.OutputDeleted(sloupceJezVratit) + whereS);
        AddCommandParameteresCombinedArrays(comm, 0, whereIs, whereIsNot, null, whereSmallerThan);
        DataTable dt = SelectDataTable(comm);
        return dt;
    }

    public DataTable DeleteWithOutput(string TableName, string sloupceJezVratit, string idColumn, object idValue)
    {
        SqlCommand comm = new SqlCommand("DELETE FROM " + TableName + GeneratorMsSql.OutputDeleted(sloupceJezVratit) + GeneratorMsSql.SimpleWhere(idColumn));
        AddCommandParameter(comm, 0, idValue);
        DataTable dt = SelectDataTable(comm);
        return dt;
    }

    public bool HasAnyValue(string table, string columnName, string iDColumnName, int idColumnValue)
    {
        return SelectCellDataTableStringOneRow(table, columnName, iDColumnName, idColumnValue) != "";
    }

    public class Parse
    {
        public class DateTime
        {
            /// <summary>
            /// Vrátí -1 v případě že se nepodaří vyparsovat
            /// </summary>
            /// <param name = "p"></param>
            /// <returns></returns>
            public System.DateTime ParseDateTimeMinVal(string p)
            {
                System.DateTime p2;
                if (System.DateTime.TryParse(p, out p2))
                {
                    return p2;
                }

                return MSStoredProceduresI.DateTimeMinVal;
            }
        }
    }

    public int SelectID(string tabulka, string nazevSloupce, object hodnotaSloupce)
    {
        return SelectID(false, tabulka, nazevSloupce, hodnotaSloupce);
    }

    

    private static void AddCommandParameteresCombinedArrays(SqlCommand comm, int i2, ABC where, ABC isNotWhere, ABC greaterThanWhere, ABC lowerThanWhere)
    {
        int l = CA.GetLength(where);
        l += CA.GetLength(isNotWhere);
        l += CA.GetLength(greaterThanWhere);
        l += CA.GetLength(lowerThanWhere);
        AB[] ab = new AB[l];
        int dex = 0;
        if (where != null)
        {
            for (int i = 0; i < where.Count; i++)
            {
                ab[dex++] = where[i];
            }
        }

        if (isNotWhere != null)
        {
            for (int i = 0; i < isNotWhere.Count; i++)
            {
                ab[dex++] = isNotWhere[i];
            }
        }

        if (greaterThanWhere != null)
        {
            for (int i = 0; i < greaterThanWhere.Count; i++)
            {
                ab[dex++] = greaterThanWhere[i];
            }
        }

        if (lowerThanWhere != null)
        {
            for (int i = 0; i < lowerThanWhere.Count; i++)
            {
                ab[dex++] = lowerThanWhere[i];
            }
        }

        AddCommandParameteresArrays(comm, i2, ab);
    }

    public List<bool> DataTableToListBool(DataTable dataTable, int dex)
    {
        List<bool> vr = new List<bool>(dataTable.Rows.Count);
        foreach (DataRow item in dataTable.Rows)
        {
            vr.Add((bool)item.ItemArray[dex]);
        //vr.Add(bool.Parse(item.ItemArray[dex].ToString()));
        }

        return vr;
    }

    public List<short> DataTableToListShort(DataTable dataTable, int p)
    {
        List<short> vr = new List<short>(dataTable.Rows.Count);
        foreach (DataRow item in dataTable.Rows)
        {
            vr.Add((short)item.ItemArray[p]);
        //vr.Add(bool.Parse(item.ItemArray[dex].ToString()));
        }

        return vr;
    }

    public List<int> DataTableToListInt(DataTable dataTable, int p)
    {
        List<int> vr = new List<int>(dataTable.Rows.Count);
        foreach (DataRow item in dataTable.Rows)
        {
            vr.Add((int)item.ItemArray[p]);
        //vr.Add(bool.Parse(item.ItemArray[dex].ToString()));
        }

        return vr;
    }

    public List<string> DataTableToListString(DataTable dataTable, int dex)
    {
        List<string> vr = new List<string>(dataTable.Rows.Count);
        foreach (DataRow item in dataTable.Rows)
        {
            vr.Add(item.ItemArray[dex].ToString());
        }

        return vr;
    }

    public int DeleteAllSmallerThan(string TableName, string nameColumnSmallerThan, object valueColumnSmallerThan, params AB[] where)
    {
        AB[] whereSmallerThan = CA.ToArrayT<AB>(AB.Get(nameColumnSmallerThan, valueColumnSmallerThan));
        string whereS = GeneratorMsSql.CombinedWhere(where, null, null, whereSmallerThan);
        SqlCommand comm = new SqlCommand("DELETE FROM " + TableName + whereS);
        AddCommandParameteresCombinedArrays(comm, 0, where, null, null, whereSmallerThan);
        int f = ExecuteNonQuery(comm);
        return f;
    }

    public List<int> SelectAllInColumnLargerThanInt(string TableName, string columnReturn, string nameColumnLargerThan, object valueColumnLargerThan, params AB[] where)
    {
        AB[] whereSmallerThan = CA.ToArrayT<AB>(AB.Get(nameColumnLargerThan, valueColumnLargerThan));
        string whereS = GeneratorMsSql.CombinedWhere(where, null, whereSmallerThan, null);
        SqlCommand comm = new SqlCommand("select " + columnReturn + " FROM " + TableName + whereS);
        AddCommandParameteresCombinedArrays(comm, 0, where, null, whereSmallerThan, null);
        return ReadValuesInt(comm);
    }

    public int DeleteAllLargerThan(string TableName, string nameColumnLargerThan, object valueColumnLargerThan, params AB[] where)
    {
        AB[] whereSmallerThan = CA.ToArrayT<AB>(AB.Get(nameColumnLargerThan, valueColumnLargerThan));
        string whereS = GeneratorMsSql.CombinedWhere(where, null, null, whereSmallerThan);
        SqlCommand comm = new SqlCommand("DELETE FROM " + TableName + whereS);
        AddCommandParameteresCombinedArrays(comm, 0, where, null, whereSmallerThan, null);
        int f = ExecuteNonQuery(comm);
        return f;
    }

    /// <summary>
    /// Pouýžívá se když chceš odstranit více řádků najednou pomocí AB. Nedá se použít pokud aspoň na jednom řádku potřebuješ AND
    /// </summary>
    /// <param name = "p"></param>
    /// <param name = "aB"></param>
    /// <returns></returns>
    public int DeleteOR(string TableName, params AB[] where)
    {
        string whereS = GeneratorMsSql.CombinedWhereOR(where);
        SqlCommand comm = new SqlCommand("DELETE FROM " + TableName + whereS);
        AddCommandParameterFromAbc(comm, where);
        int f = ExecuteNonQuery(comm);
        return f;
    }

    public void DropAllTables()
    {
        List<string> dd = SelectGetAllTablesInDB();
        foreach (string item in dd)
        {
            ExecuteNonQuery(new SqlCommand("DROP TABLE " + item));
        }
    }

    private float ExecuteScalarFloat(bool signed, SqlCommand comm)
    {
        object o = ExecuteScalar(comm);
        if (o == null)
        {
            if (signed)
            {
                return float.MaxValue;
            //return short.MaxValue;
            }
            else
            {
                return -1;
            }
        }

        return Convert.ToSingle(o);
    }

    private long ExecuteScalarLong(bool signed, SqlCommand comm)
    {
        object o = ExecuteScalar(comm);
        if (o == null)
        {
            if (signed)
            {
                return long.MaxValue;
            }
            else
            {
                return -1;
            }
        }

        return Convert.ToInt64(o);
    }

    private bool? ExecuteScalarNullableBool(SqlCommand comm)
    {
        object o = ExecuteScalar(comm);
        if (o == null)
        {
            return null;
        }

        return Convert.ToBoolean(o);
    }

    /// <summary>
    /// 3
    /// A2 může být ID nebo cokoliv začínající na ID(ID*)
    /// A2 je ID řádku na který se bude vkládat. Název/hodnota/whatever A2 už nesmí být v A3
    /// Používej tehdy když chceš určit index na který vkládat.
    /// </summary>
    /// <param name = "tabulka"></param>
    /// <param name = "IDUsers"></param>
    /// <param name = "sloupce"></param>
    public void Insert3(string tabulka, long IDUsers, params object[] sloupce)
    {
        sloupce = CA.TwoDimensionParamsIntoOne(sloupce);
        string hodnoty = MSDatabaseLayer.GetValues(CA.JoinVariableAndArray(IDUsers, sloupce));
        SqlCommand comm = new SqlCommand(string.Format("INSERT INTO {0} VALUES {1}", tabulka, hodnoty));
        comm.Parameters.AddWithValue("@p0", IDUsers);
        int to = sloupce.Length;
        for (int i = 0; i < to; i++)
        {
            object o = sloupce[i];
            AddCommandParameter(comm, i + 1, o);
        //DateTime.Now.Month;
        }

        ExecuteNonQuery(comm);
    }

    public long Insert5(string table, string nazvySloupcu, params object[] sloupce)
    {
        sloupce = CA.TwoDimensionParamsIntoOne(sloupce);
        string hodnoty = MSDatabaseLayer.GetValues(sloupce);
        SqlCommand comm = new SqlCommand(string.Format("INSERT INTO {0} {2} VALUES {1}", table, hodnoty, nazvySloupcu));
        int to = sloupce.Length;
        for (int i = 0; i < to; i++)
        {
            object o = sloupce[i];
            AddCommandParameter(comm, i, o);
        //DateTime.Now.Month;
        }

        ExecuteNonQuery(comm);
        return Convert.ToInt64(sloupce[0]);
    }

    /// <summary>
    /// Jediná metoda kde můžeš specifikovat sloupce do kterých chceš vložit
    /// Sloupec který nevkládáš musí být auto_increment
    /// ÏD si pak musíš zjistit sám pomocí nějaké identifikátoru - například sloupce Uri
    /// </summary>
    /// <param name = "table"></param>
    /// <param name = "nazvySloupcu"></param>
    /// <param name = "sloupce"></param>
    /// <returns></returns>
    public void Insert6(string table, string nazvySloupcu, params object[] sloupce)
    {
        sloupce = CA.TwoDimensionParamsIntoOne(sloupce);
        string hodnoty = MSDatabaseLayer.GetValues(sloupce);
        SqlCommand comm = new SqlCommand(string.Format("INSERT INTO {0} {2} VALUES {1}", table, hodnoty, nazvySloupcu.Replace(AllStrings.lb, "(newid(),")));
        int to = sloupce.Length;
        for (int i = 0; i < to; i++)
        {
            object o = sloupce[i];
            AddCommandParameter(comm, i, o);
        //DateTime.Now.Month;
        }

        ExecuteNonQuery(comm);
    //return Convert.ToInt64( sloupce[0]);
    }

    public int InsertRowTypeEnum(string tabulka, string nazev)
    {
        int vr = SelectFindOutNumberOfRows(tabulka) + 1;
        SqlCommand c = new SqlCommand(string.Format("INSERT INTO {0} (ID, Name) VALUES (@p0, @p1)", tabulka));
        AddCommandParameter(c, 0, vr);
        AddCommandParameter(c, 1, nazev);
        ExecuteNonQuery(c);
        return vr;
    }

    /// <summary>
    /// Raději používej metodu s 3/2A sloupecID, pokud používáš v tabulce sloupce ID, které se nejmenují ID
    /// Sloupec u kterého se bude určovat poslední index a ten inkrementovat a na ten vkládat je ID
    /// Používej tehdy když ID sloupec má nějaký standardní název, Tedy ID, ne IDUsers atd.
    /// </summary>
    /// <param name = "tabulka"></param>
    /// <param name = "sloupce"></param>
    /// <returns></returns>
    public Guid InsertToRowGuid(string tabulka, params object[] sloupce)
    {
        sloupce = CA.TwoDimensionParamsIntoOne(sloupce);
        return InsertToRowGuid2(tabulka, "ID", sloupce);
    }

    /// <summary>
    /// Do této metody se vkládají hodnoty bez ID
    /// ID se počítá jako v Sqlite - tedy od 1 
    /// A2 je zde proto aby se mohlo určit poslední index a ten inkrementovat a na ten vložit. Název/hodnota/whatever tohoto sloupce musí být 1. v A3.
    /// Používej tehdy když ID sloupec má nějaký speciální název, např. IDUsers
    /// </summary>
    /// <param name = "tabulka"></param>
    /// <param name = "sloupce"></param>
    /// <returns></returns>
    public Guid InsertToRowGuid2(string tabulka, string sloupecID, params object[] sloupce)
    {
        sloupce = CA.TwoDimensionParamsIntoOne(sloupce);
        int hodnotyLenght = sloupce.Length + 1;
        string hodnoty = MSDatabaseLayer.GetValuesDirect(hodnotyLenght);
        SqlCommand comm = new SqlCommand(string.Format("INSERT INTO {0} VALUES {1}", tabulka, hodnoty));
        for (int i = 1; i < hodnotyLenght; i++)
        {
            object o = sloupce[i - 1];
            AddCommandParameter(comm, i, o);
        //DateTime.Now.Month;
        }

        Guid vr = SelectNewId();
        AddCommandParameter(comm, 0, vr);
        ExecuteNonQuery(comm);
        return vr;
    }

    /// <summary>
    /// A2 je ID řádku na který se bude vkládat. Název/hodnota/whatever tohoto sloupce musí být 1. v A3.
    /// Používej tehdy když chceš určit index na který vkládat.
    /// </summary>
    /// <param name = "tabulka"></param>
    /// <param name = "IDUsers"></param>
    /// <param name = "sloupce"></param>
    public void InsertToRowGuid3(string tabulka, Guid IDUsers, params object[] sloupce)
    {
        sloupce = CA.TwoDimensionParamsIntoOne(sloupce);
        string hodnoty = MSDatabaseLayer.GetValues(CA.JoinVariableAndArray(IDUsers, sloupce));
        SqlCommand comm = new SqlCommand(string.Format("INSERT INTO {0} VALUES {1}", tabulka, hodnoty));
        comm.Parameters.AddWithValue("@p0", IDUsers);
        int to = sloupce.Length;
        for (int i = 0; i < to; i++)
        {
            object o = sloupce[i];
            AddCommandParameter(comm, i + 1, o);
        //DateTime.Now.Month;
        }

        ExecuteNonQuery(comm);
    }

    public List<string> SelectValuesOfColumnAllRowsString(string tabulka, string sloupec, string whereSloupec, object whereHodnota, string orderBy = "")
    {
        if (orderBy != "")
        {
            orderBy = AllStrings.space + orderBy;
        }

        SqlCommand comm = new SqlCommand(string.Format("SELECT {0} FROM {1}", sloupec, tabulka) + GeneratorMsSql.SimpleWhere(whereSloupec) + orderBy);
        AddCommandParameter(comm, 0, whereHodnota);
        return ReadValuesString(comm);
    }

    public List<string> SelectValuesOfColumnAllRowsString(string tabulka, string sloupec, ABC where, ABC whereIsNot, string orderBy = "")
    {
        if (orderBy != "")
        {
            orderBy = AllStrings.space + orderBy;
        }

        SqlCommand comm = new SqlCommand(string.Format("SELECT {0} FROM {1}", sloupec, tabulka) + GeneratorMsSql.CombinedWhere(where, whereIsNot, null, null) + orderBy);
        AddCommandParameteresCombinedArrays(comm, 0, where, whereIsNot, null, null);
        return ReadValuesString(comm);
    }

    public int RandomValueFromColumnInt(string table, string column)
    {
        return ExecuteScalarInt(true, new SqlCommand("select " + column + " from " + table + " where " + column + " in (select top 1 " + column + " from " + table + " order by newid())"));
    }

    public short RandomValueFromColumnShort(string table, string column)
    {
        return ExecuteScalarShort(true, new SqlCommand("select " + column + " from " + table + " where " + column + " in (select top 1 " + column + " from " + table + " order by newid())"));
    }

    /// <summary>
    /// Jakékoliv změny zde musíš provést i v metodě SelectValuesOfColumnAllRowsStringTrim
    /// </summary>
    public List<string> SelectValuesOfColumnAllRowsString(string tabulka, string sloupec)
    {
        SqlCommand comm = new SqlCommand(string.Format("SELECT {0} FROM {1}", sloupec, tabulka));
        return ReadValuesString(comm);
    }

    public List<string> SelectValuesOfColumnAllRowsStringTrim(string tabulka, string sloupec, string idn, object idv)
    {
        SqlCommand comm = new SqlCommand(string.Format("SELECT {0} FROM {1}", sloupec, tabulka) + GeneratorMsSql.SimpleWhere(idn));
        AddCommandParameter(comm, 0, idv);
        return ReadValuesStringTrim(comm);
    }

    /// <summary>
    /// Pokud řádek ve sloupci A2 má hodnotu DBNull.Value, zapíšu do výsledku 0
    /// </summary>
    /// <param name = "p1"></param>
    /// <param name = "p2"></param>
    /// <param name = "aB1"></param>
    /// <param name = "aB2"></param>
    /// <returns></returns>
    public List<byte> SelectValuesOfColumnAllRowsByte(string tabulka, object vetsiNez, object mensiNez, string hledanySloupec, params AB[] aB)
    {
        string hodnoty = MSDatabaseLayer.GetValues(aB.ToArray());
        //"OrderVerse"
        SqlCommand comm = new SqlCommand(string.Format("SELECT {0} FROM {1} {2}", hledanySloupec, tabulka, GeneratorMsSql.CombinedWhere(aB, null, new ABC(AB.Get(hledanySloupec, vetsiNez)).ToArray(), new ABC(AB.Get(hledanySloupec, mensiNez)).ToArray())));
        int i = 0;
        for (; i < aB.Length; i++)
        {
            AddCommandParameter(comm, i, aB[i].B);
        }

        i = AddCommandParameter(comm, i, vetsiNez);
        i = AddCommandParameter(comm, i, mensiNez);
        return ReadValuesByte(comm);
    }

    public List<byte> SelectValuesOfColumnByte(string tabulka, string sloupecHledaný, string sloupecVeKteremHledat, object hodnota)
    {
        // SQLiteDataReader je třída zásadně pro práci s jedním řádkem výsledků, ne s 2mi a více !!
        SqlCommand comm = new SqlCommand(string.Format("SELECT {0} FROM {1} WHERE {2} = @p0", sloupecHledaný, tabulka, sloupecVeKteremHledat));
        AddCommandParameter(comm, 0, hodnota);
        return ReadValuesByte(comm);
    }

    

    public List<string> ReadValuesStringTrim(SqlCommand comm)
    {
        List<string> vr = new List<string>();
        SqlDataReader r = ExecuteReader(comm);
        ;
        if (r.HasRows)
        {
            while (r.Read())
            {
                string o = r.GetString(0).TrimEnd(AllChars.space);
                //Type t = val.GetType();
                vr.Add(o);
            }
        }

        return vr;
    }

    private List<string> ReadValuesString(SqlCommand comm)
    {
        List<string> vr = new List<string>();
        SqlDataReader r = ExecuteReader(comm);
        ;
        if (r.HasRows)
        {
            while (r.Read())
            {
                string o = r.GetString(0);
                //Type t = val.GetType();
                vr.Add(o);
            }
        }

        return vr;
    }

    /// <summary>
    /// Vymže tabulku A1 a přejmenuje tabulku A1+"2" na A1
    /// </summary>
    /// <param name = "table"></param>
    public void sp_rename(string table)
    {
        DropTableIfExists(table);
        SqlCommand comm = new SqlCommand("EXEC sp_rename 'dbo." + table + "2', '" + table + "'");
        ExecuteNonQuery(comm);
    }

    /// <summary>
    /// 
    /// </summary>
    public List<string> SelectGetAllTablesInDB()
    {
        List<string> vr = new List<string>();
        DataTable dt = SelectDataTableSelective("INFORMATION_SCHEMA.TABLES", "TABLE_NAME", "TABLE_TYPE", "BASE TABLE");
        foreach (DataRow item in dt.Rows)
        {
            vr.Add(item.ItemArray[0].ToString());
        }

        return vr;
    }

    /// <summary>
    /// 
    /// </summary>
    public List<string> SelectColumnsNamesOfTable(string p)
    {
        List<string> vr = new List<string>();
        DataTable dt = SelectDataTableSelective("INFORMATION_SCHEMA.COLUMNS", "COLUMN_NAME", "TABLE_NAME", p);
        foreach (DataRow item in dt.Rows)
        {
            vr.Add(item.ItemArray[0].ToString());
        }

        return vr;
    }

    /// <summary>
    /// 
    /// </summary>
    public DataTable SelectDataTableLimit(string tableName, int limit, string sloupecWhere, object hodnotaWhere)
    {
        SqlCommand comm = new SqlCommand("SELECT TOP(" + limit.ToString() + ") * FROM " + tableName + GeneratorMsSql.SimpleWhere(sloupecWhere));
        AddCommandParameter(comm, 0, hodnotaWhere);
        return SelectDataTable(comm);
    }

    /// <summary>
    /// A2 je sloupec na který se prohledává pro A3
    /// </summary>
    /// <param name = "tabulka"></param>
    /// <param name = "sloupecID"></param>
    /// <param name = "textPartOfCity"></param>
    /// <param name = "nazvySloupcu"></param>
    /// <returns></returns>
    public DataTable SelectDataTableSelectiveLikeContains(string tabulka, string nazvySloupcu, string sloupecID, string textPartOfCity)
    {
        SqlCommand comm = new SqlCommand(string.Format("SELECT {0} FROM {1} WHERE {2} LIKE AllChars.modulo + @p0 + AllChars.modulo", nazvySloupcu, tabulka, sloupecID));
        AddCommandParameter(comm, 0, textPartOfCity);
        //NT
        return this.SelectDataTable(comm);
    }

    public DataTable SelectAllRowsOfColumnsAB(string table, string vratit, ABC abObsahuje, ABC abNeobsahuje, ABC abVetsiNez, ABC abMensiNez)
    {
        string sql = "SELECT " + vratit + " FROM " + table;
        sql += GeneratorMsSql.CombinedWhere(abObsahuje, abNeobsahuje, abVetsiNez, abMensiNez);
        SqlCommand comm = new SqlCommand(sql);
        int i = 0;
        i = AddCommandParameterFromAbc(comm, abObsahuje, i);
        i = AddCommandParameterFromAbc(comm, abNeobsahuje, i);
        i = AddCommandParameterFromAbc(comm, abVetsiNez, i);
        AddCommandParameterFromAbc(comm, abVetsiNez, i);
        return SelectDataTable(comm);
    }

    /// <summary>
    /// Řadí metodou DESC
    /// Tato metoda se přesně hodí když chci získat nějaký nejoblíbenější obsah - srovnává podle hodnoty v A4.
    /// </summary>
    /// <param name = "tableName"></param>
    /// <param name = "limit"></param>
    /// <param name = "sloupecOrder"></param>
    /// <param name = "abc"></param>
    /// <returns></returns>
    public DataTable SelectDataTableLimitLastRows(string tableName, int limit, string columns, string sloupecOrder, params AB[] where)
    {
        //SELECT TOP 1000 * FROM [SomeTable] ORDER BY MySortColumn DESC
        SqlCommand comm = new SqlCommand("SELECT TOP(" + limit.ToString() + ") " + columns + " FROM " + tableName + GeneratorMsSql.CombinedWhere(where) + " ORDER BY " + sloupecOrder + " DESC");
        AddCommandParameteres(comm, 0, where);
        return SelectDataTable(comm);
    }

    /// <summary>
    /// Řadí metodou DESC
    /// Tato metoda se přesně hodí když chci získat nějaký nejoblíbenější obsah - srovnává podle hodnoty v A4.
    /// </summary>
    /// <param name = "tableName"></param>
    /// <param name = "limit"></param>
    /// <param name = "sloupecOrder"></param>
    /// <param name = "abc"></param>
    /// <returns></returns>
    public DataTable SelectDataTableLimitLastRows(string tableName, int limit, string columns, string sloupecOrder, AB[] whereIs, AB[] whereIsNot, AB[] whereGreaterThan, AB[] whereLowerThan)
    {
        //SELECT TOP 1000 * FROM [SomeTable] ORDER BY MySortColumn DESC
        SqlCommand comm = new SqlCommand("SELECT TOP(" + limit.ToString() + ") " + columns + " FROM " + tableName + GeneratorMsSql.CombinedWhere(whereIs, whereIsNot, whereGreaterThan, whereLowerThan) + " ORDER BY " + sloupecOrder + " DESC");
        AddCommandParameteresCombinedArrays(comm, 0, whereIs, whereIsNot, whereGreaterThan, whereLowerThan);
        return SelectDataTable(comm);
    }

    /// <summary>
    /// Vrátí mi všechny položky ze sloupce 
    /// </summary>
    public DataTable SelectGreaterThan(string tableName, string tableColumn, object hodnotaOd)
    {
        SqlCommand comm = new SqlCommand(string.Format("SELECT * FROM {0} WHERE {1} > @p0", tableName, tableColumn));
        AddCommandParameter(comm, 0, hodnotaOd);
        return SelectDataTable(comm);
    }

    /// <summary>
    /// Tato metoda má přívlastek Columns protože v ní jde specifikovat sloupce které má metoda vrátit
    /// </summary>
    /// <param name = "tableName"></param>
    /// <param name = "limit"></param>
    /// <param name = "sloupce"></param>
    /// <param name = "sloupecWhere"></param>
    /// <param name = "hodnotaWhere"></param>
    /// <returns></returns>
    public DataTable SelectDataTableLimitColumns(string tableName, int limit, string sloupce, string sloupecWhere, object hodnotaWhere)
    {
        SqlCommand comm = new SqlCommand("SELECT TOP(" + limit.ToString() + ") " + sloupce + " FROM " + tableName + GeneratorMsSql.SimpleWhere(sloupecWhere));
        AddCommandParameter(comm, 0, hodnotaWhere);
        return SelectDataTable(comm);
    }

    public DataTable SelectTableInnerJoin(string tableFromWithShortVersion, string tableJoinWithShortVersion, string sloupceJezZiskavat, string onKlazuleOdNuly, params object[] fixniHodnotyOdNuly)
    {
        fixniHodnotyOdNuly = CA.TwoDimensionParamsIntoOne(fixniHodnotyOdNuly);
        return SelectDataTable("select " + sloupceJezZiskavat + " from " + tableFromWithShortVersion + " inner join " + tableJoinWithShortVersion + " on " + onKlazuleOdNuly, fixniHodnotyOdNuly);
    }

    /// <summary>
    /// Řadí metodou DESC
    /// </summary>
    /// <param name = "tableFromWithShortVersion"></param>
    /// <param name = "tableJoinWithShortVersion"></param>
    /// <param name = "sloupceJezZiskavat"></param>
    /// <param name = "onKlazuleOdNuly"></param>
    /// <param name = "limit"></param>
    /// <param name = "sloupecPodleKterehoRadit"></param>
    /// <param name = "whereIs"></param>
    /// <param name = "whereIsNot"></param>
    /// <returns></returns>
    public DataTable SelectDataTableLimitLastRowsInnerJoin(string tableFromWithShortVersion, string tableJoinWithShortVersion, string sloupceJezZiskavat, string onKlazuleOdNuly, int limit, string sloupecPodleKterehoRadit, AB[] whereIs, AB[] whereIsNot, params object[] hodnotyOdNuly)
    {
        hodnotyOdNuly = CA.TwoDimensionParamsIntoOne(hodnotyOdNuly);
        SqlCommand comm = new SqlCommand("select TOP(" + limit.ToString() + ") " + sloupceJezZiskavat + " from " + tableFromWithShortVersion + " inner join " + tableJoinWithShortVersion + " on " + onKlazuleOdNuly + GeneratorMsSql.CombinedWhere(whereIs, whereIsNot, null, null) + " ORDER BY " + sloupecPodleKterehoRadit + " DESC");
        AddCommandParameteres(comm, 0, hodnotyOdNuly);
        AddCommandParameteresCombinedArrays(comm, hodnotyOdNuly.Length, whereIs, whereIsNot, null, null);
        return SelectDataTable(comm);
    }

    public DataTable SelectTableInnerJoin(string tableFromWithShortVersion, string tableJoinWithShortVersion, string sloupceJezZiskavat, string onKlazuleOdNuly, AB[] whereIs, AB[] whereIsNot)
    {
        SqlCommand comm = new SqlCommand("select " + sloupceJezZiskavat + " from " + tableFromWithShortVersion + " inner join " + tableJoinWithShortVersion + " on " + onKlazuleOdNuly + GeneratorMsSql.CombinedWhere(whereIs, whereIsNot, null, null));
        AddCommandParameteresCombinedArrays(comm, 0, whereIs, whereIsNot, null, null);
        return SelectDataTable(comm);
    }

    /// <summary>
    /// Do A4 se zadává např. p.ID = stf.IDPhoto
    /// Tato metoda zde musí být, jinak vzniká chyba No mapping exists from object type AB to a known managed provider native type.
    /// </summary>
    /// <param name = "tableFromWithShortVersion"></param>
    /// <param name = "tableJoinWithShortVersion"></param>
    /// <param name = "sloupceJezZiskavat"></param>
    /// <param name = "onKlazuleOdNuly"></param>
    /// <param name = "where"></param>
    /// <returns></returns>
    public DataTable SelectTableInnerJoin(string tableFromWithShortVersion, string tableJoinWithShortVersion, string sloupceJezZiskavat, string onKlazuleOdNuly, params AB[] where)
    {
        SqlCommand comm = new SqlCommand("select " + sloupceJezZiskavat + " from " + tableFromWithShortVersion + " inner join " + tableJoinWithShortVersion + " on " + onKlazuleOdNuly + GeneratorMsSql.CombinedWhere(where));
        AddCommandParameterFromAbc(comm, where);
        return SelectDataTable(comm);
    }

    /// <summary>
    /// POkud nechceš používat reader, který furt nefugnuje, použij metodu SelectOneRowInnerJoin, má úplně stejnou hlavičku a jen funguje s DataTable
    /// Do A4 se zadává např. p.ID = stf.IDPhoto
    /// </summary>
    public object[] SelectOneRowInnerJoinReader(string tableFromWithShortVersion, string tableJoinWithShortVersion, string sloupceJezZiskavat, string onKlazuleOdNuly, params AB[] where)
    {
        SqlCommand comm = new SqlCommand("select " + sloupceJezZiskavat + " from " + tableFromWithShortVersion + " inner join " + tableJoinWithShortVersion + " on " + onKlazuleOdNuly + GeneratorMsSql.CombinedWhere(where));
        AddCommandParameterFromAbc(comm, where);
        return SelectRowReader(comm);
    }

    public object[] SelectOneRowInnerJoin(string tableFromWithShortVersion, string tableJoinWithShortVersion, string sloupceJezZiskavat, string onKlazuleOdNuly, params AB[] where)
    {
        SqlCommand comm = new SqlCommand("select " + sloupceJezZiskavat + " from " + tableFromWithShortVersion + " inner join " + tableJoinWithShortVersion + " on " + onKlazuleOdNuly + GeneratorMsSql.CombinedWhere(where));
        AddCommandParameterFromAbc(comm, where);
        DataTable dt = SelectDataTable(comm);
        if (dt.Rows.Count == 0)
        {
            return null; // CA.CreateEmptyArray(pocetSloupcu);
        }

        return dt.Rows[0].ItemArray;
    }

    /// <summary>
    /// Do A4 se zadává např. p.ID = stf.IDPhoto
    /// </summary>
    public DataTable SelectTableInnerJoin(string tableFromWithShortVersion, string tableJoinWithShortVersion, string sloupceJezZiskavat, string onKlazuleOdNuly)
    {
        SqlCommand comm = new SqlCommand("select " + sloupceJezZiskavat + " from " + tableFromWithShortVersion + " inner join " + tableJoinWithShortVersion + " on " + onKlazuleOdNuly);
        //AddCommandParameterFromAbc(comm, where);
        return SelectDataTable(comm);
    }

    public void UpdateValuesCombinationCombinedWhere(string TableName, AB[] sets, AB[] where)
    {
        string setString = GeneratorMsSql.CombinedSet(sets);
        string whereString = GeneratorMsSql.CombinedWhere(where);
        int indexParametrWhere = sets.Length + 1;
        SqlCommand comm = new SqlCommand(string.Format("UPDATE {0} {1} WHERE {2}", TableName, setString, whereString));
        for (int i = 0; i < indexParametrWhere; i++)
        {
            // V takových případech se nikdy nepokoušej násobit, protože to vždy končí špatně
            AddCommandParameter(comm, i, sets[i].B);
        }

        indexParametrWhere--;
        for (int i = 0; i < where.Length; i++)
        {
            AddCommandParameter(comm, indexParametrWhere++, where[i].B);
        }

        // NT-Při úpravách uprav i UpdateValuesCombination
        ExecuteNonQuery(comm);
    }

    /// <summary>
    /// Pokud se řádek nepodaří najít, vrátí -1
    /// </summary>
    /// <param name = "table"></param>
    /// <param name = "sloupecID"></param>
    /// <param name = "id"></param>
    /// <param name = "sloupecKUpdate"></param>
    /// <param name = "pridej"></param>
    /// <returns></returns>
    public float UpdateRealValue(string table, string sloupecID, int id, string sloupecKUpdate, float pridej)
    {
        float d = SelectCellDataTableFloatOneRow(true, table, sloupecKUpdate, AB.Get(sloupecID, id));
        if (d != 0 && d != -1 && d != float.MaxValue)
        {
            pridej = (d + pridej) / 2;
        }
        else
        {
            // Zde to má být prázdné
            return -1;
        }

        Update(table, sloupecKUpdate, pridej, sloupecID, id);
        return pridej;
    }

    /// <summary>
    /// Updatuje pouze 1 řádek
    /// </summary>
    /// <param name = "tablename"></param>
    /// <param name = "updateColumn"></param>
    /// <param name = "idColumn"></param>
    /// <param name = "idValue"></param>
    /// <returns></returns>
    public bool UpdateSwitchBool(string tablename, string updateColumn, string idColumn, object idValue)
    {
        bool vr = !SelectCellDataTableBoolOneRow(tablename, idColumn, idValue, updateColumn);
        UpdateOneRow(tablename, updateColumn, vr, idColumn, idValue);
        return vr;
    }

    /// <summary>
    /// 
    /// </summary>
    public int UpdateAppendStringValue(string tableName, string sloupecID, object hodnotaID, string sloupecAppend, string hodnotaAppend)
    {
        string aktual = SelectCellDataTableStringOneRow(tableName, sloupecAppend, sloupecID, hodnotaID);
        aktual += hodnotaAppend;
        return UpdateOneRow(tableName, sloupecAppend, aktual, sloupecID, hodnotaID);
    }

    /// <summary>
    /// Vrátí nohou hodnotu v DB
    /// </summary>
    /// <param name = "table"></param>
    /// <param name = "sloupecID"></param>
    /// <param name = "id"></param>
    /// <param name = "sloupecKUpdate"></param>
    /// <param name = "pridej"></param>
    /// <returns></returns>
    public int UpdateSumIntValue(string table, string sloupecID, object id, string sloupecKUpdate, int pridej)
    {
        int d = SelectCellDataTableIntOneRow(true, table, sloupecKUpdate, AB.Get(sloupecID, id));
        if (d == int.MaxValue)
        {
            return d;
        }

        int n = pridej + d;
        Update(table, sloupecKUpdate, n, sloupecID, id);
        return n;
    }

    public long UpdateSumLongValue(string table, string sloupecID, object id, string sloupecKUpdate, int pridej)
    {
        long d = SelectCellDataTableLongOneRow(false, table, sloupecID, id, sloupecKUpdate);
        long n = pridej + d;
        Update(table, sloupecKUpdate, n, sloupecID, id);
        return n;
    }

    /// <summary>
    /// Nahrazuje ve všech řádcích
    /// </summary>
    /// <param name = "table"></param>
    /// <param name = "sloupecKUpdate"></param>
    /// <param name = "odeber"></param>
    /// <param name = "abc"></param>
    /// <returns></returns>
    public short UpdateMinusShortValue(string table, string sloupecKUpdate, short odeber, params AB[] abc)
    {
        short d = SelectCellDataTableShortOneRow(true, table, sloupecKUpdate, abc);
        if (d == short.MinValue)
        {
            return d;
        }

        odeber = (short)(d - odeber);
        Update(table, sloupecKUpdate, odeber, abc);
        return odeber;
    }

    /// <summary>
    /// Vrací normalizovaný short
    /// </summary>
    /// <param name = "table"></param>
    /// <param name = "sloupecKUpdate"></param>
    /// <param name = "odeber"></param>
    /// <param name = "abc"></param>
    /// <returns></returns>
    public ushort UpdateMinusNormalizedShortValue(string table, string sloupecKUpdate, ushort odeber, params AB[] abc)
    {
        ushort d = NormalizeNumbers.NormalizeShort(SelectCellDataTableShortOneRow(true, table, sloupecKUpdate, abc));
        if (d == NormalizeNumbers.NormalizeShort(short.MinValue))
        {
            return d;
        }

        odeber = (ushort)(d - odeber);
        Update(table, sloupecKUpdate, odeber, abc);
        return odeber;
    }

    public short UpdateMinusShortValue(string table, string sloupecKUpdate, short odeber, string sloupecID, object hodnotaID)
    {
        short d = SelectCellDataTableShortOneRow(true, table, sloupecID, hodnotaID, sloupecKUpdate);
        if (d == short.MaxValue)
        {
            odeber = 0;
        }
        else
        {
            odeber = (short)(d - odeber);
        }

        Update(table, sloupecKUpdate, odeber, sloupecID, hodnotaID);
        return odeber;
    }

    /// <summary>
    /// Aktualizuje všechny řádky
    /// Vrátí novou zapsanou hodnotu
    /// </summary>
    /// <param name = "table"></param>
    /// <param name = "sloupecKUpdate"></param>
    /// <param name = "odeber"></param>
    /// <param name = "abc"></param>
    /// <returns></returns>
    public int UpdateMinusIntValue(string table, string sloupecKUpdate, int odeber, params AB[] abc)
    {
        int d = SelectCellDataTableIntOneRow(true, table, sloupecKUpdate, abc);
        if (d == int.MaxValue)
        {
            return d;
        }

        int n = odeber;
        n = d - odeber;
        Update(table, sloupecKUpdate, n, abc);
        return n;
    }

    public int UpdateMinusIntValue(string table, string sloupecKUpdate, int odeber, string sloupecID, object hodnotaID)
    {
        int d = SelectCellDataTableIntOneRow(true, table, sloupecKUpdate, sloupecID, hodnotaID);
        if (d == int.MinValue)
        {
            return d;
        }

        int n = odeber;
        n = d - odeber;
        Update(table, sloupecKUpdate, n, sloupecID, hodnotaID);
        return n;
    }

    public long UpdateMinusLongValue(string table, string sloupecKUpdate, long odeber, string sloupecID, object hodnotaID)
    {
        long d = SelectCellDataTableLongOneRow(true, table, sloupecID, hodnotaID, sloupecKUpdate);
        if (d == long.MaxValue)
        {
            return d;
        }

        long n = odeber;
        n = d - odeber;
        Update(table, sloupecKUpdate, n, sloupecID, hodnotaID);
        return n;
    }

    public short UpdatePlusShortValue(string table, string sloupecKUpdate, short pridej, string sloupecID, object hodnotaID)
    {
        short d = SelectCellDataTableShortOneRow(true, table, sloupecID, hodnotaID, sloupecKUpdate);
        if (d == short.MaxValue)
        {
            return d;
        }

        short n = pridej;
        n = (short)(d + pridej);
        //}
        Update(table, sloupecKUpdate, n, sloupecID, hodnotaID);
        return n;
    }

    public byte UpdateMinusByteValue(string table, string sloupecKUpdate, byte pridej, string sloupecID, object hodnotaID)
    {
        byte d = SelectCellDataTableByteOneRow(table, sloupecID, hodnotaID, sloupecKUpdate);
        if (d == 255)
        {
            return d;
        }
        else
        {
            pridej = (byte)(d + pridej);
        }

        Update(table, sloupecKUpdate, pridej, sloupecID, hodnotaID);
        return pridej;
    }

    /// <summary>
    /// Pouze když hodnota nebude existovat, přidá ji znovu
    /// </summary>
    /// <param name = "tableName"></param>
    /// <param name = "sloupecID"></param>
    /// <param name = "hodnotaID"></param>
    /// <param name = "sloupecAppend"></param>
    /// <param name = "hodnotaAppend"></param>
    /// <returns></returns>
    public int UpdateAppendStringValueCheckExistsOneRow(string tableName, string sloupecAppend, string hodnotaAppend, string sloupecID, object hodnotaID)
    {
        string aktual = SelectCellDataTableStringOneRow(tableName, sloupecAppend, sloupecID, hodnotaID);
        List<string> d = new List<string>(SH.Split(aktual, AllStrings.comma));
        if (!d.Contains(hodnotaAppend))
        {
            aktual += hodnotaAppend + AllStrings.comma;
            string save = SH.Join(AllChars.comma, d.ToArray());
            return UpdateOneRow(tableName, sloupecAppend, aktual, sloupecID, hodnotaID);
        }

        return 0;
    }

    public DataTable SelectDateTableGroupBy(string table, string columns, string groupByColumns)
    {
        string sql = "select " + columns + " from " + table + " group by " + groupByColumns;
        SqlCommand comm = new SqlCommand(sql);
        //AddCommandParameter(comm, 0, IDColumnValue);
        return SelectDataTable(comm);
    }

    /// <summary>
    /// 
    /// </summary>
    public int UpdateCutStringValue(string tableName, string sloupecCut, string hodnotaCut, string sloupecID, object hodnotaID)
    {
        string aktual = SelectCellDataTableStringOneRow(tableName, sloupecCut, sloupecID, hodnotaID);
        List<string> d = new List<string>(SH.Split(aktual, AllStrings.comma));
        d.Remove(hodnotaCut);
        string save = SH.JoinWithoutTrim(AllStrings.comma, d);
        return UpdateOneRow(tableName, sloupecCut, save, sloupecID, hodnotaID);
    }

    /// <summary>
    /// Conn přidá automaticky
    /// Název metody je sice OneRow ale updatuje to libovolný počet řádků které to najde pomocí where - je to moje interní pojmenování aby mě to někdy trklo, možná později přijdu na způsob jak updatovat jen jeden řádek.
    /// </summary>
    public int UpdateOneRow(string table, string sloupecKUpdate, object n, string sloupecID, object id)
    {
        string sql = string.Format("UPDATE TOP(1) {0} SET {1}=@p1 WHERE {2} = @p2", table, sloupecKUpdate, sloupecID);
        SqlCommand comm = new SqlCommand(sql);
        AddCommandParameter(comm, 1, n);
        AddCommandParameter(comm, 2, id);
        return ExecuteNonQuery(comm);
    }


    public int UpdateWhereIsLowerThan(string table, string columnToUpdate, object newValue, string columnLowerThan, DateTime valueLowerThan, params AB[] where)
    {
        AB[] lowerThan = CA.ToArrayT<AB>(AB.Get(columnLowerThan, valueLowerThan));
        int parametrSet = lowerThan.Length + 1;
        string sql = string.Format("UPDATE {0} SET {1}=@p" + parametrSet + " {2}", table, columnToUpdate, GeneratorMsSql.CombinedWhere(where, null, null, lowerThan));
        SqlCommand comm = new SqlCommand(sql);
        AddCommandParameter(comm, parametrSet, newValue);
        AddCommandParameteresCombinedArrays(comm, 0, where, null, null, lowerThan);
        int vr = ExecuteNonQuery(comm);
        return vr;
    }

    public int UpdateMany(string table, string columnID, object valueID, params AB[] update)
    {
        int vr = 0;
        foreach (AB item in update)
        {
            string sql = string.Format("UPDATE {0} SET {1} = @p0 {2}", table, item.A, GeneratorMsSql.SimpleWhere("ID", 1));
            SqlCommand comm = new SqlCommand(sql);
            AddCommandParameter(comm, 0, item.B);
            AddCommandParameter(comm, 1, valueID);
            vr += ExecuteNonQuery(comm);
        }

        return vr;
    }

    /// <summary>
    /// Pracuje jako signed.
    /// Vrací skutečně nejvyšší ID, proto když chceš pomocí ní ukládat do DB, musíš si to číslo inkrementovat
    /// Ignoruje vynechaná čísla. Žádná hodnota v sloupci A2 nebyla nalezena, vrátí long.MaxValue
    /// </summary>
    /// <param name = "p"></param>
    /// <param name = "sloupecID"></param>
    /// <returns></returns>
    public long SelectLastIDFromTable(string p, string sloupecID)
    {
        return ExecuteScalarLong(true, new SqlCommand("SELECT MAX(" + sloupecID + ") FROM " + p));
    }

    public int SelectFirstAvailableIntIndex(bool signed, string table, string column)
    {
        if (SelectCount(table) == 0)
        {
            if (signed)
            {
                return int.MinValue;
            }
            else
            {
                return 0;
            }
        }

        // TODO: Zde to pak nahradit metodou, která toto bude dělat přímo v databázi a vrátí mi pouze výsledek
        List<int> allIDs = SelectValuesOfColumnAllRowsInt(table, column);
        allIDs.Sort();
        int to = allIDs[allIDs.Count - 1];
        int y = 0;
        int i = int.MinValue;
        for (; i < to; i++)
        {
            if (allIDs[y] != i)
            {
                return i;
            }

            y++;
        }

        return ++i;
    }

    public short SelectFirstAvailableShortIndex(bool signed, string table, string column)
    {
        if (SelectCount(table) == 0)
        {
            if (signed)
            {
                return short.MinValue;
            }
            else
            {
                return 0;
            }
        }

        // TODO: Zde to pak nahradit metodou, která toto bude dělat přímo v databázi a vrátí mi pouze výsledek
        List<short> allIDs = SelectValuesOfColumnAllRowsShort(table, column);
        allIDs.Sort();
        short to = allIDs[allIDs.Count - 1];
        int y = 0;
        short i = short.MinValue;
        for (; i < to; i++)
        {
            if (allIDs[y] != i)
            {
                return i;
            }

            y++;
        }

        return ++i;
    }

    public short SelectMaxShortMinValue(string table, string column)
    {
        if (SelectCount(table) == 0)
        {
            return short.MinValue;
        }

        return ExecuteScalarShort(true, new SqlCommand("SELECT MAX(" + column + ") FROM " + table));
    }

    /// <summary>
    /// Vrací 0 pokud tabulka nebude mít žádné řádky, na rozdíl od metody SelectMaxIntMinValue, která vrací int.MinValue
    /// </summary>
    /// <param name = "table"></param>
    /// <param name = "column"></param>
    /// <returns></returns>
    public int SelectMaxInt(string table, string column)
    {
        if (SelectCount(table) == 0)
        {
            return 0;
        }

        return ExecuteScalarInt(true, new SqlCommand("SELECT MAX(" + column + ") FROM " + table));
    }

    public short SelectMinShortMinValue(string table, string sloupec, params AB[] aB)
    {
        SqlCommand comm = new SqlCommand("SELECT MIN(" + sloupec + ") FROM " + table + GeneratorMsSql.CombinedWhere(aB));
        AddCommandParameteres(comm, 0, aB);
        return ExecuteScalarShort(true, comm);
    }

    public byte SelectMaxByte(string table, string column, params AB[] aB)
    {
        if (SelectCount(table) == 0)
        {
            return 0;
        }

        ABC abc = new ABC(aB);
        return Convert.ToByte(ExecuteScalar("SELECT MAX(" + column + ") FROM " + table + GeneratorMsSql.CombinedWhere(aB), abc.OnlyBs()));
    }

    public int SelectMinInt(string table, string column)
    {
        if (SelectCount(table) == 0)
        {
            return 0;
        }

        return ExecuteScalarInt(true, new SqlCommand("SELECT MIN(" + column + ") FROM " + table));
    }

    public Guid SelectNewId()
    {
        // NEWSEQUENTIALID() zde nemůžu použít, to se může pouze při vytváření nové tabulky
        return new Guid(ExecuteScalar("SELECT NEWID()").ToString());
    }

    public long SelectCountOrMinusOne(string table)
    {
        if (!SelectExistsTable(table))
        {
            return -1;
        }

        return Convert.ToInt64(ExecuteScalar("SELECT COUNT(*) FROM " + table));
    }

    public List<long> SelectGroupByLong(string table, string GroupByColumn, params AB[] where)
    {
        string sql = "select " + GroupByColumn + " from " + table + GeneratorMsSql.CombinedWhere(where);
        SqlCommand comm = new SqlCommand(sql);
        //AddCommandParameter(comm, 0, IDColumnValue);
        AddCommandParameterFromAbc(comm, where);
        return ReadValuesLong(comm);
    }

    public List<int> SelectGroupByInt(string table, string GroupByColumn, params AB[] where)
    {
        string sql = "select " + GroupByColumn + " from " + table + GeneratorMsSql.CombinedWhere(where);
        SqlCommand comm = new SqlCommand(sql);
        //AddCommandParameter(comm, 0, IDColumnValue);
        AddCommandParameterFromAbc(comm, where);
        return ReadValuesInt(comm);
    }

    public List<long> SelectGroupByLong(bool signed, string table, string GroupByColumn, string IDColumnName, object IDColumnValue)
    {
        string sql = "select " + GroupByColumn + " from " + table + GeneratorMsSql.SimpleWhere(IDColumnName) + " group by " + GroupByColumn;
        SqlCommand comm = new SqlCommand(sql);
        AddCommandParameter(comm, 0, IDColumnValue);
        return ReadValuesLong(comm);
    }

    /// <summary>
    /// Zjištuje to ze všech řádků v databázi.
    /// </summary>
    /// <param name = "from"></param>
    /// <param name = "to"></param>
    /// <param name = "table"></param>
    /// <param name = "idEntity"></param>
    /// <returns></returns>
    public uint SelectSumOfViewCount(string table, long idEntity)
    {
        SqlCommand comm = new SqlCommand("select SUM(ViewCount) from " + table + " where EntityID = @p0");
        //SqlCommand comm2 = new SqlCommand("select count(*) as AccValue from PageViews where Date <= @p0 AND Date >= @p1 AND IDPage = @p2");
        AddCommandParameter(comm, 0, idEntity);
        object o = ExecuteScalar(comm);
        if (o == null || o == DBNull.Value)
        {
            return 0;
        }

        return Convert.ToUInt32(o);
    }

    public int SelectSum(string table, string columnToSum, params AB[] aB)
    {
        //DataTable dt = SelectDataTableSelectiveCombination()
        List<int> nt = SelectValuesOfColumnAllRowsInt(true, table, columnToSum, aB);
        return nt.Sum();
    }

    public int SelectSumByte(string table, string columnToSum, params AB[] aB)
    {
        int vr = 0;
        List<byte> nt = SelectValuesOfColumnAllRowsByte(table, columnToSum, aB);
        foreach (var item in nt)
        {
            vr += item;
        }

        return vr;
    }

    /// <summary>
    /// Tuto metodu nepoužívej například po vkládání, když chceš zjistit ID posledního řádku, protože když tam bude něco smazaného , tak to budeš mít o to posunuté !!
    /// 
    /// </summary>
    public int SelectFindOutNumberOfRows(string tabulka)
    {
        SqlCommand comm = new SqlCommand("SELECT Count(*) FROM " + tabulka);
        //comm.Transaction = tran;
        return Convert.ToInt32(ExecuteScalar(comm));
    }

    /// <summary>
    /// 
    /// </summary>
    public List<string> SelectNamesOfIDs(string tabulka, List<int> idFces)
    {
        List<string> vr = new List<string>();
        foreach (int var in idFces)
        {
            vr.Add(SelectNameOfID(tabulka, var));
        }

        return vr;
    }

    /// <summary>
    /// 
    /// </summary>
    public int SelectID(bool signed, string tabulka, string nazevSloupce, object hodnotaSloupce)
    {
        SqlCommand c = new SqlCommand(string.Format("SELECT (ID) FROM {0} WHERE {1} = @p0", tabulka, nazevSloupce));
        AddCommandParameter(c, 0, hodnotaSloupce);
        return ExecuteScalarInt(signed, c);
    }

    public string SelectNameOfIDOrSE(string tabulka, string idColumnName, int id)
    {
        return SelectCellDataTableStringOneRow(tabulka, "Name", idColumnName, id);
    }

    public object[] SelectRowReaderLimit(string tableName, int limit, string sloupce, string sloupecWhere, object hodnotaWhere)
    {
        SqlCommand comm = new SqlCommand("SELECT TOP(" + limit.ToString() + ") " + sloupce + " FROM " + tableName + GeneratorMsSql.SimpleWhere(sloupecWhere));
        AddCommandParameter(comm, 0, hodnotaWhere);
        return SelectRowReader(comm);
    }

    /// <summary>
    /// Vrací -1 pokud se nepodaří najít if !A1 nebo long.MaxValue když A1
    /// </summary>
    /// <param name = "signed"></param>
    /// <param name = "table"></param>
    /// <param name = "vracenySloupec"></param>
    /// <param name = "abc"></param>
    /// <returns></returns>
    public long SelectCellDataTableLongOneRow(bool signed, string table, string vracenySloupec, params AB[] abc)
    {
        string sql = GeneratorMsSql.SimpleSelectOneRow(vracenySloupec, table) + GeneratorMsSql.CombinedWhere(abc);
        SqlCommand comm = new SqlCommand(sql);
        AddCommandParameterFromAbc(comm, abc);
        return ExecuteScalarLong(signed, comm);
    }

    /// <summary>
    /// Vrací -1 pokud se nepodaří najít if !A1 nebo long.MaxValue když A1.
    /// </summary>
    /// <param name = "table"></param>
    /// <param name = "idColumnName"></param>
    /// <param name = "idColumnValue"></param>
    /// <param name = "vracenySloupec"></param>
    /// <returns></returns>
    public long SelectCellDataTableLongOneRow(bool signed, string table, string idColumnName, object idColumnValue, string vracenySloupec)
    {
        string sql = GeneratorMsSql.SimpleWhereOneRow(vracenySloupec, table, idColumnName);
        SqlCommand comm = new SqlCommand(sql);
        AddCommandParameter(comm, 0, idColumnValue);
        return ExecuteScalarLong(true, comm);
    }

    /// <summary>
    /// Vrátí null pokud se řádek nepodaří najít
    /// A3 nebo A4 může být null
    /// </summary>
    /// <param name = "table"></param>
    /// <param name = "vracenySloupec"></param>
    /// <param name = "where"></param>
    /// <param name = "whereIsNot"></param>
    /// <returns></returns>
    public bool? SelectCellDataTableNullableBoolOneRow(string table, string vracenySloupec, AB[] where, AB[] whereIsNot)
    {
        int dd = 0;
        string sql = GeneratorMsSql.SimpleSelectOneRow(vracenySloupec, table) + GeneratorMsSql.CombinedWhere(where, ref dd) + GeneratorMsSql.CombinedWhereNotEquals(true, ref dd, whereIsNot);
        SqlCommand comm = new SqlCommand(sql);
        AddCommandParameteresArrays(comm, 0, where, whereIsNot);
        return ExecuteScalarNullableBool(comm);
    }

    public bool? SelectCellDataTableNullableBoolOneRow(string table, string vracenySloupec, params AB[] abc)
    {
        string sql = "SELECT TOP(1) " + vracenySloupec + " FROM " + table + AllStrings.space + GeneratorMsSql.CombinedWhere(abc);
        SqlCommand comm = new SqlCommand(sql);
        AddCommandParameteres(comm, 0, abc);
        return ExecuteScalarNullableBool(comm);
    }

    /// <summary>
    /// Vrátí -1 když řádek nebude nalezen a !A1.
    /// Vrátí float.MaxValue když řádek nebude nalezen a A1.
    /// </summary>
    /// <param name = "signed"></param>
    /// <param name = "table"></param>
    /// <param name = "idColumnName"></param>
    /// <param name = "idColumnValue"></param>
    /// <param name = "vracenySloupec"></param>
    /// <returns></returns>
    public float SelectCellDataTableFloatOneRow(bool signed, string table, string idColumnName, object idColumnValue, string vracenySloupec)
    {
        string sql = GeneratorMsSql.SimpleWhereOneRow(vracenySloupec, table, idColumnName);
        SqlCommand comm = new SqlCommand(sql);
        AddCommandParameter(comm, 0, idColumnValue);
        return ExecuteScalarFloat(signed, comm);
    }

    /// <summary>
    /// Vrátí -1 když řádek nebude nalezen a !A1.
    /// Vrátí float.MaxValue když řádek nebude nalezen a A1.
    /// </summary>
    /// <param name = "signed"></param>
    /// <param name = "table"></param>
    /// <param name = "idColumnName"></param>
    /// <param name = "idColumnValue"></param>
    /// <param name = "vracenySloupec"></param>
    /// <returns></returns>
    public float SelectCellDataTableFloatOneRow(bool signed, string table, string vracenySloupec, params AB[] ab)
    {
        string sql = GeneratorMsSql.CombinedWhere(table, true, vracenySloupec, ab);
        SqlCommand comm = new SqlCommand(sql);
        AddCommandParameterFromAbc(comm, ab);
        return ExecuteScalarFloat(signed, comm);
    }

    public string SelectCellDataTableStringOneLastRow(string table, string vracenySloupec, string orderByDesc, string idColumnName, object idColumnValue)
    {
        //SELECT TOP 1 * FROM table_Name ORDER BY unique_column DESC
        string sql = GeneratorMsSql.SimpleWhereOneRow(vracenySloupec, table, idColumnName);
        SqlCommand comm = new SqlCommand(sql + string.Format(" ORDER BY {0} DESC", orderByDesc));
        AddCommandParameter(comm, 0, idColumnValue);
        return ExecuteScalarString(comm);
    }

    /// <summary>
    /// Nepoužívat a smazat!!!
    /// Tato metoda se přesně hodí když chci získat nějaký nejoblíbenější obsah - srovnává podle hodnoty v A3.
    /// Tato metoda vrací celé řádky z tabulky, Je zde stejně pojmenovaná metoda s A4, kde můžeš specifikovat sloupce které chceš vrátit.
    /// </summary>
    /// <param name = "tableName"></param>
    /// <param name = "limit"></param>
    /// <param name = "sloupecID"></param>
    /// <param name = "abc"></param>
    /// <returns></returns>
    public DataTable SelectDataTableLimitLastRows(string tableName, int limit, string sloupecID, params AB[] abc)
    {
        return SelectDataTableLimitLastRows(tableName, limit, AllStrings.asterisk, sloupecID, abc);
    }

    /// <summary>
    /// Nepoužívat a smazat!!!
    /// Jakékoliv změny zde musíš provést i v metodě SelectValuesOfColumnAllRowsString
    /// </summary>
    /// <param name = "tabulka"></param>
    /// <param name = "sloupec"></param>
    /// <returns></returns>
    public List<string> SelectValuesOfColumnAllRowsStringTrim(string tabulka, string sloupec)
    {
        //List<string> vr = new List<string>();
        SqlCommand comm = new SqlCommand(string.Format("SELECT {0} FROM {1}", sloupec, tabulka));
        return ReadValuesStringTrim(comm);
    }

    public List<byte> SelectValuesOfColumnAllRowsByte(string tabulka, string sloupec, params AB[] ab)
    {
        //List<byte> vr = new List<byte>();
        SqlCommand comm = new SqlCommand(string.Format("SELECT {0} FROM {1}", sloupec, tabulka + GeneratorMsSql.CombinedWhere(ab)));
        AddCommandParameteres(comm, 0, ab);
        return ReadValuesByte(comm);
    }

    public List<byte> SelectValuesOfColumnAllRowsByte(string tabulka, int limit, string sloupec, params AB[] ab)
    {
        //List<byte> vr = new List<byte>();
        SqlCommand comm = new SqlCommand(string.Format("SELECT TOP(" + limit + ") {0} FROM {1}", sloupec, tabulka + GeneratorMsSql.CombinedWhere(ab)));
        AddCommandParameteres(comm, 0, ab);
        return ReadValuesByte(comm);
    }

    /// <summary>
    /// Nepoužívat a smazat!!!
    /// </summary>
    public DataTable SelectDataTableLimit(string tableName, int limit)
    {
        SqlCommand comm = new SqlCommand("SELECT TOP(" + limit.ToString() + ") * FROM " + tableName);
        //AddCommandParameter(comm, 0, hodnotaWhere);
        return SelectDataTable(comm);
    }
}
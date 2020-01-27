using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using sunamo;
using sunamo.Values;

/// <summary>
/// 12-1-2019 refactoring:
/// 1)all methods here take ABC if take more than Where. otherwise is allowed params AB[] / object[]
/// 2) always is table - select / update column - where
/// 
/// </summary>
public partial class MSStoredProceduresIBase : SqlServerHelper
{
    public static PpkOnDrive loggedCommands = null;

    static MSStoredProceduresIBase()
    {
        //var f = AppData.ci.GetFile(AppFolders.Logs, "sqlCommands.txt");
        //loggedCommands = new PpkOnDrive(f);
    }

    /// <summary>
    /// A1 NSN
    /// </summary>
    /// <param name="signed"></param>
    /// <param name="tabulka"></param>
    /// <param name="hledanySloupec"></param>
    /// <param name="aB"></param>
    /// <returns></returns>
    public List<int> SelectValuesOfColumnAllRowsInt(bool signed, string tabulka, string hledanySloupec, params AB[] aB)
    {
        string hodnoty = MSDatabaseLayer.GetValues(aB.ToArray());
        SqlCommand comm = new SqlCommand(string.Format("SELECT {0} FROM {1} {2}", hledanySloupec, tabulka, GeneratorMsSql.CombinedWhere(aB)));
        for (int i = 0; i < aB.Length; i++)
        {
            AddCommandParameter(comm, i, aB[i].B);
        }
        return ReadValuesInt(comm);
    }

    private static void AddCommandParameter(SqlCommand comm, int i, object b)
    {
        SqlOperations.AddCommandParameter(comm, i, b);
    }

    public static string table2 = null;
    public static string column2 = null;
    public static bool isNVarChar2 = false;


    

    public static Func<string, string, bool> IsNVarChar = null;

    public DataTable DeleteAllSmallerThanWithOutput(string TableName, string sloupceJezVratit, string nameColumnSmallerThan, object valueColumnSmallerThan, ABC whereIs, ABC whereIsNot)
    {
        return SqlOpsI.ci. DeleteAllSmallerThanWithOutput(SqlData.Empty, TableName, sloupceJezVratit, nameColumnSmallerThan, valueColumnSmallerThan, whereIs, whereIsNot).result;
    }

    public DataTable DeleteWithOutput(string TableName, string sloupceJezVratit, string idColumn, object idValue)
    {
        return SqlOpsI.ci.DeleteWithOutput(SqlData.Empty, TableName, sloupceJezVratit, idColumn, idValue).result;
    }

    public Dictionary<T, string> SelectIDNames<T>(Func<string, T> parse, string solutions, params AB[] where)
    {
        return SqlOpsI.ci.SelectIDNames<T>(SqlData.Empty, parse, solutions, where);
    }

    public bool HasAnyValue(string table, string columnName, string iDColumnName, int idColumnValue)
    {
        return SqlOpsI.ci.HasAnyValue(SqlData.Empty, table, columnName, iDColumnName, idColumnValue).result;
    }

    public class Parse
    {
        public class DateTime
        {
            /// <summary>
            /// Vrátí -1 v případě že se nepodaří vyparsovat
            /// </summary>
            /// <param name="p"></param>
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

    public int SelectID( string tabulka, string nazevSloupce, object hodnotaSloupce)
    {
        return SelectID(false, tabulka, nazevSloupce, hodnotaSloupce);
    }


    public MSStoredProceduresIBase()
    {

    }

    /// <summary>
    /// Počítá od nuly
    /// </summary>
    /// <param name="comm"></param>
    /// <param name="where"></param>
    private static void AddCommandParameterFromAbc(SqlCommand comm, params AB[] where)
    {
        SqlOperations.AddCommandParameterFromAbc(comm, where);
    }

    public bool SelectExistsDatabase(string p)
    {
        return SqlOpsI.ci.SelectExistsDatabase(SqlData.Empty, p).result;
    }

    SqlData d = SqlData.Empty;

    public void CreateDatabase(string p)
    {
        return SqlOpsI.ci.CreateDatabase(d, p);
    }

    /// <summary>
    /// Počítá od nuly
    /// Mohu volat i s A2 null, v takovém případě se nevykoná žádný kód
    /// </summary>
    /// <param name="comm"></param>
    /// <param name="where"></param>
    private static int AddCommandParameterFromAbc(SqlCommand comm, ABC where, int i)
    {
        return SqlOperations.AddCommandParameterFromAbc(comm, where, i);
    }

    #region Commented
    ///// <summary>
    ///// Libovolné z hodnot A2 až A5 může být null, protože se to postupuje metodě AddCommandParameteresArrays. Use CA.TwoDimensionParamsIntoOne instead
    ///// </summary>
    ///// <param name="comm"></param>
    ///// <param name="where"></param>
    ///// <param name="isNotWhere"></param>
    ///// <param name="greaterThanWhere"></param>
    ///// <param name="lowerThanWhere"></param>
    //public static void AddCommandParameteresCombinedArrays(SqlCommand comm, int i, ABC where, ABC isNotWhere, ABC greaterThanWhere, ABC lowerThanWhere)
    //{
    //    var arr = CA.TwoDimensionParamsIntoOne<AB>(where, isNotWhere, greaterThanWhere, lowerThanWhere);
    //    AddCommandParameteres(comm, i, ABC.OnlyBs(arr));
    //}

    ///// <summary>
    ///// Bude se počítat od nuly
    ///// Některé z vnitřních polí může být null
    ///// </summary>
    ///// <param name="comm"></param>
    ///// <param name="where"></param>
    ///// <param name="whereIsNot"></param>
    //private static void AddCommandParameteresArrays(SqlCommand comm, int i, params AB[][] where)
    //{
    //    //int i = 0;
    //    foreach (var item in where)
    //    {
    //        if (item != null)
    //        {
    //            foreach (var item2 in item)
    //            {
    //                i = AddCommandParameter(comm, i, item2.B);
    //            }
    //        }
    //    }
    //} 
    #endregion

    public static void AddCommandParameteresCombinedArrays(SqlCommand comm, int i2, ABC where, ABC isNotWhere, ABC greaterThanWhere, ABC lowerThanWhere)
    {
        int l = CA.GetLength(where);
        l += CA.GetLength(isNotWhere);
        l += CA.GetLength(greaterThanWhere);
        l += CA.GetLength(lowerThanWhere);
        ABC ab = new ABC(l);
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
        AddCommandParameterFromAbc(comm, ab, i2);
    }

    private static void AddCommandParameteresArrays(SqlCommand comm, int i, params ABC[] where)
    {
        AddCommandParameteresCombinedArrays(comm, i, CA.IndexOrNull(where, 0), CA.IndexOrNull(where, 1), CA.IndexOrNull(where, 2), CA.IndexOrNull(where, 3));
    }

    

    public static int AddCommandParameteres(SqlCommand comm, int pocIndex, params AB[] hodnotyOdNuly)
    {
        return SqlOperations.AddCommandParameter(comm, pocIndex, hodnotyOdNuly);
    }

    public static int AddCommandParameteres(SqlCommand comm, int pocIndex, ABC aWhere)
    {
        return SqlOperations.AddCommandParameter(comm, pocIndex, aWhere);
    }



    public List<bool> DataTableToListBool(DataTable dataTable, int dex)
    {
        return SqlOpsI.ci.DataTableToListBool(dataTable, dex);
    }

    public List<short> DataTableToListShort(DataTable dataTable, int p)
    {
        return SqlOpsI.ci.DataTableToListShort(dataTable, p);
    }

    public List<int> DataTableToListInt(DataTable dataTable, int p)
    {
        return SqlOpsI.ci.DataTableToListInt(dataTable, p);
    }



    public List<string> DataTableToListString(DataTable dataTable, int dex)
    {
        return SqlOpsI.ci.DataTableToListString(dataTable, dex);
    }

    /// <summary>
    /// Maže všechny řádky, ne jen jeden.
    /// </summary>
    public int Delete(string table, string sloupec, object id)
    {
        return SqlOpsI.ci.Delete(d, table, sloupec, id).affectedRows;
    }

    /// <summary>
    /// Vrací A2
    /// A2 je ID řádku na který se bude vkládat. Název/hodnota/whatever tohoto sloupce musí být 1. v A3.
    /// Používej tehdy když chceš určit index na který vkládat.
    /// </summary>
    /// <param name="tabulka"></param>
    /// <param name="IDUsers"></param>
    /// <param name="sloupce"></param>
    public void Insert3(string tabulka, long IDUsers, params object[] sloupce2)
    {
        SqlOpsI.ci.Insert3(d, tabulka, IDUsers, sloupce2);
    }

    /// <summary>
    /// Conn nastaví automaticky
    /// Vrátí zda byl vymazán alespoň jeden řádek
    /// 
    /// </summary>
    /// <param name="TableName"></param>
    /// <param name="where"></param>
    /// <returns></returns>
    public int Delete(string TableName, params AB[] where)
    {
        return SqlOpsI.ci.Delete(TableName, where).affectedRows;
    }

    public int DeleteAllSmallerThan(string TableName, string nameColumnSmallerThan, object valueColumnSmallerThan, params AB[] where)
    {
        return SqlOpsI.ci.DeleteAllSmallerThan(d, TableName, nameColumnSmallerThan, valueColumnSmallerThan, where).affectedRows;
    }

    public List<int> SelectAllInColumnLargerThanInt(string TableName, string columnReturn, string nameColumnLargerThan, object valueColumnLargerThan, params AB[] where)
    {
        return SqlOpsI.ci.SelectAllInColumnLargerThanInt(d, TableName, columnReturn, nameColumnLargerThan, valueColumnLargerThan, where).result;
    }

    public int DeleteAllLargerThan(string TableName, string nameColumnLargerThan, object valueColumnLargerThan, params AB[] where)
    {
        return SqlOpsI.ci.DeleteAllLargerThan(d, TableName, nameColumnLargerThan, valueColumnLargerThan, where).affectedRows;
    }

    public int DeleteOneRow(string table, string sloupec, object id)
    {
        return SqlOpsI.ci.DeleteOneRow(d, table, id).affectedRows;
    }

    public bool DeleteOneRow(string TableName, params AB[] where)
    {
        return SqlOpsI.ci.DeleteOneRow(d, TableName, where).result;
    }

    /// <summary>
    /// Pouýžívá se když chceš odstranit více řádků najednou pomocí AB. Nedá se použít pokud aspoň na jednom řádku potřebuješ AND
    /// </summary>
    /// <param name="p"></param>
    /// <param name="aB"></param>
    /// <returns></returns>
    public int DeleteOR(string TableName, params AB[] where)
    {
        return SqlOpsI.ci.DeleteOR(d, TableName, where);
    }

    public void DropAllTables()
    {
        return SqlOpsI.ci.DropAllTables(d);
    }

    public void DropAndCreateTable(string p, Dictionary<string, MSColumnsDB> dictionary)
    {
        SqlOpsI.ci.DropAndCreateTable(d, p, dictionary);
    }

    public void DropAndCreateTable(string p, Dictionary<string, MSColumnsDB> dictionary, SqlConnection conn)
    {
        if (dictionary.ContainsKey(p))
        {
            DropTableIfExists(p);
            dictionary[p].GetSqlCreateTable(p, true, conn).ExecuteNonQuery();
        }
    }

    public string _cs = null;

    string Cs
    {
        get
        {
            if (_cs != null)
            {
                return _cs;
            }

            return MSDatabaseLayer.cs;
        }
    }



    public void DropAndCreateTable(string p, MSColumnsDB msc)
    {
        SqlOpsI.ci.DropAndCreateTable(d, p, msc);
    }

    public void DropAndCreateTable2(string p, Dictionary<string, MSColumnsDB> dictionary)
    {
        return SqlOpsI.ci.DropAndCreateTable2(d, p, dictionary);
    }
    public int DropTableIfExists(string table)
    {
        return SqlOpsI.ci.DropTableIfExists(d, table).affectedRows;
    }

    /// <summary>
    /// Conn nastaví automaticky
    /// </summary>
    /// <param name="sql"></param>
    /// <returns></returns>
    public DataTable SelectDataTable(SqlCommand comm)
    {
        return SqlOpsI.ci.SelectDataTable(d, comm);
    }

    /// <summary>
    /// A1 jsou hodnoty bez převedení AddCommandParameter nebo ReplaceValueOnlyOne
    /// Conn nastaví automaticky
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="_params"></param>
    /// <returns></returns>
    private DataTable SelectDataTable(string sql, params object[] _params)
    {
        return SqlOpsI.ci.SelectDataTable(d, sql, _params);
    }


    /// <summary>
    /// Return count of rows affected
    /// </summary>
    /// <param name="comm"></param>
    /// <returns></returns>
    public int ExecuteNonQuery(SqlCommand comm)
    {
        return SqlOpsI.ci.ExecuteNonQuery(d, comm);
    }

    private void PrintDebugParameters(SqlCommand comm)
    {
        SqlOpsI.ci.PrintDebugParameters(comm);
    }

    public int ExecuteNonQuery(string commText, params object[] para)
    {
        return SqlOpsI.ci.ExecuteNonQuery(d, commText, para);
    }

    /// <summary>
    /// MUST CALL conn.Close(); AFTER GET DATA
    /// </summary>
    /// <param name="comm"></param>
    /// <returns></returns>
    private SqlDataReader ExecuteReader(SqlCommand comm)
    {
        return SqlOpsI.ci.ExecuteReader(d, comm);

    }

    /// <summary>
    /// Automaticky doplní connection
    /// </summary>
    /// <param name="comm"></param>
    /// <returns></returns>
    public object ExecuteScalar(SqlCommand comm)
    {
        return SqlOpsI.ci.ExecuteScalar(d, comm);
    }

    public object ExecuteScalar(string commText, params object[] para)
    {
        return SqlOpsI.ci.ExecuteScalar(d, commText, para);
    }

    private bool ExecuteScalarBool(SqlCommand comm)
    {
        return SqlOpsI.ci.ExecuteScalarBool(d, comm);
    }

    private byte ExecuteScalarByte(SqlCommand comm)
    {
        return SqlOpsI.ci.ExecuteScalarByte(d, comm);
    }

    private DateTime ExecuteScalarDateTime(DateTime getIfNotFound, SqlCommand comm)
    {
        return SqlOpsI.ci.ExecuteScalarDateTime(d, getIfNotFound, comm);
    }

    private float ExecuteScalarFloat(bool signed, SqlCommand comm)
    {

        return SqlOpsI.ci.ExecuteScalarFloat(Signed(signed), comm);
    }

    private SqlData Signed(bool signed)
    {
        return new SqlData { signed = signed };
    }

    private int ExecuteScalarInt(bool signed, SqlCommand comm)
    {
        return SqlOpsI.ci.ExecuteScalarInt(Signed(signed), comm);
    }

    private long ExecuteScalarLong(bool signed, SqlCommand comm)
    {
        return SqlOpsI.ci.ExecuteScalarLong(Signed(signed), comm);
    }

    private bool? ExecuteScalarNullableBool(SqlCommand comm)
    {
        return SqlOpsI.ci.ExecuteScalarNullableBool(d, comm);
    }

    private short ExecuteScalarShort(bool signed, SqlCommand comm)
    {
        return SqlOpsI.ci.ExecuteScalarShort(Signed(signed), comm);
    }

    private string ExecuteScalarString(SqlCommand comm)
    {
        return SqlOpsI.ci.ExecuteScalarString(d, comm);
    }

    /// <summary>
    /// For getting ID use SelectLastIDFromTableSigned (without 2 postfix)
    /// Used in TableRow* 
    /// Do této metody se vkládají hodnoty bez ID
    /// Vrátí mi nejmenší volné číslo tabulky A1
    /// Pokud bude obsazene 1,3, vrátí až 4
    /// ID se počítá jako v Sqlite - tedy od 1 
    /// A2 je zde proto aby se mohlo určit poslední index a ten inkrementovat a na ten vložit. Název/hodnota/whatever tohoto sloupce musí být 1. v A3.
    /// Používej tehdy když ID sloupec má nějaký speciální název, např. IDUsers
    /// </summary>
    /// <param name="tabulka"></param>
    /// <param name="sloupce"></param>
    /// <returns></returns>
    public long Insert(string tabulka, Type idt, string sloupecID, params object[] sloupce)
    {
        return SqlOpsI.ci.Insert(Signed(false), tabulka, idt, sloupecID, sloupce);
    }

    public long InsertSigned(string tabulka, Type idt, string sloupecID, params object[] sloupce)
    {
        return SqlOpsI.ci.InsertSigned(d, tabulka, idt, sloupecID, sloupce);
    }

    private long Insert1(string tabulka, Type idt, string sloupecID, object[] sloupce, bool signed)
    {
        return SqlOpsI.ci.Insert1(Signed(signed), tabulka, idt, sloupecID, sloupce);
    }

    /// <summary>
    /// For getting ID use SelectLastIDFromTableSigned2 (with 2 postfix)
    /// Tato metoda je vyjímečná, vkládá hodnoty signed, hodnotu kterou vložit si zjistí sám a vrátí ji.
    /// </summary>
    /// <param name="tabulka"></param>
    /// <param name="sloupecID"></param>
    /// <param name="nazvySloupcu"></param>
    /// <param name="sloupce"></param>
    /// <returns></returns>
    public long Insert2(string tabulka, string sloupecID, Type typSloupecID, params object[] sloupce)
    {
        return SqlOpsI.ci.Insert2(d, tabulka, sloupecID, typSloupecID, sloupce);
    }


    /// <summary>
    /// In scz use nowhere 
    /// A2 může být ID nebo cokoliv začínající na ID(ID*)
    /// A2 je ID řádku na který se bude vkládat. Název/hodnota/whatever A2 už nesmí být v A3
    /// Používej tehdy když chceš určit index na který vkládat.
    /// </summary>
    /// <param name="tabulka"></param>
    /// <param name="IDUsers"></param>
    /// <param name="sloupce"></param>
    public void Insert3(string tabulka, long IDUsers, params object[] sloupce)
    {
        return SqlOpsI.ci.Insert3(d, tabulka, IDUsers, sloupce);
    }

    /// <summary>
    /// Stjená jako 3, jen ID* je v A2 se všemi
    /// </summary>
    /// <param name="tabulka"></param>
    /// <param name="sloupce"></param>
    public void Insert4(string tabulka, params object[] sloupce)
    {
        return SqlOpsI.ci.Insert4(d, tabulka, sloupce);
    }

    /// <summary>
    /// In scz used nowhere
    /// </summary>
    /// <param name="table"></param>
    /// <param name="nazvySloupcu"></param>
    /// <param name="sloupce"></param>
    /// <returns></returns>
    public long Insert5(string table, string nazvySloupcu, params object[] sloupce)
    {
        return SqlOpsI.ci.Insert5(d, table, nazvySloupcu, sloupce);
    }

    /// <summary>
    /// In scz used nowhere
    /// Jediná metoda kde můžeš specifikovat sloupce do kterých chceš vložit
    /// Sloupec který nevkládáš musí být auto_increment
    /// ÏD si pak musíš zjistit sám pomocí nějaké identifikátoru - například sloupce Uri
    /// </summary>
    /// <param name="table"></param>
    /// <param name="nazvySloupcu"></param>
    /// <param name="sloupce"></param>
    /// <returns></returns>
    public void Insert6(string table, string nazvySloupcu, params object[] sloupce)
    {
        return SqlOpsI.ci.Insert6(d, table, nazvySloupcu, sloupce);
    }

    /// <summary>
    /// For inserting to table id-name
    /// </summary>
    /// <param name="tabulka"></param>
    /// <param name="nazev"></param>
    /// <returns></returns>
    public int InsertRowTypeEnum(string tabulka, string nazev)
    {
        return SqlOpsI.ci.InsertRowTypeEnum(d, tabulka, nazev);
    }

    /// <summary>
    /// Raději používej metodu s 3/2A sloupecID, pokud používáš v tabulce sloupce ID, které se nejmenují ID
    /// Sloupec u kterého se bude určovat poslední index a ten inkrementovat a na ten vkládat je ID
    /// Používej tehdy když ID sloupec má nějaký standardní název, Tedy ID, ne IDUsers atd.
    /// </summary>
    /// <param name="tabulka"></param>
    /// <param name="sloupce"></param>
    /// <returns></returns>
    public Guid InsertToRowGuid(string tabulka, params object[] sloupce)
    {
        return SqlOpsI.ci.InsertToRowGuid(d, tabulka, sloupce);
    }

    /// <summary>
    /// Do této metody se vkládají hodnoty bez ID
    /// ID se počítá jako v Sqlite - tedy od 1 
    /// A2 je zde proto aby se mohlo určit poslední index a ten inkrementovat a na ten vložit. Název/hodnota/whatever tohoto sloupce musí být 1. v A3.
    /// Používej tehdy když ID sloupec má nějaký speciální název, např. IDUsers
    /// </summary>
    /// <param name="tabulka"></param>
    /// <param name="sloupce"></param>
    /// <returns></returns>
    public Guid InsertToRowGuid2(string tabulka, string sloupecID, params object[] sloupce)
    {
        return SqlOpsI.ci.InsertToRowGuid2(d, tabulka, sloupecID, sloupce);
    }



    /// <summary>
    /// A2 je ID řádku na který se bude vkládat. Název/hodnota/whatever tohoto sloupce musí být 1. v A3.
    /// Používej tehdy když chceš určit index na který vkládat.
    /// </summary>
    /// <param name="tabulka"></param>
    /// <param name="IDUsers"></param>
    /// <param name="sloupce"></param>
    public void InsertToRowGuid3(string tabulka, Guid IDUsers, params object[] sloupce)
    {
        return SqlOpsI.ci.InsertToRowGuid3(d, tabulka, IDUsers, sloupce);
    }

    public List<string> SelectValuesOfColumnAllRowsString(string tabulka, string sloupec, params AB[] aB)
    {
        return SqlOpsI.ci.SelectValuesOfColumnAllRowsString(d, tabulka, sloupec, aB);
    }
    public List<string> SelectValuesOfColumnAllRowsString(string tabulka, string sloupec, string whereSloupec, object whereHodnota, string orderBy = "")
    {
        return SqlOpsI.ci.SelectValuesOfColumnAllRowsString(d, tabulka, sloupec, whereSloupec, whereHodnota, orderBy);
    }

    public List<string> SelectValuesOfColumnAllRowsString(string tabulka, string sloupec, ABC where, ABC whereIsNot, string orderBy = "")
    {
        return SqlOpsI.ci.SelectValuesOfColumnAllRowsString(d, tabulka, sloupec, where, whereIsNot, orderBy);
    }

    /// <summary>
    /// POkud bude v DB hodnota DBNull.Value, vrátí se -1
    /// </summary>
    /// <param name="tabulka"></param>
    /// <param name="sloupec"></param>
    /// <returns></returns>
    public List<int> SelectValuesOfColumnAllRowsInt(string tabulka, string sloupec)
    {
        return SqlOpsI.ci.SelectValuesOfColumnAllRowsInt(d, tabulka, sloupec);
    }

    public IList SelectValuesOfColumnAllRowsNumeric(string tabulka, string sloupec, params AB[] ab)
    {
        return SqlOpsI.ci.SelectValuesOfColumnAllRowsNumeric(d, tabulka, sloupec, ab);
    }

    public IList SelectValuesOfColumnAllRowsNumeric(string tabulka, string sloupec)
    {
        return SqlOpsI.ci.SelectValuesOfColumnAllRowsNumeric(d, tabulka, sloupec);
    }



    /// <summary>
    /// POkud bude v DB hodnota DBNull.Value, vrátí se -1
    /// </summary>
    /// <param name="tabulka"></param>
    /// <param name="sloupec"></param>
    /// <returns></returns>
    public List<short> SelectValuesOfColumnAllRowsShort(string tabulka, string sloupec)
    {
        return SqlOpsI.ci.SelectValuesOfColumnAllRowsShort(d, tabulka, sloupec);
    }

    public List<short> SelectValuesOfColumnAllRowsShort(string tabulka, string sloupec, string idColumn, object idValue)
    {
        return SqlOpsI.ci.SelectValuesOfColumnAllRowsShort(d, tabulka, sloupec, idColumn, idValue);
    }


    /// <summary>
    /// Not return space on left or right
    /// </summary>
    /// <param name="d"></param>
    /// <returns></returns>
    private string Distinct(SqlData d)
    {
        return SqlOpsI.ci.Distinct(d);
    }

    public List<short> SelectValuesOfColumnAllRowsShort(string tabulka, int limit, string sloupec, string idColumn, object idValue)
    {
        return SqlOpsI.ci.SelectValuesOfColumnAllRowsShort(d, tabulka, limit, sloupec, idColumn, idValue);
    }


    public List<int> SelectValuesOfColumnAllRowsInt(string tabulka, int limit, string sloupec, string idColumn, object idValue)
    {
        return SqlOpsI.ci.SelectValuesOfColumnAllRowsInt(d, tabulka, limit, sloupec, idColumn, idValue);
    }

    public int RandomValueFromColumnInt(string table, string column)
    {
        return SqlOpsI.ci.RandomValueFromColumnInt(d, table, column);
    }

    public short RandomValueFromColumnShort(string table, string column)
    {
        return SqlOpsI.ci.RandomValueFromColumnShort(d, table, column);
    }

    public List<short> SelectValuesOfColumnAllRowsShort(string tabulka, string sloupec, ABC whereIs, ABC whereIsNot)
    {
        return SqlOpsI.ci.SelectValuesOfColumnAllRowsShort(d, tabulka, sloupec, whereIs, whereIsNot);
    }
    public List<int> SelectValuesOfColumnAllRowsInt(string tabulka, string sloupec, ABC whereIs, ABC whereIsNot)
    {
        return SqlOpsI.ci.SelectValuesOfColumnAllRowsInt(d, tabulka, sloupec, whereIs, whereIsNot);
    }

    public List<int> SelectValuesOfColumnAllRowsInt(string tabulka, string sloupec, int dnuPozpatku, ABC whereIs, ABC whereIsNot)
    {
        return SqlOpsI.ci.SelectValuesOfColumnAllRowsIntDate(d, tabulka, sloupec, dnuPozpatku, whereIs, whereIsNot);
    }



    public List<int> SelectValuesOfColumnAllRowsInt(string tabulka, string sloupec, ABC whereIs, ABC whereIsNot, ABC greaterThanWhere, ABC lowerThanWhere)
    {
        return SqlOpsI.ci.SelectValuesOfColumnAllRowsInt(d, tabulka, sloupec, whereIs, whereIsNot, greaterThanWhere, lowerThanWhere);
    }
    /// <summary>
    /// POkud bude v DB hodnota DBNull.Value, vrátí se -1
    /// </summary>
    /// <param name="tabulka"></param>
    /// <param name="sloupec"></param>
    /// <returns></returns>
    public List<short> SelectValuesOfColumnAllRowsShort(string tabulka, string sloupec, params AB[] ab)
    {
        return SqlOpsI.ci.SelectValuesOfColumnAllRowsShort(d, tabulka, sloupec, ab);
    }

    public List<string> SelectValuesOfColumnAllRowsString(string tabulka, string sloupec)
    {
        return SqlOpsI.ci.SelectValuesOfColumnAllRowsString(d, tabulka, sloupec);
    }

    public List<string> SelectValuesOfColumnAllRowsStringTrim(string tabulka, string sloupec, string idn, object idv)
    {
        return SqlOpsI.ci.SelectValuesOfColumnAllRowsString(d, tabulka, sloupec, idn, idv);
    }

    /// <summary>
    /// Tato metoda má navíc možnost specifikovat simple where.
    /// </summary>
    /// <param name="tabulka"></param>
    /// <param name="hledanySloupec"></param>
    /// <param name="idColumn"></param>
    /// <param name="idValue"></param>
    /// <returns></returns>
    public List<int> SelectValuesOfColumnAllRowsInt(bool signed, string tabulka, string hledanySloupec, string idColumn, object idValue)
    {
        return SqlOpsI.ci.SelectValuesOfColumnAllRowsInt(Signed(signed), tabulka, hledanySloupec, idColumn, idValue);
    }

    public List<long> SelectValuesOfColumnAllRowsLong(bool signed, string tabulka, string hledanySloupec, params AB[] aB)
    {
        return SqlOpsI.ci.SelectValuesOfColumnAllRowsLong(Signed(signed), tabulka, hledanySloupec, aB);
    }


    public List<int> SelectValuesOfColumnAllRowsInt(bool signed, string tabulka, int maxRows, string hledanySloupec, params AB[] aB)
    {
        return SqlOpsI.ci.SelectValuesOfColumnAllRowsInt(Signed(signed), tabulka, maxRows, hledanySloupec, aB);
    }

    /// <summary>
    /// Pokud bude buňka DBNull, nebudu ukládat do G nic
    /// </summary>
    /// <param name="table"></param>
    /// <param name="returnColumns"></param>
    /// <param name="where"></param>
    /// <returns></returns>
    public List<DateTime> SelectValuesOfColumnAllRowsDateTime(string table, string returnColumns, params AB[] where)
    {
        return SqlOpsI.ci.SelectValuesOfColumnAllRowsDateTime(d, table, returnColumns, where);
    }

    /// <summary>
    /// Pokud řádek ve sloupci A2 má hodnotu DBNull.Value, zapíšu do výsledku 0
    /// </summary>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    /// <param name="aB1"></param>
    /// <param name="aB2"></param>
    /// <returns></returns>
    public List<byte> SelectValuesOfColumnAllRowsByte(string tabulka, string hledanySloupec, object vetsiNez, object mensiNez, params AB[] aB)
    {
        return SqlOpsI.ci.SelectValuesOfColumnAllRowsByte(d, tabulka, hledanySloupec, vetsiNez, mensiNez, aB);
    }

    /// <summary>
    /// Používej místo této M metodu SelectValuesOfColumnAllRowsInt která je úplně stejná
    /// </summary>
    /// <param name="tabulka"></param>
    /// <param name="sloupecHledaný"></param>
    /// <param name="abc"></param>
    /// <returns></returns>
    public List<int> SelectValuesOfColumnInt(bool signed, string tabulka, string sloupecHledaný, params AB[] abc)
    {
        return SqlOpsI.ci.SelectValuesOfColumnInt(Signed(signed), tabulka, sloupecHledaný, abc);
    }

    public List<byte> SelectValuesOfColumnByte(string tabulka, string sloupecHledaný, string sloupecVeKteremHledat, object hodnota)
    {
        return SqlOpsI.ci.SelectValuesOfColumnByte(d, tabulka, sloupecHledaný, sloupecVeKteremHledat, hodnota);
    }

    /// <summary>
    /// Používej místo této M metodu SelectValuesOfColumnAllRowsInt, která je úplně stejná
    /// </summary>
    /// <param name="tabulka"></param>
    /// <param name="sloupecHledaný"></param>
    /// <param name="sloupecVeKteremHledat"></param>
    /// <param name="hodnota"></param>
    /// <returns></returns>
    public List<int> SelectValuesOfColumnInt(bool signed, string tabulka, string sloupecHledaný, string sloupecVeKteremHledat, object hodnota)
    {
        return SqlOpsI.ci.SelectValuesOfColumnInt(Signed(signed), tabulka, sloupecHledaný, sloupecVeKteremHledat, hodnota);
    }

    private List<int> ReadValuesInt(SqlCommand comm)
    {
        return SqlOpsI.ci.ReadValuesInt(d, comm);
    }

    public List<string> ReadValuesStringTrim(SqlCommand comm)
    {
        return SqlOpsI.ci.ReadValuesStringTrim(d, comm);
    }

    private List<string> ReadValuesString(SqlCommand comm)
    {
        return SqlOpsI.ci.ReadValuesString(d, comm);
    }

    private List<byte> ReadValuesByte(SqlCommand comm)
    {
        return SqlOpsI.ci.ReadValuesByte(d, comm);
    }

    private List<DateTime> ReadValuesDateTime(SqlCommand comm)
    {
        return SqlOpsI.ci.ReadValuesDateTime(d, comm);
    }

    private List<long> ReadValuesLong(SqlCommand comm)
    {
        return SqlOpsI.ci.ReadValuesLong(d, comm);
    }

    private List<short> ReadValuesShort(SqlCommand comm)
    {
        return SqlOpsI.ci.ReadValuesShort(d, comm);
    }

    /// <summary>
    /// Vymže tabulku A1 a přejmenuje tabulku A1+"2" na A1
    /// </summary>
    /// <param name="table"></param>
    public void sp_rename(string table)
    {
        return SqlOpsI.ci.sp_rename(d, table);
    }

    /// <summary>
    /// 
    /// </summary>
    public List<string> SelectGetAllTablesInDB()
    {
        return SqlOpsI.ci.SelectGetAllTablesInDB(d);
    }



    /// <summary>
    /// 
    /// </summary>
    public List<string> SelectColumnsNamesOfTable(string p)
    {
        return SqlOpsI.ci.SelectColumnsNamesOfTable(d, p);
    }

    public bool SelectExistsTable(string p)
    {
        return SqlOpsI.ci.SelectExistsTable(d, p);
    }

    private DataTable SelectDataTable(SqlConnection conn, string sql, params object[] _params)
    {
        return SqlOpsI.ci.SelectDataTable(d, conn, sql, _params);
    }
    public DataTable SelectDataTable(SqlConnection conn, SqlCommand comm)
    {
        return SqlOpsI.ci.SelectDataTable(d, conn, comm);
    }
    public bool SelectExistsTable(string p, SqlConnection conn)
    {
        return SqlOpsI.ci.SelectExistsTable(d, p, conn);
    }

    /// <summary>
    /// Pokud chceš použít OrderBy, je tu metoda SelectDataTableLastRows nebo SelectDataTableLastRowsInnerJoin
    /// Conn nastaví automaticky
    /// Vrátí prázdnou tabulku pokud se nepodaří žádný řádek najít
    /// Vyplň A2 na SE pokud chceš všechny sloupce
    /// </summary>
    public DataTable SelectDataTableSelective(string tabulka, string nazvySloupcu, params AB[] ab)
    {
        return SqlOpsI.ci.SelectDataTableSelective(d, tabulka, nazvySloupcu, ab);
    }

    /// <summary>
    /// Conn nastaví automaticky
    /// Vrátí prázdnou tabulku pokud se nepodaří žádný řádek najít
    /// </summary>
    public DataTable SelectDataTableSelective(string tabulka, string nazvySloupcu, string sloupecID, object id)
    {
        return SqlOpsI.ci.SelectDataTableSelective(d, tabulka, nazvySloupcu, sloupecID, id);
    }

    public DataTable SelectDataTableSelective(string tabulka, string nazvySloupcu, string sloupecID, object id, string orderByColumn, SortOrder sortOrder)
    {
        return SqlOpsI.ci.SelectDataTableSelective(d, tabulka, nazvySloupcu, sloupecID, id, orderByColumn, sortOrder);
    }

    /// <summary>
    /// 
    /// </summary>
    public DataTable SelectDataTable(string tableName, int limit, string sloupecWhere, object hodnotaWhere)
    {
        return SqlOpsI.ci.SelectDataTable(d, tableName, limit, sloupecWhere, hodnotaWhere);
    }



    /// <summary>
    /// A2 je sloupec na který se prohledává pro A3
    /// </summary>
    /// <param name="tabulka"></param>
    /// <param name="sloupecID"></param>
    /// <param name="textPartOfCity"></param>
    /// <param name="nazvySloupcu"></param>
    /// <returns></returns>
    public DataTable SelectDataTableSelectiveLikeContains(string tabulka, string nazvySloupcu, string sloupecID, string textPartOfCity)
    {
        return SqlOpsI.ci.SelectDataTableSelectiveLikeContains(d, tabulka, nazvySloupcu, sloupecID, textPartOfCity);
    }

    public DataTable SelectAllRowsOfColumns(string table, string vratit, ABC abObsahuje, ABC abNeobsahuje, ABC abVetsiNez, ABC abMensiNez)
    {
        return SqlOpsI.ci.SelectAllRowsOfColumns(d, table, vratit, abObsahuje, abNeobsahuje, abVetsiNez, abMensiNez);
    }

    public DataTable SelectDataTableSelective(string table, string vraceneSloupce, ABC where, ABC whereIsNot)
    {
        return SqlOpsI.ci.SelectDataTableSelective(d, table, vraceneSloupce, where, whereIsNot);
    }

    public DataTable SelectDataTableSelective(string table, string vraceneSloupce, ABC where, ABC whereIsNot, ABC greaterThan, ABC lowerThan)
    {
        return SqlOpsI.ci.SelectDataTableSelective(d, table, vraceneSloupce, where, whereIsNot, greaterThan, lowerThan);
    }

    /// <summary>
    /// Řadí metodou DESC
    /// Tato metoda se přesně hodí když chci získat nějaký nejoblíbenější obsah - srovnává podle hodnoty v A4.
    /// </summary>
    /// <param name="tableName"></param>
    /// <param name="limit"></param>
    /// <param name="sloupecOrder"></param>
    /// <param name="abc"></param>
    /// <returns></returns>
    public DataTable SelectDataTableLastRows(string tableName, int limit, string columns, string sloupecOrder, params AB[] where)
    {
        return SqlOpsI.ci.SelectDataTableLastRows(d, tableName, limit, columns, sloupecOrder, where);
    }

    /// <summary>
    /// Řadí metodou DESC
    /// Tato metoda se přesně hodí když chci získat nějaký nejoblíbenější obsah - srovnává podle hodnoty v A4.
    /// </summary>
    /// <param name="tableName"></param>
    /// <param name="limit"></param>
    /// <param name="sloupecOrder"></param>
    /// <param name="abc"></param>
    /// <returns></returns>
    public DataTable SelectDataTableLastRows(string tableName, int limit, string columns, string sloupecOrder, ABC whereIs, ABC whereIsNot, ABC whereGreaterThan, ABC whereLowerThan)
    {
        return SqlOpsI.ci.SelectDataTableLastRows(d, tableName, limit, columns, sloupecOrder, whereIs, whereIsNot, whereGreaterThan, whereLowerThan);
    }

    /// <summary>
    /// 2
    /// </summary>
    public DataTable SelectAllRowsOfColumns(string p, string selectSloupce)
    {
        return SqlOpsI.ci.SelectAllRowsOfColumns(d, p, selectSloupce);
    }

    public DataTable SelectAllRowsOfColumns(string p, string ziskaneSloupce, string idColumnName, object idColumnValue)
    {
        return SqlOpsI.ci.SelectAllRowsOfColumns(d, p, ziskaneSloupce, idColumnName, idColumnValue);
    }
    public DataTable SelectAllRowsOfColumns(string p, string ziskaneSloupce, params AB[] ab)
    {
        return SqlOpsI.ci.SelectAllRowsOfColumns(d, p, ziskaneSloupce, ab);
    }
    /// <summary>
    /// Vrátí mi všechny položky ze sloupce 
    /// </summary>
    public DataTable SelectGreaterThan(string tableName, string tableColumn, object hodnotaOd)
    {
        return SqlOpsI.ci.SelectGreaterThan(d, tableName, tableColumn, hodnotaOd);
    }

    /// <summary>
    /// Tato metoda má přívlastek Columns protože v ní jde specifikovat sloupce které má metoda vrátit
    /// </summary>
    /// <param name="tableName"></param>
    /// <param name="limit"></param>
    /// <param name="sloupce"></param>
    /// <param name="sloupecWhere"></param>
    /// <param name="hodnotaWhere"></param>
    /// <returns></returns>
    public DataTable SelectDataTableColumns(string tableName, int limit, string sloupce, string sloupecWhere, object hodnotaWhere)
    {
        return SqlOpsI.ci.SelectDataTableColumns(d, tableName, limit, sloupce, sloupecWhere, hodnotaWhere);
    }




    public DataTable SelectTableInnerJoin(string tableFromWithShortVersion, string tableJoinWithShortVersion, string sloupceJezZiskavat, string onKlazuleOdNuly, params object[] fixniHodnotyOdNuly)
    {
        return SqlOpsI.ci.SelectTableInnerJoin(d, tableFromWithShortVersion, tableJoinWithShortVersion, sloupceJezZiskavat, onKlazuleOdNuly, fixniHodnotyOdNuly);
    }


    public DataTable SelectTableInnerJoin(string tableFromWithShortVersion, string tableJoinWithShortVersion, string sloupceJezZiskavat, string onKlazuleOdNuly, ABC whereIs, ABC whereIsNot)
    {
        return SqlOpsI.ci.SelectTableInnerJoin(d, tableFromWithShortVersion, tableJoinWithShortVersion, sloupceJezZiskavat, onKlazuleOdNuly, whereIs, whereIsNot);
    }

    /// <summary>
    /// Do A4 se zadává např. p.ID = stf.IDPhoto
    /// Tato metoda zde musí být, jinak vzniká chyba No mapping exists from object type AB to a known managed provider native type.
    /// </summary>
    /// <param name="tableFromWithShortVersion"></param>
    /// <param name="tableJoinWithShortVersion"></param>
    /// <param name="sloupceJezZiskavat"></param>
    /// <param name="onKlazuleOdNuly"></param>
    /// <param name="where"></param>
    /// <returns></returns>
    public DataTable SelectTableInnerJoin(string tableFromWithShortVersion, string tableJoinWithShortVersion, string sloupceJezZiskavat, string onKlazuleOdNuly, params AB[] where)
    {
        return SqlOpsI.ci.SelectTableInnerJoin(d, tableFromWithShortVersion, tableJoinWithShortVersion, sloupceJezZiskavat, onKlazuleOdNuly, where);
    }

    /// <summary>
    /// POkud nechceš používat reader, který furt nefugnuje, použij metodu SelectOneRowInnerJoin, má úplně stejnou hlavičku a jen funguje s DataTable
    /// Do A4 se zadává např. p.ID = stf.IDPhoto
    /// </summary>
    public object[] SelectOneRowInnerJoinReader(string tableFromWithShortVersion, string tableJoinWithShortVersion, string sloupceJezZiskavat, string onKlazuleOdNuly, params AB[] where)
    {
        return SqlOpsI.ci.SelectOneRowInnerJoinReader(d, tableFromWithShortVersion, tableJoinWithShortVersion, sloupceJezZiskavat, onKlazuleOdNuly, where);
    }

    public object[] SelectOneRowInnerJoin(string tableFromWithShortVersion, string tableJoinWithShortVersion, string sloupceJezZiskavat, string onKlazuleOdNuly, params AB[] where)
    {
        return SqlOpsI.ci.SelectOneRowInnerJoin(d, tableFromWithShortVersion, tableJoinWithShortVersion, sloupceJezZiskavat, onKlazuleOdNuly, where);
    }

    /// <summary>
    /// Do A4 se zadává např. p.ID = stf.IDPhoto
    /// </summary>
    public DataTable SelectTableInnerJoin(string tableFromWithShortVersion, string tableJoinWithShortVersion, string sloupceJezZiskavat, string onKlazuleOdNuly)
    {
        return SqlOpsI.ci.SelectTableInnerJoin(tableFromWithShortVersion, tableJoinWithShortVersion, sloupceJezZiskavat, onKlazuleOdNuly);
    }

    /// <summary>
    /// Pokud nenajde, vrátí DateTime.MinValue
    /// Do A4 zadej DateTime.MinValue pokud nevíš - je to původní hodnota
    /// </summary>
    /// <param name="table"></param>
    /// <param name="column"></param>
    /// <returns></returns>
    public DateTime SelectMaxDateTime(string table, string column, ABC whereIs, ABC whereIsNot, DateTime getIfNotFound)
    {
        return SqlOpsI.ci.SelectMaxDateTime(d, table, column, whereIs, whereIsNot, getIfNotFound);
    }

    public List<int> SelectValuesOfColumnAllRowsInt(bool signed, string tabulka, string sloupec, int maxRows, ABC whereIs, ABC whereIsNot)
    {
        return SqlOpsI.ci.SelectValuesOfColumnAllRowsInt(Signed(signed), tabulka, sloupec, maxRows, whereIs, whereIsNot);
    }
    public DataTable SelectDataTableGroupBy(string table, string columns, string groupByColumns)
    {
        return SqlOpsI.ci.SelectDataTableGroupBy(d, table, columns, groupByColumns);
    }


    /// <summary>
    /// Pracuje jako signed.
    /// Vrací skutečně nejvyšší ID, proto když chceš pomocí ní ukládat do DB, musíš si to číslo inkrementovat
    /// Ignoruje vynechaná čísla. Žádná hodnota v sloupci A2 nebyla nalezena, vrátí long.MaxValue
    /// </summary>
    /// <param name="p"></param>
    /// <param name="sloupecID"></param>
    /// <returns></returns>
    public long SelectLastIDFromTable(string p, string sloupecID)
    {
        return SqlOpsI.ci.SelectLastIDFromTable(d, p, sloupecID);
    }

    /// <summary>
    /// If no row was found, return max value
    /// 
    /// Vrátí všechny hodnoty z sloupce A3 a pak počítá od A2.MinValue až narazí na hodnotu která v tabulce nebyla, tak ji vrátí
    /// Proto není potřeba vr nijak inkrementovat ani jinak měnit
    /// </summary>
    /// <param name="table"></param>
    /// <param name="idt"></param>
    /// <param name="sloupecID"></param>
    /// <returns></returns>
    public object SelectLastIDFromTableSigned2(string table, Type idt, string sloupecID)
    {
        return SqlOpsI.ci.SelectLastIDFromTableSigned2(d, table, idt, sloupecID);
    }

    /// <summary>
    /// Has signed, therefore can return values below -1
    /// 
    /// 
    /// Nedá se použít na desetinné typy
    /// Vrátí mi nejmenší volné číslo tabulky A1
    /// Pokud bude obsazene 1,3, vrátí až 4
    /// </summary>
    /// <param name="p"></param>
    /// <param name="idt"></param>
    /// <param name="sloupecID"></param>
    /// <param name="totalLower"></param>
    /// <returns></returns>
    public object SelectLastIDFromTableSigned(string p, Type idt, string sloupecID, out bool totalLower)
    {
        return SqlOpsI.ci.SelectLastIDFromTableSigned(d, p, idt, sloupecID, out totalLower).result;
    }

    public int SelectFirstAvailableIntIndex(bool signed, string table, string column)
    {
        return SqlOpsI.ci.SelectFirstAvailableIntIndex(Signed(signed), table, column);
    }

    public short SelectFirstAvailableShortIndex(bool signed, string table, string column)
    {
        return SqlOpsI.ci.SelectFirstAvailableShortIndex(Signed(signed), table, column);
    }

    public short SelectMaxShortMinValue(string table, string column)
    {
        return SqlOpsI.ci.SelectMaxShortMinValue(d, table, column);
    }

    /// <summary>
    /// Vrací int.MaxValue pokud tabulka nebude mít žádné řádky, na rozdíl od metody SelectMaxInt, která vrací 0
    /// To co vrátí tato metoda můžeš vždy jen inkrementovat a vložit do tabulky
    /// </summary>
    /// <param name="table"></param>
    /// <param name="column"></param>
    /// <returns></returns>
    public int SelectMaxIntMinValue(string table, string column)
    {
        return SqlOpsI.ci.SelectMaxIntMinValue(d, table, column);
    }

    public DateTime SelectMaxDateTime(string table, string column, params AB[] ab)
    {
        return SqlOpsI.ci.SelectMaxDateTime(d, table, column, ab);
    }

    /// <summary>
    /// Vrací 0 pokud tabulka nebude mít žádné řádky, na rozdíl od metody SelectMaxIntMinValue, která vrací int.MinValue
    /// </summary>
    /// <param name="table"></param>
    /// <param name="column"></param>
    /// <returns></returns>
    public int SelectMaxInt(string table, string column)
    {
        return SqlOpsI.ci.SelectMaxInt(table, column);
    }

    public int SelectMaxIntMinValue(string table, string sloupec, params AB[] aB)
    {
        return SqlOpsI.ci.SelectMaxIntMinValue(d, table, sloupec, aB);
    }

    public short SelectMinShortMinValue(string table, string sloupec, params AB[] aB)
    {
        return SqlOpsI.ci.SelectMinShortMinValue(d, table, sloupec, aB);
    }

    public byte SelectMaxByte(string table, string column, params AB[] aB)
    {
        return SqlOpsI.ci.SelectMaxByte(d, table, column, aB);
    }

    public int SelectMinInt(string table, string column)
    {
        return SqlOpsI.ci.SelectMaxByte(d, table, column);
    }



    public Guid SelectNewId()
    {
        return SqlOpsI.ci.SelectNewId(d);
    }



    public long SelectCount(string table)
    {
        return SqlOpsI.ci.SelectCount(d, table);
    }

    public long SelectCountOrMinusOne(string table)
    {
        return SqlOpsI.ci.SelectCountOrMinusOne(d, table);
    }

    public long SelectCount(string table, params AB[] abc)
    {
        return SqlOpsI.ci.SelectCount(d, table, abc);
    }

    public List<long> SelectGroupByLong(string table, string GroupByColumn, params AB[] where)
    {
        return SqlOpsI.ci.SelectGroupByLong(d, table, GroupByColumn, where);
    }

    public List<int> SelectGroupByInt(string table, string GroupByColumn, params AB[] where)
    {
        return SqlOpsI.ci.SelectGroupByInt(d, table, GroupByColumn, where);
    }

    /// <summary>
    /// Vrátí z řádků který je označen jako group by vždy jen 1 řádek
    /// </summary>
    /// <param name="signed"></param>
    /// <param name="table"></param>
    /// <param name="GroupByColumn"></param>
    /// <param name="IDColumnName"></param>
    /// <param name="IDColumnValue"></param>
    /// <returns></returns>
    public List<short> SelectGroupByShort(bool signed, string table, string GroupByColumn, string IDColumnName, object IDColumnValue)
    {
        return SqlOpsI.ci.SelectGroupByShort(Signed(signed), table, GroupByColumn, IDColumnName, IDColumnValue);
    }

    public List<long> SelectGroupByLong(bool signed, string table, string GroupByColumn, string IDColumnName, object IDColumnValue)
    {
        return SqlOpsI.ci.SelectGroupByLong(Signed(signed), table, GroupByColumn, IDColumnName, IDColumnValue);
    }

    /// <summary>
    /// Zjištuje to ze všech řádků v databázi.
    /// Calculate sum of ViewCount of A2 in A1
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <param name="table"></param>
    /// <param name="idEntity"></param>
    /// <returns></returns>
    public uint SelectSumOfViewCount(string table, long idEntity)
    {
        return SqlOpsI.ci.SelectSumOfViewCount(d, table, idEntity);
    }


    public int SelectSum(string table, string columnToSum, params AB[] aB)
    {
        return SqlOpsI.ci.SelectSum(d, table, columnToSum, aB);
    }

    public int SelectSumByte(string table, string columnToSum, params AB[] aB)
    {
        return SqlOpsI.ci.SelectSumByte(table, columnToSum, aB);
    }

    /// <summary>
    /// Tuto metodu nepoužívej například po vkládání, když chceš zjistit ID posledního řádku, protože když tam bude něco smazaného , tak to budeš mít o to posunuté !!
    /// 
    /// </summary>
    public int SelectFindOutNumberOfRows(string tabulka)
    {
        return SqlOpsI.ci.SelectFindOutNumberOfRows(d, tabulka);
    }

    /// <summary>
    /// 
    /// </summary>
    public List<string> SelectNamesOfIDs(string tabulka, List<int> idFces)
    {
        return SqlOpsI.ci.SelectNamesOfIDs(d, tabulka, idFces);
    }

    /// <summary>
    /// 
    /// </summary>
    public int SelectID(bool signed, string tabulka, string nazevSloupce, object hodnotaSloupce)
    {
        return SqlOpsI.ci.SelectID(Signed(signed), tabulka, nazevSloupce, hodnotaSloupce);
    }

    /// <summary>
    /// Vrátí SE, když nebude nalezena 
    /// </summary>
    public string SelectNameOfID(string tabulka, long id)
    {
        return SqlOpsI.ci.SelectNameOfID(d, tabulka, id);
    }

    public string SelectNameOfID(string tabulka, long id, string nameColumnID)
    {
        return SqlOpsI.ci.SelectNameOfID(d, tabulka, id, nameColumnID);
    }

    public string SelectNameOfIDOrSE(string tabulka, string idColumnName, int id)
    {
        return SqlOpsI.ci.SelectNameOfIDOrSE(d, tabulka, idColumnName, id);
    }

    public object[] SelectRowReaderLimit(string tableName, int limit, string sloupce, string sloupecWhere, object hodnotaWhere)
    {
        return SqlOpsI.ci.SelectRowReaderLimit(d, tableName, limit, sloupce, sloupecWhere, hodnotaWhere);
    }

    /// <summary>
    /// Interně volá metodu SelectRowReader
    /// If fail, return null
    /// </summary>
    /// <param name="tabulka"></param>
    /// <param name="sloupecID"></param>
    /// <param name="id"></param>
    /// <param name="nazvySloupcu"></param>
    /// <returns></returns>
    public object[] SelectSelectiveOneRow(string tabulka, string nazvySloupcu, string sloupecID, object id)
    {
        return SqlOpsI.ci.SelectSelectiveOneRow(d, tabulka, nazvySloupcu, sloupecID, id);
    }



    public object[] SelectRowReader(string tabulka, string nazvySloupcu, string sloupecID, object id)
    {
        return SqlOpsI.ci.SelectRowReader(d,tabulka, nazvySloupcu, sloupecID, id);
    }

    /// <summary>
    /// Vrátí null, pokud výsledek nebude mít žádné řádky
    /// </summary>
    /// <param name="comm"></param>
    /// <returns></returns>
    private object[] SelectRowReader(SqlCommand comm)
    {
        return SqlOpsI.ci.SelectRowReader(d, comm);
    }

    /// <summary>
    /// Vrátí null pokud žádný takový řádek nebude nalezen
    /// </summary>
    /// <param name="table"></param>
    /// <param name="vratit"></param>
    /// <param name="ab"></param>
    /// <returns></returns>
    public object[] SelectSelectiveOneRow(string table, string vratit, params AB[] ab)
    {
        return SqlOpsI.ci.SelectSelectiveOneRow(d, table, vratit, ab);
    }

    public bool SelectExistsCombination(string p, params AB[] aB)
    {
        return SqlOpsI.ci.SelectExistsCombination(d, p, aB);
    }

    /// <summary>
    /// 
    /// </summary>
    public bool SelectExistsCombination(string p, ABC aB)
    {
        return SqlOpsI.ci.SelectExistsCombination(d, p, aB);
    }

    public bool SelectExistsCombination(string p, ABC where, ABC whereIsNot)
    {
        return SqlOpsI.ci.SelectExistsCombination(d, p, where, whereIsNot);
    }

    /// <summary>
    /// 
    /// </summary>
    public bool SelectExists(string tabulka, string sloupec, object hodnota)
    {
        return SqlOpsI.ci.SelectExists(d, tabulka, sloupec, hodnota);
    }

    //public short SelectCellDataTableShortOneRow(bool signed, string table, string vracenySloupec, string whereColumn, object whereValue)
    //{
    //    return SelectCellDataTableShortOneRow(signed, table, vracenySloupec, new AB(whereColumn, whereValue));
    //}

    /// <summary>
    /// Exists method without AB but has switched whereColumn and whereValue
    /// </summary>
    /// <param name="signed"></param>
    /// <param name="table"></param>
    /// <param name="vracenySloupec"></param>
    /// <param name="abc"></param>
    /// <returns></returns>
    public short SelectCellDataTableShortOneRow(bool signed, string table, string vracenySloupec, params AB[] abc)
    {
        return SqlOpsI.ci.SelectCellDataTableShortOneRow(Signed(signed), table, vracenySloupec, abc);
    }

    /// <summary>
    /// G -1 když se žádný takový řádek nepodaří najít
    /// </summary>
    /// <param name="table"></param>
    /// <param name="vracenySloupec"></param>
    /// <param name="abc"></param>
    /// <returns></returns>
    public int SelectCellDataTableIntOneRow(bool signed, string table, string vracenySloupec, ABC whereIs, ABC whereIsNot)
    {
        return SqlOpsI.ci.SelectCellDataTableIntOneRow(Signed(signed), table, vracenySloupec, whereIs, whereIsNot);
    }

    /// <summary>
    /// Vrací -1 pokud se nepodaří najít if !A1 nebo long.MaxValue když A1
    /// </summary>
    /// <param name="signed"></param>
    /// <param name="table"></param>
    /// <param name="vracenySloupec"></param>
    /// <param name="abc"></param>
    /// <returns></returns>
    public long SelectCellDataTableLongOneRow(bool signed, string table, string vracenySloupec, params AB[] abc)
    {
        return SqlOpsI.ci.SelectCellDataTableLongOneRow(Signed(signed), table, vracenySloupec, abc);
    }

    /// <summary>
    /// Vrátí -1 pokud žádný takový řádek nenalezne pokud !A1 enbo short.MaxValue pokud A1
    /// </summary>
    /// <param name="table"></param>
    /// <param name="idColumnName"></param>
    /// <param name="idColumnValue"></param>
    /// <param name="vracenySloupec"></param>
    /// <returns></returns>
    public int SelectCellDataTableIntOneRow(bool signed, string table, string vracenySloupec, string idColumnName, object idColumnValue)
    {
        return SqlOpsI.ci.SelectCellDataTableIntOneRow(Signed(signed), table, vracenySloupec, idColumnName, idColumnValue);
    }

    /// <summary>
    /// Vrací -1 pokud se nepodaří najít if !A1 nebo long.MaxValue když A1.
    /// </summary>
    /// <param name="table"></param>
    /// <param name="idColumnName"></param>
    /// <param name="idColumnValue"></param>
    /// <param name="vracenySloupec"></param>
    /// <returns></returns>
    public long SelectCellDataTableLongOneRow(bool signed, string table, string vracenySloupec, string idColumnName, object idColumnValue)
    {
        return SqlOpsI.ci.SelectCellDataTableLongOneRow(Signed(signed), table, vracenySloupec, idColumnName, idColumnValue);
    }

    public DateTime SelectCellDataTableDateTimeOneRow(string table, string vracenySloupec, DateTime getIfNotFound, string idColumnName, object idColumnValue)
    {
        d.getIfNotFound = getIfNotFound;
        return SqlOpsI.ci.SelectCellDataTableDateTimeOneRow(d, table, vracenySloupec, )
    }

    /// <summary>
    /// Vrátí DateTimeMinVal, pokud takový řádek nebude nalezen.
    /// </summary>
    /// <param name="table"></param>
    /// <param name="vracenySloupec"></param>
    /// <param name="where"></param>
    /// <param name="whereIsNot"></param>
    /// <returns></returns>
    public DateTime SelectCellDataTableDateTimeOneRow(string table, string vracenySloupec, DateTime getIfNotFound, ABC where, ABC whereIsNot)
    {
        var d = new SqlData { getIfNotFound = getIfNotFound };
        return SqlOpsI.ci.SelectCellDataTableDateTimeOneRow(d, table, vracenySloupec, where, whereIsNot);
    }

    /// <summary>
    /// Vrátí null pokud se řádek nepodaří najít
    /// A3 nebo A4 může být null
    /// </summary>
    /// <param name="table"></param>
    /// <param name="vracenySloupec"></param>
    /// <param name="where"></param>
    /// <param name="whereIsNot"></param>
    /// <returns></returns>
    public bool? SelectCellDataTableNullableBoolOneRow(string table, string vracenySloupec, ABC where, ABC whereIsNot)
    {
        return SqlOpsI.ci.SelectCellDataTableBoolOneRow(d, table, vracenySloupec, where, whereIsNot);
    }

    public bool SelectCellDataTableBoolOneRow(string table, string vracenySloupec, ABC where, ABC whereIsNot)
    {
        return SqlOpsI.ci.SelectCellDataTableBoolOneRow(d, table, vracenySloupec, where, whereIsNot);
    }

    /// <summary>
    /// Vrátí 0 pokud takový řádek nebude nalezen.
    /// </summary>
    public byte SelectCellDataTableByteOneRow(string table, string vracenySloupec, params AB[] where)
    {
        return SqlOpsI.ci.SelectCellDataTableByteOneRow(d, table, vracenySloupec, where);
    }

    /// <summary>
    /// When not found, return int.MaxValue when A1 or -1 when not
    /// </summary>
    /// <param name="signed"></param>
    /// <param name="table"></param>
    /// <param name="vracenySloupec"></param>
    /// <param name="abc"></param>
    /// <returns></returns>
    public int SelectCellDataTableIntOneRow(bool signed, string table, string vracenySloupec, params AB[] abc)
    {
        return SqlOpsI.ci.SelectCellDataTableIntOneRow(Signed(signed), table, vracenySloupec, abc);
    }

    /// <summary>
    /// Vrátí 0 pokud takový řádek nebude nalezen.
    /// </summary>
    /// <param name="table"></param>
    /// <param name="idColumnName"></param>
    /// <param name="idColumnValue"></param>
    /// <param name="vracenySloupec"></param>
    /// <returns></returns>
    public byte SelectCellDataTableByteOneRow(string table, string vracenySloupec, string idColumnName, object idColumnValue)
    {
        return SqlOpsI.ci.SelectCellDataTableByteOneRow(d, table, vracenySloupec, idColumnName, idColumnValue);
    }

    public bool SelectCellDataTableBoolOneRow(string table, string vracenySloupec, string idColumnName, object idColumnValue)
    {
        return SqlOpsI.ci.SelectCellDataTableBoolOneRow(d, table, vracenySloupec, idColumnName, idColumnValue);
    }

    public bool? SelectCellDataTableNullableBoolOneRow(string table, string vracenySloupec, params AB[] abc)
    {
        return SqlOpsI.ci.SelectCellDataTableNullableBoolOneRow(d, table, vracenySloupec, abc);
    }

    /// <summary>
    /// V případě nenalezení vrátí -1 pokud !A1, jinak short.MaxValue
    /// </summary>
    /// <param name="table"></param>
    /// <param name="idColumnName"></param>
    /// <param name="idColumnValue"></param>
    /// <param name="vracenySloupec"></param>
    /// <returns></returns>
    public short SelectCellDataTableShortOneRow(bool signed, string table, string vracenySloupec, string idColumnName, object idColumnValue)
    {
        return SqlOpsI.ci.SelectCellDataTableShortOneRow(Signed(signed), table, vracenySloupec, idColumnName, idColumnValue);
    }

    /// <summary>
    /// Vrátí -1 když řádek nebude nalezen a !A1.
    /// Vrátí float.MaxValue když řádek nebude nalezen a A1.
    /// </summary>
    /// <param name="signed"></param>
    /// <param name="table"></param>
    /// <param name="idColumnName"></param>
    /// <param name="idColumnValue"></param>
    /// <param name="vracenySloupec"></param>
    /// <returns></returns>
    public float SelectCellDataTableFloatOneRow(bool signed, string table, string vracenySloupec, string idColumnName, object idColumnValue)
    {
        return SqlOpsI.ci.SelectCellDataTableFloatOneRow(Signed(signed), table, vracenySloupec, idColumnName, idColumnValue);
    }

    /// <summary>
    /// Vrátí -1 když řádek nebude nalezen a !A1.
    /// Vrátí float.MaxValue když řádek nebude nalezen a A1.
    /// </summary>
    /// <param name="signed"></param>
    /// <param name="table"></param>
    /// <param name="idColumnName"></param>
    /// <param name="idColumnValue"></param>
    /// <param name="vracenySloupec"></param>
    /// <returns></returns>
    public float SelectCellDataTableFloatOneRow(bool signed, string table, string vracenySloupec, params AB[] ab)
    {
        return SqlOpsI.ci.SelectCellDataTableFloatOneRow(Signed(signed), table, vracenySloupec, ab);
    }

    public object SelectCellDataTableObjectOneRow(string table, string vracenySloupec, string idColumnName, object idColumnValue)
    {
        return SqlOpsI.ci.SelectCellDataTableObjectOneRow(d, table, vracenySloupec, idColumnName, idColumnValue);
    }

    /// <summary>
    /// Vykonává metodou ExecuteScalar. Ta pokud vrátí null, metoda vrátí "". To je taky rozdíl oproti metodě SelectCellDataTableStringOneRowABC.
    /// </summary>
    /// <param name="tabulka"></param>
    /// <param name="nazvySloupcu"></param>
    /// <param name="ab"></param>
    /// <returns></returns>
    public string SelectCellDataTableStringOneRow(string tabulka, string nazvySloupcu, params AB[] ab)
    {
        return SqlOpsI.ci.SelectCellDataTableStringOneRow(d, tabulka, nazvySloupcu, ab);
    }



    public string SelectCellDataTableStringOneLastRow(string table, string vracenySloupec, string orderByDesc, string idColumnName, object idColumnValue)
    {
        var data = new SqlData { orderBy = orderByDesc, sortOrder = SortOrder.Descending };
        return SqlOpsI.ci.SelectCellDataTableStringOneRow(data, table, vracenySloupec, idColumnName, idColumnValue);
    }

    /// <summary>
    /// Vrátí SE v případě že řádek nebude nalezen, nikdy nevrací null.
    /// Automaticky vytrimuje
    /// </summary>
    /// <param name="table"></param>
    /// <param name="idColumnName"></param>
    /// <param name="idColumnValue"></param>
    /// <param name="vracenySloupec"></param>
    /// <returns></returns>
    public string SelectCellDataTableStringOneRow(string table, string vracenySloupec, string idColumnName, object idColumnValue)
    {
        return SqlOpsI.ci.SelectCellDataTableStringOneRow(d, table, vracenySloupec, idColumnName, idColumnValue);
    }

    /// <summary>
    /// A4 může být null, A3 nikoliv
    /// </summary>
    /// <param name="table"></param>
    /// <param name="vracenySloupec"></param>
    /// <param name="where"></param>
    /// <param name="whereIsNot"></param>
    /// <returns></returns>
    public string SelectCellDataTableStringOneRow(string table, string vracenySloupec, ABC where, ABC whereIsNot)
    {
        return SqlOpsI.ci.SelectCellDataTableStringOneRow(d, table, vracenySloupec, where, whereIsNot);
    }

    /// <summary>
    /// Nepoužívat a smazat!!!
    /// </summary>
    /// <param name="TableName"></param>
    /// <param name="whereSloupec"></param>
    /// <param name="whereValue"></param>
    /// <returns></returns>
    public DataTable SelectDataTableAllRows(string TableName, string whereSloupec, object whereValue)
    {
        return SqlOpsI.ci.SelectDataTableAllRows(d, TableName, whereSloupec, whereValue);
    }

    /// <summary>
    /// Nepužívat a smazat!!!
    /// </summary>
    public DataTable SelectDataTableAllRows(string table)
    {
        return SqlOpsI.ci.SelectDataTableAllRows(d, table);
    }

    /// <summary>
    /// Nepoužívat a smazat!!!
    /// Tato metoda se přesně hodí když chci získat nějaký nejoblíbenější obsah - srovnává podle hodnoty v A3.
    /// Tato metoda vrací celé řádky z tabulky, Je zde stejně pojmenovaná metoda s A4, kde můžeš specifikovat sloupce které chceš vrátit.
    /// </summary>
    /// <param name="tableName"></param>
    /// <param name="limit"></param>
    /// <param name="sloupecID"></param>
    /// <param name="abc"></param>
    /// <returns></returns>
    public DataTable SelectDataTableLastRows(string tableName, int limit, string sloupecID, params AB[] abc)
    {
        var data = new SqlData { limit = limit };
        return SqlOpsI.ci.SelectDataTableLastRows(data, tableName, sloupecID, abc);
    }

    /// <summary>
    /// Nepoužívat a smazat !!!
    /// Vrátí null když nenalezne žádný řádek
    /// </summary>
    public object[] SelectOneRow(string TableName, string nazevSloupce, object hodnotaSloupce)
    {
        return SqlOpsI.ci.SelectDataTableLastRows(d, TableName, "*", nazevSloupce, hodnotaSloupce).result;
    }

    public object[] SelectOneRow(string TableName, string nazevSloupce, object hodnotaSloupce)
    {
        return SqlOpsI.ci.SelectOneRow(d, TableName, nazevSloupce, hodnotaSloupce);
    }

    /// <summary>
    /// Nepoužívat a smazat!!!
    /// Jakékoliv změny zde musíš provést i v metodě SelectValuesOfColumnAllRowsString
    /// </summary>
    /// <param name="tabulka"></param>
    /// <param name="sloupec"></param>
    /// <returns></returns>
    public List<string> SelectValuesOfColumnAllRowsStringTrim(string tabulka, string sloupec)
    {
        return SqlOpsI.ci.SelectValuesOfColumnAllRowsStringTrim(d, tabulka, sloupec);
    }

    public List<byte> SelectValuesOfColumnAllRowsByte(string tabulka, string sloupec, params AB[] ab)
    {
        return SqlOpsI.ci.SelectValuesOfColumnAllRowsByte(d, tabulka, sloupec, ab);
    }

    public List<byte> SelectValuesOfColumnAllRowsByte(string tabulka, int limit, string sloupec, params AB[] ab)
    {
        var data = new SqlData { limit  = limit};
        return SqlOpsI.ci.SelectValuesOfColumnAllRowsByte(data, tabulka, sloupec, ab);
    }

    /// <summary>
    /// Nepoužívat a smazat!!!
    /// </summary>
    public DataTable SelectDataTable(string tableName, int limit)
    {
        
    }

    public int UpdateWhereIsLowerThan(string table, string columnToUpdate, object newValue, string columnLowerThan, DateTime valueLowerThan, params AB[] where)
    {
        return SqlOpsI.ci.UpdateWhereIsLowerThan(d, table, columnToUpdate, newValue, columnLowerThan, valueLowerThan, where);
    }

    public void UpdateValuesCombinationCombinedWhere(string TableName, ABC sets, ABC where)
    {
        return SqlOpsI.ci.UpdateValuesCombinationCombinedWhere(d, TableName, sets, where);
    }

    /// <summary>
    /// Pokud se řádek nepodaří najít, vrátí -1
    /// </summary>
    /// <param name="table"></param>
    /// <param name="sloupecID"></param>
    /// <param name="id"></param>
    /// <param name="sloupecKUpdate"></param>
    /// <param name="pridej"></param>
    /// <returns></returns>
    public float UpdatePlusRealValue(string table, string sloupecKUpdate, float pridej, string sloupecID, int id)
    {
        return SqlOpsI.ci.UpdatePlusRealValue(d, table, sloupecKUpdate, pridej, sloupecID, id);
    }

    /// <summary>
    /// Updatuje pouze 1 řádek
    /// </summary>
    /// <param name="tablename"></param>
    /// <param name="updateColumn"></param>
    /// <param name="idColumn"></param>
    /// <param name="idValue"></param>
    /// <returns></returns>
    public bool UpdateSwitchBool(string tablename, string updateColumn, string idColumn, object idValue)
    {
        return SqlOpsI.ci.UpdateSwitchBool(d, tablename, updateColumn, idColumn, idValue);
    }

    /// <summary>
    /// 
    /// </summary>
    public int UpdateAppendStringValue(string tableName, string sloupecAppend, string hodnotaAppend, string sloupecID, object hodnotaID)
    {
        return SqlOpsI.ci.UpdateAppendStringValue(d, tableName, sloupecAppend, hodnotaAppend, sloupecID, hodnotaID);
    }

    /// <summary>
    /// Vrátí nohou hodnotu v DB
    /// </summary>
    /// <param name="table"></param>
    /// <param name="sloupecID"></param>
    /// <param name="id"></param>
    /// <param name="sloupecKUpdate"></param>
    /// <param name="pridej"></param>
    /// <returns></returns>
    public int UpdateSumIntValue(string table, string sloupecKUpdate, int pridej, string sloupecID, object id)
    {
        return SqlOpsI.ci.UpdateSumIntValue(d, table, sloupecKUpdate, pridej, sloupecID, id);
    }

    public long UpdateSumLongValue(string table, string sloupecKUpdate, int pridej, string sloupecID, object id)
    {
        return SqlOpsI.ci.UpdateSumLongValue(d, table, sloupecKUpdate, pridej, sloupecKUpdate, id);
    }

    /// <summary>
    /// Nahrazuje ve všech řádcích
    /// </summary>
    /// <param name="table"></param>
    /// <param name="sloupecKUpdate"></param>
    /// <param name="odeber"></param>
    /// <param name="abc"></param>
    /// <returns></returns>
    public short UpdateMinusShortValue(string table, string sloupecKUpdate, short odeber, params AB[] abc)
    {
        return SqlOpsI.ci.UpdateMinusShortValue(d, table, sloupecKUpdate, odeber, abc);
    }

    /// <summary>
    /// Vrací normalizovaný short
    /// </summary>
    /// <param name="table"></param>
    /// <param name="sloupecKUpdate"></param>
    /// <param name="odeber"></param>
    /// <param name="abc"></param>
    /// <returns></returns>
    public ushort UpdateMinusNormalizedShortValue(string table, string sloupecKUpdate, ushort odeber, params AB[] abc)
    {
        return SqlOpsI.ci.UpdateMinusNormalizedShortValue(d, table, sloupecKUpdate, odeber, abc);
    }

    public short UpdateMinusShortValue(string table, string sloupecKUpdate, short odeber, string sloupecID, object hodnotaID)
    {
        short d = SelectCellDataTableShortOneRow(true, table, sloupecKUpdate, sloupecID, hodnotaID);
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

    public int UpdatePlusIntValue(string table, string sloupecKUpdate, int pridej, params AB[] abc)
    {
        int d = SelectCellDataTableIntOneRow(true, table, sloupecKUpdate, abc);
        // Check for signed is useless - in signed or not always return maxValue
        if (d == int.MaxValue)
        {
            return d;
        }
        int n = pridej;
        n = d + pridej;
        Update(table, sloupecKUpdate, n, abc);
        return n;
    }

    public int UpdatePlusIntValue(string table, string sloupecKUpdate, int pridej, string sloupecID, object hodnotaID)
    {
        int d = SelectCellDataTableIntOneRow(true, table, sloupecKUpdate, sloupecID, hodnotaID);

        if (d == int.MaxValue)
        {
            return d;
        }
        int n = pridej;
        n = d + pridej;
        Update(table, sloupecKUpdate, n, sloupecID, hodnotaID);
        return n;
    }

    /// <summary>
    /// Aktualizuje všechny řádky
    /// Vrátí novou zapsanou hodnotu
    /// </summary>
    /// <param name="table"></param>
    /// <param name="sloupecKUpdate"></param>
    /// <param name="odeber"></param>
    /// <param name="abc"></param>
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
        long d = SelectCellDataTableLongOneRow(true, table, sloupecKUpdate, sloupecID, hodnotaID);

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
        short d = SelectCellDataTableShortOneRow(true, table, sloupecKUpdate, sloupecID, hodnotaID);
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
        byte d = SelectCellDataTableByteOneRow(table, sloupecKUpdate, sloupecID, hodnotaID);
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
    /// <param name="tableName"></param>
    /// <param name="sloupecID"></param>
    /// <param name="hodnotaID"></param>
    /// <param name="sloupecAppend"></param>
    /// <param name="hodnotaAppend"></param>
    /// <returns></returns>
    public int UpdateAppendStringValueCheckExistsOneRow(string tableName, string sloupecAppend, string hodnotaAppend, string sloupecID, object hodnotaID)
    {
        string aktual = SelectCellDataTableStringOneRow(tableName, sloupecAppend, sloupecID, hodnotaID);

        List<string> d = new List<string>(SH.Split(aktual, ","));
        if (!d.Contains(hodnotaAppend))
        {
            aktual += hodnotaAppend + ",";
            string save = SH.Join(',', d.ToArray());

            return Update(tableName, sloupecAppend, aktual, sloupecID, hodnotaID);
        }
        return 0;
    }

    /// <summary>
    /// 
    /// </summary>
    public int UpdateCutStringValue(string tableName, string sloupecCut, string hodnotaCut, string sloupecID, object hodnotaID)
    {
        string aktual = SelectCellDataTableStringOneRow(tableName, sloupecCut, sloupecID, hodnotaID);

        List<string> d = new List<string>(SH.Split(aktual, ","));
        d.Remove(hodnotaCut);
        string save = SH.JoinWithoutTrim(",", d);
        return Update(tableName, sloupecCut, save, sloupecID, hodnotaID);
    }

    /// <summary>
    /// Conn nastaví automaticky
    /// </summary>
    public int Update(string table, string sloupecKUpdate, object n, string sloupecID, object id)
    {
        string sql = string.Format("UPDATE {0} SET {1}=@p0 WHERE {2} = @p1", table, sloupecKUpdate, sloupecID);
        SqlCommand comm = new SqlCommand(sql);
        AddCommandParameter(comm, 0, n);
        AddCommandParameter(comm, 1, id);
        //SqlException: String or binary data would be truncated.
        return ExecuteNonQuery(comm);
    }

    /// <summary>
    /// Conn přidá automaticky
    /// Název metody je sice OneRow ale updatuje to libovolný počet řádků které to najde pomocí where - je to moje interní pojmenování aby mě to někdy trklo, možná později přijdu na způsob jak updatovat jen jeden řádek.
    /// </summary>
    public int Update(string table, string sloupecKUpdate, object n, string sloupecID, object id)
    {
        string sql = string.Format("UPDATE TOP(1) {0} SET {1}=@p1 WHERE {2} = @p2", table, sloupecKUpdate, sloupecID);
        SqlCommand comm = new SqlCommand(sql);
        AddCommandParameter(comm, 1, n);
        AddCommandParameter(comm, 2, id);
        return ExecuteNonQuery(comm);
    }

    public int Update(string table, string sloupecKUpdate, object n, params AB[] ab)
    {
        int pridavatOd = 1;
        string sql = string.Format("UPDATE TOP(1) {0} SET {1}=@p0", table, sloupecKUpdate) + GeneratorMsSql.CombinedWhere(new ABC(ab), ref pridavatOd);
        SqlCommand comm = new SqlCommand(sql);
        AddCommandParameter(comm, 0, n);
        AddCommandParameteres(comm, 1, ab);
        return ExecuteNonQuery(comm);
    }

    private int Update(string table, string sloupecKUpdate, int n, ABC abc)
    {
        int parametrSet = abc.Length;
        string sql = string.Format("UPDATE {0} SET {1}=@p" + parametrSet + " {2}", table, sloupecKUpdate, GeneratorMsSql.CombinedWhere(abc));
        SqlCommand comm = new SqlCommand(sql);
        AddCommandParameter(comm, parametrSet, n);
        for (int i = 0; i < parametrSet; i++)
        {
            AddCommandParameter(comm, i, abc[i].B);
        }
        int vr = ExecuteNonQuery(comm);
        return vr;
    }


    /// <summary>
    /// Return count of rows affected
    /// </summary>
    /// <param name="table"></param>
    /// <param name="columnToUpdate"></param>
    /// <param name="newValue"></param>
    /// <param name="abc"></param>
    /// <returns></returns>
    public int Update(string table, string columnToUpdate, object newValue, params AB[] abc)
    {
        int parametrSet = abc.Length;
        string sql = string.Format("UPDATE {0} SET {1}=@p" + parametrSet + " {2}", table, columnToUpdate, GeneratorMsSql.CombinedWhere(abc));
        SqlCommand comm = new SqlCommand(sql);
        AddCommandParameter(comm, parametrSet, newValue);
        for (int i = 0; i < parametrSet; i++)
        {
            AddCommandParameter(comm, i, abc[i].B);
        }
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

    public void UpdateValuesCombination(string TableName, string nameOfColumn, object valueOfColumn, params object[] setsNameValue)
    {
        ABC abc = new ABC(setsNameValue);
        UpdateValuesCombination(TableName, nameOfColumn, valueOfColumn, abc.ToArray());
    }

    /// <summary>
    /// Conn nastaví automaticky
    /// </summary>
    public void UpdateValuesCombination(string TableName, string nameOfColumn, object valueOfColumn, params AB[] sets)
    {
        string setString = GeneratorMsSql.CombinedSet(sets);
        //int pocetParametruSets = sets.Length;
        int indexParametrWhere = sets.Length;
        SqlCommand comm = new SqlCommand(string.Format("UPDATE {0} {1} WHERE {2}={3}", TableName, setString, nameOfColumn, "@p" + (indexParametrWhere).ToString()));
        for (int i = 0; i < indexParametrWhere; i++)
        {
            // V takových případech se nikdy nepokoušej násobit, protože to vždy končí špatně
            AddCommandParameter(comm, i, sets[i].B);
        }
        AddCommandParameter(comm, indexParametrWhere, valueOfColumn);
        // NT-Při úpravách uprav i UpdateValuesCombinationCombinedWhere
        ExecuteNonQuery(comm);
    }


}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using sunamo;


/// <summary>
/// 12-1-2019 refactoring:
/// 1)all methods here take ABC if take more than Where. otherwise is allowed params AB[] / object[]
/// 2) always is table - select / update column - where
/// 
/// </summary>
public partial class SqlOperations : SqlServerHelper
{
    //public static PpkOnDrive loggedCommands = null;
    public static string table2 = null;
    public static string column2 = null;
    public static bool isNVarChar2 = false;
    public static Func<string, string, bool> IsNVarChar = null;
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

    public static SqlOperations ci = new SqlOperations();

    #region Inner classes
    public class Parse
    {
        public class DateTime
        {
            /// <summary>
            /// Vrátí -1 v případě že se nepodaří vyparsovat
            /// </summary>
            /// <param name="p"></param>
            public System.DateTime ParseDateTimeMinVal(string p)
            {
                System.DateTime p2;
                if (System.DateTime.TryParse(p, out p2))
                {
                    return p2;
                }
                return Consts.DateTimeMinVal;
            }
        }
    }
    #endregion

    #region ctors
    static SqlOperations()
    {
        //var f = AppData.ci.GetFile(AppFolders.Logs, "sqlCommands.txt");
        //loggedCommands = new PpkOnDrive(f);
    }

    public SqlOperations()
    {

    } 
    #endregion

    #region AddCommandParameter
    public static int AddCommandParameter(SqlCommand comm, int i, object o)
    {
        string table, column;
        table = column = null;
        return AddCommandParameter(comm, i, o, ref table, ref column);
    }

    /// <summary>
    /// a2 je X jako v příkazu @pX
    /// A3 cant be AB
    /// When returned value will be negative, into db was entered emtpy string - insert new with SQLServerPreparedStatement  https://stackoverflow.com/questions/9066549/nvarchar-max-gives-string-or-binary-data-would-be-truncated
    /// </summary>
    /// <param name="comm"></param>
    /// <param name="i"></param>
    /// <param name="o"></param>
    public static int AddCommandParameter(SqlCommand comm, int i, object o, ref string table, ref string column)
    {
        if (o == null || o.GetType() == DBNull.Value.GetType())
        {
            SqlParameter p = new SqlParameter();
            p.ParameterName = "@p" + i.ToString();
            p.Value = DBNull.Value;
            comm.Parameters.Add(p);
        }
        else if (o.GetType() == typeof(byte[]))
        {
            // Pokud chcete uložit pole bajtů, musíte nejdřív vytvořit parametr s typem v DB(já používám vždy Image) a teprve pak nastavit hodnotu
            SqlParameter param = comm.Parameters.Add("@p" + i.ToString(), SqlDbType.Binary);
            param.Value = o;
        }
        else if (o.GetType() == Types.tString || o.GetType() == Types.tChar)
        {
            string _ = o.ToString();
            //if (_.Length > 7999)
            //{
            //    comm.Parameters.AddWithValue("@p" + i.ToString(), string.Empty);
            //    ++i;
            //    i *= -1;
            //    return i;
            //}

            //true) //
            if (IsNVarChar != null)
            {
                if (SqlServerHelper.GetTableAndColumn(comm.CommandText, ref table2, ref column2, i))
                {
                    isNVarChar2 = IsNVarChar.Invoke(table2, column2);



                    if (!isNVarChar2)
                    {


                        _ = MSStoredProceduresI.ConvertToVarChar(_);
                    }
                }
            }

            comm.Parameters.AddWithValue("@p" + i.ToString(), _);
        }
        else
        {
            comm.Parameters.AddWithValue("@p" + i.ToString(), o);
        }

        ++i;
        return i;
    }

    /// <summary>
    /// Počítá od nuly
    /// </summary>
    /// <param name="comm"></param>
    /// <param name="where"></param>
    public static void AddCommandParameterFromAbc(SqlCommand comm, params AB[] where)
    {
        for (int i = 0; i < where.Length; i++)
        {
            AddCommandParameter(comm, i, where[i].B);
        }
    }

    /// <summary>
    /// Počítá od nuly
    /// Mohu volat i s A2 null, v takovém případě se nevykoná žádný kód
    /// </summary>
    /// <param name="comm"></param>
    /// <param name="where"></param>
    public static int AddCommandParameterFromAbc(SqlCommand comm, int i, ABC where)
    {
        if (where != null)
        {
            for (var i2 = 0; i2 < where.Count; i2++)
            {
                AddCommandParameter(comm, i, where[i2].B);
                i++;
            }
        }
        return i;
    }

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
        AddCommandParameterFromAbc(comm, i2, ab);
    }

    public static void AddCommandParameteresArrays(SqlCommand comm, int i, params ABC[] where)
    {
        AddCommandParameteresCombinedArrays(comm, i, CA.IndexOrNull(where, 0), CA.IndexOrNull(where, 1), CA.IndexOrNull(where, 2), CA.IndexOrNull(where, 3));
    }

    public static int AddCommandParameteres(SqlCommand comm, int pocIndex, params AB[] hodnotyOdNuly)
    {
        foreach (var item in hodnotyOdNuly)
        {
            AddCommandParameter(comm, pocIndex, item.B);
            pocIndex++;
        }
        return pocIndex;
    }

    public static int AddCommandParameteres(SqlCommand comm, int pocIndex, ABC aWhere)
    {
        foreach (var item in aWhere)
        {
            AddCommandParameter(comm, pocIndex, item.B);
            pocIndex++;
        }
        return pocIndex;
    }
    #endregion

    #region Commented
    //public SqlResult ExecuteNonQuery(SqlCommand comm)
    //{
    //    return ExecuteNonQuery(new SqlData { }, comm);
    //}

    ///// <summary>
    ///// Bude se počítat od nuly
    ///// Některé z vnitřních polí může být null
    ///// </summary>
    ///// <param name="comm"></param>
    ///// <param name="where"></param>
    ///// <param name="whereIsNot"></param>
    //public static void AddCommandParameteresArrays(SqlCommand comm, int i, params AB[][] where)
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

    //SqlConnection _conn = null;
    //public SqlConnection conn
    //{
    //    get
    //    {
    //        if (_conn == null)
    //        {
    //            _conn = MSDatabaseLayer._conn;

    //        }
    //        if (string.IsNullOrEmpty( _conn.ConnectionString))
    //        {
    //            MSDatabaseLayer.loadDefaultDatabase();
    //        }
    //        return _conn;
    //    }
    //    set
    //    {
    //        _conn = value;
    //    }
    //}



    //public void RepairConnection()
    //{
    //    SqlConnection.ClearAllPools();
    //    conn.Close();

    //}
    //public MSStoredProceduresIBase(SqlConnection conn)
    //{
    //    if (conn != null)
    //    {
    //        this.conn = conn;
    //    }

    //} 

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

    //public int UpdatePlusIntValue(string table, string sloupecKUpdate, int pridej, params AB[] abc)
    //{
    //    return UpdatePlusIntValue(table, sloupecKUpdate, pridej, abc);
    //}
    #endregion

    #region Other SQL
    /// <summary>
    /// Vymže tabulku A1 a přejmenuje tabulku A1+"2" na A1
    /// </summary>
    /// <param name="table"></param>
    public void sp_rename(SqlData d, string table)
    {
        DropTableIfExists(SqlData.Empty, table);
        SqlCommand comm = new SqlCommand("EXEC sp_rename 'dbo." + table + "2', '" + table + "'");
        ExecuteNonQuery(SqlData.Empty, comm);
    }

    public SqlResult CreateDatabase(SqlData d, string p)
    {
        return ExecuteNonQuery(d, "Create Database [" + p + AllStrings.rsqb);
    }

    public SqlResult<bool> HasAnyValue(SqlData data, string table, string columnName, string iDColumnName, int idColumnValue)
    {

        var v = SelectCellDataTableStringOneRow(data, table, columnName, iDColumnName, idColumnValue);
        return InstancesSqlResult.Bool(v.result != "", v);
    } 
    #endregion

    #region DataTableTo*
    /// <summary>
    /// No SqlData/SqlResult
    /// </summary>
    /// <param name="dataTable"></param>
    /// <param name="dex"></param>
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
            vr.Add(item.ItemArray[dex].ToString().TrimEnd(AllChars.space));
        }
        return vr;
    }
    #endregion

    #region Delete*
    /// <summary>
    /// Maže všechny řádky, ne jen jeden.
    /// </summary>
    public SqlResult Delete(SqlData d, string table, string sloupec, object id)
    {
        return ExecuteNonQuery(d, string.Format("DELETE FROM {0} WHERE {1} = @p0", table, sloupec), id);
    }

    public SqlResult<DataTable> DeleteAllSmallerThanWithOutput(SqlData data, string TableName, string sloupceJezVratit, string nameColumnSmallerThan, object valueColumnSmallerThan, ABC whereIs, ABC whereIsNot)
    {
        ABC whereSmallerThan = new ABC(AB.Get(nameColumnSmallerThan, valueColumnSmallerThan));
        string whereS = GeneratorMsSql.CombinedWhere(whereIs, whereIsNot, null, whereSmallerThan);
        SqlCommand comm = new SqlCommand("DELETE FROM " + TableName + GeneratorMsSql.OutputDeleted(sloupceJezVratit) + whereS);
        AddCommandParameteresCombinedArrays(comm, 0, whereIs, whereIsNot, null, whereSmallerThan);
        var dt = SelectDataTable(data, comm);
        return dt;
    }

    
    public SqlResult<DataTable> DeleteWithOutput(SqlData data, string TableName, string sloupceJezVratit, string idColumn, object idValue)
    {
        SqlCommand comm = new SqlCommand("DELETE FROM " + TableName + GeneratorMsSql.OutputDeleted(sloupceJezVratit) + GeneratorMsSql.SimpleWhere(idColumn));
        AddCommandParameter(comm, 0, idValue);
        var dt = SelectDataTable(data, comm);
        return dt;
    }

    /// <summary>
    /// Conn nastaví automaticky
    /// Vrátí zda byl vymazán alespoň jeden řádek
    /// 
    /// </summary>
    /// <param name="TableName"></param>
    /// <param name="where"></param>
    public SqlResult Delete(SqlData d, string TableName, params AB[] where)
    {
        string whereS = GeneratorMsSql.CombinedWhere(new ABC(where));
        SqlCommand comm = new SqlCommand("DELETE FROM " + TableName + whereS);
        AddCommandParameterFromAbc(comm, where);
        var f = ExecuteNonQuery(d, comm);

        return f;
    }

    public SqlResult DeleteAllSmallerThan(SqlData d, string TableName, string nameColumnSmallerThan, object valueColumnSmallerThan, params AB[] where)
    {
        var whereAbc = new ABC(where);
        ABC whereSmallerThan = new ABC(AB.Get(nameColumnSmallerThan, valueColumnSmallerThan));
        string whereS = GeneratorMsSql.CombinedWhere(whereAbc, null, null, whereSmallerThan);
        SqlCommand comm = new SqlCommand("DELETE FROM " + TableName + whereS);
        AddCommandParameteresCombinedArrays(comm, 0, whereAbc, null, null, whereSmallerThan);
        var f = ExecuteNonQuery(d, comm);

        return f;
    }

    public SqlResult DeleteAllLargerThan(SqlData d, string TableName, string nameColumnLargerThan, object valueColumnLargerThan, params AB[] where)
    {
        var whereABC = new ABC(where);
        ABC whereSmallerThan = new ABC(AB.Get(nameColumnLargerThan, valueColumnLargerThan));
        string whereS = GeneratorMsSql.CombinedWhere(whereABC, null, null, whereSmallerThan);
        SqlCommand comm = new SqlCommand("DELETE FROM " + TableName + whereS);
        AddCommandParameteresCombinedArrays(comm, 0, whereABC, null, whereSmallerThan, null);
        var f = ExecuteNonQuery(d, comm);

        return f;
    }

    public SqlResult DeleteOneRow(SqlData d, string table, string sloupec, object id)
    {
        return ExecuteNonQuery(d, string.Format("DELETE TOP(1) FROM {0} WHERE {1} = @p0", table, sloupec), id);
    }

    public SqlResult<bool> DeleteOneRow(SqlData d, string TableName, params AB[] where)
    {
        string whereS = GeneratorMsSql.CombinedWhere(new ABC(where));
        SqlCommand comm = new SqlCommand("DELETE TOP(1) FROM " + TableName + whereS);
        AddCommandParameterFromAbc(comm, where);
        var f = ExecuteNonQuery(d, comm);

        return InstancesSqlResult.Bool(f.affectedRows != 0, f);
    }

    /// <summary>
    /// Pouýžívá se když chceš odstranit více řádků najednou pomocí AB. Nedá se použít pokud aspoň na jednom řádku potřebuješ AND
    /// </summary>
    /// <param name="p"></param>
    /// <param name="aB"></param>
    public SqlResult DeleteOR(SqlData d, string TableName, params AB[] where)
    {
        string whereS = GeneratorMsSql.CombinedWhereOR(new ABC(where));
        SqlCommand comm = new SqlCommand("DELETE FROM " + TableName + whereS);
        AddCommandParameterFromAbc(comm, where);
        var f = ExecuteNonQuery(d, comm);

        return f;
    } 
    #endregion

    #region Drop
    public List<SqlResult> DropAllTables(SqlData d)
    {
        List<SqlResult> results = new List<SqlResult>();
        var dd2 = SelectGetAllTablesInDB(d);

        results.Add(dd2);

        var dd = dd2.result;
        foreach (string item in dd)
        {
            results.Add(ExecuteNonQuery(d, new SqlCommand("DROP TABLE " + item)));
        }

        return results;
    }

    public SqlResult DropAndCreateTable(SqlData data, string p, Dictionary<string, MSColumnsDB> dictionary)
    {
        using (var conn = new SqlConnection(Cs))
        {
            conn.Open();
            //var connBackup = MSDatabaseLayer.conn;
            //MSDatabaseLayer.conn = conn;
            var result = DropAndCreateTable(data, p, dictionary, conn);
            //MSDatabaseLayer.conn = conn;
            conn.Close();
            return result;
        }
    }

    public SqlResult DropAndCreateTable(SqlData data, string p, Dictionary<string, MSColumnsDB> dictionary, SqlConnection conn)
    {
        if (dictionary.ContainsKey(p))
        {
            var result = DropTableIfExists(data, p);
            dictionary[p].GetSqlCreateTable(p, true, conn).ExecuteNonQuery();
            return result;
        }
        return null;
    }

    public SqlResult DropAndCreateTable(SqlData d, string p, MSColumnsDB msc)
    {
        using (var conn = new SqlConnection(Cs))
        {
            conn.Open();
            var result = DropTableIfExists(d, p);
            msc.GetSqlCreateTable(p, false, conn).ExecuteNonQuery();
            conn.Close();
            return result;
        }
    }

    public SqlResult DropAndCreateTable2(SqlData d, string p, Dictionary<string, MSColumnsDB> dictionary)
    {
        var cs = MSDatabaseLayer.cs;
        if (dictionary.ContainsKey(p))
        {
            var result = DropTableIfExists(d, p + "2");
            dictionary[p].GetSqlCreateTable(p + "2", cs).ExecuteNonQuery();
            return result;
        }
        return null;
    }

    public SqlResult DropTableIfExists(SqlData d, string table)
    {
        var result = SelectExistsTable(d, table);
        if (result.result)
        {
            return ExecuteNonQuery(d, new SqlCommand("DROP TABLE " + table));
        }
        return InstancesSqlResult.Empty;
    }
    #endregion

    #region Select*
    #region SelectCount*
    public SqlResult<int> SelectCountInt(SqlData data, string table)
    {
        var r = ExecuteScalar(data, "SELECT COUNT(*) FROM " + table);
        return InstancesSqlResult.Int(ToInt32(r), r);
    }

    public SqlResult<long> SelectCountOrMinusOne(SqlData data, string table)
    {
        var exists = SelectExistsTable(data, table);
        if (!exists.result)
        {
            return InstancesSqlResult.Long(-1, exists);
        }
        var ex = ExecuteScalar(data, "SELECT COUNT(*) FROM " + table);
        return InstancesSqlResult.Long(ToInt64(ex), ex);
    }

    public SqlResult<long> SelectCount(SqlData data, string table, params AB[] abc)
    {
        SqlCommand comm = new SqlCommand("SELECT COUNT(*) FROM " + table + GeneratorMsSql.CombinedWhere(abc));
        AddCommandParameteres(comm, 0, abc);
        var result = ExecuteScalar(data, comm);
        return InstancesSqlResult.Long(ToInt64(result), result);
    }

    public SqlResult<long> SelectCount(SqlData data, string table)
    {
        var r = ExecuteScalar(data, "SELECT COUNT(*) FROM " + table);
        return InstancesSqlResult.Long(ToInt64(r), r);
    }
    #endregion

    #region GroupBy
    public SqlResult<List<long>> SelectGroupByLong(SqlData data, string table, string GroupByColumn, params AB[] where)
    {
        string sql = "select " + TopDistinct(data) + GroupByColumn + " from " + table + GeneratorMsSql.CombinedWhere(where);
        SqlCommand comm = new SqlCommand(sql);
        //AddCommandParameter(comm, 0, IDColumnValue);
        AddCommandParameterFromAbc(comm, where);
        return ReadValuesLong(data, comm);
    }

    public SqlResult<List<int>> SelectGroupByInt(SqlData data, string table, string GroupByColumn, params AB[] where)
    {
        string sql = "select " + GroupByColumn + " from " + table + GeneratorMsSql.CombinedWhere(where);
        SqlCommand comm = new SqlCommand(sql);
        //AddCommandParameter(comm, 0, IDColumnValue);
        AddCommandParameterFromAbc(comm, where);
        return ReadValuesInt(data, comm);
    }

    /// <summary>
    /// Vrátí z řádků který je označen jako group by vždy jen 1 řádek
    /// </summary>
    /// <param name="signed"></param>
    /// <param name="table"></param>
    /// <param name="GroupByColumn"></param>
    /// <param name="IDColumnName"></param>
    /// <param name="IDColumnValue"></param>
    public SqlResult<List<short>> SelectGroupByShort(SqlData data, string table, string GroupByColumn, string IDColumnName, object IDColumnValue)
    {
        string sql = "select " + GroupByColumn + " from " + table + GeneratorMsSql.SimpleWhere(IDColumnName) + " group by " + GroupByColumn;
        SqlCommand comm = new SqlCommand(sql);
        AddCommandParameter(comm, 0, IDColumnValue);
        return ReadValuesShort(data, comm);
    }

    public SqlResult<List<long>> SelectGroupByLong(SqlData data, string table, string GroupByColumn, string IDColumnName, object IDColumnValue)
    {
        string sql = "select " + GroupByColumn + " from " + table + GeneratorMsSql.SimpleWhere(IDColumnName) + " group by " + GroupByColumn;
        SqlCommand comm = new SqlCommand(sql);
        AddCommandParameter(comm, 0, IDColumnValue);
        return ReadValuesLong(data, comm);
    }
    #endregion

    #region SelectSum*
    /// <summary>
    /// Zjištuje to ze všech řádků v databázi.
    /// Calculate sum of ViewCount of A2 in A1
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <param name="table"></param>
    /// <param name="idEntity"></param>
    public SqlResult<uint> SelectSumOfViewCount(SqlData data, string table, long idEntity)
    {
        SqlCommand comm = new SqlCommand("select SUM(ViewCount) from " + table + " where EntityID = @p0");
        //SqlCommand comm2 = new SqlCommand("select count(*) as AccValue from PageViews where Date <= @p0 AND Date >= @p1 AND IDPage = @p2");
        AddCommandParameter(comm, 0, idEntity);

        var o = ExecuteScalar(data, comm);
        if (IsNullOrDefault(o))
        {
            return InstancesSqlResult.Uint(0, o);
        }

        return InstancesSqlResult.Uint(ToUInt32(o), o);
    }

    public SqlResult<int> SelectSum(SqlData data, string table, string columnToSum, params AB[] aB)
    {
        //DataTable dt = SelectDataTableSelectiveCombination()
        var nt2 = SelectValuesOfColumnAllRowsInt(data, table, columnToSum, aB);
        List<int> nt = nt2.result;
        return InstancesSqlResult.Int(nt.Sum(), nt2);
    }

    /// <summary>
    /// Cells is byte but sum is int
    /// </summary>
    /// <param name="data"></param>
    /// <param name="table"></param>
    /// <param name="columnToSum"></param>
    /// <param name="aB"></param>
    public SqlResult<int> SelectSumByte(SqlData data, string table, string columnToSum, params AB[] aB)
    {
        int vr = 0;
        var nt2 = SelectValuesOfColumnAllRowsByte(data, table, columnToSum, aB);
        List<byte> nt = nt2.result;

        foreach (var item in nt)
        {
            vr += item;
        }
        return InstancesSqlResult.Int(vr, nt2);
    }
    #endregion

    #region SelectName*OfID*
    #region SelectNameOfID
    /// <summary>
    /// Vrátí SE, když nebude nalezena 
    /// </summary>
    public SqlResult<string> SelectNameOfID(SqlData data, string tabulka, long id)
    {
        return SelectCellDataTableStringOneRow(data, tabulka, "Name", "ID", id);
    }

    public SqlResult<string> SelectNameOfID(SqlData data, string tabulka, string nameColumnID, long id)
    {
        return SelectCellDataTableStringOneRow(data, tabulka, "Name", nameColumnID, id);
    }

    public SqlResult<string> SelectNameOfIDOrSE(SqlData data, string tabulka, string idColumnName, int id)
    {
        return SelectCellDataTableStringOneRow(data, tabulka, "Name", idColumnName, id);
    }
    #endregion

    #region SelectNamesOfID
    [NoReturnSqlResult]
    /// <summary>
    /// 
    /// </summary>
    public SqlResult<List<string>> SelectNamesOfIDs(SqlData data, string tabulka, List<int> idFces)
    {
        List<string> vr = new List<string>();
        foreach (int var in idFces)
        {
            vr.Add(SelectNameOfID(data, tabulka, var).result);
        }
        return InstancesSqlResult.ListString(vr, InstancesSqlResult.Empty);
    }
    #endregion
    #endregion

    #region SelectRowReader*
    public SqlResult<object[]> SelectRowReader(SqlData data, string tableName, string sloupce, string sloupecWhere, object hodnotaWhere)
    {
        SqlCommand comm = new SqlCommand("SELECT "+ TopDistinct(data)  + sloupce + " FROM " + tableName + GeneratorMsSql.SimpleWhere(sloupecWhere));
        AddCommandParameter(comm, 0, hodnotaWhere);
        return SelectRowReader(data, comm);
    }

    #region SelectSelective*
    /// <summary>
    /// Interně volá metodu SelectRowReader
    /// If fail, return null
    /// </summary>
    /// <param name="tabulka"></param>
    /// <param name="sloupecID"></param>
    /// <param name="id"></param>
    /// <param name="nazvySloupcu"></param>
    public SqlResult<object[]> SelectSelectiveOneRow(SqlData data, string tabulka, string nazvySloupcu, string sloupecID, object id)
    {
        SqlCommand comm = new SqlCommand(string.Format("SELECT TOP(1) {0} FROM {1} WHERE {2} = @p0", nazvySloupcu, tabulka, sloupecID));
        AddCommandParameter(comm, 0, id);
        //NT
        return SelectRowReader(data, comm);
    }

    /// <summary>
    /// Vrátí null pokud žádný takový řádek nebude nalezen
    /// </summary>
    /// <param name="table"></param>
    /// <param name="vratit"></param>
    /// <param name="ab"></param>
    public SqlResult<object[]> SelectSelectiveOneRow(SqlData data, string table, string vratit, params AB[] ab)
    {
        string sql = "SELECT TOP(1) " + vratit + " FROM " + table;
        sql += GeneratorMsSql.CombinedWhere(ab);
        SqlCommand comm = new SqlCommand(sql);
        AddCommandParameterFromAbc(comm, ab);
        return SelectRowReader(data, comm);
    }
    #endregion

    /// <summary>
    /// Vrátí null, pokud výsledek nebude mít žádné řádky
    /// </summary>
    /// <param name="comm"></param>
    public SqlResult<object[]> SelectRowReader(SqlData data, SqlCommand comm)
    {
        var r2 = ExecuteReader(data, comm);
        var r = r2.result;

        if (r.HasRows)
        {
            object[] o = new object[r.VisibleFieldCount];
            r.Read();
            for (int i = 0; i < r.VisibleFieldCount; i++)
            {
                o[i] = r.GetValue(i);
            }

            return InstancesSqlResult.ArrayObject(o, r2);
        }
        comm.Connection.Close();
        comm.Connection.Dispose();
        return InstancesSqlResult.ArrayObject(null, r2);
    }
    #endregion

    #region SelectExistsTable
    public SqlResult<bool> SelectExistsTable(SqlData data, string p)
    {
        using (var conn = new SqlConnection(Cs))
        {
            var dt = SelectDataTable(data, conn, string.Format("SELECT * FROM sysobjects WHERE id = object_id(N'{0}') AND OBJECTPROPERTY(id, N'IsUserTable') = 1", p));

            conn.Close();
            return InstancesSqlResult.Bool(dt.result.Rows.Count != 0, dt);
        }
    }

    public SqlResult<bool> SelectExistsTable(SqlData data, string p, SqlConnection conn)
    {
        var dt2 = SelectDataTable(data, conn, string.Format("SELECT * FROM sysobjects WHERE id = object_id(N'{0}') AND OBJECTPROPERTY(id, N'IsUserTable') = 1", p));
        var dt = dt2.result;
        return InstancesSqlResult.Bool(dt.Rows.Count != 0, dt2);
    }
    #endregion

    #region SelectExistsDatabase
    public SqlResult<bool> SelectExistsDatabase(SqlData d, string p)
    {
        var v = ExecuteScalar(d, new SqlCommand("select db_id('" + p + "');"));
        return InstancesSqlResult.Bool(TryParse.Integer.Instance.TryParseInt(v.result.ToString()), v);
    }
    #endregion

    #region SelectExistsCombination
    public SqlResult<bool> SelectExistsCombination(SqlData d, string p, params AB[] aB)
    {
        return SelectExistsCombination(d, p, new ABC(aB));
    }

    /// <summary>
    /// 
    /// </summary>
    public SqlResult<bool> SelectExistsCombination(SqlData data, string p, ABC aB)
    {
        string sql = string.Format("SELECT {0} FROM {1} {2}", aB[0].A, p, GeneratorMsSql.CombinedWhere(aB));
        ABC abc = new ABC(aB);
        var vr = ExecuteScalar(data, sql, abc.OnlyBs());
        return InstancesSqlResult.Bool(!IsNullOrDefault(vr), vr);
    }

    public SqlResult<bool> SelectExistsCombination(SqlData data, string p, ABC where, ABC whereIsNot)
    {
        int dd = 0;
        string sql = string.Format("SELECT {0} FROM {1} {2} {3}", where[0].A, p, GeneratorMsSql.CombinedWhere(where, ref dd), GeneratorMsSql.CombinedWhereNotEquals(true, ref dd, whereIsNot));
        int pridatNa = 0;
        SqlCommand comm = new SqlCommand(sql);

        foreach (var item in where)
        {
            pridatNa = AddCommandParameter(comm, pridatNa, item.B);
        }

        foreach (var item in whereIsNot)
        {
            pridatNa = AddCommandParameter(comm, pridatNa, item.B);
        }

        var sc = ExecuteScalar(data, comm);
        return InstancesSqlResult.Bool(!IsNullOrDefault(sc), sc);
    }
    #endregion

    #region SelectExists
    /// <summary>
    /// 
    /// </summary>
    public SqlResult<bool> SelectExists(SqlData data, string tabulka, string sloupec, object hodnota)
    {
        string sql = string.Format("SELECT TOP(1) {0} FROM {1} {2}", sloupec, tabulka, GeneratorMsSql.SimpleWhere(sloupec));
        var result = ExecuteScalar(data, sql, hodnota);

        return InstancesSqlResult.Bool(!IsNullOrDefault(result), result);
    } 
    #endregion

    #region SelectCellDataTableShortOneRow

    /// <summary>
    /// Exists method without AB but has switched whereColumn and whereValue
    /// </summary>
    /// <param name="signed"></param>
    /// <param name="table"></param>
    /// <param name="vracenySloupec"></param>
    /// <param name="abc"></param>
    public SqlResult<short> SelectCellDataTableShortOneRow(SqlData data, string table, string vracenySloupec, params AB[] abc)
    {
        string sql = GeneratorMsSql.SimpleSelectOneRow(vracenySloupec, table) + GeneratorMsSql.CombinedWhere(abc);
        SqlCommand comm = new SqlCommand(sql);
        AddCommandParameterFromAbc(comm, abc);
        return ExecuteScalarShort(data, comm);
    }

    /// <summary>
    /// V případě nenalezení vrátí -1 pokud !A1, jinak short.MaxValue
    /// </summary>
    /// <param name="table"></param>
    /// <param name="idColumnName"></param>
    /// <param name="idColumnValue"></param>
    /// <param name="vracenySloupec"></param>
    public SqlResult<short> SelectCellDataTableShortOneRow(SqlData data, string table, string vracenySloupec, string idColumnName, object idColumnValue)
    {
        string sql = GeneratorMsSql.SimpleWhereOneRow(vracenySloupec, table, idColumnName);
        SqlCommand comm = new SqlCommand(sql);
        AddCommandParameter(comm, 0, idColumnValue);

        return ExecuteScalarShort(data, comm);
    }
    #endregion

    #region SelectCellDataTableIntOneRow
    /// <summary>
    /// When not found, return int.MaxValue when A1 or -1 when not
    /// </summary>
    /// <param name="signed"></param>
    /// <param name="table"></param>
    /// <param name="vracenySloupec"></param>
    /// <param name="abc"></param>
    public SqlResult<int> SelectCellDataTableIntOneRow(SqlData data, string table, string vracenySloupec, params AB[] abc)
    {
        string sql = GeneratorMsSql.SimpleSelectOneRow(vracenySloupec, table) + GeneratorMsSql.CombinedWhere(abc);
        SqlCommand comm = new SqlCommand(sql);
        AddCommandParameterFromAbc(comm, abc);

        return ExecuteScalarInt(data, comm);
    }

    /// <summary>
    /// G -1 když se žádný takový řádek nepodaří najít
    /// </summary>
    /// <param name="table"></param>
    /// <param name="vracenySloupec"></param>
    /// <param name="abc"></param>
    public SqlResult<int> SelectCellDataTableIntOneRow(SqlData d, string table, string vracenySloupec, ABC whereIs, ABC whereIsNot)
    {
        string sql = GeneratorMsSql.SimpleSelectOneRow(vracenySloupec, table) + GeneratorMsSql.CombinedWhere(whereIs, whereIsNot, null, null);
        SqlCommand comm = new SqlCommand(sql);
        int dalsi = AddCommandParameterFromAbc(comm, 0, whereIs);
        AddCommandParameterFromAbc(comm, dalsi, whereIsNot);
        return ExecuteScalarInt(d, comm);
    }

    /// <summary>
    /// Vrátí -1 pokud žádný takový řádek nenalezne pokud !A1 enbo short.MaxValue pokud A1
    /// </summary>
    /// <param name="table"></param>
    /// <param name="idColumnName"></param>
    /// <param name="idColumnValue"></param>
    /// <param name="vracenySloupec"></param>
    public SqlResult<int> SelectCellDataTableIntOneRow(SqlData data, string table, string vracenySloupec, string idColumnName, object idColumnValue)
    {
        string sql = GeneratorMsSql.SimpleWhereOneRow(vracenySloupec, table, idColumnName);
        SqlCommand comm = new SqlCommand(sql);
        AddCommandParameter(comm, 0, idColumnValue);
        return ExecuteScalarInt(data, comm);
    }
    #endregion

    #region SelectCellDataTableLongOneRow
    /// <summary>
    /// Vrací -1 pokud se nepodaří najít if !A1 nebo long.MaxValue když A1
    /// </summary>
    /// <param name="signed"></param>
    /// <param name="table"></param>
    /// <param name="vracenySloupec"></param>
    /// <param name="abc"></param>
    public SqlResult<long> SelectCellDataTableLongOneRow(SqlData data, string table, string vracenySloupec, params AB[] abc)
    {
        string sql = GeneratorMsSql.SimpleSelectOneRow(vracenySloupec, table) + GeneratorMsSql.CombinedWhere(abc);
        SqlCommand comm = new SqlCommand(sql);
        AddCommandParameterFromAbc(comm, abc);
        return ExecuteScalarLong(data, comm);
    }

    /// <summary>
    /// Vrací -1 pokud se nepodaří najít if !A1 nebo long.MaxValue když A1.
    /// </summary>
    /// <param name="table"></param>
    /// <param name="idColumnName"></param>
    /// <param name="idColumnValue"></param>
    /// <param name="vracenySloupec"></param>
    public SqlResult<long> SelectCellDataTableLongOneRow(SqlData data, string table, string vracenySloupec, string idColumnName, object idColumnValue)
    {
        string sql = GeneratorMsSql.SimpleWhereOneRow(vracenySloupec, table, idColumnName);
        SqlCommand comm = new SqlCommand(sql);
        AddCommandParameter(comm, 0, idColumnValue);
        data.signed = true;
        return ExecuteScalarLong(data, comm);
    }
    #endregion

    #region SelectCellDataTableDateTimeOneRow
    public SqlResult<DateTime> SelectCellDataTableDateTimeOneRow(SqlData data, string table, string vracenySloupec, string idColumnName, object idColumnValue)
    {
        string sql = GeneratorMsSql.SimpleWhereOneRow(vracenySloupec, table, idColumnName);
        SqlCommand comm = new SqlCommand(sql);
        AddCommandParameter(comm, 0, idColumnValue);
        return ExecuteScalarDateTime(data, data.getIfNotFound, comm);
    }

    /// <summary>
    /// Vrátí DateTimeMinVal, pokud takový řádek nebude nalezen.
    /// </summary>
    /// <param name="table"></param>
    /// <param name="vracenySloupec"></param>
    /// <param name="where"></param>
    /// <param name="whereIsNot"></param>
    public SqlResult<DateTime> SelectCellDataTableDateTimeOneRow(SqlData data, string table, string vracenySloupec, ABC where, ABC whereIsNot)
    {
        int dd = 0;
        string sql = GeneratorMsSql.SimpleSelectOneRow(vracenySloupec, table) + GeneratorMsSql.CombinedWhere(where, ref dd) + GeneratorMsSql.CombinedWhereNotEquals(true, ref dd, whereIsNot);
        SqlCommand comm = new SqlCommand(sql);
        //AddCommandParameter(comm, 0, idColumnValue);
        AddCommandParameteresArrays(comm, 0, where, whereIsNot);
        return ExecuteScalarDateTime(data, data.getIfNotFound, comm);
    }
    #endregion

    #region SelectCellDataTableNullableBoolOneRow
    /// <summary>
    /// Vrátí null pokud se řádek nepodaří najít
    /// A3 nebo A4 může být null
    /// </summary>
    /// <param name="table"></param>
    /// <param name="vracenySloupec"></param>
    /// <param name="where"></param>
    /// <param name="whereIsNot"></param>
    public SqlResult<bool?> SelectCellDataTableNullableBoolOneRow(SqlData data, string table, string vracenySloupec, ABC where, ABC whereIsNot)
    {
        int dd = 0;
        string sql = GeneratorMsSql.SimpleSelectOneRow(vracenySloupec, table) + GeneratorMsSql.CombinedWhere(where, ref dd) + GeneratorMsSql.CombinedWhereNotEquals(true, ref dd, whereIsNot);
        SqlCommand comm = new SqlCommand(sql);
        AddCommandParameteresArrays(comm, 0, where, whereIsNot);
        return ExecuteScalarNullableBool(data, comm);
    }

    public SqlResult<bool?> SelectCellDataTableNullableBoolOneRow(SqlData d, string table, string vracenySloupec, params AB[] abc)
    {
        string sql = "SELECT TOP(1) " + vracenySloupec + " FROM " + table + " " + GeneratorMsSql.CombinedWhere(abc);
        SqlCommand comm = new SqlCommand(sql);
        AddCommandParameteres(comm, 0, abc);
        var result = ExecuteScalarNullableBool(d, comm);
        return result;
    }
    #endregion

    #region SelectCellDataTableBoolOneRow
    public SqlResult<bool> SelectCellDataTableBoolOneRow(SqlData data, string table, string vracenySloupec, ABC where, ABC whereIsNot)
    {
        int dd = 0;
        string sql = GeneratorMsSql.SimpleSelectOneRow(vracenySloupec, table) + GeneratorMsSql.CombinedWhere(where, ref dd) + GeneratorMsSql.CombinedWhereNotEquals(true, ref dd, whereIsNot);
        SqlCommand comm = new SqlCommand(sql);
        AddCommandParameteresArrays(comm, 0, where, whereIsNot);
        return ExecuteScalarBool(data, comm);
    }

    public SqlResult<bool> SelectCellDataTableBoolOneRow(SqlData data, string table, string vracenySloupec, string idColumnName, object idColumnValue)
    {
        string sql = GeneratorMsSql.SimpleWhereOneRow(vracenySloupec, table, idColumnName);
        SqlCommand comm = new SqlCommand(sql);
        AddCommandParameter(comm, 0, idColumnValue);
        return ExecuteScalarBool(data, comm);
    }
    #endregion

    #region SelectCellDataTableByteOneRow
    /// <summary>
    /// Vrátí 0 pokud takový řádek nebude nalezen.
    /// </summary>
    public SqlResult<byte> SelectCellDataTableByteOneRow(SqlData data, string table, string vracenySloupec, params AB[] where)
    {
        string sql = GeneratorMsSql.SimpleSelectOneRow(vracenySloupec, table);
        sql += GeneratorMsSql.CombinedWhere(where);
        SqlCommand comm = new SqlCommand(sql);
        AddCommandParameterFromAbc(comm, where);
        return ExecuteScalarByte(data, comm);
    }

    /// <summary>
    /// Vrátí 0 pokud takový řádek nebude nalezen.
    /// </summary>
    /// <param name="table"></param>
    /// <param name="idColumnName"></param>
    /// <param name="idColumnValue"></param>
    /// <param name="vracenySloupec"></param>
    public SqlResult<byte> SelectCellDataTableByteOneRow(SqlData data, string table, string vracenySloupec, string idColumnName, object idColumnValue)
    {
        string sql = GeneratorMsSql.SimpleWhereOneRow(vracenySloupec, table, idColumnName);
        SqlCommand comm = new SqlCommand(sql);
        AddCommandParameter(comm, 0, idColumnValue);
        return ExecuteScalarByte(data, comm);
    } 
    #endregion

    #region SelectCellDataTableFloatOneRow
    /// <summary>
    /// Vrátí -1 když řádek nebude nalezen a !A1.
    /// Vrátí float.MaxValue když řádek nebude nalezen a A1.
    /// </summary>
    /// <param name="signed"></param>
    /// <param name="table"></param>
    /// <param name="idColumnName"></param>
    /// <param name="idColumnValue"></param>
    /// <param name="vracenySloupec"></param>
    public SqlResult<float> SelectCellDataTableFloatOneRow(SqlData data, string table, string vracenySloupec, string idColumnName, object idColumnValue)
    {
        string sql = GeneratorMsSql.SimpleWhereOneRow(vracenySloupec, table, idColumnName);
        SqlCommand comm = new SqlCommand(sql);
        AddCommandParameter(comm, 0, idColumnValue);
        return ExecuteScalarFloat(data, comm);
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
    public SqlResult<float> SelectCellDataTableFloatOneRow(SqlData data, string table, string vracenySloupec, params AB[] ab)
    {
        string sql = GeneratorMsSql.CombinedWhere(table, true, vracenySloupec,  ab);
        SqlCommand comm = new SqlCommand(sql);
        AddCommandParameterFromAbc(comm, ab);
        return ExecuteScalarFloat(data, comm);
    }
    #endregion

    #region SelectCellDataTableObjectOneRow
    public SqlResult<object> SelectCellDataTableObjectOneRow(SqlData data, string table, string vracenySloupec, string idColumnName, object idColumnValue)
    {
        string sql = GeneratorMsSql.SimpleWhereOneRow(vracenySloupec, table, idColumnName);
        SqlCommand comm = new SqlCommand(sql);
        AddCommandParameter(comm, 0, idColumnValue);
        return ExecuteScalar(data, comm);
    } 
    #endregion

    #region SelectCellDataTableStringOneRow
    /// <summary>
    /// Vykonává metodou ExecuteScalar. Ta pokud vrátí null, metoda vrátí "". To je taky rozdíl oproti metodě SelectCellDataTableStringOneRowABC.
    /// </summary>
    /// <param name="tabulka"></param>
    /// <param name="nazvySloupcu"></param>
    /// <param name="ab"></param>
    public SqlResult<string> SelectCellDataTableStringOneRow(SqlData data, string tabulka, string nazvySloupcu, params AB[] ab)
    {
        SqlCommand comm = new SqlCommand(GeneratorMsSql.CombinedWhere(tabulka, true, nazvySloupcu, ab));
        AddCommandParameterFromAbc(comm, ab);
        return ExecuteScalarString(data, comm);
    }

    /// <summary>
    /// Vrátí SE v případě že řádek nebude nalezen, nikdy nevrací null.
    /// Automaticky vytrimuje
    /// </summary>
    /// <param name="table"></param>
    /// <param name="idColumnName"></param>
    /// <param name="idColumnValue"></param>
    /// <param name="vracenySloupec"></param>
    public SqlResult<string> SelectCellDataTableStringOneRow(SqlData data, string table, string vracenySloupec, string idColumnName, object idColumnValue)
    {
        string sql = GeneratorMsSql.SimpleWhereOneRow(vracenySloupec, table, idColumnName);
        SqlCommand comm = new SqlCommand(sql);
        AddCommandParameter(comm, 0, idColumnValue);
        return ExecuteScalarString(data, comm);
    }

    public SqlResult<string> SelectCellDataTableStringOneRow(SqlData data, string table, string vracenySloupec, ABC where, ABC whereIsNot)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("SELECT TOP(1) " + vracenySloupec);
        sb.Append(" FROM " + table);
        int dd = 0;
        sb.Append(GeneratorMsSql.CombinedWhere(where, ref dd));

        sb.Append(GeneratorMsSql.CombinedWhereNotEquals(where.Length != 0, ref dd, whereIsNot));
        //string sql = GeneratorMsSql.SimpleWhereOneRow(vracenySloupec, table, idColumnName);
        SqlCommand comm = new SqlCommand(sb.ToString());
        AddCommandParameteresCombinedArrays(comm, 0, where, whereIsNot, null, null);

        return ExecuteScalarString(data, comm);
    } 
    #endregion
    public SqlResult<string> SelectCellDataTableStringOneLastRow(SqlData data, string table, string vracenySloupec, string idColumnName, object idColumnValue)
    {
        //SELECT TOP 1 * FROM table_Name ORDER BY unique_column DESC
        string sql = GeneratorMsSql.SimpleWhereOneRow(vracenySloupec, table, idColumnName);
        SqlCommand comm = new SqlCommand(sql + string.Format(" ORDER BY {0} DESC", data.orderBy));
        AddCommandParameter(comm, 0, idColumnValue);
        return ExecuteScalarString(data, comm);
    }

    #region SelectDataTableAllRows

    /// <summary>
    /// 
    /// </summary>
    /// <param name="TableName"></param>
    /// <param name="whereSloupec"></param>
    /// <param name="whereValue"></param>
    public SqlResult<DataTable> SelectDataTableAllRows(SqlData data, string TableName, string whereSloupec, object whereValue)
    {
        
        SqlCommand comm = new SqlCommand(string.Format("SELECT * FROM {0} {1}", TableName, GeneratorMsSql.SimpleWhere(whereSloupec)) + OrderBy(data));
        AddCommandParameter(comm, 0, whereValue);
        //NTd
        return this.SelectDataTable(data, comm);
    }

    /// <summary>
    /// 
    /// </summary>
    public SqlResult<DataTable> SelectDataTableAllRows(SqlData data, string table)
    {
        return SelectDataTable(data, "SELECT * FROM " + table);
    }
    #endregion

    #region SelectValuesOfColumnAllRows {Type}

    /// <summary>
    /// POkud bude v DB hodnota DBNull.Value, vrátí se -1
    /// </summary>
    /// <param name="data"></param>
    /// <param name="tabulka"></param>
    /// <param name="sloupec"></param>
    /// <param name="idColumn"></param>
    /// <param name="idValue"></param>
    public SqlResult<List<short>> SelectValuesOfColumnAllRowsShort(SqlData data, string tabulka, string sloupec, string idColumn, object idValue)
    {
        SqlCommand comm = new SqlCommand(string.Format("SELECT " + TopDistinct(data) + "{0} FROM {1} WHERE {2} = @p0", sloupec, tabulka, idColumn));
        AddCommandParameter(comm, 0, idValue);
        return ReadValuesShort(data, comm);
    }

    public SqlResult<List<string>> SelectValuesOfColumnAllRowsString(SqlData d, string tabulka, string sloupec, params AB[] aB)
    {
        return SelectValuesOfColumnAllRowsString(d, tabulka, sloupec, new ABC(aB), null);
    }

    public SqlResult<List<string>> SelectValuesOfColumnAllRowsString(SqlData data, string tabulka, string sloupec, string whereSloupec, object whereHodnota)
    {

        if (data.orderBy != string.Empty)
        {
            data.orderBy = " " + data.orderBy;
        }
        SqlCommand comm = new SqlCommand(string.Format("SELECT {0} FROM {1}", sloupec, tabulka) + GeneratorMsSql.SimpleWhere(whereSloupec) + data.orderBy);
        AddCommandParameter(comm, 0, whereHodnota);
        return ReadValuesString(data, comm);
    }

    public SqlResult<List<string>> SelectValuesOfColumnAllRowsString(SqlData data, string tabulka, string sloupec, ABC where, ABC whereIsNot)
    {
        if (data.orderBy != string.Empty)
        {
            data.orderBy = " " + data.orderBy;
        }
        SqlCommand comm = new SqlCommand(string.Format("SELECT {0} FROM {1}", sloupec, tabulka) + GeneratorMsSql.CombinedWhere(where, whereIsNot, null, null) + data.orderBy);
        AddCommandParameteresCombinedArrays(comm, 0, where, whereIsNot, null, null);
        return ReadValuesString(data, comm);
    }

    /// <summary>
    /// POkud bude v DB hodnota DBNull.Value, vrátí se -1
    /// </summary>
    /// <param name="tabulka"></param>
    /// <param name="sloupec"></param>
    public SqlResult<List<int>> SelectValuesOfColumnAllRowsInt(SqlData data, string tabulka, string sloupec)
    {
        SqlCommand comm = new SqlCommand(string.Format("SELECT {0} FROM {1}", sloupec, tabulka));
        return ReadValuesInt(data, comm);
    }

    public SqlResult<IList> SelectValuesOfColumnAllRowsNumeric(SqlData data, string tabulka, string sloupec, params AB[] ab)
    {
        SqlCommand comm = new SqlCommand(string.Format("SELECT TOP(1) {0} FROM {1}", sloupec, tabulka));
        var o2 = SelectRowReader(data, comm);
        var o = o2.result;
        if (IsNullOrDefault<object[]>(o2))
        {
            return InstancesSqlResult.List(o);
        }
        Type t = o[0].GetType();
        comm = new SqlCommand(string.Format("SELECT {0} FROM {1}", sloupec, tabulka) + GeneratorMsSql.CombinedWhere(new ABC(ab)));
        AddCommandParameteres(comm, 0, ab);

        SqlResult<IList> result = null;

        if (t == Types.tInt)
        {
            //snt = SqlNumericType.Int;

            result = new SqlResult<IList>(ReadValuesInt(data, comm));
        }
        else if (t == Types.tLong)
        {
            //snt = SqlNumericType.Long;
            result = new SqlResult<IList>(ReadValuesLong(data, comm));
        }
        else if (t == Types.tShort)
        {
            //snt = SqlNumericType.Short;
            result = new SqlResult<IList>(ReadValuesShort(data, comm));
        }
        else
        {
            result = new SqlResult<IList>(ReadValuesByte(data, comm));
        }

        return result;
    }

    public SqlResult<IList> SelectValuesOfColumnAllRowsNumeric(SqlData data, string tabulka, string sloupec)
    {
        return SelectValuesOfColumnAllRowsNumeric(data, tabulka, sloupec, new AB[0]);
    }

    /// <summary>
    /// POkud bude v DB hodnota DBNull.Value, vrátí se -1
    /// </summary>
    /// <param name="tabulka"></param>
    /// <param name="sloupec"></param>
    public SqlResult<List<short>> SelectValuesOfColumnAllRowsShort(SqlData data, string tabulka, string sloupec)
    {
        SqlCommand comm = new SqlCommand(string.Format("SELECT {0} FROM {1}", sloupec, tabulka));
        return ReadValuesShort(data, comm);
    }

   

    public SqlResult<List<int>> SelectValuesOfColumnAllRowsInt(SqlData data, string tabulka, string hledanySloupec, params AB[] aB)
    {
        string hodnoty = MSDatabaseLayer.GetValues(aB.ToArray());

        SqlCommand comm = new SqlCommand(string.Format("SELECT TOP(" + data.limit + ") {0} FROM {1} {2}", hledanySloupec, tabulka, GeneratorMsSql.CombinedWhere(new ABC(aB))));
        for (int i = 0; i < aB.Length; i++)
        {
            AddCommandParameter(comm, i, aB[i].B);
        }
        return ReadValuesInt(data, comm);
    }

    public SqlResult<List<short>> SelectValuesOfColumnAllRowsShort(SqlData data, string tabulka, string sloupec, ABC whereIs, ABC whereIsNot)
    {
        SqlCommand comm = new SqlCommand(string.Format("SELECT {0} FROM {1}", sloupec, tabulka) + GeneratorMsSql.CombinedWhere(whereIs, whereIsNot, null, null));
        AddCommandParameteresCombinedArrays(comm, 0, whereIs, whereIsNot, null, null);
        return ReadValuesShort(data, comm);
    }
    public SqlResult<List<int>> SelectValuesOfColumnAllRowsInt(SqlData data, string tabulka, string sloupec, ABC whereIs, ABC whereIsNot)
    {
        SqlCommand comm = new SqlCommand(string.Format("SELECT {0} FROM {1}", sloupec, tabulka) + GeneratorMsSql.CombinedWhere(whereIs, whereIsNot, null, null));
        AddCommandParameteresCombinedArrays(comm, 0, whereIs, whereIsNot, null, null);
        return ReadValuesInt(data, comm);
    }

    /// <summary>
    /// After adding SqlData I have two differenet method with same header
    /// SelectValuesOfColumnAllRowsIntWorker - added worker
    /// SelectValuesOfColumnAllRowsIntDate - added Date
    /// </summary>
    /// <param name="data"></param>
    /// <param name="tabulka"></param>
    /// <param name="sloupec"></param>
    /// <param name="dnuPozpatku"></param>
    /// <param name="whereIs"></param>
    /// <param name="whereIsNot"></param>
    public SqlResult<List<int>> SelectValuesOfColumnAllRowsIntDate(SqlData data, string tabulka, string sloupec, ABC whereIs, ABC whereIsNot)
    {
        var dateTime = DateTime.Today;
        if (data.dnuPozpatku.HasValue)
        {
            dateTime = dateTime.AddDays(data.dnuPozpatku.Value * -1);
        }


        ABC lowerThanWhere = new ABC(new ABC(AB.Get("Day", dateTime)));
        SqlCommand comm = new SqlCommand(string.Format("SELECT {0} FROM {1}", sloupec, tabulka) + GeneratorMsSql.CombinedWhere(whereIs, whereIsNot, lowerThanWhere, null));
        AddCommandParameteresCombinedArrays(comm, 0, whereIs, whereIsNot, lowerThanWhere, null);
        return ReadValuesInt(data, comm);
    }

    /// <summary>
    /// After adding SqlData I have two differenet method with same header
    /// SelectValuesOfColumnAllRowsIntWorker - added worker
    /// SelectValuesOfColumnAllRowsIntDate - added Date
    /// </summary>
    /// <param name="data"></param>
    /// <param name="tabulka"></param>
    /// <param name="sloupec"></param>
    /// <param name="maxRows"></param>
    /// <param name="whereIs"></param>
    /// <param name="whereIsNot"></param>
    public SqlResult<List<int>> SelectValuesOfColumnAllRowsIntWorker(SqlData data, string tabulka, string sloupec, ABC whereIs, ABC whereIsNot)
    {
        SqlCommand comm = new SqlCommand(string.Format("SELECT TOP({2}) {0} FROM {1}", sloupec, tabulka, data.limit) + GeneratorMsSql.CombinedWhere(whereIs, whereIsNot, null, null));
        SqlOperations.AddCommandParameteresCombinedArrays(comm, 0, whereIs, whereIsNot, null, null);
        return ReadValuesInt(data, comm);
    }

    public SqlResult<List<int>> SelectValuesOfColumnAllRowsInt(SqlData data, string tabulka, string sloupec, ABC whereIs, ABC whereIsNot, ABC greaterThanWhere, ABC lowerThanWhere)
    {
        SqlCommand comm = new SqlCommand(string.Format("SELECT {0} FROM {1}", sloupec, tabulka) + GeneratorMsSql.CombinedWhere(whereIs, whereIsNot, greaterThanWhere, lowerThanWhere));
        AddCommandParameteresCombinedArrays(comm, 0, whereIs, whereIsNot, greaterThanWhere, lowerThanWhere);
        return ReadValuesInt(data, comm);
    }

    /// <summary>
    /// POkud bude v DB hodnota DBNull.Value, vrátí se -1
    /// </summary>
    /// <param name="tabulka"></param>
    /// <param name="sloupec"></param>
    public SqlResult<List<short>> SelectValuesOfColumnAllRowsShort(SqlData d, string tabulka, string sloupec, params AB[] ab)
    {
        SqlCommand comm = new SqlCommand(string.Format("SELECT " + TopDistinct(d) + " {0} FROM {1} {2}", sloupec, tabulka, GeneratorMsSql.CombinedWhere(new ABC(ab))));
        AddCommandParameterFromAbc(comm, ab);
        return ReadValuesShort(d, comm);
    }

    /// <summary>
    /// Jakékoliv změny zde musíš provést i v metodě SelectValuesOfColumnAllRowsStringTrim
    /// </summary>
    public SqlResult<List<string>> SelectValuesOfColumnAllRowsString(SqlData d, string tabulka, string sloupec)
    {
        SqlCommand comm = new SqlCommand(string.Format("SELECT {0} FROM {1}", sloupec, tabulka));
        return ReadValuesString(d, comm);
    }

    public SqlResult<List<string>> SelectValuesOfColumnAllRowsStringTrim(SqlData data, string tabulka, string sloupec, string idn, object idv)
    {
        SqlCommand comm = new SqlCommand(string.Format("SELECT {0} FROM {1}", sloupec, tabulka) + GeneratorMsSql.SimpleWhere(idn));
        AddCommandParameter(comm, 0, idv);
        return ReadValuesStringTrim(data, comm);
    }

    public SqlResult<List<int>> SelectValuesOfColumnAllRowsInt(SqlData data, string tabulka, string sloupec, string idColumn, object idValue)
    {
        SqlCommand comm = new SqlCommand(string.Format("SELECT TOP(" + data.limit + ") {0} FROM {1} WHERE {2} = @p0", sloupec, tabulka, idColumn));
        AddCommandParameter(comm, 0, idValue);
        return ReadValuesInt(data, comm);
    }
    public SqlResult<List<long>> SelectValuesOfColumnAllRowsLong(SqlData data, string tabulka, string hledanySloupec, params AB[] aB)
    {
        string hodnoty = MSDatabaseLayer.GetValues(aB.ToArray());

        SqlCommand comm = new SqlCommand(string.Format("SELECT {0} FROM {1} {2}", hledanySloupec, tabulka, GeneratorMsSql.CombinedWhere(new ABC(aB))));
        for (int i = 0; i < aB.Length; i++)
        {
            AddCommandParameter(comm, i, aB[i].B);
        }
        return ReadValuesLong(data, comm);
    }

    /// <summary>
    /// Pokud bude buňka DBNull, nebudu ukládat do G nic
    /// </summary>
    /// <param name="table"></param>
    /// <param name="returnColumns"></param>
    /// <param name="where"></param>
    public SqlResult<List<DateTime>> SelectValuesOfColumnAllRowsDateTime(SqlData data, string table, string returnColumns, params AB[] where)
    {
        string hodnoty = MSDatabaseLayer.GetValues(where.ToArray());

        List<DateTime> vr = new List<DateTime>();
        SqlCommand comm = new SqlCommand(string.Format("SELECT {0} FROM {1} {2}", returnColumns, table, GeneratorMsSql.CombinedWhere(new ABC(where))));
        for (int i = 0; i < where.Length; i++)
        {
            AddCommandParameter(comm, i, where[i].B);
        }
        return ReadValuesDateTime(data, comm);
    }

    /// <summary>
    /// Pokud řádek ve sloupci A2 má hodnotu DBNull.Value, zapíšu do výsledku 0
    /// </summary>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    /// <param name="aB1"></param>
    /// <param name="aB2"></param>
    public SqlResult<List<byte>> SelectValuesOfColumnAllRowsByte(SqlData data, string tabulka, string hledanySloupec, object vetsiNez, object mensiNez, params AB[] aB)
    {
        string hodnoty = MSDatabaseLayer.GetValues(aB.ToArray());
        //"OrderVerse"
        SqlCommand comm = new SqlCommand(string.Format("SELECT {0} FROM {1} {2}", hledanySloupec, tabulka, GeneratorMsSql.CombinedWhere(new ABC(aB), null, new ABC(AB.Get(hledanySloupec, vetsiNez)), new ABC(AB.Get(hledanySloupec, mensiNez)))));
        int i = 0;
        for (; i < aB.Length; i++)
        {
            AddCommandParameter(comm, i, aB[i].B);
        }
        i = AddCommandParameter(comm, i, vetsiNez);
        i = AddCommandParameter(comm, i, mensiNez);
        return ReadValuesByte(data, comm);
    }

    /// <summary>
    /// 
    /// Jakékoliv změny zde musíš provést i v metodě SelectValuesOfColumnAllRowsString
    /// </summary>
    /// <param name="tabulka"></param>
    /// <param name="sloupec"></param>
    public SqlResult<List<string>> SelectValuesOfColumnAllRowsStringTrim(SqlData data, string tabulka, string sloupec)
    {
        //List<string> vr = new List<string>();
        SqlCommand comm = new SqlCommand(string.Format("SELECT {0} FROM {1}", sloupec, tabulka));
        return ReadValuesStringTrim(data, comm);
    }

    public SqlResult<List<byte>> SelectValuesOfColumnAllRowsByte(SqlData data, string tabulka, string sloupec, params AB[] ab)
    {
        //List<byte> vr = new List<byte>();
        SqlCommand comm = new SqlCommand(string.Format("SELECT TOP(" + data. limit + ") {0} FROM {1}", sloupec, tabulka + GeneratorMsSql.CombinedWhere(ab)));
        AddCommandParameteres(comm, 0, ab);
        return ReadValuesByte(data, comm);
    }
    #endregion

    #region SelectDataTable*

    public SqlResult<DataTable> SelectDataTableGroupBy(SqlData data, string table, string columns)
    {
        string sql = "select " + TopDistinct(data)  + columns + " from " + table;
        GroupBy(data, ref sql);
        SqlCommand comm = new SqlCommand(sql);
        //AddCommandParameter(comm, 0, IDColumnValue);
        return SelectDataTable(data, comm);
    }

    /// <summary>
    /// Tato metoda má přívlastek Columns protože v ní jde specifikovat sloupce které má metoda vrátit
    /// </summary>
    /// <param name="tableName"></param>
    /// <param name="limit"></param>
    /// <param name="sloupce"></param>
    /// <param name="sloupecWhere"></param>
    /// <param name="hodnotaWhere"></param>
    public SqlResult<DataTable> SelectDataTableColumns(SqlData data, string tableName, string sloupce, string sloupecWhere, object hodnotaWhere)
    {
        SqlCommand comm = new SqlCommand("SELECT TOP(" + data.limit.ToString() + ") " + sloupce + " FROM " + tableName + GeneratorMsSql.SimpleWhere(sloupecWhere));
        AddCommandParameter(comm, 0, hodnotaWhere);
        return SelectDataTable(data, comm);
    }

    #region SelectDataTable
    /// <summary>
    /// Conn nastaví automaticky
    /// </summary>
    /// <param name="sql"></param>
    public SqlResult<DataTable> SelectDataTable(SqlData data, SqlCommand comm)
    {
        using (var conn = new SqlConnection(Cs))
        {
            conn.Open();
            var dt = SelectDataTable(data, conn, comm);

            conn.Close();
            return dt;
        }
    }

    /// <summary>
    /// A1 jsou hodnoty bez převedení AddCommandParameter nebo ReplaceValueOnlyOne
    /// Conn nastaví automaticky
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="_params"></param>
    public SqlResult<DataTable> SelectDataTable(SqlData data, string sql, params object[] _params)
    {
        SqlCommand comm = new SqlCommand(sql);
        for (int i = 0; i < _params.Length; i++)
        {
            AddCommandParameter(comm, i, _params[i]);
        }
        return SelectDataTable(data, comm);
        //return SelectDataTable(string.Format(sql, _params));
    }

    public SqlResult<DataTable> SelectDataTable(SqlData data, SqlConnection conn, string sql, params object[] _params)
    {
        SqlCommand comm = new SqlCommand(sql);
        for (int i = 0; i < _params.Length; i++)
        {
            AddCommandParameter(comm, i, _params[i]);
        }
        return SelectDataTable(data, conn, comm);
        //return SelectDataTable(string.Format(sql, _params));
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="conn"></param>
    /// <param name="comm"></param>
    public SqlResult<DataTable> SelectDataTable(SqlData d, SqlConnection conn, SqlCommand comm)
    {
        DataTable dt = new DataTable();
        comm.Connection = conn;
        SqlDataAdapter adapter = new SqlDataAdapter(comm);
        adapter.Fill(dt);

        return InstancesSqlResult.DataTable(dt); ;
    }
    #endregion
    #endregion

    #region Select from database object
    /// <summary>
    /// 
    /// </summary>
    public SqlResult<List<string>> SelectGetAllTablesInDB(SqlData data)
    {
        List<string> vr = new List<string>();
        var dt2 = SelectDataTableSelective(data, "INFORMATION_SCHEMA.TABLES", "TABLE_NAME", "TABLE_TYPE", "BASE TABLE");
        var dt = dt2.result;
        foreach (DataRow item in dt.Rows)
        {
            vr.Add(item.ItemArray[0].ToString());
        }
        return InstancesSqlResult.ListString(vr, dt2);
    }

    /// <summary>
    /// 
    /// </summary>
    public SqlResult<List<string>> SelectColumnsNamesOfTable(SqlData data, string p)
    {
        List<string> vr = new List<string>();

        var dt2 = SelectDataTableSelective(data, "INFORMATION_SCHEMA.COLUMNS", "COLUMN_NAME", "TABLE_NAME", p);
        var dt = dt2.result;
        foreach (DataRow item in dt.Rows)
        {
            vr.Add(item.ItemArray[0].ToString());
        }
        return InstancesSqlResult.ListString(vr, dt2);
    } 
    #endregion

    #region SelectDataTableSelective
    /// <summary>
    /// Pokud chceš použít OrderBy, je tu metoda SelectDataTableLastRows nebo SelectDataTableLastRowsInnerJoin
    /// Conn nastaví automaticky
    /// Vrátí prázdnou tabulku pokud se nepodaří žádný řádek najít
    /// Vyplň A2 na SE pokud chceš všechny sloupce
    /// </summary>
    public SqlResult<DataTable> SelectDataTableSelective(SqlData data, string tabulka, string nazvySloupcu, params AB[] ab)
    {
        var com = string.Format("SELECT " + TopDistinct(data) + " {0} FROM {1}", nazvySloupcu, tabulka) + GeneratorMsSql.CombinedWhere(new ABC(ab));
        AddOrderBy(data, ref com);

        SqlCommand comm = new SqlCommand(com);
        
        AddCommandParameterFromAbc(comm, ab);
        //NT
        return this.SelectDataTable(data, comm);
    }

    public SqlResult<DataTable> SelectDataTableSelective(SqlData data, string tabulka, string nazvySloupcu, string sloupecID, object id)
    {
        string cmd = string.Format("SELECT {0} FROM {1} WHERE {2} = @p0", nazvySloupcu, tabulka, sloupecID);
        
        AddOrderBy(data, ref cmd);

        SqlCommand comm = new SqlCommand(cmd);
        AddCommandParameter(comm, 0, id);
        //NT
        return this.SelectDataTable(data, comm);
    }

    private static string AddOrderBy(SqlData data, ref string cmd)
    {
        if (data.orderBy != string.Empty)
        {
            cmd += GeneratorMsSql.OrderBy(data.orderBy, data.sortOrder);
        }

        return cmd;
    }

    public SqlResult<DataTable> SelectDataTableSelective(SqlData data, string table, string vraceneSloupce, ABC where, ABC whereIsNot)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("SELECT " + vraceneSloupce);
        sb.Append(" FROM " + table);
        int dd = 0;
        sb.Append(GeneratorMsSql.CombinedWhere(where, ref dd));
        sb.Append(GeneratorMsSql.CombinedWhereNotEquals(where != null, ref dd, whereIsNot));
        //string sql = GeneratorMsSql.SimpleWhereOneRow(vracenySloupec, table, idColumnName);
        SqlCommand comm = new SqlCommand(sb.ToString());
        AddCommandParameteresArrays(comm, 0, where, whereIsNot);
        //AddCommandParameter(comm, 0, idColumnValue);
        var dt = SelectDataTable(data, comm);
        return dt;
    }

    public SqlResult<DataTable> SelectDataTableSelective(SqlData data, string table, string vraceneSloupce, ABC where, ABC whereIsNot, ABC greaterThan, ABC lowerThan)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("SELECT " + vraceneSloupce);
        sb.Append(" FROM " + table);
        sb.Append(GeneratorMsSql.CombinedWhere(where, whereIsNot, greaterThan, lowerThan));

        //string sql = GeneratorMsSql.SimpleWhereOneRow(vracenySloupec, table, idColumnName);
        SqlCommand comm = new SqlCommand(sb.ToString());
        AddCommandParameteresCombinedArrays(comm, 0, where, whereIsNot, greaterThan, lowerThan);
        //AddCommandParameter(comm, 0, idColumnValue);
        var dt = SelectDataTable(data, comm);
        return dt;
    }
    #endregion

    #region SelectDataTable
    /// <summary>
    /// 
    /// </summary>
    public SqlResult<DataTable> SelectDataTable(SqlData data, string tableName, string sloupecWhere, object hodnotaWhere)
    {
        SqlCommand comm = new SqlCommand("SELECT TOP(" + data.limit.ToString() + ") * FROM " + tableName + GeneratorMsSql.SimpleWhere(sloupecWhere));
        AddCommandParameter(comm, 0, hodnotaWhere);
        return SelectDataTable(data, comm);
    } 
    #endregion

    #region SelectDataTableLastRows
    public SqlResult<DataTable> SelectDataTableLastRows(SqlData data, string tableName, string columns, string idn, object idv)
    {
        //SELECT TOP 1000 * FROM [SomeTable] ORDER BY MySortColumn DESC
        SqlCommand comm = new SqlCommand("SELECT TOP(" + data.limit.ToString() + ") " + columns + " FROM " + tableName + GeneratorMsSql.SimpleWhere(idn) + " ORDER BY " + data.orderBy + " DESC");
        AddCommandParameter(comm, 0, idv);
        return SelectDataTable(data, comm);
    }

    /// <summary>
    /// Řadí metodou DESC
    /// Tato metoda se přesně hodí když chci získat nějaký nejoblíbenější obsah - srovnává podle hodnoty v A4.
    /// </summary>
    /// <param name="tableName"></param>
    /// <param name="limit"></param>
    /// <param name="sloupecOrder"></param>
    /// <param name="abc"></param>
    public SqlResult<DataTable> SelectDataTableLastRows(SqlData data, string tableName, string columns, params AB[] where)
    {
        //SELECT TOP 1000 * FROM [SomeTable] ORDER BY MySortColumn DESC
        SqlCommand comm = new SqlCommand("SELECT TOP(" + data.limit.ToString() + ") " + columns + " FROM " + tableName + GeneratorMsSql.CombinedWhere(new ABC(where)) + " ORDER BY " + data.orderBy + " DESC");
        AddCommandParameteres(comm, 0, where);
        return SelectDataTable(data, comm);
    }

    /// <summary>
    /// Řadí metodou DESC
    /// Tato metoda se přesně hodí když chci získat nějaký nejoblíbenější obsah - srovnává podle hodnoty v A4.
    /// </summary>
    /// <param name="tableName"></param>
    /// <param name="limit"></param>
    /// <param name="sloupecOrder"></param>
    /// <param name="abc"></param>
    public SqlResult<DataTable> SelectDataTableLastRows(SqlData data, string tableName, string columns, ABC whereIs, ABC whereIsNot, ABC whereGreaterThan, ABC whereLowerThan)
    {
        //SELECT TOP 1000 * FROM [SomeTable] ORDER BY MySortColumn DESC
        SqlCommand comm = new SqlCommand("SELECT TOP(" + data.limit.ToString() + ") " + columns + " FROM " + tableName + GeneratorMsSql.CombinedWhere(whereIs, whereIsNot, whereGreaterThan, whereLowerThan) + " ORDER BY " + data.orderBy + " DESC");
        AddCommandParameteresCombinedArrays(comm, 0, whereIs, whereIsNot, whereGreaterThan, whereLowerThan);
        return SelectDataTable(data, comm);
    }
    #endregion

    #region SelectAllRowsOfColumns
    public SqlResult<DataTable> SelectAllRowsOfColumns(SqlData data, string table, string vratit, ABC abObsahuje, ABC abNeobsahuje, ABC abVetsiNez, ABC abMensiNez)
    {
        string sql = "SELECT " + vratit + " FROM " + table;
        sql += GeneratorMsSql.CombinedWhere(abObsahuje, abNeobsahuje, abVetsiNez, abMensiNez);
        SqlCommand comm = new SqlCommand(sql);

        int i = 0;
        i = AddCommandParameterFromAbc(comm, i, abObsahuje);
        i = AddCommandParameterFromAbc(comm, i, abNeobsahuje);
        i = AddCommandParameterFromAbc(comm, i, abVetsiNez);
        AddCommandParameterFromAbc(comm, i, abVetsiNez);

        return SelectDataTable(data, comm);
    }

    /// <summary>
    /// 2
    /// </summary>
    public SqlResult<DataTable> SelectAllRowsOfColumns(SqlData data, string p, string selectSloupce)
    {
        return SelectDataTable(data, string.Format("SELECT {0} FROM {1}", selectSloupce, p));
    }

    public SqlResult<DataTable> SelectAllRowsOfColumns(SqlData data, string p, string ziskaneSloupce, string idColumnName, object idColumnValue)
    {
        SqlCommand comm = new SqlCommand(string.Format("SELECT {0} FROM {1} ", ziskaneSloupce, p) + GeneratorMsSql.SimpleWhere(idColumnName));
        AddCommandParameter(comm, 0, idColumnValue);
        return SelectDataTable(data, comm);
    }

    public SqlResult<DataTable> SelectAllRowsOfColumns(SqlData data, string p, string ziskaneSloupce, params AB[] ab)
    {
        SqlCommand comm = new SqlCommand(string.Format("SELECT {0} FROM {1} ", ziskaneSloupce, p) + GeneratorMsSql.CombinedWhere(new ABC(ab)));
        AddCommandParameteres(comm, 0, ab);
        return SelectDataTable(data, comm);
    }
    #endregion

    /// <summary>
    /// Tuto metodu nepoužívej například po vkládání, když chceš zjistit ID posledního řádku, protože když tam bude něco smazaného , tak to budeš mít o to posunuté !!
    /// 
    /// </summary>
    public SqlResult<int> SelectFindOutNumberOfRows(SqlData d, string tabulka)
    {
        SqlCommand comm = new SqlCommand("SELECT Count(*) FROM " + tabulka);
        //comm.Transaction = tran;
        var result = ExecuteScalar(d, comm);
        return InstancesSqlResult.Int(ToInt32(result), result);
    }

    /// <summary>
    /// 
    /// </summary>
    public SqlResult<int> SelectID(SqlData data, string tabulka, string nazevSloupce, object hodnotaSloupce)
    {
        SqlCommand c = new SqlCommand(string.Format("SELECT (ID) FROM {0} WHERE {1} = @p0", tabulka, nazevSloupce));
        AddCommandParameter(c, 0, hodnotaSloupce);
        return ExecuteScalarInt(data, c);
    }

    /// <summary>
    /// A2 je sloupec na který se prohledává pro A3
    /// </summary>
    /// <param name="tabulka"></param>
    /// <param name="sloupecID"></param>
    /// <param name="textPartOfCity"></param>
    /// <param name="nazvySloupcu"></param>
    public SqlResult<DataTable> SelectDataTableSelectiveLikeContains(SqlData data, string tabulka, string nazvySloupcu, string sloupecID, string textPartOfCity)
    {
        SqlCommand comm = new SqlCommand(string.Format("SELECT {0} FROM {1} WHERE {2} LIKE '%' + @p0 + '%'", nazvySloupcu, tabulka, sloupecID));
        AddCommandParameter(comm, 0, textPartOfCity);
        //NT
        return this.SelectDataTable(data, comm);
    }

    /// <summary>
    /// Vrátí mi všechny položky ze sloupce 
    /// </summary>
    public SqlResult<DataTable> SelectGreaterThan(SqlData data, string tableName, string tableColumn, object hodnotaOd)
    {
        SqlCommand comm = new SqlCommand(string.Format("SELECT * FROM {0} WHERE {1} > @p0", tableName, tableColumn));
        AddCommandParameter(comm, 0, hodnotaOd);
        return SelectDataTable(data, comm);
    }

    /// <summary>
    /// 
    /// Vrátí null když nenalezne žádný řádek
    /// </summary>
    public SqlResult<object[]> SelectOneRow(SqlData data, string TableName, string nazevSloupce, object hodnotaSloupce)
    {
        // Index nemůže být ani pole bajtů ani null takže to je v pohodě
        var dt = SelectDataTable(data, "SELECT TOP(1) * FROM " + TableName + " WHERE " + nazevSloupce + " = @p0", hodnotaSloupce);
        if (dt.result.Rows.Count == 0)
        {
            return InstancesSqlResult.ArrayObject(null, dt); // CA.CreateEmptyArray(pocetSloupcu);
        }
        return CastSqlResult.FirstRowToArrayObject(dt);
    }

    public SqlResult<List<int>> SelectAllInColumnLargerThanInt(SqlData d, string TableName, string columnReturn, string nameColumnLargerThan, object valueColumnLargerThan, params AB[] where)
    {
        var abcWhere = new ABC(where);
        ABC whereSmallerThan = new ABC(AB.Get(nameColumnLargerThan, valueColumnLargerThan));
        string whereS = GeneratorMsSql.CombinedWhere(abcWhere, null, whereSmallerThan, null);
        SqlCommand comm = new SqlCommand("select " + columnReturn + " FROM " + TableName + whereS);
        AddCommandParameteresCombinedArrays(comm, 0, abcWhere, null, whereSmallerThan, null);
        return ReadValuesInt(d, comm);
    }

    public SqlResult<Guid> SelectNewId(SqlData data)
    {
        var result = ExecuteScalar(data, "SELECT NEWID()");
        // NEWSEQUENTIALID() zde nemůžu použít, to se může pouze při vytváření nové tabulky
        return InstancesSqlResult.Guid(result.result.ToString(), result);
    }

    public Dictionary<T, string> SelectIDNames<T>(SqlData data, Func<string, T> parse, string solutions, params AB[] where)
    {
        Dictionary<T, string> result = new Dictionary<T, string>();
        var dt = SelectDataTableSelective(data, solutions, "ID,Name", where);

        foreach (DataRow item in dt.result.Rows)
        {
            var row = item.ItemArray;

            var id = SH.ToNumber<T>(parse, row[0].ToString());
            var v = MSTableRowParse.GetString(row, 1);

            result.Add(id, v);
        }

        return result;
    }

    #region SelectTableInnerJoin
    /// <summary>
    /// Do A4 se zadává např. p.ID = stf.IDPhoto
    /// </summary>
    public SqlResult<DataTable> SelectTableInnerJoin(SqlData data, string tableFromWithShortVersion, string tableJoinWithShortVersion, string sloupceJezZiskavat, string onKlazuleOdNuly)
    {
        SqlCommand comm = new SqlCommand("select " + sloupceJezZiskavat + " from " + tableFromWithShortVersion + " inner join " + tableJoinWithShortVersion + " on " + onKlazuleOdNuly);
        //AddCommandParameterFromAbc(comm, where);
        return SelectDataTable(data, comm);
    }

    public SqlResult<DataTable> SelectTableInnerJoin(SqlData data, string tableFromWithShortVersion, string tableJoinWithShortVersion, string sloupceJezZiskavat, string onKlazuleOdNuly, params object[] fixniHodnotyOdNuly)
    {
        return SelectDataTable(data, "select " + sloupceJezZiskavat + " from " + tableFromWithShortVersion + " inner join " + tableJoinWithShortVersion + " on " + onKlazuleOdNuly, fixniHodnotyOdNuly);
    }

    /// <summary>
    /// Řadí metodou DESC
    /// </summary>
    /// <param name="tableFromWithShortVersion"></param>
    /// <param name="tableJoinWithShortVersion"></param>
    /// <param name="sloupceJezZiskavat"></param>
    /// <param name="onKlazuleOdNuly"></param>
    /// <param name="limit"></param>
    /// <param name="sloupecPodleKterehoRadit"></param>
    /// <param name="whereIs"></param>
    /// <param name="whereIsNot"></param>
    //public SqlResult<DataTable> SelectDataTableLastRowsInnerJoin(string tableFromWithShortVersion, string tableJoinWithShortVersion, string sloupceJezZiskavat, string onKlazuleOdNuly, int limit, string sloupecPodleKterehoRadit, ABC whereIs, ABC whereIsNot, params object[] hodnotyOdNuly)
    //{
    //    SqlCommand comm = new SqlCommand("select TOP(" + limit.ToString() + ") " + sloupceJezZiskavat + " from " + tableFromWithShortVersion + " inner join " + tableJoinWithShortVersion + " on " + onKlazuleOdNuly + GeneratorMsSql.CombinedWhere(whereIs, whereIsNot, null, null) + " ORDER BY " + sloupecPodleKterehoRadit + " DESC");
    //    AddCommandParameteres(comm, 0, hodnotyOdNuly);
    //    AddCommandParameteresCombinedArrays(comm, hodnotyOdNuly.Length, whereIs, whereIsNot, null, null);

    //    return SelectDataTable(comm);
    //}

    public SqlResult<DataTable> SelectTableInnerJoin(SqlData data, string tableFromWithShortVersion, string tableJoinWithShortVersion, string sloupceJezZiskavat, string onKlazuleOdNuly, ABC whereIs, ABC whereIsNot)
    {
        SqlCommand comm = new SqlCommand("select " + sloupceJezZiskavat + " from " + tableFromWithShortVersion + " inner join " + tableJoinWithShortVersion + " on " + onKlazuleOdNuly + GeneratorMsSql.CombinedWhere(whereIs, whereIsNot, null, null));
        AddCommandParameteresCombinedArrays(comm, 0, whereIs, whereIsNot, null, null);

        return SelectDataTable(data, comm);
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
    public SqlResult<DataTable> SelectTableInnerJoin(SqlData data, string tableFromWithShortVersion, string tableJoinWithShortVersion, string sloupceJezZiskavat, string onKlazuleOdNuly, params AB[] where)
    {
        SqlCommand comm = new SqlCommand("select " + sloupceJezZiskavat + " from " + tableFromWithShortVersion + " inner join " + tableJoinWithShortVersion + " on " + onKlazuleOdNuly + GeneratorMsSql.CombinedWhere(new ABC(where)));
        AddCommandParameterFromAbc(comm, where);
        return SelectDataTable(data, comm);
    }
    #endregion

    #region SelectOneRow*
    /// <summary>
    /// POkud nechceš používat reader, který furt nefugnuje, použij metodu SelectOneRowInnerJoin, má úplně stejnou hlavičku a jen funguje s DataTable
    /// Do A4 se zadává např. p.ID = stf.IDPhoto
    /// </summary>
    public SqlResult<object[]> SelectOneRowInnerJoinReader(SqlData data, string tableFromWithShortVersion, string tableJoinWithShortVersion, string sloupceJezZiskavat, string onKlazuleOdNuly, params AB[] where)
    {
        SqlCommand comm = new SqlCommand("select " + sloupceJezZiskavat + " from " + tableFromWithShortVersion + " inner join " + tableJoinWithShortVersion + " on " + onKlazuleOdNuly + GeneratorMsSql.CombinedWhere(new ABC(where)));
        AddCommandParameterFromAbc(comm, where);
        return SelectRowReader(data, comm);
    }

    public SqlResult<object[]> SelectOneRowInnerJoin(SqlData data, string tableFromWithShortVersion, string tableJoinWithShortVersion, string sloupceJezZiskavat, string onKlazuleOdNuly, params AB[] where)
    {
        SqlCommand comm = new SqlCommand("select " + sloupceJezZiskavat + " from " + tableFromWithShortVersion + " inner join " + tableJoinWithShortVersion + " on " + onKlazuleOdNuly + GeneratorMsSql.CombinedWhere(new ABC(where)));
        AddCommandParameterFromAbc(comm, where);
        var dt = SelectDataTable(data, comm);
        if (dt.result.Rows.Count == 0)
        {
            return InstancesSqlResult.ArrayObject(null, dt);// CA.CreateEmptyArray(pocetSloupcu);
        }
        return InstancesSqlResult.ArrayObject(dt.result.Rows[0].ItemArray, dt);
    } 
    #endregion

    #region SelectLastIDFromTable*
    /// <summary>
    /// Pracuje jako signed.
    /// Vrací skutečně nejvyšší ID, proto když chceš pomocí ní ukládat do DB, musíš si to číslo inkrementovat
    /// Ignoruje vynechaná čísla. Žádná hodnota v sloupci A2 nebyla nalezena, vrátí long.MaxValue
    /// </summary>
    /// <param name="p"></param>
    /// <param name="sloupecID"></param>
    public SqlResult<long> SelectLastIDFromTable(SqlData d, string p, string sloupecID)
    {
        return ExecuteScalarLong(d, new SqlCommand("SELECT MAX(" + sloupecID + ") FROM " + p));
    }

    /// <summary>
    /// If no row was found, return max value
    /// 
    /// SelectLastIDFromTableSigned2 - Vrátí všechny hodnoty z sloupce A3 a pak počítá od A2.MinValue až narazí na hodnotu která v tabulce nebyla, tak ji vrátí. Proto není potřeba vr nijak inkrementovat ani jinak měnit. 
    /// SelectLastIDFromTableSigned - use MAX operator of SQL. Nedá se použít na desetinné typy
    /// </summary>
    /// <param name="table"></param>
    /// <param name="idt"></param>
    /// <param name="sloupecID"></param>
    public SqlResult<object> SelectLastIDFromTableSigned2(SqlData d, string table, Type idt, string sloupecID)
    {
        SqlResult<object> result = new SqlResult<object>();
        bool setUp = false;
        if (idt == typeof(short))
        {

            short vratit = short.MaxValue;
            var all2 = SelectValuesOfColumnAllRowsShort(d, table, sloupecID);
            var all = all2.result;
            //all.Sort();
            short i = short.MinValue;
            for (; i < short.MaxValue; i++)
            {
                if (!all.Contains(i))
                {
                    setUp = true;
                    result = InstancesSqlResult.Object(i, all2);
                    break;
                }
            }

            if (!setUp)
            {
                result.result = ++i;
            }

        }
        else if (idt == typeof(int))
        {
            int vratit = int.MaxValue;
            var all2 = SelectValuesOfColumnAllRowsInt(d, table, sloupecID);
            var all = all2.result;
            //all.Sort();
            int i = int.MinValue;
            for (; i < int.MaxValue; i++)
            {
                if (!all.Contains(i))
                {
                    result = InstancesSqlResult.Object(i, all2);
                    setUp = true;
                    break;
                }
            }

            if (!setUp)
            {
                result.result = i;
            }


        }
        else if (idt == typeof(long))
        {
            long vratit = long.MaxValue;
            d.signed = true;
            var all2 = SelectValuesOfColumnAllRowsLong(d, table, sloupecID);
            var all = all2.result;
            //all.Sort();
            long i = long.MinValue;
            for (; i < long.MaxValue; i++)
            {
                if (!all.Contains(i))
                {
                    result = InstancesSqlResult.Object(i, all2);
                    setUp = true;
                    break;
                }
            }

            if (!setUp)
            {
                result.result = i;
            }

        }
        else

        {
            ThrowExceptions.Custom(Exc.GetStackTrace(), type, Exc.CallingMethod(),"V klazuli if v metodě MSStoredProceduresIBase.SelectLastIDFromTableSigned nebyl nalezen typ " + idt.FullName.ToString());
        }

        return result;

    }

    static Type type = typeof(SqlOperations);

    /// <summary>
    /// Has signed, therefore can return values below -1
    /// 
    /// SelectLastIDFromTableSigned2 - Vrátí všechny hodnoty z sloupce A3 a pak počítá od A2.MinValue až narazí na hodnotu která v tabulce nebyla, tak ji vrátí. Proto není potřeba vr nijak inkrementovat ani jinak měnit
    /// SelectLastIDFromTableSigned - use MAX operator of SQL. Nedá se použít na desetinné typy. Cast to specific type but return SqlResult<object>
    /// SelectLastIDFromTable - use MAX operator of SQL. Nedá se použít na desetinné typy. return in SqlResult<long>
    /// 
    /// Vrátí mi nejmenší volné číslo tabulky A1
    /// Pokud bude obsazene 1,3, vrátí až 4
    /// </summary>
    /// <param name="p"></param>
    /// <param name="idt"></param>
    /// <param name="sloupecID"></param>
    /// <param name="totalLower"></param>
    public SqlResult<object> SelectLastIDFromTableSigned(SqlData d, string p, Type idt, string sloupecID, out bool totalLower)
    {
        totalLower = false;
        var dd2 = ExecuteScalar(d, new SqlCommand("SELECT MAX(" + sloupecID + ") FROM " + p));
        var dd = dd2.result.ToString();

        if (dd == "")
        {
            totalLower = true;
            object vr = 0;
            if (d.signed)
            {
                vr = BTS.GetMinValueForType(idt);
            }

            if (idt == Types.tShort)
            {
                //short s = (short)vr;
                dd2.result = vr;
            }
            else if (idt == Types.tInt)
            {
                //int nt = (int)vr;
                dd2.result = vr;
            }
            else if (idt == Types.tByte)
            {
                dd2.result = vr;
            }
            else if (idt == Types.tLong)
            {
                //long lng = (long)vr;
                dd2.result = vr;
            }
            else
            {
                ThrowExceptions.Custom(Exc.GetStackTrace(), type, Exc.CallingMethod(),"V klazuli if v metodě MSStoredProceduresIBase.SelectLastIDFromTableSigned nebyl nalezen typ " + idt.FullName.ToString());
            }
        }

        if (idt == typeof(Byte))
        {
            dd2.result = byte.Parse(dd);
        }
        else if (idt == typeof(Int16))
        {
            dd2.result = Int16.Parse(dd);
        }
        else if (idt == typeof(Int32))
        {
            dd2.result = Int32.Parse(dd);
        }
        else if (idt == typeof(Int64))
        {
            dd2.result = Int64.Parse(dd);
        }
        else if (idt == typeof(SByte))
        {
            dd2.result = SByte.Parse(dd);
        }
        else if (idt == typeof(UInt16))
        {
            dd2.result = UInt16.Parse(dd);
        }
        else if (idt == typeof(UInt32))
        {
            dd2.result = UInt32.Parse(dd);
        }
        else if (idt == typeof(UInt64))
        {
            dd2.result = UInt64.Parse(dd);
        }
        //ThrowExceptions.Custom(Exc.GetStackTrace(), type, Exc.CallingMethod(),"Nepovolený nehodnotový typ v metodě GetMinValueForType");
        else
        {
            dd2.result = decimal.Parse(dd);
        }
        return dd2;
    } 
    #endregion

    #region SelectFirstAvailable{Type]Index
    public SqlResult<int> SelectFirstAvailableIntIndex(SqlData data, string table, string column)
    {
        var r = SelectLastIDFromTableSigned2(data, table, Types.tInt, column);
        var r2 = new SqlResult<int>(r);
        r2.result = ToInt32(r);
        return r2;
    }

    public SqlResult<short> SelectFirstAvailableShortIndex(SqlData data, string table, string column)
    {
        var r = SelectLastIDFromTableSigned2(data, table, Types.tShort, column);
        var r2 = new SqlResult<short>(r);
        r2.result = ToInt16(r);
        return r2;
    }
    #endregion

    #region SelectMax{Type}MinValue
    public SqlResult<short> SelectMaxShortMinValue(SqlData d, string table, string column)
    {
        var count = SelectCount(d, table);
        if (count.result == 0)
        {
            return InstancesSqlResult.Short(short.MinValue, count);
        }
        var rs = ExecuteScalarShort(d, new SqlCommand("SELECT MAX(" + column + ") FROM " + table));
        rs.result++;
        return rs;
    }

    /// <summary>
    /// Vrací int.MaxValue pokud tabulka nebude mít žádné řádky, na rozdíl od metody SelectMaxInt, která vrací 0
    /// To co vrátí tato metoda můžeš vždy jen inkrementovat a vložit do tabulky
    /// </summary>
    /// <param name="table"></param>
    /// <param name="column"></param>
    public SqlResult<int> SelectMaxIntMinValue(SqlData d, string table, string column)
    {
        var count = SelectCount(d, table);
        if (count.result == 0)
        {
            return InstancesSqlResult.Int(int.MinValue, count);
        }
        d.signed = true;

        return ExecuteScalarInt(d, new SqlCommand("SELECT MAX(" + column + ") FROM " + table));
    }

    public SqlResult<int> SelectMaxIntMinValue(SqlData data, string table, string sloupec, params AB[] aB)
    {
        SqlCommand comm = new SqlCommand("SELECT MAX(" + sloupec + ") FROM " + table + GeneratorMsSql.CombinedWhere(aB));
        AddCommandParameteres(comm, 0, aB);
        return ExecuteScalarInt(data, comm);
    }
    #endregion

    #region SelectMax
    public SqlResult<DateTime> SelectMaxDateTime(SqlData data, string table, string column, params AB[] ab)
    {
        SqlCommand comm = new SqlCommand("SELECT MAX(" + column + ") FROM " + table + GeneratorMsSql.CombinedWhere(ab));
        AddCommandParameteres(comm, 0, ab);
        return ExecuteScalarDateTime(data, DateTime.MinValue, comm);
    }
    #endregion

    #region SelectMax {Type}
    /// <summary>
    /// Pokud nenajde, vrátí DateTime.MinValue
    /// Do A4 zadej DateTime.MinValue pokud nevíš - je to původní hodnota
    /// </summary>
    /// <param name="table"></param>
    /// <param name="column"></param>
    public SqlResult<DateTime> SelectMaxDateTime(SqlData data, string table, string column, ABC whereIs, ABC whereIsNot)
    {
        SqlCommand comm = new SqlCommand("SELECT MAX(" + column + ") FROM " + table + GeneratorMsSql.CombinedWhere(whereIs, whereIsNot, null, null));
        AddCommandParameteresCombinedArrays(comm, 0, whereIs, whereIsNot, null, null);
        return ExecuteScalarDateTime(data, data.getIfNotFound, comm);
    }

    /// <summary>
    /// Vrací 0 pokud tabulka nebude mít žádné řádky, na rozdíl od metody SelectMaxIntMinValue, která vrací int.MinValue
    /// </summary>
    /// <param name="table"></param>
    /// <param name="column"></param>
    public SqlResult<int> SelectMaxInt(SqlData d, string table, string column)
    {
        d.signed = true;

        var count = SelectCount(d, table);
        if (count.result == 0)
        {
            return InstancesSqlResult.Int(0, count);
        }
        return ExecuteScalarInt(d, new SqlCommand("SELECT MAX(" + column + ") FROM " + table));
    }

    public SqlResult<byte> SelectMaxByte(SqlData data, string table, string column, params AB[] aB)
    {
        var count = SelectCount(data, table);
        if (count.result == 0)
        {
            return InstancesSqlResult.Byte(0, count);
        }
        ABC abc = new ABC(aB);
        var result = ExecuteScalar(data, "SELECT MAX(" + column + ") FROM " + table + GeneratorMsSql.CombinedWhere(aB), abc.OnlyBs());
        return InstancesSqlResult.Byte(ToByte(result), result);
    }
    #endregion

    #region SelectMin {Type} MinValue
    public SqlResult<short> SelectMinShortMinValue(SqlData data, string table, string sloupec, params AB[] aB)
    {
        SqlCommand comm = new SqlCommand("SELECT MIN(" + sloupec + ") FROM " + table + GeneratorMsSql.CombinedWhere(aB));
        AddCommandParameteres(comm, 0, aB);
        data.signed = true;
        return ExecuteScalarShort(data, comm);
    } 
    #endregion

    #region SelectMin {Type}
    public SqlResult<int> SelectMinInt(SqlData data, string table, string column)
    {
        var count = SelectCountInt(data, table);
        var count2 = count.result;
        if (count2 == 0)
        {
            return InstancesSqlResult.Int(count2, count);
        }
        data.signed = true;
        return ExecuteScalarInt(data, new SqlCommand("SELECT MIN(" + column + ") FROM " + table));
    } 
    #endregion

    #region SelectValuesOfColumn {Type}
    /// <summary>
    /// Používej místo této M metodu SelectValuesOfColumnAllRowsInt která je úplně stejná
    /// </summary>
    /// <param name="tabulka"></param>
    /// <param name="sloupecHledaný"></param>
    /// <param name="abc"></param>
    public SqlResult<List<int>> SelectValuesOfColumnInt(SqlData data, string tabulka, string sloupecHledaný, params AB[] abc)
    {
        SqlCommand comm = new SqlCommand(string.Format("SELECT {0} FROM {1} {2}", sloupecHledaný, tabulka, GeneratorMsSql.CombinedWhere(new ABC(abc))));
        for (int i = 0; i < abc.Length; i++)
        {
            AddCommandParameter(comm, i, abc[i].B);
        }
        return ReadValuesInt(data, comm);
    }

    public SqlResult<List<byte>> SelectValuesOfColumnByte(SqlData data, string tabulka, string sloupecHledaný, string sloupecVeKteremHledat, object hodnota)
    {
        // SQLiteDataReader je třída zásadně pro práci s jedním řádkem výsledků, ne s 2mi a více !!
        SqlCommand comm = new SqlCommand(string.Format("SELECT {0} FROM {1} WHERE {2} = @p0", sloupecHledaný, tabulka, sloupecVeKteremHledat));
        AddCommandParameter(comm, 0, hodnota);
        return ReadValuesByte(data, comm);
    }

    /// <summary>
    /// Používej místo této M metodu SelectValuesOfColumnAllRowsInt, která je úplně stejná
    /// </summary>
    /// <param name="tabulka"></param>
    /// <param name="sloupecHledaný"></param>
    /// <param name="sloupecVeKteremHledat"></param>
    /// <param name="hodnota"></param>
    public SqlResult<List<int>> SelectValuesOfColumnInt(SqlData data, string tabulka, string sloupecHledaný, string sloupecVeKteremHledat, object hodnota)
    {
        SqlCommand comm = new SqlCommand(string.Format("SELECT {0} FROM {1} WHERE {2} = @p0", sloupecHledaný, tabulka, sloupecVeKteremHledat));
        AddCommandParameter(comm, 0, hodnota);
        var result = ReadValuesInt(data, comm);
        comm.Connection.Close();
        return result;
    } 
    #endregion
    #endregion

    #region Execute*
    public SqlResult<int> ExecuteScalarInt(SqlData d, SqlCommand comm)
    {
        var o = ExecuteScalar(d, comm);
        if (IsNullOrDefault(o))
        {
            if (d.signed)
            {
                o.result = int.MaxValue;
            }
            else
            {
                o.result = -1;
            }
            return InstancesSqlResult.Int(ToInt32(o), o);
        }
        return InstancesSqlResult.Int(ToInt32(o), o);
    }

    public SqlResult<long> ExecuteScalarLong(SqlData data, SqlCommand comm)
    {
        var o = ExecuteScalar(data, comm);
        if (IsNullOrDefault(o))
        {
            if (data.signed)
            {
                o.result = long.MaxValue;
            }
            else
            {
                o.result = -1;
            }
            return InstancesSqlResult.Long(ToInt64(o), o);
        }
        return InstancesSqlResult.Long(ToInt64(o), o);
    }

    public SqlResult<bool?> ExecuteScalarNullableBool(SqlData d, SqlCommand comm)
    {
        var o = ExecuteScalar(d, comm);
        if (IsNullOrDefault(o))
        {
            return InstancesSqlResult.NullableBool(null, o);
        }
        return InstancesSqlResult.NullableBool(ToBoolean(o), o);
    }

    public SqlResult<short> ExecuteScalarShort(SqlData data, SqlCommand comm)
    {
        var o = ExecuteScalar(data, comm);
        if (IsNullOrDefault(o))
        {
            if (data.signed)
            {
                o.result = short.MaxValue;
            }
            else
            {
                o.result = -1;
            }
            return InstancesSqlResult.Short(ToInt16(o), o);
        }
        return InstancesSqlResult.Short(ToInt16(o), o);
    }

    public SqlResult<string> ExecuteScalarString(SqlData d, SqlCommand comm)
    {
        var o = ExecuteScalar(d, comm);
        if (IsNullOrDefault(o))
        {
            return InstancesSqlResult.String(string.Empty, o);
        }
        
        return InstancesSqlResult.String(o.result.ToString().TrimEnd(AllChars.space), o);
    }

    public SqlResult<byte> ExecuteScalarByte(SqlData data, SqlCommand comm)
    {
        var o = ExecuteScalar(data, comm);
        if (IsNullOrDefault(o))
        {
            return InstancesSqlResult.Byte(0, o);
        }
        return InstancesSqlResult.Byte(ToByte(o), o);
    }

    private bool IsNullOrDefault(SqlResult<object> o)
    {
        return IsNullOrDefault<object>(o);
    }

    private bool IsNullOrDefault<T>(SqlResult<T> o)
    {
        bool vr = false;
        if (EqualityComparer<T>.Default.Equals(o.result, default(T)))
        {
            vr = true;
        }
        else if (IsNull(o.result))
        {
            vr = true;
        }
        return vr;
    }

    public SqlResult<DateTime> ExecuteScalarDateTime(SqlData data, DateTime getIfNotFound, SqlCommand comm)
    {
        var o = ExecuteScalar(data, comm);
        if (IsNullOrDefault(o))
        {
            return InstancesSqlResult.DateTime(getIfNotFound, o);
        }
        return InstancesSqlResult.DateTime(ToDateTime(o), o);
    }

    public SqlResult ExecuteNonQuery(SqlData d, string commText, params object[] para)
    {
        SqlCommand comm = new SqlCommand(commText);
        for (int i = 0; i < para.Length; i++)
        {
            AddCommandParameter(comm, i, para[i]);
        }
        return ExecuteNonQuery(d, comm);
    }

    public SqlResult<float> ExecuteScalarFloat(SqlData data, SqlCommand comm)
    {
        var o = ExecuteScalar(data, comm);
        if (IsNullOrDefault(o))
        {
            if (data.signed)
            {
                return InstancesSqlResult.Float(float.MaxValue, o);
                //return short.MaxValue;
            }
            else
            {
                return InstancesSqlResult.Float(-1, o);
            }
        }
        return InstancesSqlResult.Float(ToSingle(o), o);
    }

    /// <summary>
    /// MUST CALL conn.Close(); AFTER GET DATA
    /// </summary>
    /// <param name="comm"></param>
    public SqlResult<SqlDataReader> ExecuteReader(SqlData data, SqlCommand comm)
    {
        var conn = new SqlConnection(Cs);

        conn.Open();
        comm.Connection = conn;
        var result = comm.ExecuteReader(CommandBehavior.Default);

        return InstancesSqlResult.SqlDataReader(result, InstancesSqlResult.Empty);

    }

    /// <summary>
    /// Save into result
    /// Automaticky doplní connection
    /// </summary>
    /// <param name="comm"></param>
    public SqlResult<object> ExecuteScalar(SqlData d, SqlCommand comm)
    {
        using (var conn = new SqlConnection(Cs))
        {
            SqlResult<object> sqlResult = new SqlResult<object>();
            //loggedCommands.Add(comm.CommandText);
            conn.Open();
            //SqlDbType.SmallDateTime;
            comm.Connection = conn;
            var result = comm.ExecuteScalar();
            conn.Close();

            sqlResult.result = result;

            return sqlResult;
        }
    }

    public SqlResult<object> ExecuteScalar(SqlData data, string commText, params object[] para)
    {
        SqlCommand comm = new SqlCommand(commText);
        for (int i = 0; i < para.Length; i++)
        {
            AddCommandParameter(comm, i, para[i]);
        }
        var result = ExecuteScalar(data, comm);
        return result;
    }

    public SqlResult<bool> ExecuteScalarBool(SqlData data, SqlCommand comm)
    {
        var o = ExecuteScalar(data, comm);
        if (IsNullOrDefault(o))
        {
            return InstancesSqlResult.Bool(false, o); ;
        }
        return InstancesSqlResult.Bool(ToBoolean(o), o);
    }

    /// <summary>
    /// Return count of rows affected
    /// </summary>
    /// <param name="comm"></param>
    public SqlResult ExecuteNonQuery(SqlData d, SqlCommand comm)
    {
        using (SqlConnection conn = new SqlConnection(Cs))
        {
            SqlResult result = new SqlResult(d);

            //string result = string.Empty;

            conn.Open();
            comm.Connection = conn;

            PrintDebugParameters(comm);

            try
            {
                result.affectedRows = comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                result.exc = Exceptions.TextOfExceptions(ex);
            }

            conn.Close();
            return result;
        }
    }
    #endregion

    #region To* - must be type SqlResult<object>, when I need to convert not this type, use Convert type directly
    public uint ToUInt32(SqlResult<object> result)
    {
        return Convert.ToUInt32(result.result);
    }

    public short ToInt16(SqlResult<object> result)
    {
        return Convert.ToInt16(result.result);
    }

    public bool ToBoolean(SqlResult<object> o)
    {
        return Convert.ToBoolean(o.result);
    }

    public DateTime ToDateTime(SqlResult<object> o)
    {
        return Convert.ToDateTime(o.result);
    }

    public float ToSingle(SqlResult<object> o)
    {
        return Convert.ToSingle(o.result);
    }

    public long ToInt64(SqlResult<object> result)
    {
        return Convert.ToInt64(result.result);
    }

    public int ToInt32(SqlResult<object> result)
    {
        return Convert.ToInt32(result.result);
    }

    public byte ToByte(SqlResult<object> result)
    {
        return Convert.ToByte(result.result);
    }
    #endregion

    #region Insert*
    #region Insert 1-6
    /// <summary>
    /// Insert1 - Find ID automatically. Use SelectLastIDFromTableSigned  (as name of method is Insert1)
    /// </summary>
    /// <param name="data"></param>
    /// <param name="tabulka"></param>
    /// <param name="idt"></param>
    /// <param name="sloupecID"></param>
    /// <param name="sloupce"></param>
    public SqlResult<long> Insert1(SqlData data, string tabulka, Type idt, string sloupecID, object[] sloupce)
    {
        string hodnoty = MSDatabaseLayer.GetValuesDirect(sloupce.Length + 1);

        SqlCommand comm = new SqlCommand(string.Format("INSERT INTO {0} VALUES {1}", tabulka, hodnoty));
        bool totalLower = false;
        var d = SelectLastIDFromTableSigned(data, tabulka, idt, sloupecID, out totalLower);
        int pricist = 0;
        if (!totalLower)
        {
            pricist = 1;
        }
        else if (idt == Types.tByte)
        {
            pricist = 1;
        }
        if (idt == typeof(Byte))
        {
            Byte b = ToByte(d);
            comm.Parameters.AddWithValue("@p0", b + pricist);
        }
        else if (idt == typeof(Int16))
        {
            Int16 i1 = ToInt16(d);
            comm.Parameters.AddWithValue("@p0", i1 + pricist);
        }
        else if (idt == typeof(Int32))
        {
            Int32 i2 = ToInt32(d);
            comm.Parameters.AddWithValue("@p0", i2 + pricist);
        }
        else if (idt == typeof(Int64))
        {
            Int64 i3 = ToInt64(d);
            comm.Parameters.AddWithValue("@p0", i3 + pricist);
        }
        int to = sloupce.Length + 1;
        for (int i = 1; i < to; i++)
        {
            object o = sloupce[i - 1];
            AddCommandParameter(comm, i, o);
            //DateTime.Now.Month;
        }
        var sqlResult = ExecuteNonQuery(data, comm);

        long vr = ToInt64(d);
        vr += pricist;
        return InstancesSqlResult.Long( vr, sqlResult);
    }

    /// <summary>
    /// Insert2 - For getting ID use SelectLastIDFromTableSigned2 (with 2 postfix)
    /// Tato metoda je vyjímečná, vkládá hodnoty signed, hodnotu kterou vložit si zjistí sám a vrátí ji.
    /// 
    /// Find ID automatically. Use SelectLastIDFromTableSigned2 (as name of method is Insert2)
    /// </summary>
    /// <param name="tabulka"></param>
    /// <param name="sloupecID"></param>
    /// <param name="nazvySloupcu"></param>
    /// <param name="sloupce"></param>
    public SqlResult<long> Insert2(SqlData d, string tabulka, string sloupecID, Type typSloupecID, params object[] sloupce)
    {
        string hodnoty = MSDatabaseLayer.GetValuesDirect(sloupce.Length + 1);

        SqlCommand comm = new SqlCommand(string.Format("INSERT INTO {0} VALUES {1}", tabulka, hodnoty));
        //bool totalLower = false;
        var l = SelectLastIDFromTableSigned2(d, tabulka, typSloupecID, sloupecID);

        long id = Convert.ToInt64(l);
        AddCommandParameter(comm, 0, id);
        for (int i = 0; i < sloupce.Length; i++)
        {
            AddCommandParameter(comm, i + 1, sloupce[i]);
        }
        var result = ExecuteNonQuery(d, comm);
        return InstancesSqlResult.Long(id, result);
    }

    /// <summary>
    /// Insert3 - insert with specified id
    /// 
    /// In scz use nowhere 
    /// A2 může být ID nebo cokoliv začínající na ID(ID*)
    /// A2 je ID řádku na který se bude vkládat. Název/hodnota/whatever A2 už nesmí být v A3
    /// Používej tehdy když chceš určit index na který vkládat.
    /// </summary>
    /// <param name="tabulka"></param>
    /// <param name="IDUsers"></param>
    /// <param name="sloupce"></param>
    public SqlResult Insert3(SqlData d, string tabulka, long IDUsers, params object[] sloupce)
    {
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
        var result = ExecuteNonQuery(d, comm);

        return result;
    }

    /// <summary>
    /// Insert4 - Stejná jako 3, jen ID* je v A2 se všemi
    /// </summary>
    /// <param name="tabulka"></param>
    /// <param name="sloupce"></param>
    public SqlResult Insert4(SqlData d, string tabulka, params object[] sloupce)
    {
        string hodnoty = MSDatabaseLayer.GetValues(sloupce);

        SqlCommand comm = new SqlCommand(string.Format("INSERT INTO {0} VALUES {1}", tabulka, hodnoty));

        int to = sloupce.Length;
        for (int i = 0; i < to; i++)
        {
            object o = sloupce[i];
            AddCommandParameter(comm, i, o);
            //DateTime.Now.Month;
        }
        return ExecuteNonQuery(d, comm);
        //return ToInt64( sloupce[0]);
    }

    /// <summary>
    /// Insert5 - Can specify columns to which to insert
    /// </summary>
    /// <param name="table"></param>
    /// <param name="nazvySloupcu"></param>
    /// <param name="sloupce"></param>
    public SqlResult<long> Insert5(SqlData d, string table, string nazvySloupcu, params object[] sloupce)
    {
        string hodnoty = MSDatabaseLayer.GetValues(sloupce);

        SqlCommand comm = new SqlCommand(string.Format("INSERT INTO {0} {2} VALUES {1}", table, hodnoty, nazvySloupcu));

        int to = sloupce.Length;
        for (int i = 0; i < to; i++)
        {
            object o = sloupce[i];
            AddCommandParameter(comm, i, o);
            //DateTime.Now.Month;
        }
        var result = ExecuteNonQuery(d, comm);
        return InstancesSqlResult.Long(Convert.ToInt64(sloupce[0]), result);
    }

    /// <summary>
    /// Insert6 - replace (", for "(newid()," -> in 14 will be about one element less than in A3
    /// 
    /// In scz used nowhere
    /// Jediná metoda kde můžeš specifikovat sloupce do kterých chceš vložit
    /// Sloupec který nevkládáš musí být auto_increment
    /// ÏD si pak musíš zjistit sám pomocí nějaké identifikátoru - například sloupce Uri
    /// </summary>
    /// <param name="table"></param>
    /// <param name="nazvySloupcu"></param>
    /// <param name="sloupce"></param>
    public SqlResult<long> Insert6(SqlData d, string table, string nazvySloupcu, params object[] sloupce)
    {
        string hodnoty = MSDatabaseLayer.GetValues(sloupce);

        SqlCommand comm = new SqlCommand(string.Format("INSERT INTO {0} {2} VALUES {1}", table, hodnoty, nazvySloupcu.Replace("(", "(newid(),")));

        int to = sloupce.Length;
        for (int i = 0; i < to; i++)
        {
            object o = sloupce[i];
            AddCommandParameter(comm, i, o);
            //DateTime.Now.Month;
        }
        var result = ExecuteNonQuery(d, comm);
        return InstancesSqlResult.Long(Convert.ToInt64(sloupce[0]), result);
    } 
    #endregion

    /// <summary>
    /// For inserting to table id-name
    /// </summary>
    /// <param name="tabulka"></param>
    /// <param name="nazev"></param>
    public SqlResult<long> InsertRowTypeEnum(SqlData d, string tabulka, string nazev)
    {
        var vr2 = SelectFindOutNumberOfRows(d, tabulka);
        var vr = vr2.result + 1;
        SqlCommand c = new SqlCommand(string.Format("INSERT INTO {0} (ID, Name) VALUES (@p0, @p1)", tabulka));
        AddCommandParameter(c, 0, vr);
        AddCommandParameter(c, 1, nazev);
        var sqlResult = ExecuteNonQuery(d, c);
        return InstancesSqlResult.Long(vr, sqlResult);
    }

    #region Insert - simple methods which uses Insert1
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
    public SqlResult<long> Insert(SqlData data, string tabulka, Type idt, string sloupecID, params object[] sloupce)
    {
        data.signed = false;

        return Insert1(data, tabulka, idt, sloupecID, sloupce);
    }

    public SqlResult<long> InsertSigned(SqlData d, string tabulka, Type idt, string sloupecID, params object[] sloupce)
    {
        d.signed = true;
        return Insert1(d, tabulka, idt, sloupecID, sloupce);
        
    }
    #endregion

    #region InsertToRowGuid
    /// <summary>
    /// InsertToRowGuid - insert with generated guid to column A1
    /// 
    /// Raději používej metodu s 3/2A sloupecID, pokud používáš v tabulce sloupce ID, které se nejmenují ID
    /// Sloupec u kterého se bude určovat poslední index a ten inkrementovat a na ten vkládat je ID
    /// Používej tehdy když ID sloupec má nějaký standardní název, Tedy ID, ne IDUsers atd.
    /// </summary>
    /// <param name="tabulka"></param>
    /// <param name="sloupce"></param>
    public SqlResult<Guid> InsertToRowGuid(SqlData d, string tabulka, params object[] sloupce)
    {
        return InsertToRowGuid2(d, tabulka, "ID", sloupce);
    }

    /// <summary>
    /// InsertToRowGuid2 - insert with generated guid to column A3
    /// 
    /// Do této metody se vkládají hodnoty bez ID
    /// ID se počítá jako v Sqlite - tedy od 1 
    /// A2 je zde proto aby se mohlo určit poslední index a ten inkrementovat a na ten vložit. Název/hodnota/whatever tohoto sloupce musí být 1. v A3.
    /// Používej tehdy když ID sloupec má nějaký speciální název, např. IDUsers
    /// </summary>
    /// <param name="tabulka"></param>
    /// <param name="sloupce"></param>
    public SqlResult<Guid> InsertToRowGuid2(SqlData d, string tabulka, string sloupecID, params object[] sloupce)
    {
        int hodnotyLenght = sloupce.Length + 1;
        string hodnoty = MSDatabaseLayer.GetValuesDirect(hodnotyLenght);

        SqlCommand comm = new SqlCommand(string.Format("INSERT INTO {0} VALUES {1}", tabulka, hodnoty));
        for (int i = 1; i < hodnotyLenght; i++)
        {
            object o = sloupce[i - 1];
            AddCommandParameter(comm, i, o);
            //DateTime.Now.Month;
        }
        Guid vr = SelectNewId(d).result;
        AddCommandParameter(comm, 0, vr);

        var result = ExecuteNonQuery(d, comm);
        return InstancesSqlResult.Guid(vr, result);
    }

    /// <summary>
    /// InsertToRowGuid3 - insert to specified guid in A3
    /// 
    /// A2 je ID řádku na který se bude vkládat. Název/hodnota/whatever tohoto sloupce musí být 1. v A3.
    /// Používej tehdy když chceš určit index na který vkládat.
    /// </summary>
    /// <param name="tabulka"></param>
    /// <param name="IDUsers"></param>
    /// <param name="sloupce"></param>
    public SqlResult InsertToRowGuid3(SqlData d, string tabulka, Guid IDUsers, params object[] sloupce)
    {
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
        var result = ExecuteNonQuery(d, comm);
        return result;
    }
    #endregion
    #endregion

    #region Helpers methods
    public void JoinSqlResults(SqlResult r1, SqlResult r2)
    {
        // Cant be joined, sqlExceptions is Enum, can be raise exeptions of more types

    }

    public void PrintDebugParameters(SqlCommand comm)
    {
        //foreach (SqlParameter item in comm.Parameters)
        //{
        //    //DebugLogger.DebugWriteLine(SH.NullToStringOrDefault( item.Value));
        //}
    }
    #endregion

    #region Content injectors for SqlData
    public string OrderBy(SqlData data)
    {
        return GeneratorMsSql.OrderBy(data.orderBy, data.sortOrder);
    }

    /// <summary>
    /// Not return space on left or right
    /// </summary>
    /// <param name="d"></param>
    public string TopDistinct(SqlData d)
    {
        StringBuilder sb = new StringBuilder();
        if (d.distinct)
        {
            sb.Append( "distinct");
        }
        if (d.limit != int.MaxValue)
        {
             sb.Append( " TOP(" + d.limit.ToString() + ") ");
        }

        return sb.ToString();
    }

 

    private void GroupBy(SqlData data, ref string sql)
    {
        if (!string.IsNullOrEmpty(data.GroupByColumn))
        {
            sql += " group by " + data.GroupByColumn;
        }
    }
    #endregion

    #region Random*
    public SqlResult<int> RandomValueFromColumnInt(SqlData data, string table, string column)
    {
        data.signed = true;
        return ExecuteScalarInt(data, new SqlCommand("select " + column + " from " + table + " where " + column + " in (select top 1 " + column + " from " + table + " order by newid())"));
    }

    public SqlResult<short> RandomValueFromColumnShort(SqlData d, string table, string column)
    {
        d.signed = true;
        var result = ExecuteScalarShort(d, new SqlCommand("select " + column + " from " + table + " where " + column + " in (select top 1 " + column + " from " + table + " order by newid())"));
        return result;
    } 
    #endregion

    #region Read*
    public SqlResult<List<int>> ReadValuesInt(SqlData data, SqlCommand comm)
    {
        List<int> vr = new List<int>();
        SqlDataReader r = null;
        var r2 = ExecuteReader(data, comm);
        r = r2.result;
        if (r.HasRows)
        {
            while (r.Read())
            {
                int o = r.GetInt32(0);
                //Type t = val.GetType();
                vr.Add(o);
            }
        }
        comm.Connection.Close();
        comm.Connection.Dispose();
        return InstancesSqlResult.ListInt(vr, r2);
    }

    public SqlResult<List<string>> ReadValuesStringTrim(SqlData data, SqlCommand comm)
    {
        List<string> vr = new List<string>();
        var r2 = ExecuteReader(data, comm); ;
        var r = r2.result;

        if (r.HasRows)
        {
            while (r.Read())
            {
                string o = r.GetString(0).TrimEnd(' ');
                //Type t = val.GetType();
                vr.Add(o);
            }
        }
        comm.Connection.Close();
        comm.Connection.Dispose();
        return InstancesSqlResult.ListString(vr, r2);
    }


    public SqlResult<List<string>> ReadValuesString(SqlData d, SqlCommand comm)
    {
        List<string> vr = new List<string>();
        var r2 = ExecuteReader(d, comm);
        var r = r2.result;

        if (r.HasRows)
        {
            while (r.Read())
            {
                string o = d.stringUseWhenNull;
                // Better is use GetValue than GetString, if I found Null value with GetString raise exception
                var s = r.GetValue(0);
                if (!IsNull(s))
                {
                    o = s.ToString().TrimEnd(AllChars.space);
                }
                //o = r.GetString(0);

                //Type t = val.GetType();
                vr.Add(o);
            }
        }
        comm.Connection.Close();
        comm.Connection.Dispose();

        CA.Trim(vr);

        return InstancesSqlResult.ListString(vr, r2);
    }

    public SqlResult<List<byte>> ReadValuesByte(SqlData data, SqlCommand comm)
    {
        List<byte> vr = new List<byte>();
        var r2 = ExecuteReader(data, comm); ;
        var r = r2.result;

        if (r.HasRows)
        {
            while (r.Read())
            {
                byte o = r.GetByte(0);
                //Type t = val.GetType();
                vr.Add(o);
            }
        }
        comm.Connection.Close();
        comm.Connection.Dispose();
        return InstancesSqlResult.ListByte(vr, r2);
    }

    public SqlResult<List<DateTime>> ReadValuesDateTime(SqlData data, SqlCommand comm)
    {
        var r2 = ExecuteReader(data, comm);

        List<DateTime> vr = new List<DateTime>();
        var r = r2.result;

        if (r.HasRows)
        {
            while (r.Read())
            {
                DateTime o = r.GetDateTime(0);
                //Type t = val.GetType();
                vr.Add(o);
            }
        }

        comm.Connection.Close();
        comm.Connection.Dispose();
        return InstancesSqlResult.ListDateTime(vr, r2);
    }

    public SqlResult<List<long>> ReadValuesLong(SqlData data, SqlCommand comm)
    {
        List<long> vr = new List<long>();
        var r2 = ExecuteReader(data, comm);
        var r = r2.result;


        if (r.HasRows)
        {
            while (r.Read())
            {
                long o = r.GetInt64(0);
                //Type t = val.GetType();
                vr.Add(o);
            }
        }

        comm.Connection.Close();
        comm.Connection.Dispose();
        return InstancesSqlResult.ListLong(vr, r2);
    }

    public SqlResult<List<short>> ReadValuesShort(SqlData data, SqlCommand comm)
    {
        List<short> vr = new List<short>();

        var r2 = ExecuteReader(data, comm);

        var r = r2.result;

        if (r.HasRows)
        {
            while (r.Read())
            {
                short o = r.GetInt16(0);
                //Type t = val.GetType();
                vr.Add(o);
            }
        }

        comm.Connection.Close();
        comm.Connection.Dispose();
        return InstancesSqlResult.ListShort(vr, r2);
    }
    #endregion

    #region Update*
    public SqlResult UpdateWhereIsLowerThan(SqlData d, string table, string columnToUpdate, object newValue, string columnLowerThan, DateTime valueLowerThan, params AB[] where)
    {
        ABC lowerThan = new ABC(AB.Get(columnLowerThan, valueLowerThan));
        int parametrSet = lowerThan.Length + 1;
        string sql = string.Format("UPDATE {0} SET {1}=@p" + parametrSet + " {2}", table, columnToUpdate, GeneratorMsSql.CombinedWhere(new ABC(where), null, null, lowerThan));
        SqlCommand comm = new SqlCommand(sql);
        AddCommandParameter(comm, parametrSet, newValue);
        AddCommandParameteresCombinedArrays(comm, 0, new ABC(where), null, null, lowerThan);

        var vr = ExecuteNonQuery(d, comm);
        return vr;
    }

    

    /// <summary>
    /// Updatuje pouze 1 řádek
    /// </summary>
    /// <param name="tablename"></param>
    /// <param name="updateColumn"></param>
    /// <param name="idColumn"></param>
    /// <param name="idValue"></param>
    public SqlResult<bool> UpdateSwitchBool(SqlData data, string tablename, string updateColumn, string idColumn, object idValue)
    {
        var vr2 = SelectCellDataTableBoolOneRow(data, tablename, updateColumn, idColumn, idValue);
        bool vr = !vr2.result;
        var result = Update(data, tablename, updateColumn, vr, idColumn, idValue);

        return InstancesSqlResult.Bool(vr, result);
    }

    #region UpdateSum {Type} Value
    /// <summary>
    /// Vrátí nohou hodnotu v DB
    /// </summary>
    /// <param name="table"></param>
    /// <param name="sloupecID"></param>
    /// <param name="id"></param>
    /// <param name="sloupecKUpdate"></param>
    /// <param name="pridej"></param>
    public SqlResult<int> UpdateSumIntValue(SqlData data, string table, string sloupecKUpdate, int pridej, string sloupecID, object id)
    {
        data.signed = true;
        var d2 = SelectCellDataTableIntOneRow(data, table, sloupecKUpdate, AB.Get(sloupecID, id));
        var d = d2.result;
        if (d == int.MaxValue)
        {
            return InstancesSqlResult.Int(d, d2);
        }
        int n = pridej + d;
        var result = Update(data, table, sloupecKUpdate, n, sloupecID, id);
        return InstancesSqlResult.Int(n, result);
    }

    public SqlResult<long> UpdateSumLongValue(SqlData data, string table, string sloupecKUpdate, int pridej, string sloupecID, object id)
    {
        var d = SelectCellDataTableLongOneRow(data, table, sloupecKUpdate, sloupecID, id);
        long n = pridej + d.result;
        var result2 = Update(data, table, sloupecKUpdate, n, sloupecID, id);
        return InstancesSqlResult.Long(n, result2);
    }
    #endregion

    #region Update {Op} RealValue
    /// <summary>
    /// Pokud se řádek nepodaří najít, vrátí -1
    /// </summary>
    /// <param name="table"></param>
    /// <param name="sloupecID"></param>
    /// <param name="id"></param>
    /// <param name="sloupecKUpdate"></param>
    /// <param name="pridej"></param>
    public SqlResult<float> UpdatePlusRealValue(SqlData data, string table, string sloupecKUpdate, float pridej, string sloupecID, int id)
    {
        data.signed = true;
        var d2 = SelectCellDataTableFloatOneRow(data, table, sloupecKUpdate, AB.Get(sloupecID, id));
        var d = d2.result;
        if (d != 0 && d != -1 && d != float.MaxValue)
        {
            pridej = (d + pridej) / 2;

        }
        else
        {
            // Zde to má být prázdné
            pridej = -1;
        }
        var result = Update(data, table, sloupecKUpdate, pridej, sloupecID, id);
        return InstancesSqlResult.Float(pridej, result);
    } 
    #endregion

    #region Update {Op} ShortValue
    #region UpdateMinusShortValue
    /// <summary>
    /// Nahrazuje ve všech řádcích
    /// </summary>
    /// <param name="table"></param>
    /// <param name="sloupecKUpdate"></param>
    /// <param name="odeber"></param>
    /// <param name="abc"></param>
    public SqlResult<short> UpdateMinusShortValue(SqlData data, string table, string sloupecKUpdate, short odeber, params AB[] abc)
    {
        var d2 = SelectCellDataTableShortOneRow(data, table, sloupecKUpdate, abc);
        var d = d2.result;
        if (d == short.MinValue)
        {
            return InstancesSqlResult.Short(d, d2);
        }

        odeber = (short)(d - odeber);
        var result = Update(data, table, sloupecKUpdate, odeber, abc);
        return InstancesSqlResult.Short(odeber, result);
    }
    public SqlResult<short> UpdateMinusShortValue(SqlData data, string table, string sloupecKUpdate, short odeber, string sloupecID, object hodnotaID)
    {
        var d2 = SelectCellDataTableShortOneRow(data, table, sloupecKUpdate, sloupecID, hodnotaID);
        var d = d2.result;
        if (d == short.MaxValue)
        {
            odeber = 0;
        }
        else
        {
            odeber = (short)(d - odeber);
        }
        var result = Update(data, table, sloupecKUpdate, odeber, sloupecID, hodnotaID);
        return InstancesSqlResult.Short(odeber, result);
    }
    #endregion

    #region UpdatePlusShortValue
    public SqlResult<short> UpdatePlusShortValue(SqlData data, string table, string sloupecKUpdate, short pridej, string sloupecID, object hodnotaID)
    {
        var d2 = SelectCellDataTableShortOneRow(data, table, sloupecKUpdate, sloupecID, hodnotaID);
        var d = d2.result;

        if (d == short.MaxValue)
        {
            return InstancesSqlResult.Short(d, d2);
        }
        short n = pridej;
        n = (short)(d + pridej);
        //}
        var result = Update(data, table, sloupecKUpdate, n, sloupecID, hodnotaID);
        return InstancesSqlResult.Short(n, result);
    } 
    #endregion

    #region UpdateMinusNormalizedShortValue
    /// <summary>
    /// Vrací normalizovaný short
    /// </summary>
    /// <param name="table"></param>
    /// <param name="sloupecKUpdate"></param>
    /// <param name="odeber"></param>
    /// <param name="abc"></param>
    public SqlResult<ushort> UpdateMinusNormalizedShortValue(SqlData data, string table, string sloupecKUpdate, ushort odeber, params AB[] abc)
    {
        var d2 = SelectCellDataTableShortOneRow(data, table, sloupecKUpdate, abc);
        ushort d = NormalizeNumbers.NormalizeShort(d2.result);
        if (d == NormalizeNumbers.NormalizeShort(short.MinValue))
        {
            return InstancesSqlResult.Ushort(d, d2);
        }

        odeber = (ushort)(d - odeber);
        var update = Update(data, table, sloupecKUpdate, odeber, abc);
        return InstancesSqlResult.Ushort(odeber, update);
    }
    #endregion 
    #endregion

    #region Update {Op} IntValue
    #region UpdatePlusIntValue
    public SqlResult<int> UpdatePlusIntValue(SqlData data, string table, string sloupecKUpdate, int pridej, params AB[] abc)
    {
        var d2 = SelectCellDataTableIntOneRow(data, table, sloupecKUpdate, abc);
        var d = d2.result;
        // Check for signed is useless - in signed or not always return maxValue
        if (d == int.MaxValue)
        {
            return d2;
        }
        int n = pridej;
        n = d + pridej;
        var r = Update(data, table, sloupecKUpdate, n, abc);

        return InstancesSqlResult.Int(n, r);
    }

    public SqlResult<int> UpdatePlusIntValue(SqlData data, string table, string sloupecKUpdate, int pridej, string sloupecID, object hodnotaID)
    {
        var d2 = SelectCellDataTableIntOneRow(data, table, sloupecKUpdate, sloupecID, hodnotaID);
        var d = d2.result;

        if (d == int.MaxValue)
        {
            return InstancesSqlResult.Int(d, d2);
        }
        int n = pridej;
        n = d + pridej;
        var sqlResult = Update(data, table, sloupecKUpdate, n, sloupecID, hodnotaID);


        return InstancesSqlResult.Int(n, sqlResult);
    }
    #endregion

    #region UpdateMinusIntValue
    /// <summary>
    /// Aktualizuje všechny řádky
    /// Vrátí novou zapsanou hodnotu
    /// </summary>
    /// <param name="table"></param>
    /// <param name="sloupecKUpdate"></param>
    /// <param name="odeber"></param>
    /// <param name="abc"></param>
    public SqlResult<int> UpdateMinusIntValue(SqlData data, string table, string sloupecKUpdate, int odeber, params AB[] abc)
    {
        var d2 = SelectCellDataTableIntOneRow(data, table, sloupecKUpdate, abc);
        var d = d2.result;

        if (d == int.MaxValue)
        {
            return InstancesSqlResult.Int(d, d2);
        }
        int n = odeber;
        n = d - odeber;

        var result = Update(data, table, sloupecKUpdate, n, abc);
        return InstancesSqlResult.Int(n, result);
    }

    public SqlResult<int> UpdateMinusIntValue(SqlData data, string table, string sloupecKUpdate, int odeber, string sloupecID, object hodnotaID)
    {
        data.signed = true;
        var d2 = SelectCellDataTableIntOneRow(data, table, sloupecKUpdate, sloupecID, hodnotaID);
        var d = d2.result;
        if (d == int.MinValue)
        {
            return InstancesSqlResult.Int(d, d2);
        }
        int n = odeber;
        n = d - odeber;

        var result = Update(data, table, sloupecKUpdate, n, sloupecID, hodnotaID);
        return InstancesSqlResult.Int(n, result);
    }
    #endregion
    #endregion

    #region Update {Op} LongValue
    public SqlResult<long> UpdateMinusLongValue(SqlData data, string table, string sloupecKUpdate, long odeber, string sloupecID, object hodnotaID)
    {
        var d2 = SelectCellDataTableLongOneRow(data, table, sloupecKUpdate, sloupecID, hodnotaID);
        var d = d2.result;

        if (d == long.MaxValue)
        {
            return InstancesSqlResult.Long(d, d2);
        }
        long n = odeber;
        n = d - odeber;

        var result = Update(data, table, sloupecKUpdate, n, sloupecID, hodnotaID);
        return InstancesSqlResult.Long(n, result);
    }
    #endregion

    #region Update {Op} ByteValue
    public SqlResult<byte> UpdateMinusByteValue(SqlData data, string table, string sloupecKUpdate, byte pridej, string sloupecID, object hodnotaID)
    {
        var d2 = SelectCellDataTableByteOneRow(data, table, sloupecKUpdate, sloupecID, hodnotaID);
        var d = d2.result;
        if (d == 255)
        {
            pridej = d;
        }
        else
        {
            pridej = (byte)(d + pridej);
        }
        var result = Update(data, table, sloupecKUpdate, pridej, sloupecID, hodnotaID);
        return InstancesSqlResult.Byte(pridej, result);
    }
    #endregion

    #region Update string
    /// <summary>
    /// Pouze když hodnota nebude existovat, přidá ji znovu
    /// </summary>
    /// <param name="tableName"></param>
    /// <param name="sloupecID"></param>
    /// <param name="hodnotaID"></param>
    /// <param name="sloupecAppend"></param>
    /// <param name="hodnotaAppend"></param>
    public SqlResult<string> UpdateAppendStringValueCheckExistsOneRow(SqlData data, string tableName, string sloupecAppend, string hodnotaAppend, string sloupecID, object hodnotaID)
    {
        var aktual2 = SelectCellDataTableStringOneRow(data, tableName, sloupecAppend, sloupecID, hodnotaID);
        var aktual = aktual2.result;

        List<string> d = new List<string>(SH.Split(aktual, ","));
        if (!d.Contains(hodnotaAppend))
        {
            aktual += hodnotaAppend + ",";
            string save = SH.Join(',', d.ToArray());

            var update = Update(data, tableName, sloupecAppend, aktual, sloupecID, hodnotaID);
            return InstancesSqlResult.String(save, update);
        }
        return InstancesSqlResult.String(string.Empty, aktual2);
    }

    /// <summary>
    /// 
    /// </summary>
    public SqlResult<string> UpdateCutStringValue(SqlData data, string tableName, string sloupecCut, string hodnotaCut, string sloupecID, object hodnotaID)
    {
        var aktual2 = SelectCellDataTableStringOneRow(data, tableName, sloupecCut, sloupecID, hodnotaID);
        string aktual = aktual2.result;

        List<string> d = new List<string>(SH.Split(aktual, ","));
        d.Remove(hodnotaCut);
        string save = SH.JoinWithoutTrim(",", d);
        var update = Update(data, tableName, sloupecCut, save, sloupecID, hodnotaID);
        return InstancesSqlResult.String(save, update);
    }

    /// <summary>
    /// 
    /// </summary>
    public SqlResult<string> UpdateAppendStringValue(SqlData data, string tableName, string sloupecAppend, string hodnotaAppend, string sloupecID, object hodnotaID)
    {
        var s = SelectCellDataTableStringOneRow(data, tableName, sloupecAppend, sloupecID, hodnotaID); ;

        var aktual = s.result;
        aktual += hodnotaAppend;
        var result = Update(data, tableName, sloupecAppend, aktual, sloupecID, hodnotaID);
        return InstancesSqlResult.String(aktual, result);
    } 
    #endregion

    #region Update
    /// <summary>
    /// Conn nastaví automaticky
    /// </summary>
    public SqlResult Update(SqlData d, string table, string sloupecKUpdate, object n, string sloupecID, object id)
    {
        string sql = string.Format("UPDATE {0} SET {1}=@p0 WHERE {2} = @p1", table, sloupecKUpdate, sloupecID);
        SqlCommand comm = new SqlCommand(sql);
        AddCommandParameter(comm, 0, n);
        AddCommandParameter(comm, 1, id);
        //Sql String or binary data would be truncated.
        return ExecuteNonQuery(d, comm);
    }

    public SqlResult Update(SqlData d, string table, string sloupecKUpdate, int n, ABC abc)
    {
        int parametrSet = abc.Length;
        string sql = string.Format("UPDATE {0} SET {1}=@p" + parametrSet + " {2}", table, sloupecKUpdate, GeneratorMsSql.CombinedWhere(abc));
        SqlCommand comm = new SqlCommand(sql);
        AddCommandParameter(comm, parametrSet, n);
        for (int i = 0; i < parametrSet; i++)
        {
            AddCommandParameter(comm, i, abc[i].B);
        }
        var vr = ExecuteNonQuery(d, comm);
        return vr;
    }

    /// <summary>
    /// Return count of rows affected
    /// </summary>
    /// <param name="table"></param>
    /// <param name="columnToUpdate"></param>
    /// <param name="newValue"></param>
    /// <param name="abc"></param>
    public SqlResult Update(SqlData d, string table, string columnToUpdate, object newValue, params AB[] abc)
    {
        int parametrSet = abc.Length;
        string sql = string.Format("UPDATE {0} SET {1}=@p" + parametrSet + " {2}", table, columnToUpdate, GeneratorMsSql.CombinedWhere(abc));
        SqlCommand comm = new SqlCommand(sql);
        AddCommandParameter(comm, parametrSet, newValue);

        string table2, column;
        table2 = column = null;

        List<string> columns = new List<string>();
        List<object> values = new List<object>();

        for (int i = 0; i < parametrSet; i++)
        {
            var o = abc[i].B;
            AddCommandParameter(comm, i, o, ref table2, ref column);
            values.Add(o);
            columns.Add(column);
        }

        var vr = ExecuteNonQuery(new SqlData { columns = columns, table = table2, values = values }, comm);
        return vr;
    } 
    #endregion

    #region UpdateValuesCombination
    /// <summary>
    /// Conn nastaví automaticky
    /// Set more values A5 into single row identified by A3,4
    /// </summary>
    public SqlResult UpdateValuesCombination(SqlData d, string TableName, string nameOfColumn, object valueOfColumn, params AB[] sets)
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
        return ExecuteNonQuery(d, comm);
    }

    /// <summary>
    /// Set more values A3 into single row identified by A4
    /// </summary>
    /// <param name="d"></param>
    /// <param name="TableName"></param>
    /// <param name="sets"></param>
    /// <param name="where"></param>
    public SqlResult UpdateValuesCombinationCombinedWhere(SqlData d, string TableName, ABC sets, ABC where)
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
        var result = ExecuteNonQuery(d, comm);
        return result;
    }
    #endregion
    #endregion
}
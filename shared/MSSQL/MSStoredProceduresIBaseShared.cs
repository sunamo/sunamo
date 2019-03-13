using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using sunamo;
using sunamo.Values;

public partial class MSStoredProceduresIBase{ 
public void RepairConnection()
    {
        SqlConnection.ClearAllPools();
        conn.Close();
    }

    SqlConnection _conn = null;
    public SqlConnection conn
    {
        get
        {
            if (_conn == null)
            {
                return MSDatabaseLayer.conn;
            }

            return _conn;
        }

        set
        {
            _conn = value;
        }
    }

    public MSStoredProceduresIBase(SqlConnection conn)
    {
        this.conn = conn;
    }

public bool SelectExistsTable(string p)
    {
        DataTable dt = SelectDataTable(conn, string.Format("SELECT * FROM sysobjects WHERE id = object_id(N'{0}') AND OBJECTPROPERTY(id, N'IsUserTable') = 1", p));
        return dt.Rows.Count != 0;
    }
public bool SelectExistsTable(string p, SqlConnection conn)
    {
        DataTable dt = SelectDataTable(conn, string.Format("SELECT * FROM sysobjects WHERE id = object_id(N'{0}') AND OBJECTPROPERTY(id, N'IsUserTable') = 1", p));
        return dt.Rows.Count != 0;
    }

/// <summary>
    /// Conn nastaví automaticky
    /// </summary>
    /// <param name = "sql"></param>
    /// <returns></returns>
    public DataTable SelectDataTable(SqlCommand comm)
    {
        DataTable dt = new DataTable();
        comm.Connection = conn;
        SqlDataAdapter adapter = new SqlDataAdapter(comm);
        adapter.Fill(dt);
        return dt;
    }
/// <summary>
    /// A1 jsou hodnoty bez převedení AddCommandParameter nebo ReplaceValueOnlyOne
    /// Conn nastaví automaticky
    /// </summary>
    /// <param name = "sql"></param>
    /// <param name = "_params"></param>
    /// <returns></returns>
    private DataTable SelectDataTable(string sql, params object[] _params)
    {
        _params = CA.TwoDimensionParamsIntoOne(_params);
        SqlCommand comm = new SqlCommand(sql);
        _params = CA.TwoDimensionParamsIntoOne(_params);
        for (int i = 0; i < _params.Length; i++)
        {
            AddCommandParameter(comm, i, _params[i]);
        }

        return SelectDataTable(comm);
    //return SelectDataTable(string.Format(sql, _params));
    }
private DataTable SelectDataTable(SqlConnection conn, string sql, params object[] _params)
    {
        _params = CA.TwoDimensionParamsIntoOne(_params);
        SqlCommand comm = new SqlCommand(sql);
        for (int i = 0; i < _params.Length; i++)
        {
            AddCommandParameter(comm, i, _params[i]);
        }

        return SelectDataTable(conn, comm);
    //return SelectDataTable(string.Format(sql, _params));
    }
public DataTable SelectDataTable(SqlConnection conn, SqlCommand comm)
    {
        DataTable dt = new DataTable();
        comm.Connection = conn;
        SqlDataAdapter adapter = new SqlDataAdapter(comm);
        adapter.Fill(dt);
        return dt;
    }

/// <summary>
    /// a2 je X jako v příkazu @pX
    /// </summary>
    /// <param name = "comm"></param>
    /// <param name = "i"></param>
    /// <param name = "o"></param>
    public static int AddCommandParameter(SqlCommand comm, int i, object o)
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
        else if (o.GetType() == Consts.tString || o.GetType() == Consts.tChar)
        {
            string _ = o.ToString();
            comm.Parameters.AddWithValue("@p" + i.ToString(), SqlServerHelper.ConvertToVarChar(_));
        }
        else
        {
            comm.Parameters.AddWithValue("@p" + i.ToString(), o);
        }

        ++i;
        return i;
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
private int Update(string table, string sloupecKUpdate, int n, AB[] abc)
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

public int ExecuteNonQuery(SqlCommand comm)
    {
        comm.Connection = conn;
        return comm.ExecuteNonQuery();
    }
public int ExecuteNonQuery(string commText, params object[] para)
    {
        para = CA.TwoDimensionParamsIntoOne(para);
        SqlCommand comm = new SqlCommand(commText);
        for (int i = 0; i < para.Length; i++)
        {
            AddCommandParameter(comm, i, para[i]);
        }

        return ExecuteNonQuery(comm);
    }

/// <summary>
    /// Vrátí 0 pokud takový řádek nebude nalezen.
    /// </summary>
    public byte SelectCellDataTableByteOneRow(string table, string vracenySloupec, params AB[] where)
    {
        string sql = GeneratorMsSql.SimpleSelectOneRow(vracenySloupec, table);
        sql += GeneratorMsSql.CombinedWhere(where);
        SqlCommand comm = new SqlCommand(sql);
        AddCommandParameterFromAbc(comm, where);
        return ExecuteScalarByte(comm);
    }

    /// <summary>
    /// Počítá od nuly
    /// </summary>
    /// <param name = "comm"></param>
    /// <param name = "where"></param>
    private static void AddCommandParameterFromAbc(SqlCommand comm, params AB[] where)
    {
        for (int i = 0; i < where.Length; i++)
        {
            AddCommandParameter(comm, i, where[i].B);
        }
    }

    /// <summary>
    /// Automaticky doplní connection
    /// </summary>
    /// <param name = "comm"></param>
    /// <returns></returns>
    public object ExecuteScalar(SqlCommand comm)
    {
        //SqlDbType.SmallDateTime;
        comm.Connection = conn;
        return comm.ExecuteScalar();
    }

    private byte ExecuteScalarByte(SqlCommand comm)
    {
        object o = ExecuteScalar(comm);
        if (o == null)
        {
            return 0;
        }

        return Convert.ToByte(o);
    }


    /// <summary>
    /// Vrátí 0 pokud takový řádek nebude nalezen.
    /// </summary>
    /// <param name = "table"></param>
    /// <param name = "idColumnName"></param>
    /// <param name = "idColumnValue"></param>
    /// <param name = "vracenySloupec"></param>
    /// <returns></returns>
    public byte SelectCellDataTableByteOneRow(string table, string idColumnName, object idColumnValue, string vracenySloupec)
    {
        string sql = GeneratorMsSql.SimpleWhereOneRow(vracenySloupec, table, idColumnName);
        SqlCommand comm = new SqlCommand(sql);
        AddCommandParameter(comm, 0, idColumnValue);
        return ExecuteScalarByte(comm);
    }

/// <summary>
    /// Používej místo této M metodu SelectValuesOfColumnAllRowsInt která je úplně stejná
    /// </summary>
    /// <param name = "tabulka"></param>
    /// <param name = "sloupecHledaný"></param>
    /// <param name = "abc"></param>
    /// <returns></returns>
    public List<int> SelectValuesOfColumnInt(bool signed, string tabulka, string sloupecHledaný, params AB[] abc)
    {
        SqlCommand comm = new SqlCommand(string.Format("SELECT {0} FROM {1} {2}", sloupecHledaný, tabulka, GeneratorMsSql.CombinedWhere(abc)));
        for (int i = 0; i < abc.Length; i++)
        {
            AddCommandParameter(comm, i, abc[i].B);
        }

        return ReadValuesInt(comm);
    }
/// <summary>
    /// Používej místo této M metodu SelectValuesOfColumnAllRowsInt, která je úplně stejná
    /// </summary>
    /// <param name = "tabulka"></param>
    /// <param name = "sloupecHledaný"></param>
    /// <param name = "sloupecVeKteremHledat"></param>
    /// <param name = "hodnota"></param>
    /// <returns></returns>
    public List<int> SelectValuesOfColumnInt(bool signed, string tabulka, string sloupecHledaný, string sloupecVeKteremHledat, object hodnota)
    {
        SqlCommand comm = new SqlCommand(string.Format("SELECT {0} FROM {1} WHERE {2} = @p0", sloupecHledaný, tabulka, sloupecVeKteremHledat));
        AddCommandParameter(comm, 0, hodnota);
        return ReadValuesInt(comm);
    }

    private SqlDataReader ExecuteReader(SqlCommand comm)
    {
        comm.Connection = conn;
        return comm.ExecuteReader(CommandBehavior.Default);
    }

    private List<int> ReadValuesInt(SqlCommand comm)
    {
        List<int> vr = new List<int>();
        SqlDataReader r = null;
        r = ExecuteReader(comm);
        if (r.HasRows)
        {
            while (r.Read())
            {
                int o = r.GetInt32(0);
                //Type t = val.GetType();
                vr.Add(o);
            }
        }

        return vr;
    }

    /// <summary>
    /// Maže všechny řádky, ne jen jeden.
    /// </summary>
    public int Delete(string table, string sloupec, object id)
    {
        return ExecuteNonQuery(string.Format("DELETE FROM {0} WHERE {1} = @p0", table, sloupec), id);
    }
/// <summary>
    /// Conn nastaví automaticky
    /// Vrátí zda byl vymazán alespoň jeden řádek
    /// 
    /// </summary>
    /// <param name = "TableName"></param>
    /// <param name = "where"></param>
    /// <returns></returns>
    public int Delete(string TableName, params AB[] where)
    {
        string whereS = GeneratorMsSql.CombinedWhere(where);
        SqlCommand comm = new SqlCommand("DELETE FROM " + TableName + whereS);
        AddCommandParameterFromAbc(comm, where);
        int f = ExecuteNonQuery(comm);
        return f;
    }

/// <summary>
    /// Pokud bude buňka DBNull, nebudu ukládat do G nic
    /// </summary>
    /// <param name = "table"></param>
    /// <param name = "returnColumns"></param>
    /// <param name = "where"></param>
    /// <returns></returns>
    public List<DateTime> SelectValuesOfColumnAllRowsDateTime(string table, string returnColumns, params AB[] where)
    {
        string hodnoty = MSDatabaseLayerBase.GetValues(where.ToArray());
        List<DateTime> vr = new List<DateTime>();
        SqlCommand comm = new SqlCommand(string.Format("SELECT {0} FROM {1} {2}", returnColumns, table, GeneratorMsSql.CombinedWhere(where)));
        for (int i = 0; i < where.Length; i++)
        {
            AddCommandParameter(comm, i, where[i].B);
        }

        return ReadValuesDateTime(comm);
    }

    private List<DateTime> ReadValuesDateTime(SqlCommand comm)
    {
        List<DateTime> vr = new List<DateTime>();
        SqlDataReader r = ExecuteReader(comm);
        if (r.HasRows)
        {
            while (r.Read())
            {
                DateTime o = r.GetDateTime(0);
                //Type t = val.GetType();
                vr.Add(o);
            }
        }

        return vr;
    }

    /// <summary>
    /// Pokud chceš použít OrderBy, je tu metoda SelectDataTableLimitLastRows nebo SelectDataTableLimitLastRowsInnerJoin
    /// Conn nastaví automaticky
    /// Vrátí prázdnou tabulku pokud se nepodaří žádný řádek najít
    /// Vyplň A2 na SE pokud chceš všechny sloupce
    /// </summary>
    public DataTable SelectDataTableSelective(string tabulka, string nazvySloupcu, params AB[] ab)
    {
        SqlCommand comm = new SqlCommand(string.Format("SELECT {0} FROM {1}", nazvySloupcu, tabulka) + GeneratorMsSql.CombinedWhere(ab));
        AddCommandParameterFromAbc(comm, ab);
        //NT
        return this.SelectDataTable(comm);
    }
/// <summary>
    /// Conn nastaví automaticky
    /// Vrátí prázdnou tabulku pokud se nepodaří žádný řádek najít
    /// </summary>
    public DataTable SelectDataTableSelective(string tabulka, string nazvySloupcu, string sloupecID, object id)
    {
        SqlCommand comm = new SqlCommand(string.Format("SELECT {0} FROM {1} WHERE {2} = @p0", nazvySloupcu, tabulka, sloupecID));
        AddCommandParameter(comm, 0, id);
        //NT
        return this.SelectDataTable(comm);
    }
public DataTable SelectDataTableSelective(string tabulka, string nazvySloupcu, string sloupecID, object id, string orderByColumn, SortOrder sortOrder)
    {
        SqlCommand comm = new SqlCommand(string.Format("SELECT {0} FROM {1} WHERE {2} = @p0", nazvySloupcu, tabulka, sloupecID) + GeneratorMsSql.OrderBy(orderByColumn, sortOrder));
        AddCommandParameter(comm, 0, id);
        //NT
        return this.SelectDataTable(comm);
    }
public DataTable SelectDataTableSelective(string table, string vraceneSloupce, AB[] where, AB[] whereIsNot)
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
        DataTable dt = SelectDataTable(comm);
        return dt;
    }
public DataTable SelectDataTableSelective(string table, string vraceneSloupce, AB[] where, AB[] whereIsNot, AB[] greaterThan, AB[] lowerThan)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("SELECT " + vraceneSloupce);
        sb.Append(" FROM " + table);
        sb.Append(GeneratorMsSql.CombinedWhere(where, whereIsNot, greaterThan, lowerThan));
        //string sql = GeneratorMsSql.SimpleWhereOneRow(vracenySloupec, table, idColumnName);
        SqlCommand comm = new SqlCommand(sb.ToString());
        AddCommandParameteresArrays(comm, 0, where, whereIsNot, greaterThan, lowerThan);
        //AddCommandParameter(comm, 0, idColumnValue);
        DataTable dt = SelectDataTable(comm);
        return dt;
    }

    /// <summary>
    /// Bude se počítat od nuly
    /// Některé z vnitřních polí může být null
    /// </summary>
    /// <param name = "comm"></param>
    /// <param name = "where"></param>
    /// <param name = "whereIsNot"></param>
    private static void AddCommandParameteresArrays(SqlCommand comm, int i, params AB[][] where)
    {
        //int i = 0;
        foreach (var item in where)
        {
            if (item != null)
            {
                foreach (var item2 in item)
                {
                    i = AddCommandParameter(comm, i, item2.B);
                }
            }
        }
    }

    /// <summary>
    /// Libovolné z hodnot A2 až A5 může být null, protože se to postupuje metodě AddCommandParameteresArrays
    /// </summary>
    /// <param name = "comm"></param>
    /// <param name = "where"></param>
    /// <param name = "isNotWhere"></param>
    /// <param name = "greaterThanWhere"></param>
    /// <param name = "lowerThanWhere"></param>
    private static void AddCommandParameteresCombinedArrays(SqlCommand comm, int i, AB[] where, AB[] isNotWhere, AB[] greaterThanWhere, AB[] lowerThanWhere)
    {
        AddCommandParameteresArrays(comm, i, CA.ToArrayT<AB[]>(where, isNotWhere, greaterThanWhere, lowerThanWhere));
    }

/// <summary>
    /// Vykonává metodou ExecuteScalar. Ta pokud vrátí null, metoda vrátí "". To je taky rozdíl oproti metodě SelectCellDataTableStringOneRowABC.
    /// </summary>
    /// <param name = "tabulka"></param>
    /// <param name = "nazvySloupcu"></param>
    /// <param name = "ab"></param>
    /// <returns></returns>
    public string SelectCellDataTableStringOneRow(string tabulka, string nazvySloupcu, params AB[] ab)
    {
        SqlCommand comm = new SqlCommand(GeneratorMsSql.CombinedWhere(tabulka, true, nazvySloupcu, ab));
        AddCommandParameterFromAbc(comm, ab);
        return ExecuteScalarString(comm);
    }
/// <summary>
    /// Vrátí SE v případě že řádek nebude nalezen, nikdy nevrací null.
    /// Automaticky vytrimuje
    /// </summary>
    /// <param name = "table"></param>
    /// <param name = "idColumnName"></param>
    /// <param name = "idColumnValue"></param>
    /// <param name = "vracenySloupec"></param>
    /// <returns></returns>
    public string SelectCellDataTableStringOneRow(string table, string vracenySloupec, string idColumnName, object idColumnValue)
    {
        string sql = GeneratorMsSql.SimpleWhereOneRow(vracenySloupec, table, idColumnName);
        SqlCommand comm = new SqlCommand(sql);
        AddCommandParameter(comm, 0, idColumnValue);
        return ExecuteScalarString(comm);
    }
/// <summary>
    /// A4 může být null, A3 nikoliv
    /// </summary>
    /// <param name = "table"></param>
    /// <param name = "vracenySloupec"></param>
    /// <param name = "where"></param>
    /// <param name = "whereIsNot"></param>
    /// <returns></returns>
    public string SelectCellDataTableStringOneRow(string table, string vracenySloupec, AB[] where, AB[] whereIsNot)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("SELECT TOP(1) " + vracenySloupec);
        sb.Append(" FROM " + table);
        int dd = 0;
        sb.Append(GeneratorMsSql.CombinedWhere(where, ref dd));
        sb.Append(GeneratorMsSql.CombinedWhereNotEquals(where.Length != 0, ref dd, whereIsNot));
        //string sql = GeneratorMsSql.SimpleWhereOneRow(vracenySloupec, table, idColumnName);
        SqlCommand comm = new SqlCommand(sb.ToString());
        AddCommandParameteresArrays(comm, 0, where, whereIsNot);
        return ExecuteScalarString(comm);
    }

public IList SelectValuesOfColumnAllRowsNumeric(string tabulka, string sloupec, params AB[] ab)
    {
        SqlCommand comm = new SqlCommand(string.Format("SELECT TOP(1) {0} FROM {1}", sloupec, tabulka));
        object[] o = SelectRowReader(comm);
        if (o == null)
        {
            return new List<long>();
        }

        Type t = o[0].GetType();
        comm = new SqlCommand(string.Format("SELECT {0} FROM {1}", sloupec, tabulka) + GeneratorMsSql.CombinedWhere(ab));
        AddCommandParameteres(comm, 0, ab);
        if (t == Consts.tInt)
        {
            //snt = SqlNumericType.Int;
            return ReadValuesInt(comm);
        }
        else if (t == Consts.tLong)
        {
            //snt = SqlNumericType.Long;
            return ReadValuesLong(comm);
        }
        else if (t == Consts.tShort)
        {
            //snt = SqlNumericType.Short;
            return ReadValuesShort(comm);
        }

        return ReadValuesByte(comm);
    }
public IList SelectValuesOfColumnAllRowsNumeric(string tabulka, string sloupec)
    {
        return SelectValuesOfColumnAllRowsNumeric(tabulka, sloupec, new AB[0]);
    }

/// <summary>
    /// 2
    /// </summary>
    public DataTable SelectAllRowsOfColumns(string p, string selectSloupce)
    {
        return SelectDataTable(string.Format("SELECT {0} FROM {1}", selectSloupce, p));
    }
public DataTable SelectAllRowsOfColumns(string p, string ziskaneSloupce, string idColumnName, object idColumnValue)
    {
        SqlCommand comm = new SqlCommand(string.Format("SELECT {0} FROM {1} ", ziskaneSloupce, p) + GeneratorMsSql.SimpleWhere(idColumnName));
        AddCommandParameter(comm, 0, idColumnValue);
        return SelectDataTable(comm);
    }
public DataTable SelectAllRowsOfColumns(string p, string ziskaneSloupce, params AB[] ab)
    {
        SqlCommand comm = new SqlCommand(string.Format("SELECT {0} FROM {1} ", ziskaneSloupce, p) + GeneratorMsSql.CombinedWhere(ab));
        AddCommandParameteres(comm, 0, ab);
        return SelectDataTable(comm);
    }

/// <summary>
    /// 
    /// </summary>
    public bool SelectExists(string tabulka, string sloupec, object hodnota)
    {
        string sql = string.Format("SELECT TOP(1) {0} FROM {1} {2}", sloupec, tabulka, GeneratorMsSql.SimpleWhere(sloupec));
        return ExecuteScalar(sql, hodnota) != null;
    }

public int DeleteOneRow(string table, string sloupec, object id)
    {
        return ExecuteNonQuery(string.Format("DELETE TOP(1) FROM {0} WHERE {1} = @p0", table, sloupec), id);
    }
public bool DeleteOneRow(string TableName, params AB[] where)
    {
        string whereS = GeneratorMsSql.CombinedWhere(where);
        SqlCommand comm = new SqlCommand("DELETE TOP(1) FROM " + TableName + whereS);
        AddCommandParameterFromAbc(comm, where);
        int f = ExecuteNonQuery(comm);
        return f == 1;
    }
    public object ExecuteScalar(string commText, params object[] para)
    {
        para = CA.TwoDimensionParamsIntoOne(para);
        SqlCommand comm = new SqlCommand(commText);
        for (int i = 0; i < para.Length; i++)
        {
            AddCommandParameter(comm, i, para[i]);
        }

        return ExecuteScalar(comm);
    }
    public long SelectCount(string table)
    {
        return Convert.ToInt64(ExecuteScalar("SELECT COUNT(*) FROM " + table));
    }
public long SelectCount(string table, params AB[] abc)
    {
        SqlCommand comm = new SqlCommand("SELECT COUNT(*) FROM " + table + GeneratorMsSql.CombinedWhere(abc));
        AddCommandParameteres(comm, 0, abc);
        return Convert.ToInt64(ExecuteScalar(comm));
    }

public int UpdatePlusIntValue(string table, string sloupecKUpdate, int pridej, params AB[] abc)
    {
        int d = SelectCellDataTableIntOneRow(true, table, sloupecKUpdate, abc);
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

public DateTime SelectCellDataTableDateTimeOneRow(string table, string vracenySloupec, string idColumnName, object idColumnValue, DateTime getIfNotFound)
    {
        string sql = GeneratorMsSql.SimpleWhereOneRow(vracenySloupec, table, idColumnName);
        SqlCommand comm = new SqlCommand(sql);
        AddCommandParameter(comm, 0, idColumnValue);
        return ExecuteScalarDateTime(getIfNotFound, comm);
    }
/// <summary>
    /// Vrátí DateTimeMinVal, pokud takový řádek nebude nalezen.
    /// </summary>
    /// <param name = "table"></param>
    /// <param name = "vracenySloupec"></param>
    /// <param name = "where"></param>
    /// <param name = "whereIsNot"></param>
    /// <returns></returns>
    public DateTime SelectCellDataTableDateTimeOneRow(string table, string vracenySloupec, DateTime getIfNotFound, AB[] where, AB[] whereIsNot)
    {
        int dd = 0;
        string sql = GeneratorMsSql.SimpleSelectOneRow(vracenySloupec, table) + GeneratorMsSql.CombinedWhere(where, ref dd) + GeneratorMsSql.CombinedWhereNotEquals(true, ref dd, whereIsNot);
        SqlCommand comm = new SqlCommand(sql);
        //AddCommandParameter(comm, 0, idColumnValue);
        AddCommandParameteresArrays(comm, 0, where, whereIsNot);
        return ExecuteScalarDateTime(getIfNotFound, comm);
    }

public object SelectCellDataTableObjectOneRow(string table, string idColumnName, object idColumnValue, string vracenySloupec)
    {
        string sql = GeneratorMsSql.SimpleWhereOneRow(vracenySloupec, table, idColumnName);
        SqlCommand comm = new SqlCommand(sql);
        AddCommandParameter(comm, 0, idColumnValue);
        return ExecuteScalar(comm);
    }

/// <summary>
    /// G -1 když se žádný takový řádek nepodaří najít
    /// </summary>
    /// <param name = "table"></param>
    /// <param name = "vracenySloupec"></param>
    /// <param name = "abc"></param>
    /// <returns></returns>
    public int SelectCellDataTableIntOneRow(bool signed, string table, string vracenySloupec, ABC whereIs, ABC whereIsNot)
    {
        string sql = GeneratorMsSql.SimpleSelectOneRow(vracenySloupec, table) + GeneratorMsSql.CombinedWhere(whereIs, whereIsNot, null, null);
        SqlCommand comm = new SqlCommand(sql);
        int dalsi = AddCommandParameterFromAbc(comm, whereIs, 0);
        AddCommandParameterFromAbc(comm, whereIsNot, dalsi);
        return ExecuteScalarInt(signed, comm);
    }
/// <summary>
    /// Vrátí -1 pokud žádný takový řádek nenalezne pokud !A1 enbo int.MaxValue pokud A1
    /// </summary>
    /// <param name = "table"></param>
    /// <param name = "idColumnName"></param>
    /// <param name = "idColumnValue"></param>
    /// <param name = "vracenySloupec"></param>
    /// <returns></returns>
    public int SelectCellDataTableIntOneRow(bool signed, string table, string vracenySloupec, string idColumnName, object idColumnValue)
    {
        string sql = GeneratorMsSql.SimpleWhereOneRow(vracenySloupec, table, idColumnName);
        SqlCommand comm = new SqlCommand(sql);
        AddCommandParameter(comm, 0, idColumnValue);
        return ExecuteScalarInt(signed, comm);
    }
public int SelectCellDataTableIntOneRow(bool signed, string table, string vracenySloupec, params AB[] abc)
    {
        string sql = GeneratorMsSql.SimpleSelectOneRow(vracenySloupec, table) + GeneratorMsSql.CombinedWhere(abc);
        SqlCommand comm = new SqlCommand(sql);
        AddCommandParameterFromAbc(comm, abc);
        return ExecuteScalarInt(signed, comm);
    }

private DateTime ExecuteScalarDateTime(DateTime getIfNotFound, SqlCommand comm)
    {
        object o = ExecuteScalar(comm);
        if (o == null || o == DBNull.Value)
        {
            return getIfNotFound;
        }

        return Convert.ToDateTime(o);
    }

private int ExecuteScalarInt(bool signed, SqlCommand comm)
    {
        object o = ExecuteScalar(comm);
        if (o == null)
        {
            if (signed)
            {
                return int.MaxValue;
            }
            else
            {
                return -1;
            }
        }

        return Convert.ToInt32(o);
    }

public object[] SelectRowReader(string tabulka, string sloupecID, object id, string nazvySloupcu)
    {
        SqlCommand comm = new SqlCommand(string.Format("SELECT TOP(1) {0} FROM {1} WHERE {2} = @p0", nazvySloupcu, tabulka, sloupecID));
        AddCommandParameter(comm, 0, id);
        //NT
        return SelectRowReader(comm);
    }
/// <summary>
    /// Vrátí null, pokud výsledek nebude mít žádné řádky
    /// </summary>
    /// <param name = "comm"></param>
    /// <returns></returns>
    private object[] SelectRowReader(SqlCommand comm)
    {
        SqlDataReader r = ExecuteReader(comm);
        if (r.HasRows)
        {
            object[] o = new object[r.VisibleFieldCount];
            r.Read();
            for (int i = 0; i < r.VisibleFieldCount; i++)
            {
                o[i] = r.GetValue(i);
            }

            return o;
        }

        return null;
    }

private int AddCommandParameteres(SqlCommand comm, int pocIndex, object[] hodnotyOdNuly)
    {
        foreach (var item in hodnotyOdNuly)
        {
            AddCommandParameter(comm, pocIndex, item);
            pocIndex++;
        }

        return pocIndex;
    }
public static int AddCommandParameteres(SqlCommand comm, int pocIndex, AB[] aWhere)
    {
        foreach (var item in aWhere)
        {
            AddCommandParameter(comm, pocIndex, item.B);
            pocIndex++;
        }

        return pocIndex;
    }

private string ExecuteScalarString(SqlCommand comm)
    {
        object o = ExecuteScalar(comm);
        if (o == null)
        {
            return "";
        }
        else if (o == DBNull.Value)
        {
            return "";
        }

        return o.ToString().TrimEnd(' ');
    }

private List<short> ReadValuesShort(SqlCommand comm)
    {
        List<short> vr = new List<short>();
        SqlDataReader r = ExecuteReader(comm);
        if (r.HasRows)
        {
            while (r.Read())
            {
                short o = r.GetInt16(0);
                //Type t = val.GetType();
                vr.Add(o);
            }
        }

        return vr;
    }

private List<long> ReadValuesLong(SqlCommand comm)
    {
        List<long> vr = new List<long>();
        SqlDataReader r = ExecuteReader(comm);
        if (r.HasRows)
        {
            while (r.Read())
            {
                long o = r.GetInt64(0);
                //Type t = val.GetType();
                vr.Add(o);
            }
        }

        return vr;
    }

private List<byte> ReadValuesByte(SqlCommand comm)
    {
        List<byte> vr = new List<byte>();
        SqlDataReader r = ExecuteReader(comm);
        ;
        if (r.HasRows)
        {
            while (r.Read())
            {
                byte o = r.GetByte(0);
                //Type t = val.GetType();
                vr.Add(o);
            }
        }

        return vr;
    }

/// <summary>
    /// Počítá od nuly
    /// Mohu volat i s A2 null, v takovém případě se nevykoná žádný kód
    /// </summary>
    /// <param name = "comm"></param>
    /// <param name = "where"></param>
    private static int AddCommandParameterFromAbc(SqlCommand comm, ABC where, int i)
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
}
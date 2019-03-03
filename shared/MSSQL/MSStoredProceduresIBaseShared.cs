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
        string hodnoty = MSDatabaseLayer.GetValues(where.ToArray());
        List<DateTime> vr = new List<DateTime>();
        SqlCommand comm = new SqlCommand(string.Format("SELECT {0} FROM {1} {2}", returnColumns, table, GeneratorMsSql.CombinedWhere(where)));
        for (int i = 0; i < where.Length; i++)
        {
            AddCommandParameter(comm, i, where[i].B);
        }

        return ReadValuesDateTime(comm);
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
}
//using sunamo;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.SqlClient;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//public partial class MSStoredProceduresIBase : SqlServerHelper
//{
//    SqlConnection _conn = null;
//    public SqlConnection conn
//    {
//        get
//        {
//            if (_conn == null)
//            {
//                return MSDatabaseLayer.conn;
//            }
//            return _conn;
//        }
//        set
//        {
//            _conn = value;
//        }
//    }

//    public short SelectCellDataTableShortOneRow(bool signed, string table, string vracenySloupec, params AB[] abc)
//    {
//        string sql = GeneratorMsSql.SimpleSelectOneRow(vracenySloupec, table) + GeneratorMsSql.CombinedWhere(abc);
//        SqlCommand comm = new SqlCommand(sql);
//        AddCommandParameterFromAbc(comm, abc);
//        return ExecuteScalarShort(signed, comm);
//    }

//    /// <summary>
//    /// Interně volá metodu SelectRowReader
//    /// </summary>
//    /// <param name="tabulka"></param>
//    /// <param name="sloupecID"></param>
//    /// <param name="id"></param>
//    /// <param name="nazvySloupcu"></param>
//    /// <returns></returns>
//    public object[] SelectSelectiveOneRow(string tabulka, string sloupecID, object id, string nazvySloupcu)
//    {
//        SqlCommand comm = new SqlCommand(SH.Format2("SELECT TOP(1) {0} FROM {1} WHERE {2} = @p0", nazvySloupcu, tabulka, sloupecID));
//        AddCommandParameter(comm, 0, id);
//        //NT
//        return SelectRowReader(comm);
//    }

//    public object[] SelectOneRowForTableRow(string TableName, string nazevSloupce, object hodnotaSloupce)
//    {
//        // Index nemůže být ani pole bajtů ani null takže to je v pohodě
//        DataTable dt = SelectDataTable("SELECT TOP(1) * FROM " + TableName + " WHERE " + nazevSloupce + " = @p0", hodnotaSloupce);
//        if (dt.Rows.Count == 0)
//        {
//            return null; // CA.CreateEmptyArray(pocetSloupcu);
//        }
//        return dt.Rows[0].ItemArray;
//    }





//    public bool SelectExistsTable(string p)
//    {
//        DataTable dt = SelectDataTable(conn, SH.Format2("SELECT * FROM sysobjects WHERE id = object_id(N'{0}') AND OBJECTPROPERTY(id, N'IsUserTable') = 1", p));
//        return dt.Rows.Count != 0;
//    }


//    /// <summary>
//    /// Conn nastaví automaticky
//    /// </summary>
//    public int Update(string table, string sloupecKUpdate, object n, string sloupecID, object id)
//    {
//        string sql = SH.Format2("UPDATE {0} SET {1}=@p0 WHERE {2} = @p1", table, sloupecKUpdate, sloupecID);
//        SqlCommand comm = new SqlCommand(sql);
//        AddCommandParameter(comm, 0, n);
//        AddCommandParameter(comm, 1, id);
//        //SqlException: String or binary data would be truncated.
//        return ExecuteNonQuery(comm);
//    }
//    private int Update(string table, string sloupecKUpdate, int n, AB[] abc)
//    {
//        int parametrSet = abc.Length;
//        string sql = SH.Format2("UPDATE {0} SET {1}=@p" + parametrSet + " {2}", table, sloupecKUpdate, GeneratorMsSql.CombinedWhere(abc));
//        SqlCommand comm = new SqlCommand(sql);
//        AddCommandParameter(comm, parametrSet, n);
//        for (int i = 0; i < parametrSet; i++)
//        {
//            AddCommandParameter(comm, i, abc[i].B);
//        }
//        int vr = ExecuteNonQuery(comm);
//        return vr;
//    }

//    public DataTable SelectDataTableSelective(string tabulka, string nazvySloupcu, string sloupecID, object id, string orderByColumn, SortOrder sortOrder)
//    {
//        SqlCommand comm = new SqlCommand(SH.Format2("SELECT {0} FROM {1} WHERE {2} = @p0", nazvySloupcu, tabulka, sloupecID) + GeneratorMsSql.OrderBy(orderByColumn, sortOrder));
//        AddCommandParameter(comm, 0, id);
//        //NT
//        return this.SelectDataTable(comm);
//    }
//    public DataTable SelectDataTableSelective(string table, string vraceneSloupce, AB[] where, AB[] whereIsNot)
//    {
//        StringBuilder sb = new StringBuilder();
//        sb.Append("SELECT " + vraceneSloupce);
//        sb.Append(" FROM " + table);
//        int dd = 0;
//        sb.Append(GeneratorMsSql.CombinedWhere(where, ref dd));
//        sb.Append(GeneratorMsSql.CombinedWhereNotEquals(where != null, ref dd, whereIsNot));
//        //string sql = GeneratorMsSql.SimpleWhereOneRow(vracenySloupec, table, idColumnName);
//        SqlCommand comm = new SqlCommand(sb.ToString());
//        AddCommandParameteresArrays(comm, 0, where, whereIsNot);
//        //AddCommandParameter(comm, 0, idColumnValue);
//        DataTable dt = SelectDataTable(comm);
//        return dt;
//    }
//    public DataTable SelectDataTableSelective(string table, string vraceneSloupce, AB[] where, AB[] whereIsNot, AB[] greaterThan, AB[] lowerThan)
//    {
//        StringBuilder sb = new StringBuilder();
//        sb.Append("SELECT " + vraceneSloupce);
//        sb.Append(" FROM " + table);
//        sb.Append(GeneratorMsSql.CombinedWhere(where, whereIsNot, greaterThan, lowerThan));

//        //string sql = GeneratorMsSql.SimpleWhereOneRow(vracenySloupec, table, idColumnName);
//        SqlCommand comm = new SqlCommand(sb.ToString());
//        AddCommandParameteresArrays(comm, 0, where, whereIsNot, greaterThan, lowerThan);
//        //AddCommandParameter(comm, 0, idColumnValue);
//        DataTable dt = SelectDataTable(comm);
//        return dt;
//    }

//    /// <summary>
//    /// Pokud chceš použít OrderBy, je tu metoda SelectDataTableLimitLastRows nebo SelectDataTableLimitLastRowsInnerJoin
//    /// Conn nastaví automaticky
//    /// Vrátí prázdnou tabulku pokud se nepodaří žádný řádek najít
//    /// Vyplň A2 na SE pokud chceš všechny sloupce
//    /// </summary>
//    public DataTable SelectDataTableSelective(string tabulka, string nazvySloupcu, params AB[] ab)
//    {
//        SqlCommand comm = new SqlCommand(SH.Format2("SELECT {0} FROM {1}", nazvySloupcu, tabulka) + GeneratorMsSql.CombinedWhere(ab));
//        AddCommandParameterFromAbc(comm, ab);
//        //NT
//        return this.SelectDataTable(comm);
//    }
//    /// <summary>
//    /// Conn nastaví automaticky
//    /// Vrátí prázdnou tabulku pokud se nepodaří žádný řádek najít
//    /// </summary>
//    public DataTable SelectDataTableSelective(string tabulka, string nazvySloupcu, string sloupecID, object id)
//    {
//        SqlCommand comm = new SqlCommand(SH.Format2("SELECT {0} FROM {1} WHERE {2} = @p0", nazvySloupcu, tabulka, sloupecID));
//        AddCommandParameter(comm, 0, id);
//        //NT
//        return this.SelectDataTable(comm);
//    }


//    /// <summary>
//    /// a2 je X jako v příkazu @pX
//    /// </summary>
//    /// <param name="comm"></param>
//    /// <param name="i"></param>
//    /// <param name="o"></param>
//    public static int AddCommandParameter(SqlCommand comm, int i, object o)
//    {
//        if (o == null || o.GetType() == DBNull.Value.GetType())
//        {
//            SqlParameter p = new SqlParameter();
//            p.ParameterName = "@p" + i.ToString();
//            p.Value = DBNull.Value;
//            comm.Parameters.Add(p);
//        }
//        else if (o.GetType() == typeof(byte[]))
//        {
//            // Pokud chcete uložit pole bajtů, musíte nejdřív vytvořit parametr s typem v DB(já používám vždy Image) a teprve pak nastavit hodnotu
//            SqlParameter param = comm.Parameters.Add("@p" + i.ToString(), SqlDbType.Binary);
//            param.Value = o;
//        }

//        else if (o.GetType() == Consts.tString || o.GetType() == Consts.tChar)
//        {
//            string _ = o.ToString();
//            comm.Parameters.AddWithValue("@p" + i.ToString(), MSStoredProceduresI.ConvertToVarChar(_));
//        }
//        else
//        {
//            comm.Parameters.AddWithValue("@p" + i.ToString(), o);
//        }

//        ++i;
//        return i;
//    }

//    public bool SelectExistsTable(string p, SqlConnection conn)
//    {
//        DataTable dt = SelectDataTable(conn, SH.Format2("SELECT * FROM sysobjects WHERE id = object_id(N'{0}') AND OBJECTPROPERTY(id, N'IsUserTable') = 1", p));
//        return dt.Rows.Count != 0;
//    }
//}
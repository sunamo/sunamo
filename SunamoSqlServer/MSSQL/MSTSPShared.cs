using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Data;

public partial class MSTSP
{
    public SqlConnection conn
    {
        get { return MSDatabaseLayer.conn; }
    }

    /// <summary>
    /// Conn nastaví automaticky
    /// </summary>
    public DataTable SelectDataTableSelective(SqlTransaction tran, string tabulka, string sloupecID, object id, string hledanySloupec)
    {
        SqlCommand comm = new SqlCommand(SH.Format2("SELECT {0} FROM {1} WHERE {2} = @p0", hledanySloupec, tabulka, sloupecID));
        AddCommandParameter(comm, 0, id);
        //NT
        return this.SelectDataTable(tran, comm);
    }
    /// <summary>
    /// Conn nastaví automaticky
    /// </summary>
    public DataTable SelectDataTableSelective(SqlTransaction tran, string TableName, ABC where, string nazvySloupcu)
    {
        SqlCommand comm = new SqlCommand(SH.Format2("SELECT {0} FROM {1} {2}", nazvySloupcu, TableName, GeneratorMsSql.CombinedWhere(where)));
        AddCommandParameterFromAbc(comm, where);
        //NTd
        return this.SelectDataTable(tran, comm);
    }
    /// <summary>
    /// Conn nastaví automaticky
    /// </summary>
    public DataTable SelectDataTableSelective(SqlTransaction tran, string tabulka, string sloupec, object hodnota)
    {
        SqlCommand comm = new SqlCommand(GeneratorMsSql.SimpleWhere(AllStrings.asterisk, tabulka, sloupec), conn);
        AddCommandParameter(comm, 0, hodnota);
        return SelectDataTable(tran, comm);
    }

    /// <summary>
    /// a2 je X jako v příkazu @pX
    /// </summary>
    /// <param name="comm"></param>
    /// <param name="i"></param>
    /// <param name="o"></param>
    public static void AddCommandParameter(SqlCommand comm, int i, object o)
    {
        if (o == null)
        {
            // Pokud chcete uložit null do DB, musíte nastavit hodnotu parametru na DBNull.Value
            comm.Parameters.AddWithValue("@p" + i.ToString(), DBNull.Value);
        }
        else if (o.GetType() == typeof(SqlParameter))
        {
            // Pokud chcete uložit null do Image, nezbývá vám nic jiného než jako hodnotu dát SqlParameter, kde zadáte SqlDbType a value[object]
            SqlParameter param = (SqlParameter)o;
            param.ParameterName = "@p" + i.ToString();
            comm.Parameters.Add(param);
        }
        else if (o.GetType() == typeof(byte[]))
        {
            // Pokud chcete uložit pole bajtů, musíte nejdřív vytvořit parametr s typem v DB(já používám vždy Image) a teprve pak nastavit hodnotu
            SqlParameter param = comm.Parameters.Add("@p" + i.ToString(), SqlDbType.Binary);
            param.Value = o;
        }
        else
        {
            // Pro všechny ostatní případy, zavolám metodu, jejíž část jsem si vypůjčil ze podobné metody pro SQLite a kterou přikládám níže
            comm.Parameters.AddWithValue("@p" + i.ToString(), o);
        }
    }

    /// <summary>
    /// Conn nastaví automaticky
    /// </summary>
    /// <param name="sql"></param>
    /// <returns></returns>
    public DataTable SelectDataTable(SqlTransaction tran, SqlCommand comm)
    {
        DataTable dt = new DataTable();
        comm.Connection = conn;
        comm.Transaction = tran;
        SqlDataAdapter adapter = new SqlDataAdapter(comm);
        adapter.Fill(dt);
        return dt;
    }
    /// <summary>
    /// A1 jsou hodnoty bez převedení AddCommandParameter nebo ReplaceValueOnlyOne
    /// Conn nastaví automaticky
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="_params"></param>
    /// <returns></returns>
    private DataTable SelectDataTable(SqlTransaction tran, string sql, params object[] _params)
    {
        SqlCommand comm = new SqlCommand(sql);
        for (int i = 0; i < _params.Length; i++)
        {
            AddCommandParameter(comm, i, _params[i]);
        }
        return SelectDataTable(tran, comm);
        //return SelectDataTable(SH.Format2(sql, _params));
    }

    private static void AddCommandParameterFromAbc(SqlCommand comm, params AB[] where)
    {
        AddCommandParameterFromAbc(comm, new ABC(where));
    }

    /// <summary>
    /// Počítá od nuly
    /// </summary>
    /// <param name = "comm"></param>
    /// <param name = "where"></param>
    private static void AddCommandParameterFromAbc(SqlCommand comm, ABC where)
    {
        for (int i = 0; i < where.Length; i++)
        {
            AddCommandParameter(comm, i, where[i].B);
        }
    }


public List<int> SelectValuesOfColumnInt(SqlTransaction tran, string tabulka, string sloupecHledaný, string sloupecVeKteremHledat, object hodnota)
    {
        SqlCommand comm = new SqlCommand(string.Format("SELECT {0} FROM {1} WHERE {2} = @p0", sloupecHledaný, tabulka, sloupecVeKteremHledat));
        AddCommandParameter(comm, 0, hodnota);
        //SQLiteCommand comm = new SQLiteCommand(sql, conn);
        List<int> vr = new List<int>();

        DataTable dt = SelectDataTable(tran, comm);
        foreach (DataRow item in dt.Rows)
        {
            object[] o = item.ItemArray; ;
            if (o[0] == DBNull.Value)
            {
                vr.Add(-1);
            }
            else
            {
                vr.Add(int.Parse(o[0].ToString()));
            }
            //}
        }
        //return SelectValuesOfColumnAllRowsInt(comm);
        return vr;
    }

/// <summary>
    /// POkud bude v DB hodnota DBNull.Value, vrátí se -1
    /// </summary>
    /// <param name="tabulka"></param>
    /// <param name="sloupec"></param>
    /// <returns></returns>
    public List<int> SelectValuesOfColumnAllRowsInt(SqlTransaction tran, string tabulka, string sloupec)
    {
        List<int> vr = new List<int>();
        SqlCommand comm = new SqlCommand(string.Format("SELECT {0} FROM {1}", sloupec, tabulka));
        DataTable dt = SelectDataTable(tran, comm);
        foreach (DataRow var in dt.Rows)
        {
            object o = var.ItemArray[0];
            if (o != DBNull.Value)
            {
                vr.Add(int.Parse(o.ToString()));
            }
            else
            {
                vr.Add(-1);
            }
        }
        return vr;
    }
/// <summary>
    /// Tato metoda má navíc možnost specifikovat simple where.
    /// </summary>
    /// <param name="tabulka"></param>
    /// <param name="hledanySloupec"></param>
    /// <param name="idColumn"></param>
    /// <param name="idValue"></param>
    /// <returns></returns>
    public List<int> SelectValuesOfColumnAllRowsInt(SqlTransaction tran, string tabulka, string hledanySloupec, string idColumn, object idValue)
    {
        List<int> vr = new List<int>();
        SqlCommand comm = new SqlCommand(string.Format("SELECT {0} FROM {1} WHERE {2} = @p0", hledanySloupec, tabulka, idColumn));
        AddCommandParameter(comm, 0, idValue);
        DataTable dt = SelectDataTable(tran, comm);
        foreach (DataRow var in dt.Rows)
        {
            object o = var.ItemArray[0];
            if (o != DBNull.Value)
            {
                vr.Add(int.Parse(o.ToString()));
            }
            else
            {
                vr.Add(-1);
            }
        }
        return vr;
    }
public List<int> SelectValuesOfColumnAllRowsInt(SqlTransaction tran, string tabulka, string hledanySloupec, params AB[] aB)
    {
        string hodnoty = MSDatabaseLayer.GetValues(aB.ToArray());

        List<int> vr = new List<int>();
        SqlCommand comm = new SqlCommand(string.Format("SELECT {0} FROM {1} {2}", hledanySloupec, tabulka, GeneratorMsSql.CombinedWhere(aB)));
        for (int i = 0; i < aB.Length; i++)
        {
            AddCommandParameter(comm, i, aB[i].B);
        }
        DataTable dt = SelectDataTable(tran, comm);
        foreach (DataRow var in dt.Rows)
        {
            object o = var.ItemArray[0];
            if (o != DBNull.Value)
            {
                vr.Add(int.Parse(o.ToString()));
            }
            else
            {
                vr.Add(-1);
            }
        }
        return vr;
    }
}
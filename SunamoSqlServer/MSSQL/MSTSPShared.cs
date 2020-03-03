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
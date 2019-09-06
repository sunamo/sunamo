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

    /// <summary>
    /// a2 je X jako v příkazu @pX
    /// A3 cant be AB
    /// </summary>
    /// <param name="comm"></param>
    /// <param name="i"></param>
    /// <param name="o"></param>
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
            comm.Parameters.AddWithValue("@p" + i.ToString(), MSStoredProceduresI.ConvertToVarChar(_));
        }
        else
        {
            comm.Parameters.AddWithValue("@p" + i.ToString(), o);
        }

        ++i;
        return i;
    }
}
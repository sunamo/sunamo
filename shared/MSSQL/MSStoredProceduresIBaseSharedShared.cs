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
    /// Tato metoda má navíc možnost specifikovat simple where.
    /// </summary>
    /// <param name="tabulka"></param>
    /// <param name="hledanySloupec"></param>
    /// <param name="idColumn"></param>
    /// <param name="idValue"></param>
    /// <returns></returns>
    public List<int> SelectValuesOfColumnAllRowsInt(bool signed, string tabulka, string hledanySloupec, string idColumn, object idValue)
    {
        SqlCommand comm = new SqlCommand(string.Format("SELECT {0} FROM {1} {2}", hledanySloupec, tabulka, GeneratorMsSql.SimpleWhere(idColumn)));
        AddCommandParameter(comm, 0, idValue);
        return ReadValuesInt(comm);
    }
public List<int> SelectValuesOfColumnAllRowsInt(string tabulka, string sloupec, ABC whereIs, ABC whereIsNot)
    {
        SqlCommand comm = new SqlCommand(string.Format("SELECT {0} FROM {1}", sloupec, tabulka) + GeneratorMsSql.CombinedWhere(whereIs, whereIsNot, null, null));
        AddCommandParameteresCombinedArrays(comm, 0, whereIs.ToArray(), whereIsNot.ToArray(), null, null);
        return ReadValuesInt(comm);
    }
public List<int> SelectValuesOfColumnAllRowsInt(string tabulka, int limit, string sloupec, string idColumn, object idValue)
    {
        SqlCommand comm = new SqlCommand(string.Format("SELECT TOP(" + limit + ") {0} FROM {1} WHERE {2} = @p0", sloupec, tabulka, idColumn));
        AddCommandParameter(comm, 0, idValue);
        return ReadValuesInt(comm);
    }
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
}
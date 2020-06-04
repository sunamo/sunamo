using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using sunamo;

public partial class MSStoredProceduresIBase : SqlServerHelper
{
    /// <summary>
    /// 1 = from
    /// 2 = to
    /// </summary>
    /// <param name="table"></param>
    /// <param name="returnColumn"></param>
    /// <param name="y1"></param>
    /// <param name="m1"></param>
    /// <param name="d1"></param>
    /// <param name="y2"></param>
    /// <param name="m2"></param>
    /// <param name="d2"></param>
    /// <returns></returns>
    public List<int> GetBetweenDateIntervalSameMonthIdsListInt(string table, string returnColumn, int y1, int m1, int d1, int d2)
    {
        var y = ColumnNamesWeb.Year;
        var m = ColumnNamesWeb.Month;
        var d = ColumnNamesWeb.Day;

        var result = this.SelectValuesOfColumnAllRowsInt(table, returnColumn, new ABC(m, m1, y, NH.Last2NumberByte( y1)), null, new ABC(d, d1 - 1), new ABC(d, d2 + 1));
        return result;
    }

    public List<short> GetBetweenDateIntervalSameMonthIdsListShort(string table, string returnColumn, int y1, int m1, int d1, int d2)
    {
        var y = ColumnNamesWeb.Year;
        var m = ColumnNamesWeb.Month;
        var d = ColumnNamesWeb.Day;

        var result = this.SelectValuesOfColumnAllRowsShort(table, returnColumn, new ABC(m, m1, y, NH.Last2NumberByte(y1)), null, new ABC(d, d1 - 1), new ABC(d, d2 + 1));
        return result;
    }

    public string AverageLenghtOfColumnData(string table, string column)
    {
        var c = MSStoredProceduresI.ci.SelectValuesOfColumnAllRowsString(table, column);

        CA.RemoveStringsEmpty(c);

        if (c.Count == 1)
        {
            return table + "." + column + " contains only one string with lenght " + c[0].Length;
        }
        else if (c.Count == 0)
        {
            return table + "." + column + " contains zero elements";
        }

        var l = new List<double>(c.Count);
        foreach (var item in c)
        {
            l.Add(item.Length);
        }



        return NH.CalculateMedianAverage(l);
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
    public DataTable SelectDataTableLastRowsInnerJoin(string tableFromWithShortVersion, string tableJoinWithShortVersion, string sloupceJezZiskavat, string onKlazuleOdNuly, int limit, string sloupecPodleKterehoRadit, ABC whereIs, ABC whereIsNot, params object[] hodnotyOdNuly)
    {
        List<AB> ab = new List<AB>(hodnotyOdNuly.Length);
        for (int i = 0; i < hodnotyOdNuly.Length; i++)
        {
            ab.Add(AB.Get(string.Empty, hodnotyOdNuly[i]));
        }

        SqlCommand comm = new SqlCommand("select TOP(" + limit.ToString() + ") " + sloupceJezZiskavat + " from " + tableFromWithShortVersion + " inner join " + tableJoinWithShortVersion + " on " + onKlazuleOdNuly + GeneratorMsSql.CombinedWhere(whereIs, whereIsNot, null, null) + " ORDER BY " + sloupecPodleKterehoRadit + " DESC");
        AddCommandParameteres(comm, 0, ab.ToArray());
        AddCommandParameteresCombinedArrays(comm, hodnotyOdNuly.Length, whereIs, whereIsNot, null, null);

        return SelectDataTable(comm);
    }
}
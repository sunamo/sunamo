using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using sunamo;
using sunamo.Values;
public partial class MSStoredProceduresIBase : SqlServerHelper
{
  

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
}
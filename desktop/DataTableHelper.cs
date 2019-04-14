using sunamo;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Controls;

public class DataTableHelper
{
    public static void NewColumn(DataTable dt, string name, object type)
    {
        DataColumn dc = new DataColumn(name, RH.GetTypeForObject(type));
        
        dt.Columns.Add(dc);
    }

    internal static void NewColumn(DataTable dt, int v, IList<string> columns, IList f)
    {
        NewColumn(dt, columns[v], f[v]);
    }
}


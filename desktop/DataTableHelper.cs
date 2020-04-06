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
        if (!(type is Type))
        {
            type = type.GetType();
        }
        
        DataColumn dc = new DataColumn(name, (Type)type);
        
        dt.Columns.Add(dc);
    }

    public static void NewColumn(DataTable dt, int v, IList<string> columns, IList f)
    {
        NewColumn(dt, columns[v], f[v]);
    }

    public static DataTable CreateDataTable(List<object> defaultValue, List<IList> o, params string[] columns)
    {
        DataTable dt = new DataTable();

        for (int i = 0; i < columns.Count(); i++)
        {
            NewColumn(dt, i, columns, defaultValue);
        }

        foreach (var item in o)
        {
            var row = dt.NewRow();
            for (int i = 0; i < columns.Length; i++)
            {
                row[columns[i]] = item[i];
            }

            dt.Rows.Add(row);
        }

        return dt;
    }
}
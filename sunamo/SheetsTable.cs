using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class SheetsTable
{
    DataTable dt = new DataTable();
    public Dictionary<string, FromToT<int>> ft = new Dictionary<string, FromToT<int>>();
    public SheetsTable(string input)
    {
        var r = SheetsHelper.Rows(input);

        int maxColumns = 0;

        foreach (var item in r)
        {
            var c = SheetsHelper.SplitFromGoogleSheetsRow(item, false);
            if (c.Count > maxColumns)
            {
                maxColumns = c.Count;
            }
        }

        var columnNames = SheetsHelper.SplitFromGoogleSheetsRow(r.First(), false);
        foreach (var item in columnNames)
        {
            dt.Columns.Add(item); 
        }

        int i = 0;

        foreach (var item in r)
        {
            var c = SheetsHelper.SplitFromGoogleSheetsRow(item, false);
            var dr = dt.NewRow();
            dr.ItemArray = CA.ToObject(c).ToArray();

            var first = c.First();

            if (CA.IsAllTheSame<string>(string.Empty, c.Skip(1).ToList()) && first.EndsWith(":"))
            {
                if (ft.Count > 0)
                {
                    ft.Last().Value.to = i ;
                }

                var ft2 = new FromToT<int>();
                ft2.from = i + 1;

                ft.Add(first, ft2);
            }

           

            dt.Rows.Add(dr);

            i++;
        }

        if (ft.Count > 0)
        {
            ft.Last().Value.to = i;
        }
    }

    public List<string> RowsFromColumn(int dx, FromToT<int> ft = null)
    {
        List<string> vr = new List<string>();

        if (ft != null)
        {
            for (int i = ft.from; i < ft.to; i++)
            {
                vr.Add(dt.Rows[i].ItemArray[dx].ToString());
            }
        }
        else
        {
            foreach (DataRow item in dt.Rows)
            {
                vr.Add(item.ItemArray[dx].ToString());
            }
        }

        return vr;
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class SheetsTable
{
    DataTable dt = new DataTable();

    public SheetsTable(string input)
    {
        var r = SheetsHelper.Rows(input);
        foreach (var item in r)
        {
            var c = SheetsHelper.SplitFromGoogleSheetsRow(item, false);
            var dr = dt.NewRow();
            dr.ItemArray = CA.ToObject(c).ToArray();
        }


    }

    public List<string> RowsFromColumn(int dx)
    {
        List<string> vr = new List<string>();

        foreach (DataRow item in dt.Rows)
        {
            vr.Add(item.ItemArray[dx].ToString());
        }

        return vr;
    }
}

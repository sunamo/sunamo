using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class SheetsGeneratorTemplate
{
    


    public static string AndroidAppComparing(List<StoreParsedApp> spa)
    {
        Dictionary<string, List<object>> ob = new Dictionary<string, List<object>>();

        var l = @"Name
Category
Uri
Count of ratings
Average rating
Overall users in thousands (k)
Price
In-app purchases
Last updated

Run test

Final - Official Web
Further test
Price for year subs
Price for lifelong subs";

        DataTable dt = new DataTable();
        var li = SH.GetLines(l);

        foreach (var item in li)
        {
            List<object> lo = new List<object>(spa.Count + 1);

            lo.Add(item);

            foreach (var item2 in spa)
            {
                lo.Add(item2.GetValueForRow(item));
            }
        }

        foreach (var item in li)
        {
            var row = dt.NewRow();
            row.ItemArray = ob[item].ToArray();
            dt.Rows.Add(row);
        }

        return SheetsHelper.DataTableToString(dt);
    }
}
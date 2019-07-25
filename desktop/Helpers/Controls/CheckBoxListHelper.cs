using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

public class CheckBoxListHelper
{
    public static IEnumerable<int> CheckedIndexes(IEnumerable<CheckBox> chbs)
    {
        //chbs[0].IsChecked = true;
        var indexes = chbs.Select((v, i) => new { v, i });
        var where = indexes.Where(x => BTS.GetValueOfNullable(x.v.IsChecked));
        List<int> v2 = new List<int>();
        foreach (var item in where)
        {
            v2.Add(item.i);
        }
        //var select = where.Select(x => x.i);
        //where.SelectMany<int>(d => d.;
        return v2;
    }

    public static IEnumerable<object> CheckedContent(IEnumerable<CheckBox> chbs)
    {
        //chbs[0].IsChecked = true;
        var indexes = chbs.Select((v, i) => new { v, i });
        var where = indexes.Where(x => BTS.GetValueOfNullable(x.v.IsChecked));
        return where.Select(d => d.v.Content);
    }

    internal static List<string> AllContent(IEnumerable<CheckBox> chbs)
    {
        var d = chbs.Select(e => e.Content);
        var result = CA.ToListString(d);
        return result;
    }
}


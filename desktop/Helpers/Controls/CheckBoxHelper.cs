using sunamo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;


    public class CheckBoxHelper
    {
        public static CheckBox Get(string text)
        {
            CheckBox chb = new CheckBox();
            chb.Content = text;
            return chb;
        }

    public static IEnumerable<int> CheckedIndexes(ObservableCollection<CheckBox> chbs)
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

    }

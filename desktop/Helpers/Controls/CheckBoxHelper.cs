using sunamo;
using System;
using System.Collections.Generic;
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

    public static IEnumerable<int> CheckedIndexes(List<CheckBox> chbs)
    {
        var indexes = chbs.Select((v, i) => new { v, i })
                    .Where(x => BTS.GetValueOfNullable( x.v.IsChecked))
                    .Select(x => x.i);
        return indexes;
    }

    }

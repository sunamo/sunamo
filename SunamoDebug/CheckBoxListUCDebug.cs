using desktop.Controls.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class CheckBoxListUCDebug
{
    public static void IsChecked(string when, CheckBoxListUC chbl)
    {
        DebugLogger.Instance.WriteLine(when);

        int i = 0;
        foreach (var chb in chbl.l)
        {
                var ch = chb.IsChecked;
                var ch2 = chb.o.IsChecked;

                DebugLogger.Instance.WriteLine(i + " " + ch + " " + ch2);

            i++;
        }
    }
}
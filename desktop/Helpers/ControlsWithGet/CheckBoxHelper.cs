using sunamo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
public partial class CheckBoxHelper
{
    internal static bool IsChecked(CheckBox v)
    {
        var r = WpfApp.cd.Invoke(() => v.IsChecked);
        return r.GetValueOrDefault();
    }
}
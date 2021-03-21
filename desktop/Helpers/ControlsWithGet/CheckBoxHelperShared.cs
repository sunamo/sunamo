using sunamo;
using sunamo.Essential;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

public partial class CheckBoxHelper{

    public static bool IsChecked(CheckBox v)
    {
        var r = WpfApp.cd.Invoke(() => v.IsChecked);
        return r.GetValueOrDefault();
    }

    /// <summary>
    /// tag is not needed, value is obtained through []
    /// Tag here is mainly for comment what data control hold 
    /// </summary>
    /// <param name="text"></param>
    public static CheckBox Get(ControlInitData d)
    {
        CheckBox chb = new CheckBox();
        ControlHelper.SetForeground(chb, d.foreground);
        chb.Content = ContentControlHelper.GetContent(d);
        if (d.OnClick != null)
        {
            chb.Click += d.OnClick;
        }
        chb.Tag = ControlNameGenerator.GetSeries(chb.GetType());
        chb.ToolTip = d.tooltip;
        return chb;
    }
}
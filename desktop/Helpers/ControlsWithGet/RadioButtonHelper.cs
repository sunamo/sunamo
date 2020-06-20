using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

public class RadioButtonHelper
{
    /// <summary>
    /// tag is not needed, value is obtained through []
    /// Tag here is mainly for comment what data control hold 
    /// </summary>
    /// <param name="text"></param>
    /// <param name="name"></param>
    public static RadioButton Get(ControlInitData d)
    {
        RadioButton chb = new RadioButton();
        ControlHelper.SetForeground(chb, d.foreground);
        chb.GroupName = d.group;
        chb.Content = ContentControlHelper.GetContent(d);
        chb.IsChecked = d.isChecked;
        chb.Checked += d.OnClick;
        if (d.tag == null)
        {
            chb.Tag = ControlNameGenerator.GetSeries(chb.GetType());
        }
        else
        {
            chb.Tag = d.tag;
        }
        chb.ToolTip = d.tooltip;
        return chb;
    }
}
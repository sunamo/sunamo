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
    /// <returns></returns>
    public static RadioButton Get(ControlInitData d)
    {
        RadioButton chb = new RadioButton();
        chb.Content = d.text;
        chb.GroupName = d.group;
        chb.Tag = ControlNameGenerator.GetSeries(chb.GetType());
        return chb;
    }
}


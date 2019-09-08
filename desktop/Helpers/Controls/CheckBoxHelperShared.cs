using sunamo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

public partial class CheckBoxHelper{
    /// <summary>
    /// tag is not needed, value is obtained through []
    /// Tag here is mainly for comment what data control hold 
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public static CheckBox Get(ControlInitData d)
    {
        CheckBox chb = new CheckBox();
        chb.Content = d.text;
        chb.Tag = ControlNameGenerator.GetSeries(chb.GetType());
        return chb;
    }
}
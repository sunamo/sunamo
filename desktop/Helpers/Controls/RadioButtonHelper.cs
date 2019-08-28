using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

public class RadioButtonHelper
{
    public static RadioButton Get(string text, string name)
    {
        RadioButton chb = new RadioButton();
        chb.Content = text;
        chb.GroupName = name;
        chb.Tag = ControlNameGenerator.GetSeries(chb.GetType());
        return chb;
    }
}


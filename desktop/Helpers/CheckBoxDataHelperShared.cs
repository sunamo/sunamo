using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using sunamo.Data;

public partial class CheckBoxDataHelper{ 
private static CheckBoxData<UIElement> Get(UIElement c)
    {
        var vr = new CheckBoxData<UIElement>();
        vr.t = c;
        return vr;
    }
}
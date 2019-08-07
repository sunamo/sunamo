using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using sunamo.Data;

public class CheckBoxDataHelper
{
    public static CheckBoxData<UIElement> TextBlock(string text)
    {
        var vr = new CheckBoxData<UIElement>();
        vr.t = TextBlockHelper.Get(text);
        return vr;
    }
}


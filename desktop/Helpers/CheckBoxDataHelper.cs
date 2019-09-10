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
    public static CheckBoxData<UIElement> TextBlock(ControlInitData c)
    {
        return Get(TextBlockHelper.Get(c));
    }

    private static CheckBoxData<UIElement> Get(UIElement c)
    {
        var vr = new CheckBoxData<UIElement>();
        vr.t = c;
        return vr;
    }

    public static CheckBoxData<UIElement> CheckBox(ControlInitData c)
    {
        return Get(CheckBoxHelper.Get(c));
    }

    public static CheckBoxData<UIElement> Button(ControlInitData c)
    {
        return Get(ButtonHelper.Get(c));
    }
}


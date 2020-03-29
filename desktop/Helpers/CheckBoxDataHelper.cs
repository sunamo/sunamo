using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using sunamo.Data;
public partial class CheckBoxDataHelper
{
    public static CheckBoxData<UIElement> TextBlock(ControlInitData c)
    {
        return Get(TextBlockHelper.Get(c));
    }

    public static CheckBoxData<UIElement> CheckBox(ControlInitData c)
    {
        return Get(CheckBoxHelper.Get(c));
    }

    /// <summary>
    /// Use ActionButton() for buttons without handler
    /// </summary>
    /// <param name="c"></param>
    public static CheckBoxData<UIElement> Button(ControlInitData c)
    {
        return Get(ButtonHelper.Get(c));
    }
}
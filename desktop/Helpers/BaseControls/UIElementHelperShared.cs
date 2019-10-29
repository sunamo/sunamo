using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

public partial class UIElementHelper{ 
public static void SetVisibility(bool v, params UIElement[] elements)
    {
        foreach (var item in elements)
        {
            item.Visibility = v ? Visibility.Visible : Visibility.Collapsed;
        }
    }

/// <summary>
    /// Return null if A1 wont be null
    /// </summary>
    /// <param name="o"></param>
    public static object GetContent(object o)
    {
        var ui = (UIElement)o;
        if (ui != null)
        {
            return ui.GetContent();
        }
        return null;
    }
}
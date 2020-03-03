using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

public class ControlFinder
{
    public static StackPanel StackPanel(FrameworkElement parent, string name)
    {
        return FindControl< StackPanel>(parent, name);
    }

    private static T FindControl<T>(FrameworkElement parent, string name) where T : UIElement
    {
        return (T)parent.FindName(name);
    }

    public static T FindControlExclude<T>(FrameworkElement parent, params UIElement[] exclude) where T: UIElement
    {
        //var d = VisualTreeHelpers.FindDescendents<UIElement>(parent);
        //var d3 = VisualTreeHelpers.FindDescendents3<UIElement>(parent);
        //var t = VisualTreeHelpers.GetLogicalChildCollection<UIElement>(parent);
        int i = 0;
        return default(T);
    }
}
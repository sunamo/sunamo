using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

public class GridViewItem2 : GridViewItem
{
    public int LengthItems()
    {
        return ((StackPanel)this.Content).Children.Count;
    }

    public double WidthOfItem(int nt)
    {
        StackPanel sp = (StackPanel)this.Content;
        var d = (FrameworkElement)sp.Children[nt];
        return d.Width;
    }

    public void WidthOfItem(int nt, double w)
    {
        StackPanel sp = (StackPanel)this.Content;
        var d = (FrameworkElement)sp.Children[nt];
        d.Width = w;
    }

    public UIElementCollection Children()
    {
        StackPanel sp = (StackPanel)this.Content;
        var d = sp.Children;
        return d;
    }
}

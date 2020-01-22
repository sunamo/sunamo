using sunamo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

public partial class FrameworkElementHelper{ 
/// <summary>
    /// If A1 will be fw, will set Margin
    /// </summary>
    /// <param name = "o"></param>
    /// <param name = "allSides"></param>
    public static void SetMargin(object o, double allSides)
    {
        var fw = (FrameworkElement)o;
        if (fw != null)
        {
            fw.Margin = new Thickness(allSides, allSides, allSides, allSides);
        }
    }


    

public static void SetMargin3(object o, double allSides)
    {
        var fw = (FrameworkElement)o;
        if (fw != null)
        {
            fw.Margin = new Thickness(allSides, allSides, allSides, allSides);
        }
    }

public static void SetAll3Widths(FrameworkElement fe, double w)
    {
        fe.Width = fe.MaxWidth = fe.MinWidth = w;
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

public class FrameworkElementHelper
{


    public static Size GetMaxContentSize(FrameworkElement fe)
    {
        return new Size(fe.ActualWidth, fe.ActualHeight);
    }

    public static void SetMaxContentSize(FrameworkElement fe, Size s)
    {
        fe.MaxWidth = s.Width;
        fe.MaxHeight = s.Height;
        fe.Width = s.Width;
        fe.Height = s.Height;
    }

    public static void SetAll3Widths(FrameworkElement fe, double w)
    {
        fe.Width = fe.MaxWidth = fe.MinWidth = w;
    }

    public static void SetWidthAndHeight(FrameworkElement fe, Size s)
    {
        fe.Width = s.Width;
        fe.Height = s.Height;
        fe.UpdateLayout();
    }
}


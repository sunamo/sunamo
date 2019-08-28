using System.Windows;
using System.Windows.Controls;

public partial class ControlHelper{ 
public static Size ActualInnerSize(ContentControl w)
    {
        

        var fw = w.Content as FrameworkElement;
        return new Size(fw.ActualWidth, fw.ActualHeight);
    }
    public static readonly Size SizePositiveInfinity = new Size(double.PositiveInfinity, double.PositiveInfinity);
}
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

public partial class ControlHelper{
    public static readonly Size SizePositiveInfinity = new Size(double.PositiveInfinity, double.PositiveInfinity);
    public static void SetForeground(Control c, Brush fg)
    {
        if (fg != null)
        {
            c.Foreground = fg;
        }
    }
public static Size ActualInnerSize(ContentControl w)
    {
        

        var fw = w.Content as FrameworkElement;
        return new Size(fw.ActualWidth, fw.ActualHeight);
    }
    


}
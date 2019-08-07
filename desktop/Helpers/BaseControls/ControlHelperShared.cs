using System.Windows;

public partial class ControlHelper{ 
internal static Size ActualInnerSize(WindowWithUserControl w)
    {
        var fw = w.Content as FrameworkElement;
        return new Size(fw.ActualWidth, fw.ActualHeight);
    }
    public static readonly Size SizePositiveInfinity = new Size(double.PositiveInfinity, double.PositiveInfinity);
}
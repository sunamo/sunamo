using System.Windows;

public static partial class FrameworkElementExtensions{ 
public static double ActualHeight(this FrameworkElement fe)
    {
        if (fe == null)
        {
            return 0;
        }

        return fe.ActualHeight;
    }
}
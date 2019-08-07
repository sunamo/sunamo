using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Media.Imaging;

public static partial class ButtonHelper{ 
public static void PerformClick(ButtonBase someButton)
    {
        someButton.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
    }
}
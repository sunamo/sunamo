using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;

public class ButtonHelperDesktop2
{
    public static void PerformClick(ButtonBase someButton)
    {
        someButton.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
    }
}

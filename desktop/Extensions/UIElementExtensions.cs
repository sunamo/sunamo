using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Threading;
using desktop;
using desktop.Controls;
using desktop.Controls.Controls;
using desktop.Controls.Input;
using PathEditor;
public static partial class UIElementExtensions
{
    
    private static Action EmptyDelegate = delegate ()
    {
    }

    ;
    public static void Refresh(this UIElement uiElement)
    {
        uiElement.Dispatcher.Invoke(DispatcherPriority.Render, EmptyDelegate);
    }

}
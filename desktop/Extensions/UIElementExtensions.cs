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

    public static object GetContent(this UIElement ui)
    {
        var t = ui.GetType();
        if (t == TypesControls.tListBox)
        {
            var selector = (ListBox)ui;
            return selector.SelectedItems;
        }
        else if (t == TypesControls.tListView)
        {
            var lv = (ListView)ui;
            return lv.SelectedItems;
        }
        else if (t == TypesControls.tComboBox)
        {
            var cb = ui as ComboBox;
            return cb.SelectedItem;
        }
        else if (t == TypesControls.tTextBox)
        {
            var txt = (TextBox)ui;
            return txt.Text;
        }
        else if (t == TypesControls.tTwoRadiosUC)
        {
            var txt = (TwoRadiosUC)ui;
            return txt.GetBool();
        }
        else
        {
            ThrowExceptions.NotImplementedCase(type, null);
        }

        return null;
    }

}
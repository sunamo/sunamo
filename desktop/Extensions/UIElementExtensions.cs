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
    };

    static UIElementExtensions()
    {
        UIElementExtensions.SetValidatedFullDelegate = SetValidatedFull;
        UIElementExtensions.Validate2FullDelegate = Validate2Full;
    }

    public static bool? Validate2Full(this UIElement ui, string name,  ValidateData d = null)
    {
        validatedInFull = false;
        var t = ui.GetType();

        if (t == SelectMoreFolders.type)
        {
            validatedInFull = true;
            var c = ui as SelectMoreFolders;
            c.Validate(name, d);
            return SelectMoreFolders.validated;
        }
        else if (t == SelectFolder.type)
        {
            validatedInFull = true;
            var c = ui as SelectFolder;
            c.Validate(name, d);
            return SelectFolder.validated;
        }

        return null;
    }

    public static bool SetValidatedFull(this UIElement ui, bool b)
    {
        var t = ui.GetType();
        if (t == SelectMoreFolders.type)
        {
            SelectMoreFolders.validated = b;
            return true;
        }
        else if (t == SelectFolder.type)
        {
            SelectFolder.validated = b;
            return true;
        }

        return false;
    }

    public static void Refresh(this UIElement uiElement)
    {
        uiElement.Dispatcher.Invoke(DispatcherPriority.Render, EmptyDelegate);
    }
}
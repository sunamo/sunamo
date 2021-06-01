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
    };

    static UIElementExtensions()
    {
        desktop2.Extensions.UIElementExtensions2.SetValidatedDelegate = SetValidated;
        desktop2.Extensions.UIElementExtensions2.Validate2Delegate = Validate2;
        desktop2.Extensions.UIElementExtensions2.GetContentDelegate = GetContent;

        UIElementExtensions.SetValidatedFullDelegate = SetValidatedFull;
        UIElementExtensions.Validate2FullDelegate = Validate2Full;
    }

    public static Tuple< bool?, ValidateData> Validate2(this UIElement ui, string name,  ValidateData d)
    {
        var b = Validate2(ui, name, ref d);
        return new Tuple<bool?, ValidateData>(b, d);
    }

    public static bool? Validate2Full(this UIElement ui, string name,  ValidateData d = null)
    {
        validatedInFull = false;
        var t = ui.GetType();

        if (t == SelectMoreFolders.type)
        {
            validatedInFull = true;
            var c = ui as SelectMoreFolders;
            c.Validate(name, ref d);
            return SelectMoreFolders.validated;
        }
        else if (t == SelectFolder.type)
        {
            validatedInFull = true;
            var c = ui as SelectFolder;
            c.Validate(name, ref d);
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
        uiElement.Dispatcher.Invoke(DispatcherPriority.ContextIdle, EmptyDelegate);
        uiElement.Dispatcher.Invoke(DispatcherPriority.Render, EmptyDelegate);
    }
}
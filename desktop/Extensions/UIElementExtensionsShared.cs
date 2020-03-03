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

public static partial class UIElementExtensions{
    static Type type = typeof(UIElementExtensions);
    public static Func< UIElement, string, ValidateData, bool?> Validate2FullDelegate = null;
    public static Func< UIElement, bool, bool> SetValidatedFullDelegate = null;
    public static bool validatedInFull = false;

    /// <summary>
    /// Must be be Validate2 due to different with Validate which is defi
    /// </summary>
    /// <param name = "ui"></param>
    /// <param name = "name"></param>
    
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
            return cb.Text;
        }
        else if (t == TypesControls.tTextBox)
        {
            var txt = (TextBox)ui;
            return txt.Text;
        }
        else if (t == TypesControls.tCheckBox)
        {
            var txt = (CheckBox)ui;
            return txt.Content;
        }
        else if (t == TwoRadiosUC.type)
        {
            var txt = (TwoRadiosUC)ui;
            return txt.GetBool();
        }
        else if (t == SuggestTextBoxPath.type)
        {
            var txt = (SuggestTextBoxPath)ui;
            return txt.dataContext.SelectedPathPart.Path;
        }
        else if (t == TypesControls.tTextBlock)
        {
            var txt = (TextBlock)ui;
            return txt.Text;
        }
        else
        {
            ThrowExceptions.NotImplementedCase(type, null,t);
        }

        return null;
    }
}
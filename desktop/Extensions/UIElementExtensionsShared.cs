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

public static partial class UIElementExtensions{
    static Type type = typeof(UIElementExtensions);

    /// <summary>
    /// Must be be Validate2 due to different with Validate which is defi
    /// </summary>
    /// <param name = "ui"></param>
    /// <param name = "name"></param>
    /// <returns></returns>
    public static bool? Validate2(this UIElement ui, string name)
    {
        var t = ui.GetType();
        if (t == TypesControls.tTextBox)
        {
            var c = ui as TextBox;
            c.Validate(name);
            return TextBoxHelper.validated;
        }
        else if (t == TypesControls.tListBox)
        {
            var c = ui as ListBox;
            c.Validate(name);
            return TextBoxHelper.validated;
        }
        else if (t == TypesControls.tListView)
        {
            var c = ui as ListView;
            c.Validate(name);
            return TextBoxHelper.validated;
        }
        else if (t == TypesControls.tComboBox)
        {
            var c = ui as ComboBox;
            c.Validate(name);
            return TextBoxHelper.validated;
        }
        else if (t == SelectFile.type)
        {
            var c = ui as SelectFile;
            c.Validate(name);
            return SelectFile.validated;
        }
        else if (t == SelectFolder.type)
        {
            var c = ui as SelectFolder;
            c.Validate(name);
            return SelectFolder.validated;
        }
        else if (t == SelectManyFiles.type)
        {
            var c = ui as SelectManyFiles;
            c.Validate(name);
            return SelectManyFiles.validated;
        }
        else if (t == SelectMoreFolders.type)
        {
            var c = ui as SelectMoreFolders;
            c.Validate(name);
            return SelectMoreFolders.validated;
        }
        else if (t == TwoRadiosUC.type)
        {
            var c = ui as TwoRadiosUC;
            c.Validate(name);
            return SelectMoreFolders.validated;
        }
        else if (t == PathEditor.SuggestTextBoxPath.type)
        {
            var c = ui as PathEditor.SuggestTextBoxPath;
            c.Validate(name);
            return PathEditor.SuggestTextBoxPath.validated;
        }
        else
        {
            ThrowExceptions.NotImplementedCase(type, "Validate");
        }

        return null;
    }

public static void SetValidated(this UIElement ui, bool b)
    {
        var t = ui.GetType();
        if (t == TypesControls.tTextBox)
        {
            TextBoxHelper.validated = b;
            ;
        }
        else if (t == TypesControls.tListBox)
        {
            ListBoxExtensions.validated = b;
        }
        else if (t == TypesControls.tListView)
        {
            ListViewExtensions.validated = b;
        }
        else if (t == TypesControls.tComboBox)
        {
            ComboBoxExtensions.validated = b;
        }
        else if (t == SelectFile.type)
        {
            SelectFile.validated = b;
        }
        else if (t == SelectFolder.type)
        {
            SelectFolder.validated = b;
        }
        else if (t == SelectManyFiles.type)
        {
            SelectManyFiles.validated = b;
        }
        else if (t == SelectMoreFolders.type)
        {
            SelectMoreFolders.validated = b;
        }
        else if (t == PathEditor.SuggestTextBoxPath.type)
        {
            PathEditor.SuggestTextBoxPath.validated = b;
        }
        else
        {
            ThrowExceptions.NotImplementedCase(type, "SetValidated");
        }
    }
}
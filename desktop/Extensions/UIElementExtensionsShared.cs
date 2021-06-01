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
    public static Func< UIElement, string, ValidateData, bool?> Validate2FullDelegate = null;
    public static Func< UIElement, bool, bool> SetValidatedFullDelegate = null;
    public static bool validatedInFull = false;

    /// <summary>
    /// Must be be Validate2 due to different with Validate which is defi
    /// From A3 removed = null & add ref => Validate2() is only one place which ci ValidateData
    /// </summary>
    /// <param name = "ui"></param>
    /// <param name = "name"></param>
    public static bool? Validate2(this UIElement ui, string name, ref ValidateData d)
    {
        if (Validate2FullDelegate != null)
        {
            var result = Validate2FullDelegate.Invoke(ui, name, d);

            if (validatedInFull)
            {
                return result;
            }
        }
        if (d == null)
        {
            d = new ValidateData();
        }

        var t = ui.GetType();
        if (t == TypesControls.tTextBox)
        {
            var c = ui as TextBox;
            c.Validate(name, ref d);
            return TextBoxHelper.validated;
        }
        // ListBoxHelper
        //else if (t == TypesControls.tListBox)
        //{
        //    var c = ui as ListBox;
        //    c.Validate(name, d);
        //    return ListBoxHelper.validated;
        //}
        // ListViewHelper doesnt exists
        //else if (t == TypesControls.tListView)
        //{
        //    var c = ui as ListView;
        //    c.Validate(name, d);
        //    return ListViewHelper.validated;
        //}
        else if (t == TypesControls.tComboBox)
        {
            var c = ui as ComboBox;
            c.Validate(name, ref d);
            return ComboBoxHelper.validated;
        }
        else if (t == SelectFile.type)
        {
            var c = ui as SelectFile;
            c.Validate(name, ref d);
            return SelectFile.validated;
        }
       
        else if (t == SelectManyFiles.type)
        {
            var c = ui as SelectManyFiles;
            c.Validate(name, ref d);
            return SelectManyFiles.validated;
        }
        else if (t == TwoRadiosUC.type)
        {
            var c = ui as TwoRadiosUC;
            c.Validate(name, ref d);
            return TwoRadiosUC.validated;
        }
        else if (t == TypesControlsSunamo.tPathEditor)
        {
            IValidateControl td = (IValidateControl)ui;
            return td.Validate(name, ref d);
        }
        else
        {
            ThrowExceptions.NotImplementedCase(Exc.GetStackTrace(),type, sess.i18n(XlfKeys.Validated), t);
        }

        return null;
    }

   

    public static void SetValidated(this UIElement ui, bool b)
        {
            if (SetValidatedFullDelegate != null)
            {
                if (SetValidatedFullDelegate.Invoke(ui, b))
                {
                    return;
                }
            }
        

            var t = ui.GetType();
            if (t == TypesControls.tTextBox)
            {
                TextBoxHelper.validated = b;
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
            else if (t == SelectManyFiles.type)
            {
                SelectManyFiles.validated = b;
            }
            else if (t == TypesControlsSunamo.tPathEditor)
            {
            var ivc = (IValidateControl)ui;
            ivc.Validated = b;
            }
            else
            {
                ThrowExceptions.NotImplementedCase(Exc.GetStackTrace(),type, "SetValidated", t.FullName);
            }
        }

    
    /// <summary>
    /// There is no Enum with all controls
    /// </summary>
    /// <param name="ui"></param>
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
        else if (t == TypesControlsSunamo.tPathEditor)
        {
            var txt = (IValidateControl)ui;
            return txt.GetContent();
        }
        else if (t == TypesControls.tTextBlock)
        {
            var txt = (TextBlock)ui;
            return txt.Text;
        }
        else
        {
            ThrowExceptions.NotImplementedCase(Exc.GetStackTrace(),type, null,t);
        }

        return null;
    }
}
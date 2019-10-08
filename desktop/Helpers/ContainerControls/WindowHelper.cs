using desktop.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
public partial class WindowHelper
{
    public static bool? SetDialogResult(Window w, bool dialog, bool? dialogResult)
    {
        if (dialog)
        {
            var dr = w.ShowDialog();
            if (w.DialogResult != dr)
            {
            // Cant set DialogResult while window isnt show as dialog
            //DialogResult = dialogResult;
            }

            return dr;
        }
        else
        {
            return dialogResult;
        }
    }

    public static void ShowDialog(WindowWithUserControl windowWithUserControl)
    {
        if (windowWithUserControl.DialogResult == null)
        {
            // 'Cannot set Visibility or call Show, ShowDialog, or WindowInteropHelper.EnsureHandle after a Window has closed.'
            /*
             e.Cancel = true;
        this.Visibility = Visibility.Hidden;
             */
            windowWithUserControl.ShowDialog();
        }
    }

    public static void Close(Window w)
    {
        try
        {
            w.Close();
        }
        catch (Exception ex)
        {

        }
    }
}
using desktop.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

public class WindowHelper
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

    public static void ShowExceptionWindow(object e)
    {
        ShowExceptionWindow(e, Environment.NewLine);
    }

    static IWindowOpener windowOpener = null;

    public static string ShowExceptionWindow(object e, string methodName = "")
    {
        if (methodName != string.Empty)
        {
            methodName += AllStrings.space;
        }
        string dump = null;
        //dump = YamlHelper.DumpAsYaml(e);
        dump = SunamoJson.SerializeObject(e, true);

        ShowTextResult result = new ShowTextResult(methodName + dump);
        result.ChangeDialogResult += Result_ChangeDialogResult;
        windowOpener = ((IWindowOpener)Application.Current.MainWindow);
        windowOpener.windowWithUserControl = new WindowWithUserControl(result, ResizeMode.CanResizeWithGrip, true);
        windowOpener.windowWithUserControl.ShowDialog();

        return dump;
    }

    private static void Result_ChangeDialogResult(bool? b)
    {
        windowOpener.windowWithUserControl.Close();
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
}


using desktop.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

public partial class WindowHelper{
    static IWindowOpener windowOpener = null;
    public static void ShowExceptionWindow(object e)
    {
        ShowExceptionWindow(e, Environment.NewLine);
    }

    private static void Result_ChangeDialogResult(bool? b)
    {
        windowOpener.windowWithUserControl.Close();
    }
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
        windowOpener = Application.Current.MainWindow as IWindowOpener;
        if (windowOpener == null)
        {
            MessageBox.Show("MainWindow must be IWindowOpener due to show exceptions");
        }
        else
        {
            windowOpener.windowWithUserControl = new WindowWithUserControl(result, ResizeMode.CanResizeWithGrip, true);
            windowOpener.windowWithUserControl.ShowDialog();
            MessageBox.Show("Window was shown");
        }

        return dump;
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
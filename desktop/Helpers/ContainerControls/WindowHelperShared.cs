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

    /// <summary>
    /// Return dump A1
    /// </summary>
    /// <param name="e"></param>
    /// <param name="methodName"></param>
    /// <returns></returns>
    public static string ShowExceptionWindow(object e, string methodName = "")
    {
        if (methodName != string.Empty)
        {
            methodName += AllStrings.space;
        }

        string dump = null;
        //dump = YamlHelper.DumpAsYaml(e);
        //dump = SunamoJsonHelper.SerializeObject(e, true);
        //dump = JsonParser.Serialize<>
        dump = RH.DumpAsString(new DumpAsStringArgs { o = e, d = DumpProvider.Reflection });
        ShowTextResult result = new ShowTextResult(methodName + dump);
        result.ChangeDialogResult += Result_ChangeDialogResult;
        var mw = Application.Current.MainWindow;
        windowOpener = mw as IWindowOpener;

        if (windowOpener == null)
        {
            var d = sess.i18n(XlfKeys.MainWindowMustBeIWindowOpenerDueToShowExceptions) + $"Is Application.Current.MainWindow null: {mw == null}";
            if (mw != null)
            {
                d += $"Type: {mw.GetType()}";
            }
            MessageBox.Show(d);
        }
        else
        {
            windowOpener.windowWithUserControl = new WindowWithUserControl(result, ResizeMode.CanResizeWithGrip, false);
            windowOpener.windowWithUserControl.ShowDialog();
            //MessageBox.Show("Window was shown");
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
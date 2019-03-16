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

    public static void ShowExceptionWindow(object e, string methodName = "")
    {
        if (methodName != string.Empty)
        {
            methodName += " ";
        }
        string dump = null;
        //dump = YamlHelper.DumpAsYaml(e);
        dump = SunamoJson.SerializeObject(e, true);

        ShowTextResult result = new ShowTextResult(methodName + dump);
        WindowWithUserControl window = new WindowWithUserControl(result, ResizeMode.CanResizeWithGrip, true);
        window.ShowDialog();
    }

    
}


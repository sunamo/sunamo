using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

public class WindowWithUserControlArgs
{
    public object iUserControlInWindow;
    public ResizeMode rm = ResizeMode.CanResize;
    public bool useResultOfShowDialog = false;
    /// <summary>
    /// only when uc dont have own button!
    /// </summary>
    public bool addDialogButtons = false;
    public string tag = null;
}

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
    /// <summary>
    /// Whether use base.DialogResult instead of calling ChangeDialogResult
    /// </summary>
    public bool useResultOfShowDialog = false;
    /// <summary>
    /// only when uc dont have own button!
    /// </summary>
    public bool addDialogButtons = false;
    /// <summary>
    /// Tag which is set to WindowWithUserControl
    /// </summary>
    public string tag = null;
    public string hint = null;
    public bool showInTitleSizeOfWindowAndContent = false;
    public Size sizeWindow; 
}
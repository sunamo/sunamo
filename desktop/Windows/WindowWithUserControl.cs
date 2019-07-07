using desktop;
using desktop.Controls;
using desktop.Controls.Collections;
using desktop.Essential;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

/// <summary>
/// With this is allowed use logging via ThisApp (has own statusbar)
/// </summary>
public class WindowWithUserControl : Window, IUserControlWithResult
{
    UserControl userControl = null;
    DockPanel dock = null;
    IUserControl uc = null;
    IUserControlInWindow iUserControlInWindow = null;
    IUserControlWithResult userControlWithResult = null;
    DialogButtons dialogButtons = null;
    bool addDialogButtons = false;

    /// <summary>
    /// A1 can be IUserControlInWindow, if have own buttons for accepting
    /// </summary>
    /// <param name="iUserControlInWindow"></param>
    /// <param name="rm"></param>
    /// <param name="addDialogButtons"></param>
    public WindowWithUserControl(object iUserControlInWindow, ResizeMode rm, bool addDialogButtons = false)
    {
        userControl = (UserControl)iUserControlInWindow;
        this.Closed += WindowWithUserControl_Closed;
        this.uc = userControl as IUserControl;
        this.addDialogButtons = addDialogButtons;

        dock = new DockPanel();
        dock.LastChildFill = true;

        StatusBar statusBar = new StatusBar();
        statusBar.Height = 25;

        // Stupid - I have to save reference to control, because contains data
        //DialogButtons dialogButtons = new DialogButtons();
        //DockPanel.SetDock(dialogButtons, Dock.Bottom);
        //dialogButtons.ChangeDialogResult += DialogButtons_ChangeDialogResult;
        //dock.Children.Add(dialogButtons);

        
        TextBlock textBlockStatus = TextBlockHelper.Get("");
        WpfApp.SaveReferenceToTextBlockStatus(false, textBlockStatus, textBlockStatus);
        statusBar.Items.Add(textBlockStatus);
        DockPanel.SetDock(statusBar, Dock.Bottom);
        dock.Children.Add(statusBar);

        if (addDialogButtons)
        {
            dialogButtons = new DialogButtons();
            dialogButtons.ChangeDialogResult += DialogButtons_ChangeDialogResult;
            DockPanel.SetDock(dialogButtons, Dock.Bottom);
            dock.Children.Add(dialogButtons);
            
        }

        this.ResizeMode = rm;
        // Původně bylo WidthAndHeight
        this.SizeToContent = System.Windows.SizeToContent.Manual;
        //this.MaxWidth = System.Windows.SystemParameters.PrimaryScreenWidth * 0.75d;
        //this.MaxHeight = System.Windows.SystemParameters.PrimaryScreenHeight * 0.75d;
        this.Content = dock;
        this.HorizontalContentAlignment = HorizontalAlignment.Stretch;
        this.VerticalContentAlignment = VerticalAlignment.Stretch;
        this.HorizontalAlignment = HorizontalAlignment.Stretch;
        this.VerticalAlignment = VerticalAlignment.Stretch;

        AddUC();

        Loaded += WindowWithUserControl_Loaded;
    }

    private void DialogButtons_ChangeDialogResult(bool? b)
    {
        UserControlWithResult_ChangeDialogResult(b);
    }

    private void UserControlWithResult_ChangeDialogResult(bool? b)
    {
        if (userControl is CheckBoxListUC)
        {
            CheckBoxListUC checkBoxListUC = (CheckBoxListUC)userControl;

            var checked2 = checkBoxListUC.CheckedIndexes();
            if (checked2.Count() >0)
            {
                dialogButtons.IsEnabledBtnOk = true;
            }
            else
            {
                dialogButtons.IsEnabledBtnOk = false;
            }
        }
        else
        {

        }

        if (dialogButtons != null)
        {
            if (dialogButtons.clickedOk)
            {
                uc_ChangeDialogResult(b);
            }
        }
    }

    private void WindowWithUserControl_Loaded(object sender, RoutedEventArgs e)
    {
        //AddUC();

        var chbl = (CompareInCheckBoxListUC)userControl;

        Title = chbl.chblAutoYes.RenderSize.ToString();
    }

    private void AddUC()
    {
        // TODO: Find method where is mass setted all aligments
        userControl.VerticalAlignment = VerticalAlignment.Stretch;
        userControl.VerticalContentAlignment = VerticalAlignment.Stretch;
        userControl.HorizontalAlignment = HorizontalAlignment.Stretch;
        userControl.HorizontalContentAlignment = HorizontalAlignment.Stretch;

        dock.Children.Add(userControl);

        if (userControl is IUserControlWithResult)
        {
            userControlWithResult = (IUserControlWithResult)userControl;
            userControlWithResult.ChangeDialogResult += UserControlWithResult_ChangeDialogResult;

        }
        if (userControl is IUserControlInWindow)
        {
            iUserControlInWindow = (IUserControlInWindow)userControl;
            iUserControlInWindow.ChangeDialogResult += uc_ChangeDialogResult;
        }

        if (addDialogButtons)
        {
            if (iUserControlInWindow != null)
            {
                iUserControlInWindow.ChangeDialogResult -= uc_ChangeDialogResult;
            }

        }

        Activate();

        // V InvalidateMeasure není volání na žádné Invalidate
        InvalidateMeasure();
        // Díval jsem se do ReferenceSource, InvalidateVisual volá jen InvalidateArrange
        InvalidateVisual();
    }

    public event VoidBoolNullable ChangeDialogResult;

    private void WindowWithUserControl_Closed(object sender, System.EventArgs e)
    {
        WpfApp.SaveReferenceToTextBlockStatus(true, null, null);
    }

    void uc_ChangeDialogResult(bool? b)
    {
        // Throwed exception, output is captured by ChangeDialogResult
        //DialogResult = b;
        if (ChangeDialogResult != null)
        {
            ChangeDialogResult(b);
        }
    }

    public static void AvailableShortcut(Dictionary<string, string> dictionary)
    {
        ShowTextResult result = new ShowTextResult(SH.JoinDictionary(dictionary, AllStrings.swda));

        WindowWithUserControl window = new WindowWithUserControl(result, ResizeMode.NoResize);
        window.ShowDialog();
    }

    public void Accept(object input)
    {
        if (iUserControlInWindow != null)
        {
            iUserControlInWindow.Accept(input);
        }

        
    }
}

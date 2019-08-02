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
public class WindowWithUserControl : Window, IUserControlWithResult, IUserControlWithSizeChange
{
    UserControl userControl = null;
    DockPanel dock = null;
    IUserControl uc = null;
    IUserControlInWindow iUserControlInWindow = null;
    IUserControlWithResult userControlWithResult = null;
    
    bool addDialogButtons = false;
    IUserControlWithSizeChange userControlWithSizeChange = null;

    StatusBar statusBar = null;
    DialogButtons dialogButtons = null;
    Menu menu = null;

    private void WindowWithUserControl_SizeChanged(object sender, SizeChangedEventArgs e)
    {
        var hMenu = ah(menu);
        var staturBarH = ah(statusBar);
        var dialogButtonsH = ah(dialogButtons);
        var growingRow = ActualHeight - hMenu - staturBarH - dialogButtonsH ;
        OnSizeChanged(new DesktopSize(ActualWidth, growingRow));

        Title = SH.Join(",", "Growing row", growingRow, "Height", ActualHeight);
    }

    private double ah(FrameworkElement dialogButtons)
    {
        if (dialogButtons == null)
        {
            return 0;
        }
        return dialogButtons.ActualHeight;
    }

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

        menu = new Menu();
        DockPanel.SetDock(menu, Dock.Top);
        dock.Children.Add(menu);

        if (uc is IUserControlWithMenuItemsList)
        {
            IUserControlWithMenuItemsList userControlWithMenuItemsList = (IUserControlWithMenuItemsList)uc;

            var miUc = MenuItemHelper.CreateNew(userControlWithMenuItemsList.Title);

            foreach (var item in userControlWithMenuItemsList.MenuItems())
            {
                miUc.Items.Add(item);
            }

            miUc.UpdateLayout();
            menu.Items.Add(miUc);
        }

        userControlWithSizeChange = uc as IUserControlWithSizeChange;

        statusBar = new StatusBar();
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
        SizeChanged += WindowWithUserControl_SizeChanged;
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
            //DebugLogger.Instance.ClipboardOrDebug("Calling uc_ChangeDialogResult with window dialog buttons");

            if (dialogButtons.clickedOk)
            {
                uc_ChangeDialogResult(b);
            }
        }
        else
        {
            //DebugLogger.Instance.ClipboardOrDebug("Calling uc_ChangeDialogResult with NO window dialog buttons");

            uc_ChangeDialogResult(b);
        }
    }

    private void WindowWithUserControl_Loaded(object sender, RoutedEventArgs e)
    {

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

    public void OnSizeChanged(DesktopSize maxSize)
    {
        if (userControlWithSizeChange != null)
        {
            userControlWithSizeChange.OnSizeChanged(maxSize);
        }
    }

    public void Init()
    {
        
    }
}

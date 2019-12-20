using desktop;
using desktop.Controls;
using desktop.Controls.Collections;

using desktop.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

/// <summary>
/// With this is allowed use logging via ThisApp (has own statusbar)
/// </summary>
public class WindowWithUserControl : Window, IControlWithResult, IUserControlWithSizeChange
{
    UserControl userControl = null;
    DockPanel dock = null;
    IUserControl uc = null;
    IUserControlInWindow iUserControlInWindow = null;
    IControlWithResult userControlWithResult = null;
    IControlWithResultDebug controlWithResultDebug = null;
    bool isControlWithResult = false;
    IControlWithResult controlWithResult = null;
    /// <summary>
    /// Na false se nastaví při zavírání. Pokud tedy bude false, znamená to že okno se zavřelo křížkem a má tato hodnota false přednost
    /// </summary>
    bool? dialogResult = null;
    bool addDialogButtons = false;
    IUserControlWithSizeChange userControlWithSizeChange = null;
    static Type type = typeof(WindowWithUserControl);
    StatusBar statusBar = null;
    public DialogButtons dialogButtons = null;
    Menu menu = null;

    private void WindowWithUserControl_SizeChanged(object sender, SizeChangedEventArgs e)
    {
        var hMenu = menu.ActualHeight();
        var staturBarH = statusBar.ActualHeight();
        var dialogButtonsH = dialogButtons.ActualHeight();
        
        var e2 = ControlHelper.ActualInnerSize(this).Height;
        var growingRow = e2 - hMenu - staturBarH - dialogButtonsH ;
        OnSizeChanged(new DesktopSize(ActualWidth, growingRow));

        //Title = SH.Join(",", "Growing row", growingRow, "Height", ActualHeight);
    }

    private double ah(FrameworkElement dialogButtons)
    {
        return dialogButtons.ActualHeight();
        
    }

    /// <summary>
    /// A1 can be IUserControlInWindow, if have own buttons for accepting
    /// </summary>
    /// <param name="iUserControlInWindow"></param>
    /// <param name="rm"></param>
    /// <param name="addDialogButtons"></param>
    public WindowWithUserControl(object iUserControlInWindow, ResizeMode rm, bool addDialogButtons = false, string tag = null)
    {
        

        Tag = tag;
        userControl = (UserControl)iUserControlInWindow;

        

        this.Closed += WindowWithUserControl_Closed;
        this.Closing += WindowWithUserControl_Closing;
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

            var miUc = MenuItemHelper.Get(new ControlInitData { text = userControlWithMenuItemsList.Title });

            foreach (var item in userControlWithMenuItemsList.MenuItems())
            {
                miUc.Items.Add(item);
            }

            miUc.UpdateLayout();
            menu.Items.Add(miUc);
        }

        controlWithResultDebug = uc as IControlWithResultDebug;
        userControlWithSizeChange = uc as IUserControlWithSizeChange;

        controlWithResult = uc as IControlWithResult;
        isControlWithResult = controlWithResultDebug != null;

        statusBar = new StatusBar();
        statusBar.Height = 25;

        // Stupid - I have to save reference to control, because contains data
        //DialogButtons dialogButtons = new DialogButtons();
        //DockPanel.SetDock(dialogButtons, Dock.Bottom);
        //dialogButtons.ChangeDialogResult += DialogButtons_ChangeDialogResult;
        //dock.Children.Add(dialogButtons);

        
        TextBlock textBlockStatus = TextBlockHelper.Get(new ControlInitData { text = "" });
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

    private void WindowWithUserControl_Closing(object sender, CancelEventArgs e)
    {
        dialogResult = false;
    }

    private void DialogButtons_ChangeDialogResult(bool? b)
    {
        UserControlWithResult_ChangeDialogResult(b);
    }

    public bool EnableBtnOk
    {
        set
        {
            dialogButtons.btnOk.IsEnabled = value;
        }
    }

    /// <summary>
    /// Cant be used as handler, then is called multiple times becaues UserControlWithResult_ChangeDialogResult call the same method
    /// </summary>
    /// <param name="b"></param>
    void uc_ChangeDialogResult(bool? b)
    {
        // Throwed exception, output is captured by ChangeDialogResult
        //DialogResult = b;
        if (ChangeDialogResult != null)
        {
            //var tag2 = Tag.ToString();

            if (dialogResult.HasValue && !dialogResult.Value)
            {
                b = dialogResult;
            }

            // If is registered, will close window exteranlly
            ChangeDialogResult?.Invoke(b);

            WindowHelper.Close(this);
        }
        else
        {
            // Otherwise close here
            Close();
        }
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

        int countOfHandlers = 0;
        if (userControl is IControlWithResult)
        {
            userControlWithResult = (IControlWithResult)userControl;

            if (controlWithResultDebug != null)
            {
                countOfHandlers = controlWithResultDebug.CountOfHandlersChangeDialogResult();
            }

            if (isControlWithResult)
            {
                controlWithResultDebug.AttachChangeDialogResult(new VoidBoolNullable( UserControlWithResult_ChangeDialogResult), false);
            }

            if (controlWithResultDebug != null)
            {
                countOfHandlers = controlWithResultDebug.CountOfHandlersChangeDialogResult();
            }
        }

        if (userControl is IUserControlInWindow)
        {
            iUserControlInWindow = (IUserControlInWindow)userControl;

                if (controlWithResultDebug != null)
                {
                    countOfHandlers = controlWithResultDebug.CountOfHandlersChangeDialogResult();
                }

            if (isControlWithResult)
            {
                controlWithResultDebug.AttachChangeDialogResult(new VoidBoolNullable( UserControlWithResult_ChangeDialogResult), false);
            }

            if (controlWithResultDebug != null)
                    {
                        countOfHandlers = controlWithResultDebug.CountOfHandlersChangeDialogResult();
                    }
        }

        if (addDialogButtons)
        {
            if (iUserControlInWindow != null)
            {
                // IF USER CONTROL HAVE OWN ChangeDialogResult, MUST USE ALWAYS IT
                iUserControlInWindow.ChangeDialogResult -= uc_ChangeDialogResult;
                userControlWithResult.ChangeDialogResult -= UserControlWithResult_ChangeDialogResult;
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

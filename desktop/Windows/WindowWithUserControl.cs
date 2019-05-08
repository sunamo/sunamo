using desktop;
using desktop.Controls;
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
    IUserControlInWindow uc = null;

    public WindowWithUserControl(IUserControlInWindow uc, ResizeMode rm, bool addDialogButtons = false)
    {
        this.Closed += WindowWithUserControl_Closed;
        this.uc = uc;

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

        this.ResizeMode = rm;
        // Původně bylo WidthAndHeight
        this.SizeToContent = System.Windows.SizeToContent.WidthAndHeight;
        this.MaxWidth = System.Windows.SystemParameters.PrimaryScreenWidth * 0.75d;
        this.MaxHeight = System.Windows.SystemParameters.PrimaryScreenHeight * 0.75d;
        this.Content = dock;
        this.HorizontalContentAlignment = HorizontalAlignment.Stretch;
        this.VerticalContentAlignment = VerticalAlignment.Stretch;
        this.HorizontalAlignment = HorizontalAlignment.Stretch;
        this.VerticalAlignment = VerticalAlignment.Stretch;

        Loaded += WindowWithUserControl_Loaded;
    }

    private void WindowWithUserControl_Loaded(object sender, RoutedEventArgs e)
    {
        uc.ChangeDialogResult += uc_ChangeDialogResult;
        userControl = (UserControl)uc;
        // TODO: Find method where is mass setted all aligments
        userControl.VerticalAlignment = VerticalAlignment.Stretch;
        userControl.VerticalContentAlignment = VerticalAlignment.Stretch;
        userControl.HorizontalAlignment = HorizontalAlignment.Stretch;
        userControl.HorizontalContentAlignment = HorizontalAlignment.Stretch;

        dock.Children.Add(userControl);

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
        uc.Accept(input);
    }
}

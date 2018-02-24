using desktop;
using desktop.Essential;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

/// <summary>
/// With this is allowed use logging via ThisApp (has own statusbar)
/// </summary>
public class WindowWithUserControl : Window
{
    public WindowWithUserControl(IUserControlInWindow uc, ResizeMode rm)
    {
        this.Closed += WindowWithUserControl_Closed;

        DockPanel dock = new DockPanel();
        dock.LastChildFill = true;

        StatusBar statusBar = new StatusBar();
        statusBar.Height = 25;

        TextBlock textBlockStatus = TextBlockHelper.Get("");
        ThisApp.SaveReferenceToTextBlockStatus(false, textBlockStatus, textBlockStatus);
        statusBar.Items.Add(textBlockStatus);
        DockPanel.SetDock(statusBar, Dock.Bottom);
        dock.Children.Add(statusBar);

        uc.ChangeDialogResult += uc_ChangeDialogResult;
        UserControl userControl = (UserControl)uc;
        // TODO: Find method where is mass setted all aligments
        userControl.VerticalAlignment = VerticalAlignment.Stretch;
        userControl.VerticalContentAlignment = VerticalAlignment.Stretch;
        userControl.HorizontalAlignment = HorizontalAlignment.Stretch;
        userControl.HorizontalContentAlignment = HorizontalAlignment.Stretch;

        dock.Children.Add(userControl);

        this.ResizeMode = rm;
        this.SizeToContent = System.Windows.SizeToContent.WidthAndHeight;
        this.MaxWidth = System.Windows.SystemParameters.PrimaryScreenWidth * 0.75d;
        this.MaxHeight = System.Windows.SystemParameters.PrimaryScreenHeight * 0.75d;
        this.Content = dock;
    }

    private void WindowWithUserControl_Closed(object sender, System.EventArgs e)
    {
        ThisApp.SaveReferenceToTextBlockStatus(true, null, null);
    }

    void uc_ChangeDialogResult(bool? b)
    {
        DialogResult = b;
    }
}

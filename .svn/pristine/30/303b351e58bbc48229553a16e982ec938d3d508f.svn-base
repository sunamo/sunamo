using System.ComponentModel;
using System.Windows;
public class WindowWithUserControl : Window
{
    public WindowWithUserControl(IUserControlInWindow uc, ResizeMode rm)
    {
        this.ResizeMode = rm;
        this.SizeToContent = System.Windows.SizeToContent.WidthAndHeight;
        this.MaxWidth = System.Windows.SystemParameters.PrimaryScreenWidth * 0.75d;
        this.MaxHeight = System.Windows.SystemParameters.PrimaryScreenHeight * 0.75d;
        this.Content = uc;
        uc.ChangeDialogResult += uc_ChangeDialogResult;
    }

    

    void uc_ChangeDialogResult(bool? b)
    {
        DialogResult = b;
    }
}

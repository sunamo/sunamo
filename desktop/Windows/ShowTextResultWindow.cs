using desktop.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

public class ShowTextResultWindow : Window, IControlWithResult
{
    ShowTextResult s = null;

    public ShowTextResultWindow(string text)
    {
         s = new ShowTextResult(text);
        Content = s;
        s.ChangeDialogResult += S_ChangeDialogResult;
    }

    public event VoidBoolNullable ChangeDialogResult;

    public void Accept(object input)
    {
        
    }

    public void FocusOnMainElement()
    {
        s.Focus();
    }

    private void S_ChangeDialogResult(bool? b)
    {
        // It's window, not user control, therefore I have to close, not calling ChangeDialogResult
        Close();
    }
}
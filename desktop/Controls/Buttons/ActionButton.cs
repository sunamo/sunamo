using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using sunamo;

public class ActionButton<T> : Button
{
    ButtonAction action = ButtonAction.SaveToClipboard;
    T what;
    public event VoidT<T> Remove; 
    public ActionButton(ButtonAction action, T what)
    {
        this.action = action;
        this.what  = what;

        this.Click += ActionButton_Click;
    }

    private void ActionButton_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        switch (action)
        {
            case ButtonAction.Nope:
                break;
            case ButtonAction.Remove:
                Remove(what);
                break;
            case ButtonAction.SaveToClipboard:
                ClipboardHelper.SetText(what.ToString());
                break;
            case ButtonAction.Run:
                PH.Start(what.ToString());
                break;
            default:
                break;
        }
    }
}
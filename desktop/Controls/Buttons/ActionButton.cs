using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

public class ActionButton<T> : Button
{
    sunamo.ButtonAction action = sunamo.ButtonAction.SaveToClipboard;
    T what;
    public event VoidT<T> Remove; 
    public ActionButton(sunamo.ButtonAction action, T what)
    {
        this.action = action;
        this.what  = what;

        this.Click += ActionButton_Click;
    }

    private void ActionButton_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        switch (action)
        {
            case sunamo.ButtonAction.Nope:
                break;
            case sunamo.ButtonAction.Remove:
                Remove(what);
                break;
            case sunamo.ButtonAction.SaveToClipboard:
                ClipboardHelper.SetText(what.ToString());
                break;
            case sunamo.ButtonAction.Run:
                PH.Start(what.ToString());
                break;
            default:
                break;
        }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

public class ActionButton<T> : Button
{
    sunamo.Action action = sunamo.Action.SaveToClipboard;
    T what;
    public event VoidT<T> Remove; 
    public ActionButton(sunamo.Action action, T what)
    {
        this.action = action;
        this.what  = what;

        this.Click += ActionButton_Click;
    }

    private void ActionButton_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        switch (action)
        {
            case sunamo.Action.Nope:
                break;
            case sunamo.Action.Remove:
                Remove(what);
                break;
            case sunamo.Action.SaveToClipboard:
                ClipboardHelper.SetText(what.ToString());
                break;
            case sunamo.Action.Run:
                PH.Start(what.ToString());
                break;
            default:
                break;
        }
    }
}


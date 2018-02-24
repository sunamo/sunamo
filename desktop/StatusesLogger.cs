using System.Windows.Media;
using System.Windows.Controls;
public class StatusesLogger
{
    // TODO: Merge with class ThisApp

    TextBlock tb = null;
    public StatusesLogger(TextBlock tb)
    {
        this.tb = tb;
    }

    public void Warning(string mes)
    {
        WriteWithColor(Brushes.Orange, mes);
    }

    private void WriteWithColor(Brush color, string mes)
    {
        string t = DTHelper.AppendToFrontOnlyTime(mes);
        tb.Foreground = color;
        tb.Text = t;
    }
}

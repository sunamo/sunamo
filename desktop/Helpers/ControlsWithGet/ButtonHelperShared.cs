using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Media.Imaging;

public static partial class ButtonHelper{ 


/// <summary>
    /// tag is not needed, value is obtained through []
    /// Tag here is mainly for comment what data control hold 
    /// </summary>
    /// <param name="tooltip"></param>
    /// <param name="imagePath"></param>
    public static Button Get(ControlInitData d)
    {
        Button vr = new Button();
        ControlHelper.SetForeground(vr, d.foreground);
        vr.Content = ContentControlHelper.GetContent(d);
        if (d.OnClick != null)
        {
            vr.Click += d.OnClick;
        }
        vr.Tag = d.tag;
        vr.ToolTip = d.tooltip;
        return vr;
    }

    public static void PerformClick(Button btnEnter)
    {
        btnEnter.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
    }
}
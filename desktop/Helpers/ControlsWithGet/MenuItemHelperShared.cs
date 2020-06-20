using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

public partial class MenuItemHelper{ 

    /// <summary>
    /// tag is not needed, value is obtained through []
    /// Tag here is mainly for comment what data control hold 
    /// </summary>
    /// <param name="header"></param>
    /// <param name="clickHandler"></param>
    public static MenuItem Get(ControlInitData d)
    {
        MenuItem mi = new MenuItem();
        mi.IsCheckable = d.checkable;

        if (d.foreground != null)
        {
            mi.Foreground = d.foreground;
        }

        if (d.OnClick != null)
        {
            mi.Click += d.OnClick;
        }
        mi.Tag = d.tag;
        mi.ToolTip = d.tooltip;

        // into Header I cant insert StackPanel from ContentControlHelper.GetContent( d);, because then is no show
        //mi.Header = d.text;
        mi.Header = ContentControlHelper.GetContent(d);

        return mi;
    }
}
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
    /// <returns></returns>
    public static MenuItem Get(ControlInitData d)
    {
        MenuItem mi = new MenuItem();
        mi.Header = d.text;
        if (d.OnClick != null)
        {
            mi.Click += d.OnClick;
        }

        mi.Foreground = d.foreground;
        mi.Tag = d.tag;
        return mi;
    }
}
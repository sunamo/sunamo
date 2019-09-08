using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

public partial class MenuItemHelper{ 
public static MenuItem CreateNew(string p)
    {
        MenuItem tsmi = new MenuItem();
        tsmi.Header = p;
        return tsmi;
    }
    /// <summary>
    /// tag is not needed, value is obtained through []
    /// Tag here is mainly for comment what data control hold 
    /// </summary>
    /// <param name="header"></param>
    /// <param name="clickHandler"></param>
    /// <returns></returns>
    public static MenuItem CreateNew(ControlInitData d)
    {
        MenuItem menuItem = CreateNew(d.text);
        menuItem.Click += d.OnClick;
        return menuItem;
    }
}
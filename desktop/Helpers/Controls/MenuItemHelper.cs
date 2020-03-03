using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
public partial class MenuItemHelper
{
    MenuItem mi = null;
    public MenuItemHelper(MenuItem mi)
    {
        this.mi = mi;
    }

    public void AddValuesOfEnumAsItems(Array bs, RoutedEventHandler eh)
    {
        foreach (object item in bs)
        {
            MenuItem tsmi = new MenuItem();
            tsmi.Header = item.ToString();
            tsmi.Tag = item;
            tsmi.Click += eh;
            mi.Items.Add(tsmi);
        }
    }

    /// <summary>
    /// A2 was onClick
    /// A4 was tag
    /// </summary>
    /// <param name="d"></param>
    
    public static MenuItem GetCheckable(ControlInitData d)
    {
        d.checkable = true;
        return Get(d);
    }
}
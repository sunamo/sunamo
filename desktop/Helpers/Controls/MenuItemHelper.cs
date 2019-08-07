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

    public static MenuItem CreateNewCheckable(string header, RoutedEventHandler miOnlyWithSameProjectName_Click, Brush foreground, object tag)
    {
        MenuItem mi = new MenuItem();
        mi.Header = header;
        if (miOnlyWithSameProjectName_Click != null)
        {
            mi.Click += miOnlyWithSameProjectName_Click;
        }

        mi.Foreground = foreground;
        mi.Tag = tag;
        return mi;
    }
}
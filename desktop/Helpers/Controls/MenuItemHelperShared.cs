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
public static MenuItem CreateNew(string header, RoutedEventHandler clickHandler)
    {
        MenuItem menuItem = CreateNew(header);
        menuItem.Click += clickHandler;
        return menuItem;
    }
}
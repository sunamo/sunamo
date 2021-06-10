using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

public partial class GridHelper{ 
public static Grid GetAutoSize(int columns, int rows)
    {
        Grid g = new Grid();
        GetAutoSize(g, columns, rows);
        return g;
    }
/// <summary>
    /// Assign to every cell GridLength.Auto
    /// </summary>
    /// <param name = "g"></param>
    /// <param name = "columns"></param>
    /// <param name = "rows"></param>
    public static void GetAutoSize(Grid g, int columns, int rows)
    {
        for (int i = 0; i < columns; i++)
        {
            g.ColumnDefinitions.Add(GetColumnDefinition(GridLength.Auto));
        }

        for (int i = 0; i < rows; i++)
        {
            g.RowDefinitions.Add(GetRowDefinition(GridLength.Auto));
        }
    }

public static ColumnDefinition GetColumnDefinition(GridLength oneC)
    {
        ColumnDefinition cd = new ColumnDefinition();
        cd.Width = oneC;
        return cd;
    }

    public static ColumnDefinition GetColumnDefinitionDirect(double pixels)
    {
        ColumnDefinition cd = new ColumnDefinition();
        cd.Width = new GridLength(pixels);
        return cd;
    }

    public static ColumnDefinition GetColumnDefinitionDirect(double value, GridUnitType type)
    {
        ColumnDefinition cd = new ColumnDefinition();
        cd.Width = new GridLength(value, type);
        return cd;
    }

    /// <summary>
    /// With auto and star must be alwys value 1. When will be 0, no controls will be show!!!
    /// </summary>
    /// <param name="auto"></param>
    public static RowDefinition GetRowDefinition(GridLength auto)
    {
        RowDefinition rd = new RowDefinition();
        rd.Height = auto;
        return rd;
    }

    public static RowDefinition GetRowDefinitionDirect(double pixels)
    {
        RowDefinition rd = new RowDefinition();
        rd.Height = new GridLength(pixels);
        return rd;
    }

    public static RowDefinition GetRowDefinitionDirect(double value, GridUnitType type)
    {
        RowDefinition rd = new RowDefinition();
        rd.Height = new GridLength(value, type);
        return rd;
    }

    /// <summary>
    /// Will increment A3 due to top
    /// </summary>
    /// <param name="grid"></param>
    /// <param name="row"></param>
    /// <param name="dx"></param>
    public static IEnumerable<T> GetControlsFrom<T>(Grid grid, bool row, int dx) where T : UIElement
    {
        dx++;

        IEnumerable<UIElement> uiElements = null;
        if (row)
        {

            uiElements = grid.Children.Cast<UIElement>().Where(s => Grid.GetRow(s) == dx);
        }
        else
        {
            uiElements = grid.Children.Cast<UIElement>().Where(s => Grid.GetColumn(s) == dx);
        }

        List<T> result = new List<T>();

        foreach (var item in uiElements)
        {
            //var v = ControlFinder.FindControlExclude<UIElement>(item);
            result.Add((T)item);
            int i =0;
        }

        return result;
    }
}
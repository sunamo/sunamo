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

public static RowDefinition GetRowDefinition(GridLength auto)
    {
        RowDefinition rd = new RowDefinition();
        rd.Height = auto;
        return rd;
    }

public static IEnumerable<CheckBox> GetControlsFrom(Grid grid, bool row, int dx)
    {
        if (row)
        {
            return grid.Children.Cast<CheckBox>().Where(s => Grid.GetRow(s) == dx);
        }
        else
        {
            return grid.Children.Cast<CheckBox>().Where(s => Grid.GetColumn(s) == dx);
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

public partial class GridHelper
{
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

    public static List<string> ForAllTheSame(int columns)
    {
        List<string> result = new List<string>(columns);
        var d = 100d / (double)columns;
        for (int i = 0; i < columns; i++)
        {
            result.Add(d + AllStrings.asterisk);
        }

        return result;
    }
}
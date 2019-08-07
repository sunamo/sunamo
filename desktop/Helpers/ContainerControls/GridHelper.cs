
using System.Collections.Generic;

public partial class GridHelper
{
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
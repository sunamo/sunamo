using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class GridHelperSunamo
{
    /// <summary>
    /// After can be use with XamlGeneratorDesktop.Write*Definitions 
    /// </summary>
    /// <param name="columns"></param>
    /// <returns></returns>
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
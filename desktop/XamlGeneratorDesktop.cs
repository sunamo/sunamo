using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using desktop;

public class XamlGeneratorDesktop : XamlGenerator
{
    static Type type = typeof(XamlGeneratorDesktop);

    /// <summary>
    /// in outer rows
    /// in inner columns
    /// </summary>
    /// <param name="elements"></param>
    public void Grid(List<List<string>> elements)
    {
        ThrowExceptions.HaveAllInnerSameCount(type, "Grid", elements);
        int rows = elements.Count;
        int columns = elements[0].Count;

        WriteTag("Grid");

        WriteColumnDefinitions(GridHelper.ForAllTheSame(columns));
        WriteRowDefinitions(GridHelper.ForAllTheSame(rows));

        for (int row = 0; row < rows; row++)
        {
            for (int column = 0; column < columns; column++)
            {
                string cell = elements[row][column];
                cell = cell.Replace("><", $" Grid.Column=\"{column}\" Grid.Row=\"{row}\"><");
                WriteRaw(cell);
            }
        }
        TerminateTag("Grid");
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// LIke a idiot I have developed this from https://stackoverflow.com/a/856918/9327173
/// </summary>
public class CmdTable
{

    static int tableWidth = 73;

    public static void CmdTable2(List<string> headers, List<List<string>> last)
    {
        var f = last.First();

        List<int> max = new List<int>(f.Count);
        CA.InitFillWith<int>(max, f.Count, 0);

        Console.Clear();
        PrintLine();

        for (int i = 0; i < last.Count(); i++)
        {
            for (int y = 0; y < f.Count; y++)
            {
                var l = last[i];
                var length = l[y].Length;
                max[i] = Math.Max(max[i], length);
            }
        }

        var header = AbSet(max, headers);

        PrintRow(header);

        PrintLine();

        for (int i = 0; i < last.Count; i++)
        {
            header = AbSet(max, last[i]);
            PrintRow(header);
        }

        
        PrintLine();
    }

    private static List<AB> AbSet(List<int> max, List<string> headers)
    {
        List<AB> ab = new List<AB>();

        for (int i = 0; i < max.Count; i++)
        {
            ab.Add(AB.Get(headers[i], max[i]));
        }
        return ab;
    }

    static void PrintLine()
    {
        Console.WriteLine(new string('-', tableWidth));
    }

    static void PrintRow(List<AB> columns)
    {
        int width = (tableWidth - columns.Count) / columns.Count;
        string row = "|";

        foreach (var column in columns)
        {
            row += AlignCentre(column.A, (int)column.B) + "|";
        }

        Console.WriteLine(row);
    }

    static string AlignCentre(string text, int width)
    {
        text = text.Length > width ? text.Substring(0, width - 3) + "..." : text;

        if (string.IsNullOrEmpty(text))
        {
            return new string(' ', width);
        }
        else
        {
            return text.PadRight(width - (width - text.Length) / 2).PadLeft(width);
        }
    }
}
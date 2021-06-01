using System.Collections.Generic;
using System;
using System.IO;
using System.Text;
using sunamo.Data;
using System.Collections;
using System.Linq;
using sunamo.Constants;
/// <summary>
/// Is not allowed write empty elements - split of strings is running with StringSplitOptions.RemoveEmptyEntries
/// Tato třída je zde pouze kvůli GetTablesInDatabaseExportHandler.ashx.cs a General/ImportTables.aspx.cs
/// Snaž se tuto třídu nikdy nevyužívat a naopak vše ukládat do db, popř. stf/sbf
/// 
/// Pokud chci uložit jen cesty, je SF výborné, | v cestě se nemůže vyskytnout
/// Takto musím přemýšlet
/// 
/// </summary>
public static partial class SF
{
public static int keyCodeSeparator
    {
        get
        {
            return (int)s_contentArgs.separatorChar;
        }
    }

    /// <summary>
    /// Must be property - I can forget change value on three occurences. 
    /// </summary>
    public static char separatorChar
    {
        get
        {
            return s_contentArgs.separatorChar;
        }
    }

    static SF()
    {
        s_contentArgs.separatorString = AllStrings.verbar;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name = "element"></param>
    /// <param name = "line"></param>
    /// <param name = "elements"></param>
    private static string GetElementAtIndex(List<List<string>> elements, int element, int line)
    {
        if (elements.Length() > line)
        {
            var lineElements = elements[line];
            if (lineElements.Length() > element)
            {
                return lineElements[element];
            }
        }

        return null;
    }

    private static List<string> GetLinesFromString(string p)
    {
        List<string> vr = new List<string>();
        StringReader sr = new StringReader(p);
        string f = null;
        while ((f = sr.ReadLine()) != null)
        {
            vr.Add(f);
        }

        return vr;
    }

    public static List<List<string>> AppendToFile(string path, string line)
    {
        var content = TF.ReadAllLines(path);
        CA.Trim(content);
        //content += Environment.NewLine + line + Environment.NewLine;
        content.Add(line);

        var vr = GetAllElementsLines(content);

        TF.SaveLines(content, path);

        return vr;
    }

    /// <summary>
    /// In inner array is elements, in outer lines.
    /// </summary>
    /// <param name = "file"></param>
    public static List<List<string>> GetAllElementsFile(string file)
    {
        
        var lines = TF.ReadAllLines(file);
        return GetAllElementsLines( lines);
    }

    private static List<List<string>> GetAllElementsLines( List<string> lines)
    {
        List<List<string>> vr = new List<List<string>>();
        foreach (string var in lines)
        {
            if (!string.IsNullOrWhiteSpace(var))
            {
                vr.Add(SF.GetAllElementsLine(var));
            }
        }
        return vr;
    }

    /// <summary>
    /// If index won't founded, return null.
    /// </summary>
    /// <param name = "element"></param>
    /// <param name = "line"></param>
    public static string GetElementAtIndexFile(string file, int element, int line)
    {
        List<List<string>> elements = GetAllElementsFile(file);
        return GetElementAtIndex(elements, element, line);
    }

    /// <summary>
    /// G null if first element on any lines A2 dont exists
    /// </summary>
    /// <param name = "file"></param>
    /// <param name = "element"></param>
    public static List<string> GetFirstWhereIsFirstElement(string file, string element)
    {
        List<List<string>> elementsLines = SF.GetAllElementsFile(file);
        for (int i = 0; i < elementsLines.Length(); i++)
        {
            if (elementsLines[i][0] == element)
            {
                return elementsLines[i];
            }
        }

        return null;
    }

    /// <summary>
    /// G null if first element on any lines A2 dont exists
    /// </summary>
    /// <param name = "file"></param>
    /// <param name = "element"></param>
    public static List<string> GetLastWhereIsFirstElement(string file, string element)
    {
        List<List<string>> elementsLines = SF.GetAllElementsFile(file);
        for (int i = elementsLines.Length() - 1; i >= 0; i--)
        {
            if (elementsLines[i][0] == element)
            {
                return elementsLines[i];
            }
        }

        return null;
    }





    /// <summary>
    /// Read text with first delimitech which automatically delimite
    /// </summary>
    /// <param name="fileNameOrPath"></param>
    public static void ReadFileOfSettingsOther(string fileNameOrPath)
    {
        // COmmented, app data not should be in *.web. pass directly as arg
        List<string> lines = null;
        //SH.GetLines(AppData.ci.ReadFileOfSettingsOther(fileNameOrPath));
        if (lines.Count > 1)
        {
            int delimiterInt;
            if (int.TryParse(lines[0], out delimiterInt))
            {
                separatorString = ((char)delimiterInt).ToString();
            }
        }
    }

    public static void WriteAllElementsToFile(string VybranySouborLogu, List<string>[] p)
    {
        StringBuilder sb = new StringBuilder();
        foreach (List<string> item in p)
        {
            sb.AppendLine(PrepareToSerialization3(item));
        }

        File.WriteAllText(VybranySouborLogu, sb.ToString());
    }

    public static void WriteAllElementsToFile(string VybranySouborLogu, List<List<string>> p)
    {
        StringBuilder sb = new StringBuilder();
        foreach (var item in p)
        {
            sb.AppendLine(PrepareToSerialization3(item));
        }

        File.WriteAllText(VybranySouborLogu, sb.ToString());
    }
}
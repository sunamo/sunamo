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
        s_contentArgs.separatorString = AllStrings.pipe;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name = "element"></param>
    /// <param name = "line"></param>
    /// <param name = "elements"></param>
    
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
            sb.AppendLine(PrepareToSerialization(item));
        }

        File.WriteAllText(VybranySouborLogu, sb.ToString());
    }

    public static void WriteAllElementsToFile(string VybranySouborLogu, List<List<string>> p)
    {
        StringBuilder sb = new StringBuilder();
        foreach (var item in p)
        {
            sb.AppendLine(PrepareToSerialization(item));
        }

        File.WriteAllText(VybranySouborLogu, sb.ToString());
    }
}
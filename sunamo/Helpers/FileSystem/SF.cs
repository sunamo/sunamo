using System.Collections.Generic;
using System;
using System.IO;
using System.Text;
using sunamo.Data;
/// <summary>
/// Is not allowed write empty elements - split of strings is running with  StringSplitOptions.RemoveEmptyEntries
/// Tato třída je zde pouze kvůli GetTablesInDatabaseExportHandler.ashx.cs a General/ImportTables.aspx.cs
/// Snaž se tuto třídu nikdy nevyužívat a naopak vše ukládat do db, popř. stf/sbf
/// 
/// </summary>
public static class SF
{
     static SerializeContentArgs contentArgs = new SerializeContentArgs();

    /// <summary>
    /// Must be property - I can forget change value on three occurences. 
    /// </summary>
    public static char separatorChar
    {
        get
        {
            return contentArgs.separatorChar;
        }
    }
    

    public static string separatorString
    {
        get { return contentArgs.separatorString; }
        set { contentArgs.separatorString = value; }
    }

    public static int keyCodeSeparator
    {
        get
        {
            return (int)contentArgs.separatorChar;
        }
    }

    static SF()
    {
        contentArgs.separatorString = "|";
    }

    public static void ReadFileOfSettingsOther(string fileNameOrPath)
    {
        
        var lines = SH.GetLines( AppData.ReadFileOfSettingsOther(fileNameOrPath));
        if (lines.Count > 1)
        {
            int delimiterInt;
            if (int.TryParse(lines[0], out delimiterInt))
            {
                separatorString = ((char)delimiterInt).ToString();
            }
            

        }
    }

    public static readonly char replaceForSeparatorChar = '_';
    public const string replaceForSeparatorString = "_";

    /// <summary>
    /// If index won't founded, return null.
    /// </summary>
    /// <param name="element"></param>
    /// <param name="line"></param>
    /// <returns></returns>
    public static string GetElementAtIndexFile(string file, int element, int line)
    {
        string[][] elements = GetAllElementsFile(file);
        return GetElementAtIndex(elements, element, line);
    }

    /// <summary>
    /// G null if first element on any lines A2 dont exists
    /// </summary>
    /// <param name="file"></param>
    /// <param name="element"></param>
    /// <returns></returns>
    public static string[] GetFirstWhereIsFirstElement(string file, string element)
    {
        string[][] elementsLines = SF.GetAllElementsFile(file);
        for (int i = 0; i < elementsLines.Length; i++)
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
    /// <param name="file"></param>
    /// <param name="element"></param>
    /// <returns></returns>
    public static string[] GetLastWhereIsFirstElement(string file, string element)
    {
        string[][] elementsLines = SF.GetAllElementsFile(file);
        for (int i = elementsLines.Length - 1; i >= 0; i--)
        {
            if (elementsLines[i][0] == element)
            {
                return elementsLines[i];
            }
        }
        return null;
    }

    /// <summary>
    /// In inner array is elements, in outer lines.
    /// </summary>
    /// <param name="file"></param>
    /// <returns></returns>
    public static string[][] GetAllElementsFile(string file)
    {
        List<string[]> vr = new List<string[]>();
        string[] lines = File.ReadAllLines(file);

        foreach (string var in lines)
        {
            vr.Add(SF.GetAllElementsLine(var));
        }
        return vr.ToArray();
    }

    public static string[][] GetAllElementsFileAdvanced(string file, out string[] hlavicka, char oddelovaciZnak = '|')
    {
        string oz = oddelovaciZnak.ToString();
        List<string[]> vr = new List<string[]>();
        string[] lines = File.ReadAllLines(file);
        lines = CA.Trim(lines);
        hlavicka = SF.GetAllElementsLine(lines[0], '\'');
        int musiByt = SH.OccurencesOfStringIn(lines[0], oz);
        int nalezeno = 0;
        StringBuilder jedenRadek = new StringBuilder();
        for (int i = 1; i < lines.Length; i++)
        {
            nalezeno += SH.OccurencesOfStringIn(lines[i], oz);
            jedenRadek.AppendLine(lines[i]);
            if (nalezeno == musiByt)
            {
                nalezeno = 0;
                string[] columns = SF.GetAllElementsLine(jedenRadek.ToString(), '\'');
                jedenRadek = new StringBuilder();
                vr.Add(columns);
            }
        }

        return vr.ToArray();
    }

    private static string[] GetLinesFromString(string p)
    {
        List<string> vr = new List<string>();
        StringReader sr = new StringReader(p);
        string f = null;
        while ((f = sr.ReadLine()) != null)
        {
            vr.Add(f);
        }
        return vr.ToArray();
    }

    

    /// <summary>
    /// 
    /// </summary>
    /// <param name="var"></param>
    /// <returns></returns>
    public static string[] GetAllElementsLine(string var, char oddelovaciZnak = '|')
    {
        // Musí tu být none, protože pak když někde nic nebylo, tak mi to je nevrátilo a progran vyhodil IndexOutOfRangeException
        return var.Split(new char[] { oddelovaciZnak }, StringSplitOptions.None);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="element"></param>
    /// <param name="line"></param>
    /// <param name="elements"></param>
    /// <returns></returns>
    private static string GetElementAtIndex(string[][] elements, int element, int line)
    {
        if (elements.Length > line)
        {
            string[] lineElements = elements[line];
            if (lineElements.Length > element)
            {
                return lineElements[element];
            }
        }
        return null;
    }

    public static string PrepareToSerialization(params object[] o)
    {
        return  SH.GetString(o, separatorString);
    }



    /// <summary>
    /// Vrátí bez poslední 
    /// </summary>
    /// <param name="o"></param>
    /// <returns></returns>
    public static string PrepareToSerialization2(params object[] o)
    {
        string vr = SH.GetString(o, separatorChar.ToString());
        return vr.Substring(0, vr.Length - 1);
    }

    public static void WriteAllElementsToFile(string VybranySouborLogu, string[][] p)
    {
        StringBuilder sb = new StringBuilder();
        foreach (string[] item in p)
        {
            sb.AppendLine(PrepareToSerialization(item));
        }
        File.WriteAllText(VybranySouborLogu, sb.ToString());
    }

    public static string PrepareToSerializationExplicit(char p1, object[] o)
    {
        string vr = SH.GetString(o, p1.ToString());
        return vr.Substring(0, vr.Length - 1);
    }




}

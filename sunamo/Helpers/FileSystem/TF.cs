using sunamo;
using sunamo.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public partial class TF
{
    public static string ReadFileParallel(string fileName, IList<string> from, IList<string> to)
    {
        return ReadFileParallel(fileName, 1470, from, to);
    }

    public static string ReadFileParallel(string fileName, int linesCount, IList<string> from, IList<string> to)
    {
        string[] AllLines = new string[linesCount]; //only allocate memory here
        using (StreamReader sr = File.OpenText(fileName))
        {
            int x = 0;
            while (!sr.EndOfStream)
            {
                AllLines[x] = sr.ReadLine();
                x += 1;
            }
        } //CLOSE THE FILE because we are now DONE with it.

        if (from != null)
        {
            for (int i = 0; i < from.Count; i++)
            {
                Parallel.For(0, AllLines.Length, x =>
                {
                    AllLines[x] = AllLines[x].Replace(from[i], to[i]);
                });
            }
        }
        return string.Empty;
    }

    public static List<string> ReadConfigLines(string syncLocations)
    {
        var l = TF.ReadAllLines(syncLocations);
        CA.Trim(l);
        CA.RemoveStartingWith(AllStrings.num, l);
        return l;
    }

    public static byte[] ReadAllBytesArray(string item)
    {
        return File.ReadAllBytes(item);
    }

    public static Encoding GetEncoding(string filename)
    {
        var file = new FileStream(filename, FileMode.Open, FileAccess.Read);
        // Read the BOM
        var enc = GetEncoding(file);
        file.Dispose();
        return enc;
    }

    /// <summary>
    /// Dont working, with Air bank export return US-ascii / 1252, file has diacritic
    /// Atom with auto-encoding return ISO-8859-2 which is right
    /// </summary>
    /// <param name="file"></param>
    public static Encoding GetEncoding(FileStream file)
    {
        var bom = new byte[4];

        file.Read(bom, 0, 4);
        return EncodingHelper.DetectEncoding(new List<byte>(bom));
    }

    private static void AppendToStartOfFileIfDontContains(List<string> files, string append)
    {
        append += Environment.NewLine;

        foreach (var item in files)
        {
            string content = TF.ReadAllText(item);
            if (!content.Contains(append))
            {
                content = append + content;
                File.WriteAllText(item, content);
            }
        }
    }





    /// <summary>
    /// ...
    /// </summary>
    /// <param name="file"></param>
    public static int GetNumberOfLinesTrimEnd(string file)
    {
        List<string> lines = GetAllLines(file);
        for (int i = lines.Count - 1; i >= 0; i--)
        {
            if (lines[i].Trim() != "")
            {
                return i;
            }
        }
        return 0;
    }









    private static void ReplaceIfDontStartWith(List<string> files, string contains, string prefix)
    {
        foreach (var item in files)
        {
            var lines = TF.ReadAllLines<string, string>(item, null);
            for (int i = 0; i < lines.Count; i++)
            {
                string line = lines[i].Trim();
                if (line.StartsWith(contains))
                {
                    lines[i] = lines[i].Replace(contains, prefix + contains);
                }
            }

            File.WriteAllLines(item, lines);
        }
    }

    /// <summary>
    /// Return all lines except empty
    /// GetLines return ALL lines, include empty
    /// 
    /// Lze použít pouze pokud je A1 cesta ke serializovatelnému souboru, nikoliv samotný ser. řetězec
    /// Vrátí všechny řádky vytrimované z A1, ale nikoliv deserializované
    /// Every non empty line trim, every empty line don't return
    /// </summary>
    /// <param name="file"></param>
    public static List<string> GetAllLines(string file)
    {
        return File.ReadAllLines(file).ToList();
    }
}
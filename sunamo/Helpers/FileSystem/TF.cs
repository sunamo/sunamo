using sunamo;
using sunamo.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

public partial class TF
{
    public static List< byte> bomUtf8 = CA.ToList<byte>(239, 187, 191);

    public static Encoding GetEncoding(string filename)
    {
        var file = new FileStream(filename, FileMode.Open, FileAccess.Read);
        // Read the BOM
        var enc = GetEncoding(file);
        file.Dispose();
        return enc;
    }

    public static void RemoveDoubleBomUtf8(string path)
    {
        var b = TF.ReadAllBytes(path);
        var to = b.Count > 5 ? 6 : b.Count;

        var isUtf8TwoTimes = true;

        for (int i = 3; i < to; i++)
        {
            if (bomUtf8[i - 3] != b[i])
            {
                isUtf8TwoTimes = false;
                break;
            }
        }

         b = b.Skip(3).ToList();
        TF.WriteAllBytes(path, b);
    }

    /// <summary>
    /// Dont working, with Air bank export return US-ascii / 1252, file has diacritic
    /// Atom with auto-encoding return ISO-8859-2 which is right
    /// </summary>
    /// <param name="file"></param>
    /// <returns></returns>
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
            string content = File.ReadAllText(item);
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
    /// <returns></returns>
    public static int GetNumberOfLinesTrimEnd(string file)
    {
        string[] lines = GetAllLines(file);
        for (int i = lines.Length - 1; i >= 0; i--)
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
    /// <returns></returns>
    public static string[] GetAllLines(string file)
    {
        List<string> lines = TF.GetLines<string,string>(file, null);
        List<string> linesPpk = new List<string>();
        for (int i = 0; i < lines.Count; i++)
        {
            string trim = lines[i].Trim();
            if (trim != "")
            {
                linesPpk.Add(trim);
            }
        }
        return linesPpk.ToArray();
    }
}
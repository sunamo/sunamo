using sunamo;
using sunamo.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

public class TF
{
    /// <summary>
    /// Precte soubor a vrati jeho obsah. Pokud soubor neexistuje, vytvori ho a vrati SE. 
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static string ReadFile(string s)
    {
        FS.MakeUncLongPath(ref s);
        if (File.Exists(s))
        {
            return File.ReadAllText(s, Encoding.UTF8);
        }
        else
        {
            File.WriteAllText(s, "", Encoding.UTF8);
        }

        return "";
    }

    #region Encoding
    public static Encoding GetEncoding(string filename)
    {
        var file = new FileStream(filename, FileMode.Open, FileAccess.Read);
        // Read the BOM
        var enc = GetEncoding(file);
        file.Dispose();
        return enc;
    }

    public static Encoding GetEncoding(FileStream file)
    {
        var bom = new byte[4];

        file.Read(bom, 0, 4);
        return EncodingHelper.DetectEncoding(bom);
    } 
    #endregion

    static void AppendToStartOfFileIfDontContains(List<string> files, string append)
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
    /// Lze použít pouze pokud je A1 cesta ke serializovatelnému souboru, nikoliv samotný ser. řetězec
    /// Vrátí všechny řádky vytrimované z A1, ale nikoliv deserializované
    /// Every non empty line trim, every empty line don't return
    /// </summary>
    /// <param name="file"></param>
    /// <returns></returns>
    public static string[] GetAllLines(string file)
    {

        List<string> lines = TF.GetLines(file);
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

    

    

    


    public static List<string> GetLines(string sobuor)
    {
        return SH.GetLines(ReadFile(sobuor));
    }

    public static void AppendToFile(string obsah, string soubor)
    {
        SaveFile(obsah, soubor, true);
    }

    public static void CreateEmptyFileWhenDoesntExists(string path)
    {
        if (!File.Exists(path))
        {
            sunamo.FS.CreateUpfoldersPsysicallyUnlessThere(path);
            File.WriteAllText(path, "");
        }
    }

    

    static void SaveFile(string obsah, string soubor, bool pripsat)
    {
        
        if (pripsat)
        {
            File.AppendAllText(soubor, obsah);
        }
        else
        {
            File.WriteAllText(soubor, obsah);
        }
    }

    public static void SaveFile(string obsah, string soubor)
    {
        SaveFile(obsah, soubor, false);
    }

    static void ReplaceIfDontStartWith(List<string> files, string contains, string prefix)
    {
        foreach (var item in files)
        {
            string[] lines = File.ReadAllLines(item);
            for (int i = 0; i < lines.Length; i++)
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
}

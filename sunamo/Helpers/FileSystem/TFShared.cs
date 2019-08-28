using sunamo.Data;
using sunamo.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

public partial class TF
{
    /// <summary>
    /// Precte soubor a vrati jeho obsah. Pokud soubor neexistuje, vytvori ho a vrati SE. 
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static string ReadFile(string s)
    {
        FS.MakeUncLongPath(ref s);

        string result = string.Empty;

        if (FS.ExistsFile(s))
        {
            while (true)
            {
                try
                {
                    /*
                     * Měl jsme tu chybu not supported format path
                     * cesta byla na první pohled v pořádku
                     * zjistil jsem že v popředí bylo 2x BOM encoding utf8
                     * tyto znaky jsou bílé a tím pádem sjem je neviděl
                     * resetoval jsem komp. nastavil nové práva atd.
                     * všechno to začalo s používáním DetectEncoding
                     * 
                     * STAČILO BOHATĚ RESETOVAT KOMP, MOŽNÁ I JEN VRÁTIT TO DO BEZ DetectEncoding!!!
                     * 
                     */
                    //var bytesArray = File.ReadAllBytes(s);
                    //var bytes = bytesArray.ToList();
                    //var enc = EncodingHelper.DetectEncoding(bytes);



                    //result = enc.GetString(bytesArray);
                    result = File.ReadAllText(s);
                    break;
                }
                catch (Exception ex)
                {
                    Thread.Sleep(500);
                }
            }
        }
        else
        {


            File.WriteAllText(s, "", Encoding.UTF8);
        }

        return result;
    }

    private static void SaveFile(string obsah, string soubor, bool pripsat)
    {
        if (soubor == null)
        {
            return;
        }
        if (pripsat)
        {
            while (true)
            {
                try
                {
                    File.AppendAllText(soubor, obsah, Encoding.UTF8);
                    return;
                }
                catch (Exception ex)
                {
                    Thread.Sleep(500);
                }
            }
        }
        else
        {
            while (true)
            {
                try
                {
                    File.WriteAllText(soubor, obsah, Encoding.UTF8);
                    return;
                }
                catch (Exception ex)
                {
                    // not for errors like path could not found etc.
                    if (ex.Message.Contains("ccess"))
                    {
                        Thread.Sleep(500);
                    }
                    //
                }
            }
        }
    }

    public static void WriteAllLines(string file, List<string> lines)
    {
        SaveLines(lines, file);
    }

    public static void SaveFile(string obsah, string soubor)
    {
        SaveFile(obsah, soubor, false);
    }

    public static void Replace(string pathCsproj, string to, string from)
    {
        string content = TF.ReadFile(pathCsproj);
        content = content.Replace(to, from);
        TF.SaveFile(content, pathCsproj);
    }

    public static void PureFileOperation(string f, Func<string, string> transformHtmlToMetro4)
    {
        var content = TF.ReadFile(f);
        content = transformHtmlToMetro4.Invoke(content);
        TF.SaveFile(content, f);
    }

    public static void SaveFile(string content, StorageFile storageFile)
    {
        TF.SaveFile(content, storageFile.FullPath());
    }

    public static List<string> ReadAllLines(string file)
    {
        return SH.GetLines(ReadFile(file));
    }

    public static void CreateEmptyFileWhenDoesntExists(string path)
    {
        if (!FS.ExistsFile(path))
        {
            FS.CreateUpfoldersPsysicallyUnlessThere(path);
            File.WriteAllText(path, "");
        }
    }

    public static void WriteAllBytes(string file, List<byte> b)
    {
        File.WriteAllBytes(file, b.ToArray());
    }

    public static List<byte> ReadAllBytes(string file)
    {
        return File.ReadAllBytes(file).ToList();

        using (System.IO.FileStream input = new System.IO.FileStream(file,
                                    System.IO.FileMode.Open,
                                    System.IO.FileAccess.Read))
        {
            byte[] buffer = new byte[input.Length];

            int readLength = 0;

            while (readLength < buffer.Length)
                readLength += input.Read(buffer, readLength, buffer.Length - readLength);

            return buffer.ToList();
        }
    }

    /// <summary>
    /// StreamReader is derived from TextReader
    /// </summary>
    /// <param name="file"></param>
    /// <returns></returns>
    public static StreamReader TextReader(string file)
    {
        return  File.OpenText(file);
    }

    public static void WriteAllText(string file, string content)
    {
        File.WriteAllText(file, content);
    }

    public static void AppendToFile(string obsah, string soubor)
    {
        SaveFile(obsah, soubor, true);
    }

    public static List<string> GetLines(string item)
    {
        return ReadAllLines(item);
    }

public static void SaveLines(List<string> list, string file)
    {
        SaveFile(SH.JoinNL(list), file);
    }
}
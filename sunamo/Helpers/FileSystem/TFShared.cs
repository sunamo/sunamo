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
    static Type type = typeof(TF);

    public static string ReadFile(string s)
    {
        return ReadFile<string, string>(s);
    }

    /// <summary>
    /// Precte soubor a vrati jeho obsah. Pokud soubor neexistuje, vytvori ho a vrati SE. 
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static string ReadFile<StorageFolder, StorageFile>(StorageFile s, AbstractCatalog<StorageFolder, StorageFile> ac = null)
    {

        if (ac == null)
        {
            FS.MakeUncLongPath<StorageFolder, StorageFile>(ref s, ac);
        }
        

        string result = string.Empty;

        if (FS.ExistsFile(s, ac))
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

                    if (ac == null)
                    {
                        //result = enc.GetString(bytesArray);
                        result = File.ReadAllText(s.ToString());
                    }
                    else
                    {
                        result = ac.tf.readAllText(s);
                    }
                    
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
            TF.WriteAllText<StorageFolder, StorageFile>(s, string.Empty, ac);
            
        }

        return result;//
    }

    private static void SaveFile(string obsah, string soubor, bool pripsat)
    {
        var dir = FS.GetDirectoryName(soubor);

        ThrowExceptions.DirectoryWasntFound(type, "SaveFile", dir);

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

    //public static void SaveFile(string content, StorageFile storageFile)
    //{
    //    TF.SaveFile(content, storageFile.FullPath());
    //}

    public static List<string> ReadAllLines(string file)
    {
        return ReadAllLines<string, string>(file, null);
    }


    public static List<string> ReadAllLines<StorageFolder, StorageFile>(StorageFile file, AbstractCatalog<StorageFolder, StorageFile> ac)
    {
        return SH.GetLines(ReadFile<StorageFolder, StorageFile>(file,ac));
    }

    public static void CreateEmptyFileWhenDoesntExists(string path)
    {
        CreateEmptyFileWhenDoesntExists<string, string>(path, null);
    }

    public static void CreateEmptyFileWhenDoesntExists<StorageFolder, StorageFile>(StorageFile path, AbstractCatalog<StorageFolder, StorageFile> ac)
    {
        if (!FS.ExistsFile(path, ac))
        {
            FS.CreateUpfoldersPsysicallyUnlessThere<StorageFolder, StorageFile>(path, ac);
            TF.WriteAllText<StorageFolder, StorageFile>(path, "", Encoding.UTF8, ac);
        }
    }

    public static void WriteAllBytes(string file, List<byte> b)
    {
        WriteAllBytes<string, string>(file, b, null);
    }

    public static void WriteAllBytes<StorageFolder, StorageFile>(StorageFile file, List<byte> b, AbstractCatalog<StorageFolder, StorageFile> ac)
    {
        if (ac == null)
        {
            File.WriteAllBytes(file.ToString(), b.ToArray());
            
        }
        else
        {
            ac.tf.writeAllBytes(file, b);
        }
        
    }

    public static List<byte> ReadAllBytes(string file)
    {
        if (File.Exists(file))
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
        return new List<byte>();
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

    public static void WriteAllText(string file, StringBuilder sb)
    {
        WriteAllText(file, sb.ToString());
    }

    public static void WriteAllText(string file, string content, Encoding encoding)
    {
        WriteAllText<string, string>(file, content, encoding, null);
    }

    public static void WriteAllText(string file, string content)
    {
        WriteAllText<string, string>(file, content, Encoding.UTF8, null);
    }

    public static void WriteAllText<StorageFolder, StorageFile>(StorageFile file, string content, AbstractCatalog<StorageFolder, StorageFile> ac)
    {
        WriteAllText<StorageFolder, StorageFile>(file, content, Encoding.UTF8, ac);
    }

    /// <summary>
    /// A1 cant be storagefile because its
    /// not in 
    /// </summary>
    /// <param name="file"></param>
    /// <param name="content"></param>
    public static void WriteAllText<StorageFolder, StorageFile>(StorageFile file, string content, Encoding enc, AbstractCatalog<StorageFolder, StorageFile> ac)
    {
        if ( ac  == null)
        {
            File.WriteAllText(file.ToString(), content, enc);
        }
        else
        {
            ac.tf. writeAllText.Invoke(file, content);
        }
    }

    public static void AppendToFile(string obsah, string soubor)
    {
        SaveFile(obsah, soubor, true);
    }

    public static List<string> GetLines(string item)
    {
        return GetLines<string, string>(item, null);
    }

    public static List<string> GetLines<StorageFolder, StorageFile>(StorageFile item, AbstractCatalog<StorageFolder, StorageFile> ac)
    {
        return ReadAllLines<StorageFolder, StorageFile>(item, ac);
    }

public static void SaveLines(List<string> list, string file)
    {
        SaveFile(SH.JoinNL(list), file);
    }
}
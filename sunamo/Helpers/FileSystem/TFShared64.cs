using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public partial class TF
{
    static Type type = typeof(TF);
    public static Func<string, bool> isUsed = null;

    public static List<string> GetLines(string item)
    {
        return GetLines<string, string>(item, null);
    }

    public static List<string> GetLines<StorageFolder, StorageFile>(StorageFile item, AbstractCatalog<StorageFolder, StorageFile> ac)
    {
        return ReadAllLines<StorageFolder, StorageFile>(item, ac);
    }

    public static List<byte> ReadAllBytes(string file)
    {
        return File.ReadAllBytes(file).ToList();
    }

    public static void WriteAllLines(string file, List<string> lines)
    {
        SaveLines(lines, file);
    }

    public static void SaveLines(IList<string> list, string file)
    {
        File.WriteAllLines(file, list);
    }

    public static List<string> ReadAllLines(string file)
    {
        return ReadAllLines<string, string>(file, null);
    }


    public static List<string> ReadAllLines<StorageFolder, StorageFile>(StorageFile file, AbstractCatalog<StorageFolder, StorageFile> ac)
    {
        return SH.GetLines(ReadFile<StorageFolder, StorageFile>(file, ac));
    }

    public static void WriteAllText(string path, string content)
    {
        WriteAllText<string, string>(path, content, null);
    }
    public static string ReadAllText(string f)
    {

        return FS.ReadAllText(f);
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


    /// <summary>
    /// A1 cant be storagefile because its
    /// not in 
    /// </summary>
    /// <param name="file"></param>
    /// <param name="content"></param>
    public static void WriteAllText<StorageFolder, StorageFile>(StorageFile file, string content, Encoding enc, AbstractCatalog<StorageFolder, StorageFile> ac)
    {
        if (ac == null)
        {
            File.WriteAllText(file.ToString(), content, enc);
        }
        else
        {
            ac.tf.writeAllText.Invoke(file, content);
        }
    }


    /// <summary>
    /// Create folder hiearchy and write
    /// </summary>
    /// <param name="path"></param>
    /// <param name="content"></param>
    public static void WriteAllText<StorageFolder, StorageFile>(StorageFile path, string content, AbstractCatalog<StorageFolder, StorageFile> ac)
    {
        FS.CreateUpfoldersPsysicallyUnlessThere(path, ac);
        TF.WriteAllText<StorageFolder, StorageFile>(path, content, ac);
    }

    public static void SaveFile(string obsah, string soubor)
    {
        SaveFile(obsah, soubor, false);
    }

    private static void SaveFile(string obsah, string soubor, bool pripsat)
    {

        var dir = FS.GetDirectoryName(soubor);

        ThrowExceptions.DirectoryWasntFound(Exc.GetStackTrace(), type, "SaveFile", dir);

        if (soubor == null)
        {
            return;
        }
        if (pripsat)
        {
            File.AppendAllText(soubor, obsah);
        }
        else
        {
            File.WriteAllText(soubor, obsah, Encoding.UTF8);
        }
    }


    public static void AppendToFile(string obsah, string soubor)
    {
        SaveFile(obsah, soubor, true);
    }

    /// <summary>
    /// Return string.Empty when file won't exists
    /// Use FileUtil.WhoIsLocking to avoid error The process cannot access the file because it is being used by another process
    /// </summary>
    /// <param name="s"></param>
    public static string ReadFile(string s)
    {
        return ReadFile<string, string>(s);
    }

    /// <summary>
    /// Precte soubor a vrati jeho obsah. Pokud soubor neexistuje, vytvori ho a vrati SE. 
    /// </summary>
    /// <param name="s"></param>
    public static string ReadFile<StorageFolder, StorageFile>(StorageFile s, AbstractCatalog<StorageFolder, StorageFile> ac = null)
    {
        if (!File.Exists(s.ToString()))
        {
            return string.Empty;
        }

        if (ac == null)
        {
            FS.MakeUncLongPath<StorageFolder, StorageFile>(ref s, ac);
        }

        var ss = s.ToString();

        if (isUsed != null)
        {
            if (isUsed.Invoke(ss))
            {
                return string.Empty;
            }
        }

        if (ac == null)
        {
            //result = enc.GetString(bytesArray);
            return FS.ReadAllText(s.ToString());
        }
        else
        {
            return ac.tf.readAllText(s);
        }
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

}

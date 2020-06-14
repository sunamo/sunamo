using sunamo.Data;
using sunamo.Essential;
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
    public static List<byte> bomUtf8 = CA.ToList<byte>(239, 187, 191);

    /// <summary>
    /// Return string.Empty when file won't exists
    /// Use FileUtil.WhoIsLocking to avoid error The process cannot access the file because it is being used by another process
    /// </summary>
    /// <param name="s"></param>
    public static string ReadFile(string s)
    {
        return ReadFile<string, string>(s);
    }

    public static Func<string, bool> isUsed = null;

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
            return File.ReadAllText(s.ToString());
        }
        else
        {
            return ac.tf.readAllText(s);
        }
    }

    private static void SaveFile(string obsah, string soubor, bool pripsat)
    {
        var dir = FS.GetDirectoryName(soubor);

        ThrowExceptions.DirectoryWasntFound(Exc.GetStackTrace(),type, "SaveFile", dir);

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

    public static void PureFileOperation(string f, Func<string, string> transformHtmlToMetro4, string insertBetweenFilenameAndExtension)
    {
        var content = TF.ReadFile(f);
        content = transformHtmlToMetro4.Invoke(content);
        TF.SaveFile(content, FS.InsertBetweenFileNameAndExtension( f, insertBetweenFilenameAndExtension));
    }

    public static void PureFileOperation(string f, Func<string, string> transformHtmlToMetro4)
    {
        var content = TF.ReadFile(f).Trim();
        var content2 = transformHtmlToMetro4.Invoke(content);
        if (String.Compare( content, content2) != 0)
        {
            //TF.SaveFile(content, CompareFilesPaths.GetFile(CompareExt.cs, 1));
            //TF.SaveFile(content2, CompareFilesPaths.GetFile(CompareExt.cs, 2));
            TF.SaveFile(content2, f);
        }
    }

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
        return File.ReadAllBytes(file).ToList();
    }

    /// <summary>
    /// StreamReader is derived from TextReader
    /// </summary>
    /// <param name="file"></param>
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
        File.WriteAllLines( file, list);
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

    public static string ReadAllText(string path)
    {
        if (isUsed != null)
        {
            if (isUsed.Invoke(path))
            {
                return string.Empty;
            }
        }


        return File.ReadAllText(path);
    }
}
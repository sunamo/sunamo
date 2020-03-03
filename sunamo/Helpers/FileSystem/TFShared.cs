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
    /// </summary>
    /// <param name="s"></param>
    
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
}
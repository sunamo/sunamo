using sunamo.Constants;
using sunamo.Data;
using sunamo.Enums;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

public partial class FS{
    /// <summary>
    /// Dont check for size
    /// Into A2 is good put true - when storage was fulled, all new files will be written with zero size. But then failing because HtmlNode as null - empty string as input
    /// But when file is big, like backup of DB, its better false.. Then will be avoid reading whole file to determining their size and totally blocking HW resources on VPS
    /// </summary>
    /// <param name="selectedFile"></param>
    public static bool ExistsFile(string selectedFile, bool falseIfSizeZeroOrEmpty = true)
    {
        if (selectedFile == Consts.UncLongPath || selectedFile == string.Empty)
        {
            return false;
        }

        var exists = File.Exists(selectedFile);

        if (falseIfSizeZeroOrEmpty)
        {
            if (!exists)
            {
                return false;
            }
            else 
            {
                var ext = FS.GetExtension(selectedFile).ToLower();
                // Musím to kontrolovat jen když je to tmp, logicky
                if (ext == AllExtensions.tmp)
                {
                    return false;
                }
                else
                {
                    if (TF.ReadFile(selectedFile) == string.Empty)
                    {
                        // Měl jsem tu chybu že ač exists bylo true, TF.ReadFile selhalo protože soubor neexistoval. 
                        // Vyřešil jsem to kontrolou přípony, snad
                        return false;
                    }
                }
            }
        }
        return exists;
    }

    /// <summary>
    /// For empty or whitespace return false.
    /// </summary>
    /// <param name="selectedFile"></param>
    public static bool ExistsFile<StorageFolder, StorageFile>(StorageFile selectedFile, AbstractCatalog<StorageFolder, StorageFile> ac = null)
    {
        if (ac == null)
        {
            return ExistsFile(selectedFile.ToString());
        }
        return ac.fs.existsFile.Invoke(selectedFile);
    }
    public static DateTime LastModified(string rel)
    {
        var  f = new FileInfo(rel);
        return f.LastWriteTime;

    }

    

    public static bool IsException(string ext)
    {
        if (string.IsNullOrWhiteSpace( ext ))
        {
            return false;
        }
        ext = ext.TrimStart(AllChars.dot);
        return SH.ContainsOnly(ext, RandomHelper.vsZnakyWithoutSpecial);
    }

    public static string PathSpecialAndLevel(string basePath, string item, int v)
    {
        basePath = basePath.Trim(AllChars.bs);
        
        item = item.Trim(AllChars.bs);

        item = item.Replace(basePath, string.Empty);
        var pBasePath = SH.Split(basePath, AllStrings.bs);
        var basePathC = pBasePath.Count;

        var p = SH.Split(item, AllStrings.bs);
        int i = 0;
        for (; i < p.Count; i++)
        {
            if (p[i].StartsWith(AllStrings.lowbar))
            {
                pBasePath.Add(p[i]);
            }
            else
            {
                //i--;
                break;
            }
        }
        for (int y = 0; y < i; y++)
        {
            p.RemoveAt(0);
        }

        var h = p.Count - i + basePathC;
        var to = Math.Min(v, h);
        i = 0;
        for (; i < to; i++)
        {
            pBasePath.Add(p[i]);
        }

        return SH.Join(AllStrings.bs, pBasePath);
    }
}
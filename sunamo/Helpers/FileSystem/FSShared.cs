using sunamo.Constants;
using sunamo.Data;
using sunamo.Enums;
using sunamo.Essential;
using sunamo.Values;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

public partial class FS
{
    private static List<char> s_invalidPathChars = null;
    private static Type type = typeof(FS);

    private static List<char> s_invalidFileNameChars = null;
    private static List<char> s_invalidCharsForMapPath = null;
    private static List<char> s_invalidFileNameCharsWithoutDelimiterOfFolders = null;


    /// <summary>
    /// 
    /// When is occur Access denied exception, use GetFilesEveryFolder, which find files in every folder
    /// A1 have to be with ending backslash
    /// A4 must have underscore otherwise is suggested while I try type true
    /// A2 can be delimited by semicolon
    /// </summary>
    /// <param name="folder"></param>
    /// <param name="mask"></param>
    /// <param name="searchOption"></param>
    
    public static string RepairFilter(string filter)
    {
        if (!filter.Contains(AllStrings.pipe))
        {
            filter = filter.TrimStart(AllChars.asterisk);
            return AllStrings.asterisk + filter + AllStrings.pipe + AllStrings.asterisk + filter;
        }
        return filter;
    }

public static void CreateFileIfDoesntExists(string path)
    {
        CreateFileIfDoesntExists<string, string>(path, null);
    }
public static void CreateFileIfDoesntExists<StorageFolder, StorageFile>(StorageFile path, AbstractCatalog<StorageFolder, StorageFile> ac)
    {
        if (!FS.ExistsFile<StorageFolder, StorageFile>(path, ac))
        {
            TF.WriteAllBytes<StorageFolder, StorageFile>(path, CA.ToList<byte>(), ac);
        }
    }

public static string ReplaceInvalidFileNameChars(string filename)
    {
        StringBuilder sb = new StringBuilder();
        foreach (var item in filename)
        {
            if (!s_invalidFileNameChars.Contains(item))
            {
                sb.Append(item);
            }
        }
        return sb.ToString();
    }
}
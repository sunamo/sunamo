using sunamo.Constants;
using sunamo.Data;
using sunamo.Enums;
using sunamo.Values;
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
    /// </summary>
    /// <param name="selectedFile"></param>
    /// <returns></returns>
    public static bool ExistsFile(string selectedFile, bool falseIfSizeZeroOrEmpty = false)
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
            else if (TF.ReadFile(selectedFile) == string.Empty)
            {
                return false;
            }
        }
        return exists;
    }

    /// <summary>
    /// For empty or whitespace return false.
    /// </summary>
    /// <param name="selectedFile"></param>
    /// <returns></returns>
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
}
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
    /// For empty or whitespace return false.
    /// </summary>
    /// <param name="selectedFile"></param>
    /// <returns></returns>
    public static bool ExistsFile(string selectedFile)
    {
        if (selectedFile == Consts.UncLongPath || selectedFile == string.Empty)
        {
            return false;
        }

        return File.Exists(selectedFile);
    }

    public static DateTime LastModified(string rel)
    {
        var  f = new FileInfo(rel);
        return f.LastWriteTime;

    }
}
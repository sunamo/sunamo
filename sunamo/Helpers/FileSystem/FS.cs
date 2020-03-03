using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using sunamo.Enums;
using sunamo.Data;
using sunamo.Values;
using System.Runtime.CompilerServices;
using sunamo.Helpers;
using sunamo.Essential;
using sunamo.Constants;
using System.Diagnostics;
using sunamo;
using System.Linq;

public partial class FS
{
    /// <summary>
    /// c:\Users\w\AppData\Roaming\sunamo\
    /// </summary>
    /// <param name="item2"></param>
    /// <param name="exts"></param>
    
    public static string ChangeDirectory(string fileName, string changeFolderTo)
    {
        string p = FS.GetDirectoryName(fileName);
        string fn = FS.GetFileName(fileName);

        return FS.Combine(changeFolderTo, fn);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class SevenZip
{
    const string template = "7z.exe a -r \"{0}\" \"{1}{2}\"";

    public static string CreateArchive(string pathOutput, string folder, string masc)
    {
        return SH.Format3(template, pathOutput, folder, masc);
    }

    public static string CreateArchiveInUpFolder(string fullPathFolder, string masc)
    {
        var upfolder = FS.GetDirectoryName(fullPathFolder);
        var zipPath = FS.Combine(upfolder, FS.GetFileName(fullPathFolder) + AllExtensions._7z);

        return CreateArchive(zipPath, fullPathFolder, masc);    
    }
}
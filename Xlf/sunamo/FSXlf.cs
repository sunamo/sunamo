using SunamoExceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

public class FSXlf
{
    #region For easy copy
    public static string GetRelativePath(string relativeTo, string path)
    {
        bool addBs = false;
        if (path[path.Length - 1] == AllChars.bs)
        {
            addBs = true;
            path = path.Substring(0, path.Length - 1);
        }

        String pathSep = "\\";
        String fromPath = Path.GetFullPath(path);
        String baseDir = Path.GetFullPath(relativeTo);            // If folder contains upper folder references, they gets lost here. "c:\test\..\test2" => "c:\test2"

        String[] p1 = Regex.Split(fromPath, "[\\\\/]").Where(x => x.Length != 0).ToArray();
        String[] p2 = Regex.Split(baseDir, "[\\\\/]").Where(x => x.Length != 0).ToArray();
        int i = 0;

        for (; i < p1.Length && i < p2.Length; i++)
            if (String.Compare(p1[i], p2[i], true) != 0)    // Case insensitive match
                break;

        if (i == 0)     // Cannot make relative path, for example if resides on different drive
            return fromPath;

        String r = String.Join(pathSep, Enumerable.Repeat("..", p2.Length - i).Concat(p1.Skip(i).Take(p1.Length - i)));

        if (addBs)
        {
            r += AllStrings.bs;
        }

        return r;
    }
    #endregion
}
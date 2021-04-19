using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class TodoPaths
{
    #region Todo
    public const string OneDrive = @"D:\OneDrive\Drive\Todo\";
    public const string OneDriveText = @"D:\OneDrive\Text\Todo\";
    public const string GoogleDriveArchive = @"D:\Sync\_Archive\Todo\";
    public const string GoogleDrive = @"D:\Sync\Todo\";
    #endregion

    public static bool ExistsFolder(string oneDrive, string name)
    {
        var folders = FS.GetFolders(oneDrive, name + "*");
        return folders.Count > 0;
    }

    public static string FileIn(string oneDrive, string name, string file)
    {
        var folders = FS.GetFolders(oneDrive, name + " " + "*");
        if (folders.Count >0)
        {
            return FS.Combine(folders.First(), file);
        }
        return string.Empty;
    }
}
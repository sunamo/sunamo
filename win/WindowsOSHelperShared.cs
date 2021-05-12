using System.IO;
using System.Linq;

public partial class WindowsOSHelper{ 
public static string FileIn(UserFoldersWin local, string appName, string exe)
    {
        var folder = FS.Combine(PathOfAppDataFolder(local), appName);
        return FileIn( folder, exe);
    }

    public static string FileIn(string folder, string exe)
    {
        if (FS.ExistsDirectory(folder))
        {
            var masc = string.Empty; //FS.MascFromExtension(exe);
            masc = exe;

            return FS.GetFiles(folder, masc, SearchOption.AllDirectories).FirstOrDefault();
        }
        return string.Empty;
    }

    /// <summary>
    /// All
    /// </summary>
    /// <param name = "af"></param>
    public static string PathOfAppDataFolder(UserFoldersWin af)
    {
        var user = ActualWindowsUserName();
        var result = FS.Combine(@"c:\Users\" + user, "AppData", af.ToString());
        return result;
    }

public static string ActualWindowsUserName()
    {
        // return ed\w
        var un = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
        return SH.TextAfter(un, @"\");
    }
}
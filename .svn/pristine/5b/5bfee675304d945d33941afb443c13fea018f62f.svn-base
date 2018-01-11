using sunamo;
using System.IO;
using System.Web.Hosting;

public static class TempHelper
{
    public static string Get(string relativePath)
    {
        string f = HostingEnvironment.ApplicationPhysicalPath + relativePath;
        FS.CreateUpfoldersPsysicallyUnlessThere(f);
        return File.ReadAllText(f);
    }

    public static string Get(int idUser, MySitesShort mss, string file)
    {
        return Get(GetRelativePath(idUser, mss, file));
    }

    public static void Set(int idUser, MySitesShort mss, string file, string value)
    {
         Set( GetRelativePath(idUser, mss, file), value);
    }

    public static void Set(string relativePath, string value)
    {
        string f = HostingEnvironment.ApplicationPhysicalPath + relativePath;
        FS.CreateUpfoldersPsysicallyUnlessThere(f);
        File.WriteAllText(f, value);
    }

    public static string GetRelativePath(int idUser, MySitesShort mss, string file)
    {
        return "_\\temp\\U" + idUser + "\\" + mss + "\\" + file;
    }
}

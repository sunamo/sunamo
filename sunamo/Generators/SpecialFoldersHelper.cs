using sunamo;
using System;
using System.IO;
public static class SpecialFoldersHelper
{
    /// <summary>
    /// Return root folder of AppData (as C:\Users\n\AppData\)
    /// </summary>
    /// <returns></returns>
    public static string ApplicationData()
    {
        return sunamo.FS.GetDirectoryName(AppDataRoaming());
    }

    public static string AppDataRoaming()
    {
        return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
    }
}

using sunamo;
using System;
using System.IO;
public static partial class SpecialFoldersHelper
{
    /// <summary>
    /// Return root folder of AppData (as c:\Users\n\AppData\)
    /// </summary>
    public static string ApplicationData()
    {
        return FS.GetDirectoryName(AppDataRoaming());
    }
}
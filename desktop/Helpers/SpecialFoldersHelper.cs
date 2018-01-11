using sunamo;
using System;
using System.IO;
public static class SpecialFoldersHelper
{
    public static string ApplicationData()
    {
        return sunamo.FS.GetDirectoryName( Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
    }
}

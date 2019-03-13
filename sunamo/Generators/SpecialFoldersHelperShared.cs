using sunamo;
using System;
using System.IO;

public static partial class SpecialFoldersHelper{ 
public static string AppDataRoaming()
    {
        return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
    }
}
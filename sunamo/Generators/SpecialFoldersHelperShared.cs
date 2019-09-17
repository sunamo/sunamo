using sunamo;
using System;
using System.IO;

public static partial class SpecialFoldersHelper
{
    public static string AppDataRoaming()
    {
        string vr = null;
#if ASPNET
        // Create junction to Administrator
        vr = @"c:\Users\Administrator\AppData\Roaming";
#else
        vr = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
#endif
        return vr;
    }
}
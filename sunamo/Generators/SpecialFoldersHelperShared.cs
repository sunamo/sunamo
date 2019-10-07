using sunamo;
using System;
using System.IO;

public static partial class SpecialFoldersHelper
{
    public static string AppDataRoaming()
    {
        string vr = null;
        bool aspnet = false;
#if ASPNET
        aspnet = true;
        // Create junction to Administrator
        vr = @"c:\Users\Administrator\AppData\Roaming";
#else
        vr = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
#endif
        //throw new Exception(aspnet + " "+ SH.NullToStringOrDefault(vr));

        return vr;
    }
}
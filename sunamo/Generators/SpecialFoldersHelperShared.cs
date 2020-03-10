using sunamo;
using sunamo.Essential;
using System;
using System.IO;

public static partial class SpecialFoldersHelper
{
    public static bool aspnet = false;

    public static string AppDataRoaming()
    {
        string vr = null;

        if (Exc.aspnet || VpsHelperSunamo.IsVps)
        {
            // Create junction to Administrator
            vr = @"c:\Users\Administrator\AppData\Roaming";
        }
        else
        {
            // Vracelo mi to empty string  s Environment.GetFolderPath
            //vr = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            vr = @"c:\Users\n\AppData\Roaming";
        }

        return vr;
    }
}
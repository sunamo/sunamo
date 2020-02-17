using sunamo;
using sunamo.Essential;
using System;
using System.IO;

public static partial class SpecialFoldersHelper
{
    public static string AppDataRoaming(bool isVps = false)
    {
        string vr = null;
        

        if (ThisApp.aspnet || isVps)
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
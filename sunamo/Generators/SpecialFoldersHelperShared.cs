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
            vr = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        }
        

        return vr;
    }
}
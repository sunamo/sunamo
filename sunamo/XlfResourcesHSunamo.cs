using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sunamo.Constants;

public class XlfResourcesHSunamo
{
    public static void SaveResouresToRLSunamo()
    {
        
        SaveResouresToRLSunamo(null, isVps);
    }

    /// <summary>
    /// 1. Entry method 
    /// Only for non-UWP apps
    /// </summary>
    public static string SaveResouresToRLSunamo(string key)
    {
        return XlfResourcesH.SaveResouresToRL<string, string>(key, DefaultPaths.sunamoProject, new ExistsDirectory(FS.ExistsDirectoryNull), AppData.ci, isVps);
    }

    static XlfResourcesHSunamo()
    {
        TranslateDictionary.ReloadIfKeyWontBeFound = SaveResouresToRLSunamo;
        TranslateDictionary.isVps = VpsHelperSunamo.IsVpsMethod;
    }
}
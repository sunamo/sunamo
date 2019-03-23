using desktop;
using sunamo;
using System;
using System.IO;
public class ApplicationData
{
    static ApplicationData()
    {
        RoamingSettings = new ApplicationDataContainer(AppData.ci.GetFile(AppFolders.Roaming, "data.dat"));
        //FS.Combine(SpecialFoldersHelper.ApplicationData(), "Local", Consts.@sunamo, ThisApp.Name, "data.dat")
        LocalSettings = new ApplicationDataContainer(AppData.ci.GetFile(AppFolders.Local, "data.dat"));
    }

    public static ApplicationDataContainer RoamingSettings = null;
    public static ApplicationDataContainer LocalSettings = null;
}

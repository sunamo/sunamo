using desktop;
using sunamo;
using System;
using System.IO;
public  class ApplicationData
{
    static ApplicationData()
    {
        RoamingSettings = new ApplicationDataContainer(AppData.GetFile(AppFolders.Roaming, "data.dat"));
        //Path.Combine(SpecialFoldersHelper.ApplicationData(), "Local", Consts.@sunamo, ThisApp.Name, "data.dat")
        LocalSettings = new ApplicationDataContainer(AppData.GetFile(AppFolders.Local, "data.dat"));
    }

    public static ApplicationDataContainer RoamingSettings = null;
    public static ApplicationDataContainer LocalSettings = null;
}

using sunamo;
using sunamo.Data;
using sunamo.Essential;
using sunamo.Values;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

public abstract partial class AppDataBase<StorageFolder, StorageFile> 
{
    string fileFolderWithAppsFiles = "";
    public const string folderWithAppsFiles = "folderWithAppsFiles.txt";

    /// <summary>
    /// After startup will setted up in AppData/Roaming
    /// Then from fileFolderWithAppsFiles App can load alternative path - 
    /// For all apps will be valid either AppData/Roaming or alternative path
    /// </summary>
    protected StorageFolder rootFolder = default(StorageFolder);

    public dynamic Abstract
    {
        get
        {
            if (this is AppDataAbstractBase<StorageFolder, StorageFile>)
            {
                return (AppDataAbstractBase<StorageFolder, StorageFile>)this;
            }
            else if (this is AppDataAppsAbstractBase<StorageFolder, StorageFile>)
            {
                return (AppDataAppsAbstractBase<StorageFolder, StorageFile>)this;
            }
            else
            {
                return null;
            }
        }
    }

    /// <summary>
    /// Tato cesta je již s ThisApp.Name
    /// Set používej s rozvahou a vždy se ujisti zda nenastavuješ na SE(null moc nevadí, v takovém případě RootFolder bude vracet složku v dokumentech)
    /// </summary>
    public StorageFolder RootFolder
    {
        get
        {
            if (Abstract.IsRootFolderNull())
            {
                throw new Exception("Složka ke souborům aplikace nebyla zadána.");
            }

            return rootFolder;
        }

        set
        {
                rootFolder = value;
        }
    }

    

    public string RootFolderCommon(bool inFolderCommon)
    {
        //string appDataFolder = SpecialFO
        string sunamo2 = FS.Combine(SpecialFoldersHelper.AppDataRoaming(), Consts.@sunamo);
        if (inFolderCommon)
        {
            return FS.Combine(sunamo2, "Common");
        }

        return sunamo2;
    }

   

    


public string GetFolderWithAppsFiles()
    {
        //Common(true)
        string slozka = FS.Combine(RootFolderCommon(true), AppFolders.Settings.ToString());
        fileFolderWithAppsFiles = FS.Combine(slozka, folderWithAppsFiles);
        FS.CreateUpfoldersPsysicallyUnlessThere(fileFolderWithAppsFiles);
        return fileFolderWithAppsFiles;
    }
}
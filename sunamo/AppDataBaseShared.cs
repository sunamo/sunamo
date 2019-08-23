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
    private string _fileFolderWithAppsFiles = "";
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
            bool isNull = Abstract.IsRootFolderNull();
            if (isNull)
            {
                throw new Exception("Slo\u017Eka ke soubor\u016Fm aplikace nebyla zad\u00E1na" + ".");
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
        _fileFolderWithAppsFiles = FS.Combine(slozka, folderWithAppsFiles);
        FS.CreateUpfoldersPsysicallyUnlessThere(_fileFolderWithAppsFiles);
        return _fileFolderWithAppsFiles;
    }

public async Task CreateAppFoldersIfDontExists()
    {
        if (!string.IsNullOrEmpty(ThisApp.Name))
        {
            RootFolder = Abstract.GetRootFolder();

            foreach (AppFolders item in Enum.GetValues(typeof(AppFolders)))
            {
                //FS.CreateFoldersPsysicallyUnlessThere(GetFolder(item));
                FS.CreateDirectory(Abstract.GetFolder(item));
            }
        }
        else
        {
            throw new Exception("Nen\u00ED vypln\u011Bno n\u00E1zev aplikace" + ".");
        }
    }
}
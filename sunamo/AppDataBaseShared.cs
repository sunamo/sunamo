﻿using sunamo;
using sunamo.Data;
using sunamo.Essential;
using sunamo.Values;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

public abstract partial class AppDataBase<StorageFolder, StorageFile>
{
    public abstract StorageFile GetFileInSubfolder(AppFolders output, string subfolder, string file, string ext);
    public abstract StorageFolder GetFolder(AppFolders af);
    /// <summary>
    /// Must return always string, not StorageFile - in Standard is not StorageFile class and its impossible to get Path
    /// </summary>
    /// <param name="key"></param>
    
    public abstract StorageFolder GetSunamoFolder();

    //public  string CommonFolder()
    //{
    //    var path = GetSunamoFolder().Result.ToString();
    //    return FS.Combine(path, "Common", AppFolders.Settings.ToString());
    //}
    //public abstract StorageFolder CommonFolder();

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
                throw new Exception("Slo\u017Eka ke soubor\u016Fm aplikace nebyla zad\u00E1na" + " (Look direct into IsRootFolderNull()).");
            }

            return rootFolder;
        }

        set
        {
            rootFolder = value;
        }
    }

    public abstract string RootFolderCommon(bool inFolderCommon);

    






    public string GetFolderWithAppsFiles()
    {
        //Common(true)
        string slozka = FS.Combine(SpecialFoldersHelper.AppDataRoaming(), "sunamo\\Common", AppFolders.Settings.ToString());
        _fileFolderWithAppsFiles = FS.Combine(slozka, folderWithAppsFiles);

        

        FS.CreateUpfoldersPsysicallyUnlessThere(_fileFolderWithAppsFiles);

        

        return _fileFolderWithAppsFiles;
    }

public void CreateAppFoldersIfDontExists()
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
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
    

    /// <summary>
    /// After startup will setted up in AppData/Roaming
    /// Then from fileFolderWithAppsFiles App can load alternative path - 
    /// For all apps will be valid either AppData/Roaming or alternative path
    /// </summary>
    protected StorageFolder rootFolder = default(StorageFolder);

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

    /// <summary>
    /// If file A1 dont exists or have empty content, then create him with empty content and G SE
    /// </summary>
    /// <param name = "path"></param>
    public string ReadFileOfSettingsOther(string path)
    {
        if (!path.Contains(AllStrings.bs) && !path.Contains(AllStrings.slash))
        {
            path = AppData.ci.GetFile(AppFolders.Settings, path);
        }

        TF.CreateEmptyFileWhenDoesntExists(path);
        return TF.ReadFile(path);
    }

    

}
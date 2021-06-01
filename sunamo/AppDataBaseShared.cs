using sunamo;
using sunamo.Essential;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

public abstract partial class AppDataBase<StorageFolder, StorageFile>: IAppDataBase<StorageFolder, StorageFile>
{
    public abstract StorageFile GetFileInSubfolder(AppFolders output, string subfolder, string file, string ext);
    public abstract StorageFolder GetFolder(AppFolders af);
    /// <summary>
    /// Must return always string, not StorageFile - in Standard is not StorageFile class and its impossible to get Path
    /// </summary>
    /// <param name="key"></param>
    public abstract string GetFileCommonSettings(string key);
    private string _fileFolderWithAppsFiles = "";
    public const string folderWithAppsFiles = "folderWithAppsFiles.txt";

    /// <summary>
    /// After startup will setted up in AppData/Roaming
    /// Then from fileFolderWithAppsFiles App can load alternative path - 
    /// For all apps will be valid either AppData/Roaming or alternative path
    /// </summary>
    protected StorageFolder rootFolder = default(StorageFolder);

    /// <summary>
    /// Must be here and Tash because in UWP is everything async
    /// 
    /// </summary>
    public abstract StorageFolder GetSunamoFolder();

    //public  string CommonFolder()
    //{
    //    var path = GetSunamoFolder().Result.ToString();
    //    return FS.Combine(path, SunamoPageHelperSunamo.i18n(XlfKeys.Common), AppFolders.Settings.ToString());
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

    static Type type = typeof(AppDataBase<StorageFolder, StorageFile>);

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
                ThrowExceptions.Custom(Exc.GetStackTrace(), type, Exc.CallingMethod(),"Slo\u017Eka ke soubor\u016Fm aplikace nebyla zad\u00E1na "+SunamoPageHelperSunamo.i18n(XlfKeys.LookDirectIntoIsRootFolderNull)+".");
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

    public string basePath = null;

    public void CreateAppFoldersIfDontExists(string basePath = null)
    {
        if (Exc.aspnet)
        {
            this.basePath = basePath;
            RootFolder = (StorageFolder)(dynamic)basePath;
            foreach (AppFolders item in Enum.GetValues(typeof(AppFolders)))
            {
                //FS.CreateFoldersPsysicallyUnlessThere(GetFolder(item));
                var p = FS.Combine(basePath, item.ToString());
                FS.CreateDirectory(p);
            }
        }
        else
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
                ThrowExceptions.Custom(Exc.GetStackTrace(), type, Exc.CallingMethod(), "Nen\u00ED vypln\u011Bno n\u00E1zev aplikace.");
            }
        }
    }
}
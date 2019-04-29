using sunamo;
using sunamo.Data;
using sunamo.Essential;
using sunamo.Values;
using System;
using System.IO;
using System.Threading.Tasks;
/// <summary>
/// 
/// </summary>
public abstract partial class AppDataBase<StorageFolder, StorageFile>
{
    public const string folderWithAppsFiles = "folderWithAppsFiles.txt";

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

    

    string fileFolderWithAppsFiles = "";

    public AppDataBase()
    {
    }



    public string GetFolderWithAppsFiles()
    {
        //Common(true)
        string slozka = FS.Combine(RootFolderCommon(true), AppFolders.Settings.ToString());
        fileFolderWithAppsFiles = FS.Combine(slozka, folderWithAppsFiles);
        FS.CreateUpfoldersPsysicallyUnlessThere(fileFolderWithAppsFiles);
        return fileFolderWithAppsFiles;
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
            throw new Exception("Není vyplněno název aplikace.");
        }
    }

    /// <summary>
    /// If file A1 dont exists, then create him with empty content and G SE. When optained file/folder doesnt exists, return SE
    /// </summary>
    /// <param name = "path"></param>
    public string ReadFileOfSettingsExistingDirectoryOrFile(string path)
    {

        if (!path.Contains(AllStrings.bs) && !path.Contains(AllStrings.slash))
        {
            path = AppData.ci.GetFile(AppFolders.Settings, path);
        }

        TF.CreateEmptyFileWhenDoesntExists(path);
        string content = TF.ReadFile(path);
        if (FS.ExistsFile(content) || FS.ExistsDirectory(content))
        {
            return content;
        }

        return "";
    }

    /// <summary>
    /// If file A1 dont exists, then create him with empty content and G SE. When optained file/folder doesnt exists, return it anyway
    /// </summary>
    /// <param name = "path"></param>
    /// <returns></returns>
    public string ReadFileOfSettingsDirectoryOrFile(string path)
    {
        return ReadFileOfSettingsOther(path);
    }

    /// <summary>
    /// If file A1 dont exists or have empty content, then create him with empty content and G false
    /// </summary>
    /// <param name = "path"></param>
    public bool ReadFileOfSettingsBool(string path)
    {
        if (!path.Contains(AllStrings.bs) && !path.Contains(AllStrings.slash))
        {
            path = AppData.ci.GetFile(AppFolders.Settings, path);
        }

        TF.CreateEmptyFileWhenDoesntExists(path);
        string content = TF.ReadFile(path);
        bool vr = false;
        if (bool.TryParse(content.Trim(), out vr))
        {
            return vr;
        }

        return false;
    }

    /// <summary>
    /// Save file A1 to folder AF Settings with value A2.
    /// </summary>
    /// <param name = "file"></param>
    /// <param name = "value"></param>
    public void SaveFileOfSettings(string file, string value)
    {
        StorageFile fileToSave = Abstract.GetFile(AppFolders.Settings, file);
        Abstract.SaveFile(value,  fileToSave);
    }

    

    /// <summary>
    /// Save file A2 to AF A1 with contents A3
    /// </summary>
    /// <param name = "af"></param>
    /// <param name = "file"></param>
    /// <param name = "value"></param>
    public StorageFile SaveFile(AppFolders af, string file, string value)
    {
        StorageFile fileToSave = Abstract. GetFile(af, file);
        Abstract.SaveFile(value, fileToSave);
        return fileToSave;
    }

    /// <summary>
    /// Append to file A2 in AF A1 with contents A3
    /// </summary>
    /// <param name = "af"></param>
    /// <param name = "file"></param>
    /// <param name = "value"></param>
    public void AppendToFile(AppFolders af, string file, string value)
    {
        var fileToSave = Abstract.GetFile(af, file);
        AppendToFile(value, fileToSave);
    }

    /// <summary>
    /// Just call TF.AppendToFile
    /// </summary>
    /// <param name = "file"></param>
    /// <param name = "value"></param>
    public void AppendToFile(string file, string value)
    {
        TF.AppendToFile(value, file);
    }







}
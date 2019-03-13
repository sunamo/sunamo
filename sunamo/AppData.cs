﻿using sunamo;
using sunamo.Data;
using sunamo.Essential;
using sunamo.Values;
using System;
using System.IO;
using System.Threading.Tasks;
/// <summary>
/// 
/// </summary>
public partial class AppData
{
    
    static AppData()
    {
    //CreateAppFoldersIfDontExists();
    }

    static string fileFolderWithAppsFiles = "";
    //public static void ReloadFilePathsOfSettings()
    //{
    //    fileFolderWithAppsFiles = 
    //}
    public static string GetFolderWithAppsFiles()
    {
        //Common(true)
        string slozka = Path.Combine(RootFolderCommon(true), AppFolders.Settings.ToString());
        fileFolderWithAppsFiles = Path.Combine(slozka, "folderWithAppsFiles.txt");
        FS.CreateUpfoldersPsysicallyUnlessThere(fileFolderWithAppsFiles);
        return fileFolderWithAppsFiles;
    }

    public async static Task CreateAppFoldersIfDontExists()
    {
        if (!string.IsNullOrEmpty(ThisApp.Name))
        {
            string r = AppData.GetFolderWithAppsFiles();
            rootFolder = TF.ReadFile(r);
            if (string.IsNullOrWhiteSpace(rootFolder))
            {
                rootFolder = FS.Combine(SpecialFoldersHelper.AppDataRoaming(), Consts.@sunamo);
            }

            RootFolder = Path.Combine(rootFolder, ThisApp.Name);
            FS.CreateDirectory(RootFolder);
            foreach (AppFolders item in Enum.GetValues(typeof(AppFolders)))
            {
                //FS.CreateFoldersPsysicallyUnlessThere(GetFolder(item));
                FS.CreateDirectory(GetFolder(item));
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
    public static string ReadFileOfSettingsExistingDirectoryOrFile(string path)
    {
        if (!path.Contains("\\") && !path.Contains("/"))
        {
            path = AppData.GetFile(AppFolders.Settings, path);
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
    public static string ReadFileOfSettingsDirectoryOrFile(string path)
    {
        return ReadFileOfSettingsOther(path);
    }

    /// <summary>
    /// If file A1 dont exists or have empty content, then create him with empty content and G false
    /// </summary>
    /// <param name = "path"></param>
    public static bool ReadFileOfSettingsBool(string path)
    {
        if (!path.Contains("\\") && !path.Contains("/"))
        {
            path = AppData.GetFile(AppFolders.Settings, path);
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
    public static void SaveFileOfSettings(string file, string value)
    {
        string fileToSave = GetFile(AppFolders.Settings, file);
        TF.SaveFile(value, fileToSave);
    }

    /// <summary>
    /// Save file A2 to AF A1 with contents A3
    /// </summary>
    /// <param name = "af"></param>
    /// <param name = "file"></param>
    /// <param name = "value"></param>
    public static string SaveFile(AppFolders af, string file, string value)
    {
        string fileToSave = GetFile(af, file);
        TF.SaveFile(value, fileToSave);
        return fileToSave;
    }

    /// <summary>
    /// Just call TF.SaveFile
    /// </summary>
    /// <param name = "file"></param>
    /// <param name = "value"></param>
    public static void SaveFile(string file, string value)
    {
        TF.SaveFile(value, file);
    }

    /// <summary>
    /// Append to file A2 in AF A1 with contents A3
    /// </summary>
    /// <param name = "af"></param>
    /// <param name = "file"></param>
    /// <param name = "value"></param>
    public static void AppendToFile(AppFolders af, string file, string value)
    {
        string fileToSave = GetFile(af, file);
        TF.AppendToFile(value, fileToSave);
    }

    /// <summary>
    /// Just call TF.AppendToFile
    /// </summary>
    /// <param name = "file"></param>
    /// <param name = "value"></param>
    public static void AppendToFile(string file, string value)
    {
        TF.AppendToFile(value, file);
    }

    public static string GetFolder(AppFolders af)
    {
        string vr = Path.Combine(RootFolder, af.ToString());
        return vr;
    }

    public async static Task<StorageFolder> GetFolderAsync(AppFolders af)
    {
        string vr = Path.Combine(RootFolder, af.ToString());
        return new StorageFolder(vr);
    }

    /// <summary>
    /// Pokud rootFolder bude SE nebo null, G false, jinak vrátí zda rootFolder existuej ve FS
    /// </summary>
    /// <returns></returns>
    public static bool IsRootFolderOk()
    {
        if (string.IsNullOrEmpty(rootFolder))
        {
            return false;
        }

        return FS.ExistsDirectory(rootFolder);
    }

    
}
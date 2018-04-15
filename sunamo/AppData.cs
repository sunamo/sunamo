using sunamo;
using sunamo.Essential;
using sunamo.Values;
using System;
using System.IO;
using System.Web;


/// <summary>
/// 
/// </summary>
public class AppData
{
    /// <summary>
    /// After startup will setted up in AppData/Roaming
    /// Then from fileFolderWithAppsFiles App can load alternative path - 
    /// For all apps will be valid either AppData/Roaming or alternative path
    /// </summary>
    static string rootFolder = null;

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
        sunamo.FS.CreateUpfoldersPsysicallyUnlessThere(fileFolderWithAppsFiles);
        return fileFolderWithAppsFiles;
    }

    public static void CreateAppFoldersIfDontExists()
    {
        if (!string.IsNullOrEmpty(ThisApp.Name))
        {
            string r = AppData.GetFolderWithAppsFiles();

            rootFolder = TF.ReadFile(r);

            if (string.IsNullOrWhiteSpace(rootFolder))
            {
                rootFolder = FS.Combine(SpecialFoldersHelper.AppDataRoaming(), Consts.@sunamo);
            }

            RootFolder = Path.Combine(rootFolder,ThisApp.Name);
            sunamo.FS.CreateDirectory(RootFolder);
            foreach (AppFolders item in Enum.GetValues(typeof(AppFolders)))
            {
                //sunamo.FS.CreateFoldersPsysicallyUnlessThere(GetFolder(item));
                sunamo.FS.CreateDirectory(GetFolder(item));
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
    /// <param name="path"></param>
    public static string ReadFileOfSettingsExistingDirectoryOrFile(string path)
    {
        if (!path.Contains("\\") && !path.Contains("/"))
        {
            path = AppData.GetFile(AppFolders.Settings, path);
        }
        TF.CreateEmptyFileWhenDoesntExists(path);
        string content = TF.ReadFile(path);
        if (File.Exists(content) || Directory.Exists(content))
        {
            return content;
        }
        return "";
    }

    /// <summary>
    /// If file A1 dont exists, then create him with empty content and G SE. When optained file/folder doesnt exists, return it anyway
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static string ReadFileOfSettingsDirectoryOrFile(string path)
    {

        return ReadFileOfSettingsOther(path);
    }

    /// <summary>
    /// If file A1 dont exists or have empty content, then create him with empty content and G false
    /// </summary>
    /// <param name="path"></param>
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
    /// If file A1 dont exists or have empty content, then create him with empty content and G SE
    /// </summary>
    /// <param name="path"></param>
    public static string ReadFileOfSettingsOther(string path)
    {
        if (!path.Contains("\\") && !path.Contains("/"))
        {
            path = AppData.GetFile(AppFolders.Settings, path);
        }
        TF.CreateEmptyFileWhenDoesntExists(path);
        return TF.ReadFile(path);
    }

    /// <summary>
    /// Save file A1 to folder AF Settings with value A2.
    /// </summary>
    /// <param name="file"></param>
    /// <param name="value"></param>
    public static void SaveFileOfSettings(string file, string value)
    {
        string fileToSave = GetFile(AppFolders.Settings, file);
        TF.SaveFile(value, fileToSave);
    }

    /// <summary>
    /// Save file A2 to AF A1 with contents A3
    /// </summary>
    /// <param name="af"></param>
    /// <param name="file"></param>
    /// <param name="value"></param>
    public static string SaveFile(AppFolders af, string file, string value)
    {
        string fileToSave = GetFile(af, file);
        TF.SaveFile(value, fileToSave);
        return fileToSave;
    }

    /// <summary>
    /// Just call TF.SaveFile
    /// </summary>
    /// <param name="file"></param>
    /// <param name="value"></param>
    public static void SaveFile(string file, string value)
    {
        TF.SaveFile(value, file);
    }

    /// <summary>
    /// Append to file A2 in AF A1 with contents A3
    /// </summary>
    /// <param name="af"></param>
    /// <param name="file"></param>
    /// <param name="value"></param>
    public static void AppendToFile(AppFolders af, string file, string value)
    {
        string fileToSave = GetFile(af, file);
        TF.AppendToFile(value, fileToSave);
    }

    /// <summary>
    /// Just call TF.AppendToFile
    /// </summary>
    /// <param name="file"></param>
    /// <param name="value"></param>
    public static void AppendToFile(string file, string value)
    {
        TF.AppendToFile(value, file);
    }

    public static string GetFolder(AppFolders af)
    {
        string vr = Path.Combine(RootFolder, af.ToString());
        return vr;
        
        
    }

    /// <summary>
    /// G path file A2 in AF A1.
    /// Automatically create upfolder if there dont exists.
    /// </summary>
    /// <param name="af"></param>
    /// <param name="file"></param>
    /// <returns></returns>
    public static string GetFile(AppFolders af, string file)
    {
            string slozka2 = Path.Combine(RootFolder, af.ToString());
            string soubor = Path.Combine(slozka2, file);
            return soubor;
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
         return Directory.Exists(rootFolder);
     }

    /// <summary>
    /// Tato cesta je již s ThisApp.Name
    /// Set používej s rozvahou a vždy se ujisti zda nenastavuješ na SE(null moc nevadí, v takovém případě RootFolder bude vracet složku v dokumentech)
    /// </summary>
    public static string RootFolder
    {
        get
        {
            if (string.IsNullOrEmpty(rootFolder))
            {
                throw new Exception("Složka ke souborům aplikace nebyla zadána.");
            }
            return rootFolder;
        }
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                //throw new Exception("Cesta ke složce souborům aplikace musí být zadána.");
            }
            else
            {
                rootFolder = value;
            }
            
        }
    }

    public static string RootFolderCommon(bool inFolderCommon)
    {

        //string appDataFolder = SpecialFO
            string sunamo2 = Path.Combine(SpecialFoldersHelper.AppDataRoaming(), Consts.@sunamo);
            if (inFolderCommon)
            {
                return Path.Combine(sunamo2, "Common");
            }
            return sunamo2;
        
    }
}


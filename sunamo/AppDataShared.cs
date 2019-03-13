using sunamo;
using sunamo.Data;
using sunamo.Essential;
using sunamo.Values;
using System;
using System.IO;
using System.Threading.Tasks;

public partial class AppData{
    /// <summary>
    /// After startup will setted up in AppData/Roaming
    /// Then from fileFolderWithAppsFiles App can load alternative path - 
    /// For all apps will be valid either AppData/Roaming or alternative path
    /// </summary>
    static string rootFolder = null;

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

    /// <summary>
    /// If file A1 dont exists or have empty content, then create him with empty content and G SE
    /// </summary>
    /// <param name = "path"></param>
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
    /// G path file A2 in AF A1.
    /// Automatically create upfolder if there dont exists.
    /// </summary>
    /// <param name = "af"></param>
    /// <param name = "file"></param>
    /// <returns></returns>
    public static string GetFile(AppFolders af, string file)
    {
        string slozka2 = Path.Combine(RootFolder, af.ToString());
        string soubor = Path.Combine(slozka2, file);
        return soubor;
    }
}
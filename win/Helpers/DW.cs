
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;
/// <summary>
/// Use WindowsForms. Is name just DW due to filename and automatically add to git add
/// </summary>
public static partial class DW
{
    public static string SelectPathToSaveFileTo(AppFolders af, string filter, bool checkFileExists, string nameWithExt)
    {
        return SelectPathToSaveFileTo(AppData.ci.GetFolder(af), filter, checkFileExists, nameWithExt);
    }

    public static string SelectPathToSaveFileTo(AppFolders af, string filter, bool checkFileExists)
    {
        return SelectPathToSaveFileTo(AppData.ci.GetFolder(af), filter, checkFileExists);
    }

    public static string SelectPathToSaveFileTo(string initialDirectory, string filter, bool checkFileExists)
    {
        return SelectPathToSaveFileTo(initialDirectory, filter, checkFileExists, "");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name = "p"></param>
    /// <param name = "filtr"></param>
    
    public static List<string> SelectOfFiles(string filtr, string initialFolder)
    {
        return SelectOfFiles(filtr, initialFolder, true);
    }


}
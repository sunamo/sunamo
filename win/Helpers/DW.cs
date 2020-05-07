
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
    public static string SelectPathToSaveFileTo(string initialDirectory, string filter, bool checkFileExists, string nameWithExt)
    {
        SaveFileDialog sfd = new SaveFileDialog();
        sfd.CustomPlaces.Add(new FileDialogCustomPlace(FS.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), Consts.@sunamo)));
        sfd.SupportMultiDottedExtensions = true;
        sfd.InitialDirectory = initialDirectory;
        sfd.CheckPathExists = true;
        sfd.CheckFileExists = checkFileExists;
        //sfd.DefaultExt = ".txt";
        sfd.Filter = FS.RepairFilter(filter);
        sfd.ValidateNames = true;
        sfd.FileName = nameWithExt;
        sfd.Title = "Vyberte soubor do ktereho se ulozi";
        if (sfd.ShowDialog() == DialogResult.OK)
        {
            return sfd.FileName;
        }

        return null;
    }

    public static string SelectPathToSaveFileTo(string initialDirectory, string filter)
    {
        return SelectPathToSaveFileTo(initialDirectory, filter, false);
    }

    /// <summary>
    /// As filter will be set AllFiles *.*
    /// </summary>
    /// <param name = "p"></param>
    public static string SelectPathToSaveFileTo(string initialDirectory)
    {
        return SelectPathToSaveFileTo(initialDirectory, filterDefault);
    }

    public static string SelectPathToSaveFileToMustExists(string initialDirectory, string filter)
    {
        return SelectPathToSaveFileTo(initialDirectory, filter, true);
    }


    /// <summary>
    /// ...
    /// </summary>
    /// <param name = "description"></param>
    /// <param name = "masc"></param>
    public static void UpdateDefaultFilter(string description, string masc)
    {
        filterDefault = description + AllStrings.verbar + masc;
    }

    public static string GetFilter(string description, string masc)
    {
        return description + AllStrings.verbar + masc;
    }

    /// <summary>
    /// G null if no folder selected
    /// </summary>
    /// <param name = "rootFolder"></param>
    public static string SelectOfFolder(string rootFolder)
    {
        CommonOpenFileDialog fbd = new CommonOpenFileDialog();
        //FolderBrowserDialog fbd = new FolderBrowserDialog();
        fbd.Title = "Select the folder";
        // Here is available set this only way
        fbd.IsFolderPicker = true;
        fbd.InitialDirectory = rootFolder;
        if (fbd.ShowDialog() == CommonFileDialogResult.Ok)
        {
            return fbd.FileName;
        }

        return null;
    }

    public static string SelectOfFolder(Environment.SpecialFolder rootFolder)
    {
        return SelectOfFolder(Environment.GetFolderPath(rootFolder));
    }


    /// <summary>
    /// As filter is set PP filterDefault, multiselect is enabled.
    /// InitialDirectory is MyDocuments.
    /// </summary>
    public static List<string> SelectOfFiles()
    {
        return SelectOfFiles(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
    }

  

    /// <summary>
    /// Multiselect is enabled.
    /// </summary>
    /// <param name = "filtr"></param>
    /// <param name = "initialFolder"></param>
    public static List<string> SelectOfFiles(string filtr, string initialFolder)
    {
        return SelectOfFiles(filtr, initialFolder, true);
    }


}
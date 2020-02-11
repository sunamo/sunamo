using sunamo.Collections;
using System;
using System.Collections.Generic;
using System.Text;


public class VisualStudioTempFse
{
    //public static string[] foldersInSolutionToDeleteWithWildcard = new string[] { "Backup*" };
    /// <summary>
    /// , "packages" cannot be here - after every cleaning, which is most time very often, must open sunamo sln and compile all
    /// bin je např. in _Docker\DockerFromBeginWebForms,Analyzer1\Analyzer1 \Analyzer1 \Analyzer1.Test \Analyzer1.Vsix
    /// 
    /// </summary>
    public static List<string> foldersInSolutionToDelete = CA.ToListString(".vs", "_UpgradeReport_Files", "obj", "Backup*", "bin", "obj");
    //public static string[] foldersInSolutionToDelete = null;

    //public AllProjectsSearchConsts()
    //{
    //    foldersInSolutionToDelete = foldersInSolutionToDeleteWithWildcard.Concat(foldersInSolutionToDeleteWoWildcard).ToArray();
    //}
    public const string gitFolderName = ".git";
    public const string gitignoreFile = ".gitignore";

    public static List<string> foldersInProjectToDelete = CA.ToListString(".vs", "obj", "bin");
    public static ExtensionSortedCollection filesInSolutionToDelete = new ExtensionSortedCollection("UpgradeLog*.htm", "UpgradeLog*.htm", "*.suo");
    public static ExtensionSortedCollection filesInProjectToDelete = new ExtensionSortedCollection("*.user");
    public static string[] foldersAnywhereToDelete = new string[] { };
    public static ExtensionSortedCollection filesAnywhereToDelete = new ExtensionSortedCollection("Thumbs.db", "*.TMP", "*.tmp");

    public static string[] foldersInSolutionToKeep = new string[] { gitFolderName };
    public static string[] foldersInProjectToKeep = new string[] { "AppPackages", "Assets", "BundleArtifacts", "Fonts", "MultilingualResources", "Properties", "Service References" };
    public static string[] filesInSolutionToKeep = new string[] { };
    public static string[] filesInProjectToKeep = new string[] { "*.suo" };
    public static string[] filesAnywhereToKeep = new string[] { "*_TemporaryKey.pfx", "*.png", "*.jpg", "*.bmp", "*.ico", "*.dll", "README.md", "*.resx", "*.mdf", "*.ldf", "project.json", "project.lock.json", "*.nuget.targets" };

    /// <summary>
    /// save meaning as keep
    /// </summary>
    public static string[] foldersInSolutionDownloaded = new string[] { "packages", gitFolderName, "node_modules" };
    public static string[] foldersInProjectDownloaded = new string[] { };
    public static string[] filesInSolutionDownloaded = new string[] { ".gitattributes", gitignoreFile };
    public static string[] filesInProjectDownloaded = new string[] { };
    public static string[] filesAnywhereDownloaded = new string[] { };
}


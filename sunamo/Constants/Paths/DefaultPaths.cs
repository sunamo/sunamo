using System;
using System.Collections.Generic;
using System.Text;

namespace sunamo.Constants
{
    public class DefaultPaths
    {
        public const string BackupSunamosAppData = @"d:\Sync\Develop of Future\Backups\";

        public const string KeysXlf = @"e:\Documents\Visual Studio 2017\Projects\sunamo\sunamo\Enums\KeysXlf.cs";
        public const string capturedUris = @"C:\Users\Administrator\AppData\Roaming\sunamo\SunamoCzAdmin\Data\SubsSignalR\CapturedUris.txt";
        public const string capturedUris_backup = @"C:\Users\Administrator\AppData\Roaming\sunamo\SunamoCzAdmin\Data\SubsSignalR\CapturedUris_backup.txt";
        public const string DllSunamo = @"e:\Documents\Visual Studio 2017\Projects\sunamo\dll\";
        public const string rootVideos0Kb = @"d:\Documents\Videos0kb\";
        public static string Documents = @"d:\Documents\";
        public static string eDocuments = @"e:\Documents\";
        public static string Docs = @"d:\Docs\";
        public static string Downloads = @"d:\Downloads\";
        public static string Music2 = @"d:\Music2\";
        public static string Backup = @"d:\Documents\Backup\";
        public static string VisualStudio2017 = @"e:\Documents\Visual Studio 2017\";
        public static string Streamline = @"d:\Pictures\Streamline_All_Icons_PNG\PNG Icons\";

        /// <summary>
        /// For all is here sczRootPath
        /// edn with bs
        /// </summary>
        public static string sczPath = FS.Combine(Documents, @"Visual Studio 2017\Projects\sunamo.cz\sunamo.cz\");
        public static string sczOldPath = FS.Combine(Documents, @"Visual Studio 2017\Projects\sunamo.cz\sunamo.cz-old\");
        public static string sczNsnPath = FS.Combine(Documents, @"Visual Studio 2017\Projects\sunamo.cz\sunamo.cz-nsn\");
        /// <summary>
        /// Ended with backslash
        /// </summary>
        public static string sczRootPath = FS.Combine(Documents, @"Visual Studio 2017\Projects\sunamo.cz\");

        public const string ProjectsFolderNameSlash = "Projects\\";
        /// <summary>
        /// Solution, not project
        /// </summary>
        public static string sunamo = @"e:\Documents\Visual Studio 2017\Projects\sunamo\";
        /// <summary>
        /// Cant be used also VpsHelperSunamo.SunamoProject()
        /// </summary>
        public static string sunamoProject = @"e:\Documents\Visual Studio 2017\Projects\sunamo\sunamo\";
        /// <summary>
        /// e:\Documents\Visual Studio 2017\Projects\
        /// </summary>
        public static string vsProjects = @"e:\Documents\Visual Studio 2017\Projects\";
        /// <summary>
        /// e:\Documents\Visual Studio 2017\Projects\
        /// </summary>
        public static string vs = @"e:\Documents\Visual Studio 2017\Projects\";
        public static string vsDocuments = FS.Combine(DefaultPaths.Documents, @"vs\");
        /// <summary>
        /// Use vs for non shortcuted folder
        /// d:\vs17\
        /// </summary>
        public static string vs17 = @"e:\vs17\";
        public static string vs17Documents = FS.Combine(DefaultPaths.Documents, @"vs17\");
        public static string NormalizePathToFolder = FS.Combine(DefaultPaths.Documents, @"Visual Studio 2017\Projects\");
        public static string Test_MoveClassElementIntoSharedFileUC = "d:\\_Test\\AllProjectsSearch\\AllProjectsSearch\\MoveClassElementIntoSharedFileUC\\";

        public static List<string> AllPathsToProjects = CA.ToListString(Test_MoveClassElementIntoSharedFileUC, vs, vsDocuments, vs17 + ProjectsFolderNameSlash, vs17Documents + ProjectsFolderNameSlash, NormalizePathToFolder);

        public const string SyncArchived = @"d:\SyncArchived\";
        public const string SyncArchivedText = @"d:\SyncArchived\Text\";
        public const string SyncArchivedDrive = @"d:\SyncArchived\Drive\";

        public static List<string> All = new List<string> { Documents, Docs, Downloads, Music2 };

        public const string PhotosScz = @"d:\Pictures\photos.sunamo.cz\";
    }
}
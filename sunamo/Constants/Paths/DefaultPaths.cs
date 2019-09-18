using System;
using System.Collections.Generic;
using System.Text;

namespace sunamo.Constants
{
    public class DefaultPaths
    {
        public const string rootVideos0Kb = @"d:\Documents\Videos0kb\";
        public static string Documents = @"d:\Documents\";
        public static string Docs = @"d:\Docs\";
        public static string Downloads = @"d:\Downloads\";
        public static string Music2 = @"d:\Music2\";
        public static string Backup = @"d:\Documents\Backup\";
        /// <summary>
        /// For all is here sczRootPath
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
        public static string sunamo = @"d:\Documents\Visual Studio 2017\Projects\sunamo\";
        public static string sunamoProject = @"d:\Documents\Visual Studio 2017\Projects\sunamo\sunamo\";
        /// <summary>
        /// d:\Documents\Visual Studio 2017\Projects\
        /// </summary>
        public static string vsProjects = @"d:\Documents\Visual Studio 2017\Projects\";
        /// <summary>
        /// d:\Documents\Visual Studio 2017\Projects\
        /// </summary>
        public static string vs = @"d:\Documents\Visual Studio 2017\Projects\";
        public static string vsDocuments = FS.Combine(DefaultPaths.Documents, @"vs\");
        public static string vs17 = @"d:\vs17\";
        public static string vs17Documents = FS.Combine(DefaultPaths.Documents, @"vs17\");
        public static string NormalizePathToFolder = FS.Combine(DefaultPaths.Documents, @"Visual Studio 2017\Projects\");
        public static string Test_MoveClassElementIntoSharedFileUC = "d:\\_Test\\AllProjectsSearch\\AllProjectsSearch\\MoveClassElementIntoSharedFileUC\\";

        public static List<string> AllPathsToProjects = CA.ToListString(Test_MoveClassElementIntoSharedFileUC, vs, vsDocuments, vs17 + ProjectsFolderNameSlash, vs17Documents + ProjectsFolderNameSlash, NormalizePathToFolder);

        public const string SyncArchived = @"d:\SyncArchived\";
        public const string SyncArchivedText = @"d:\SyncArchived\Text\";
        public const string SyncArchivedDrive = @"d:\SyncArchived\Drive\";

        public static string[] All = new string[] { Documents, Docs, Downloads, Music2 };

        public const string PhotosScz = @"d:\Pictures\photos.sunamo.cz\";
    }
}

﻿using System;
using System.Collections.Generic;
using System.Text;

namespace sunamo.Constants
{
    public class DefaultPaths
    {
        public static string Documents = @"D:\Documents\";
        public static string Docs = @"D:\Docs\";
        public static string Downloads = @"D:\Downloads\";
        public static string Music2 = @"D:\Music2\";
        /// <summary>
        /// For all is here sczRootPath
        /// </summary>
        public static string sczPath = FS.Combine(Documents, @"Visual Studio 2017\Projects\sunamo.cz\sunamo.cz\");
        public static string sczOldPath = FS.Combine(Documents, @"Visual Studio 2017\Projects\sunamo.cz\sunamo.cz-old\");
        public static string sczNsnPath = FS.Combine(Documents, @"Visual Studio 2017\Projects\sunamo.cz\sunamo.cz-nsn\");
        public static string sczRootPath = FS.Combine(Documents, @"Visual Studio 2017\Projects\sunamo.cz\");

        public const string ProjectsFolderNameSlash = "Projects\\";
        public static string sunamo = @"d:\Documents\Visual Studio 2017\Projects\sunamo\";

        public static string vs = @"d:\vs\";
        public static string vsDocuments = FS.Combine(DefaultPaths.Documents, @"vs\");
        public static string vs17 = @"d:\vs17\";
        public static string vs17Documents = FS.Combine(DefaultPaths.Documents, @"vs17\");
        public static string NormalizePathToFolder = FS.Combine(DefaultPaths.Documents, @"Visual Studio 2017\Projects\");
        public static string Test_MoveClassElementIntoSharedFileUC = "d:\\_Test\\AllProjectSearch\\MoveClassElementIntoSharedFileUC\\";

        public static List<string> AllPathsToProjects = CA.ToListString(Test_MoveClassElementIntoSharedFileUC, vs, vsDocuments, vs17 + ProjectsFolderNameSlash, vs17Documents + ProjectsFolderNameSlash, NormalizePathToFolder);
        

        public static string[] All = new string[] { Documents, Docs, Downloads, Music2 };
        
        public const string PhotosScz = @"d:\Pictures\photos.sunamo.cz\";
    }
}

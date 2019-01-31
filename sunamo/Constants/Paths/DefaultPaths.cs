using System;
using System.Collections.Generic;
using System.Text;

namespace sunamo.Constants
{
    public class DefaultPaths
    {
        public static string Documents = @"D:\Documents";
        public static string Docs = @"D:\Docs";
        public static string Downloads = @"D:\Downloads";
        public static string Music2 = @"D:\Music2";
        /// <summary>
        /// For all is here sczRootPath
        /// </summary>
        public static string sczPath = FS.Combine(Documents, @"Visual Studio 2017\Projects\sunamo.cz\sunamo.cz\");
        public static string sczOldPath = FS.Combine(Documents, @"Visual Studio 2017\Projects\sunamo.cz\sunamo.cz-old\");
        public static string sczNsnPath = FS.Combine(Documents, @"Visual Studio 2017\Projects\sunamo.cz\sunamo.cz-nsn\");
        public static string sczRootPath = FS.Combine(Documents, @"vs\sunamo.cz");
        public static string vs = FS.Combine(Documents, @"vs");
        //

        public static string[] All = new string[] { Documents, Docs, Downloads, Music2 };
        public static string sunamo = @"d:\vs\sunamo\";
        public const string PhotosScz = @"d:\Pictures\photos.sunamo.cz\";
    }
}

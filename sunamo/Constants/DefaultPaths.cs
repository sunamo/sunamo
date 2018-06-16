using System;
using System.Collections.Generic;
using System.Text;

namespace sunamo.Constants
{
    /// <summary>
    /// Is used in cbDefaultFolders (Not control, generate in code)
    /// </summary>
    public class DefaultPaths
    {
        public static string Music2 = @"D:\Music2";
        public static string VideoFiles0kb = @"D:\Documents\VideoFiles0kb";
        public static string Downloads = @"D:\Downloads";
        public static string Documents = @"D:\Documents";
        public static string Docs = @"D:\Docs";
        public static string _100_PANA = @"d:\Pictures\100_PANA\";


        // TODO: return through reflexion
        public static string[] All = new string[] { VideoFiles0kb, Downloads, Documents, Docs, _100_PANA };
    }
}

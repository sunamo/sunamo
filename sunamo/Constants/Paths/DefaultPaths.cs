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
        public static string sczPath = FS.Combine(Documents, @"Visual Studio 2017\Projects\sunamo.cz\sunamo.cz\");

        //

        public static string[] All = new string[] { Documents, Docs, Downloads, Music2 };

       
    }
}

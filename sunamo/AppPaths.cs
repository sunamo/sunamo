﻿using sunamo;
using sunamo.Essential;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sunamo
{
	/// <summary>
    /// Must be here because is use in SunamoIni and others
    /// </summary>
    public class AppPaths
    {
        public static string GetStartupPath()
        {
            return sunamo.FS.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
        }
        public static string GetFileInStartupPath(string file)
        {
            return System.IO.Path.Combine(GetStartupPath(), file);
            
        }
    }
}
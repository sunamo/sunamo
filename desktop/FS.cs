using sunamo;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace desktop
{
    public class FS
    {
        public static string GetStartupPath()
        {
            return System.FS.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
        }
        public static string GetFileInStartupPath(string file)
        {
            return System.IO.Path.Combine(GetStartupPath(), file);
        }
    }
}

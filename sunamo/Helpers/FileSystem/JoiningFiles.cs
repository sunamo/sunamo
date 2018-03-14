using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace sunamo.Helpers.FileSystem
{
    public class JoiningFiles
    {
        public static string NumberedListWithDot(string folder)
        {
            StringBuilder sb = new StringBuilder();

            var files = FS.GetFiles(@"H:\Desktop\d\Knihy o podnikání, které změní Váš pohled na svět");
            foreach (var item in files)
            {
                var lines = File.ReadAllLines(item);
                string line = lines[0].Trim();
                line = line.Substring(line.IndexOf(' ') + 1).ToLower();
                sb.AppendLine(lines[1] + " - " + SH.FirstCharUpper(line));
            }

            return sb.ToString();
        }
    }
}

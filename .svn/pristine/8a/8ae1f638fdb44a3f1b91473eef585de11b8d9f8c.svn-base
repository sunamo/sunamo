using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sunamo.Data
{
    public class FileInfoLite
    {
        /// <summary>
        /// Plná cesta k souboru
        /// </summary>
        public string Path = null;
        /// <summary>
        /// Název souboru bez cesty s příponou a sériemi
        /// </summary>
        public string Name = null;
        public long Size = 0;

        public static FileInfoLite GetFIL(FileInfo item2)
        {
            FileInfoLite fil = new FileInfoLite();
            fil.Name = item2.Name;
            fil.Path = item2.FullName;
            fil.Size = item2.Length;
            return fil;
        }

        public static FileInfoLite GetFIL(string file)
        {
            FileInfo item2 = new FileInfo(file);
            return GetFIL(item2);
        }
    }
}


using sunamo;
using System;
using System.Collections.Generic;

namespace SunamoFtp
{
    public static class FtpHelper
    {
        /// <summary>
        /// Vrátí zda A1 je .. nebo .
        /// </summary>
        /// <param name="folderName2"></param>
        /// <returns></returns>
        public static bool IsThisOrUp(string folderName2)
        {
            return folderName2 == "." || folderName2 == "..";
        }

        /// <summary>
        /// OK
        /// </summary>
        /// <param name="item2"></param>
        /// <param name="fse"></param>
        /// <param name="fileLenght"></param>
        /// <returns></returns>
        public static bool IsFileOnHosting(string item2, string[] fse, long fileLenght)
        {
            item2 = sunamo.FS.GetFileName(item2);
            foreach (string item in fse)
            {
                long fl = 0;
                string fn = null;
                if (IsFile(item, out fn, out fl) == FileSystemType.File)
                {
                    if (fn == item2)
                    {
                        if (fl == fileLenght)
                        {
                            return true;
                        }
                    }

                }
            }
            return false;
        }

        public static FileSystemType IsFile(string entry)
        {
            string fileName = null;
            string[] tokeny = SH.Split(entry, " ");
            FileSystemType isFile = IsFileShared(entry, tokeny, out fileName);
            return isFile;
        }

        public static FileSystemType IsFile(string entry, out string fileName)
        {
            string[] tokeny = SH.Split(entry, " ");
            FileSystemType isFile = IsFileShared(entry, tokeny, out fileName);
            return isFile;
        }

        public static FileSystemType IsFile(string entry, out string fileName, out long length)
        {
            //drw-rw-rw-   1 user     group           0 Nov 21 18:03 App_Data
            string[] tokeny = SH.Split(entry, " ");
            FileSystemType isFile = IsFileShared(entry, tokeny, out fileName);
            length = long.Parse(tokeny[4]);

            return isFile;
        }

        private static FileSystemType IsFileShared(string entry, string[] tokeny, out string fileName)
        {
            fileName = SH.JoinFromIndex(8, ' ', tokeny);
            FileSystemType isFile = FileSystemType.File;
            char f = entry[0];
            if (f == '-')
            {

            }
            else if (f == 'd')
            {
                if (IsThisOrUp(fileName))
                {
                    isFile = FileSystemType.Link;
                }
                else
                {
                    isFile = FileSystemType.Folder;
                }

            }
            else
            {
                throw new Exception("Nový druh entry");
            }
            return isFile;
        }

        public static bool IsSchemaFtp(string remFileName)
        {
            return remFileName.StartsWith("ftp://");
        }

        public static IEnumerable<string> GetDirectories(string[] fse)
        {
            List<string> vr = new List<string>();
            foreach (var item in fse)
            {
                string fn = null;
                if (IsFile(item, out fn) == FileSystemType.Folder)
                {
                    vr.Add(fn);
                }
            }
            return vr;
        }

        public static string ReplaceSchemaFtp(string remoteHost2)
        {
            return remoteHost2.Replace("ftp://", "");
        }
    }
}

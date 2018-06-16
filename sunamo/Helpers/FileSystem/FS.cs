using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using sunamo.Enums;
using sunamo.Data;
using sunamo.Values;
using System.Runtime.CompilerServices;
using sunamo.Helpers;
using sunamo.Essential;
using sunamo.Constants;

namespace sunamo
{
    public class FS
    {
        static List<char> invalidPathChars = null;
        static Type type = typeof(FS);


        static List<char> invalidFileNameChars = null;
        static List<char> invalidCharsForMapPath = null;
        static List<char> invalidFileNameCharsWithoutDelimiterOfFolders = null;

        public static void RemoveDiacriticInFileContents(string folder, string mask)
        {
            string[] files = Directory.GetFiles(folder, mask, SearchOption.AllDirectories);
            foreach (string item in files)
            {
                string df2 = File.ReadAllText(item, Encoding.Default);

                if (true) //SH.ContainsDiacritic(df2))
                {
                    TF.SaveFile(SH.TextWithoutDiacritic(df2), item);
                    df2 = SH.ReplaceOnce(df2, "ď»ż", "");
                    TF.SaveFile(df2, item);
                }

            }
        }

        /// <summary>
        /// Create folder hiearchy and write
        /// </summary>
        /// <param name="path"></param>
        /// <param name="content"></param>
        public static void WriteAllText(string path, string content)
        {
            FS.CreateUpfoldersPsysicallyUnlessThere(path);
            File.WriteAllText(path, content);
        }

        public static void CopyStream(Stream input, Stream output)
        {
            byte[] buffer = new byte[8 * 1024];
            int len;
            while ((len = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                output.Write(buffer, 0, len);
            }
        }

        internal static IEnumerable<string> GetFiles(string folderPath, bool v)
        {
            return FS.GetFiles(folderPath, "*.*", v ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
        }

        public static List<string> GetFolders(string folder)
        {
            return new List<string>(Directory.GetDirectories(folder));
        }

        public static List<string> GetFolders(string v, string startingWith)
        {
            var folders = GetFolders(v);
            folders = CA.TrimEnd(folders, '\\');
            for (int i = folders.Count - 1; i >= 0; i--)
            {
                if (!Path.GetFileName(folders[i]).Contains(startingWith))
                {
                    folders.RemoveAt(i);
                }
            }

            return folders;
        }

        #region Create to avoid adding System.IO and using without ns colliding
        /// <summary>
        /// Without path
        /// </summary>
        /// <param name="jpgcka"></param>
        /// <returns></returns>
        public static string[] GetFileNamesWoExtension(string[] jpgcka)
        {
            string[] dd = new string[jpgcka.Length];
            for (int i = 0; i < jpgcka.Length; i++)
            {
                dd[i] = Path.GetFileNameWithoutExtension(jpgcka[i]);
            }

            return dd;
        }

        public static string ShrinkLongPath(string actualFilePath)
        {
            // .NET 4.7.1
            // Originally - 265 chars, 254 also too long: d:\Documents\Visual Studio 2017\Projects\Recovered data 03-23 12_11_44\Deep Scan result\Lost Partition1(NTFS)\Other lost files\c# projects - před odstraněním stejných souborů z duplicitních projektů\Visual Studio 2012\Projects\merge-obří temp\temp1\temp\Facebook.cs
            // 4+265 - OK: @"\\?\D:\_NewlyRecovered\Visual Studio 2020\Projects\Visual Studio 2017\Projects\Recovered data 03-23 12_11_44\Deep Scan result\Lost Partition1(NTFS)\Other lost files\c# projects - před odstraněním stejných souborů z duplicitních projektů\Visual Studio 2012\Projects\merge-obří temp\temp1\temp\Facebook.cs"
            // 216 - OK: d:\Recovered data 03-23 12_11_44012345678901234567890123456\Deep Scan result\Lost Partition1(NTFS)\Other lost files\c# projects - před odstraněním stejných souborů z duplicitních projektů\Visual Studio 2012\Projects\merge-obří temp\temp1\temp\
            // for many API is different limits: https://stackoverflow.com/questions/265769/maximum-filename-length-in-ntfs-windows-xp-and-windows-vista
            // 237+11 - bad 



            return Consts.UncLongPath + actualFilePath;
        }

        /// <summary>
        /// ALL EXT. HAVE TO BE ALWAYS LOWER
        /// Return in lowercase
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static string GetExtension(string v)
        {
            string result = "";
            int lastDot = v.LastIndexOf('.');
            if (lastDot == -1)
            {
                return string.Empty;
            }
            result = v.Substring(lastDot).ToLower();

            return result;
        }

        /// <summary>
        /// Pokud by byla cesta zakončená backslashem, vrátila by metoda Path.GetFileName prázdný řetězec. 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string GetFileNameWithoutExtension(string s)
        {
            return Path.GetFileNameWithoutExtension(s.TrimEnd(AllChars.bs));
        }


        #endregion


        #region Methods to avoid adding System.IO - often is colliding with other
        /// <summary>
        /// All occurences Path's method in sunamo replaced
        /// </summary>
        /// <param name="v"></param>
        public static void CreateDirectory(string v)
        {
            Directory.CreateDirectory(v);
        }

        public static void CreateDirectory(string v, DirectoryCreateCollisionOption whenExists, SerieStyle serieStyle)
        {
            if (Directory.Exists(v))
            {
                bool hasSerie;
                string nameWithoutSerie = FS.GetNameWithoutSeries(v, false, out hasSerie, serieStyle);
                if (hasSerie)
                {

                }
                if (whenExists == DirectoryCreateCollisionOption.AddSerie)
                {
                    int serie = 1;
                    while (true)
                    {
                        string newFn = nameWithoutSerie + " (" + serie + ")";
                        if (!Directory.Exists(newFn))
                        {

                            nameWithoutSerie = newFn;
                            break;
                        }
                        serie++;
                    }
                }
                else if (whenExists == DirectoryCreateCollisionOption.Delete)
                {

                }
                else if (whenExists == DirectoryCreateCollisionOption.Overwrite)
                {

                }
                else
                {
                    ThrowExceptions.NotImplementedCase(type, "CreateDirectory");
                }
            }
            else
            {
                Directory.CreateDirectory(v);
            }
        }
        #endregion



        public static string CreateNewFolderPathWithEndingNextTo(string folder, string ending)
        {
            string pathToFolder = FS.GetDirectoryName(folder.TrimEnd(AllChars.bs)) + "\\";
            string folderWithCaretFiles = pathToFolder + Path.GetFileName(folder.TrimEnd(AllChars.bs)) + ending;

            return folderWithCaretFiles;
        }

        public static int DeleteSerieDirectoryOrCreateNew(string repairedBlogPostsFolder)
        {
            int resultSerie = 1;
            string[] folders = Directory.GetDirectories(repairedBlogPostsFolder);

            bool deleted = true;
            // 0 or 1
            if (folders.Length < 2)
            {
                try
                {
                    Directory.Delete(repairedBlogPostsFolder, true);
                }
                catch (Exception)
                {
                    deleted = false;
                }
            }

            string withEndFlash = FS.WithEndSlash(repairedBlogPostsFolder);

            if (!deleted)
            {

                FS.CreateDirectory(withEndFlash + "1\\");
            }
            else
            {
                TextOutputGenerator generator = new TextOutputGenerator();
                generator.sb.Append(withEndFlash);
                generator.sb.CanUndo = true;
                for (; resultSerie < int.MaxValue; resultSerie++)
                {
                    generator.sb.Append(resultSerie);
                    string newDirectory = generator.ToString();
                    if (!Directory.Exists(newDirectory))
                    {
                        Directory.CreateDirectory(newDirectory);
                        break;
                    }
                    generator.Undo();
                }
            }

            return resultSerie;
        }

        static void CopyFilesOfExtensions(string folderFrom, string FolderTo, params string[] extensions)
        {
            folderFrom = FS.WithEndSlash(folderFrom);
            FolderTo = FS.WithEndSlash(FolderTo);

            Dictionary<string, string[]> filesOfExtension = FS.FilesOfExtensions(folderFrom, extensions);

            foreach (var item in filesOfExtension)
            {
                foreach (var path in item.Value)
                {
                    string newPath = path.Replace(folderFrom, FolderTo);
                    FS.CreateUpfoldersPsysicallyUnlessThere(newPath);
                    File.Copy(path, newPath);
                }
            }
        }

        /// <summary>
        /// Kromě jmen také zbavuje diakritiky složky.
        /// </summary>
        /// <param name="folder"></param>
        public static void RemoveDiacriticInFileSystemEntryNames(string folder)
        {
            List<string> folders = new List<string>(Directory.GetDirectories(folder, "*", SearchOption.AllDirectories));
            folders.Reverse();
            foreach (string item in folders)
            {
                string directory = FS.GetDirectoryName(item);
                string filename = Path.GetFileName(item);
                if (SH.ContainsDiacritic(filename))
                {
                    filename = SH.TextWithoutDiacritic(filename);
                    string newpath = Path.Combine(directory, filename);
                    string realnewpath = SH.Copy(newpath).TrimEnd(AllChars.bs);
                    string realnewpathcopy = SH.Copy(realnewpath);
                    int i = 0;
                    while (Directory.Exists(realnewpath))
                    {
                        realnewpath = realnewpathcopy + i.ToString();
                        i++;
                    }
                    Directory.Move(item, realnewpath);
                }
            }
            string[] files = Directory.GetFiles(folder, "*", SearchOption.AllDirectories);
            foreach (string item in files)
            {
                string directory = FS.GetDirectoryName(item);
                string filename = Path.GetFileName(item);
                if (SH.ContainsDiacritic(filename))
                {
                    filename = SH.TextWithoutDiacritic(filename);
                    string newpath = null;
                    try
                    {
                        newpath = Path.Combine(directory, filename);
                    }
                    catch (Exception)
                    {
                        File.Delete(item);
                        continue;
                    }

                    string realNewPath = SH.Copy(newpath);
                    int vlozeno = 0;
                    while (File.Exists(realNewPath))
                    {
                        realNewPath = sunamo.FS.InsertBetweenFileNameAndExtension(newpath, vlozeno.ToString());
                        vlozeno++;
                    }
                    File.Move(item, realNewPath);
                }
            }

        }

        public static string GetFilesSize(List<string> winrarFiles)
        {
            long size = 0;
            foreach (var item in winrarFiles)
            {
                size += FS.GetFileSize(item);
            }
            return GetSizeInAutoString(size);
        }

        private static string GetSizeInAutoString(double size)
        {
            ComputerSizeUnits unit = ComputerSizeUnits.B;
            if (size < Consts.kB)
            {
                unit = ComputerSizeUnits.KB;
                size /= Consts.kB;
            }
            if (size < Consts.kB)
            {
                unit = ComputerSizeUnits.MB;
                size /= Consts.kB;
            }
            if (size < Consts.kB)
            {
                unit = ComputerSizeUnits.GB;
                size /= Consts.kB;
            }
            if (size < Consts.kB)
            {
                unit = ComputerSizeUnits.TB;
                size /= Consts.kB;
            }

            return GetSizeInAutoString(size, unit);
        }

        public static string GetUpFolderWhichContainsExtension(string path, string fileExt)
        {

            while (FilesOfExtension(path, fileExt).Length == 0)
            {
                if (path.Length < 4)
                {
                    return null;
                }
                path = FS.GetDirectoryName(path);
            }
            return path;
        }

        /// <summary>
        /// Non recursive
        /// </summary>
        /// <param name="folder"></param>
        /// <param name="fileExt"></param>
        /// <returns></returns>
        public static string[] FilesOfExtension(string folder, string fileExt)
        {
            return Directory.GetFiles(folder, "*." + fileExt, SearchOption.TopDirectoryOnly);
        }

        public static void TrimContentInFilesOfFolder(string slozka, string searchPattern, SearchOption searchOption)
        {
            string[] files = Directory.GetFiles(slozka, searchPattern, searchOption);
            foreach (var item in files)
            {
                FileStream fs = new FileStream(item, FileMode.Open);
                StreamReader sr = new StreamReader(fs, true);
                string content = sr.ReadToEnd();
                Encoding enc = sr.CurrentEncoding;
                //sr.Close();
                sr.Dispose();
                sr = null;
                string contentTrim = content.Trim();
                File.WriteAllText(item, contentTrim, enc);
                //}
            }
        }

        /// <summary>
        /// Náhrada za metodu ReplaceFileName se stejnými parametry
        /// </summary>
        /// <param name="oldPath"></param>
        /// <param name="what"></param>
        /// <param name="forWhat"></param>
        /// <returns></returns>
        public static string ReplaceInFileName(string oldPath, string what, string forWhat)
        {
            string p2, fn;
            GetPathAndFileName(oldPath, out p2, out fn);
            string vr = p2 + "\\" + fn.Replace(what, forWhat);
            return vr;
        }

        public static byte[] StreamToArrayBytes(System.IO.Stream stream)
        {
            long originalPosition = 0;

            if (stream.CanSeek)
            {
                originalPosition = stream.Position;
                stream.Position = 0;
            }

            try
            {
                byte[] readBuffer = new byte[4096];

                int totalBytesRead = 0;
                int bytesRead;

                while ((bytesRead = stream.Read(readBuffer, totalBytesRead, readBuffer.Length - totalBytesRead)) > 0)
                {
                    totalBytesRead += bytesRead;

                    if (totalBytesRead == readBuffer.Length)
                    {
                        int nextByte = stream.ReadByte();
                        if (nextByte != -1)
                        {
                            byte[] temp = new byte[readBuffer.Length * 2];
                            Buffer.BlockCopy(readBuffer, 0, temp, 0, readBuffer.Length);
                            Buffer.SetByte(temp, totalBytesRead, (byte)nextByte);
                            readBuffer = temp;
                            totalBytesRead++;
                        }
                    }
                }

                byte[] buffer = readBuffer;
                if (readBuffer.Length != totalBytesRead)
                {
                    buffer = new byte[totalBytesRead];
                    Buffer.BlockCopy(readBuffer, 0, buffer, 0, totalBytesRead);
                }
                return buffer;
            }
            finally
            {
                if (stream.CanSeek)
                {
                    stream.Position = originalPosition;
                }
            }
        }

        public static long GetSizeIn(long value, ComputerSizeUnits b, ComputerSizeUnits to)
        {
            if (b == to)
            {
                return value;
            }

            bool toLarger = ((byte)b) < ((byte)to);

            if (toLarger)
            {
                value = ConvertToSmallerComputerUnitSize(value, b, ComputerSizeUnits.B);
                if (to == ComputerSizeUnits.Auto)
                {
                    throw new Exception("Byl specifikován výstupní ComputerSizeUnit, nemůžu toto nastavení změnit");
                }
                else if (to == ComputerSizeUnits.KB && b != ComputerSizeUnits.KB)
                {
                    value /= 1024;
                }
                else if (to == ComputerSizeUnits.MB && b != ComputerSizeUnits.MB)
                {
                    value /= 1024 * 1024;
                }
                else if (to == ComputerSizeUnits.GB && b != ComputerSizeUnits.GB)
                {
                    value /= 1024 * 1024 * 1024;
                }
                else if (to == ComputerSizeUnits.TB && b != ComputerSizeUnits.TB)
                {
                    value /= (1024L * 1024L * 1024L * 1024L);
                }
            }
            else
            {
                value = ConvertToSmallerComputerUnitSize(value, b, to);
            }
            return value;
        }

        private static long ConvertToSmallerComputerUnitSize(long value, ComputerSizeUnits b, ComputerSizeUnits to)
        {
            return ConvertToSmallerComputerUnitSize(value, b, to);
        }

        private static double ConvertToSmallerComputerUnitSize(double value, ComputerSizeUnits b, ComputerSizeUnits to)
        {
            if (to == ComputerSizeUnits.Auto)
            {
                throw new Exception("Byl specifikován výstupní ComputerSizeUnit, nemůžu toto nastavení změnit");
            }
            else if (to == ComputerSizeUnits.KB && b != ComputerSizeUnits.KB)
            {
                value *= 1024;
            }
            else if (to == ComputerSizeUnits.MB && b != ComputerSizeUnits.MB)
            {
                value *= 1024 * 1024;
            }
            else if (to == ComputerSizeUnits.GB && b != ComputerSizeUnits.GB)
            {
                value *= 1024 * 1024 * 1024;
            }
            else if (to == ComputerSizeUnits.TB && b != ComputerSizeUnits.TB)
            {
                value *= (1024L * 1024L * 1024L * 1024L);
            }

            return value;
        }

        public static string GetSizeInAutoString(long value, ComputerSizeUnits b)
        {
            return GetSizeInAutoString(value, b);
        }

        public static string GetSizeInAutoString(double value, ComputerSizeUnits b)
        {
            if (b != ComputerSizeUnits.B)
            {
                // Získám hodnotu v bytech
                value = ConvertToSmallerComputerUnitSize(value, b, ComputerSizeUnits.B);
            }


            if (value < 1024)
            {
                return value + " B";
            }

            double previous = value;
            value /= 1024;

            if (value < 1)
            {
                return previous + " B";
            }

            previous = value;
            value /= 1024;

            if (value < 1)
            {
                return previous + " KB";
            }

            previous = value;
            value /= 1024;
            if (value < 1)
            {
                return previous + " MB";
            }

            previous = value;
            value /= 1024;

            if (value < 1)
            {
                return previous + " GB";
            }

            return value + " TB";

        }

        /// <summary>
        /// Zjistí všechny složky rekurzivně z A1 a prvně maže samozřejmě ty, které mají více tokenů
        /// </summary>
        /// <param name="v"></param>
        public static void DeleteAllEmptyDirectories(string v)
        {
            List<ItemWithCount<string>> dirs = sunamo.FS.DirectoriesWithToken(v, AscDesc.Desc);

            foreach (var item in dirs)
            {
                if (sunamo.FS.IsDirectoryEmpty(item.t, true, true))
                {
                    sunamo.FS.TryDeleteDirectory(item.t);
                }

            }
        }

        public static List<ItemWithCount<string>> DirectoriesWithToken(string v, AscDesc sb)
        {
            string[] dirs = Directory.GetDirectories(v, "*", SearchOption.AllDirectories);
            List<ItemWithCount<string>> vr = new List<ItemWithCount<string>>();
            foreach (var item in dirs)
            {
                vr.Add(new ItemWithCount<string> { t = item, count = SH.OccurencesOfStringIn(item, "\\") });
            }
            if (sb == AscDesc.Asc)
            {
                vr.Sort(new SunamoComparerICompare.ItemWithCountComparer.Asc<string>(new SunamoComparer.ItemWithCountSunamoComparer<string>()));
            }
            else if (sb == AscDesc.Desc)
            {
                vr.Sort(new SunamoComparerICompare.ItemWithCountComparer.Desc<string>(new SunamoComparer.ItemWithCountSunamoComparer<string>()));
            }

            return vr;
        }

        /// <summary>
        /// No recursive, all extension
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static List<string> GetFiles(string path)
        {
            return FS.GetFiles(path, "*", SearchOption.TopDirectoryOnly);
        }

        /// <summary>
        /// A1 have to be with ending backslash
        /// </summary>
        /// <param name="folder"></param>
        /// <param name="mask"></param>
        /// <param name="searchOption"></param>
        /// <returns></returns>
        public static List<string> GetFiles(string folder, string mask, SearchOption searchOption, bool trimA1 = false)
        {
            var list = new List<string>(Directory.GetFiles(folder, mask, searchOption));
            if (trimA1)
            {
                list = CA.ChangeContent(list, d => d = d.Replace(folder, ""));
            }
            return list;
        }

        /// <summary>
        /// A1 i A2 musí končit backslashem
        /// Může vyhodit výjimku takže je nutné to odchytávat ve volající metodě
        /// If destination folder exists, source folder without files keep
        /// Return message if success, or null
        /// A5 false
        /// </summary>
        /// <param name="p"></param>
        /// <param name="to"></param>
        /// <param name="co"></param>
        public static string MoveDirectoryNoRecursive(string item, string nova, DirectoryMoveCollisionOption co, FileMoveCollisionOption co2)
        {
            string vr = null;
            if (Directory.Exists(nova))
            {
                if (co == DirectoryMoveCollisionOption.AddSerie)
                {
                    int serie = 1;
                    while (true)
                    {
                        string newFn = nova + " (" + serie + ")";
                        if (!Directory.Exists(newFn))
                        {
                            vr = "Folder has been renamed to " + Path.GetFileName(newFn);
                            nova = newFn;
                            break;
                        }
                        serie++;
                    }
                }
                else if (co == DirectoryMoveCollisionOption.DiscardFrom)
                {
                    Directory.Delete(item, true);
                    return vr;
                }
                else if (co == DirectoryMoveCollisionOption.Overwrite)
                {
                }
            }

            string[] files = Directory.GetFiles(item, "*", SearchOption.TopDirectoryOnly);
            sunamo.FS.CreateFoldersPsysicallyUnlessThere(nova);
            foreach (var item2 in files)
            {

                string fileTo = nova + item2.Substring(item.Length);
                MoveFile(item2, fileTo, co2);
            }

            try
            {
                Directory.Move(item, nova);
            }
            catch (Exception ex)
            {

            }

            if (sunamo.FS.IsDirectoryEmpty(item, true, true))
            {
                sunamo.FS.TryDeleteDirectory(item);
            }

            return vr;
        }

        private static bool IsDirectoryEmpty(string item, bool folders, bool files)
        {
            int fse = 0;
            if (folders)
            {
                fse += Directory.GetDirectories(item, "*", SearchOption.TopDirectoryOnly).Length;
            }
            if (files)
            {
                fse += Directory.GetFiles(item, "*", SearchOption.TopDirectoryOnly).Length;
            }
            return fse == 0;
        }

        /// <summary>
        /// Vyhazuje výjimky, takže musíš volat v try-catch bloku
        /// A2 is root of target folder
        /// </summary>
        /// <param name="p"></param>
        /// <param name="to"></param>
        public static void MoveAllRecursivelyAndThenDirectory(string p, string to, FileMoveCollisionOption co)
        {
            string[] files = Directory.GetFiles(p, "*", SearchOption.AllDirectories);
            foreach (var item in files)
            {
                string fileTo = to + item.Substring(p.Length);
                MoveFile(item, fileTo, co);
            }
            string[] dirs = Directory.GetDirectories(p, "*", SearchOption.AllDirectories);
            for (int i = dirs.Length - 1; i >= 0; i--)
            {
                Directory.Delete(dirs[i], false);
            }
            Directory.Delete(p, false);
        }

        public static void DeleteFilesWithSameContent(List<string> files)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>(files.Count);
            foreach (var item in files)
            {
                dictionary.Add(item, TF.ReadFile(item));

            }

            Dictionary<string, List<string>> sameContent = DictionaryHelper.GroupByValues<string, string>(dictionary);

            foreach (var item in sameContent)
            {
                if (item.Value.Count > 1)
                {
                    item.Value.RemoveAt(0);
                    item.Value.ForEach(d => File.Delete(d));
                }
            }

        }

        public static string MakeUncLongPath(ref string path)
        {
            if (!path.StartsWith(Consts.UncLongPath))
            {
                path = Consts.UncLongPath + path;
            }
            return path;
        }

        /// <summary>
        /// A2 is path of target file
        /// </summary>
        /// <param name="item"></param>
        /// <param name="fileTo"></param>
        /// <param name="co"></param>
        public static void MoveFile(string item, string fileTo, FileMoveCollisionOption co)
        {
            item = Consts.UncLongPath + item;
            MakeUncLongPath(ref fileTo);
            sunamo.FS.CreateUpfoldersPsysicallyUnlessThere(fileTo);
            if (File.Exists(fileTo))
            {
                if (co == FileMoveCollisionOption.AddFileSize)
                {
                    string newFn = sunamo.FS.InsertBetweenFileNameAndExtension(fileTo, " " + sunamo.FS.GetFileSize(item));
                    if (File.Exists(newFn))
                    {
                        File.Delete(item);
                        return;
                    }
                    fileTo = newFn;
                }
                else if (co == FileMoveCollisionOption.AddSerie)
                {
                    int serie = 1;
                    while (true)
                    {
                        string newFn = sunamo.FS.InsertBetweenFileNameAndExtension(fileTo, " (" + serie + ")");
                        if (!File.Exists(newFn))
                        {
                            fileTo = newFn;
                            break;
                        }
                        serie++;
                    }
                }
                else if (co == FileMoveCollisionOption.DiscardFrom)
                {
                    File.Delete(item);
                    return;
                }
                else if (co == FileMoveCollisionOption.Overwrite)
                {
                    File.Delete(fileTo);
                }
                else if (co == FileMoveCollisionOption.LeaveLarger)
                {
                    long fsFrom = sunamo.FS.GetFileSize(item);
                    long fsTo = sunamo.FS.GetFileSize(fileTo);
                    if (fsFrom > fsTo)
                    {
                        File.Delete(fileTo);

                    }
                    else //if (fsFrom < fsTo)
                    {
                        File.Delete(item);
                        return;
                    }

                }
            }
            File.Move(item, fileTo);
        }

        public static Dictionary<string, List<string>> SortPathsByFileName(string[] allCsFilesInFolder, bool onlyOneExtension)
        {
            Dictionary<string, List<string>> vr = new Dictionary<string, List<string>>();
            foreach (var item in allCsFilesInFolder)
            {
                string fn = null;
                if (onlyOneExtension)
                {
                    fn = Path.GetFileNameWithoutExtension(item);
                }
                else
                {
                    fn = Path.GetFileName(item);
                }

                DictionaryHelper.AddOrCreate<string, string>(vr, fn, item);
            }
            return vr;
        }

        /// <summary>
        /// Vyhazuje výjimky, takže musíš volat v try-catch bloku
        /// </summary>
        /// <param name="p"></param>
        public static void DeleteAllRecursivelyAndThenDirectory(string p)
        {

            string[] files = Directory.GetFiles(p, "*", SearchOption.AllDirectories);
            foreach (var item in files)
            {
                File.Delete(item);
            }
            string[] dirs = Directory.GetDirectories(p, "*", SearchOption.AllDirectories);
            for (int i = dirs.Length - 1; i >= 0; i--)
            {
                Directory.Delete(dirs[i], false);
            }
            Directory.Delete(p, false);


        }

        public static string[] OnlyExtensions(List<string> cesta)
        {
            string[] vr = new string[cesta.Count];
            for (int i = 0; i < vr.Length; i++)
            {
                vr[i] = FS.GetExtension(cesta[i]);
            }
            return vr;
        }

        /// <summary>
        /// Both filenames and extension convert to lowercase
        /// Filename is without extension
        /// </summary>
        /// <param name="folder"></param>
        /// <param name="mask"></param>
        /// <param name="searchOption"></param>
        /// <returns></returns>
        public static Dictionary<string, List<string>> GetDictionaryByExtension(string folder, string mask, SearchOption searchOption)
        {
            Dictionary<string, List<string>> extDict = new Dictionary<string, List<string>>();
            foreach (var item in Directory.GetFiles(folder, mask, searchOption))
            {
                string ext = FS.GetExtension(item);
                string fn = FS.GetFileNameWithoutExtensionLower(item);
                DictionaryHelper.AddOrCreate<string, string>(extDict, ext, fn);
            }
            return extDict;
        }

        public static string[] OnlyExtensionsToLower(List<string> cesta)
        {
            string[] vr = new string[cesta.Count];
            for (int i = 0; i < vr.Length; i++)
            {
                vr[i] = FS.GetExtension(cesta[i]).ToLower();
            }
            return vr;
        }

        public static string[] OnlyExtensionsToLowerWithPath(List<string> cesta)
        {
            string[] vr = new string[cesta.Count];
            for (int i = 0; i < vr.Length; i++)
            {
                string path, fn, ext;
                FS.GetPathAndFileName(cesta[i], out path, out fn, out ext);
                vr[i] = path + fn + ext.ToLower();
            }
            return vr;
        }

        /// <summary>
        /// Create all upfolders of A1 with, if they dont exist 
        /// A2 zdali je A1 folder. Pokud je A1 soubor, dej false.
        /// Tuto metodu s parametrem false můžeš používat stejně jako CreateUpfoldersPsysicallyUnlessThere s 1 parametrem, tato ale bude o něco málo rychlejší.
        /// </summary>
        /// <param name="nad"></param>
        public static void CreateFoldersPsysicallyUnlessThere(string nad, bool isFolder)
        {
            if (Directory.Exists(nad))
            {
                return;
            }
            else
            {
                List<string> slozkyKVytvoreni = new List<string>();
                if (isFolder)
                {
                    slozkyKVytvoreni.Add(nad);
                }

                while (true)
                {
                    nad = sunamo.FS.GetDirectoryName(nad);

                    if (Directory.Exists(nad))
                    {
                        break;
                    }

                    string kopia = nad;
                    slozkyKVytvoreni.Add(kopia);
                }
                slozkyKVytvoreni.Reverse();
                foreach (string item in slozkyKVytvoreni)
                {
                    if (!Directory.Exists(item))
                    {
                        Directory.CreateDirectory(item);
                    }
                }
            }
        }

        public static List<string> AllExtensionsInFolders(SearchOption so, params string[] v)
        {
            List<string> vr = new List<string>();
            List<string> files = AllFilesInFolders(so, v);

            files = new List<string>(OnlyExtensionsToLower(files));
            foreach (var item in files)
            {
                if (!vr.Contains(item))
                {
                    vr.Add(item);
                }
            }
            return vr;
        }

        public static List<string> AllFilesInFolders(SearchOption so, params string[] v)
        {
            List<string> files = new List<string>();
            foreach (var item in v)
            {
                files.AddRange(Directory.GetFiles(item, "*.*", so));
            }
            return files;
        }

        public static string ReplaceIncorrectCharactersFile(string p, string replaceAllOfThisThen, string replaceForThis)
        {
            string t = p;
            foreach (char item in Path.GetInvalidFileNameChars())
            {
                StringBuilder sb = new StringBuilder();
                foreach (char item2 in t)
                {
                    if (item != item2)
                    {
                        sb.Append(item2);
                    }
                }
                t = sb.ToString();
            }
            if (!string.IsNullOrEmpty(replaceAllOfThisThen))
            {
                t = SH.ReplaceAll(t, replaceForThis, replaceAllOfThisThen);

            }
            return t;
        }

        /// <summary>
        /// Pro odstranění špatných znaků odstraní všechny výskyty A2 za mezery a udělá z více mezere jediné
        /// </summary>
        /// <param name="p"></param>
        /// <param name="replaceAllOfThisThen"></param>
        /// <returns></returns>
        public static string ReplaceIncorrectCharactersFile(string p, string replaceAllOfThisThen)
        {
            string t = p;
            foreach (char item in Path.GetInvalidFileNameChars())
            {
                StringBuilder sb = new StringBuilder();
                foreach (char item2 in t)
                {
                    if (item != item2)
                    {
                        sb.Append(item2);
                    }
                }
                t = sb.ToString();
            }
            if (!string.IsNullOrEmpty(replaceAllOfThisThen))
            {
                t = SH.ReplaceAll(t, " ", replaceAllOfThisThen);
                t = SH.ReplaceAll(t, " ", "  ");
            }
            return t;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static string RepairFilter(string filter)
        {
            if (!filter.Contains("|"))
            {
                filter = filter.TrimStart('*');
                return "*" + filter + "|" + "*" + filter;
            }
            return filter;
        }



        /// <summary>
        /// Pokud by byla cesta zakončená backslashem, vrátila by metoda Path.GetFileName prázdný řetězec. 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string GetFileNameWithoutExtensionLower(string s)
        {
            return GetFileNameWithoutExtension(s).ToLower();
        }

        /// <summary>
        /// Do A1 se dává buď celá cesta ke souboru, nebo jen jeho název(může být i včetně neomezeně přípon)
        /// A2 říká, zda se má vrátit plná cesta ke souboru A1, upraví se pouze samotný název souboru
        /// Works for brackets, not dash 
        /// </summary>
        public static string GetNameWithoutSeries(string p, bool path)
        {
            int serie;
            bool hasSerie = false;
            return GetNameWithoutSeries(p, path, out hasSerie, SerieStyle.Brackets, out serie);
        }

        //public static string GetNameWithoutSeries(string p, bool path, out bool hasSerie, SerieStyle serieStyle)
        //{
        //    int serie;
        //    return GetNameWithoutSeries(p, path, out hasSerie, serieStyle, out serie);
        //}

        public static string GetNameWithoutSeries(string p, bool path, out bool hasSerie, SerieStyle serieStyle)
        {
            int serie;
            return GetNameWithoutSeries(p, path, out hasSerie, serieStyle, out serie);
        }

        /// <summary>
        /// 
        /// Vrací vždy s příponou
        /// Do A1 se dává buď celá cesta ke souboru, nebo jen jeho název(může být i včetně neomezeně přípon)
        /// A2 říká, zda se má vrátit plná cesta ke souboru A1, upraví se pouze samotný název souboru
        /// When file has unknown extension, return SE
        /// Default for A4 was bracket
        /// </summary>
        /// <param name="p"></param>
        /// <param name="path"></param>
        /// <param name="hasSerie"></param>
        /// <returns></returns>
        public static string GetNameWithoutSeries(string p, bool path, out bool hasSerie, SerieStyle serieStyle, out int serie)
        {
            serie = -1;
            hasSerie = false;
            string dd = sunamo.FS.WithEndSlash(FS.GetDirectoryName(p));
            StringBuilder sbExt = new StringBuilder();
            string ext = FS.GetExtension(p);
            p = SH.TrimEnd(p, ext);
            sbExt.Append(ext);
            int pocetSerii = 0;

            while (true)
            {
                ext = FS.GetExtension(p);
                if (ext == string.Empty)
                {
                    break;
                }

                if (p.Contains(AllStrings.us))
                {
                    RemoveSerieUnderscore(ref serie, ref p, ref pocetSerii);
                }

                ext = FS.GetExtension(p);
                if (ext == string.Empty)
                {
                    break;
                }

                sbExt.Insert(0, ext);
                p = SH.TrimEnd(p, ext);
                // better than in cycle remove extensions - resistant to file with many extensions Image-2015-01-27-at-8.09.26-PM
                if (AllExtensionsHelper.FindTypeWithDot(ext) == TypeOfExtension.other)
                {
                    return "";
                }
            }
            ext = sbExt.ToString();

            string g = p;

            if (dd.Length != 0)
            {
                g = g.Substring(dd.Length);
            }

            // Nejdříve ořežu všechny přípony a to i tehdy, má li soubor více přípon
            
            if (serieStyle == SerieStyle.Brackets || serieStyle == SerieStyle.All)
            {
                while (true)
                {
                    g = g.Trim();
                    int lb = g.LastIndexOf(AllChars.lb);
                    int rb = g.LastIndexOf(AllChars.rb);

                    if (lb != -1 && rb != -1)
                    {
                        string between = SH.GetTextBetweenTwoChars(g, lb, rb);
                        if (SH.IsNumber(between))
                        {
                            serie = int.Parse(between);
                            pocetSerii++;
                            // s - 4, on end (1) - 
                            g = g.Substring(0, lb);
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }


                }
            }

            if (serieStyle == SerieStyle.Dash || serieStyle == SerieStyle.All)
            {
                int dex = g.IndexOf(AllChars.dash);

                if (g[g.Length - 3] == '-')
                {
                    serie = int.Parse(g.Substring(g.Length - 2));
                    g = g.Substring(0, g.Length - 3);
                }
                else if (g[g.Length - 2] == '-')
                {
                    serie = int.Parse(g.Substring(g.Length - 1));
                    g = g.Substring(0, g.Length - 2);

                }
                // To true hasSerie
                pocetSerii++;

            }

            if (serieStyle == SerieStyle.Underscore || serieStyle == SerieStyle.All)
            {
                RemoveSerieUnderscore(ref serie, ref g, ref pocetSerii);

            }

            if (pocetSerii != 0)
            {
                hasSerie = true;
            }
            g = g.Trim();
            if (path)
            {
                return dd + g + ext;
            }
            return g + ext;

            return "";
        }

        public static string RemoveSerieUnderscore(string d)
        {
            int serie = 0;
            int pocetSerii = 0;
            RemoveSerieUnderscore(ref serie, ref d, ref pocetSerii);
            return d;
        }

        private static void RemoveSerieUnderscore(ref int serie, ref string g, ref int pocetSerii)
        {
            while (true)
            {
                int dex = g.LastIndexOf('_');
                if (dex != -1)
                {
                    string serieS = g.Substring(dex + 1);
                    g = g.Substring(0, dex);

                    if (int.TryParse(serieS, out serie))
                    {

                        pocetSerii++;
                    }

                }
                else
                {
                    break;
                }
            }
        }

        /// <summary>
        /// Use this than Path.Combine which if argument starts with backslash ignore all arguments before this
        /// </summary>
        /// <param name="upFolderName"></param>
        /// <param name="dirNameDecoded"></param>
        /// <returns></returns>
        public static string Combine(params string[] s)
        {
            s = CA.TrimStart(AllChars.bs, s);
            return Path.Combine(s);
        }

        public static string WithoutEndSlash(string v)
        {
            return v.TrimEnd(AllChars.bs);
        }

        public static string WithEndSlash(string v)
        {
            if (v == string.Empty)
            {
                return v;
            }
            return v.TrimEnd(AllChars.bs) + AllChars.bs;
        }

        /// <summary>
        /// Works with and without end backslash
        /// Return with backslash
        /// </summary>
        /// <param name="rp"></param>
        /// <returns></returns>
        public static string GetDirectoryName(string rp)
        {
            rp = rp.TrimEnd(AllChars.bs);
            int dex = rp.LastIndexOf(AllChars.bs);
            if (dex != -1)
            {
                return rp.Substring(0, dex + 1);
            }
            return "";
        }

        /// <summary>
        /// If path ends with backslash, FS.GetDirectoryName returns empty string
        /// </summary>
        /// <param name="rp"></param>
        /// <returns></returns>
        public static string GetFileName(string rp)
        {
            rp = rp.TrimEnd(AllChars.bs);
            int dex = rp.LastIndexOf(AllChars.bs);
            return rp.Substring(dex + 1);
        }

        /// <summary>
        /// Keys returns with normalized ext
        /// In case zero files of ext wont be included in dict
        /// </summary>
        /// <param name="folderFrom"></param>
        /// <param name="extensions"></param>
        /// <returns></returns>
        public static Dictionary<string, string[]> FilesOfExtensions(string folderFrom, params string[] extensions)
        {
            Dictionary<string, string[]> dict = new Dictionary<string, string[]>();
            foreach (var item in extensions)
            {
                string ext = FS.NormalizeExtension(item);
                string[] files = Directory.GetFiles(folderFrom, "*" + ext, SearchOption.AllDirectories);
                if (files.Length != 0)
                {
                    dict.Add(ext, files);
                }

            }
            return dict;
        }

        /// <summary>
        /// convert to lowercase and remove first dot
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static string NormalizeExtension(string item)
        {
            return "." + item.TrimStart('.');
        }

        public static string GetNormalizedExtension(string filename)
        {
            return NormalizeExtension(filename);
        }

        static FS()
        {
            invalidPathChars = new List<char>(Path.GetInvalidPathChars());
            if (!invalidPathChars.Contains('/'))
            {
                invalidPathChars.Add('/');
            }
            if (!invalidPathChars.Contains(AllChars.bs))
            {
                invalidPathChars.Add(AllChars.bs);
            }
            invalidFileNameChars = new List<char>(Path.GetInvalidFileNameChars());
            for (char i = (char)65529; i < 65534; i++)
            {
                invalidFileNameChars.Add(i);
            }

            invalidCharsForMapPath = new List<char>();
            invalidCharsForMapPath.AddRange(invalidFileNameChars.ToArray());
            foreach (var item in Path.GetInvalidFileNameChars())
            {
                if (!invalidCharsForMapPath.Contains(item))
                {
                    invalidCharsForMapPath.Add(item);
                }
            }

            invalidCharsForMapPath.Remove('/');

            invalidFileNameCharsWithoutDelimiterOfFolders = new List<char>(invalidFileNameChars.ToArray());

            invalidFileNameCharsWithoutDelimiterOfFolders.Remove(AllChars.bs);
            invalidFileNameCharsWithoutDelimiterOfFolders.Remove('/');
        }

        public static void ReplaceDiacriticRecursive(string folder, bool dirs, bool files, DirectoryMoveCollisionOption fo, FileMoveCollisionOption co)
        {
            if (dirs)
            {
                List<ItemWithCount<string>> dires = sunamo.FS.DirectoriesWithToken(folder, AscDesc.Desc);
                foreach (var item in dires)
                {
                    var dirPath = sunamo.FS.WithoutEndSlash(item.t);
                    string dirName = Path.GetFileName(dirPath);
                    if (SH.ContainsDiacritic(dirName))
                    {
                        string dirNameWithoutDiac = SH.TextWithoutDiacritic(dirName);
                        sunamo.FS.RenameDirectory(item.t, dirNameWithoutDiac, fo, co);
                    }
                }
            }

            if (files)
            {
                string[] files2 = Directory.GetFiles(folder, "*", SearchOption.AllDirectories);
                foreach (var item in files2)
                {
                    string filePath = item;
                    string fileName = Path.GetFileName(filePath);
                    if (SH.ContainsDiacritic(fileName))
                    {
                        string dirNameWithoutDiac = SH.TextWithoutDiacritic(fileName);
                        sunamo.FS.RenameFile(item, dirNameWithoutDiac, co);
                    }

                }
            }
        }

        /// <summary>
        /// Physically rename file, this method is different from ChangeFilename in FileMoveCollisionOption A3 which can control advanced collision solution
        /// </summary>
        /// <param name="item"></param>
        /// <param name="dirNameWithoutDiac"></param>
        /// <param name="co"></param>
        public static void RenameFile(string item, string dirNameWithoutDiac, FileMoveCollisionOption co)
        {
            sunamo.FS.MoveFile(item, sunamo.FS.ChangeFilename(item, dirNameWithoutDiac, false), co);
        }

        /// <summary>
        /// Může výhodit výjimku, proto je nutné používat v try-catch bloku
        /// Vrátí řetězec se zprávou kterou vypsat nebo null
        /// </summary>
        /// <param name="path"></param>
        /// <param name="newname"></param>
        public static string RenameDirectory(string path, string newname, DirectoryMoveCollisionOption co, FileMoveCollisionOption fo)
        {
            string vr = null;
            path = sunamo.FS.WithoutEndSlash(path);
            string cesta = FS.GetDirectoryName(path);
            string nova = Path.Combine(cesta, newname);

            vr = MoveDirectoryNoRecursive(path, nova, co, fo);
            return vr;
        }

        /// <summary>
        /// Pokusí se max. 10x smazat soubor A1, pokud se nepodaří, GF, jinak GT
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static bool TryDeleteWithRepetition(string item)
        {
            int pokusyOSmazani = 0;
            while (true)
            {
                try
                {
                    File.Delete(item);
                    return true;
                }
                catch (Exception ex)
                {
                    pokusyOSmazani++;
                    if (pokusyOSmazani == 9)
                    {
                        return false;
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static bool TryDeleteFile(string item)
        {
            // TODO: To all code message logging as here

            try
            {
                File.Delete(item);
                return true;
            }
            catch (Exception ex)
            {
                ThisApp.SetStatus(TypeOfMessage.Error, "File can't be deleted: " + item);
                return false;
            }
        }

        public static bool TryDeleteFile(string item, out string message)
        {
            message = null;
            try
            {
                File.Delete(item);
                return true;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// Vrátí cestu a název souboru s ext
        /// </summary>
        /// <param name="fn"></param>
        /// <param name="path"></param>
        /// <param name="file"></param>
        public static void GetPathAndFileName(string fn, out string path, out string file)
        {
            path = FS.GetDirectoryName(fn);
            file = Path.GetFileName(fn);
        }

        /// <summary>
        /// Vrátí cestu a název souboru s ext a ext
        /// </summary>
        /// <param name="fn"></param>
        /// <param name="path"></param>
        /// <param name="file"></param>
        /// <param name="ext"></param>
        public static void GetPathAndFileName(string fn, out string path, out string file, out string ext)
        {
            path = FS.GetDirectoryName(fn) + AllChars.bs;
            file = Path.GetFileName(fn);
            ext = FS.GetExtension(file);
        }

        /// <summary>
        /// Vrátí cestu a název souboru bez ext a ext
        /// All returned is normal case
        /// </summary>
        /// <param name="fn"></param>
        /// <param name="path"></param>
        /// <param name="file"></param>
        /// <param name="ext"></param>
        public static void GetPathAndFileNameWithoutExtension(string fn, out string path, out string file, out string ext)
        {
            path = Path.GetDirectoryName(fn) + AllChars.bs;
            file = Path.GetFileNameWithoutExtension(fn);
            ext = Path.GetExtension(fn);
        }

        public static List<string> FilesOfExtensionsArray(string folder, List<string> extension)
        {
            List<string> foundedFiles = new List<string>();

            FS.NormalizeExtensions(extension);

            var files = Directory.EnumerateFiles(folder, "*.*", SearchOption.AllDirectories);
            foreach (var item in files)
            {
                string ext = FS.GetNormalizedExtension(item);
                if (extension.Contains(ext))
                {
                    foundedFiles.Add(ext);
                }
            }

            return foundedFiles;
        }

        /// <summary>
        /// convert to lowercase and remove first dot
        /// </summary>
        /// <param name="extension"></param>
        private static void NormalizeExtensions(List<string> extension)
        {
            for (int i = 0; i < extension.Count; i++)
            {
                extension[i] = NormalizeExtension(extension[i]);
            }
        }

        /// <summary>
        /// A1 může obsahovat celou cestu, vrátí jen název sobuoru bez připony a příponu
        /// </summary>
        /// <param name="fn"></param>
        /// <param name="path"></param>
        /// <param name="file"></param>
        /// <param name="ext"></param>
        public static void GetFileNameWithoutExtensionAndExtension(string fn, out string file, out string ext)
        {
            file = Path.GetFileNameWithoutExtension(fn);
            ext = FS.GetExtension(file);
        }

        /// <summary>
        /// Get number higher by one from the number filenames with highest value (as 3.txt)
        /// </summary>
        /// <param name="slozka"></param>
        /// <param name="fn"></param>
        /// <param name="ext"></param>
        /// <returns></returns>
        public static string GetFileSeries(string slozka, string fn, string ext)
        {
            int dalsi = 0;
            string[] soubory = System.IO.Directory.GetFiles(slozka);
            foreach (string item in soubory)
            {
                int p;
                string withoutFn = SH.ReplaceOnce(item, fn, "");
                string withoutFnAndExt = SH.ReplaceOnce(withoutFn, ext, "");
                if (int.TryParse(System.IO.Path.GetFileNameWithoutExtension(withoutFnAndExt), out p))
                {
                    if (p > dalsi)
                    {
                        dalsi = p;
                    }
                }
            }

            dalsi++;

            return Path.Combine(slozka, fn + "_" + dalsi + ext);
        }

        public static void CreateDirectoryIfNotExists(string p)
        {
            if (!Directory.Exists(p))
            {
                Directory.CreateDirectory(p);
            }
        }

        public static void SaveMemoryStream(System.IO.MemoryStream mss, string path)
        {
            path = path.Replace("\\\\", "\\");
            if (!File.Exists(path))
            {
                using (System.IO.FileStream fs = new System.IO.FileStream(path, System.IO.FileMode.Create, System.IO.FileAccess.Write))
                {
                    byte[] matriz = mss.ToArray();
                    fs.Write(matriz, 0, matriz.Length);
                }

            }
        }

        public static void SaveStream(string path, Stream s)
        {

            using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
            {
                sunamo.FS.CopyStream(s, fs);
                fs.Flush();

            }


        }

        public static long GetFileSize(string item)
        {
            FileInfo fi = null;
            try
            {
                fi = new FileInfo(item);
            }
            catch (Exception)
            {
                // Například příliš dlouhý název souboru
                return 0;
            }
            if (fi.Exists)
            {
                return fi.Length;
            }
            return 0;
        }

        /// <summary>
        /// Vrátí vč. cesty
        /// </summary>
        /// <param name="orig"></param>
        /// <param name="whatInsert"></param>
        /// <returns></returns>
        public static string InsertBetweenFileNameAndExtension(string orig, string whatInsert)
        {
            string p = FS.GetDirectoryName(orig);
            string fn = Path.GetFileNameWithoutExtension(orig);
            string e = FS.GetExtension(orig);
            return Path.Combine(p, fn + whatInsert + e);
        }
        /// <summary>
        /// Vratí bez cesty, pouze název souboru
        /// </summary>
        /// <param name="orig"></param>
        /// <param name="whatInsert"></param>
        /// <returns></returns>
        public static string InsertBetweenFileNameAndExtension2(string orig, string whatInsert)
        {
            string fn = Path.GetFileNameWithoutExtension(orig);
            string e = FS.GetExtension(orig);
            return Path.Combine(fn + whatInsert + e);
        }
        /// <summary>
        /// Create all upfolders of A1, if they dont exist 
        /// </summary>
        /// <param name="nad"></param>
        public static void CreateUpfoldersPsysicallyUnlessThere(string nad)
        {
            CreateFoldersPsysicallyUnlessThere(FS.GetDirectoryName(nad));
        }

        /// <summary>
        /// Create all upfolders of A1 with, if they dont exist 
        /// </summary>
        /// <param name="nad"></param>
        public static void CreateFoldersPsysicallyUnlessThere(string nad)
        {
            if (Directory.Exists(nad))
            {
                return;
            }
            else
            {
                List<string> slozkyKVytvoreni = new List<string>();
                slozkyKVytvoreni.Add(nad);
                while (true)
                {
                    nad = FS.GetDirectoryName(nad);

                    if (Directory.Exists(nad))
                    {
                        break;
                    }

                    string kopia = nad;
                    slozkyKVytvoreni.Add(kopia);
                }
                slozkyKVytvoreni.Reverse();
                foreach (string item in slozkyKVytvoreni)
                {
                    string folder = Consts.UncLongPath + item;
                    if (!Directory.Exists(folder))
                    {
                        Directory.CreateDirectory(folder);
                    }
                }
            }
        }

        public static string[] OnlyNames(string[] files2)
        {
            return OnlyNames(CA.ToListString(files2)).ToArray();
        }

        /// <summary>
        /// Returns with extension
        /// POZOR: Na rozdíl od stejné metody v swf tato metoda vrací úplně nové pole a nemodifikuje A1
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        public static List<string> OnlyNames(List<string> files2)
        {
            List<string> files = new List<string>(files2.Count);
            for (int i = 0; i < files2.Count; i++)
            {
                files.Add(Path.GetFileName(files2[i]));
            }
            return files;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static string[] OnlyNamesWithoutExtension(string[] p)
        {
            for (int i = 0; i < p.Length; i++)
            {
                p[i] = Path.GetFileNameWithoutExtension(p[i]);
            }
            return p;
        }

        public static string[] OnlyNamesWithoutExtensionCopy(List<string> p2)
        {
            string[] p = new string[p2.Count];
            for (int i = 0; i < p2.Count; i++)
            {
                p[i] = Path.GetFileNameWithoutExtension(p2[i]);
            }
            return p;
        }

        public static string DeleteWrongCharsInFileName(string p, bool isPath)
        {
            List<char> invalidFileNameChars2 = null;

            if (isPath)
            {
                invalidFileNameChars2 = invalidFileNameCharsWithoutDelimiterOfFolders;
            }
            else
            {
                invalidFileNameChars2 = invalidFileNameChars;
            }

            StringBuilder sb = new StringBuilder();
            foreach (char item in p)
            {
                if (!invalidFileNameChars2.Contains(item))
                {
                    sb.Append(item);
                }
            }

            return sb.ToString();
        }

        public static string DeleteWrongCharsInDirectoryName(string p)
        {

            StringBuilder sb = new StringBuilder();
            foreach (char item in p)
            {
                if (!invalidPathChars.Contains(item))
                {
                    sb.Append(item);
                }

            }
            return sb.ToString();
        }

        public static string[] OnlyNamesWithoutExtension(string appendToStart, string[] fullPaths)
        {
            string[] ds = new string[fullPaths.Length];
            for (int i = 0; i < fullPaths.Length; i++)
            {
                ds[i] = appendToStart + Path.GetFileNameWithoutExtension(fullPaths[i]);
            }
            return ds;
        }

        public static string[] OnlyNames(string appendToStart, string[] fullPaths)
        {
            string[] ds = new string[fullPaths.Length];
            for (int i = 0; i < fullPaths.Length; i++)
            {
                ds[i] = appendToStart + Path.GetFileName(fullPaths[i]);
            }
            return ds;
        }

        /// <summary>
        /// Odstraňuje samozřejmě ve výjimce
        /// </summary>
        /// <param name="path"></param>
        public static void DeleteFileIfExists(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public static bool TryDeleteDirectory(string v)
        {
            try
            {
                Directory.Delete(v, true);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool ContainsInvalidPathCharForPartOfMapPath(string p)
        {
            foreach (var item in invalidCharsForMapPath)
            {
                if (p.IndexOf(item) != -1)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Pokud hledáš metodu ReplacePathToFile, je to tato. Sloučeny protože dělali totéž.
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="changeFolderTo"></param>
        /// <returns></returns>
        public static string ChangeDirectory(string fileName, string changeFolderTo)
        {
            string p = sunamo.FS.GetDirectoryName(fileName);
            string fn = sunamo.FS.GetFileName(fileName);

            return Path.Combine(changeFolderTo, fn);
        }

        /// <summary>
        /// Zmeni nazev souboru na A2
        /// Pro A3 je výchozí z minulosti true - jakoby s false se chovala metoda ReplaceFileName
        /// Pokud nechci nazev souboru uplne menit, ale pouze v nem neco nahradit, pouziva se metoda ReplaceInFileName
        /// </summary>
        /// <param name="item"></param>
        /// <param name="g"></param>
        /// <param name="onDrive"></param>
        public static string ChangeFilename(string item, string g, bool physically)
        {
            string cesta = FS.GetDirectoryName(item);
            string nova = Path.Combine(cesta, g);

            if (physically)
            {
                try
                {
                    if (File.Exists(nova))
                    {
                        File.Delete(nova);
                    }
                    File.Move(item, nova);
                }
                catch (Exception ex)
                {
                }
            }
            return nova;
        }

        public static string ChangeExtension(string item, string newExt, bool physically)
        {
            string cesta = FS.GetDirectoryName(item);
            string fnwoe = Path.GetFileNameWithoutExtension(item);
            string nova = Path.Combine(cesta, fnwoe + newExt);

            if (physically)
            {
                try
                {
                    if (File.Exists(nova))
                    {
                        File.Delete(nova);
                    }
                    File.Move(item, nova);
                }
                catch (Exception ex)
                {
                }
            }
            return nova;
        }
    }
}

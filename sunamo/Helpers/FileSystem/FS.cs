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
using System.Diagnostics;
using sunamo;

public partial class FS
{
    public static string GetActualDateTime()
    {
        DateTime dt = DateTime.Now;
        return ReplaceIncorrectCharactersFile(dt.ToString());
    }
    public static Task DeleteFile(StorageFile t)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// A4 can be null if !A5
    /// In A1 will keep files which doesnt exists in A3
    /// </summary>
    /// <param name="filesFrom"></param>
    /// <param name="folderFrom"></param>
    /// <param name="folderTo"></param>
    /// <param name="wasntExistsInFrom"></param>
    /// <param name="mustExistsInTarget"></param>
    /// <param name="copy"></param>
    public static void CopyMoveFilesInList(List<string> filesFrom, string folderFrom, string folderTo,  List<string> wasntExistsInFrom, bool mustExistsInTarget, bool copy)
    {
        FS.WithoutEndSlash(folderFrom);
        FS.WithoutEndSlash(folderTo);

        CA.RemoveStringsEmpty2(filesFrom);

        for (int i = filesFrom.Count - 1; i >= 0; i--)
        {
            filesFrom[i] = filesFrom[i].Replace(folderFrom, string.Empty);

            var oldPath = folderFrom + filesFrom[i];
            var newPath = folderTo + filesFrom[i];

            if (!File.Exists(oldPath))
            {
                
                wasntExistsInFrom.Add(filesFrom[i]);
                filesFrom.RemoveAt(i);
                continue;
            }

            if (!File.Exists(newPath) && mustExistsInTarget)
            {
                
                
                continue;
            }
            else
            {
                if (copy)
                {
                    FS.CopyFile(oldPath, newPath, FileMoveCollisionOption.Overwrite);
                }
                else
                {
                    FS.MoveFile(oldPath, newPath, FileMoveCollisionOption.Overwrite);
                }
                filesFrom.RemoveAt(i);
            }
        }
    }

    public async static System.Threading.Tasks.Task<StorageFile> GetStorageFile(StorageFolder folder, string v)
    {
        return new StorageFile(folder.fullPath, v);
    }

    public static void DeleteEmptyFiles(string folder, SearchOption so)
    {
        var files = FS.GetFiles(folder, FS.MascFromExtension(), so);
        foreach (var item in files)
        {
            var fs = FS.GetFileSize(item);
            if (fs == 0)
            {
                FS.TryDeleteFile(item);
            }
            else if (fs < 4)
            {
                if (TF.ReadFile(item).Trim() == string.Empty)
                {
                    FS.TryDeleteFile(item);
                }
            }
        }
    }

    /// <summary>
    /// either A1 or A2 can be null
    /// When A2 is null, will get from file path A1
    /// </summary>
    /// <param name="item"></param>
    /// <param name="folder"></param>
    /// <param name="insert"></param>
    /// <returns></returns>
    public static string InsertBetweenFileNameAndPath(string item, string folder, string insert)
    {
        if (folder == null)
        {
            folder = FS.GetDirectoryName(item);
        }
        var outputFolder = FS.Combine(folder, insert);
        FS.CreateDirectoryIfNotExists(outputFolder);
        return FS.Combine(outputFolder, Path.GetFileName(item));
    }

    public static void ReplaceInAllFiles(string from, string to, List<string> files, bool pairLinesInFromAndTo)
    {
        if (pairLinesInFromAndTo)
        {
            var from2 = SH.Split(from, Environment.NewLine);
            var to2 = SH.Split(to, Environment.NewLine);
            ThrowExceptions.DifferentCountInLists(s_type, "ReplaceInAllFiles", "from2", from2, "to2", to2);

            ReplaceInAllFiles(from2, to2, files);
        }
        else
        {
            ReplaceInAllFiles(CA.ToListString(from), CA.ToListString(to), files);
        }
    }

    public static void ReplaceInAllFiles(string folder, string extension, IList<string> replaceFrom, IList<string> replaceTo)
    {
        var files = FS.GetFiles(folder, FS.MascFromExtension(extension), SearchOption.AllDirectories);
        ThrowExceptions.DifferentCountInLists(s_type, "ReplaceInAllFiles", "replaceFrom", replaceFrom, "replaceTo", replaceTo);
        ReplaceInAllFiles(replaceFrom, replaceTo, files);
    }

    /// <summary>
    /// A4 - whether use s.Contains. A4 - SH.ReplaceAll2
    /// </summary>
    /// <param name="replaceFrom"></param>
    /// <param name="replaceTo"></param>
    /// <param name="files"></param>
    /// <param name="dontReplaceAll"></param>
    public static void ReplaceInAllFiles(IList<string> replaceFrom, IList<string> replaceTo, List<string> files)
    {
        foreach (var item in files)
        {
            if (!EncodingHelper.isBinary(item))
            {
                var content = TF.ReadFile(item);
                if (SH.ContainsAny(content, false, replaceFrom).Count > 0)
                {
                    for (int i = 0; i < replaceFrom.Count; i++)
                    {
                        content = content.Replace(replaceFrom[i], replaceTo[i]);
                    }
                    TF.SaveFile(content, item);
                }
            }
        }
    }


    /// <summary>
    /// Jen kvuli starým aplikacím, at furt nenahrazuji.
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    public static string GetFileInStartupPath(string v)
    {
        return AppPaths.GetFileInStartupPath(v);
    }

    public static void RemoveDiacriticInFileContents(string folder, string mask)
    {
        string[] files = Directory.GetFiles(folder, mask, SearchOption.AllDirectories);
        foreach (string item in files)
        {
            string df2 = File.ReadAllText(item, Encoding.Default);

            if (true) //SH.ContainsDiacritic(df2))
            {
                TF.SaveFile(SH.TextWithoutDiacritic(df2), item);
                df2 = SH.ReplaceOnce(df2, "\u010F\u00BB\u017C", "");
                TF.SaveFile(df2, item);
            }
        }
    }

    public static string RemoveFile(string fullPathCsproj)
    {
        // Most effecient way to handle csproj and dir
        var ext = FS.GetExtension(fullPathCsproj);
        if (ext != string.Empty)
        {
            fullPathCsproj = FS.GetDirectoryName(fullPathCsproj);
        }
        return FS.WithoutEndSlash(fullPathCsproj);
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

    public static string MakeFromLastPartFile(string fullPath, string ext)
    {
        FS.WithoutEndSlash(ref fullPath);
        return fullPath + ext;
    }

    public static void CreateFileIfDoesntExists(string path)
    {
        if (!FS.ExistsFile(path))
        {
            File.CreateText(path);
        }
    }

    /// <summary>
    /// Remove all extensions, not only one
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public static string GetFileNameWithoutExtensions(string item)
    {
        while (Path.HasExtension(item))
        {
            item = FS.GetFileNameWithoutExtension(item);
        }
        return item;
    }

    public static void CopyAs0KbFiles(string pathDownload, string pathVideos0Kb)
    {
        FS.WithEndSlash(ref pathDownload);
        FS.WithEndSlash(ref pathVideos0Kb);

        var files = FS.GetFiles(pathDownload, true);
        foreach (var item in files)
        {
            var path = item.Replace(pathDownload, pathVideos0Kb);

            FS.CreateUpfoldersPsysicallyUnlessThere(path);
            TF.WriteAllText(path, string.Empty);
        }
    }

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

    public static string ReplaceInvalidFileNameChars(string filename)
    {
        StringBuilder sb = new StringBuilder();
        foreach (var item in filename)
        {
            if (!s_invalidFileNameChars.Contains(item))
            {
                sb.Append(item);
            }
        }
        return sb.ToString();
    }



    public static string CreateNewFolderPathWithEndingNextTo(string folder, string ending)
    {
        string pathToFolder = FS.GetDirectoryName(folder.TrimEnd(AllChars.bs)) + AllStrings.bs;
        string folderWithCaretFiles = pathToFolder + Path.GetFileName(folder.TrimEnd(AllChars.bs)) + ending;

        return folderWithCaretFiles;
    }



    public static void CopyFilesOfExtensions(string folderFrom, string FolderTo, params string[] extensions)
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
        List<string> folders = new List<string>(FS.GetFolders(folder, AllStrings.asterisk, SearchOption.AllDirectories));
        folders.Reverse();
        foreach (string item in folders)
        {
            string directory = FS.GetDirectoryName(item);
            string filename = Path.GetFileName(item);
            if (SH.ContainsDiacritic(filename))
            {
                filename = SH.TextWithoutDiacritic(filename);
                string newpath = FS.Combine(directory, filename);
                string realnewpath = SH.Copy(newpath).TrimEnd(AllChars.bs);
                string realnewpathcopy = SH.Copy(realnewpath);
                int i = 0;
                while (FS.ExistsDirectory(realnewpath))
                {
                    realnewpath = realnewpathcopy + i.ToString();
                    i++;
                }
                Directory.Move(item, realnewpath);
            }
        }
        string[] files = Directory.GetFiles(folder, AllStrings.asterisk, SearchOption.AllDirectories);
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
                    newpath = FS.Combine(directory, filename);
                }
                catch (Exception ex)
                {
                    File.Delete(item);
                    continue;
                }

                string realNewPath = SH.Copy(newpath);
                int vlozeno = 0;
                while (FS.ExistsFile(realNewPath))
                {
                    realNewPath = FS.InsertBetweenFileNameAndExtension(newpath, vlozeno.ToString());
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

    /// <summary>
    /// A2 true - bs to slash. false - slash to bs
    /// </summary>
    /// <param name="path"></param>
    /// <param name="v"></param>
    /// <returns></returns>
    public static string Slash(string path, bool slash)
    {
        if (slash)
        {
            return SH.ReplaceAll2(path, AllStrings.slash, AllStrings.bs);
        }
        else
        {
            return SH.ReplaceAll2(path, AllStrings.bs, AllStrings.slash);
        }
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
        string vr = p2 + AllStrings.bs + fn.Replace(what, forWhat);
        return vr;
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
                throw new Exception("Byl specifikov\u00E1n v\u00FDstupn\u00ED ComputerSizeUnit, nem\u016F\u017Eu toto nastaven\u00ED zm\u011Bnit");
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
            throw new Exception("Byl specifikov\u00E1n v\u00FDstupn\u00ED ComputerSizeUnit, nem\u016F\u017Eu toto nastaven\u00ED zm\u011Bnit");
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
        List<ItemWithCount<string>> dirs = FS.DirectoriesWithToken(v, AscDesc.Desc);


        foreach (var item in dirs)
        {
            if (FS.IsDirectoryEmpty(item.t, true, true))
            {
                FS.TryDeleteDirectory(item.t);
            }
        }
    }

    public static List<ItemWithCount<string>> DirectoriesWithToken(string v, AscDesc sb)
    {
        var dirs = FS.GetFolders(v, AllStrings.asterisk, SearchOption.AllDirectories);
        List<ItemWithCount<string>> vr = new List<ItemWithCount<string>>();
        foreach (var item in dirs)
        {
            vr.Add(new ItemWithCount<string> { t = item, count = SH.OccurencesOfStringIn(item, AllStrings.bs) });
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


    public static List<string> AllFilesInFolders(IEnumerable<string> folders, IEnumerable<string> exts, SearchOption so)
    {
        List<string> files = new List<string>();
        foreach (var item in folders)
        {
            foreach (var ext in exts)
            {
                files.AddRange(Directory.GetFiles(item, FS.MascFromExtension(ext), so));
            }
        }
        return files;
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
        if (FS.ExistsDirectory(nova))
        {
            if (co == DirectoryMoveCollisionOption.AddSerie)
            {
                int serie = 1;
                while (true)
                {
                    string newFn = nova + " (" + serie + AllStrings.rb;
                    if (!FS.ExistsDirectory(newFn))
                    {
                        vr = "Folder has been renamed to" + " " + Path.GetFileName(newFn);
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

        string[] files = Directory.GetFiles(item, AllStrings.asterisk, SearchOption.TopDirectoryOnly);
        FS.CreateFoldersPsysicallyUnlessThere(nova);
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

        if (FS.IsDirectoryEmpty(item, true, true))
        {
            FS.TryDeleteDirectory(item);
        }

        return vr;
    }

    private static bool IsDirectoryEmpty(string item, bool folders, bool files)
    {
        int fse = 0;
        if (folders)
        {
            fse += FS.GetFolders(item, AllStrings.asterisk, SearchOption.TopDirectoryOnly).Length();
        }
        if (files)
        {
            fse += Directory.GetFiles(item, AllStrings.asterisk, SearchOption.TopDirectoryOnly).Length();
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
        MoveAllFilesRecursively(p, to, co, null);
        var dirs = FS.GetFolders(p, AllStrings.asterisk, SearchOption.AllDirectories);
        for (int i = dirs.Length() - 1; i >= 0; i--)
        {
            Directory.Delete(dirs[i], false);
        }
        Directory.Delete(p, false);
    }

    public static void MoveAllFilesRecursively(string p, string to, FileMoveCollisionOption co, string contains = null)
    {
        CopyMoveAllFilesRecursively(p, to, co, true, contains, SearchOption.AllDirectories);
    }

    /// <summary>
    /// Unit tests = OK
    /// </summary>
    /// <param name="files"></param>
    public static void DeleteFilesWithSameContentBytes(List<string> files)
    {
        DeleteFilesWithSameContentWorking<byte[], byte>(files, File.ReadAllBytes);
    }

    /// <summary>
    /// Unit tests = OK
    /// </summary>
    /// <param name="files"></param>
    public static void DeleteDuplicatedImages(List<string> files)
    {
        ThrowExceptions.Custom(s_type, "DeleteDuplicatedImages", "Only for test files for another apps" + ". ");
    }

    public static void DeleteFilesWithSameContentWorking<T, ColType>(List<string> files, Func<string, T> readFunc)
    {
        Dictionary<string, T> dictionary = new Dictionary<string, T>(files.Count);
        foreach (var item in files)
        {
            dictionary.Add(item, readFunc.Invoke(item));
        }

        Dictionary<T, List<string>> sameContent = DictionaryHelper.GroupByValues<string, T, ColType>(dictionary);

        foreach (var item in sameContent)
        {
            if (item.Value.Count > 1)
            {
                item.Value.RemoveAt(0);
                item.Value.ForEach(d => File.Delete(d));
            }
        }
    }

    /// <summary>
    /// Working fine, verified by Unit tests
    /// </summary>
    /// <param name="files"></param>
    public static void DeleteFilesWithSameContent(List<string> files)
    {
        DeleteFilesWithSameContentWorking<string, object>(files, TF.ReadFile);
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

    public static void DeleteAllRecursively(string p, bool rootDirectoryToo = false)
    {
        if (FS.ExistsDirectory(p))
        {
            string[] files = Directory.GetFiles(p, AllStrings.asterisk, SearchOption.AllDirectories);
            foreach (var item in files)
            {
                File.Delete(item);
            }
            var dirs = FS.GetFolders(p, AllStrings.asterisk, SearchOption.AllDirectories);
            for (int i = dirs.Length() - 1; i >= 0; i--)
            {
                Directory.Delete(dirs[i], false);
            }
            if (rootDirectoryToo)
            {
                Directory.Delete(p, false);
            }
        }
    }

    /// <summary>
    /// Vyhazuje výjimky, takže musíš volat v try-catch bloku
    /// </summary>
    /// <param name="p"></param>
    public static void DeleteAllRecursivelyAndThenDirectory(string p)
    {
        DeleteAllRecursively(p, true);
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
    /// files as .bowerrc return whole
    /// </summary>
    /// <param name="so"></param>
    /// <param name="folders"></param>
    /// <returns></returns>
    public static List<string> AllExtensionsInFolders(SearchOption so, params string[] folders)
    {
        ThrowExceptions.NoPassedFolders(s_type, "AllExtensionsInFolders", folders);

        List<string> vr = new List<string>();
        List<string> files = AllFilesInFolders(CA.ToListString(folders), CA.ToListString("*."), so);

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



    public static string replaceIncorrectFor = string.Empty;

    /// <summary>
    /// Replacement can be configured with replaceIncorrectFor
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    public static string ReplaceIncorrectCharactersFile(string p)
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
                else
                {
                    sb.Append(replaceIncorrectFor);
                }
            }
            t = sb.ToString();
        }
        return t;
    }

    /// <summary>
    /// A3 is applicable only for A2. In general is use replaceIncorrectFor
    /// </summary>
    /// <param name="p"></param>
    /// <param name="replaceAllOfThisByA3"></param>
    /// <param name="replaceForThis"></param>
    /// <returns></returns>
    public static string ReplaceIncorrectCharactersFile(string p, string replaceAllOfThisByA3, string replaceForThis)
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
                else
                {
                    sb.Append(replaceIncorrectFor);
                }
            }
            t = sb.ToString();
        }
        if (!string.IsNullOrEmpty(replaceAllOfThisByA3))
        {
            t = SH.ReplaceAll(t, replaceForThis, replaceAllOfThisByA3);
        }
        return t;
    }

    public static string ExpandEnvironmentVariables(EnvironmentVariables environmentVariable)
    {
        return Environment.ExpandEnvironmentVariables(SH.WrapWith(environmentVariable.ToString(), AllChars.modulo));
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
                else
                {
                    sb.Append(replaceIncorrectFor);
                }
            }
            t = sb.ToString();
        }
        if (!string.IsNullOrEmpty(replaceAllOfThisThen))
        {
            t = SH.ReplaceAll(t, AllStrings.space, replaceAllOfThisThen);
            t = SH.ReplaceAll(t, AllStrings.space, AllStrings.doubleSpace);
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
        if (!filter.Contains(AllStrings.pipe))
        {
            filter = filter.TrimStart(AllChars.asterisk);
            return AllStrings.asterisk + filter + AllStrings.pipe + AllStrings.asterisk + filter;
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

    public static string AddUpfoldersToRelativePath(int i, string file, char delimiter)
    {
        var jumpUp = AllStrings.dd + delimiter;

        return SH.JoinTimes(i, jumpUp) + file;
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
            string[] files = Directory.GetFiles(folderFrom, AllStrings.asterisk + ext, SearchOption.AllDirectories);
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
        return AllStrings.dot + item.TrimStart(AllChars.dot);
    }

    public static string GetNormalizedExtension(string filename)
    {
        return NormalizeExtension(filename);
    }



    public static long ModifiedinUnix(string dsi)
    {
        return (long)(File.GetLastWriteTimeUtc(dsi).Subtract(DTConstants.UnixFsStart)).TotalSeconds;
    }

    public static void ReplaceDiacriticRecursive(string folder, bool dirs, bool files, DirectoryMoveCollisionOption fo, FileMoveCollisionOption co)
    {
        if (dirs)
        {
            List<ItemWithCount<string>> dires = FS.DirectoriesWithToken(folder, AscDesc.Desc);
            foreach (var item in dires)
            {
                var dirPath = FS.WithoutEndSlash(item.t);
                string dirName = Path.GetFileName(dirPath);
                if (SH.ContainsDiacritic(dirName))
                {
                    string dirNameWithoutDiac = SH.TextWithoutDiacritic(dirName);
                    FS.RenameDirectory(item.t, dirNameWithoutDiac, fo, co);
                }
            }
        }

        if (files)
        {
            string[] files2 = Directory.GetFiles(folder, AllStrings.asterisk, SearchOption.AllDirectories);
            foreach (var item in files2)
            {
                string filePath = item;
                string fileName = Path.GetFileName(filePath);
                if (SH.ContainsDiacritic(fileName))
                {
                    string dirNameWithoutDiac = SH.TextWithoutDiacritic(fileName);
                    FS.RenameFile(item, dirNameWithoutDiac, co);
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
        FS.MoveFile(item, FS.ChangeFilename(item, dirNameWithoutDiac, false), co);
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
        path = FS.WithoutEndSlash(path);
        string cesta = FS.GetDirectoryName(path);
        string nova = FS.Combine(cesta, newname);

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
            catch
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
        catch
        {
            ThisApp.SetStatus(TypeOfMessage.Error, "File can't be deleted" + ": " + item);
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

        var files = Directory.EnumerateFiles(folder, FS.MascFromExtension(), SearchOption.AllDirectories);
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

        return FS.Combine(slozka, fn + AllStrings.us + dalsi + ext);
    }

    public static void CreateDirectoryIfNotExists(string p)
    {
        MakeUncLongPath(ref p);
        if (!FS.ExistsDirectory(p))
        {
            Directory.CreateDirectory(p);
        }
    }

    public static void SaveStream(string path, Stream s)
    {
        using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
        {
            FS.CopyStream(s, fs);
            fs.Flush();
        }
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

    public static string[] OnlyNamesWithoutExtension(string appendToStart, string[] fullPaths)
    {
        string[] ds = new string[fullPaths.Length];
        for (int i = 0; i < fullPaths.Length; i++)
        {
            ds[i] = appendToStart + Path.GetFileNameWithoutExtension(fullPaths[i]);
        }
        return ds;
    }

    public static bool TryDeleteDirectory(string v)
    {
        try
        {
            Directory.Delete(v, true);
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    /// <summary>
    /// Pokud hledáš metodu ReplacePathToFile, je to tato. Sloučeny protože dělali totéž.
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="changeFolderTo"></param>
    /// <returns></returns>
    public static string ChangeDirectory(string fileName, string changeFolderTo)
    {
        string p = FS.GetDirectoryName(fileName);
        string fn = FS.GetFileName(fileName);

        return FS.Combine(changeFolderTo, fn);
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
        string nova = FS.Combine(cesta, g);

        if (physically)
        {
            try
            {
                if (FS.ExistsFile(nova))
                {
                    File.Delete(nova);
                }
                File.Move(item, nova);
            }
            catch
            {
            }
        }
        return nova;
    }
}
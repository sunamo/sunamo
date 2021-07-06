using sunamo.Essential;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public partial class FS
{
    public static string WithoutEndSlash(string v)
    {
        return WithoutEndSlash(ref v);
    }
    public static string WithoutEndSlash(ref string v)
    {
        v = v.TrimEnd(AllChars.bs);
        return v;
    }

    public static string MascFromExtension(string ext2 = AllStrings.asterisk)
    {
        if (char.IsLetterOrDigit(ext2[0]))
        {
            // For wildcard, dot (simply non letters) include .
            ext2 = "." + ext2;
        }
        if (!ext2.StartsWith("*"))
        {
            ext2 = "*" + ext2;
        }
        if (!ext2.StartsWith("*.") && ext2.StartsWith(AllStrings.dot))
        {
            ext2 = "*." + ext2;
        }

        return ext2;

        //if (ext2 == ".*")
        //{
        //    return "*.*";
        //}


        //var ext = FS.GetExtension(ext2);
        //var fn = FS.GetFileNameWithoutExtension(ext2);
        //// isContained must be true, in BundleTsFile if false masc will be .ts, not *.ts and won't found any file
        //var isContained = AllExtensionsHelper.IsContained(ext);
        //ComplexInfoString cis = new ComplexInfoString(fn);

        ////Already tried
        ////(cis.QuantityLowerChars > 0 || cis.QuantityUpperChars > 0);
        //// (cis.QuantityLowerChars > 0 || cis.QuantityUpperChars > 0); - in MoveClassElementIntoSharedFileUC
        //// !(!ext.Contains("*") && !ext.Contains("?") && !(cis.QuantityLowerChars > 0 || cis.QuantityUpperChars > 0)) - change due to MoveClassElementIntoSharedFileUC

        //// not working for *.aspx.cs
        ////var isNoMascEntered = !(!ext2.Contains("*") && !ext2.Contains("?") && !(cis.QuantityLowerChars > 0 || cis.QuantityUpperChars > 0));
        //// Is succifient one of inner condition false and whole is true

        //var isNoMascEntered = !((ext2.Contains("*") || ext2.Contains("?")));// && !(cis.QuantityLowerChars > 0 || cis.QuantityUpperChars > 0));
        //// From base of logic - isNoMascEntered must be without !. When something another wont working, edit only evaluate of condition above
        //if (!ext.StartsWith("*.") && isNoMascEntered && isContained && ext == FS.GetExtension( ext2)) 
        //{
        //    // Dont understand why, when I insert .aspx.cs, then return just .aspx without *
        //    //if (cis.QuantityUpperChars > 0 || cis.QuantityLowerChars > 0)
        //    //{
        //    //    return ext2;
        //    //}

        //    var vr = AllStrings.asterisk + AllStrings.dot + ext2.TrimStart(AllChars.dot);
        //    return vr;
        //}

        //return ext2; 
    }

    public static List<string> GetFiles(string folderPath, bool recursive)
    {
        return FS.GetFiles(folderPath, FS.MascFromExtension(), recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
    }
    public static string ReadAllText(string filename)
    {
        return File.ReadAllText(filename);
    }

    /// <summary>
    /// A2 is path of target file
    /// </summary>
    /// <param name="item"></param>
    /// <param name="fileTo"></param>
    /// <param name="co"></param>
    public static void MoveFile(string item, string fileTo, FileMoveCollisionOption co)
    {
        if (CopyMoveFilePrepare(ref item, ref fileTo, co))
        {
            try
            {
                item = FS.MakeUncLongPath(item);
                fileTo = FS.MakeUncLongPath(fileTo);

                File.Move(item, fileTo);
            }
            catch (Exception ex)
            {
                ThisApp.SetStatus(TypeOfMessage.Error, item + " : " + ex.Message);
            }
        }
        else
        {

        }
    }

    public static bool CopyMoveFilePrepare(ref string item, ref string fileTo, FileMoveCollisionOption co)
    {
        //var fileTo = fileTo2.ToString();
        item = Consts.UncLongPath + item;
        MakeUncLongPath(ref fileTo);
        FS.CreateUpfoldersPsysicallyUnlessThere(fileTo);

        // Toto tu je důležité, nevím který kokot to zakomentoval
        if (FS.ExistsFile(fileTo))
        {
            if (co == FileMoveCollisionOption.AddFileSize)
            {
                var newFn = FS.InsertBetweenFileNameAndExtension(fileTo, AllStrings.space + FS.GetFileSize(item));
                if (FS.ExistsFile(newFn))
                {
                    File.Delete(item);
                    return true;
                }
                fileTo = newFn;
            }
            else if (co == FileMoveCollisionOption.AddSerie)
            {
                int serie = 1;
                while (true)
                {
                    var newFn = FS.InsertBetweenFileNameAndExtension(fileTo, " (" + serie + AllStrings.rb);
                    if (!FS.ExistsFile(newFn))
                    {
                        fileTo = newFn;
                        break;
                    }
                    serie++;
                }
            }
            else if (co == FileMoveCollisionOption.DiscardFrom)
            {
                // Cant delete from because then is file deleting
                //File.Delete(item);
            }
            else if (co == FileMoveCollisionOption.Overwrite)
            {
                File.Delete(fileTo);
            }
            else if (co == FileMoveCollisionOption.LeaveLarger)
            {
                long fsFrom = FS.GetFileSize(item);
                long fsTo = FS.GetFileSize(fileTo);
                if (fsFrom > fsTo)
                {
                    File.Delete(fileTo);
                }
                else //if (fsFrom < fsTo)
                {
                    File.Delete(item);
                }
            }
            else if (co == FileMoveCollisionOption.DontManipulate)
            {
                if (FS.ExistsFile(fileTo))
                {
                    return false;
                }
            }
        }

        return true;
    }

    public static long GetFileSize(string item)
    {
        FileInfo fi = null;
        try
        {
            fi = new FileInfo(item);
        }
        catch (Exception ex)
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

    public static string InsertBetweenFileNameAndExtension(string orig, string whatInsert)
    {
        return InsertBetweenFileNameAndExtension<string, string>(orig, whatInsert, null);
    }

    /// <summary>
    /// Vrátí vč. cesty
    /// </summary>
    /// <param name="orig"></param>
    /// <param name="whatInsert"></param>
    public static StorageFile InsertBetweenFileNameAndExtension<StorageFolder, StorageFile>(StorageFile orig, string whatInsert, AbstractCatalog<StorageFolder, StorageFile> ac)
    {
        string p = FS.GetDirectoryName(orig.ToString());
        string fn = FS.GetFileNameWithoutExtension(orig.ToString());
        string e = FS.GetExtension(orig.ToString());
        return FS.CiStorageFile<StorageFolder, StorageFile>(FS.Combine(p, fn + whatInsert + e), ac);
    }

    public static StorageFile CiStorageFile<StorageFolder, StorageFile>(string path, AbstractCatalog<StorageFolder, StorageFile> ac)
    {
        if (ac == null)
        {
            return (dynamic)path.ToString();
        }
        return ac.fs.ciStorageFile.Invoke(path);
    }


    public static void CopyAllFilesRecursively(string p, string to, FileMoveCollisionOption co, string contains = null)
    {
        CopyMoveAllFilesRecursively(p, to, co, false, contains, SearchOption.AllDirectories);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="p"></param>
    /// <param name="to"></param>
    /// <param name="co"></param>
    /// <param name="contains"></param>
    public static void CopyAllFiles(string p, string to, FileMoveCollisionOption co, string contains = null)
    {
        CopyMoveAllFilesRecursively(p, to, co, false, contains, SearchOption.TopDirectoryOnly);
    }

    /// <summary>
    /// If want use which not contains, prefix A4 with !
    /// </summary>
    /// <param name="p"></param>
    /// <param name="to"></param>
    /// <param name="co"></param>
    /// <param name="contains"></param>
    private static void CopyMoveAllFilesRecursively(string p, string to, FileMoveCollisionOption co, bool move, string contains, SearchOption so)
    {
        var files = FS.GetFiles(p, AllStrings.asterisk, so);
        if (!string.IsNullOrEmpty(contains))
        {
            foreach (var item in files)
            {
                if (SH.IsContained(item, ref contains))
                {
                    MoveOrCopy(p, to, co, move, item);
                }
            }
        }
    }

    private static void MoveOrCopy(string p, string to, FileMoveCollisionOption co, bool move, string item)
    {
        string fileTo = to + item.Substring(p.Length);
        if (move)
        {
            MoveFile(item, fileTo, co);
        }
        else
        {
            CopyFile(item, fileTo, co);
        }
    }

    /// <summary>
    /// Copy file by ordinal way 
    /// </summary>
    /// <param name="jsFiles"></param>
    /// <param name="v"></param>
    public static void CopyFile(string jsFiles, string v)
    {
        File.Copy(jsFiles, v, true);
    }

    public static void CopyFile(string item, string fileTo2, FileMoveCollisionOption co)
    {
        var fileTo = fileTo2.ToString();
        if (CopyMoveFilePrepare(ref item, ref fileTo, co))
        {
            File.Copy(item, fileTo);
        }
    }

    public static string GetFileNameWithoutExtension(string s)
    {
        return GetFileNameWithoutExtension<string, string>(s, null);
    }

    public static DateTime LastModified(string rel)
    {
        var f = new FileInfo(rel);
        return f.LastWriteTime;

    }

    /// <summary>
    /// Pokud by byla cesta zakončená backslashem, vrátila by metoda FS.GetFileName prázdný řetězec. 
    /// if have more extension, remove just one
    /// </summary>
    /// <param name="s"></param>
    public static StorageFile GetFileNameWithoutExtension<StorageFolder, StorageFile>(StorageFile s, AbstractCatalog<StorageFolder, StorageFile> ac)
    {
        if (ac == null)
        {
            var ss = s.ToString();
            var vr = Path.GetFileNameWithoutExtension(ss.TrimEnd(AllChars.bs));
            var ext = Path.GetExtension(ss).TrimStart(AllChars.dot);

            if (!SH.ContainsOnly(ext, RandomHelper.vsZnakyWithoutSpecial))
            {
                if (ext != string.Empty)
                {
                    return (dynamic)vr + AllStrings.dot + ext;
                }
            }
            return (dynamic)vr;
        }
        else
        {
            ThrowNotImplementedUwp();
            return s;
        }
    }


    public static bool TryDeleteDirectoryOrFile(string v)
    {
        if (!FS.TryDeleteDirectory(v))
        {
            return FS.TryDeleteFile(v);
        }
        return true;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="item"></param>
    public static bool TryDeleteFile(string item)
    {
        // TODO: To all code message logging as here

        try
        {
            // If file won't exists, wont throw any exception
            File.Delete(item);
            return true;
        }
        catch
        {
            ThisApp.SetStatus(TypeOfMessage.Error, SunamoPageHelperSunamo.i18n(XlfKeys.FileCanTBeDeleted) + ": " + item);
            return false;
        }
    }

    public static Func<string, List<string>> InvokePs;

    public static bool TryDeleteDirectory(string v)
    {
        try
        {

            Directory.Delete(v, true);
            return true;
        }
        catch (Exception ex)
        {
             var result = InvokePs(v);
            if (result.Count > 0)
            {
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// ReplaceIncorrectCharactersFile - can specify char for replace with
    /// ReplaceInvalidFileNameChars - all wrong chars skip
    /// </summary>
    /// <param name="filename"></param>
    /// <returns></returns>
    public static string ReplaceInvalidFileNameChars(string filename, params  char[] ch)
    {
        StringBuilder sb = new StringBuilder();
        foreach (var item in filename)
        {
            if (!s_invalidFileNameChars.Contains(item) || ch.Contains(item))
            {
                sb.Append(item);
            }
        }
        return sb.ToString();
    }
    readonly static List<char> invalidFileNameChars = Path.GetInvalidFileNameChars().ToList();
    readonly static List<string> invalidFileNameStrings;
    static FS()
    {
        invalidFileNameStrings = CA.ToListString(invalidFileNameChars);

        s_invalidPathChars = new List<char>(Path.GetInvalidPathChars());
        if (!s_invalidPathChars.Contains(AllChars.slash))
        {
            s_invalidPathChars.Add(AllChars.slash);
        }
        if (!s_invalidPathChars.Contains(AllChars.bs))
        {
            s_invalidPathChars.Add(AllChars.bs);
        }


        s_invalidFileNameChars = new List<char>(invalidFileNameChars);
        s_invalidFileNameCharsString = SH.Join(string.Empty, invalidFileNameChars);
        for (char i = (char)65529; i < 65534; i++)
        {
            s_invalidFileNameChars.Add(i);
        }

        s_invalidCharsForMapPath = new List<char>();
        s_invalidCharsForMapPath.AddRange(s_invalidFileNameChars.ToArray());
        foreach (var item in invalidFileNameChars)
        {
            if (!s_invalidCharsForMapPath.Contains(item))
            {
                s_invalidCharsForMapPath.Add(item);
            }
        }

        s_invalidCharsForMapPath.Remove(AllChars.slash);

        s_invalidFileNameCharsWithoutDelimiterOfFolders = new List<char>(s_invalidFileNameChars.ToArray());

        s_invalidFileNameCharsWithoutDelimiterOfFolders.Remove(AllChars.bs);
        s_invalidFileNameCharsWithoutDelimiterOfFolders.Remove(AllChars.slash);
    }

    private static List<char> s_invalidPathChars = null;

    /// <summary>
    /// Field as string because I dont have array and must every time ToArray() to construct string
    /// </summary>
    public static string s_invalidFileNameCharsString = null;
    public static List<char> s_invalidFileNameChars = null;
    private static List<char> s_invalidCharsForMapPath = null;
    private static List<char> s_invalidFileNameCharsWithoutDelimiterOfFolders = null;

    public async static Task<List<string>> GetFilesMoreMascAsync(string path, string masc, SearchOption searchOption, GetFilesMoreMascArgs e = null)

    {
        if (e == null)
        {
            e = new GetFilesMoreMascArgs();
        }

#if DEBUG
        string d = null;

        if (e.LoadFromFileWhenDebug)
        {
            var s = FS.ReplaceInvalidFileNameChars(SH.Join(path, masc, searchOption));
            d = AppData.ci.GetFile(AppFolders.Cache, "GetFilesMoreMasc" + s + ".txt");

            if (FS.ExistsFile(d))
            {
                return TF.ReadAllLines(path);
            }
        }
#endif

        var c = AllStrings.comma;
        var sc = AllStrings.sc;
        List<string> result = new List<string>();
        List<string> masks = new List<string>();

        if (masc.Contains(c))
        {
            masks.AddRange(SH.Split(masc, c));
        }
        else if (masc.Contains(sc))
        {
            masks.AddRange(SH.Split(masc, sc));
        }
        else
        {
            masks.Add(masc);
        }

        if (e.deleteFromDriveWhenCannotBeResolved)
        {
            foreach (var item in masks)
            {
                try
                {
                    result.AddRange(Directory.GetFiles(path, item, searchOption));
                }
                catch (Exception ex)
                {
                    if (ex.Message.StartsWith(NetFxExceptionsNotTranslateAble.TheNameOfTheFileCannotBeResolvedByTheSystem))
                    {
                        FS.TryDeleteDirectoryOrFile(path);
                    }
                }

            }
        }
        else
        {
            foreach (var item in masks)
            {
                result.AddRange(Directory.GetFiles(path, item, searchOption));
            }
        }

#if DEBUG
        if (e.LoadFromFileWhenDebug)
        {
            if (FS.ExistsFile(d))
            {
                TF.WriteAllLines(d, result);
            }
        }
#endif

        return result;
    }

    public  static List<string> GetFilesMoreMasc(string path, string masc, SearchOption searchOption, GetFilesMoreMascArgs e = null)

    {
        if (e == null)
        {
            e = new GetFilesMoreMascArgs();
        }

#if DEBUG
        string d = null;

        if (e.LoadFromFileWhenDebug)
        {
            var s = FS.ReplaceInvalidFileNameChars(SH.Join(path, masc, searchOption));
            d = AppData.ci.GetFile(AppFolders.Cache, "GetFilesMoreMasc" + s + ".txt");

            if (FS.ExistsFile(d))
            {
                return TF.ReadAllLines(path);
            }
        }
#endif

        var c = AllStrings.comma;
        var sc = AllStrings.sc;
        List<string> result = new List<string>();
        List<string> masks = new List<string>();

        if (masc.Contains(c))
        {
            masks.AddRange(SH.Split(masc, c));
        }
        else if (masc.Contains(sc))
        {
            masks.AddRange(SH.Split(masc, sc));
        }
        else
        {
            masks.Add(masc);
        }

        if (e.deleteFromDriveWhenCannotBeResolved)
        {
            foreach (var item in masks)
            {
                try
                {
                    result.AddRange(Directory.GetFiles(path, item, searchOption));
                }
                catch (Exception ex)
                {
                    if (ex.Message.StartsWith(NetFxExceptionsNotTranslateAble.TheNameOfTheFileCannotBeResolvedByTheSystem))
                    {
                        FS.TryDeleteDirectoryOrFile(path);
                    }
                }

            }
        }
        else
        {
            foreach (var item in masks)
            {
                result.AddRange(Directory.GetFiles(path, item, searchOption));
            }
        }

#if DEBUG
        if (e.LoadFromFileWhenDebug)
        {
            if (FS.ExistsFile(d))
            {
                TF.WriteAllLines(d, result);
            }
        }
#endif

        return result;
    }



    /// <summary>
    /// 
    /// When is occur Access denied exception, use GetFilesEveryFolder, which find files in every folder
    /// A1 have to be with ending backslash
    /// A4 must have underscore otherwise is suggested while I try type true
    /// A2 can be delimited by semicolon. In case more extension use FS.GetFilesOfExtensions
    /// </summary>
    /// <param name="folder"></param>
    /// <param name="mask"></param>
    /// <param name="searchOption"></param>
    public static List<string> GetFiles(string folder2, string mask, SearchOption searchOption, GetFilesArgs getFilesArgs = null)
    {
        if (!FS.ExistsDirectory(folder2) && !folder2.Contains(";"))
        {
            ThisApp.SetStatus(TypeOfMessage.Warning, folder2 + "does not exists");
            return new List<string>();
        }

        if (getFilesArgs == null)
        {
            getFilesArgs = new GetFilesArgs();
        }

        var folders = SH.Split(folder2, AllStrings.sc);
        CA.PostfixIfNotEnding(AllStrings.bs, folders);

        List<string> list = new List<string>();
        foreach (var folder in folders)
        {
            if (!FS.ExistsDirectory(folder))
            {

            }
            else
            {
                //Task.Run<>(async () => await FunctionAsync());
                //var r = Task.Run<List<string>>(async () => await FS.GetFilesMoreMascAsync(folder, mask, searchOption));
                //return r.Result;
                return FS.GetFilesMoreMasc(folder, mask, searchOption);

                //if (mask.Contains(AllStrings.sc))
                //{
                //    //list = new List<string>();
                //    var masces = SH.Split(mask, AllStrings.sc);

                //    foreach (var item in masces)
                //    {
                //        var masc = item;
                //        if (getFilesArgs.useMascFromExtension)
                //        {
                //            masc = FS.MascFromExtension(item);
                //        }

                //        try
                //        {
                //            list.AddRange(FS.GetFilesMoreMasc(folder, masc, searchOption));
                //        }
                //        catch (Exception ex)
                //        {
                //        }

                //    }
                //}
                //else
                //{

                //    try
                //    {
                //        var folder3 = FS.WithoutEndSlash(folder);
                //        DirectoryInfo di = new DirectoryInfo(folder3);
                //        var masc = mask;
                //        if (getFilesArgs.useMascFromExtension)
                //        {
                //            masc = FS.MascFromExtension(mask);
                //        }

                //        var files = di.GetFiles(masc, searchOption);
                //        var files2 = files.Select(d => d.FullName);

                //        //list.AddRange(Directory.GetFiles(folder3, masc, searchOption));
                //        list.AddRange(files2);
                //    }
                //    catch (Exception ex)
                //    {
                //    }
                //}
            }
        }

        CA.ChangeContent(null, list, d => SH.FirstCharLower(d));

        if (getFilesArgs._trimA1)
        {
            foreach (var folder in folders)
            {
                list = CA.ChangeContent(null, list, d => d = d.Replace(folder, ""));
            }

        }

        if (getFilesArgs._trimExt)
        {
            foreach (var folder in folders)
            {
                list = CA.ChangeContent(null, list, d => d = SH.RemoveAfterLast(AllChars.dot, d));
            }

        }

        if (getFilesArgs.excludeFromLocationsCOntains != null)
        {
            // I want to find files recursively
            foreach (var item in getFilesArgs.excludeFromLocationsCOntains)
            {
                CA.RemoveWhichContains(list, item, false);
            }
        }

        Dictionary<string, DateTime> dictLastModified = null;

        bool isLastModifiedFromFn = getFilesArgs.LastModifiedFromFn != null;

        if (getFilesArgs.dontIncludeNewest || (getFilesArgs.byDateOfLastModifiedAsc || isLastModifiedFromFn))
        {
            dictLastModified = new Dictionary<string, DateTime>();
            foreach (var item in list)
            {
                DateTime? dt = null;

                if (isLastModifiedFromFn)
                {
                    dt = getFilesArgs.LastModifiedFromFn(FS.GetFileNameWithoutExtension(item));
                }

                if (!dt.HasValue)
                {
                    dt = FS.LastModified(item);
                }

                dictLastModified.Add(item, dt.Value);
            }
            list = dictLastModified.OrderBy(t => t.Value).Select(r => r.Key).ToList();
        }

        if (getFilesArgs.dontIncludeNewest)
        {

            list.RemoveAt(list.Count - 1);
        }



        if (getFilesArgs.excludeWithMethod != null)
        {
            getFilesArgs.excludeWithMethod.Invoke(list);
        }

        return list;
    }


    /// <summary>
    /// No recursive, all extension
    /// </summary>
    /// <param name="path"></param>
    public static List<string> GetFiles(string path)
    {
        return FS.GetFiles(path, AllStrings.asterisk, SearchOption.TopDirectoryOnly);
    }

    /// <summary>
    /// Get number higher by one from the number filenames with highest value (as 3.txt)
    /// </summary>
    /// <param name="slozka"></param>
    /// <param name="fn"></param>
    /// <param name="ext"></param>
    public static string GetFileSeries(string slozka, string fn, string ext)
    {
        int dalsi = 0;
        var soubory = FS.GetFiles(slozka);
        foreach (string item in soubory)
        {
            int p;
            string withoutFn = SH.ReplaceOnce(FS.GetFileName(item), fn, "");
            string withoutFnAndExt = SH.ReplaceOnce(withoutFn, ext, "");
            withoutFnAndExt = withoutFnAndExt.TrimStart(AllChars.lowbar);
            if (int.TryParse(withoutFnAndExt, out p))
            {
                if (p > dalsi)
                {
                    dalsi = p;
                }
            }
        }

        dalsi++;

        return FS.Combine(slozka, fn + AllStrings.lowbar + dalsi + ext);
    }

    /// <summary>
    /// ALL EXT. HAVE TO BE ALWAYS LOWER
    /// Return in lowercase
    /// </summary>
    /// <param name="v"></param>
    public static string GetExtension(string v, bool returnOriginalCase = false)
    {
        string result = "";
        int lastDot = v.LastIndexOf(AllChars.dot);
        if (lastDot == -1)
        {
            return string.Empty;
        }
        int lastSlash = v.LastIndexOf(AllChars.slash);
        int lastBs = v.LastIndexOf(AllChars.bs);
        if (lastSlash > lastDot)
        {
            return string.Empty;
        }
        if (lastBs > lastDot)
        {
            return string.Empty;
        }
        result = v.Substring(lastDot);

        if (!returnOriginalCase)
        {
            result = result.ToLower();
        }

        if (!IsExtension(result))
        {
            return string.Empty;
        }

        return result;
    }

    public static bool IsExtension(string result)
    {
        if (!SH.ContainsOnly(result.Substring(1), RandomHelper.vsZnakyWithoutSpecial))
        {
            return false;
        }
        return true;
    }

    /// <summary>
    /// If path ends with backslash, FS.GetDirectoryName returns empty string
    /// </summary>
    /// <param name="rp"></param>
    public static string GetFileName(string rp)
    {
        rp = rp.TrimEnd(AllChars.bs);
        int dex = rp.LastIndexOf(AllChars.bs);
        return rp.Substring(dex + 1);
    }

    /// <summary>
    /// Use FirstCharLower instead
    /// </summary>
    /// <param name="result"></param>
    private static string FirstCharUpper(ref string result)
    {
        if (IsWindowsPathFormat(result))
        {
            result = SH.FirstCharUpper(result);
        }
        return result;
    }

    private static string FirstCharLower(ref string result)
    {
        if (IsWindowsPathFormat(result))
        {
            result = SH.FirstCharLower(result);
        }
        return result;
    }

    public static bool IsWindowsPathFormat(string argValue)
    {
        if (string.IsNullOrWhiteSpace(argValue))
        {
            return false;
        }

        bool badFormat = false;

        if (argValue.Length < 3)
        {
            return badFormat;
        }
        if (!char.IsLetter(argValue[0]))
        {
            badFormat = true;
        }



        if (char.IsLetter(argValue[1]))
        {
            badFormat = true;
        }

        if (argValue.Length > 2)
        {
            if (argValue[1] != '\\' && argValue[2] != '\\')
            {
                badFormat = true;
            }
        }

        return !badFormat;
    }



    public static bool ExistsDirectory<StorageFolder, StorageFile>(string item, AbstractCatalog<StorageFolder, StorageFile> ac = null, bool _falseIfContainsNoFile = false)
    {
        if (ac == null)
        {
            return ExistsDirectoryWorker(item, _falseIfContainsNoFile);
        }
        else
        {
            // Call from Apps
            return BTS.GetValueOfNullable(ac.fs.existsDirectory.Invoke(item));
        }
    }

    /// <summary>
    /// Convert to UNC path
    /// </summary>
    /// <param name="item"></param>
    public static bool ExistsDirectoryWorker(string item, bool  _falseIfContainsNoFile = false)
    {
        // Not working, flags from GeoCachingTool wasnt transfered to standard
#if NETFX_CORE
        ThrowExceptions.IsNotAvailableInUwpWindowsStore(type, Exc.CallingMethod(), "  "+-SunamoPageHelperSunamo.i18n(XlfKeys.UseMethodsInFSApps));
#endif
#if WINDOWS_UWP
        ThrowExceptions.IsNotAvailableInUwpWindowsStore(type, Exc.CallingMethod(), "  "+-SunamoPageHelperSunamo.i18n(XlfKeys.UseMethodsInFSApps));
#endif

        if (item == Consts.UncLongPath || item == string.Empty)
        {
            return false;
        }

        var item2 = MakeUncLongPath(item);

        // FS.ExistsDirectory if pass SE or only start of Unc return false
        var result = Directory.Exists(item2);
        if (_falseIfContainsNoFile)
        {
            if (result)
            {
                var f = FS.GetFiles(item, "*", SearchOption.AllDirectories).Count;
                result = f > 0;
            }
        }
        return result;
    }
    public static void MakeUncLongPath<StorageFolder, StorageFile>(ref StorageFile path, AbstractCatalog<StorageFolder, StorageFile> ac)
    {
        if (ac == null)
        {
            path = (StorageFile)(dynamic)MakeUncLongPath(path.ToString());
        }
        else
        {
            ThrowNotImplementedUwp();
        }
        //return path;
    }

    private static void ThrowNotImplementedUwp()
    {
        ThrowExceptions.Custom(Exc.GetStackTrace(), type, Exc.CallingMethod(), SunamoPageHelperSunamo.i18n(XlfKeys.NIUwpSeeMethodForStacktrace));
    }

    public static string MakeUncLongPath(string path)
    {
        return MakeUncLongPath(ref path);
    }

    public static string MakeUncLongPath(ref string path)
    {
        if (!path.StartsWith(Consts.UncLongPath))
        {
            // V ASP.net mi vrátilo u každé directory.exists false. Byl jsem pod ApplicationPoolIdentity v IIS a bylo nastaveno Full Control pro IIS AppPool\DefaultAppPool
#if !ASPNET
            //  asp.net / vps nefunguje, ve windows store apps taktéž, NECHAT TO TRVALE ZAKOMENTOVANÉ
            // v asp.net toto způsobí akorát zacyklení, IIS začne vyhazovat 0xc00000fd, pak už nejde načíst jediná stránka
            //path = Consts.UncLongPath + path;
#endif
        }
        return path;
    }

    /// <summary>
    /// For empty or whitespace return false.
    /// </summary>
    /// <param name="selectedFile"></param>
    public static bool ExistsFile<StorageFolder, StorageFile>(StorageFile selectedFile, AbstractCatalog<StorageFolder, StorageFile> ac = null)
    {
        if (ac == null)
        {
            return ExistsFile(selectedFile.ToString());
        }
        return ac.fs.existsFile.Invoke(selectedFile);
    }

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
        ThrowExceptions.IsNullOrEmpty(Exc.GetStackTrace(), type, "CreateFoldersPsysicallyUnlessThere", "nad", nad);
        ThrowExceptions.IsNotWindowsPathFormat(Exc.GetStackTrace(), type, Exc.CallingMethod(), "nad", nad);

        FS.MakeUncLongPath(ref nad);
        if (FS.ExistsDirectory(nad))
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

                // TODO: Tady to nefunguje pro UWP/UAP apps protoze nemaji pristup k celemu disku. Zjistit co to je UWP/UAP/... a jak v nem ziskat/overit jakoukoliv slozku na disku
                if (FS.ExistsDirectory(nad))
                {
                    break;
                }

                string kopia = nad;
                slozkyKVytvoreni.Add(kopia);
            }

            slozkyKVytvoreni.Reverse();
            foreach (string item in slozkyKVytvoreni)
            {
                string folder = FS.MakeUncLongPath(item);
                if (!FS.ExistsDirectory(folder))
                {
                    Directory.CreateDirectory(folder);
                }
            }
        }
    }

    /// <summary>
    /// Create all upfolders of A1, if they dont exist 
    /// </summary>
    /// <param name="nad"></param>
    public static void CreateUpfoldersPsysicallyUnlessThere<StorageFolder, StorageFile>(StorageFile nad, AbstractCatalog<StorageFolder, StorageFile> ac)
    {
        if (ac == null)
        {
            CreateUpfoldersPsysicallyUnlessThere(nad.ToString());
        }
        else
        {
            CreateFoldersPsysicallyUnlessThereFolder<StorageFolder, StorageFile>(FS.GetDirectoryName<StorageFolder, StorageFile>(nad, ac), ac);
        }
    }

    /// <summary>
    /// Works with and without end backslash
    /// Return with backslash
    /// </summary>
    /// <param name="rp"></param>
    public static StorageFolder GetDirectoryName<StorageFolder, StorageFile>(StorageFile rp2, AbstractCatalog<StorageFolder, StorageFile> ac)
    {
        if (ac != null)
        {
            return ac.fs.getDirectoryName.Invoke(rp2);
        }

        var rp = rp2.ToString();
        return (dynamic)GetDirectoryName(rp);
    }


    public static void CreateFoldersPsysicallyUnlessThereFolder<StorageFolder, StorageFile>(StorageFolder nad, AbstractCatalog<StorageFolder, StorageFile> ac)
    {
        if (ac == null)
        {
            CreateFoldersPsysicallyUnlessThere(nad.ToString());
        }
        else
        {
            ThrowNotImplementedUwp();
        }
    }

    public static bool? ExistsDirectoryNull(string item)
    {
        return ExistsDirectoryNull(item, false);
    }

    public static bool? ExistsDirectoryNull(string item, bool _falseIfContainsNoFile = false)
    {
        return ExistsDirectory(item, _falseIfContainsNoFile);
    }

    public static bool ExistsDirectory(string item, bool _falseIfContainsNoFile = false)
    {
        return ExistsDirectory<string, string>(item, null, _falseIfContainsNoFile);
    }

    /// <summary>
    /// Dont check for size
    /// Into A2 is good put true - when storage was fulled, all new files will be written with zero size. But then failing because HtmlNode as null - empty string as input
    /// But when file is big, like backup of DB, its better false.. Then will be avoid reading whole file to determining their size and totally blocking HW resources on VPS
    /// </summary>
    /// <param name="selectedFile"></param>
    public static bool ExistsFile(string selectedFile, bool falseIfSizeZeroOrEmpty = true)
    {
        if (selectedFile.Contains("VS Code"))
        {

        }

        if (selectedFile == Consts.UncLongPath || selectedFile == string.Empty)
        {
            return false;
        }

        FS.MakeUncLongPath(ref selectedFile);

        var exists = File.Exists(selectedFile);

        if (falseIfSizeZeroOrEmpty)
        {
            if (!exists)
            {
                
                return false;
            }
            else
            {
                var ext = FS.GetExtension(selectedFile).ToLower();
                // Musím to kontrolovat jen když je to tmp, logicky
                if (ext == AllExtensions.tmp)
                {
                    return false;
                }
                else
                {
                    var c = string.Empty;
                    try
                    {
                        c = TF.ReadFile(selectedFile);
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message.StartsWith("The process cannot access the file"))
                        {
                            return true;
                        }

                    }

                    if (c == string.Empty)
                    {
                        // Měl jsem tu chybu že ač exists bylo true, TF.ReadFile selhalo protože soubor neexistoval. 
                        // Vyřešil jsem to kontrolou přípony, snad
                        return false;
                    }
                }
            }
        }
        return exists;
    }

    private static Type type = typeof(FS);
    public static string GetDirectoryName(string rp)
    {

        ThrowExceptions.IsNullOrEmpty(Exc.GetStackTrace(), type, "GetDirectoryName", "rp", rp);
        ThrowExceptions.IsNotWindowsPathFormat(Exc.GetStackTrace(), type, Exc.CallingMethod(), "rp", rp);

        rp = rp.TrimEnd(AllChars.bs);
        int dex = rp.LastIndexOf(AllChars.bs);
        if (dex != -1)
        {
            var result = rp.Substring(0, dex + 1);
            FS.FirstCharLower(ref result);
            return result;
        }
        return "";
    }


    /// <summary>
    /// All occurences Path's method in sunamo replaced
    /// </summary>
    /// <param name="v"></param>
    public static void CreateDirectory(string v)
    {
        try
        {
            Directory.CreateDirectory(v);
        }
        catch (NotSupportedException)
        {


        }
    }

    /// <summary>
    /// Cant return with end slash becuase is working also with files
    /// Use this than FS.Combine which if argument starts with backslash ignore all arguments before this
    /// </summary>
    /// <param name="upFolderName"></param>
    /// <param name="dirNameDecoded"></param>
    public static string Combine(params string[] s)
    {
        return CombineWorker(true, s);
    }

    /// <summary>
    /// Cant return with end slash becuase is working also with files
    /// </summary>
    /// <param name="firstCharLower"></param>
    /// <param name="s"></param>
    private static string CombineWorker(bool firstCharLower, params string[] s)
    {
        s = CA.TrimStart(AllChars.bs, s).ToArray();
        var result = Path.Combine(s);
        if (firstCharLower)
        {
            result = FS.FirstCharLower(ref result);
        }
        else
        {
            result = FS.FirstCharUpper(ref result);
        }
        // Cant return with end slash becuase is working also with files
        //FS.WithEndSlash(ref result);
        return result;
    }

    public static string WithEndSlash(ref string v)
    {
        if (v != string.Empty)
        {
            v = v.TrimEnd(AllChars.bs) + AllChars.bs;
        }
        FirstCharLower(ref v);
        return v;
    }
}

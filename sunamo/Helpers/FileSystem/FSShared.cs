using sunamo.Constants;
using sunamo.Data;
using sunamo.Enums;
using sunamo.Essential;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

public partial class FS
{
    private static List<char> s_invalidPathChars = null;
    private static Type type = typeof(FS);
    /// <summary>
    /// Field as string because I dont have array and must every time ToArray() to construct string
    /// </summary>
    public static string s_invalidFileNameCharsString = null;
    public static List<char> s_invalidFileNameChars = null;
    
    private static List<char> s_invalidCharsForMapPath = null;
    private static List<char> s_invalidFileNameCharsWithoutDelimiterOfFolders = null;


    /// <summary>
    /// 
    /// When is occur Access denied exception, use GetFilesEveryFolder, which find files in every folder
    /// A1 have to be with ending backslash
    /// A4 must have underscore otherwise is suggested while I try type true
    /// A2 can be delimited by semicolon
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

            if (mask.Contains(AllStrings.sc))
            {
                //list = new List<string>();
                var masces = SH.Split(mask, AllStrings.sc);

                foreach (var item in masces)
                {
                    var masc = FS.MascFromExtension(item);
                    try
                    {
                        list.AddRange(Directory.GetFiles(folder, masc, searchOption));
                    }
                    catch (Exception)
                    {


                    }

                }
            }
            else
            {

                try
                {
                    mask = FS.MascFromExtension(mask);
                    list.AddRange(Directory.GetFiles(folder, mask, searchOption));
                }
                catch (Exception ex)
                {
                }
            }
        }
        CA.ChangeContent(list, d => SH.FirstCharLower(d));

        if (getFilesArgs._trimA1)
        {
            foreach (var folder in folders)
            {
                list = CA.ChangeContent(list, d => d = d.Replace(folder, ""));
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

        if (getFilesArgs.dontIncludeNewest || getFilesArgs.byDateOfLastModifiedAsc)
        {
            dictLastModified = new Dictionary<string, DateTime>();
            foreach (var item in list)
            {
                DateTime dt = FS.LastModified(item);

                dictLastModified.Add(item, dt);
            }
            list = dictLastModified.OrderBy(t => t.Value).Select(r => r.Key).ToList();
        }

        if(getFilesArgs.dontIncludeNewest)
        { 
            
            list.RemoveAt(list.Count - 1);
        }



        if (getFilesArgs.excludeWithMethod != null)
        {
            getFilesArgs.excludeWithMethod.Invoke(list);
        }

        return list;
    }



    public static List<string> GetFileNamesWoExtension(List<string> jpgcka)
    {
        var dd = new List<string>(jpgcka.Count);
        for (int i = 0; i < jpgcka.Count; i++)
        {
            dd.Add(FS.GetFileNameWithoutExtension(jpgcka[i]));
        }

        return dd;
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

    /// <summary>
    /// path + file
    /// </summary>
    public static string GetTempFilePath()
    {
        return FS.Combine(System.IO.Path.GetTempPath(), System.IO.Path.GetTempFileName()); 
    }
    public static void MakeUncLongPath<StorageFolder,StorageFile>(ref StorageFile path, AbstractCatalog<StorageFolder, StorageFile> ac)
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
            //path = Consts.UncLongPath + path;
#endif
        }
        return path;
    }

    /// <summary>
    /// Copy file A1 into A2
    /// </summary>
    /// <param name="v"></param>
    /// <param name="nad"></param>
    public static void CopyTo(string v, string nad)
    {
        var fileTo = FS.Combine(nad, FS.GetFileName(v));
        CopyFile(v, fileTo);
    }

    public static bool? ExistsDirectoryNull(string item)
    {
        return ExistsDirectory(item);
    }

    public static bool ExistsDirectory(string item)
    {
        return ExistsDirectory<string, string>(item);
    }

    public static bool ExistsDirectory<StorageFolder, StorageFile>(string item, AbstractCatalog<StorageFolder, StorageFile> ac = null)
    {
        if (ac == null)
        {
            return ExistsDirectoryWorker(item);
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
    public static bool ExistsDirectoryWorker(string item)
    {
        // Not working, flags from GeoCachingTool wasnt transfered to standard
#if NETFX_CORE
        ThrowExceptions.IsNotAvailableInUwpWindowsStore(type, Exc.CallingMethod(), " - use methods in FSApps");
#endif
#if WINDOWS_UWP
        ThrowExceptions.IsNotAvailableInUwpWindowsStore(type, Exc.CallingMethod(), " - use methods in FSApps");
#endif


        if (item == Consts.UncLongPath || item == string.Empty)
        {
            return false;
        }

        var item2 = MakeUncLongPath(item);

      

        // FS.ExistsDirectory if pass SE or only start of Unc return false
        return Directory.Exists(item2);
    }

    public static string GetDirectoryName(string rp)
    {
        
        ThrowExceptions.IsNullOrEmpty(Exc.GetStackTrace(),type, "GetDirectoryName", "rp", rp);
        ThrowExceptions.IsNotWindowsPathFormat(Exc.GetStackTrace(),type, Exc.CallingMethod(), "rp", rp);

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
    /// Works with and without end backslash
    /// Return with backslash
    /// </summary>
    /// <param name="rp"></param>
    public static StorageFolder GetDirectoryName<StorageFolder, StorageFile>(StorageFile rp2, AbstractCatalog<StorageFolder, StorageFile> ac)
    {
        if (ac!= null)
        {
            return ac.fs.getDirectoryName.Invoke(rp2);
        }
        
        var rp = rp2.ToString();
        return (dynamic)GetDirectoryName(rp);
    }

    public static StorageFolder GetDirectoryNameFolder<StorageFolder, StorageFile>(StorageFolder rp2, AbstractCatalog<StorageFolder, StorageFile> ac)
    {
        if (ac != null)
        {
            return ac.fs.getDirectoryNameFolder.Invoke(rp2);
        }
        //ThrowExceptions.Custom(Exc.GetStackTrace(), type, Exc.CallingMethod(), "GetDirectoryName");
        var rp = rp2.ToString();
        return (dynamic)GetDirectoryName(rp);
    }

    private static void ThrowNotImplementedUwp()
    {
        ThrowExceptions.Custom(Exc.GetStackTrace(), type, Exc.CallingMethod(),"NI uwp - see method for stacktrace");
    }

    /// <summary>
    /// Create all upfolders of A1 with, if they dont exist 
    /// </summary>
    /// <param name="nad"></param>
    public static void CreateFoldersPsysicallyUnlessThere(string nad)
    {
        ThrowExceptions.IsNullOrEmpty(Exc.GetStackTrace(),type, "CreateFoldersPsysicallyUnlessThere", "nad", nad);
        ThrowExceptions.IsNotWindowsPathFormat(Exc.GetStackTrace(),type, Exc.CallingMethod(), "nad", nad);

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

    public static void CreateFoldersPsysicallyUnlessThere<StorageFolder, StorageFile>(StorageFile nad, AbstractCatalog<StorageFolder, StorageFile> ac)
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

    /// <summary>
    /// change all first (drive) letter to uppercase
    /// </summary>
    /// <param name="p"></param>
    /// <param name="folderWithProjectsFolders"></param>
    /// <param name="folderWithTemporaryMovedContentWithoutBackslash"></param>
    public static string ReplaceDirectoryThrowExceptionIfFromDoesntExists(string p, string folderWithProjectsFolders, string folderWithTemporaryMovedContentWithoutBackslash)
    {
        p = SH.FirstCharUpper(p);
        folderWithProjectsFolders = SH.FirstCharUpper(folderWithProjectsFolders);
        folderWithTemporaryMovedContentWithoutBackslash = SH.FirstCharUpper(folderWithTemporaryMovedContentWithoutBackslash);

        if (!ThrowExceptions.NotContains(Exc.GetStackTrace(),type, "ReplaceDirectoryThrowExceptionIfFromDoesntExists", p, folderWithProjectsFolders))
        {
            // Here can never accomplish when exc was throwed
            return p;
        }

        // Here can never accomplish when exc was throwed
        return p.Replace(folderWithProjectsFolders, folderWithTemporaryMovedContentWithoutBackslash);
    }

    /// <summary>
    /// Direct edit
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    public static List<string> OnlyNamesWithoutExtension(List<string> p)
    {
        for (int i = 0; i < p.Count; i++)
        {
            p[i] = FS.GetFileNameWithoutExtension(p[i]);
        }
        return p;
    }

    /// <summary>
    /// Vrátí cestu a název souboru s ext
    /// </summary>
    /// <param name="fn"></param>
    /// <param name="path"></param>
    /// <param name="file"></param>
    public static void GetPathAndFileName(string fn, out string path, out string file)
    {
        path = FS.WithEndSlash(FS.GetDirectoryName(fn));
        file = FS.GetFileName(fn);
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
        path = FS.WithEndSlash( FS.GetDirectoryName(fn));
        file = FS.GetFileNameWithoutExtension(fn);
        ext = FS.GetExtension(fn);
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

        if (!SH.ContainsOnly(result.Substring(1), RandomHelper.vsZnakyWithoutSpecial))
        {
            return string.Empty;
        }

        return result;
    }

    /// <summary>
    /// Cant name GetAbsolutePath because The call is ambiguous between the following methods or properties: 'CA.ChangeContent(List<string>, Func<string, string, string>)' and 'CA.ChangeContent(List<string>, Func<string, string>)'
    /// </summary>
    /// <param name="a"></param>
    public static string AbsoluteFromCombinePath(string a)
    {
        var r = Path.GetFullPath((new Uri(a)).LocalPath);
        return r;
    }
    public static string GetAbsolutePath(string dir, string relativePath)
    {
        var ds = AllStrings.ds;
        var dds = AllStrings.dds;

        var dds2 = 0;
        while (true)
        {
            if (relativePath.StartsWith(ds))
            {
                relativePath = relativePath.Substring(ds.Length());
            }
            else if (relativePath.StartsWith(dds))
            {
                dds2++;
                relativePath = relativePath.Substring(dds.Length);
            }
            else
            {
                break;
            }
        }

        var tokens = FS.GetTokens(relativePath);
        tokens = tokens.Skip(dds2).ToList();
        tokens.Insert(0, dir);

        var vr = Combine(tokens.ToArray());
        return vr;
    }

    public static List<string> GetTokens(string relativePath)
    {
        var deli = "";
        if (relativePath.Contains(AllStrings.bs))
        {
            deli = AllStrings.bs;
        }
        else if (relativePath.Contains(AllStrings.slash))
        {
            deli = AllStrings.slash;
        }

        return SH.Split(relativePath, deli);
    }

    public static string WithoutEndSlash(string v)
    {
        return WithoutEndSlash(ref v);
    }
    public static string WithoutEndSlash(ref string v)
    {
        v = v.TrimEnd(AllChars.bs);
        return v;
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

    /// <summary>
    /// Remove path to project folder as are in DefaultPaths.AllPathsToProjects
    /// A2 is here to remove also solution
    /// </summary>
    /// <param name="fullPathOriginalFile"></param>
    /// <param name="combineWithA1"></param>
    /// <param name="empty"></param>
    public static string ReplaceVsProjectFolder(string fullPathOriginalFile, string combineWithA1, string empty)
    {
        fullPathOriginalFile = SH.FirstCharLower(fullPathOriginalFile);
        foreach (var item in DefaultPaths.AllPathsToProjects)
        {
            string replace = FS.WithEndSlash(FS.Combine(item, combineWithA1));
            if (fullPathOriginalFile.StartsWith(replace))
            {
                return fullPathOriginalFile.Replace(replace, empty);
            }
        }
        return fullPathOriginalFile;
    }

   

    /// <summary>
    /// Cant return with end slash becuase is working also with files
    /// </summary>
    /// <param name="s"></param>
    public static string CombineWithoutFirstCharLower(params string[] s)
    {
        return CombineWorker(false, s);
    }

    public static bool IsWindowsPathFormat(string argValue)
    {
        if (string.IsNullOrWhiteSpace(argValue))
        {
            return false;
        }

        bool badFormat = false;

        if (argValue.Length <3)
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

    /// <summary>
    /// Cant return with end slash becuase is working also with files
    /// </summary>
    /// <param name="firstCharLower"></param>
    /// <param name="s"></param>
    private static string CombineWorker(bool firstCharLower, params string[] s)
    {
        s = CA.TrimStart(AllChars.bs, s).ToArray() ;
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

    public static string WithEndSlash(ref string v)
    {
        if (v != string.Empty)
        {
            v = v.TrimEnd(AllChars.bs) + AllChars.bs;
        }
        FirstCharLower(ref v);
        return v;
    }

    public static string WithEndSlash(string v)
    {
        return WithEndSlash(ref v);
    }

    public static void SaveMemoryStream(System.IO.MemoryStream mss, string path)
    {
        SaveMemoryStream<string, string>(mss, path, null);
    }

    public static void SaveMemoryStream<StorageFolder, StorageFile>(System.IO.MemoryStream mss, StorageFile path, AbstractCatalog<StorageFolder, StorageFile> ac)
    {
        
        if (!FS.ExistsFile(path, ac))
        {
            if (ac == null)
            {
                using (System.IO.FileStream fs = new System.IO.FileStream(path.ToString(), System.IO.FileMode.Create, System.IO.FileAccess.Write))
                {
                    byte[] matriz = mss.ToArray();
                    fs.Write(matriz, 0, matriz.Length);
                }
            }
            else
            {
                ThrowExceptions.Custom(Exc.GetStackTrace(), type, Exc.CallingMethod(),"SaveMemoryStream");
            }
        }
    }

    public static void CreateUpfoldersPsysicallyUnlessThere(string nad)
    {
        CreateFoldersPsysicallyUnlessThere(FS.GetDirectoryName(nad));
    }

    /// <summary>
    /// Create all upfolders of A1, if they dont exist 
    /// </summary>
    /// <param name="nad"></param>
    public static void CreateUpfoldersPsysicallyUnlessThere<StorageFolder, StorageFile>(StorageFile nad, AbstractCatalog<StorageFolder, StorageFile> ac)
    {
        if (ac ==null )
        {
            CreateUpfoldersPsysicallyUnlessThere(nad.ToString());
        }
        else
        {
            CreateFoldersPsysicallyUnlessThereFolder<StorageFolder, StorageFile>(FS.GetDirectoryName<StorageFolder, StorageFile>(nad, ac), ac);
        }
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
        return FS.CiStorageFile < StorageFolder, StorageFile >( FS.Combine(p, fn + whatInsert + e), ac);
    }

    public static StorageFolder CiStorageFolder<StorageFolder, StorageFile>(string path, AbstractCatalog<StorageFolder, StorageFile> ac)
    {
        if (ac == null)
        {
            var ps = path.ToString();
            ps = FS.WithEndSlash(ps);
            return (dynamic)ps;
        }
        return ac.fs.ciStorageFolder.Invoke(path);
    }
    public static StorageFile CiStorageFile<StorageFolder, StorageFile>(string path, AbstractCatalog<StorageFolder, StorageFile> ac)
    {
        if (ac == null)
        {
            return (dynamic)path.ToString();
        }
        return ac.fs.ciStorageFile.Invoke(path);
    }

    public static string DeleteWrongCharsInDirectoryName(string p)
    {
        StringBuilder sb = new StringBuilder();
        foreach (char item in p)
        {
            if (!s_invalidPathChars.Contains(item))
            {
                sb.Append(item);
            }
        }
        var result = sb.ToString();
        FS.FirstCharLower(ref result);
        return result;
    }

    public static string DeleteWrongCharsInFileName(string p, bool isPath)
    {
        List<char> invalidFileNameChars2 = null;

        if (isPath)
        {
            invalidFileNameChars2 = s_invalidFileNameCharsWithoutDelimiterOfFolders;
        }
        else
        {
            invalidFileNameChars2 = s_invalidFileNameChars;
        }

        StringBuilder sb = new StringBuilder();
        foreach (char item in p)
        {
            if (!invalidFileNameChars2.Contains(item))
            {
                sb.Append(item);
            }
        }

        var result = sb.ToString();
        FS.FirstCharLower(ref result);
        return result;
    }

    public static bool ContainsInvalidPathCharForPartOfMapPath(string p)
    {
        foreach (var item in s_invalidCharsForMapPath)
        {
            if (p.IndexOf(item) != -1)
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Odstraňuje samozřejmě ve výjimce
    /// </summary>
    /// <param name="path"></param>
    public static void DeleteFileIfExists(string path)
    {
        if (FS.ExistsFile(path))
        {
            File.Delete(path);
        }
    }

    public static List<string> OnlyNames(String[] files2)
    {
        return OnlyNames(CA.ToListString(files2));
    }
    /// <summary>
    /// No direct edit
    /// Returns with extension
    /// POZOR: Na rozdíl od stejné metody v sunamo tato metoda vrací úplně nové pole a nemodifikuje A1
    /// </summary>
    /// <param name="files"></param>
    public static List<string> OnlyNames(List<string> files2)
    {
        List<string> files = new List<string>(files2.Count);
        for (int i = 0; i < files2.Count; i++)
        {
            files.Add(FS.GetFileName(files2[i]));
        }
        return files;
    }
    public static List<string> OnlyNames(string appendToStart, List<string> fullPaths)
    {
        List<string> ds = new List<string>( fullPaths.Count);
        CA.InitFillWith(ds, fullPaths.Count);
        for (int i = 0; i < fullPaths.Count; i++)
        {
            ds[i] = appendToStart + FS.GetFileName(fullPaths[i]);
        }
        return ds;
    }

    public static List<string> GetFolders(string folder)
    {
        return GetFolders(folder, SearchOption.TopDirectoryOnly);
    }

    /// <summary>
    /// Return only subfolder if A3, a1 not include
    /// Must have backslash on end - is folder
    /// </summary>
    /// <param name="folder"></param>
    /// <param name="masc"></param>
    /// <param name="so"></param>
    /// <param name="_trimA1"></param>
    public static List<string> GetFolders(string folder, string masc, SearchOption so, bool _trimA1 = false)
    {
        List<string> dirs = null;
        try
        {
            dirs = Directory.GetDirectories(folder, masc, so).ToList(); 
        }
        catch (Exception ex)
        {

        }

        if (dirs == null)
        {
            return new List<string>();
        }

        CA.ChangeContent(dirs, d => SH.FirstCharLower(d));

        if (_trimA1)
        {
            CA.Replace(dirs, folder, string.Empty);
        }
        // Must have backslash on end - is folder
        CA.PostfixIfNotEnding("\\", dirs);
        return dirs;
    }
    public static List<string> GetFolders(string folder, SearchOption so)
    {
        return GetFolders(folder, AllStrings.asterisk, so);
    }
    public static List<string> GetFolders(string v, string contains)
    {
        var folders = GetFolders(v);
        folders = CA.TrimEnd(folders, AllChars.bs);
        for (int i = folders.Count - 1; i >= 0; i--)
        {
            if (!Wildcard.IsMatch( FS.GetFileName(folders[i]), contains))
            {
                folders.RemoveAt(i);
            }
        }

        return folders;
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

    /// <summary>
    /// A2 is path of target file
    /// </summary>
    /// <param name="item"></param>
    /// <param name="fileTo"></param>
    /// <param name="co"></param>
    public static void CopyFile<StorageFolder, StorageFile>(string item, string fileTo2, FileMoveCollisionOption co, AbstractCatalog<StorageFolder, StorageFile> ac = null)
    {
        if (ac == null)
        {
            CopyFile(item, fileTo2, co);
        }
        else
        {
            ThrowNotImplementedUwp();
        }
    }

    public static bool CopyMoveFilePrepare(ref string item, ref string fileTo, FileMoveCollisionOption co)
    {
        //var fileTo = fileTo2.ToString();
        item = Consts.UncLongPath + item;
        MakeUncLongPath(ref fileTo);
        FS.CreateUpfoldersPsysicallyUnlessThere(fileTo);
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
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// A1,2 isnt  working like ref
    /// </summary>
    /// <typeparam name="StorageFolder"></typeparam>
    /// <typeparam name="StorageFile"></typeparam>
    /// <param name="item"></param>
    /// <param name="fileTo"></param>
    /// <param name="co"></param>
    /// <param name="ac"></param>
    public static bool CopyMoveFilePrepare<StorageFolder, StorageFile>(ref StorageFile item, ref StorageFile fileTo, FileMoveCollisionOption co, AbstractCatalog<StorageFolder, StorageFile> ac)
    {
        if (ac == null)
        {
            var item2 = item.ToString();
            var fileTo2 = fileTo.ToString();
            return CopyMoveFilePrepare(ref item2, ref fileTo2, co);
        }

        ThrowNotImplementedUwp();
            MakeUncLongPath(ref item, ac);
            MakeUncLongPath<StorageFolder, StorageFile>(ref fileTo, ac);
            FS.CreateUpfoldersPsysicallyUnlessThere<StorageFolder, StorageFile>(fileTo, ac);
            if (FS.ExistsFile<StorageFolder, StorageFile>(fileTo, ac))
            {
            }
        return false;
    }

    public static bool CopyMoveFilePrepare<StorageFolder, StorageFile>(ref string item, ref StorageFile fileTo2, FileMoveCollisionOption co, AbstractCatalog<StorageFolder, StorageFile> ac)
    {
        if (ac == null)
        {
            var fileTo = fileTo2.ToString();
            return CopyMoveFilePrepare(ref item, ref fileTo, co);
        }

        ThrowNotImplementedUwp();
        return false;
    }

    public static string ChangeExtension(string item, string newExt, bool physically)
    {
        if (UH.HasHttpProtocol(item))
        {
            return UH.ChangeExtension(item, FS.GetExtension(item, true), newExt);
        }

        string cesta = FS.GetDirectoryName(item);
        string fnwoe = FS.GetFileNameWithoutExtension(item);
        string nova = FS.Combine(cesta, fnwoe + newExt);

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
        FirstCharLower(ref nova);
        return nova;
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
        catch (NotSupportedException ex)
        {

            
        }
    }
  
    public static void CreateDirectory(string v, DirectoryCreateCollisionOption whenExists, SerieStyle serieStyle)
    {
        if (FS.ExistsDirectory(v))
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
                    string newFn = nameWithoutSerie + " (" + serie + AllStrings.rb;
                    if (!FS.ExistsDirectory(newFn))
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
                ThrowExceptions.NotImplementedCase(Exc.GetStackTrace(),type, "CreateDirectory", whenExists);
            }
        }
        else
        {
            Directory.CreateDirectory(v);
        }
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
    public static string GetNameWithoutSeries(string p, bool path, out bool hasSerie, SerieStyle serieStyle, out int serie)
    {
        serie = -1;
        hasSerie = false;
        string dd = FS.WithEndSlash(FS.GetDirectoryName(p));
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

            if (p.Contains(AllStrings.lowbar))
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

            if (g[g.Length - 3] == AllChars.dash)
            {
                serie = int.Parse(g.Substring(g.Length - 2));
                g = g.Substring(0, g.Length - 3);
            }
            else if (g[g.Length - 2] == AllChars.dash)
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
            int dex = g.LastIndexOf(AllChars.lowbar);
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

    public static string MascFromExtension(string ext2 = AllStrings.asterisk)
    {
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
    public static List<string> GetFiles(string folderPath, string masc)
    {
        return FS.GetFiles(folderPath, masc, SearchOption.TopDirectoryOnly);
    }
    public static List<string> GetFiles(string folderPath, bool recursive)
    {
        return FS.GetFiles(folderPath, FS.MascFromExtension(), recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
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
    /// It's always recursive
    /// </summary>
    /// <param name="folder"></param>
    /// <param name="mask"></param>
    public static List<string> GetFoldersEveryFolder(string folder, string mask, GetFilesArgs a = null)
    {
        if (a == null)
        {
            a = new GetFilesArgs();
        }

        List<string> list = new List<string>();

        GetFoldersEveryFolder(folder, mask, list);

        if (a._trimA1)
        {
                list = CA.ChangeContent(list, d => d = d.Replace(folder, ""));
        }
        if (a.excludeFromLocationsCOntains != null)
        {
            // I want to find files recursively
            foreach (var item in a.excludeFromLocationsCOntains)
            {
                CA.RemoveWhichContains(list, item, false);
            }
        }

        return list;
    }

    private static void GetFoldersEveryFolder(string folder, string mask, List<string> list)
    {
        try
        {
            var folders = Directory.GetDirectories(folder, mask, SearchOption.TopDirectoryOnly);
            list.AddRange(folders);

            foreach (var item in folders)
            {
                GetFoldersEveryFolder(item, mask, list);
            }
        }
        catch (Exception ex)
        {
            // Not throw exception, it's probably Access denied  on Documents and Settings etc
            //ThrowExceptions.Custom(Exc.GetStackTrace(), type, Exc.CallingMethod(),"GetFoldersEveryFolder with path: " + folder, ex);
        }
    }

    /// <summary>
    /// In item1 is all directories, in Item2 all files
    /// </summary>
    /// <param name="folder"></param>
    /// <param name="ask"></param>
    /// <param name="searchOption"></param>
    /// <param name="_trimA1"></param>
    public static List<string> GetFilesEveryFolder(string folder, string mask, SearchOption searchOption, bool _trimA1 = false)
    {
        List<string> list = new List<string>(); ;
        List<string> dirs = null;

        try
        {
            dirs = GetFoldersEveryFolder(folder, "*").ToList();
        }
        catch (Exception ex)
        {
            ThrowExceptions.Custom(Exc.GetStackTrace(), type, Exc.CallingMethod(),"GetFiles with path: " + folder);
        }

        dirs.Insert(0, folder);
        foreach (var item in dirs)
        {
            try
            {
                list.AddRange(Directory.GetFiles(item, mask, SearchOption.TopDirectoryOnly));
            }
            catch (Exception ex)
            {
                // Not throw exception, it's probably Access denied on Documents and Settings etc
                //ThrowExceptions.FileSystemException(Exc.GetStackTrace(),type, Exc.CallingMethod(), ex);
            }
        }

        CA.ChangeContent(list, d => SH.FirstCharLower(d));

        if (_trimA1)
        {
            list = CA.ChangeContent(list, d => d = d.Replace(folder, ""));
        }
        return list;
    }

    

    

    /// <summary>
    /// A2 is path of target file
    /// </summary>
    /// <param name="item"></param>
    /// <param name="fileTo"></param>
    /// <param name="co"></param>
    public static void MoveFile(string item, string fileTo, FileMoveCollisionOption co)
    {
        if( CopyMoveFilePrepare(ref item, ref fileTo, co))
        {
            try
            {
                File.Move(item, fileTo);
            }
            catch (Exception ex)
            {
                ThisApp.SetStatus(TypeOfMessage.Error, item + " : " + ex.Message);
                
            }
        }
    }

public static byte[] StreamToArrayBytes(System.IO.Stream stream)
    {
        if (stream == null)
        {
            return new byte[0];
        }

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

    public static string GetFileNameWithoutExtension(string s)
    {
        return GetFileNameWithoutExtension<string, string>(s, null);
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
                return s;
            }
            return (dynamic)vr;
        }
        else
        {
            ThrowNotImplementedUwp();
            return s;
        }
    }

public static string AddExtensionIfDontHave(string file, string ext)
    {
        // For *.* and git paths {dir}/*
        if (file[file.Length - 1] == AllChars.asterisk)
        {
            return file;
        }
        if (GetExtension(file) == string.Empty)
        {
            return file + ext;
        }

        return file;
    }

/// <summary>
    /// Vratí bez cesty, pouze název souboru
    /// </summary>
    /// <param name="orig"></param>
    /// <param name="whatInsert"></param>
    public static string InsertBetweenFileNameAndExtension2(string orig, string whatInsert)
    {
        string fn = FS.GetFileNameWithoutExtension(orig);
        string e = FS.GetExtension(orig);
        return FS.Combine(fn + whatInsert + e);
    }

/// <summary>
    /// In key are just filename, in value full path to all files 
    /// </summary>
    /// <param name="linesFiles"></param>
    /// <param name="searchOnlyWithExtension"></param>
    public static Dictionary<string, List<string>> GetDictionaryByFileNameWithExtension(List<string> files)
    {
        Dictionary<string, List<string>> result = new Dictionary<string, List<string>>();
        foreach (var item in files)
        {
            string filename = FS.GetFileName(item);
            DictionaryHelper.AddOrCreateIfDontExists<string, string>(result, filename, item);
        }

        return result;
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

    public static string ChangeFilename(string item, string newFileName, bool physically)
    {
        string cesta = FS.GetDirectoryName(item);
        string nova = FS.Combine(cesta, newFileName);

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


/// <summary>
/// Zmeni nazev souboru na A2
/// Pro A3 je výchozí z minulosti true - jakoby s false se chovala metoda ReplaceFileName
/// Pokud nechci nazev souboru uplne menit, ale pouze v nem neco nahradit, pouziva se metoda ReplaceInFileName
/// </summary>
/// <param name="item"></param>
/// <param name="newFileName"></param>
/// <param name="onDrive"></param>
    public static string ChangeFilename<StorageFolder, StorageFile>(StorageFile item, string newFileName, bool physically, AbstractCatalog<StorageFolder, StorageFile> ac)
    {
        if (ac == null)
        {
            ChangeFilename(item.ToString(), newFileName, physically);
        }
        ThrowNotImplementedUwp();
        return null;
    }

/// <summary>
    /// A2 true - bs to slash. false - slash to bs
    /// </summary>
    /// <param name="path"></param>
    /// <param name="v"></param>
    public static string Slash(string path, bool slash)
    {
        string result = null;
        if (slash)
        {
            result = SH.ReplaceAll2(path, AllStrings.slash, AllStrings.bs);
        }
        else
        {
            result = SH.ReplaceAll2(path, AllStrings.bs, AllStrings.slash);
        }

        FS.FirstCharLower(ref result);
        return result;
    }

/// <summary>
    /// Pokusí se max. 10x smazat soubor A1, pokud se nepodaří, GF, jinak GT
    /// </summary>
    /// <param name="item"></param>
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
            string withoutFn = SH.ReplaceOnce(item, fn, "");
            string withoutFnAndExt = SH.ReplaceOnce(withoutFn, ext, "");
            if (int.TryParse(FS.GetFileNameWithoutExtension(withoutFnAndExt), out p))
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
    /// 
    /// </summary>
    /// <param name="item"></param>
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

private static long ConvertToSmallerComputerUnitSize(long value, ComputerSizeUnits b, ComputerSizeUnits to)
    {
        return ConvertToSmallerComputerUnitSize(value, b, to);
    }
private static double ConvertToSmallerComputerUnitSize(double value, ComputerSizeUnits b, ComputerSizeUnits to)
    {
        if (to == ComputerSizeUnits.Auto)
        {
            ThrowExceptions.Custom(Exc.GetStackTrace(), type, Exc.CallingMethod(),"Byl specifikov\u00E1n v\u00FDstupn\u00ED ComputerSizeUnit, nem\u016F\u017Eu toto nastaven\u00ED zm\u011Bnit");
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
        file = FS.GetFileNameWithoutExtension(fn);
        ext = Path.GetExtension(fn);
    }

/// <summary>
    /// 
    /// </summary>
    /// <param name="filter"></param>
    public static string RepairFilter(string filter)
    {
        if (!filter.Contains(AllStrings.verbar))
        {
            filter = filter.TrimStart(AllChars.asterisk);
            return AllStrings.asterisk + filter + AllStrings.verbar + AllStrings.asterisk + filter;
        }
        return filter;
    }

public static void CreateFileIfDoesntExists(string path)
    {
        CreateFileIfDoesntExists<string, string>(path, null);
    }
public static void CreateFileIfDoesntExists<StorageFolder, StorageFile>(StorageFile path, AbstractCatalog<StorageFolder, StorageFile> ac)
    {
        if (!FS.ExistsFile<StorageFolder, StorageFile>(path, ac))
        {
            TF.WriteAllBytes<StorageFolder, StorageFile>(path, CA.ToList<byte>(), ac);
        }
    }

    /// <summary>
    /// ReplaceIncorrectCharactersFile - can specify char for replace with
    /// ReplaceInvalidFileNameChars - all wrong chars skip
    /// </summary>
    /// <param name="filename"></param>
    /// <returns></returns>
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

/// <summary>
    /// Replacement can be configured with replaceIncorrectFor
    /// 
    /// </summary>
    /// <param name="p"></param>
    public static string ReplaceIncorrectCharactersFile(string p)
    {
        string t = p;
        foreach (char item in invalidFileNameChars)
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
                    sb.Append(AllStrings.space);
                }
            }
            t = sb.ToString();
        }
        return t;
    }
/// <summary>
    /// ReplaceIncorrectCharactersFile - can specify char for replace with
    /// ReplaceInvalidFileNameChars - all wrong chars skip
    /// 
    /// A2 - can specify more letter in one string
    /// A3 is applicable only for A2. In general is use replaceIncorrectFor
    /// </summary>
    /// <param name="p"></param>
    /// <param name="replaceAllOfThisByA3"></param>
    /// <param name="replaceForThis"></param>
    public static string ReplaceIncorrectCharactersFile(string p, string replaceAllOfThisByA3, string replaceForThis)
    {
        string t = p;
        foreach (char item in invalidFileNameChars)
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
                    sb.Append(replaceForThis);
                }
            }
            t = sb.ToString();
        }
        if (!string.IsNullOrEmpty(replaceAllOfThisByA3))
        {
            foreach (char item in replaceAllOfThisByA3)
            {
                t = SH.ReplaceAll(t, replaceForThis, item.ToString());
            }
            
        }
        return t;
    }
/// <summary>
    /// Pro odstranění špatných znaků odstraní všechny výskyty A2 za mezery a udělá z více mezere jediné
    /// </summary>
    /// <param name="p"></param>
    /// <param name="replaceAllOfThisThen"></param>
    public static string ReplaceIncorrectCharactersFile(string p, string replaceAllOfThisThen)
    {
        string replaceFor = AllStrings.space;
        string t = p;
        foreach (char item in invalidFileNameChars)
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
                    sb.Append(replaceFor);
                }
            }
            t = sb.ToString();
        }
        if (!string.IsNullOrEmpty(replaceAllOfThisThen))
        {
            t = SH.ReplaceAll(t, replaceFor, replaceAllOfThisThen);
            t = SH.ReplaceAll(t, replaceFor, AllStrings.doubleSpace);
        }
        return t;
    }

/// <summary>
    /// either A1 or A2 can be null
    /// When A2 is null, will get from file path A1
    /// </summary>
    /// <param name="item"></param>
    /// <param name="folder"></param>
    /// <param name="insert"></param>
    public static string InsertBetweenFileNameAndPath(string item, string folder, string insert)
    {
        if (folder == null)
        {
            folder = FS.GetDirectoryName(item);
        }
        var outputFolder = FS.Combine(folder, insert);
        FS.CreateFoldersPsysicallyUnlessThere(outputFolder);
        return FS.Combine(outputFolder, FS.GetFileName(item));
    }

/// <summary>
    /// Pokud hledáš metodu ReplacePathToFile, je to tato. Sloučeny protože dělali totéž.
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="changeFolderTo"></param>
    public static string ChangeDirectory(string fileName, string changeFolderTo)
    {
        string p = FS.GetDirectoryName(fileName);
        string fn = FS.GetFileName(fileName);
        return FS.Combine(changeFolderTo, fn);
    }
}
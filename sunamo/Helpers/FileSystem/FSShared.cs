using sunamo.Constants;
using sunamo.Data;
using sunamo.Enums;
using sunamo.Values;
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
    private static Type s_type = typeof(FS);

    private static List<char> s_invalidFileNameChars = null;
    private static List<char> s_invalidCharsForMapPath = null;
    private static List<char> s_invalidFileNameCharsWithoutDelimiterOfFolders = null;

    static FS()
    {
        s_invalidPathChars = new List<char>(Path.GetInvalidPathChars());
        if (!s_invalidPathChars.Contains(AllChars.slash))
        {
            s_invalidPathChars.Add(AllChars.slash);
        }
        if (!s_invalidPathChars.Contains(AllChars.bs))
        {
            s_invalidPathChars.Add(AllChars.bs);
        }
        s_invalidFileNameChars = new List<char>(Path.GetInvalidFileNameChars());
        for (char i = (char)65529; i < 65534; i++)
        {
            s_invalidFileNameChars.Add(i);
        }

        s_invalidCharsForMapPath = new List<char>();
        s_invalidCharsForMapPath.AddRange(s_invalidFileNameChars.ToArray());
        foreach (var item in Path.GetInvalidFileNameChars())
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
    /// For empty or whitespace return false.
    /// </summary>
    /// <param name="selectedFile"></param>
    /// <returns></returns>
    public static bool ExistsFile(string selectedFile)
    {
        if (selectedFile == Consts.UncLongPath || selectedFile == string.Empty)
        {
            return false;
        }

        return File.Exists(selectedFile);
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

    /// <summary>
    /// Convert to UNC path
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public static bool ExistsDirectory(string item)
    {
        if (item == Consts.UncLongPath || item == string.Empty)
        {
            return false;
        }
        // FS.ExistsDirectory if pass SE or only start of Unc return false
        return Directory.Exists(MakeUncLongPath(item));
    }



    /// <summary>
    /// Works with and without end backslash
    /// Return with backslash
    /// </summary>
    /// <param name="rp"></param>
    /// <returns></returns>
    public static string GetDirectoryName(string rp)
    {
        ThrowExceptions.IsNullOrEmpty(s_type, "GetDirectoryName", "rp", rp);

        rp = rp.TrimEnd(AllChars.bs);
        int dex = rp.LastIndexOf(AllChars.bs);
        if (dex != -1)
        {
            return rp.Substring(0, dex + 1);
        }
        return "";
    }

    /// <summary>
    /// Create all upfolders of A1 with, if they dont exist 
    /// </summary>
    /// <param name="nad"></param>
    public static void CreateFoldersPsysicallyUnlessThere(string nad)
    {
        ThrowExceptions.IsNullOrEmpty(s_type, "CreateFoldersPsysicallyUnlessThere", "nad", nad);

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
    /// Create all upfolders of A1 with, if they dont exist 
    /// A2 zdali je A1 folder. Pokud je A1 soubor, dej false.
    /// Tuto metodu s parametrem false můžeš používat stejně jako CreateUpfoldersPsysicallyUnlessThere s 1 parametrem, tato ale bude o něco málo rychlejší.
    /// </summary>
    /// <param name="nad"></param>
    public static void CreateFoldersPsysicallyUnlessThere(string nad, bool isFolder)
    {
        if (FS.ExistsDirectory(nad))
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
                nad = FS.GetDirectoryName(nad);

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
                if (!FS.ExistsDirectory(item))
                {
                    Directory.CreateDirectory(item);
                }
            }
        }
    }

    /// <summary>
    /// change all first (drive) letter to uppercase
    /// </summary>
    /// <param name="p"></param>
    /// <param name="folderWithProjectsFolders"></param>
    /// <param name="folderWithTemporaryMovedContentWithoutBackslash"></param>
    /// <returns></returns>
    public static string ReplaceDirectoryThrowExceptionIfFromDoesntExists(string p, string folderWithProjectsFolders, string folderWithTemporaryMovedContentWithoutBackslash)
    {
        p = SH.FirstCharUpper(p);
        folderWithProjectsFolders = SH.FirstCharUpper(folderWithProjectsFolders);
        folderWithTemporaryMovedContentWithoutBackslash = SH.FirstCharUpper(folderWithTemporaryMovedContentWithoutBackslash);

        if (!ThrowExceptions.NotContains(s_type, "ReplaceDirectoryThrowExceptionIfFromDoesntExists", p, folderWithProjectsFolders))
        {
            // Here can never accomplish when exc was throwed
            return p;
        }

        // Here can never accomplish when exc was throwed
        return p.Replace(folderWithProjectsFolders, folderWithTemporaryMovedContentWithoutBackslash);
    }

    public static List<string> OnlyNamesWithoutExtension(List<string> p)
    {
        for (int i = 0; i < p.Count; i++)
        {
            p[i] = Path.GetFileNameWithoutExtension(p[i]);
        }
        return p;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    public static string[] OnlyNamesWithoutExtension(string[] p)
    {
        return OnlyNamesWithoutExtension(new List<string>(p)).ToArray();
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
    /// ALL EXT. HAVE TO BE ALWAYS LOWER
    /// Return in lowercase
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    public static string GetExtension(string v)
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
        result = v.Substring(lastDot).ToLower();

        return result;
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
    /// <returns></returns>
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
    /// Use this than FS.Combine which if argument starts with backslash ignore all arguments before this
    /// </summary>
    /// <param name="upFolderName"></param>
    /// <param name="dirNameDecoded"></param>
    /// <returns></returns>
    public static string Combine(params string[] s)
    {
        return CombineWorker(true, s);
    }

    public static string CombineWithoutFirstCharLower(params string[] s)
    {
        return CombineWorker(false, s);
    }

    private static string CombineWorker(bool firstCharLower, params string[] s)
    {
        s = CA.TrimStart(AllChars.bs, s);
        var result = Path.Combine(s);
        if (firstCharLower)
        {
            result = SH.FirstCharLower(result);
        }
        else
        {
            result = SH.FirstCharUpper(result);
        }
        return result;
    }

    public static string WithEndSlash(ref string v)
    {
        if (v != string.Empty)
        {
            v = v.TrimEnd(AllChars.bs) + AllChars.bs;
        }
        return v;
    }
    public static string WithEndSlash(string v)
    {
        return WithEndSlash(ref v);
    }

    public static void SaveMemoryStream(System.IO.MemoryStream mss, string path)
    {
        path = path.Replace(@"\", AllStrings.bs);
        if (!FS.ExistsFile(path))
        {
            using (System.IO.FileStream fs = new System.IO.FileStream(path, System.IO.FileMode.Create, System.IO.FileAccess.Write))
            {
                byte[] matriz = mss.ToArray();
                fs.Write(matriz, 0, matriz.Length);
            }
        }
    }

    /// <summary>
    /// Create all upfolders of A1, if they dont exist 
    /// </summary>
    /// <param name="nad"></param>
    public static void CreateUpfoldersPsysicallyUnlessThere(string nad)
    {
        CreateFoldersPsysicallyUnlessThere(FS.GetDirectoryName(nad));
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
        return FS.Combine(p, fn + whatInsert + e);
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
        return sb.ToString();
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

        return sb.ToString();
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

    public static string[] OnlyNames(string[] files2)
    {
        return OnlyNames(CA.ToListString(files2)).ToArray();
    }
    /// <summary>
    /// No direct edit
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
    public static string[] OnlyNames(string appendToStart, string[] fullPaths)
    {
        string[] ds = new string[fullPaths.Length];
        for (int i = 0; i < fullPaths.Length; i++)
        {
            ds[i] = appendToStart + Path.GetFileName(fullPaths[i]);
        }
        return ds;
    }

    public static List<string> GetFolders(string folder)
    {
        return GetFolders(folder, SearchOption.TopDirectoryOnly);
    }

    /// <summary>
    /// Return only subfolder if A3, a1 not include
    /// </summary>
    /// <param name="folder"></param>
    /// <param name="masc"></param>
    /// <param name="so"></param>
    /// <param name="_trimA1"></param>
    /// <returns></returns>
    public static List<string> GetFolders(string folder, string masc, SearchOption so, bool _trimA1 = false)
    {
        var dirs = Directory.GetDirectories(folder, masc, so).ToList();
        if (_trimA1)
        {
            CA.Replace(dirs, folder, string.Empty);
        }
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
            if (!Path.GetFileName(folders[i]).Contains(contains))
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
    /// <returns></returns>
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

    /// <summary>
    /// A2 is path of target file
    /// </summary>
    /// <param name="item"></param>
    /// <param name="fileTo"></param>
    /// <param name="co"></param>
    public static void CopyFile(string item, string fileTo, FileMoveCollisionOption co)
    {
        CopyMoveFilePrepare(ref item, ref fileTo, co);
        File.Copy(item, fileTo);
    }

    public static void CopyMoveFilePrepare(ref string item, ref string fileTo, FileMoveCollisionOption co)
    {
        item = Consts.UncLongPath + item;
        MakeUncLongPath(ref fileTo);
        FS.CreateUpfoldersPsysicallyUnlessThere(fileTo);
        if (FS.ExistsFile(fileTo))
        {
            if (co == FileMoveCollisionOption.AddFileSize)
            {
                string newFn = FS.InsertBetweenFileNameAndExtension(fileTo, AllStrings.space + FS.GetFileSize(item));
                if (FS.ExistsFile(newFn))
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
                    string newFn = FS.InsertBetweenFileNameAndExtension(fileTo, " (" + serie + AllStrings.rb);
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
                return;
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
                    return;
                }
            }
        }
    }

    public static string ChangeExtension(string item, string newExt, bool physically)
    {
        string cesta = FS.GetDirectoryName(item);
        string fnwoe = Path.GetFileNameWithoutExtension(item);
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
                ThrowExceptions.NotImplementedCase(s_type, "CreateDirectory");
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
    /// <returns></returns>
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
            int dex = g.LastIndexOf(AllChars.us);
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

    public static string MascFromExtension(string ext = AllStrings.asterisk)
    {
        if (!ext.StartsWith("*."))
        {
            return AllStrings.asterisk + AllStrings.dot + ext.TrimStart(AllChars.dot);
        }
        return ext;
    }

    public static List<string> GetFiles(string folderPath, bool recursive)
    {
        return FS.GetFiles(folderPath, FS.MascFromExtension(), recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
    }
    /// <summary>
    /// No recursive, all extension
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static List<string> GetFiles(string path)
    {
        return FS.GetFiles(path, AllStrings.asterisk, SearchOption.TopDirectoryOnly);
    }
    /// <summary>
    /// A1 have to be with ending backslash
    /// A4 must have underscore otherwise is suggested while I try type true
    /// </summary>
    /// <param name="folder"></param>
    /// <param name="mask"></param>
    /// <param name="searchOption"></param>
    /// <returns></returns>
    public static List<string> GetFiles(string folder, string mask, SearchOption searchOption, bool _trimA1 = false)
    {
        List<string> list = null;
        try
        {
            list = new List<string>(Directory.GetFiles(folder, mask, searchOption));
        }
        catch (Exception ex)
        {
            throw new Exception("GetFiles with path: " + folder, ex);
        }
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
        CopyMoveFilePrepare(ref item, ref fileTo, co);
        File.Move(item, fileTo);
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

/// <summary>
    /// Pokud by byla cesta zakončená backslashem, vrátila by metoda Path.GetFileName prázdný řetězec. 
    /// if have more extension, remove just one
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static string GetFileNameWithoutExtension(string s)
    {
        return Path.GetFileNameWithoutExtension(s.TrimEnd(AllChars.bs));
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
    /// <returns></returns>
    public static string InsertBetweenFileNameAndExtension2(string orig, string whatInsert)
    {
        string fn = Path.GetFileNameWithoutExtension(orig);
        string e = FS.GetExtension(orig);
        return FS.Combine(fn + whatInsert + e);
    }

/// <summary>
    /// In key are just filename, in value full path to all files 
    /// </summary>
    /// <param name="linesFiles"></param>
    /// <param name="searchOnlyWithExtension"></param>
    /// <returns></returns>
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

    internal static void CopyAllFiles(string p, string to, FileMoveCollisionOption co, string contains = null)
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
    private static void CopyMoveAllFilesRecursively(string p, string to, FileMoveCollisionOption co, bool move, string contains, SearchOption so )
    {
        string[] files = Directory.GetFiles(p, AllStrings.asterisk, so);
        foreach (var item in files)
        {
            if (!string.IsNullOrEmpty(contains))
            {
                bool negation = SH.IsNegation(ref contains);

                if (negation && item.Contains(contains))
                {
                    continue;
                }
                else if (!negation && !item.Contains(contains))
                {
                    continue;
                }
            }
            MoveOrCopy(p, to, co, move, item);
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
}
using sunamo.Constants;
using sunamo.Data;
using sunamo.Values;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public partial class FS
{
    static List<char> invalidPathChars = null;
    static Type type = typeof(FS);

    static List<char> invalidFileNameChars = null;
    static List<char> invalidCharsForMapPath = null;
    static List<char> invalidFileNameCharsWithoutDelimiterOfFolders = null;

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
            //path = Consts.UncLongPath + path;
        }
        return path;
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

    internal static Task DeleteFile(StorageFile t)
    {
        throw new NotImplementedException();
    }

    internal async static System.Threading.Tasks.Task<StorageFile> GetStorageFile(StorageFolder folder, string v)
    {
        return new StorageFile(folder.fullPath, v);
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
            int lastDot = v.LastIndexOf('.');
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

public static string WithoutEndSlash(string v)
        {
            return WithoutEndSlash(ref v);
        }
public static string WithoutEndSlash(ref string v)
        {
            return v.TrimEnd(AllChars.bs);
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
            string replace = FS.WithEndSlash( FS.Combine(item, combineWithA1));
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
            s = CA.TrimStart(AllChars.bs, s);
            return SH.FirstCharLower( Path.Combine(s));
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
            path = path.Replace("\\\\", "\\");
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
                if (!invalidPathChars.Contains(item))
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

    public static List<string> GetFolders(string folder, string masc, SearchOption so)
    {
        return Directory.GetDirectories(folder, masc, so).ToList();
    }
public static List<string> GetFolders(string folder, SearchOption so)
        {
        return GetFolders(folder, "*", so);
        }
public static List<string> GetFolders(string v, string contains)
        {
            var folders = GetFolders(v);
            folders = CA.TrimEnd(folders, '\\');
            for (int i = folders.Count - 1; i >= 0; i--)
            {
                if (!Path.GetFileName(folders[i]).Contains(contains))
                {
                    folders.RemoveAt(i);
                }
            }

            return folders;
        }
}
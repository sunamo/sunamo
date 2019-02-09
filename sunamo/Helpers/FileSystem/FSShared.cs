using sunamo.Values;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

public partial class FS
{
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
}
using sunamo;
using sunamo.Data;
using System.Collections.Generic;
using System.IO;
using System.Linq;
/// <summary>
/// Připomíná práci s databází - k označení složek se používají čísla int
/// </summary>
public class FileIndex
{
    public List<FileItem> files = new List<FileItem>();
    List<FolderItem> folders = new List<FolderItem>();
    int actualFolderID = -1;
    /// <summary>
    /// Všechny složky tak jak byly postupně přidávany do metody AddFolderRecursively
    /// Pokud chceš zjistit např. soubory ve složce, musíš na tuto kolekci použít IndexOf a používat pak zjištěný index
    /// </summary>
    public static List<string> directories = new List<string>();
    public static List<string> relativeDirectories = new List<string>();

    public IEnumerable<FolderItem> GetFoldersWithName(int[] prohledavatSlozky, string name)
    {
        if (prohledavatSlozky == null)
        {
            return folders.Where(c => c.Name == name);
        }
        return folders.Where(c => c.Name == name).Where(d => prohledavatSlozky.Contains(d.IDParent));
    }

    /// <summary>
    /// A1 musí být cesta zakončená slashem
    /// </summary>
    /// <param name="folder"></param>
    /// <param name="relativeDirectoryName"></param>
    public void AddFolderRecursively(string folder, bool relativeDirectoryName)
    {
        folder = sunamo.FS.WithEndSlash(folder);
        actualFolderID++;
        directories.Add(folder);
        string[] dirs  = Directory.GetDirectories(folder, "*", SearchOption.AllDirectories);
        List<string> fils = new List<string>();

        foreach (var item in dirs)
        {
            folders.Add(GetFolderItem(item));
            AddFilesFromFolder(folder, relativeDirectoryName, item);
        }

        AddFilesFromFolder(folder, relativeDirectoryName, sunamo.FS.WithoutEndSlash( folder));
    }

    private void AddFilesFromFolder(string folder, bool relativeDirectoryName, string item)
    {
        var files2 = Directory.GetFiles(item, "*.*", SearchOption.TopDirectoryOnly);
        files2.ToList().ForEach(c => files.Add(GetFileItem(c, folder, relativeDirectoryName)));
    }

    private FolderItem GetFolderItem(string p)
    {
        FolderItem fi = new FolderItem();
        fi.IDParent = actualFolderID;
        fi.Name = sunamo.FS.GetFileName(p);
        fi.Path = sunamo.FS.GetDirectoryName(p);
        return fi;
    }

    private FileItem GetFileItem(string p, string basePath, bool relativeDirectoryName)
    {
        FileItem fi = new FileItem();
        fi.IDDirectory = folders.Count;
        fi.IDParent = actualFolderID;
        fi.Name = sunamo.FS.GetFileName(p);
        fi.Path = sunamo.FS.GetDirectoryName(p);
        if (relativeDirectoryName)
        {
            string relDirName = p.Replace(basePath, "");
            if (!relativeDirectories.Contains(relDirName))
            {
                relativeDirectories.Add(relDirName);
                // Počítá se od 1
                fi.IDRelativeDirectory = relativeDirectories.Count;
            }
            else
            {
                fi.IDRelativeDirectory = relativeDirectories.IndexOf(relDirName) + 1;
            }
        }
        return fi;
    }

    public void Nuke()
    {
        folders.Clear();
        files.Clear();
    }

    public int GetIndexOfFolder(FolderItem item)
    {
        return folders.IndexOf(item);
    }

    public IEnumerable<FileItem> GetFilesInFolder(int p)
    {
        return files.Where(c => c.IDDirectory == p);
    }

    public IEnumerable<FileItem> GetFilesInRelativeFolder(int p)
    {
        return files.Where(c => c.IDRelativeDirectory == p);
    }

    /// <summary>
    /// Process recursively A1 - for every folder one object FileIndex in output
    /// 
    /// </summary>
    /// <param name="folders"></param>
    /// <returns></returns>
    public static Dictionary<string, FileIndex> IndexFolders(IEnumerable<string> folders)
    {
        Dictionary<string, FileIndex> vr = new Dictionary<string, FileIndex>();
        foreach (var item in folders)
        {
            FileIndex fi = new FileIndex();
            fi.AddFolderRecursively(item, true);
            vr.Add(item, fi);
        }
        return vr;
    }

    /// <summary>
    /// Use relative path to file to find id directory and insert bot to A2
    /// A1 - used to make relative file paths from A2
    /// 
    /// A3 - key is relative file path, value is index of relative directory
    /// A4 - relative paths to files which is used to fill A3. no change
    /// </summary>
    /// <param name="folderOfSolution"></param>
    /// <param name="fi"></param>
    /// <param name="relativeFilePathForEveryColumn"></param>
    /// <param name="filesFromAllFoldersUniqueRelative"></param>
    public static void AggregateFilesFromAllFolders(string folderOfSolution, FileIndex fi, Dictionary<string, int> relativeFilePathForEveryColumn, List<string> filesFromAllFoldersUniqueRelative)
    {
        foreach (var item2 in fi.files)
        {
            string relativeFilePath = (item2.Path + item2.Name).Replace(folderOfSolution, "");

            if (!relativeFilePathForEveryColumn.ContainsKey(relativeFilePath))
            {
                int relativeDirectoryId = filesFromAllFoldersUniqueRelative.IndexOf(relativeFilePath);
                relativeFilePathForEveryColumn.Add(relativeFilePath, relativeDirectoryId);
            }
        }
    }

    /// <summary>
    /// Tato metoda má za úkol vytvořit matici ze souborů v A1, kde každý soubor bude v daném sloupci dle A2
    /// Load size of files from disc
    /// </summary>
    /// <param name="files"></param>
    /// <param name="relativeFilePathForEveryColumn"></param>
    /// <returns></returns>
    public static CheckBoxData<TWithSize<string>>[,] ExistsFilesOnDrive(Dictionary<string, FileIndex> files, Dictionary<string, int> relativeFilePathForEveryColumn)
    {
        int columns = relativeFilePathForEveryColumn.Count;
        CheckBoxData<TWithSize<string>>[,] vr = new CheckBoxData<TWithSize<string>>[files.Count, columns];
        int r = -1;

        int? first = null;

        foreach (var item in files)
        {
            r++;
            var fi = item.Value;
            List<long> fileSize = new List<long>(columns);
            List<int> added = new List<int>();

            for (int c = 0; c < fi.files.Count; c++)
            {
                var file = fi.files[c];

                string relativeFilePath = (file.Path + file.Name).Replace(item.Key, ""); //FileIndex.relativeDirectories[file.IDRelativeDirectory - 1];
                int columnToInsert = relativeFilePathForEveryColumn[relativeFilePath];
                string fullFilePath = file.Path + file.Name;
                if (File.Exists(fullFilePath))
                {
                    long l2 = sunamo.FS.GetFileSize(fullFilePath);
                    vr[r, columnToInsert] = new CheckBoxData<TWithSize<string>> { t = new TWithSize<string> { t = fullFilePath, size = l2 } };
                }
                else
                {
                    vr[r, columnToInsert] = null;
                }
            }
        }

        return vr;
    }

    /// <summary>
    /// Check (or uncheck) all in columns by filesize
    /// </summary>
    /// <param name="allRows"></param>
    /// <returns></returns>
    public static CheckBoxData<TWithSize<string>>[,] CheckVertically(CheckBoxData<TWithSize<string>>[,] allRows)
    {
        int columns = allRows.GetLength(1);
        int rows = allRows.GetLength(0);

        // List all files
        for (int c = 0; c < columns; c++)
        {
            Dictionary<int, long> fileSize = new Dictionary<int, long>();
            List<long> fileSize2 = new List<long>();

            for (int r = 0; r < rows; r++)
            {
                CheckBoxData<TWithSize<string>> cbd = allRows[r, c];
                if (cbd != null)
                {
                    fileSize.Add(r, cbd.t.size);
                    fileSize2.Add(cbd.t.size);
                }
            }

            fileSize2.Sort();

            long min = fileSize2[0];
            long max = fileSize2[fileSize2.Count - 1];

            if (fileSize.Count > 1)
            {
                if (min == max)
                {
                    TickIfItIsForDelete(allRows, 0, c, fileSize, min, max, false);
                    for (int r = 1; r < rows; r++)
                    {
                        TickIfItIsForDelete(allRows, r, c, fileSize, min, max, true);
                    }
                }
                else
                {
                    for (int r = 0; r < rows; r++)
                    {
                        TickIfItIsForDelete(allRows, r, c, fileSize, min, max, null);
                    }
                }

            }
            else
            {
                // Maybe leave file with zero size?
                    TickIfItIsForDelete(allRows, 0, c, fileSize, min, max, false);   
            }
        }
        return allRows;
    }

    /// <summary>
    /// Check CheckBox by specified parameter row A2 and column A3
    /// If size is A5 min, check. If A6 max, uncheck. Or none of this, set null. This behaviour can be changed setted A7 forceToAll
    /// </summary>
    /// <param name="allRows"></param>
    /// <param name="row"></param>
    /// <param name="column"></param>
    /// <param name="fileSize"></param>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <param name="forceToAll"></param>
    private static void TickIfItIsForDelete( CheckBoxData<TWithSize<string>>[,] allRows, int row, int column, Dictionary<int,long> fileSize, long min, long max, bool? forceToAll)
    {
            CheckBoxData<TWithSize<string>> cbd = allRows[row, column];
            if (cbd != null)
            {
                long filSiz = fileSize[row];
                if (filSiz == -1)
                {

                }
                else if (filSiz == max)
                {
                    if (forceToAll.HasValue)
                    {
                        cbd.tick = forceToAll.Value;
                    }
                    else
                    {
                        cbd.tick = false;
                    }

                }
                else if (filSiz == min)
                {
                    if (forceToAll.HasValue)
                    {
                        cbd.tick = forceToAll.Value;
                    }
                    else
                    {
                        cbd.tick = true;
                    }

                }
                else
                {
                    if (forceToAll.HasValue)
                    {
                        cbd.tick = forceToAll.Value;
                    }
                    else
                    {
                        cbd.tick = null;
                    }
                }
            }
        
    }

    public class FileItem : IFSItem
{
    string name = null;
    public string Name
    {
        get
        {
            return name;
        }
        set
        {
            name = value;
        }
    }

    string path = null;
    public string Path
    {
        get
        {
            return path;
        }
        set
        {
            path = value;
        }
    }

    int iDParent = -1;
    public int IDParent
    {
        get
        {
            return iDParent;
        }
        set
        {
            iDParent = value;
        }
    }

    int iDDirectory = -1;
    public int IDDirectory
    {
        get
        {
            return iDDirectory;
        }
        set
        {
            iDDirectory = value;
        }
    }

    long length = -1;
    public long Length
    {
        get
        {
            return length;
        }
        set
        {
            length = value;
        }
    }

    /// <summary>
    /// POZOR: Počítá se od 1
    /// Relativní cesta k souboru (na začátku chybí bázová třída)
    /// </summary>
    public int IDRelativeDirectory { get; set; }
}

    public class FolderItem : IFSItem
{
    string name = null;
    public string Name
    {
        get => name;
        set => name = value;
    }

    string path = null;
    public string Path
    {
        get =>  path;
        set => path = value;
    }


    int iDParent = -1;
    public int IDParent
    {
        get =>  iDParent;
        set => iDParent = value;
    }
    long length = -1;
    public long Length
    {
        get
        {
            return length;
        }
        set
        {
            length = value;
        }
    }
    bool hasFolderSubfolder = false;
    public bool HasFolderSubfolder
    {
        get
        {
            return hasFolderSubfolder;
        }
        set
        {
            hasFolderSubfolder = value;
        }
    }
}
}

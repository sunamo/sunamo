using System;
using System.Collections.Generic;
using System.IO;
using System.Text;


public class FSAbstract<StorageFolder,StorageFile>
{
    public ExistsDirectory existsDirectory;
    /// <summary>
    /// From storage folder and name
    /// </summary>
    public Func<StorageFolder, string, StorageFile> getStorageFile;
    public Func<StorageFolder, string, bool, List<StorageFile>> getFiles;
    public Func<StorageFile, bool> existsFile;
    /// <summary>
    ///  get StorageFile from path
    /// </summary>
    public Func<string, StorageFile> ciStorageFile;
    public Func<StorageFile, string> pathFromStorageFile;
}
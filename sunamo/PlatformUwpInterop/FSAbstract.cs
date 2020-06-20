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
    /// <summary>
    /// folder,mask,recursive : List<StorageFile>
    /// </summary>
    public Func<StorageFolder, string, bool, List<StorageFile>> getFiles;
    //GetFilesOfExtensionCaseInsensitiveRecursively
    public Func<StorageFile, bool> existsFile;
    /// <summary>
    ///  get StorageFile from path
    /// </summary>
    public Func<string, StorageFile> ciStorageFile;
    public Func<string, StorageFolder> ciStorageFolder;
    public Func<StorageFile, string> pathFromStorageFile;
    public Func<StorageFile, Stream> openStreamForReadAsync;
    public Func<StorageFolder, StorageFolder, bool> isFoldersEquals;
    public Func<StorageFile, string> storageFilePath;
    public Func< StorageFolder, StorageFolder> getDirectoryNameFolder;
    public Func<StorageFile, StorageFolder> getDirectoryName;
    public Func<StorageFile, string> getFileName;
    public Func<StorageFolder, string, List<StorageFile>> getFilesOfExtensionCaseInsensitiveRecursively;

    
}
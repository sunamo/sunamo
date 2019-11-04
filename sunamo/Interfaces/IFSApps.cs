//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Threading.Tasks;

//public interface IFSApps<StorageFolder, StorageFile, IRandomAccessStreamWithContentType>
//    {
//    Task DeleteFile(StorageFile item);
//    Task DeleteFiles(List<StorageFile> tt);
//    Task DeleteFiles(StorageFolder tt);
//    Task DeleteFilesWithSize(StorageFolder task, ulong v);
//    Task<bool> ExistsDirectory(StorageFolder rootFolder);
//    Task<bool> ExistsFile(StorageFile sf);
//    Task<bool> ExistsFile(StorageFolder sf, string fileName);
//    Task<StorageFile> ExistsFileCreateIfNot(StorageFolder sf, string fileName);
//    Task<StorageFolder> ExistsFolderCreateIfNot(StorageFolder sf, string folderName);
//    string GetFileNameWithoutExtension(StorageFile storageFile);
//    Task<ulong> GetFileSize(StorageFile sf);
//    Task<IReadOnlyList<StorageFile>> GetFilesOfExtensionCaseInsensitive(StorageFolder sf, string ext);
//    Task<List<StorageFile>> GetFilesOfExtensionCaseInsensitiveRecursively(StorageFolder sf, string ext);
//    Task<string> GetPathIfNotExists(StorageFolder sf, string fileName);
//    Task<StorageFile> GetStorageFile(sunamo.Data.StorageFile file);
//    Task<StorageFile> GetStorageFile(StorageFolder sf, string soubor);
//    Task<StorageFile> GetStorageFile(StorageFolder sf, string soubor, bool odstranitPrazdneSlozky);
//    Task<StorageFolder> GetStorageFolder(StorageFolder sf, string slozka2, bool odstranitPrazdneSlozky, bool forceReturn);
//    Task<IRandomAccessStreamWithContentType> OpenReadAsync(StorageFile item);
//    Task<StorageFile> RandomFileFromFolder(string v);
//    Task<List<StorageFolder>> RecursivelyReturnAllFolders(string nameOfFolder, StorageFolder storageFolder);
//    Task<StorageFile> TryGetStorageFile(StorageFolder sf, string fileName);
//}


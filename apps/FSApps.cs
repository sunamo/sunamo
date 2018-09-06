using sunamo;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;

//namespace apps
//{
    /// <summary>
    /// Must be FSApps because is used many methods from FSApps
    /// </summary>
    public class FSApps //: sunamo.FS
    {
        public static FileExceptions folderExc = FileExceptions.None;
        public static FileExceptions fileExc = FileExceptions.None;
        public static StorageFile file = null;
        public static StorageFolder folder = null;

        public async static Task DeleteFiles(StorageFolder tt)
        {
            foreach (var item in await tt.GetFilesAsync())
            {
                await DeleteFile(item);
            }
        }

        public async static Task DeleteFiles(List<StorageFile> tt)
        {
            foreach (var item in tt)
            {
                await DeleteFile(item);
            }
        }

        public static string GetFileNameWithoutExtension(StorageFile storageFile)
        {
            return storageFile.Name.Substring(0, storageFile.Name.Length - storageFile.FileType.Length);
        }

        /// <summary>
        /// Pokud se nepodaří smazat, nevyhodí žádnou výjimku
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async static Task DeleteFile(StorageFile item)
        {
            try
            {
                await item.DeleteAsync(StorageDeleteOption.PermanentDelete);
            }
            catch (Exception ex)
            {
            }
        }

        public async static Task DeleteFilesWithSize(StorageFolder task, ulong v)
        {
            var q = await task.GetFilesAsync(Windows.Storage.Search.CommonFileQuery.DefaultQuery);
            foreach (var item in q)
            {
                if (await FSApps.GetFileSize(item) == v)
                {
                    try
                    {
                        await DeleteFile(item);
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }

        public async static Task<IReadOnlyList<StorageFile>> GetFilesOfExtensionCaseInsensitive(StorageFolder sf, string ext)
        {
            ext = ext.ToLower();
            List<StorageFile> vr = new List<StorageFile>();
            var dd = await sf.GetFilesAsync(Windows.Storage.Search.CommonFileQuery.DefaultQuery);
            foreach (var item in dd)
            {
                if (item.Name.ToLower().EndsWith(ext))
                {
                    vr.Add(item);
                }
            }
            return vr;
        }

        public async static Task<List<StorageFile>> GetFilesOfExtensionCaseInsensitiveRecursively(StorageFolder sf, string ext)
        {
            List<StorageFile> files = new List<StorageFile>();
            files = await FSApps.GetFilesRek(sf, files);
            for (int i = files.Count - 1; i >= 0; i--)
            {
                if (!files[i].Name.ToLower().EndsWith(ext))
                {
                    files.RemoveAt(i);
                }
            }
            return files;
        }

        private async static Task< List<StorageFile>> GetFilesRek(StorageFolder folder, List<StorageFile> files)
        {
            StorageFolder fold = folder;

            var items = await fold.GetItemsAsync();

            foreach (var item in items)
            {
                if (item.GetType() == typeof(StorageFile))
                {
                    files.Add(item as StorageFile);
                }
                else if (item is StorageFolder)
                {
                    files = await GetFilesRek(item as StorageFolder, files);
                }
            }

            return files;
        }

        public static async Task<StorageFile> ExistsFileCreateIfNot(StorageFolder sf, string fileName)
        {
            file = await sf.CreateFileAsync(fileName, CreationCollisionOption.OpenIfExists);
            return file;
        }

        public async static Task<StorageFile> RandomFileFromFolder(string v)
        {
            StorageFolder sf = await StorageFolder.GetFolderFromPathAsync(v);
            var v2 = await sf.GetFilesAsync(Windows.Storage.Search.CommonFileQuery.DefaultQuery);
            return RandomHelper.RandomElementOfCollectionT<StorageFile>(v2);
        }

        public static async Task<StorageFolder> ExistsFolderCreateIfNot(StorageFolder sf, string folderName)
        {
            folder = await sf.CreateFolderAsync(folderName, CreationCollisionOption.OpenIfExists);
            return folder;
        }

        /// <summary>
        /// ZMĚNA
        /// Vytvořím nebo získám soubor a vrátím zda jeho velikost není 0
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public async static Task<bool> ExistsFile(StorageFolder sf, string fileName)
        {
            StorageFile file = await TryGetStorageFile(sf, fileName);
#if DEBUG
            Debug.WriteLine("Existuje soubor " + fileName + ": " + (file != null).ToString());
#endif
            if (file != null)
            {
                // The file exists, "file" variable contains a reference to it.
                return true;
            }
            else
            {
                // The file doesn't exist.
                return false;
            }


        }

        public async static Task<bool> ExistsFile(StorageFile sf)
        {
            var bp = await sf.GetBasicPropertiesAsync();
            return bp.Size != 0;
        }

        public static async Task<StorageFile> TryGetStorageFile(StorageFolder sf, string fileName)
        {
            return await sf.TryGetItemAsync(fileName) as StorageFile;
        }

        public static async Task<string> GetPathIfNotExists(StorageFolder sf, string fileName)
        {
            bool exists = await FSApps.ExistsFile(sf, fileName);
            StorageFile file = await sf.CreateFileAsync(fileName, CreationCollisionOption.OpenIfExists) as StorageFile;
            if (await GetFileSize(file) != 0)
            {
                return null;
            }
            else
            {
                await FSApps.DeleteFile(file);
                if (!exists)
                {
                    return file.Path;
                }

            }
            return null;
        }

        public async static Task<ulong> GetFileSize(StorageFile sf)
        {
            var bp = await sf.GetBasicPropertiesAsync();
            return bp.Size;
        }

        public async static Task<IRandomAccessStreamWithContentType> OpenReadAsync(StorageFile item)
        {
            //bool found = false;
            IRandomAccessStreamWithContentType tem = null;
            try
            {
                tem = await item.OpenReadAsync();
                fileExc = FileExceptions.None;
                //found = true;
            }
            catch (System.IO.FileNotFoundException)
            {
                fileExc = FileExceptions.FileNotFound;

            }
            catch (UnauthorizedAccessException)
            {
                fileExc = FileExceptions.UnauthorizedAccess;

            }
            catch
            {
                fileExc = FileExceptions.General;

            }

            return tem;
        }

        public async static Task<List<StorageFolder>> RecursivelyReturnAllFolders(string nameOfFolder, StorageFolder storageFolder)
        {
            List<StorageFolder> vr = new List<StorageFolder>();
            await RecursivelyReturnAllFolders(nameOfFolder, storageFolder, vr);
            return vr;
        }

        private async static Task RecursivelyReturnAllFolders(string nameOfFolder, StorageFolder storageFolder, List<StorageFolder> vr)
        {
            IReadOnlyList<StorageFolder> sfs = await storageFolder.GetFoldersAsync();
            foreach (StorageFolder item in sfs)
            {
                if (item.Name == nameOfFolder)
                {
                    vr.Add(item);
                }
                await RecursivelyReturnAllFolders(nameOfFolder, item, vr);
            }
        }

        /// <summary>
        /// Pokud chceš jen vytvořit složku, použij sf.CreateFolderAsync kde můžeš specifikovat co se má stát když složka již bude existovat
        /// Vytvoří složku A2 v A1. Pokud se nepodaří vytvořit, smaže všechny soubory ze složky A2(nekontroluje ale zda existuje a nesmaže samotnou složku)
        /// Pokud A3, odstraním prázdné složky z A1
        /// Pokud A4, vrátím objekt StorageFolder z A2 bez ohledu na jeho hodnotu nebo zda složka existuje
        /// </summary>
        /// <param name="sf"></param>
        /// <param name="slozka2"></param>
        /// <param name="odstranitPrazdneSlozky"></param>
        /// <param name="forceReturn"></param>
        /// <returns></returns>
        public async static Task<StorageFolder> GetStorageFolder(StorageFolder sf, string slozka2, bool odstranitPrazdneSlozky, bool forceReturn)
        {
            // Vytvořím složku A2 v A1, když se podaří, vrátím nově vytvořenou složku
            slozka2 = SH.RemoveLastCharIfIs(slozka2, '\\');
            StorageFolder vr = await sf.CreateFolderAsync(slozka2, CreationCollisionOption.OpenIfExists);
            if (await ExistsFolder(vr))
            {
                return vr;
            }
            // Odstraním všechny soubory z A2
            int pocetSlozek = SH.SplitNone(slozka2, "\\").Length;
            await FSApps.DeleteFiles(vr);

            // Pokud A3, odstraním prázdné složky z A1
            if (odstranitPrazdneSlozky)
            {
                string slozka = FS.GetDirectoryName(vr.Path);

                while (pocetSlozek > 1)
                {
                    pocetSlozek--;
                    StorageFolder mayDelete = await StorageFolder.GetFolderFromPathAsync(slozka);
                    if ((await mayDelete.GetFoldersAsync()).Count == 0)
                    {
                        if ((await mayDelete.GetFilesAsync()).Count == 0)
                        {
                            slozka = FS.GetDirectoryName(mayDelete.Path);
                            await FSApps.DeleteFiles(mayDelete);
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

            // Pokud A4, vrátím objekt StorageFolder bez ohledu na jeho hodnotu nebo zda složka existuje
            if (forceReturn)
            {
                return vr;
            }
            return null;
        }

        /// <summary>
        /// Vrátí mi zda v této složce je alespoň jeden soubor a/nebo adresář.
        /// </summary>
        /// <param name="mayDelete"></param>
        /// <returns></returns>
        private async static Task<bool> ExistsFolder(StorageFolder mayDelete)
        {
            if ((await mayDelete.GetFoldersAsync()).Count == 0)
            {
                if ((await mayDelete.GetFilesAsync()).Count == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            return true;
        }

        public async static Task<StorageFile> GetStorageFile(StorageFolder sf, string soubor)
        {
            return await GetStorageFile(sf, soubor, false);
        }

        public async static Task<StorageFile> GetStorageFile(StorageFolder sf, string soubor, bool odstranitPrazdneSlozky)
        {
            StorageFile vr = await sf.CreateFileAsync(soubor, CreationCollisionOption.OpenIfExists);
            if (await ExistsFile(sf, soubor))
            {
                return vr;
            }

            int pocetSlozek = SH.SplitNone(soubor, "\\").Length;
            await FSApps.DeleteFile(vr);
            if (odstranitPrazdneSlozky)
            {
                string slozka = FS.GetDirectoryName(vr.Path);

                while (pocetSlozek > 1)
                {
                    pocetSlozek--;
                    StorageFolder mayDelete = await StorageFolder.GetFolderFromPathAsync(slozka);
                    if ((await mayDelete.GetFoldersAsync()).Count == 0)
                    {
                        if ((await mayDelete.GetFilesAsync()).Count == 0)
                        {
                            slozka = FS.GetDirectoryName(mayDelete.Path);
                            await FSApps.DeleteFiles(mayDelete);
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
            return null;
        }
    }
//}

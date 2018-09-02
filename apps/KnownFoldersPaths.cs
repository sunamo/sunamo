using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace apps
{
    /// <summary>
    /// Zde to vždy vytváří nějaký soubor, pro jednoduché zjištění použij třídu KnownFolders
    /// </summary>
    public class KnownFoldersPaths
    {
        private async Task<StorageFolder> GetStorageFileOfKnownFolder(StorageFolder storageFolder)
        {
            StorageFolder sf = await FSApps.GetStorageFolder(storageFolder, "a", true, true);
            // Již s koncovým lomítkem na konci vrací
            return sf;// +"\\";

        }

        public async Task<StorageFolder> MediaServerDevices()
        {
            return await GetStorageFileOfKnownFolder(KnownFolders.MediaServerDevices);
        }

        public async Task<StorageFolder> MusicLibrary()
        {
            return await GetStorageFileOfKnownFolder(KnownFolders.MusicLibrary);
        }

        public async Task<StorageFolder> PicturesLibrary()
        {
            return await GetStorageFileOfKnownFolder(KnownFolders.PicturesLibrary);
        }

        public async Task<StorageFolder> RemovableDevices()
        {
            return await GetStorageFileOfKnownFolder(KnownFolders.RemovableDevices);
        }

        public async Task<StorageFolder> VideosLibrary()
        {
            return await GetStorageFileOfKnownFolder(KnownFolders.VideosLibrary);
        }
    }
}

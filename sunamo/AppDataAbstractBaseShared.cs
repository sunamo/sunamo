using sunamo.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace sunamo
{
    public abstract partial class AppDataAbstractBase<StorageFolder, StorageFile> : AppDataBase<StorageFolder, StorageFile>
    {
        public abstract StorageFolder GetRootFolder();


        protected abstract void SaveFile(string content, StorageFile sf);



        public abstract StorageFolder GetFolder(AppFolders af);

        /// <summary>
        /// Pokud rootFolder bude SE nebo null, G false, jinak vrátí zda rootFolder existuej ve FS
        /// </summary>
        /// <returns></returns>
        public abstract bool IsRootFolderOk();
        public abstract void AppendToFile(AppFolders af, string file, string value);
        public abstract void AppendToFile(string value, StorageFile file);

        /// <summary>
        /// G path file A2 in AF A1.
        /// Automatically create upfolder if there dont exists.
        /// </summary>
        /// <param name = "af"></param>
        /// <param name = "file"></param>
        /// <returns></returns>
        public abstract StorageFile GetFile(AppFolders af, string file);
        public abstract bool IsRootFolderNull();
        public abstract StorageFolder GetSunamoFolder();
        public abstract StorageFolder GetCommonSettings(string key);

        public abstract void SetCommonSettings(string key, string value);

        public abstract StorageFile GetFileCommonSettings(string key);
    }
}

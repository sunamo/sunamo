
using System;
using System.Collections.Generic;
using System.Text;

namespace sunamo
{
    /// <summary>
    /// Another methods which is adecvate only in desktop apps
    /// AppDataAppsAbstractBase - methods which are applied only on UAP
    /// AppDataAbstractBase (this) - methods which are applied only on desktop
    /// </summary>
    /// <typeparam name="StorageFolder"></typeparam>
    /// <typeparam name="StorageFile"></typeparam>
    public abstract partial class AppDataAbstractBase<StorageFolder, StorageFile> : AppDataBase<StorageFolder, StorageFile>
    {
        public abstract StorageFolder GetRootFolder();
        

        protected abstract void SaveFile(string content, StorageFile sf);
        


        

        /// <summary>
        /// Pokud rootFolder bude SE nebo null, G false, jinak vrátí zda rootFolder existuej ve FS
        /// </summary>
        public abstract bool IsRootFolderOk();
        public abstract void AppendToFile(AppFolders af, string file, string value);
        public abstract void AppendToFile(string value, StorageFile file);

        /// <summary>
        /// G path file A2 in AF A1.
        /// Automatically create upfolder if there dont exists.
        /// </summary>
        /// <param name = "af"></param>
        /// <param name = "file"></param>
        public abstract StorageFile GetFile(AppFolders af, string file);
        public abstract bool IsRootFolderNull();
        //public abstract StorageFolder GetSunamoFolder();
        public abstract StorageFolder GetCommonSettings(string key);

        public abstract void SetCommonSettings(string key, string value);

        
    }
}
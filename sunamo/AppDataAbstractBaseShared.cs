using sunamo.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace sunamo
{
    public  abstract partial class AppDataAbstractBase<StorageFolder, StorageFile> : AppDataBase<StorageFolder, StorageFile>
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
        public abstract void AppendToFile( string value, StorageFile file);
        
    }
}

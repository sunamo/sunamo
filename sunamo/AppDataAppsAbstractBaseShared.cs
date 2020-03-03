using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sunamo
{
    public abstract partial class AppDataAppsAbstractBase<StorageFolder, StorageFile> : AppDataBase<StorageFolder, StorageFile>
    {
        public abstract StorageFolder GetRootFolder();

        protected abstract void SaveFile(string content, StorageFile sf);



        

        /// <summary>
        /// Pokud rootFolder bude SE nebo null, G false, jinak vrátí zda rootFolder existuej ve FS
        /// </summary>
        public abstract bool IsRootFolderOk();
        public abstract void AppendToFile(AppFolders af, string file, string value);
        public abstract void AppendToFile(string value, StorageFile file);
    }
}
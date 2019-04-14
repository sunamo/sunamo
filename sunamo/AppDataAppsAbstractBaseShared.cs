using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sunamo
{
    public abstract partial class AppDataAppsAbstractBase<StorageFolder, StorageFile> : AppDataBase<StorageFolder, StorageFile>
    {
        public  abstract Task< StorageFolder> GetRootFolder();

        protected  abstract Task SaveFile(string content, StorageFile sf);

        

        public  abstract Task<StorageFolder> GetFolder(AppFolders af);

        /// <summary>
        /// Pokud rootFolder bude SE nebo null, G false, jinak vrátí zda rootFolder existuej ve FS
        /// </summary>
        /// <returns></returns>
        public  abstract Task< bool> IsRootFolderOk();
        public abstract Task AppendToFile(AppFolders af, string file, string value);
        public abstract Task AppendToFile( string value, StorageFile file);
        
    }
}

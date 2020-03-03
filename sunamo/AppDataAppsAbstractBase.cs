using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sunamo
{
    public abstract partial class AppDataAppsAbstractBase<StorageFolder, StorageFile> : AppDataBase<StorageFolder, StorageFile>
    {
        /// <summary>
        /// AppDataAppsAbstractBase (this) - methods which are applied only on UAP
        /// AppDataAbstractBase  - methods which are applied only on desktop
        /// G path file A2 in AF A1.
        /// Automatically create upfolder if there dont exists.
        /// </summary>
        /// <param name = "af"></param>
        /// <param name = "file"></param>
        public abstract StorageFile GetFile(AppFolders af, string file);
        public abstract bool IsRootFolderNull();

        
        //public abstract StorageFolder> GetSunamoFolder();
    }
}
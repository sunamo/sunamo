using sunamo.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace sunamo
{
    public abstract partial class AppDataAbstractBase<StorageFolder, StorageFile> : AppDataBase<StorageFolder, StorageFile>
    {
        /// <summary>
        /// G path file A2 in AF A1.
        /// Automatically create upfolder if there dont exists.
        /// </summary>
        /// <param name = "af"></param>
        /// <param name = "file"></param>
        /// <returns></returns>
        public abstract StorageFile GetFile(AppFolders af, string file);
        public abstract bool IsRootFolderNull();

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunamoFtp
{
    public abstract class FtpAbstract
    {
        public abstract void D(string what, string text, params object[] args);
        public abstract void DebugActualFolder();
        #region Abstract methods
        public abstract bool mkdir(string dirName);
        public abstract bool download(string remFileName, string locFileName, Boolean deleteLocalIfExists);
        public abstract bool deleteRemoteFile(string fileName);
        public abstract void renameRemoteFile(string oldFileName, string newFileName);
        public abstract bool rmdir(List<string> slozkyNeuploadovatAVS, string dirName);
        public abstract void DeleteRecursively(List<string> slozkyNeuploadovatAVS, string dirName, int i, List<DirectoriesToDelete> td);
        public abstract void CreateDirectoryIfNotExists(string dirName);
        public abstract string[] ListDirectoryDetails();
        public abstract Dictionary<string, List<string>> getFSEntriesListRecursively(List<string> slozkyNeuploadovatAVS);
        public abstract void chdirLite(string dirName);

        public abstract void goToUpFolderForce();
        public abstract void goToUpFolder();
        public abstract void LoginIfIsNot(bool startup);

        public abstract long getFileSize(string filename);
        public abstract void goToPath(string slozkaNaHostingu);
        #endregion
    }
}

using Sunamo.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunamoFtp
{
    public abstract class FtpAbstract
    {
        #region Variables
        public IFtpClientExt MainWindow = null;
        /// <summary>
        /// Je public jen kvůli třídě Ftp
        /// </summary>
        public PathSelector ps = null;
        /// <summary>
        /// Vzdálený hostitel
        /// </summary>
        public string remoteHost;
        /// <summary>
        /// Uživatel který se pokouší přihlásit - používá se s příkazem USER
        /// </summary>
        public string remoteUser;
        /// <summary>
        /// Heslo uživatele který se pokouší autentizovat. Posílá se s příkazem PASS
        /// </summary>F
        public string remotePass;
        /// <summary>
        /// 
        /// </summary>
        public int remotePort;
        public bool logined;
        /// <summary>
        /// Pokud bude nastaveno na false, nebude se uploadovat na hosting nic - používá se pouze v této třídě, proto všechno ostatní bude fungovat normálně
        /// </summary>
        public bool reallyUpload = true;
        /// <summary>
        /// Počet výjimek u jedné operace. Ideální pro to aby napočítalo do 3 a pak celou operaci zrušilo
        /// </summary>
        protected int pocetExc = 0;
        protected int maxPocetExc = 3;
        protected bool startup = true;
        public ulong folderSizeRec = 0;
        #endregion

        #region Set variables methods
        /// <summary>
        /// S PP remoteHost A1
        /// </summary>
        /// <param name="remoteHost"></param>
        public void setRemoteHost(string remoteHost)
        {
            this.remoteHost = remoteHost;
        }

        /// <summary>
        /// G adresu vzdáleného hostitele
        /// </summary>
        /// <returns></returns>
        public string getRemoteHost()
        {
            return remoteHost;
        }

        /// <summary>
        /// S PP remotePort A1
        /// </summary>
        /// <param name="remotePort"></param>
        public void setRemotePort(int remotePort)
        {
            this.remotePort = remotePort;
        }

        /// <summary>
        /// G port který se používá pro vzdálený přenos
        /// </summary>
        /// <returns></returns>
        public int getRemotePort()
        {
            return remotePort;
        }

        /// <summary>
        /// S PP remoteUser A1
        /// </summary>
        /// <param name="remoteUser"></param>
        public void setRemoteUser(string remoteUser)
        {
            this.remoteUser = remoteUser;
        }

        /// <summary>
        /// S PP remotePass A1
        /// </summary>
        /// <param name="remotePass"></param>
        public void setRemotePass(string remotePass)
        {
            this.remotePass = remotePass;
        }
        #endregion

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

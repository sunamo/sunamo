using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Limilabs.FTP.Client;
using sunamo;
using sunamo.Essential;
using Sunamo.Data;


namespace SunamoFtp
{
    public class FtpDllWrapper : FtpBaseNew
    {
        public Ftp Client = null;
        static Type type = typeof(FtpDllWrapper);
        public FtpDllWrapper(Ftp ftp)
        {
            Client = ftp;
        }

        

        public override void chdirLite(string dirName)
        {
            ThrowExceptions.NotImplementedMethod(Exc.GetStackTrace(), type, Exc.CallingMethod());
        }

        public override void CreateDirectoryIfNotExists(string dirName)
        {
            ThrowExceptions.NotImplementedMethod(Exc.GetStackTrace(), type, Exc.CallingMethod());
        }

        public override void D(string what, string text, params object[] args)
        {
            ThrowExceptions.NotImplementedMethod(Exc.GetStackTrace(), type, Exc.CallingMethod());
        }

        public override void DebugActualFolder()
        {
			InitApp.Logger.WriteLine("Actual dir" + ":", Client.GetCurrentFolder());
        }

        public override void DebugAllEntries()
        {
			InitApp.Logger.WriteLine("All file entries" + ":");
			Client.GetList().ForEach(d => InitApp.Logger.WriteLine(d.Name));
            
        }

        public override void DebugDirChmod(string dir)
        {
            ThrowExceptions.NotImplementedMethod(Exc.GetStackTrace(), type, Exc.CallingMethod());
        }

        public override void DeleteRecursively(List<string> slozkyNeuploadovatAVS, string dirName, int i, List<DirectoriesToDelete> td)
        {
            ThrowExceptions.NotImplementedMethod(Exc.GetStackTrace(), type, Exc.CallingMethod());
        }

        public override bool deleteRemoteFile(string fileName)
        {
            ThrowExceptions.NotImplementedMethod(Exc.GetStackTrace(), type, Exc.CallingMethod());
            return false;
        }

        public override bool download(string remFileName, string locFileName, bool deleteLocalIfExists)
        {
            ThrowExceptions.NotImplementedMethod(Exc.GetStackTrace(), type, Exc.CallingMethod());
            return false;
        }

        public override long getFileSize(string filename)
        {
            ThrowExceptions.NotImplementedMethod(Exc.GetStackTrace(), type, Exc.CallingMethod());
            return 0;
        }

        public override Dictionary<string, List<string>> getFSEntriesListRecursively(List<string> slozkyNeuploadovatAVS)
        {
            ThrowExceptions.NotImplementedMethod(Exc.GetStackTrace(), type, Exc.CallingMethod());
            return null;
        }

        public override void goToPath(string slozkaNaHostingu)
        {
            ThrowExceptions.NotImplementedMethod(Exc.GetStackTrace(), type, Exc.CallingMethod());
        }

        public override void goToUpFolder()
        {
            ThrowExceptions.NotImplementedMethod(Exc.GetStackTrace(), type, Exc.CallingMethod());
        }

        public override void goToUpFolderForce()
        {
            ThrowExceptions.NotImplementedMethod(Exc.GetStackTrace(), type, Exc.CallingMethod());
        }

        public override List<string> ListDirectoryDetails()
        {
            ThrowExceptions.NotImplementedMethod(Exc.GetStackTrace(), type, Exc.CallingMethod());
            return null;
        }

        public override void LoginIfIsNot(bool startup)
        {
            ThrowExceptions.NotImplementedMethod(Exc.GetStackTrace(), type, Exc.CallingMethod());
        }

        public override bool mkdir(string dirName)
        {
            ThrowExceptions.NotImplementedMethod(Exc.GetStackTrace(), type, Exc.CallingMethod());
            return false;
        }

        public override void renameRemoteFile(string oldFileName, string newFileName)
        {
            ThrowExceptions.NotImplementedMethod(Exc.GetStackTrace(), type, Exc.CallingMethod());
        }

        public override bool rmdir(List<string> slozkyNeuploadovatAVS, string dirName)
        {
            ThrowExceptions.NotImplementedMethod(Exc.GetStackTrace(), type, Exc.CallingMethod());
            return false;
        }
    }
}
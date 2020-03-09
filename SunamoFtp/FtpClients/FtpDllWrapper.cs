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

        public FtpDllWrapper(Ftp ftp)
        {
            Client = ftp;
        }

        

        public override void chdirLite(string dirName)
        {
            ThrowExceptions.Custom(RuntimeHelper.GetStackTrace(), type, RH.CallingMethod(),NotImplementedException();
        }

        public override void CreateDirectoryIfNotExists(string dirName)
        {
            ThrowExceptions.Custom(RuntimeHelper.GetStackTrace(), type, RH.CallingMethod(),NotImplementedException();
        }

        public override void D(string what, string text, params object[] args)
        {
            ThrowExceptions.Custom(RuntimeHelper.GetStackTrace(), type, RH.CallingMethod(),NotImplementedException();
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
            ThrowExceptions.Custom(RuntimeHelper.GetStackTrace(), type, RH.CallingMethod(),NotImplementedException();
        }

        public override void DeleteRecursively(List<string> slozkyNeuploadovatAVS, string dirName, int i, List<DirectoriesToDelete> td)
        {
            ThrowExceptions.Custom(RuntimeHelper.GetStackTrace(), type, RH.CallingMethod(),NotImplementedException();
        }

        public override bool deleteRemoteFile(string fileName)
        {
            ThrowExceptions.Custom(RuntimeHelper.GetStackTrace(), type, RH.CallingMethod(),NotImplementedException();
        }

        public override bool download(string remFileName, string locFileName, bool deleteLocalIfExists)
        {
            ThrowExceptions.Custom(RuntimeHelper.GetStackTrace(), type, RH.CallingMethod(),NotImplementedException();
        }

        public override long getFileSize(string filename)
        {
            ThrowExceptions.Custom(RuntimeHelper.GetStackTrace(), type, RH.CallingMethod(),NotImplementedException();
        }

        public override Dictionary<string, List<string>> getFSEntriesListRecursively(List<string> slozkyNeuploadovatAVS)
        {
            ThrowExceptions.Custom(RuntimeHelper.GetStackTrace(), type, RH.CallingMethod(),NotImplementedException();
        }

        public override void goToPath(string slozkaNaHostingu)
        {
            ThrowExceptions.Custom(RuntimeHelper.GetStackTrace(), type, RH.CallingMethod(),NotImplementedException();
        }

        public override void goToUpFolder()
        {
            ThrowExceptions.Custom(RuntimeHelper.GetStackTrace(), type, RH.CallingMethod(),NotImplementedException();
        }

        public override void goToUpFolderForce()
        {
            ThrowExceptions.Custom(RuntimeHelper.GetStackTrace(), type, RH.CallingMethod(),NotImplementedException();
        }

        public override List<string> ListDirectoryDetails()
        {
            ThrowExceptions.Custom(RuntimeHelper.GetStackTrace(), type, RH.CallingMethod(),NotImplementedException();
        }

        public override void LoginIfIsNot(bool startup)
        {
            ThrowExceptions.Custom(RuntimeHelper.GetStackTrace(), type, RH.CallingMethod(),NotImplementedException();
        }

        public override bool mkdir(string dirName)
        {
            ThrowExceptions.Custom(RuntimeHelper.GetStackTrace(), type, RH.CallingMethod(),NotImplementedException();
        }

        public override void renameRemoteFile(string oldFileName, string newFileName)
        {
            ThrowExceptions.Custom(RuntimeHelper.GetStackTrace(), type, RH.CallingMethod(),NotImplementedException();
        }

        public override bool rmdir(List<string> slozkyNeuploadovatAVS, string dirName)
        {
            ThrowExceptions.Custom(RuntimeHelper.GetStackTrace(), type, RH.CallingMethod(),NotImplementedException();
        }
    }
}
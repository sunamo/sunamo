using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Limilabs.FTP.Client;
using shared.Essential;
using sunamo;
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
            throw new NotImplementedException();
        }

        public override void CreateDirectoryIfNotExists(string dirName)
        {
            throw new NotImplementedException();
        }

        public override void D(string what, string text, params object[] args)
        {
            throw new NotImplementedException();
        }

        public override void DebugActualFolder()
        {
            ConsoleLogger.Instance.WriteLine("Actual dir:", Client.GetCurrentFolder());
        }

        public override void DebugAllEntries()
        {
            ConsoleLogger.Instance.WriteLine("All file entries:");
            Client.GetList().ForEach(d => ConsoleLogger.Instance.WriteLine(d.Name));
            
        }

        public override void DebugDirChmod(string dir)
        {
            throw new NotImplementedException();
        }

        public override void DeleteRecursively(List<string> slozkyNeuploadovatAVS, string dirName, int i, List<DirectoriesToDelete> td)
        {
            throw new NotImplementedException();
        }

        public override bool deleteRemoteFile(string fileName)
        {
            throw new NotImplementedException();
        }

        public override bool download(string remFileName, string locFileName, bool deleteLocalIfExists)
        {
            throw new NotImplementedException();
        }

        public override long getFileSize(string filename)
        {
            throw new NotImplementedException();
        }

        public override Dictionary<string, List<string>> getFSEntriesListRecursively(List<string> slozkyNeuploadovatAVS)
        {
            throw new NotImplementedException();
        }

        public override void goToPath(string slozkaNaHostingu)
        {
            throw new NotImplementedException();
        }

        public override void goToUpFolder()
        {
            throw new NotImplementedException();
        }

        public override void goToUpFolderForce()
        {
            throw new NotImplementedException();
        }

        public override string[] ListDirectoryDetails()
        {
            throw new NotImplementedException();
        }

        public override void LoginIfIsNot(bool startup)
        {
            throw new NotImplementedException();
        }

        public override bool mkdir(string dirName)
        {
            throw new NotImplementedException();
        }

        public override void renameRemoteFile(string oldFileName, string newFileName)
        {
            throw new NotImplementedException();
        }

        public override bool rmdir(List<string> slozkyNeuploadovatAVS, string dirName)
        {
            throw new NotImplementedException();
        }
    }
}

using Limilabs.FTP.Client;

using SunamoFtp;
using System;

namespace tempConsole
{
    public class FtpTest
    {
        private static void SetConnectionInfo(FtpAbstract ftpBase)
        {
            ftpBase.setRemoteHost("185.8.239.101");
            ftpBase.setRemoteUser("defaultscz");
            ftpBase.setRemotePass("hekaPuC4;");
        }

        public static void FtpDll()
        {
            FtpDllWrapper ftpDll = new FtpDllWrapper(new Ftp());
            SetConnectionInfo(ftpDll);
            Ftp ftp = ftpDll.Client;

            ftp.Connect(ftpDll.remoteHost); 
            ftp.Login(ftpDll.remoteUser, ftpDll.remotePass);
            
            string folder = "a";
            ftpDll.DebugActualFolder();
            ftpDll.DebugAllEntries();
            
            ftp.CreateFolder("/" + folder);
            ftp.ChangeFolder(folder);
            ftpDll.DebugActualFolder();
            ftp.UploadFiles("D:\a.txt");

            ftp.Close();
        }


        public static void FluentFtp()
        {
            SunamoFtp.FluentFtpWrapper fluentFtpWrapper = new SunamoFtp.FluentFtpWrapper();
            
            fluentFtpWrapper.TestBasicFunctionality();
        }
    }
}

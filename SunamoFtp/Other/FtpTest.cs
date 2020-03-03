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
            ftpBase.setRemoteUser(AppData.ci.GetCommonSettings(CommonSettingsKeys.ftp_test_user));
            ftpBase.setRemotePass(AppData.ci.GetCommonSettings(CommonSettingsKeys.ftp_test_pw));
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
            
            ftp.CreateFolder(AllStrings.slash + folder);
            ftp.ChangeFolder(folder);
            ftpDll.DebugActualFolder();
            ftp.UploadFiles("d:\a.txt");

            ftp.Close();
        }


        public static void FluentFtp()
        {
            SunamoFtp.FluentFtpWrapper fluentFtpWrapper = new SunamoFtp.FluentFtpWrapper();
            
            fluentFtpWrapper.TestBasicFunctionality();
        }
    }
}
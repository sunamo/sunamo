
﻿using FluentFTP;
using Sunamo.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SunamoFtp
{
    public class FluentFtpWrapper : FtpBaseNew
    {
        static Type type = typeof(FluentFtpWrapper);
        public FtpClient client = null;

        public override void DebugActualFolder()
        {
            ThrowExceptions.NotImplementedMethod(Exc.GetStackTrace(), type, Exc.CallingMethod());
        }

        public override void D(string what, string text, params object[] args)
        {
            ThrowExceptions.NotImplementedMethod(Exc.GetStackTrace(), type, Exc.CallingMethod());
        }

        public override void DebugAllEntries()
        {
            ThrowExceptions.NotImplementedMethod(Exc.GetStackTrace(), type, Exc.CallingMethod());
        }

        public  void TestBasicFunctionality()
        {
            // create an FTP client
            client = new FtpClient(remoteHost);

            // if you don't specify login credentials, we use the "anonymous" user account
            client.Credentials = new NetworkCredential(remoteUser, remotePass);

            // begin connecting to the server
            client.Connect();

            Console.WriteLine( client.GetChmod(AllStrings.slash));

            // get a list of files and directories in the "/" + "htdocs" folder
            foreach (FtpListItem item in client.GetListing(AllStrings.slash))
            {

                // if this is a file
                if (item.Type == FtpFileSystemObjectType.File)
                {
                    // get the file size
                    long size = client.GetFileSize(item.FullName);
                }

                Console.WriteLine(item.Chmod);
                Console.WriteLine(item.Name);


                // get modified date/time of the file or folder
                DateTime time = client.GetModifiedTime(item.FullName);

                // calculate a hash for the file on the server side (default algorithm)
                FtpHash hash = client.GetHash(item.FullName);
            }
            return;
            var d = client.GetWorkingDirectory();

            client.CreateDirectory("htdocs2");
            CreateDirectoryIfNotExists("/" + "htdocs");

            // upload a file
            client.UploadFile(@"d:\a.txt", "/htdocs/big.txt");

            // rename the uploaded file
            client.Rename("/htdocs/big.txt", "/htdocs/big2.txt");

            // download the file again
            client.DownloadFile(@"d:\b.txt", "/htdocs/big2.txt");

            // delete the file
            client.DeleteFile("/htdocs/big2.txt");

            // delete a folder recursively
            client.DeleteDirectory("/" + "htdocs/extras" + "/");

            // check if a file exists
            if (client.FileExists("/htdocs/big2.txt")) { }

            client.CreateDirectory("/" + "htdocs/extras" + "/");

            // upload a file and retry 3 times before giving up
            client.RetryAttempts = 3;
            client.UploadFile(@"c:\MyVideo.mp4", "/htdocs/big.txt", FtpExists.Overwrite, false, FtpVerify.Retry);

            // disconnect! good bye!
            client.Disconnect();

        }

        #region Other


        public override void CreateDirectoryIfNotExists(string dirName)
        {
            // check if a folder exists
            if (!client.DirectoryExists(dirName))
            {
                client.CreateDirectory(dirName);
            }
            else
            {
                int i = 0;
            }
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
        #endregion

        public override void DebugDirChmod(string dir)
        {
            //ConsoleLogger.Instance.WriteLine(client.GetChmod(dir).ToString());
            //client.Chmod(dir, 777)
        }

        #region Other
        public override void DeleteRecursively(List<string> slozkyNeuploadovatAVS, string dirName, int i, List<DirectoriesToDelete> td)
        {
            ThrowExceptions.NotImplementedMethod(Exc.GetStackTrace(), type, Exc.CallingMethod());
        }


        /// <summary>
        /// Must be here due to interface
        /// </summary>
        /// <param name="dirName"></param>
        public override void chdirLite(string dirName)
        {
            ThrowExceptions.NotImplementedMethod(Exc.GetStackTrace(), type, Exc.CallingMethod());
        }
        #endregion
    }
}
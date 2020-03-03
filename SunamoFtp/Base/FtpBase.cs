using sunamo;
using sunamo.Essential;
using Sunamo.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;

namespace SunamoFtp
{
    public abstract class FtpBase : FtpAbstract
    {
        

        #region ctor
        /// <summary>
        /// IK, OOP.
        /// </summary>
        public FtpBase()
        {
            ps = new PathSelector("");
            remoteHost = string.Empty;
            //remotePath = AllStrings.dot;
            remoteUser = string.Empty;
            remotePass = string.Empty;
            remotePort = 21;
            logined = false;
        }
        #endregion

        
        
        

        

        public void OnNewStatusNewFolder()
        {
            NewStatus("Nová složka je" + " " + ps.ActualPath);
        }

        /// <summary>
        /// Upload file by FtpWebRequest
        /// OK
        /// STOR
        /// Pokud chceš uploadovat soubor do aktuální složky a zvlolit pouze název souboru na disku, použij metodu UploadFile.
        /// </summary>
        /// <param name="local"></param>
        /// <param name="_UploadPath"></param>
        public virtual bool UploadFileMain(String local, String _UploadPath)
        {
            if (pocetExc < maxPocetExc)
            {
                OnNewStatus("Uploaduji" + " " + _UploadPath);

                System.IO.FileInfo _FileInfo = new System.IO.FileInfo(local);
                System.IO.Stream _Stream = null;
                System.IO.FileStream _FileStream = null;

                try
                {
                    // Create FtpWebRequest object from the Uri provided
                    FtpWebRequest _FtpWebRequest = (FtpWebRequest)FtpWebRequest.Create(new Uri(_UploadPath));

                    // Provide the WebPermission Credintials
                    _FtpWebRequest.Credentials = new NetworkCredential(remoteUser, remotePass);

                    _FtpWebRequest.KeepAlive = false;

                    // set timeout for 20 seconds
                    _FtpWebRequest.Timeout = 20000;

                    // Specify the command to be executed.
                    _FtpWebRequest.Method = WebRequestMethods.Ftp.UploadFile;

                    // Specify the data transfer type.
                    _FtpWebRequest.UseBinary = true;

                    // Notify the server about the size of the uploaded file
                    _FtpWebRequest.ContentLength = _FileInfo.Length;

                    // The buffer size is set to 2kb
                    int buffLength = 2048;
                    byte[] buff = new byte[buffLength];

                    // Opens a file stream (System.IO.FileStream) to read the file to be uploaded
                    _FileStream = _FileInfo.OpenRead();

                    // Stream to which the file to be upload is written
                    _Stream = _FtpWebRequest.GetRequestStream();

                    // Read from the file stream 2kb at a time
                    int contentLen = _FileStream.Read(buff, 0, buffLength);

                    // Till Stream content ends
                    while (contentLen != 0)
                    {
                        // Write Content from the file stream to the FTP Upload Stream
                        _Stream.Write(buff, 0, contentLen);
                        contentLen = _FileStream.Read(buff, 0, buffLength);
                    }

                    // Close the file stream and the Request Stream
                    _Stream.Close();
                    _Stream.Dispose();
                    _FileStream.Close();
                    _FileStream.Dispose();

                    pocetExc = 0;
                    // Close the file stream and the Request Stream
                }
                catch (Exception ex)
                {
                    pocetExc++;
                    CleanUp.Streams(_Stream, _FileStream);
                    OnNewStatus("Upload file error" + ": " + ex.Message);
                    return UploadFileMain(local, _UploadPath);
                }
                finally
                {
                    CleanUp.Streams(_Stream, _FileStream);
                }
                pocetExc = 0;
                return true;
            }
            else
            {
                pocetExc = 0;
                return false;
            }
        }

        #region Statuses
        public void OnUploadingNewStatus(string path)
        {
            OnNewStatus("Uploaduji" + " " + path + " " + "bezpečnou metodou");
        }
        #endregion

        

        public static event VoidObjectParamsObjects NewStatus;

        /// <summary>
        /// OK
        /// </summary>
        /// <param name="s"></param>
        /// <param name="p"></param>
        public static void OnNewStatus(string s, params object[] p)
        {
            NewStatus(s, p);
        }





        /// <summary>
        /// STOR
        /// Nauploaduje pouze soubory které ještě v adresáři nejsou
        /// </summary>
        /// <param name="files"></param>
        /// <param name="iw"></param>
        
        public bool uploadFolder(string slozkaFrom, bool FTPclass, IWorking working)
        {
            string actPath = ps.ActualPath;
            bool vr = uploadFolderShared(slozkaFrom, false, working);
            if (FTPclass)
            {
                goToPath(actPath);
            }
            return vr;
        }


        #endregion
    }
}

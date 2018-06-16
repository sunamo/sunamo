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
            //remotePath = ".";
            remoteUser = string.Empty;
            remotePass = string.Empty;
            remotePort = 21;
            logined = false;
        }
        #endregion

        
        
        

        

        public void OnNewStatusNewFolder()
        {
            NewStatus("Nová složka je " + ps.ActualPath);
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
                OnNewStatus("Uploaduji " + _UploadPath);

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
                    OnNewStatus("Upload file error: " + ex.Message);
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
            OnNewStatus("Uploaduji " + path + " bezpečnou metodou");
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
        /// <returns></returns>
        public bool UploadFiles(string[] files)
        {

            string[] fse = ListDirectoryDetails();
            foreach (string item in files)
            {

                long fileSize = sunamo.FS.GetFileSize(item);
                if (!FtpHelper.IsFileOnHosting(item, fse, fileSize))
                {

                    UploadFile(item);
                }

            }
            return true;
        }

        public  string GetActualPath()
        {
            return UH.Combine(true, remoteHost + ":" + remotePort, ps.ActualPath);
        }

        /// <summary>
        /// A1 musí být vždy pouze název adresáře/souboru, nikdy to nemůže být plná cesta
        /// </summary>
        /// <param name="dirName"></param>
        /// <returns></returns>
        public string GetActualPath(string dirName)
        {
            string s = UH.Combine(true, remoteHost + ":" + remotePort, UH.Combine(true, ps.ActualPath, dirName));
            return s.TrimEnd('/');
        }



        #region Zakomentované metody
        #endregion

        #region OK metody
        /// <summary>
        /// STOR
        /// </summary>
        /// <param name="slozkaNaLocalu"></param>
        /// <param name="slozkaNaHostingu"></param>
        /// <param name="iw"></param>
        /// <returns></returns>
        public bool uploadFolderRek(string slozkaNaLocalu, string slozkaNaHostingu)
        {
            // Musí to tu být právě kvůli předchozímu řádku string[] fse = getFSEntriesList(); kdy získávám seznam souborů na FTP serveru
            goToPath(slozkaNaHostingu);



            string[] directories = Directory.GetDirectories(slozkaNaLocalu);
            string[] files = Directory.GetFiles(slozkaNaLocalu);
            OnNewStatus("Uploaduji všechny soubory (" + files.Length + ") do složky ftp serveru " + ps.ActualPath);

            if (!UploadFiles(files))
            {
                return false;
            }

            foreach (string item in directories)
            {
                if (!uploadFolderRek(item, UH.Combine(false, slozkaNaHostingu, sunamo.FS.GetFileName(item))))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// OK
        /// </summary>
        /// <param name="slozkaNaLocalu"></param>
        /// <returns></returns>
        public bool uploadFolderRek(string slozkaNaLocalu, IWorking iw)
        {
            return uploadFolderShared(slozkaNaLocalu, true, iw);
        }

        

        /// <summary>
        /// OK
        /// LIST
        /// Tato metoda není vstupní, nevolej ji, zavolej místo toho getFSEntriesListRecursively s 1 parametrem
        /// </summary>
        /// <param name="projiteSlozky"></param>
        /// <param name="vr"></param>
        /// <param name="p"></param>
        /// <param name="folderName"></param>
        public void getFSEntriesListRecursively(List<string> slozkyNeuploadovatAVS, List<string> projiteSlozky, Dictionary<string, List<string>> vr, string p, string folderName)
        {
            LoginIfIsNot(startup);
            string nextPath = UH.Combine(true, ps.ActualPath, folderName);
            if (!projiteSlozky.Contains(nextPath))
            {
                NewStatus("Složka do které se mělo přejít (" + nextPath + ") ještě nebyla v projeté kolekci");
                ps.AddToken(folderName);
                projiteSlozky.Add(nextPath);


                string[] fse = ListDirectoryDetails();
                string actualPath = ps.ActualPath;
                foreach (string item in fse)
                {
                    string size = SH.JoinFromIndex(4, ' ', SH.Split(item, " "));
                    char fz = item[0];
                    if (fz == '-')
                    {
                        if (size != "0")
                        {
                            folderSizeRec += ulong.Parse(size.Substring(0, size.IndexOf(' ') + 1));
                        }

                        if (vr.ContainsKey(actualPath))
                        {
                            vr[actualPath].Add(item);
                        }
                        else
                        {
                            List<string> ppk = new List<string>();
                            ppk.Add(item);
                            vr.Add(actualPath, ppk);
                        }
                    }
                    else if (fz == 'd')
                    {
                        string folderName2 = SH.JoinFromIndex(8, ' ', SH.Split(item, " "));
                        if (!FtpHelper.IsThisOrUp(folderName2))
                        {
                            if (slozkyNeuploadovatAVS.Contains(folderName2) && ps.ActualPath == MainWindow.WwwSlash)
                            {
                                continue;
                            }
                            if (vr.ContainsKey(actualPath))
                            {
                                vr[actualPath].Add(item);
                            }
                            else
                            {
                                List<string> ppk = new List<string>();
                                ppk.Add(item);
                                vr.Add(actualPath, ppk);
                            }
                            getFSEntriesListRecursively(slozkyNeuploadovatAVS, projiteSlozky, vr, ps.ActualPath, folderName2);
                        }
                    }

                    else
                    {

                        throw new Exception("Nepodporovaný typ objektu");
                    }
                }
                if (ps.CanGoToUpFolder)
                {
                    goToUpFolder();
                    //ps.RemoveLastToken();
                }
            }
            else
            {
                NewStatus("Složka do které se mělo přejít (" + nextPath + ") již byla v projeté kolekci");
            }

            //ps.ActualPath = p;
        }

        /// <summary>
        /// OK
        /// RETR
        /// Stáhne soubor A1 do lok. souboru A2. Nenavazuje
        /// </summary>
        /// <param name="remFileName"></param>
        /// <param name="locFileName"></param>
        public void download(string remFileName, string locFileName)
        {
            download(remFileName, locFileName, true);
        }

        /// <summary>
        /// OK
        /// STOR
        /// Před použitím této metody se musím přesunout do složky do které chci uploadovat.
        /// Methods to upload file to FTP Server
        /// </summary>
        /// <param name="_FileName">local source file name</param>
        /// <param name="_UploadPath">Upload FTP path including Host name</param>
        /// <param name="_FTPUser">FTP login username</param>
        /// <param name="_FTPPass">FTP login password</param>
        public void UploadFile(string _FileName)
        {
            String _UploadPath = UH.Combine(false, remoteHost + ":" + remotePort + "/", UH.Combine(true, ps.ActualPath, sunamo.FS.GetFileName(_FileName)));
            if (reallyUpload)
            {
                UploadFileMain(_FileName, _UploadPath);
            }

            //MainWindow.FileUploaded(_FileName);
        }

        /// <summary>
        /// OK
        /// STOR
        /// Metoda s druhým argumentem, pokud chci uploadovat do jiné složky, než ve které teď jsem
        /// </summary>
        /// <param name="fullFilePath"></param>
        /// <param name="actualFtpPath"></param>
        /// <returns></returns>
        public bool UploadFile(string fullFilePath, string actualFtpPath)
        {
            String _UploadPath = UH.Combine(false, remoteHost + ":" + remotePort + "/" + "/", UH.Combine(false, actualFtpPath, sunamo.FS.GetFileName(fullFilePath)));
            var vr = true;
            if (reallyUpload)
            {
                vr = UploadFileMain(fullFilePath, _UploadPath);
            }
            return vr;
        }



        /// <summary>
        /// OK
        /// STOR
        /// </summary>
        /// <param name="slozkaTo"></param>
        /// <param name="iw"></param>
        /// <returns></returns>
        public bool uploadFolderShared(string slozkaFrom, bool rek, IWorking working)
        {
            string nazevSlozky = Path.GetFileName(slozkaFrom);
            string pathFolder = UH.Combine(true, ps.ActualPath, nazevSlozky);
            slozkaFrom = slozkaFrom.TrimEnd('\\');
            string[] soubory = Directory.GetFiles(slozkaFrom);
            string[] slozky = Directory.GetDirectories(slozkaFrom);

            NewStatus("Uploaduji všechny soubory (" + soubory.Length + ") do složky ftp serveru " + pathFolder);

            CreateDirectoryIfNotExists(nazevSlozky);
            foreach (var item in soubory)
            {
                if (!working.IsWorking)
                {
                    return false;
                }
                UploadFile(item);
            }

            if (rek)
            {
                if (slozky.Length == 0)
                {
                    goToUpFolder();
                }
                else
                {
                    foreach (var item in slozky)
                    {
                        uploadFolderShared(item, rek, working);
                    }
                    if (slozky.Length != 0)
                    {
                        goToUpFolder();
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// OK
        /// Do A1 se zadává název souboru bez cesty
        /// </summary>
        /// <param name="folder"></param>
        /// <returns></returns>
        public bool ExistsFolder(string folder)
        {
            string[] fse = ListDirectoryDetails();
            List<string> d = new List<string>(FtpHelper.GetDirectories(fse));
            return d.Contains(folder);
        }
        #endregion

        #region OK Methods
        /// <summary>
        /// OK
        /// Po zavolání této metody v třídě FTP, pokud chceš do adresáře, kde jsi byl před jejím zavoláním, musíš zavolat goToUpFolder
        /// </summary>
        /// <param name="slozkaFrom"></param>
        /// <returns></returns>
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

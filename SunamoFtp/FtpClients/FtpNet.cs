using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Security.Permissions;
using System.Threading;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Globalization;

using System.Diagnostics;
using System.Windows;
using sunamo.Essential;
using Sunamo.Data;

namespace SunamoFtp
{
    public class FtpNet : FtpBase
    {
        public override void LoginIfIsNot(bool startup)
        {
            base.startup = startup;
            // Není potřeba se přihlašovat, přihlašovácí údaje posílám při každém příkazu
        }

        public override void goToPath(string remoteFolder)
        {
            OnNewStatus("Přecházím do složky " + remoteFolder);
            string actualPath = ps.ActualPath;
            int dd = remoteFolder.Length - 1;
            if (actualPath == remoteFolder)
            {
                return;
            }
            else
            {
                // Vzdálená složka začíná s aktuální cestou == vzdálená složka je delší. Pouze přejdi hloubš
                if (remoteFolder.StartsWith(actualPath))
                {
                    remoteFolder = remoteFolder.Substring(actualPath.Length);
                    string[] tokens = SH.Split(remoteFolder, ps.Delimiter);
                    foreach (string item in tokens)
                    {
                        CreateDirectoryIfNotExists(item);
                    }
                }
                // Vzdálená složka nezačíná aktuální cestou, 
                else
                {
                    ps.ActualPath = "";
                    string[] tokens = SH.Split(remoteFolder, ps.Delimiter);
                    int pridat = 0;
                    for (int i = 0 + pridat; i < tokens.Length; i++)
                    {
                        CreateDirectoryIfNotExists(tokens[i]);
                    }
                }
            }
        }




        /// <summary>
        /// RENAME
        /// Pošlu příkaz RNFR A1 a když bude odpoveď 350, tak RNTO
        /// </summary>
        /// <param name="oldFileName"></param>
        /// <param name="newFileName"></param>
        public override void renameRemoteFile(string oldFileName, string newFileName)
        {
            OnNewStatus("Ve složce " + ps.ActualPath + " přejmenovávám soubor z " + oldFileName + " na " + newFileName);

            if (pocetExc < maxPocetExc)
            {

                FtpWebRequest reqFTP = null;
                Stream ftpStream = null;
                FtpWebResponse response = null;
                try
                {
                    reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(GetActualPath(oldFileName)));
                    reqFTP.Method = WebRequestMethods.Ftp.Rename;
                    reqFTP.RenameTo = newFileName;
                    reqFTP.UseBinary = true;
                    reqFTP.Credentials = new NetworkCredential(remoteUser, remotePass);
                    response = (FtpWebResponse)reqFTP.GetResponse();
                    ftpStream = response.GetResponseStream();
                }
                catch (Exception ex)
                {
                    OnNewStatus("Error rename file: " + ex.Message);
                    pocetExc++;
                    renameRemoteFile(oldFileName, newFileName);
                }
                finally
                {
                    if (ftpStream != null)
                    {
                        ftpStream.Dispose();
                    }
                    if (response != null)
                    {
                        response.Dispose();
                    }
                }
            }
            pocetExc = 0;
        }

        #region Zakomentované metody
        /// <summary>
        /// Před zavoláním této metody se musí musí zjistit zda první znak je d(adresář) nebo -(soubor)
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        #endregion

        #region OK Metody
        /// <summary>
        /// OK
        /// RMD
        /// Smaže v akt. složce adr. A1 příkazem RMD
        /// Tato metoda se může volat pouze když se bude vědět se složka je prázdná, jinak se program nesmaže a program vypíše chybu 550
        /// </summary>
        /// <param name="dirName"></param>
        public override bool rmdir(List<string> slozkyNeuploadovatAVS, string dirName)
        {
            if (pocetExc < maxPocetExc)
            {
                string ma = GetActualPath(dirName).TrimEnd('/');
                OnNewStatus("Mažu adresář " + ma);

                FtpWebRequest clsRequest = null;
                StreamReader sr = null;
                Stream datastream = null;
                FtpWebResponse response = null;
                try
                {
                    clsRequest = (FtpWebRequest)WebRequest.Create(new Uri(ma));
                    clsRequest.Credentials = new NetworkCredential(remoteUser, remotePass);

                    clsRequest.Method = WebRequestMethods.Ftp.RemoveDirectory;

                    string result = string.Empty;
                    response = (FtpWebResponse)clsRequest.GetResponse();

                    long size = response.ContentLength;
                    datastream = response.GetResponseStream();
                    sr = new StreamReader(datastream);
                    result = sr.ReadToEnd();
                }
                catch (Exception ex)
                {
                    pocetExc++;
                    if (sr != null)
                    {
                        sr.Dispose();
                    }
                    if (datastream != null)
                    {
                        datastream.Dispose();
                    }
                    if (response != null)
                    {
                        response.Dispose();
                    }
                    OnNewStatus("Error delete folder: " + ex.Message);
                    return rmdir(slozkyNeuploadovatAVS, dirName);

                }
                finally
                {
                    if (sr != null)
                    {
                        sr.Dispose();
                    }
                    if (datastream != null)
                    {
                        datastream.Dispose();
                    }
                    if (response != null)
                    {
                        response.Dispose();
                    }
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

        /// <summary>
        /// OK
        /// DELE + RMD
        /// </summary>
        /// <param name="slozkyNeuploadovatAVS"></param>
        /// <param name="dirName"></param>
        public override void DeleteRecursively(List<string> slozkyNeuploadovatAVS, string dirName, int i, List<DirectoriesToDelete> td)
        {

            i++;
            string[] smazat = ListDirectoryDetails();
            //bool pridano = false;
            td.Add(new DirectoriesToDelete { hloubka = i });
            Dictionary<string, List<string>> ds = null;
            foreach (var item in td)
            {
                if (item.hloubka == i)
                {
                    if (item.adresare.Count != 0)
                    {


                        foreach (var item2 in item.adresare)
                        {
                            foreach (var item3 in item2)
                            {
                                if (item3.Key == ps.ActualPath)
                                {
                                    ds = item2;
                                }
                            }
                        }
                    }
                    else
                    {
                        ds = new Dictionary<string, List<string>>();
                    }
                    //ds = ; 
                }
            }
            for (int z = 0; z < td.Count; z++)
            {
                var item = td[z];


                if (item.hloubka == i)
                {


                    //ds.Add(ps.ActualPath, new List<string>());
                    foreach (var item2 in smazat)
                    {
                        string fn = "";
                        FileSystemType fst = FtpHelper.IsFile(item2, out fn);
                        if (fst == FileSystemType.File)
                        {
                            if (ds.ContainsKey(ps.ActualPath))
                            {

                            }
                            else
                            {
                                ds.Add(ps.ActualPath, new List<string>());
                            }
                            var f = ds[ps.ActualPath];
                            f.Add(fn);
                        }
                        else if (fst == FileSystemType.Folder)
                        {

                            ps.AddToken(fn);
                            ds.Add(ps.ActualPath, new List<string>());
                            //pridano = true;
                            DeleteRecursively(slozkyNeuploadovatAVS, fn, i, td);

                        }
                        //Debug.Print(item2);
                    }
                    //item.adresare.Add(ds);
                }
            }
            if (true)
            {
                foreach (var item in td)
                {
                    if (item.hloubka == i)
                    {
                        item.adresare.Add(ds);
                    }
                }
            }
            if (i == 1)
            {
                List<string> smazaneAdresare = new List<string>();
                for (int y = td.Count - 1; y >= 0; y--)
                {
                    foreach (var item in td[y].adresare)
                    {
                        foreach (var item2 in item)
                        {
                            ps.ActualPath = item2.Key;
                            string sa = item2.Key;
                            if (!smazaneAdresare.Contains(sa))
                            {


                                smazaneAdresare.Add(sa);
                                foreach (var item3 in item2.Value)
                                {
                                    while (!
                                    deleteRemoteFile(item3))
                                    {

                                    }
                                }
                                goToUpFolderForce();
                                rmdir(new List<string>(), Path.GetFileName(item2.Key.TrimEnd('/')));
                            }
                        }
                    }
                }
            }

        }

        /// <summary>
        /// OK
        /// NLST
        /// Vrátí pouze názvy souborů, bez složek nebo linků
        /// Pokud nejsem přihlášený, přihlásím se M login
        /// Vytvořím objekt Socket metodou createDataSocket ze které budu přidávat znaky
        /// Zavolám příkaz NLST s A1,
        /// Skrz objekt Socket získám bajty, které okamžitě přidávám do řetězce
        /// Odpověď získám M readReply a G
        /// </summary>
        /// <param name="mask"></param>
        /// <returns></returns>
        public string[] getFileList(string mask)
        {
            if (pocetExc < maxPocetExc)
            {
                OnNewStatus("Získávám seznam souborů ze složky " + ps.ActualPath + " příkazem NLST");


                StringBuilder result = new StringBuilder();
                FtpWebRequest reqFTP = null;
                StreamReader reader = null;
                WebResponse response = null;
                try
                {
                    reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(GetActualPath()));
                    reqFTP.UseBinary = true;
                    reqFTP.Credentials = new NetworkCredential(remoteUser, remotePass);
                    reqFTP.Method = WebRequestMethods.Ftp.ListDirectory;

                    response = reqFTP.GetResponse();
                    reader = new StreamReader(response.GetResponseStream(), System.Text.Encoding.GetEncoding("windows-1250"));
                    string line = reader.ReadLine();
                    while (line != null)
                    {
                        result.Append(line);
                        result.Append("\n");
                        line = reader.ReadLine();
                    }
                    result.Remove(result.ToString().LastIndexOf('\n'), 1);
                    return result.ToString().Split('\n');
                }
                catch (Exception ex)
                {
                    if (reader != null)
                    {
                        reader.Dispose();
                    }
                    if (response != null)
                    {
                        response.Dispose();
                    }
                    OnNewStatus("Error get filelist: " + ex.Message);
                    if (pocetExc == 2)
                    {
                        pocetExc = 0;
                        string[] downloadFiles = new string[0];
                        return downloadFiles;
                    }
                    else
                    {
                        return getFileList(mask);
                    }
                }
                finally
                {
                    if (reader != null)
                    {
                        reader.Dispose();
                    }
                    if (response != null)
                    {
                        response.Dispose();
                    }
                }
            }
            else
            {
                pocetExc = 0;
                string[] downloadFiles = new string[0];
                return downloadFiles;
            }

        }

        /// <summary>
        /// OK
        /// MKD
        /// Adresář vytvoří pokud nebude existovat
        /// </summary>
        /// <param name="dirName"></param>
        public override void CreateDirectoryIfNotExists(string dirName)
        {

            if (dirName != "")
            {
                dirName = Path.GetFileName(dirName.TrimEnd('/'));
                if (dirName[dirName.Length - 1] == "/"[0])
                {
                    dirName = dirName.Substring(0, dirName.Length - 1);
                }
            }
            else
            {
                OnNewStatus("Nemohl být vytvořen nový adresář, protože nebyl zadán jeho název");
                return;
            }

            bool nalezenAdresar = false;
            string[] fse = null;
            bool vseMa8 = false;
            while (!vseMa8)
            {
                vseMa8 = true;
                fse = this.ListDirectoryDetails();

                foreach (string item in fse)
                {
                    int tokens = SH.Split(item, " ").Length;
                    if (tokens < 8)
                    {
                        vseMa8 = false;
                    }
                }
            }
            foreach (string item in fse)
            {
                string fn = null;
                if (FtpHelper.IsFile(item, out fn) == FileSystemType.Folder)
                {
                    if (fn == dirName)
                    {
                        nalezenAdresar = true;
                        break;
                    }
                }
            }
            if (!nalezenAdresar)
            {
                if (mkdir(dirName))
                {
                }
            }
            else
            {
                ps.AddToken(dirName);
            }
        }

        public override void chdirLite(string dirName)
        {
            // Trim slash from end in dirName variable
            if (dirName != "")
            {
                if (dirName[dirName.Length - 1] == "/"[0])
                {
                    dirName = dirName.Substring(0, dirName.Length - 1);
                }
            }
            else
            {
                dirName = MainWindow.Www;
            }


            bool nalezenAdresar = false;
            string[] fse = null;
            bool vseMa8 = false;
            while (!vseMa8)
            {
                vseMa8 = true;
                fse = this.ListDirectoryDetails();

                foreach (string item in fse)
                {
                    int tokens = SH.Split(item, " ").Length;
                    if (tokens < 8)
                    {
                        vseMa8 = false;
                    }
                }
            }

            foreach (string item in fse)
            {
                string fn = null;
                if (FtpHelper.IsFile(item, out fn) == FileSystemType.Folder)
                {
                    if (fn == dirName)
                    {
                        nalezenAdresar = true;
                        break;
                    }
                }
            }

            if (!nalezenAdresar)
            {
                if (mkdir(dirName))
                {
                    //this.remotePath = dirName;
                }
            }
            else
            {

                if (dirName == "..")
                {
                    ps.RemoveLastToken();
                }
                else
                {

                    ps.AddToken(dirName);
                }
            }
        }

        /// <summary>
        /// OK
        /// MKD
        /// Vytvoří v akt. složce A1 adresář A1 příkazem MKD
        /// </summary>
        /// <param name="dirName"></param>
        public override bool mkdir(string dirName)
        {
            if (pocetExc < maxPocetExc)
            {
                string adr = sunamo.UH.Combine(true, ps.ActualPath, dirName);

                OnNewStatus("Vytvářím adresář " + adr);
                FtpWebRequest reqFTP = null;
                FtpWebResponse response = null;
                Stream ftpStream = null;
                try
                {
                    // dirName = name of the directory to create.
                    Uri uri = new Uri(GetActualPath(dirName));
                    reqFTP = (FtpWebRequest)FtpWebRequest.Create(uri);
                    reqFTP.Method = WebRequestMethods.Ftp.MakeDirectory;
                    reqFTP.UseBinary = true;
                    reqFTP.Credentials = new NetworkCredential(remoteUser, remotePass);
                    response = (FtpWebResponse)reqFTP.GetResponse();
                    ftpStream = response.GetResponseStream();

                    ps.AddToken(dirName);

                    ftpStream.Dispose();
                    response.Dispose();
                    pocetExc = 0;
                    return true;
                }
                catch (Exception ex)
                {
                    if (ftpStream != null)
                    {
                        ftpStream.Dispose();
                    }
                    if (response != null)
                    {
                        response.Dispose();
                    }
                    pocetExc++;
                    OnNewStatus("Error create new dir: " + ex.Message);
                    return mkdir(dirName);

                }
                finally
                {
                    if (ftpStream != null)
                    {
                        ftpStream.Dispose();
                    }
                    if (response != null)
                    {
                        response.Dispose();
                    }
                }

            }
            else
            {
                pocetExc = 0;
                return false;
            }
        }

        /// <summary>
        /// OK
        /// LIST
        /// Vrátí složky, soubory i Linky
        /// </summary>
        /// <returns></returns>
        public override string[] ListDirectoryDetails()
        {
            List<string> vr = new List<string>();
            if (pocetExc < maxPocetExc)
            {
                StreamReader reader = null;
                FtpWebResponse response = null;

                String _Path = sunamo.UH.Combine(true, remoteHost + ":" + remotePort, ps.ActualPath);
                try
                {
                    // Get the object used to communicate with the server.
                    FtpWebRequest request = (FtpWebRequest)WebRequest.Create(_Path);
                    request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;

                    // This example assumes the FTP site uses anonymous logon.
                    request.Credentials = new NetworkCredential(remoteUser, remotePass);

                    response = (FtpWebResponse)request.GetResponse();

                    Stream responseStream = response.GetResponseStream();
                    reader = new StreamReader(responseStream, System.Text.Encoding.GetEncoding("windows-1250"));
                    if (reader != null)
                    {
                        while (!reader.EndOfStream)
                        {
                            string line = reader.ReadLine();
                            vr.Add(line);
                        }

                        reader.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    if (response != null)
                    {
                        response.Dispose();
                    }
                    pocetExc++;
                    OnNewStatus("Command LIST error: " + ex.Message);
                    return ListDirectoryDetails();
                }
                finally
                {
                    if (response != null)
                    {
                        response.Dispose();
                    }
                }
                pocetExc = 0;
                return vr.ToArray();
            }
            else
            {
                pocetExc = 0;
                return vr.ToArray();
            }
        }

        /// <summary>
        /// OK
        /// DELE
        /// Odstraním vzdálený soubor jména A1.
        /// </summary>
        /// <param name="fileName"></param>
        public override bool deleteRemoteFile(string fileName)
        {
            bool vr = true;
            if (pocetExc < maxPocetExc)
            {
                OnNewStatus("Odstraňuji ze ftp serveru soubor " + sunamo.UH.Combine(false, ps.ActualPath, fileName));
                FtpWebRequest reqFTP = null;
                StreamReader sr = null;
                Stream datastream = null;
                FtpWebResponse response = null;
                try
                {
                    reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(GetActualPath(fileName)));

                    reqFTP.Credentials = new NetworkCredential(remoteUser, remotePass);
                    reqFTP.KeepAlive = false;
                    reqFTP.Method = WebRequestMethods.Ftp.DeleteFile;

                    string result = String.Empty;
                    response = (FtpWebResponse)reqFTP.GetResponse();
                    long size = response.ContentLength;
                    datastream = response.GetResponseStream();
                    sr = new StreamReader(datastream);
                    result = sr.ReadToEnd();

                    sr.Dispose();
                    datastream.Dispose();
                    response.Dispose();
                }
                catch (Exception ex)
                {
                    //vr = false;
                    pocetExc++;
                    OnNewStatus("Error delete file: " + ex.Message);
                    if (sr != null)
                    {
                        sr.Dispose();
                    }
                    if (datastream != null)
                    {
                        datastream.Dispose();
                    }
                    if (response != null)
                    {
                        response.Dispose();
                    }

                    return deleteRemoteFile(fileName);
                }
                finally
                {
                    if (sr != null)
                    {
                        sr.Dispose();
                    }
                    if (datastream != null)
                    {
                        datastream.Dispose();
                    }
                    if (response != null)
                    {
                        response.Dispose();
                    }
                }
                pocetExc = 0;
                return vr;
            }
            else
            {
                pocetExc = 0;
                return false;
            }
        }
        /// <summary>
        /// OK
        /// SIZE
        /// Posílám příkaz SIZE. Pokud nejsem nalogovaný, přihlásím se.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public override long getFileSize(string fileName)
        {
            long fileSize = 0;
            if (pocetExc < maxPocetExc)
            {
                OnNewStatus("Pokouším se získat velikost souboru " + sunamo.UH.Combine(false, ps.ActualPath, fileName));

                FtpWebRequest reqFTP = null;
                Stream ftpStream = null;
                FtpWebResponse response = null;

                try
                {
                    reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(GetActualPath(fileName)));
                    reqFTP.Method = WebRequestMethods.Ftp.GetFileSize;
                    reqFTP.UseBinary = true;
                    reqFTP.Credentials = new NetworkCredential(remoteUser, remotePass);
                    response = (FtpWebResponse)reqFTP.GetResponse();
                    ftpStream = response.GetResponseStream();
                    fileSize = response.ContentLength;
                }
                catch (Exception ex)
                {
                    OnNewStatus("Error get filesize: " + ex.Message);
                    if (ftpStream != null)
                    {
                        ftpStream.Dispose();
                    }
                    if (response != null)
                    {
                        response.Dispose();
                    }
                    pocetExc++;
                    return getFileSize(fileName);
                }
                finally
                {
                    if (ftpStream != null)
                    {
                        ftpStream.Dispose();
                    }
                    if (response != null)
                    {
                        response.Dispose();
                    }
                }
                pocetExc = 0;
                return fileSize;
            }
            else
            {
                pocetExc = 0;
                return fileSize;
            }
        }

        /// <summary>
        /// OK
        /// RETR
        /// Stáhne soubor A1 do lok. souboru A2. Navazuje pokud A3.
        /// Pokud A2 bude null, M vyhodí výjimku
        /// Pokud neexistuje, vytvořím jej a hned zavřu. Načtu jej do FS s FileMode Open
        /// Pokud otevřený soubor nemá velikost 0, pošlu příkaz REST čímž nastavím offset
        /// Pokud budeme navazovat, posunu v otevřeném souboru na konec
        /// Pošlu příkaz RETR a všechny přijaté bajty zapíšu
        /// </summary>
        /// <param name="remFileName"></param>
        /// <param name="locFileName"></param>
        /// <param name="resume"></param>
        public override bool download(string remFileName, string locFileName, Boolean deleteLocalIfExists)
        {
            if (!FtpHelper.IsSchemaFtp(remFileName))
            {
                remFileName = GetActualPath(remFileName);
            }

            if (string.IsNullOrEmpty(locFileName))
            {
                OnNewStatus("Do metody download byl předán prázdný parametr locFileName");
                return false;
            }

            OnNewStatus("Stahuji " + remFileName);

            if (File.Exists(locFileName))
            {
                if (deleteLocalIfExists)
                {
                    try
                    {
                        File.Delete(locFileName);
                    }
                    catch (Exception)
                    {
                        OnNewStatus("Soubor " + remFileName + " nemohl být stažen, protože soubor " + locFileName + " nešel smazat");
                        return false;
                    }
                }
                else
                {
                    OnNewStatus("Soubor " + remFileName + " nemohl být stažen, protože soubor " + locFileName + " existoval již na disku a nebylo povoleno jeho smazání");
                    return false;
                }
            }

            if (pocetExc < maxPocetExc)
            {

                FtpWebRequest reqFTP = null;
                Stream ftpStream = null;
                FileStream outputStream = null;
                FtpWebResponse response = null;

                try
                {
                    outputStream = new FileStream(locFileName, FileMode.Create);

                    reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(remFileName));
                    reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
                    reqFTP.UseBinary = true;
                    reqFTP.Credentials = new NetworkCredential(remoteUser, remotePass);
                    response = (FtpWebResponse)reqFTP.GetResponse();
                    ftpStream = response.GetResponseStream();
                    long cl = response.ContentLength;
                    int bufferSize = 2048;
                    int readCount;
                    byte[] buffer = new byte[bufferSize];

                    readCount = ftpStream.Read(buffer, 0, bufferSize);
                    while (readCount > 0)
                    {
                        outputStream.Write(buffer, 0, readCount);
                        readCount = ftpStream.Read(buffer, 0, bufferSize);
                    }
                }
                catch (Exception ex)
                {
                    OnNewStatus("Error download file: " + ex.Message);
                    if (ftpStream != null)
                    {
                        ftpStream.Dispose();
                    }
                    if (outputStream != null)
                    {
                        outputStream.Dispose();
                    }
                    if (response != null)
                    {
                        response.Dispose();
                    }
                    pocetExc++;
                    return download(remFileName, locFileName, deleteLocalIfExists);
                }
                finally
                {
                    if (ftpStream != null)
                    {
                        ftpStream.Dispose();
                    }
                    if (outputStream != null)
                    {
                        outputStream.Dispose();
                    }
                    if (response != null)
                    {
                        response.Dispose();
                    }
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

        /// <summary>
        /// OK
        /// LIST
        /// Toto je vstupní metoda, metodu getFSEntriesListRecursively s 5ti parametry nevolej, ač má stejný název
        /// Vrátí soubory i složky, ale pozor, složky jsou vždycky až po souborech
        /// </summary>
        /// <param name="slozkyNeuploadovatAVS"></param>
        /// <returns></returns>
        public override Dictionary<string, List<string>> getFSEntriesListRecursively(List<string> slozkyNeuploadovatAVS)
        {

            // Musí se do ní ukládat cesta k celé složce, nikoliv jen název aktuální složky
            List<string> projeteSlozky = new List<string>();
            Dictionary<string, List<string>> vr = new Dictionary<string, List<string>>();
            string[] fse = ListDirectoryDetails();

            string actualPath = ps.ActualPath;
            OnNewStatus("Získávám rekurzivní seznam souborů ze složky " + actualPath);
            foreach (string item in fse)
            {
                char fz = item[0];
                if (fz == '-')
                {
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
                    string folderName = SH.JoinFromIndex(8, ' ', SH.Split(item, " "));

                    if (!FtpHelper.IsThisOrUp(folderName))
                    {
                        if (vr.ContainsKey(actualPath))
                        {
                            vr[actualPath].Add(item + "/");
                        }
                        else
                        {
                            List<string> ppk = new List<string>();
                            ppk.Add(item + "/");
                            vr.Add(actualPath, ppk);
                        }
                        base.getFSEntriesListRecursively(slozkyNeuploadovatAVS, projeteSlozky, vr, ps.ActualPath, folderName);
                    }
                }
                else
                {
                    throw new Exception("Nepodporovaný typ objektu");
                }
            }
            return vr;
        }

        /// <summary>
        /// OK
        /// Tuto metodu nepoužívej, protože fakticky způsobuje neošetřenou výjimku, pokud již cesta bude skutečně / a a nebude moci se přesunout nikde výš
        /// </summary>
        public override void goToUpFolderForce()
        {
            OnNewStatus("Přecházím do nadsložky " + ps.ActualPath);
            ps.RemoveLastTokenForce();
            OnNewStatusNewFolder();
        }
        /// <summary>
        /// OK
        /// </summary>
        public override void goToUpFolder()
        {
            if (ps.CanGoToUpFolder)
            {
                ps.RemoveLastToken();
                OnNewStatusNewFolder();
            }
            else
            {
                OnNewStatus("Nemohl jsem přejít do nadsložky.");
            }
        }

        public override void DebugActualFolder()
        {
            throw new NotImplementedException();
        }

        public override void D(string what, string text, params object[] args)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}

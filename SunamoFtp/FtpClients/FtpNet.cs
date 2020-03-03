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
using SunamoFtp.Other;

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
            if (FtpLogging.GoToFolder)
            {
                OnNewStatus("Přecházím do složky" + " " + remoteFolder);
            }
            
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
                    var tokens = SH.Split(remoteFolder, ps.Delimiter);
                    foreach (string item in tokens)
                    {
                        CreateDirectoryIfNotExists(item);
                    }
                }
                // Vzdálená složka nezačíná aktuální cestou, 
                else
                {
                    ps.ActualPath = "";
                    var tokens = SH.Split(remoteFolder, ps.Delimiter);
                    int pridat = 0;
                    for (int i = 0 + pridat; i < tokens.Count; i++)
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
            OnNewStatus("Ve složce" + " " + ps.ActualPath + " " + "přejmenovávám soubor" + " " + "" + " " + oldFileName + " na " + newFileName);

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
                    OnNewStatus("Error rename file" + ": " + ex.Message);
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
        
        public override Dictionary<string, List<string>> getFSEntriesListRecursively(List<string> slozkyNeuploadovatAVS)
        {

            // Musí se do ní ukládat cesta k celé složce, nikoliv jen název aktuální složky
            List<string> projeteSlozky = new List<string>();
            Dictionary<string, List<string>> vr = new Dictionary<string, List<string>>();
            List<string> fse = ListDirectoryDetails();

            string actualPath = ps.ActualPath;
            OnNewStatus("Získávám rekurzivní seznam souborů ze složky" + " " + actualPath);
            foreach (string item in fse)
            {
                char fz = item[0];
                if (fz == AllChars.dash)
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
                    string folderName = SH.JoinFromIndex(8, AllChars.space, SH.Split(item, AllStrings.space));

                    if (!FtpHelper.IsThisOrUp(folderName))
                    {
                        if (vr.ContainsKey(actualPath))
                        {
                            vr[actualPath].Add(item + AllStrings.slash);
                        }
                        else
                        {
                            List<string> ppk = new List<string>();
                            ppk.Add(item + AllStrings.slash);
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
            if (FtpLogging.GoToUpFolder)
            {
                OnNewStatus("Přecházím do nadsložky" + " " + ps.ActualPath);
            }
            
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
                OnNewStatus("Nemohl jsem přejít do nadsložky" + ".");
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

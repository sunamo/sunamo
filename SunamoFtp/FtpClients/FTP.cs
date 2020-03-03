using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Security.Permissions;
using System.Threading;
using System.IO;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Globalization;
using System.Diagnostics;
using System.Windows;
using sunamo;
using Sunamo.Data;
using SunamoFtp.Other;

namespace SunamoFtp
{
    public class FTP : FtpBase
    {
        /// <summary>
        /// aktuální vzdálený adresář.
        /// </summary>
        //string remotePath;
        string remotePath
        {
            get
            {
                return ps.ActualPath;
            }
            set
            {
            }
        }

        string mes;
        /// <summary>
        /// 
        /// </summary>
        private int bytes;
        /// <summary>
        /// 
        /// </summary>
        private Socket clientSocket;
        /// <summary>
        /// Hodnotu kterou ukládá třeba readReply, který volá třeba sendCommand
        /// </summary>
        private int retValue;
        /// <summary>
        /// Zda se vypisují příkazy na konzoli.
        /// </summary>
        private Boolean debug;
        private string reply;
        /// <summary>
        /// Zda se používá PP stream(binární přenos) místo clientSocket(ascii převod)
        /// </summary>
        private bool useStream;
        private bool isUpload;
        /// <summary>
        /// Stream který se používá při downloadu.
        /// </summary>
        private Stream stream = null;
        /// <summary>
        /// Stream který se používá při uploadu a to tak že do něho zapíšu jeho M Write
        /// </summary>
        private Stream stream2 = null;

        /// <summary>
        /// Velikost bloku po které se čte.
        /// </summary>
        private static int BLOCK_SIZE = 1024;
        /// <summary>
        /// Buffer je pouhý 1KB
        /// </summary>
        Byte[] buffer = new Byte[BLOCK_SIZE];
        /// <summary>
        /// Konstanta obsahující ASCII kódování 
        /// </summary>
        readonly Encoding ASCII = Encoding.ASCII;
        IFtpClientExt ftpClient = null;
        /// <summary>
        /// IK, OOP.
        /// </summary>
        public FTP(IFtpClientExt ftpClient)
        {
            this.ftpClient = ftpClient;
            debug = false;
        }


        /// <summary>
        /// Nastaví zda se používá binární přenos.
        /// </summary>
        /// <param name="value"></param>
        public void setUseStream(bool value)
        {
            this.useStream = value;
        }





        /// <summary>
        /// S PP remotePath na A1
        /// </summary>
        /// <param name="remotePath"></param>
        public void setRemotePath(string remotePath)
        {
            OnNewStatus("Byl nastavena cesta ftp na" + " " + remotePath);
            if (remotePath == ftpClient.WwwSlash)
            {
                if (ps.ActualPath != ftpClient.WwwSlash)
                {


                    while (ps.CanGoToUpFolder)
                    {
                        //ps.RemoveLastToken();
                        goToUpFolder();
                    }
                }
                //chdirLite("www");
            }
            else
            {
                ps.ActualPath = remotePath;
            }
        }

        /// <summary>
        /// G aktuální vzdálený adresář.
        /// </summary>
        
        public Socket createDataSocket()
        {
            #region Nastavím pasivní způsob přenosu(příkaz PASV)
            sendCommand("PASV");

            if (retValue != 227)
            {
                throw new IOException(reply.Substring(4));
            }
            #endregion

            #region Získám IP adresu v řetězci z reply
            int index1 = reply.IndexOf(AllChars.lb);
            int index2 = reply.IndexOf(AllChars.rb);
            string ipData = reply.Substring(index1 + 1, index2 - index1 - 1);
            int[] parts = new int[6];

            int len = ipData.Length;
            int partCount = 0;
            string buf = "";
            #endregion

            #region Získám do pole intů jednotlivé části IP adresy a spojím je do řetězce s tečkama
            for (int i = 0; i < len && partCount <= 6; i++)
            {

                char ch = Char.Parse(ipData.Substring(i, 1));
                if (Char.IsDigit(ch))
                    buf += ch;
                else if (ch != AllChars.comma)
                {
                    throw new IOException("Malformed PASV reply" + ": " + reply);
                }

                #region Pokud je poslední znak čárka,
                if (ch == AllChars.comma || i + 1 == len)
                {

                    try
                    {
                        parts[partCount++] = Int32.Parse(buf);
                        buf = "";
                    }
                    catch (Exception ex)
                    {
                        throw new IOException("Malformed PASV reply" + ": " + reply);
                    }
                }
                #endregion
            }

            string ipAddress = parts[0] + AllStrings.dot + parts[1] + AllStrings.dot +
              parts[2] + AllStrings.dot + parts[3];
            #endregion

            #region Port získám tak čtvrtou část ip adresy bitově posunu o 8 a sečtu s pátou částí. Získám Socket, O IPEndPoint a pokusím se připojit na tento objekt.
            int port = (parts[4] << 8) + parts[5];

            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ep = new IPEndPoint(Dns.Resolve(ipAddress).AddressList[0], port);

            try
            {
                s.Connect(ep);
            }
            catch (Exception ex)
            {
                throw new IOException("Can't connect to remoteserver");
            }

            return s;
            #endregion
        }


        public void uploadSecureFolder(string slozkaTo)
        {
            OnNewStatus("Byla volána metoda uploadSecureFolder která je prázdná");
            // Zkontrolovat zda se první nauploadoval _.txt
        }






        #region OK metody
        /// <summary>
        /// OK
        /// </summary>
        /// <param name="slozkyNeuploadovatAVS"></param>
        /// <param name="dirName"></param>
        /// <param name="i"></param>
        /// <param name="td"></param>
        public override void DeleteRecursively(List<string> slozkyNeuploadovatAVS, string dirName, int i, List<DirectoriesToDelete> td)
        {
            chdirLite(dirName);
            List<string> smazat = ListDirectoryDetails();
            foreach (var item2 in smazat)
            {
                FileSystemType fst = FtpHelper.IsFile(item2, out string fn);
                if (fst == FileSystemType.File)
                {
                    deleteRemoteFile(fn);
                }
                else if (fst == FileSystemType.Folder)
                {
                    DeleteRecursively(slozkyNeuploadovatAVS, fn, i, td);
                }
                //////DebugLogger.Instance.WriteLine(item2);
            }
            goToUpFolderForce();
            rmdir(slozkyNeuploadovatAVS, dirName);
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

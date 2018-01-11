using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;


    public class PH
    {
        public static void SaveAndOpenInBrowser(Browsers prohlizec, string htmlKod)
        {
            string s = Path.GetTempFileName() + ".html";
            File.WriteAllText(s, htmlKod);
            OpenInBrowser(prohlizec, s);
        }

        public static void OpenInBrowser(Browsers prohlizec, string s)
        {
            string b = "";
            switch (prohlizec)
            {
                case Browsers.Chrome:
                    b = @"C:\Users\sunamo\AppData\Local\Google\Chrome\Application\chrome.exe";
                    break;
                case Browsers.Firefox:
                    b = @"C:\Program Files (x86)\Mozilla Firefox\firefox.exe";
                    break;
                case Browsers.Opera:
                    b = @"C:\Program Files (x86)\Opera\opera.exe";
                    break;
                case Browsers.IE:
                    b = @"C:\Program Files (x86)\Internet Explorer\iexplore.exe";
                    break;
                default:
                    throw new Exception("Neimplementovan� prohl�e�");
                    break;
            }
            Process.Start(new ProcessStartInfo(b, s));
        }
    }

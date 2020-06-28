using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sunamo.Helpers;

public class PHWin
{
    static Type type = typeof(PHWin);
    public static void SaveAndOpenInBrowser(Browsers prohlizec, string htmlKod)
    {
        string s = Path.GetTempFileName() + ".html";
        File.WriteAllText(s, htmlKod);
        OpenInBrowser(prohlizec, s);
    }

    public static bool IsUsed(string fullPath)
    {
        return FileUtil.WhoIsLocking(fullPath).Count > 0;
    }

    static int open = 0;

    public static void AddBrowsers()
    {
        if (path.Count == 0)
        {
            var all = EnumHelper.GetValues<Browsers>();
            foreach (var item in all)
            {
                AddBrowser(item);
            }
        }
    }

    static void AddBrowser(Browsers prohlizec)
    {
        string b = string.Empty;
        switch (prohlizec)
        {
            case Browsers.Chrome:
                b = @"c:\Program Files (x86)\Google\Chrome\Application\chrome.exe";
                break;
            case Browsers.Firefox:
                b = @"c:\Program Files (x86)\Mozilla Firefox\firefox.exe";
                break;
            case Browsers.InternetExplorer:
                b = @"c:\Program Files (x86)\Internet Explorer\iexplore.exe";
                break;
            case Browsers.Opera:
                // Opera has version also when is installing to PF, it cant be changed
                b = @"C:\Program Files\Opera\65.0.3467.78\opera.exe";
                break;
            case Browsers.Edge:
                b = @"c:\Windows\SystemApps\Microsoft.MicrosoftEdge_8wekyb3d8bbwe\MicrosoftEdge.exe";
                break;
            case Browsers.Vivaldi:
                b = WindowsOSHelper.FileIn(UserFoldersWin.Local, XlfKeys.Vivaldi, "vivaldi.exe");
                break;
            case Browsers.ChromeCanary:
                b = WindowsOSHelper.FileIn(UserFoldersWin.Local, @"Google\Chrome SxS", "chrome.exe");
                break;
            default:

                //ThrowExceptions.NotImplementedCase(Exc.GetStackTrace(),type, Exc.CallingMethod());
                break;
        }

        path.Add(prohlizec, b);
    }

    public static void OpenInBrowser(Browsers prohlizec, string s)
    {
        open++;
        string b = path[prohlizec];


        if (open % 10 == 0)
        {
            Debugger.Break();
        }

        Process.Start(new ProcessStartInfo(b, PH.NormalizeUri(s)));
    }

    static Dictionary<Browsers, string> path = new Dictionary<Browsers, string>();
    public static void OpenInBrowser(string uri)
    {
        OpenInBrowser(Browsers.Chrome, uri);
    }

}
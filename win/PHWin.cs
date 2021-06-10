using System;
using System.Collections;
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

    static int opened = 0;

    /// <summary>
    /// A1 is chrome replacement
    /// </summary>
    /// <param name="array"></param>
    /// <param name="what"></param>
    public static void SearchInAll(IEnumerable array, string what)
    {
        foreach (var item in array)
        {
            opened++;
            string uri = UriWebServices.FromChromeReplacement(item.ToString(), what);
            PHWin.OpenInBrowser(uri);
            if (opened % 10 == 0)
            {
                Debugger.Break();
            }
        }
    }

    public static void AddBrowsers()
    {
        if (path.Count == 0)
        {
            var all = EnumHelper.GetValues<Browsers>();
            foreach (var item in all)
            {
                if (item != Browsers.None)
                {
                    AddBrowser(item);
                }
            }
        }
    }

    public static void AssignSearchInAll()
    {
        AddBrowsers();
        UriWebServices.AssignSearchInAll(PHWin.SearchInAll);
    }

    public static string AddBrowser(Browsers prohlizec)
    {
        string b = string.Empty;

        switch (prohlizec)
        {
            case Browsers.Chrome:
                b = @"c:\Program Files\Google\Chrome\Application\chrome.exe";
                break;
            case Browsers.Firefox:
                b = @"c:\Program Files (x86)\Mozilla Firefox\firefox.exe";
                if (!FS.ExistsFile(b))
                {
                    b = @"c:\Program Files\Mozilla Firefox\firefox.exe";
                }
                break;
            case Browsers.Edge:
                b = @"c:\Windows\SystemApps\Microsoft.MicrosoftEdge_8wekyb3d8bbwe\MicrosoftEdge.exe";
                
                if (!FS.ExistsFile(b))
                {
                    //c:\Users\Administrator\AppData\Local\Microsoft\WindowsApps\Microsoft.MicrosoftEdge_8wekyb3d8bbwe\MicrosoftEdge.exe
                    b = WindowsOSHelper.FileIn(UserFoldersWin.Local, @"Microsoft\WindowsApps\Microsoft.MicrosoftEdge_8wekyb3d8bbwe", "MicrosoftEdge.exe");
                }

                break;
            case Browsers.Opera:
                // Opera has version also when is installing to PF, it cant be changed
                //b = @"C:\Program Files\Opera\65.0.3467.78\opera.exe";
                b = WindowsOSHelper.FileIn(@"C:\Program Files\Opera\", "opera.exe");
                if (!FS.ExistsFile(b))
                {
                    b = WindowsOSHelper.FileIn(UserFoldersWin.Local, @"Programs\Opera", "opera.exe");
                }
                break;
            case Browsers.Vivaldi:
                b = @"C:\Program Files\Vivaldi\Application\vivaldi.exe";
                if (!FS.ExistsFile(b))
                {
                    b = WindowsOSHelper.FileIn(UserFoldersWin.Local, XlfKeys.Vivaldi, "vivaldi.exe");
                }
                break;
            //case Browsers.InternetExplorer:
            //    b = @"c:\Program Files (x86)\Internet Explorer\iexplore.exe";
            //    break;
            case Browsers.Maxthon:
                b = @"C:\Program Files (x86)\Maxthon5\Bin\Maxthon.exe";
                if (!FS.ExistsFile(b))
                {
                    b = WindowsOSHelper.FileIn(UserFoldersWin.Local, @"Maxthon\Application", "Maxthon.exe");
                }
                break;
            case Browsers.Seznam:
                b = WindowsOSHelper.FileIn(UserFoldersWin.Roaming, @"Seznam Browser", "Seznam.cz.exe");
                break;
            case Browsers.Chromium:
                b = @"d:\paSync\_browsers\Chromium\chrome.exe";
                break;
            case Browsers.ChromeCanary:
                b = WindowsOSHelper.FileIn(UserFoldersWin.Local, @"Google\Chrome SxS", "chrome.exe");
                break;
            case Browsers.Tor:
                b = @"d:\Desktop\Tor Browser\Browser\firefox.exe";
                break;
            case Browsers.Bravebrowser:
                b = @"C:\Program Files\BraveSoftware\Brave-Browser\Application\brave.exe";
                break;
            case Browsers.Torch:
                b = WindowsOSHelper.FileIn(UserFoldersWin.Local, @"Torch\Application", "torch.exe");
                break;
            default:
                ThrowExceptions.NotImplementedCase(Exc.GetStackTrace(),type, Exc.CallingMethod(), prohlizec);
                break;
        }

        if (b == null)
        {
            b = string.Empty;
        }

        if (b == string.Empty)
        {

        }

        path.Add(prohlizec, b);

        return b;
    }

    public static void OpenInBrowser(Browsers prohlizec, string s)
    {
        opened++;
        string b = path[prohlizec];
        if (opened % 10 == 0)
        {
            Debugger.Break();
        }
        s = PH.NormalizeUri(s);

        if (!UH.HasHttpProtocol(s))
        {
            s = SH.WrapWithQm(s);
        }; ;

        Process.Start(new ProcessStartInfo(b, s));
    }

    /// <summary>
    /// Not contains Other
    /// </summary>
    static Dictionary<Browsers, string> path = new Dictionary<Browsers, string>();
    public static void OpenInBrowser(string uri)
    {
        OpenInBrowser(Browsers.Chrome, uri);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public static List<string> BrowsersWhichDontHaveExeInDefinedPath()
    {
        List<string> doesntExists = new List<string>();

        AddBrowsers();
        foreach (var item in path)
        {
            if (!FS.ExistsFile( item.Value))
            {

                doesntExists.Add(item.Value);
            }
        }

        return doesntExists;
    }

    public static void OpenInAllBrowsers(string uri)
    {
        AddBrowsers();
        foreach (var item in path)
        {
            OpenInBrowser(item.Key, uri);
        }
    }

    public static void OpenFolder(string folder)
    {
        Process.Start(Environment.GetEnvironmentVariable("WINDIR") + @"\explorer.exe", folder);
    }
}
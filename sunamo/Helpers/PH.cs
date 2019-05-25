using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using sunamo;
using System.Linq;

public class PH
    {
    public static void Start(string p)
    {
        try
        {
            Process.Start(p);
        }
        catch (Exception)
        {

        }
    }

    public static void StartAllUri(List<string> all)
    {
        foreach (var item in all)
        {
            Uri(UH.AppendHttpIfNotExists(item));
        }
    }

    public static List<string> GetProcessesNames(bool lower)
    {
        var p = Process.GetProcesses().Select(d=> d.ProcessName).ToList();
        if (lower)
        {
            CA.ToLower(p);
        }
        return p;
    }

    /// <summary>
    /// For search one term in all uris use UriWebServices.SearchInAll
    /// </summary>
    /// <param name="carModels"></param>
    /// <param name="v"></param>
    public static void StartAllUri(List<string> carModels, string v)
    {
        foreach (var item in carModels)
        {
            Uri(UH.AppendHttpIfNotExists(UriWebServices.FromChromeReplacement(v, item)));
        }
    }

    public static void StartAllUri(List<string> carModels, Func<string, string> spritMonitor)
    {
        carModels = CA.ChangeContent(carModels, spritMonitor);
        carModels = CA.ChangeContent(carModels, NormalizeUri);
        StartAllUri(carModels);
    }

    private static string NormalizeUri(string v)
    {
        // Without this cant search for google apps
        v = SH.ReplaceAll(v, "%22", AllStrings.qm);
        return v;
    }

    public static void Uri(string v)
    {
        v = NormalizeUri(v);
        v = v.Trim();
        if (System.Uri.IsWellFormedUriString(v, UriKind.Absolute))
        {
            Process.Start(v);
        }
        else
        {
            DebugLogger.Instance.WriteLine("Wasnt in right format: " + v);
        }
    }

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
                    b = @"c:\Program Files (x86)\Google\Chrome\Application\chrome.exe";
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
            Process.Start(new ProcessStartInfo(b, NormalizeUri( s)));
        }

    /// <summary>
    /// A1 without extension
    /// </summary>
    /// <param name="name"></param>
    public static int Terminate(string name)
    {
        int deleted = 0;
        foreach (var process in Process.GetProcessesByName(name))
        {
            process.Kill();
            deleted++;
        }
        return deleted;
    }

    /// <summary>
    /// Start all uri in clipboard, splitted by whitespace
    /// </summary>
    public static void StartAllUri()
    {
        var text = ClipboardHelper.GetText();
        var uris = SH.SplitByWhiteSpaces(text);
        StartAllUri(uris);
    }

    public static void OpenInBrowser(string uri)
    {
         OpenInBrowser(Browsers.Chrome, uri);
    }

    public static bool IsAlreadyRunning(string name)
    {
        var pr = Process.GetProcessesByName(name).Select(d => d.ProcessName);
        //var processes = Process.GetProcesses(name).Where(s => s.ProcessName.Contains(name)).Select(d => d.ProcessName);
        return pr.Count() > 1;
    }
}

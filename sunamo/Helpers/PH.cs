using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using sunamo;
using System.Linq;
using System.Web;

public partial class PH
{
    public static void StartAllUri(List<string> all)
    {
        foreach (var item in all)
        {
            Uri(UH.AppendHttpIfNotExists(item));
        }
    }

    public static List<string> GetProcessesNames(bool lower)
    {
        var p = Process.GetProcesses().Select(d => d.ProcessName).ToList();
        if (lower)
        {
            CA.ToLower(p);
        }

        return p;
    }

    /// <summary>
    /// For search one term in all uris use UriWebServices.SearchInAll
    /// </summary>
    /// <param name = "carModels"></param>
    /// <param name = "v"></param>
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

    public static void SaveAndOpenInBrowser(Browsers prohlizec, string htmlKod)
    {
        string s = Path.GetTempFileName() + ".html";
        File.WriteAllText(s, htmlKod);
        OpenInBrowser(prohlizec, s);
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
}
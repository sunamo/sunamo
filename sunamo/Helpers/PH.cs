using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using sunamo;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

public partial class PH
{
    public static void ShutdownProcessWhichOccupyFileHandleExe(string fileName)
    {
        Process tool = new Process();
        tool.StartInfo.FileName = "handle64.exe";
        tool.StartInfo.Arguments = fileName + " /accepteula";
        tool.StartInfo.UseShellExecute = false;
        tool.StartInfo.RedirectStandardOutput = true;
        tool.Start();
        tool.WaitForExit();
        string outputTool = tool.StandardOutput.ReadToEnd();

        string matchPattern = @"(?<=\s+pid:\s+)\b(\d+)\b(?=\s+)";
        var matches = Regex.Matches(outputTool, matchPattern);
        foreach (Match match in matches)
        {
            var pr = Process.GetProcessById(int.Parse(match.Value));
            KillProcess(pr);
        }
    }

    public static void KillProcess(Process pr)
    {
        try
        {
            pr.Kill();
        }
        catch (Exception ex)
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
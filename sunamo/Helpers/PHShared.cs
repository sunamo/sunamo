﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using sunamo;
using System.Linq;
using System.Web;

public partial class PH
{
    static Type type = typeof(PH);

    public static void Start(string p)
    {
        try
        {
            Process.Start("cmd.exe", "/c " + p);
        }
        catch (Exception ex)
        {
            ThrowExceptions.CustomWithStackTrace(ex);
        }
        
    }

    public static void Start(string p, string arguments)
    {
        try
        {
            Process.Start(p, arguments);
        }
        catch (Exception ex)
        {
            ThrowExceptions.CustomWithStackTrace(ex);
        }

    }

    

    public static void StartHidden(string p, string k)
    {
        try
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.WorkingDirectory = k;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/C " + p;
            process.StartInfo = startInfo;
            process.Start();
        }
        catch (Exception ex)
        {
            ThrowExceptions.CustomWithStackTrace(ex);
        }
    }

public static bool IsAlreadyRunning(string name)
    {
        var pr = Process.GetProcessesByName(name).Select(d => d.ProcessName);
        //var processes = Process.GetProcesses(name).Where(s => s.ProcessName.Contains(name)).Select(d => d.ProcessName);
        return pr.Count() > 1;
    }

public static void Uri(string v)
    {
        v = NormalizeUri(v);
        v = v.Trim();
        //Must UrlDecode for https://mapy.cz/?q=Antala+Sta%c5%a1ka+1087%2f3%2c+Hav%c3%ad%c5%99ov&sourceid=Searchmodule_1
        // to fulfillment RFC 3986 and RFC 3987 https://docs.microsoft.com/en-us/dotnet/api/system.uri.iswellformeduristring?view=netframework-4.8
        v = HttpUtility.UrlDecode(v);
        if (System.Uri.IsWellFormedUriString( v, UriKind.RelativeOrAbsolute))
        {
            Process.Start(v);
        }
        else
        {
            //////////DebugLogger.Instance.WriteLine("Wasnt in right format: " + v);
        }
    }

public static string NormalizeUri(string v)
    {
        // Without this cant search for google apps
        v = SH.ReplaceAll(v, "%22", AllStrings.qm);
        return v;
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

    public static int Terminate(string name)
    {
        int deleted = 0;

        foreach (var process in Process.GetProcessesByName(name))
        {
            PH.KillProcess(process);
            deleted++;
        }

        return deleted;
    }
}
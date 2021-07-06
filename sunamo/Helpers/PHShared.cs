using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using sunamo;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using System.Collections.Specialized;

public partial class PH
{
    static Type type = typeof(PH);

    public static bool ExecCmd(string cmd)
    {
        String output;
        var b = ExecCmd(cmd, out output);
        return b;
    }

    /// <summary>
    /// Executes command
    /// </summary>
    /// <param name="cmd">command to be executed</param>
    /// <param name="output">output which application produced</param>
    /// <param name="transferEnvVars">true - if retain PATH environment variable from executed command</param>
    /// <returns>true if process exited with code 0</returns>
    public static bool ExecCmd(string cmd, out String output, bool transferEnvVars = false)
    {
        ProcessStartInfo processInfo;

        if (transferEnvVars)
            cmd = cmd + " && echo --VARS-- && set";

        processInfo = new ProcessStartInfo("cmd.exe", "/c " + cmd);
        processInfo.CreateNoWindow = true;
        processInfo.UseShellExecute = false;
        processInfo.RedirectStandardError = true;
        processInfo.RedirectStandardOutput = true;


        output = RunWithOutput(processInfo, transferEnvVars);
        return string.IsNullOrEmpty(output);
    }

    public static string RunWithOutput(string exe, string arguments)
    {
        return RunWithOutput(new ProcessStartInfo { FileName = exe, Arguments = arguments, UseShellExecute = false });
    }

    public static string RunWithOutput(ProcessStartInfo processInfo, bool transferEnvVars = false)
    {
        Process process;
        process = new Process();
        string output = null;

        processInfo.RedirectStandardError = true;
        processInfo.RedirectStandardOutput = true;
        processInfo.CreateNoWindow = true;
        processInfo.UseShellExecute = false;


        // Executing long lasting operation in batch file will hang the process, as it will wait standard output / error pipes to be processed.
        // We process these pipes here asynchronously.
        StringBuilder so = new StringBuilder();
        process.OutputDataReceived += (sender, args) => { so.AppendLine(args.Data); };
        StringBuilder se = new StringBuilder();
        process.ErrorDataReceived += (sender, args) => { se.AppendLine(args.Data); };

        process.StartInfo = processInfo;
        process.Start();

        process.BeginOutputReadLine();
        process.BeginErrorReadLine();

        

        process.WaitForExit();

        output = so.ToString();
        String error = se.ToString();

        if (transferEnvVars)
        {
            Regex r = new Regex("--VARS--(.*)", RegexOptions.Singleline);
            var m = r.Match(output);
            if (m.Success)
            {
                output = r.Replace(output, "");

                foreach (Match m2 in new Regex("(.*?)=([^\r]*)", RegexOptions.Multiline).Matches(m.Groups[1].ToString()))
                {
                    String key = m2.Groups[1].Value;
                    String value = m2.Groups[2].Value;
                    Environment.SetEnvironmentVariable(key, value);
                }
            }
        }

        if (error.Length != 0)
            output += error;
        int exitCode = process.ExitCode;

        if (exitCode != 0)
            Console.WriteLine("Error: " + output + "\r\n" + error);

        process.Close();
        //return exitCode == 0;

        return output;
    }

    /// <summary>
    /// Exe must be in path
    /// </summary>
    /// <param name="p"></param>
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
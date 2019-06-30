using System.Collections.Generic;
using System.Diagnostics;
using System.Management.Automation;
using System.Text;

namespace win.Helpers.Powershell
{
    public class PowershellHelper
    {
        public static List<string> ProcessNames()
        {
            List<string> processNames = new List<string>();
            PowerShell ps = PowerShell.Create();
            ps.AddCommand("Get-Process");
            var processes = ps.Invoke();
            foreach (var item in processes)
            {
                Process process = (Process)item.BaseObject;
                processNames.Add(process.ProcessName);
            }
            return processNames;
        }

        public async static void CmdC(string v)
        {
            PowershellBuilder ps = PowershellBuilder.GetInstance();
            ps.CmdC(v);
            await PowershellRunner.InvokeAsync(ps.ToList());
        }

    const string lang = "language" + ":";

    public static string DetectLanguageForFileGithubLinguist(string windowsPath)
    {
        string command = null;
        string arguments = null;

        // With WSL or WSL 2 not working. In both cases Powershell returns right values but in c# everything empty. Asked on StackOverflow
        StringBuilder linuxPath = new StringBuilder();
        linuxPath.Append("/" + "mnt" + "/");
        linuxPath.Append(windowsPath[0].ToString().ToLower());
        var parts = SH.Split(windowsPath, AllStrings.bs);
        for (int i = 1; i < parts.Count; i++)
        {
            linuxPath.Append("/" + parts[i]);
        }

        command = "wsl";
        arguments = " bash -c \"github-linguist '" + linuxPath + "'\"";

        var lines = PowershellRunner.InvokeProcess(command + ".exe", arguments);

        var line = CA.ReturnWhichContains(lines, lang).FirstOrNull();
        if (line == null)
        {
            return null;
        }
        var result = line.ToString().Replace(lang, string.Empty).Trim();
        return result;
    }
    }
}
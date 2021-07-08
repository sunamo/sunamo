using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public partial class PH
{
    

    public static string RunFromPath2(string exe, string arguments)
    {
        // Commented due to 'StringDictionary' does not contain a definition for 'Replace'

        //ProcessStartInfo psi = new ProcessStartInfo(exe);
        //psi.Arguments = arguments;
        //StringDictionary dictionary = psi.EnvironmentVariables;

        //// Manipulate dictionary...\

        //psi.EnvironmentVariables["PATH"] = dictionary.Replace(@"\\", @"\");
        //return RunWithOutput(exe, arguments);

        return null;
    }

    /// <summary>
    /// Exe must be in path
    /// 
    /// </summary>
    /// <param name="exe"></param>
    /// <param name="fileWithoutQm"></param>
    public static void RunFromPath3(string exe, string fileWithoutQm)
    {
        ProcessStartInfo pi = new ProcessStartInfo();
        pi.FileName = exe;
        pi.Arguments = SH.WrapWithQm(fileWithoutQm);
        // To use env variables
        pi.UseShellExecute = true;
        Process.Start(pi);

        //var cmd = exe + AllStrings.space + SH.WrapWithQm(fileWithoutQm);
        //Process.Start(@"C:\Windows\System32\cmd.exe", "/c " + cmd);
        //PH.ExecCmd(cmd);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="exe"></param>
    /// <param name="arguments"></param>
    /// <returns></returns>
    public static string RunFromPathBetter(string exe, string arguments)
    {
        string enviromentPath = System.Environment.GetEnvironmentVariable("PATH");
        string[] paths = enviromentPath.Split(';');

        foreach (string thisPath in paths)
        {
            string thisFile = System.IO.Path.Combine(thisPath, exe);
            string[] executableExtensions = new string[] { ".exe" }; // , ".com", ".bat", ".sh", ".vbs", ".vbscript", ".vbe", ".js", ".rb", ".cmd", ".cpl", ".ws", ".wsf", ".msc", ".gadget"

            foreach (string extension in executableExtensions)
            {
                string fullFile = thisFile + extension;

                try
                {
                    if (FS.ExistsFile(fullFile))
                    {
                        exe = fullFile;
                        break;
                    }
                }
                catch (System.Exception ex)
                {
                    ThrowExceptions.Custom(Exc.GetStackTrace(), type, Exc.CallingMethod(), ex);
                }
            }
        }

        foreach (string thisPath in paths)
        {
            string thisFile = System.IO.Path.Combine(thisPath, exe);

            try
            {
                if (FS.ExistsFile(thisFile))
                {
                    exe = thisFile;
                    break;
                }
            }
            catch (System.Exception ex)
            {
                ThrowExceptions.Custom(Exc.GetStackTrace(), type, Exc.CallingMethod(), ex);
            }
        }

        return RunWithOutput(exe, arguments);
    }


}

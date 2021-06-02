using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;
using sunamo.Essential;

namespace win.Helpers.Powershell
{
	public class PowershellRunner
	{
        public PowershellRunner()
        {
            FS.InvokePs = Invoke;
        }

    /// <summary>
    /// If in A1 will be full path specified = 'The system cannot find the file specified'
    /// A1 if dont contains extension, append exe
    /// </summary>
    /// <param name="exeFileNameWithoutPath"></param>
    /// <param name="arguments"></param>
    public static List<string> InvokeProcess(string exeFileNameWithoutPath, string arguments)
    {
            W32.EnableWow64FSRedirection(false);
            FS.AddExtensionIfDontHave(exeFileNameWithoutPath, AllExtensions.exe);

        //Create process
        System.Diagnostics.Process pProcess = new System.Diagnostics.Process();

        // Must contains only filename, not full path
        pProcess.StartInfo.FileName = exeFileNameWithoutPath;

        //strCommandParameters are parameters to pass to program
        pProcess.StartInfo.Arguments = arguments;

        pProcess.StartInfo.UseShellExecute = false;

        //Set output of program to be written to process output stream
        pProcess.StartInfo.RedirectStandardOutput = true;

        //Optional, recommended do not enter, then old value is not deleted and both paths is combined
        //pProcess.StartInfo.WorkingDirectory = ;

        //Start the process
        pProcess.Start();
        W32.EnableWow64FSRedirection(true);

        //Get program output
        string strOutput = pProcess.StandardOutput.ReadToEnd();

        //Wait for process to finish
        pProcess.WaitForExit();

        var result = SH.GetLines(strOutput);
        return result;
    }

    static PowershellBuilder builder = new PowershellBuilder();

    /// <summary>
    /// Tested, working
    /// For every command return at least one entry in result
    /// </summary>
    /// <param name="commands"></param>
    public async static Task<List< List<string>>> InvokeAsync(IEnumerable<string> commands)
	{
            List<List<string>> returnList = new List<List<string>>();
            PowerShell ps = null;
            //  After leaving using is closed pipeline, must watch for complete or 
            using (ps = PowerShell.Create())
            {
                foreach (var item in commands)
                {
                    ps.AddScript(item);

                    //var asyncObject = Task.Factory.FromAsync(ps.BeginInvoke(), ps.EndInvoke);
                    //var psObjects =  asyncObject.Result;

                    var async = ps.BeginInvoke();
                    // Return for SleepWithRandomOutputConsole zero outputs
                    // Pokud se to zasekává, zkontroluj si jestli nenecháváš v app Console.Readline(). S tímto ji powershell nikdy nedokončí
                    var psObjects = ps.EndInvoke(async);

                    returnList.Add(ProcessPSObjects(psObjects));
                }
            }

            return returnList;

        }

        public static List<string> Invoke(string commands)
        {
            return Invoke(CA.ToListString( commands), false)[0];
        }

        public static List<List<string>> Invoke(IEnumerable<string> commands)
        {
            return Invoke(commands, false);
        }

    /// <summary>
    /// Tested, working
    /// For every command return at least one entry in result
    /// When return no elements, try InvokeProcess
    /// </summary>
    /// <param name="commands"></param>
        public static List<List<string>> Invoke(IEnumerable<string> commands, bool immediatelyToStatus = false)
    {
        var result = InvokeAsync(commands);
        result.Wait();
        var output = result.Result;
        if (immediatelyToStatus)
            {
                foreach (var item in output)
                {
                    foreach (var item2 in item)
                    {
                        if (!string.IsNullOrEmpty(item2))
                        {
                            ThisApp.SetStatus(TypeOfMessage.Information, item2);
                        }
                    }
                }
            }
            return output;
    }

    private static List<string> ProcessPSObjects(ICollection<PSObject> pso)
    {
        List<string> output = new List<string>();

        foreach (var item in pso)
        {
            if (item != null)
            {
                output.Add(item.ToString());
            }
        }

        return output;
    }

    public static List<string> InvokeSingle(string command)
    {
        return Invoke(CA.ToListString(command))[0];
    }

    }
}
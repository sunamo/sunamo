using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace win.Helpers.Powershell
{
	public class PowershellRunner
	{
        
        
        

    /// <summary>
    /// If in A1 will be full path specified = 'The system cannot find the file specified'
    /// A1 if dont contains extension, append exe
    /// </summary>
    /// <param name="exeFileNameWithoutPath"></param>
    /// <param name="arguments"></param>
    /// <returns></returns>
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
    /// <returns></returns>
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

				var async = ps.BeginInvoke();
                // Return for SleepWithRandomOutputConsole zero outputs
                var psObjects = ps.EndInvoke(async);

                returnList.Add(ProcessPSObjects(psObjects));
			}
		}

		return returnList;
	}

    /// <summary>
    /// Tested, working
    /// For every command return at least one entry in result
    /// When return no elements, try InvokeProcess
    /// </summary>
    /// <param name="commands"></param>
    /// <returns></returns>
    public static List<List<string>> Invoke(IEnumerable<string> commands)
    {
        var result = InvokeAsync(commands);
        result.Wait();
        return result.Result;
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


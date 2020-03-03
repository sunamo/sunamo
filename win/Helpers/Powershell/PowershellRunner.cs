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


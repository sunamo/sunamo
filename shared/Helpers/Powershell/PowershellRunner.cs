using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace sunamo.Helpers
{
    public class PowershellRunner
    {
        public static List<string> WaitForResult(string command)
        {
            List<string> output = null;
            using (PowerShell ps = PowerShell.Create())
            {
                ps.AddScript(command);
                output = Invoke(ps);

            }
            return output;
        }

        public static List<string> Invoke(PowerShell ps)
        {
            return ProcessPSObjects(ps.Invoke());

        }

        public static List<string> ProcessPSObjects(ICollection<PSObject> pso)
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

        public static List<string> InvokeAsync(params string[] commands)
        {
            List<string> returnList = new List<string>();
            PowerShell ps = null;

            foreach (var item in commands)
            {
                //  After leaving using is closed pipeline, must watch for complete or 
                using (ps = PowerShell.Create())
                {
                    ps.AddScript(item);
                    var async = ps.BeginInvoke();
                    returnList.AddRange(ProcessPSObjects(ps.EndInvoke(async)));
                }
            }

            return returnList;
        }

        public async static Task DontWaitForResultAsync(params string[] commands)
        {
            PowerShell ps = null;

            foreach (var item in commands)
            {
                //  After leaving using is closed pipeline, must watch for complete or 
                using (ps = PowerShell.Create())
                {
                    ps.AddScript(item);
                    var async = ps.BeginInvoke();
                    ps.EndInvoke(async);
                }
            }
        }
    }
}

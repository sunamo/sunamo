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
        static PowershellBuilder builder = new PowershellBuilder();
        
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
        /// </summary>
        /// <param name="commands"></param>
        /// <returns></returns>
        public static List<List<string>> Invoke(IEnumerable<string> commands)
        {
            var result = InvokeAsync(commands);
            result.Wait();
            return result.Result;
        }

    }
}


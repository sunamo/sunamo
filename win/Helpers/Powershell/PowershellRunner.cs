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

        #region Not tested, probably not working
        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="path"></param>
        /// <returns></returns>
		public static List<List<string>> WaitForResult( List<string> command, string path = null)
		{
			List<List<string>> output = new List<List<string>>();
            using (PowerShell ps = PowerShell.Create())
            {
                builder.Clear();
                if (path != null)
                {
                    Invoke(builder.Cd(path) , output, ps);
                }

                for (int i = 0; i < command.Count; i++)
                {
                    Invoke(command[i], output, ps);
                }
            }
			
			return output;
		}
        #endregion

        private static void Invoke(string command, List<List<string>> output, PowerShell ps)
        {
            ps.AddScript(command);
            var invoke = ProcessPSObjects( ps.Invoke());
            output.Add(invoke);
        }

        
        /// <summary>
        /// Tested, working
        /// </summary>
        /// <param name="commands"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public async static Task DontWaitForResultAsync(List<string> commands, string path = null)
        {
            PowerShell ps = null;

            foreach (var item in commands)
            {
                //  After leaving using is closed pipeline, must watch for complete or 
                using (ps = PowerShell.Create())
                {
                    if (path != null)
                    {

                    }
                    ps.AddScript(item);
                    //s ziskanim vysledku a EndInvoke - synchronni
                    //bez ziskani vysledku - okamzite ale neni mozne ziskat vystup
                    ps.BeginInvoke();

                    //var async =
                    //ps.EndInvoke(async);
                }
            }
        }

        public static List<string> Invoke(PowerShell ps)
        {
            return ProcessPSObjects(ps.Invoke());

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

        /// <summary>
        /// Tested, working
        /// </summary>
        /// <param name="commands"></param>
        /// <returns></returns>
        public async static Task<List< List<string>>> InvokeAsync(params string[] commands)
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

        public  static List<List<string>> Invoke(params string[] commands)
        {
            var result = InvokeAsync(commands);
            result.Wait();
            return result.Result;
        }

    }
}


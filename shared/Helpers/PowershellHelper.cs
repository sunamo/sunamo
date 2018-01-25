using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management.Automation;
using System.Collections.ObjectModel;
using System.Diagnostics;
using sunamo.Essential;
using System.Management.Automation.Runspaces;

namespace sunamo.Helpers
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
    }
}

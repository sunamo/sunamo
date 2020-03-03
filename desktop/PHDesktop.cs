using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using win.Helpers.Powershell;

public class PHDesktop
{
    /// <summary>
    /// A1 without extension
    /// </summary>
    /// <param name = "name"></param>
    public static int Terminate(string name)
    {
        int deleted = 0;

        var cmdHandle = "handle.exe |findstr /i ";
        const string pid = "pid:";
        const string pskill = "pskill ";
        var result = PowershellRunner.Invoke(CA.ToListString(cmdHandle + name))[0];

        var lines = result.Where(d =>d.Contains(pid));

        var processid = -1;

        foreach (var item in lines)
        {
            processid = -1;

            var p = SH.SplitByWhiteSpaces(item, true);
            var dx = p.IndexOf(pid);

            if (dx != -1)
            {
                if (p.Count > dx+1)
                {
                    processid = BTS.ParseInt(p[dx + 1]);
                }
            }

            if (processid != -1)
            {
                PowershellRunner.Invoke(CA.ToListString(pskill + processid));
                deleted++;
            }
        }

        
        //foreach (var process in Process.GetProcessesByName(name))
        //{
        //    process.Kill();
        //    deleted++;
        //}

        return deleted;
    }
}
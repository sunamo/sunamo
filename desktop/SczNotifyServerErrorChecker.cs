using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using sunamo.Essential;

public class SczNotifyServerErrorChecker
{
    static Type type = typeof(SczNotifyServerErrorChecker);

     Timer t;

    public SczNotifyServerErrorChecker()
    {
        if (!VpsHelperSunamo.IsVps)
        {
            t = new Timer(60000);
            t.AutoReset = true;
            t.Elapsed += T_Elapsed;
            T_Elapsed(null, null);
        }
    }

    private void T_Elapsed(object sender, ElapsedEventArgs e2)
    {
            var p = Process.GetProcesses().Where(e => e.ProcessName == "SczNotifyServerError").Select(d => d.ProcessName).ToList();
            if (p.Count == 0)
            {
                //ThrowExceptions.Custom(RuntimeHelper.GetStackTrace(),type, RH.CallingMethod(), "SczNotifyServerError is not running, starting it");
                Process.Start(@"D:\pa\_sunamo\SczNotifyServerError\SczNotifyServerError.exe");
           }
        
    }
}
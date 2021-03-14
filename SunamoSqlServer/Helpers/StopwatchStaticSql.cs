using sunamo.Essential;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class StopwatchStaticSql
{
    static Stopwatch sw = new Stopwatch();
    public const int maxMs = 1000;

    internal static void StopAndPrintElapsed(string v)
    {
        sw.Stop();
        if (sw.ElapsedMilliseconds > maxMs)
        {
            if (VpsHelperSunamo.IsVps || MSStoredProceduresI.forceIsVps)
            {
                // everything begin with select, update etc. so is no needed delimiter
                ThisApp.swSqlLog.WriteLine(sw.ElapsedMilliseconds + v);
                ThisApp.IncrementWrittenLines();
            }
        }
    }

    internal static void Start()
    {
        sw.Start();
    }
}

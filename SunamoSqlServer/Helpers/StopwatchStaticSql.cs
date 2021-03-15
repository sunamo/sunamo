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
    static long ms = 0;

    internal static void StopAndPrintElapsed(string v)
    {
        ms = sw.ElapsedMilliseconds;
        sw.Reset();
        if (ms > maxMs)
        {
            if (VpsHelperSunamo.IsVps || MSStoredProceduresI.forceIsVps)
            {
                // everything begin with select, update etc. so is no needed delimiter
                ThisApp.swSqlLog.WriteLine(ms + v);
                ThisApp.IncrementWrittenLines();
            }
        }
    }

    internal static void Start()
    {
        sw.Start();
    }
}

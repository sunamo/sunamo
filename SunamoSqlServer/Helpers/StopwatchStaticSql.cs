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

    internal static void StopAndPrintElapsed(string v)
    {
        sw.Stop();
        // everything begin with select, update etc. so is no needed delimiter
        ThisApp.swSqlLog.WriteLine(sw.ElapsedMilliseconds + v);
        

    }

    internal static void Start()
    {
        sw.Start();
    }
}

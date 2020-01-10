using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;


public class StopwatchHelper
{
    static Stopwatch sw = new Stopwatch();

    public  void Start()
    {
        sw.Reset();
        sw.Start();
    }

    public  void StopAndPrintElapsed(string operation, string p, params object[] parametry)
    {
        sw.Stop();
        DebugLogger.Instance.WriteLine(operation + " takes " + sw.ElapsedMilliseconds + "ms" + p, parametry);
    }

    public  long ElapsedMS
    {
        get
        {
            return sw.ElapsedMilliseconds;
        }
    }

    public void StopAndPrintElapsed(string operation)
    {
        StopAndPrintElapsed(operation, string.Empty);
    }
}
using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;


public class StopwatchHelper
{
    public  Stopwatch sw = new Stopwatch();

    public  void Start()
    {
        sw.Reset();
        sw.Start();
    }

    public  long StopAndPrintElapsed(string operation, string p, params object[] parametry)
    {
        sw.Stop();
        //////DebugLogger.Instance.WriteLine(operation + " takes " + sw.ElapsedMilliseconds + "ms" + p, parametry);
        return sw.ElapsedMilliseconds;
    }

    public  long ElapsedMS
    {
        get
        {
            return sw.ElapsedMilliseconds;
        }
    }

    public long StopAndPrintElapsed(string operation)
    {
        return StopAndPrintElapsed(operation, string.Empty);
    }
}
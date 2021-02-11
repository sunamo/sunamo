using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using sunamo.Essential;

public class StopwatchHelper
{
    public  Stopwatch sw = new Stopwatch();

    public  void Start()
    {
        sw.Reset();
        sw.Start();
    }

    /// <summary>
    /// Write ElapsedMilliseconds
    /// </summary>
    /// <param name="operation"></param>
    /// <param name="p"></param>
    /// <param name="parametry"></param>
    /// <returns></returns>
    public long StopAndPrintElapsed(string operation, string p, params object[] parametry)
    {
        sw.Stop();
        string message = string.Format(operation + " takes " + sw.ElapsedMilliseconds + "ms" + p, parametry);
        ThisApp.SetStatus(TypeOfMessage.Information, message);
#if DEBUG
        DebugLogger.Instance.WriteLine(message);
#endif 
        return sw.ElapsedMilliseconds;
    }

    public  long ElapsedMS
    {
        get
        {
            return sw.ElapsedMilliseconds;
        }
    }

    /// <summary>
    /// Write ElapsedMilliseconds
    /// </summary>
    /// <param name="operation"></param>
    /// <returns></returns>
    public long StopAndPrintElapsed(string operation)
    {
        return StopAndPrintElapsed(operation, string.Empty);
    }
}
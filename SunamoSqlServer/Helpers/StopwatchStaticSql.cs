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
    public const int maxMs = 500;
    static long ms = 0;

    /// <summary>
    /// Must if (MSStoredProceduresI.measureTime) before calling
    /// </summary>
    /// <returns></returns>
    public static bool AboveLimit()
    {
        if( sw.ElapsedMilliseconds > maxMs)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// Must if (MSStoredProceduresI.measureTime) before calling
    /// </summary>
    /// <param name="v"></param>
    public static void StopAndPrintElapsed(string v)
    {
        
            ms = sw.ElapsedMilliseconds;
        sw.Reset();
        
            if (ms > maxMs)
            {
                if (VpsHelperSunamo.IsVps || MSStoredProceduresI.forceIsVps)
                {
                    // everything begin with select, update etc. so is no needed delimiter
                    SqlMeasureTimeWorker.swSqlLog.WriteLine(ms + v);
                    SqlMeasureTimeWorker.IncrementWrittenLines();
                }
            }
        
    }

    public static bool forceIsVps = false;

    /// <summary>
    /// Must if (MSStoredProceduresI.measureTime) before calling
    /// </summary>
    internal static void Start()
    {
        sw.Start();
    }
}
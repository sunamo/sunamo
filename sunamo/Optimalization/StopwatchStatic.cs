using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;


    public class StopwatchStatic
    {
        static StopwatchHelper sw = new StopwatchHelper();

        public static void Start()
        {
        sw.Start();
        }

    public static void Reset()
    {
        sw.Reset();
    }

        public static long StopAndEllapsedMs()
    {
        
        var l = sw.sw.ElapsedMilliseconds;
        sw.sw.Reset();
        return l;
    }

    /// <summary>
    /// Write ElapsedMilliseconds
    /// </summary>
    /// <param name="operation"></param>
    /// <returns></returns>
    public static long StopAndPrintElapsed(string operation)
    {
        return sw.StopAndPrintElapsed(operation);
    }

        public static long StopAndPrintElapsed(string operation, string p, params object[] parametry)
        {
            return sw.StopAndPrintElapsed(operation, p, parametry);
        }

        public static long ElapsedMS
        {
            get
            {
                return sw.ElapsedMS;
            }
        }

    /// <summary>
    /// Call Start() Aganin
    /// </summary>
    /// <param name="notTranslateAbleString"></param>
    public static void PrintElapsedAndContinue(string notTranslateAbleString)
    {
        StopAndPrintElapsed(notTranslateAbleString);
        Start();
    }
}
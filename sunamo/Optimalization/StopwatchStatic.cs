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

        public static long StopAndEllapsedMs()
    {
        sw.sw.Stop();
        return sw.sw.ElapsedMilliseconds;
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
    }
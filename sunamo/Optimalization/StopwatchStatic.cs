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

        public static void StopAndPrintElapsed(string operation, string p, params object[] parametry)
        {
            sw.StopAndPrintElapsed(operation, p, parametry);
        }

        public static long ElapsedMS
        {
            get
            {
                return sw.ElapsedMS;
            }
        }
    }
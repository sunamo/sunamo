using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace DocArch
{
    class StopwatchStatic
    {
        static Stopwatch sw = new Stopwatch();

        public static void Start()
        {
            sw.Reset();
            sw.Start();
        }

        public static void StopAVypis(string p, params object[] parametry)
        {
            sw.Stop();
            DebugLogger.Instance.WriteLine(p, parametry);
        }

        public static long ElapsedMS
        {
            get
            {
                return sw.ElapsedMilliseconds;
            }
        }
    }
}

using sunamo;
using sunamo.Values;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace desktop
{
    public class ProcessHelper
    {
        public static void Start(string p)
        {
            try
            {
                Process.Start(p);
            }
            catch (Exception)
            {
                
            }
        }

        public static void StartAllUri(List<string> all)
        {
            foreach (var item in all)
            {
                Process.Start(UH.AppendHttpIfNotExists(item));
            }
        }
    }
}

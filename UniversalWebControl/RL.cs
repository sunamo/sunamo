using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;

namespace UniversalWebControl
{
    internal class RL
    {
        static ResourceLoader rl = ResourceLoader.GetForCurrentView("UniversalWebControl/Resources");

        public static string GetString(string k)
        {
            return rl.GetString(k);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ThreadHelper
{
    public static bool NeedDispatcher(string tName)
    {
#if DEBUG
        //DebugLogger.DebugWriteLine(tName);
#endif

        if (tName == "UIElementCollection")
        {
            return true;
        }
        return false;
    }
}
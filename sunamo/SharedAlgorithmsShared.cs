using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

public partial class SharedAlgorithms
{
    public static Out RepeatAfterTimeXTimes<Out>(int times, int timeoutMs, Func<Out> a)
    {
        Out result = default(Out);
        bool ok = false;

        for (int i = 0; i < times; i++)
        {
            try
            {
                result = a();
                ok = true;
            }
            catch (Exception ex)
            {
                Thread.Sleep(timeoutMs);
            }

            if (ok)
            {
                break;
            }
        }

        return result;
    }
}

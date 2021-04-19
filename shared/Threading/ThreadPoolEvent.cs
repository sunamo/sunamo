using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ThreadPoolEvent
{
    int n = 0;
    int finished = 0;
    public event Action Done;

    public ThreadPoolEvent(int n)
    {
        this.n = n;
    }

    public void PartiallyDone()
    {
        finished++;
        if (finished ==n)
        {
            Done();
        }
    }
}
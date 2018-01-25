using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace sunamo.Threading
{
    public class ThreadPoolWithoutWaiting
    {
        object o = new object();

        List<Thread> threads = new List<Thread>();

        public ThreadPoolWithoutWaiting(int size, ParameterizedThreadStart start, params string[] args)
        {
            foreach (var item in args)
            {
                Thread thread = new Thread(start);

            }
        }
    }
}

using sunamo.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;

namespace ThreadsIfaces
{
    //public delegate void WaitCallback();
    public interface IThreadPool
    {
        bool QueueUserWorkItem(WaitCallback callBack);
        bool SetPoolSize(int size);
        int PoolSize { get; }
    }

    /// <summary>
    /// MyThreadPool implements a simple thread pool, that allows for dynamic change of number
    /// of working threads.
    /// Size of pool isnt fixed, it can has more elements than poolSize
    /// </summary>
    public class MyThreadPool : IThreadPool
    {
        /// <summary>
        /// Max. size of pool
        /// </summary>
        private int poolSize;
        /// <summary>
        /// Threads running in momen
        /// </summary>
        private List<Thread> threads = new List<Thread>();
        /// <summary>
        /// 
        /// </summary>
        private Queue<WaitCallback> jobs = new Queue<WaitCallback>();

        /// <summary>
        /// Add thread to queue and call Monitor.Pulse on queue
        /// </summary>
        /// <param name="callBack"></param>
        
        private bool killThreadIfNeeded()
        {
            if (poolSize < threads.Count)
            {
                lock (threads)
                {
                    if (poolSize < threads.Count)
                    {
                        threads.Remove(Thread.CurrentThread);
                        return true;
                    }
                }
            }

            return false;
        }

        /* Returns most recently set size of the pool. */
        public int PoolSize { get { return poolSize; } }

        /* Gets the actual size of the pool. It might not be equal to PoolSize, when number
		   of threads is stabilizing after pool size change. This value is for information only
		   and should not be relied upon. */
        public int ActualPoolSize { get { return threads.Count; } }
    }
}

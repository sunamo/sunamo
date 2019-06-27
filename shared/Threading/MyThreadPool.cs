﻿using sunamo.Interfaces;
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
        /// <returns></returns>
        public bool QueueUserWorkItem(System.Threading.WaitCallback callBack)
        {
            if (callBack == null)
                throw new NotSupportedException(" " + " callback method cannot be null");

            lock (jobs)
            {
                jobs.Enqueue(callBack);
                Monitor.Pulse(jobs);
            }

            return true;
        }


        /// <summary>
        /// After reducing the number of working threads, currently working threads are allowed to finish their jobs (they're not interrupted).
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public bool SetPoolSize(int size)
        {
            lock (threads)
            {
                poolSize = size;
                if (poolSize > threads.Count)
                    spawnThreads();
                else if (poolSize < threads.Count)
                {
                    lock (jobs) Monitor.PulseAll(jobs); //wake them up, so some of them might finish
                }
            }

            return true;
        }

        /// <summary>
        /// Run new threads up to size of pool
        /// </summary>
        private void spawnThreads()
        {
            while (threads.Count < poolSize)
            {
                Thread t = new Thread(ConsumeJobs);
                threads.Add(t);
                t.Start();
            }
        }

        /// <summary>
        /// Runner method for new thread
        /// </summary>
        private void ConsumeJobs()
        {
            WaitCallback job;

            while (true)
            {
                if (killThreadIfNeeded()) return;

                lock (jobs)
                {
                    while (jobs.Count == 0 && !(poolSize < threads.Count))
                        Monitor.Wait(jobs); //wait, if no more jobs, but should not kill any threads
                    if (killThreadIfNeeded()) return;
                    job = jobs.Dequeue();
                }
                job(null);
            }
        }

        /// <summary>
        /// If is more running then poolSize, remove Thread.CurrentThread
        /// Returns true if invoking thread should be killed (break his loop), false otherwise
        /// </summary>
        /// <returns></returns>
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

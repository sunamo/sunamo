using sunamo;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

/// <summary>
/// Run by time new thread. 
/// </summary>
    public class TimeThreadPool : IDisposable
    {
        Timer timer = null;
        Dictionary<int, Thread> threads = new Dictionary<int, Thread>();
        Stack<int> stack = new Stack<int>();
        int maxThreadAtTime = 3;
    int zbyva = 0;
    object[] args = null;

    /// <summary>
    /// A3 nemůže být params
    /// </summary>
    /// <param name="metoda"></param>
    /// <param name="maxThreadAtTime"></param>
    /// <param name="args"></param>
    public TimeThreadPool(ParameterizedThreadStart metoda, int maxThreadAtTime, object[] args)
        {
            
            if (args.Length < maxThreadAtTime)
            {
                maxThreadAtTime = 0;
            }
            zbyva = args.Length;
            this.args = args;
            for (int i = 0; i < args.Length; i++)
            {
                stack.Push(i);
                Thread thread = new Thread(metoda);
                //thread.Start(args[i]);
                threads.Add(i, thread);
            }
        timer = new Timer(TimerElapsed, null, 0, 1000);
        
    }

        

        void TimerElapsed(object o)
        {
#if DEBUG
        DebugLogger.Instance.WriteLine(DateTime.Now.ToLongTimeString());
#endif

        if (zbyva != 0)
            {
                zbyva--;
                int thread = stack.Pop();
                threads[thread].Start(args[thread]);
            }
            else
            {
                DisposeTimer();
            }
        }

        public void StopAll()
        {
            // Je lepší Timer úplně zlikvidovat, protože tato třída stejně neumí resumovat ani znovu spouštět
            DisposeTimer();
            foreach (KeyValuePair<int, Thread> item in threads)
            {
                ////////"StopAll:" + item.Value.ThreadState.ToString());
                if (IsThreadTurnedOn(item))
                {
                        item.Value.Interrupt();
                    //}
                }
            }
        }

        private bool IsThreadTurnedOn(KeyValuePair<int, Thread> item)
        {
            return item.Value.ThreadState != System.Threading.ThreadState.Stopped && item.Value.ThreadState != System.Threading.ThreadState.StopRequested && item.Value.ThreadState != System.Threading.ThreadState.WaitSleepJoin;
        }

        /// <summary>
        /// Volá na všechny vlákna metodu Join, nevím zda to je správný přístup, lepší bude asi tuto metodu nepoužívat
        /// </summary>
        public void Dispose()
        {
        }

        private void DisposeTimer()
        {
            if (timer != null)
            {
                timer.Change(System.Threading.Timeout.Infinite, 0);
                timer.Dispose();
                timer = null;
            }
        }
    }

using sunamo.Essential;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace sunamo.Collections
{
    /// <summary>
    /// Can be use also in production
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DebugCollection<T> : List<T>
    {
        public List<T> dontAllow = new List<T>();

        public new void Add(T t)
        {
            if (dontAllow.Contains(t))
            {
#if DEBUG
                ////DebugLogger.Break();
#endif
            }
            else
            {
                base.Add(t);
            }
        }
    }
}
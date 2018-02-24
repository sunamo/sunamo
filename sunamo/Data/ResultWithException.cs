using System;
using System.Collections.Generic;
using System.Text;

namespace sunamo.Data
{
    public class ResultWithException<T>
    {
        public T Data = default(T);
        /// <summary>
        /// only string, because Message property isn't editable after instatiate
        /// </summary>
        public string exc;
    }
}

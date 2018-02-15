using System;
using System.Collections.Generic;
using System.Text;

namespace sunamo.Data
{
    public class TWithException<T>
    {
        public T Data;
        /// <summary>
        /// only string, because Message property isn't editable after instatiate
        /// </summary>
        public string exc;
    }
}

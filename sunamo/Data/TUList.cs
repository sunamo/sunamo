using System;
using System.Collections.Generic;
using System.Text;

namespace sunamo.Data
{
    public class TUList<T, U> : List<TU<T, U>>
    {
        public List<TU<T, U>> list
        {
            get
            {
                return this;
            }
        }
    }
}
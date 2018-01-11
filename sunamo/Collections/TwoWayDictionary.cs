using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sunamo.Collections
{
    class TwoWayDictionary<T, U>
    {
        Dictionary<T, U> d1 = new Dictionary<T, U>();
        Dictionary<U, T> d2 = new Dictionary<U, T>();

        public void Add(T key, U value)
        {
            d1.Add(key, value);
            d2.Add(value, key);
        }
    }
}

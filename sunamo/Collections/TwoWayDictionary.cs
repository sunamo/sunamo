using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sunamo.Collections
{
    public class TwoWayDictionary<T, U>
    {
        public Dictionary<T, U> _d1 = null;
        public Dictionary<U, T> _d2 = null;

        public TwoWayDictionary(int c)
        {
            _d1 = new Dictionary<T, U>(c);
            _d2 = new Dictionary<U, T>(c);
        }

        public TwoWayDictionary()
        {
            _d1 = new Dictionary<T, U>();
            _d2 = new Dictionary<U, T>();
        }


        public void Add(T key, U value)
        {
            _d1.Add(key, value);
            _d2.Add(value, key);
        }
    }
}
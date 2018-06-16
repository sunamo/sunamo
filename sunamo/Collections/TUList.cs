using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sunamo.Collections
{
    public class TUList<T, U>
    {
        public List<TU<T, U>> list = new List<TU<T, U>>();

        public void Add(T t, U u)
        {
            list.Add(new TU<T, U>() { Key = t, Value = u });
        }

        /// <summary>
        /// When A1 will be SE/null, dont add
        /// </summary>
        /// <param name="t"></param>
        /// <param name="u"></param>
        public void AddString(T t, U u)
        {
            if (string.IsNullOrEmpty(t.ToString()))
            {
                return;
            }
            list.Add(new TU<T, U>() { Key = t, Value = u });
        }
    }
}

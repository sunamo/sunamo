using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sunamo.Data
{
    public class TWithPath<T>
    {
        public T t = default(T);
        public string path = null;
    }
}

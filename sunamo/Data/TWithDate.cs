using System;
using System.Collections.Generic;
using System.Text;

namespace sunamo.Data
{
    public class TWithDate<T>
    {
        public T t = default(T);
        public DateTime date = DateTime.MinValue;
    }
}

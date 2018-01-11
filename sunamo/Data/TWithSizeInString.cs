using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sunamo.Data
{
    public class TWithSizeInString<T>
    {
        public T t = default(T);
        public string sizeS = "";

        public override string ToString()
        {
            return t + " (" + sizeS + ")";

        }
    }
}

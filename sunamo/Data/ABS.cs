using System;
using System.Collections.Generic;
using System.Text;

namespace sunamo.Data
{
    /// <summary>
    /// Implicitly are strings.Empty to avoid cheching data class for null
    /// </summary>
    public class ABS : ABT<string, string>
    {
        public ABS()
        {
            A = B = string.Empty;
        }

        public ABS(string a, string b)
        {
            this.A = a;
            this.B = b;
        }
    }
}
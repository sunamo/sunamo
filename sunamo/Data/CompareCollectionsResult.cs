using System;
using System.Collections.Generic;
using System.Text;

namespace sunamo.Data
{
    public class CompareCollectionsResult<T>
    {
        public List<T> OnlyInFirst;
        public List<T> OnlyInSecond;
        public List<T> Both;
    }
}

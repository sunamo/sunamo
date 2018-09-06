
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apps
{
    class LogMessagesComparer : IComparer<LogMessage>
    {
        //public int Asc(LogMessage x, LogMessage y)
        //{
        //    return x.datum.CompareTo(y.datum) * -1;
        //}

        public int Compare(LogMessage x, LogMessage y)
        {
            return Desc(x, y);
        }

        public int Desc(LogMessage x, LogMessage y)
        {
            return x.Dt.CompareTo(y.Dt);
        }
    }
}

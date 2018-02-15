using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace sunamo
{
    public class SunamoDictionary<T, U> : Dictionary<T, U>
    {
        public int CountOfValueNot(U u)
        {
            int vr = 0;
            foreach (var item in this)
            {
                if (!EqualityComparer<U>.Default.Equals(item.Value, u))
                {
                    vr++;
                }
            }
            return vr;
        }

        public bool AnyValue(U u)
        {
            foreach (var item in this)
            {
                if (EqualityComparer<U>.Default.Equals(item.Value, u))
                {
                    return true;
                }
            }
            return false;
        }

        public int CountOfValue(U u)
        {
            int vr = 0;
            foreach (var item in this)
            {
                if (EqualityComparer<U>.Default.Equals(item.Value, u))
                {
                    vr++;
                }
            }
            return vr;
        }

        
    }
}

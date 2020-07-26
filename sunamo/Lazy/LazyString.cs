using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class LazyString : LazyT<string>
{
    public LazyString(Func<string, string> getCommonSettings, string key) : base(getCommonSettings, key)
    {

    }
}
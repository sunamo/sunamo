using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class LazyGetCommonSettings : LazyString
{
    public LazyGetCommonSettings(string key) : base(AppData.ci.GetCommonSettings, key)
    {

    }
}
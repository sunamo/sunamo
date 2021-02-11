using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public partial class CharHelper
{
    public static string CharWhichIsNotContained(string item)
    {
        var v = RH.GetValuesOfConsts(typeof(AllStrings));
        foreach (var item2 in v)
        {
            if (!item.Contains(item2))
            {
                return item2;
            }
        }
        return null;
    }
}
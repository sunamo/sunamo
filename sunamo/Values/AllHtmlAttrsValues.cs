using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sunamo;

public class AllHtmlAttrsValues
{
    static bool initialized = false;
    public static List<string> list = new List<string>();

    public static void Init()
    {
        if (!initialized)
        {
            initialized = true;
            var d = RH.GetConsts(typeof(HtmlAttrValue));
            
            foreach (var item in d)
            {
                list.Add(item.GetValue(null).ToString());
            }

            list.Sort(new SunamoComparerICompare.StringLength.Desc(SunamoComparer.StringLength.Instance));
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    public static class ListExtensions
    {
        public static List<string> InsertTs(this IList<string> list, string item)
        {
        if (!list.Contains(item))
        {
            list.Insert(0, string.Format(GeneralConsts.tsInclude, item));
        }
            
            return (List<string>)list;
        }
    }


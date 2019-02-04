using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    public static class ListExtensions
    {
    /// <summary>
    /// can be use only if A2 is in root of web / ts/_ folder
    /// </summary>
    /// <param name="list"></param>
    /// <param name="item"></param>
    /// <returns></returns>
        public static List<string> InsertTs(this IList<string> list, string item)
        {
        if (!list.Contains(item))
        {
            list.Insert(0, SH.Format(GeneralConsts.tsInclude, item));
        }
            
            return (List<string>)list;
        }

        public static List<string> Leading(this List<string> list, string item)
        {
        list.Insert(0, item);
        return list;
        }
    }


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shared.Extensions
{
    public class DictionaryExtensions
    {
        /// <summary>
        /// Is stupid use this method, is enough import System.Linq
        /// </summary>
        /// <typeparam name="Key"></typeparam>
        /// <typeparam name="Value"></typeparam>
        /// <param name="dict"></param>
        public static IEnumerable<Key> GetKeys<Key, Value>(Dictionary<Key, Value> dict)
        {
            //return dict.Keys.ToList();
            return dict.Select(d => d.Key);
        }
    }
}
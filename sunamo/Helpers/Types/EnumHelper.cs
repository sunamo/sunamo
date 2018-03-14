using sunamo.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sunamo
{
    public static class EnumHelper
    {
        /// <summary>
        /// Pokud bude A1 null nebo nebude obsahovat žádný element T, vrátí A1
        /// Pokud nebude obsahovat všechny, vrátí jen některé - nutno kontrolovat počet výstupních elementů pole
        /// Pokud bude prvek duplikován, zařadí se jen jednou
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="v"></param>
        /// <returns></returns>
        public static List<T> GetEnumList<T>(List<T> _def, string[] v) where T: struct
        {
            if (v == null)
            {
                return _def;
            }
            List<T> vr = new List<T>();
            foreach (string item in v)
            {
                T t;
                if (Enum.TryParse<T>(item, out t))
                {
                    vr.Add(t);
                }
            }

            if (vr.Count == 0)
            {
                return _def;
            }

            return vr;
        }

        public static Dictionary<T, string> EnumToString<T>(Type enumType)
        {
            return Enum.GetValues(enumType).Cast<T>().Select(t => new { Key = t, Value = t.ToString().ToLower() }).ToDictionary(r => r.Key, r => r.Value);
        }

        public static List<T> GetValues<T>() where T : struct
        {
            Type type = typeof(T);
            var values = Enum.GetValues(type).Cast<T>().ToList();
            T nope;
            if(Enum.TryParse<T>(CodeElementsConstants.NopeValue, out nope))
            {
                values.Remove(nope);
            }
            return values;
        }
    }
}

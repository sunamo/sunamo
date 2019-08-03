using sunamo.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static partial class EnumHelper
{
    public static List<T> GetValues<T>()
            where T : struct
    {
        return GetValues<T>(typeof(T));
    }
    /// <summary>
    /// Get all values expect of Nope/None
    /// </summary>
    /// <typeparam name = "T"></typeparam>
    /// <param name = "type"></param>
    /// <returns></returns>
    public static List<T> GetValues<T>(Type type)
        where T : struct
    {
        var values = Enum.GetValues(type).Cast<T>().ToList();
        T nope;
        if (Enum.TryParse<T>(CodeElementsConstants.NopeValue, out nope))
        {
            values.Remove(nope);
        }

        if (Enum.TryParse<T>(CodeElementsConstants.NoneValue, out nope))
        {
            values.Remove(nope);
        }

        return values;
    }
}
using sunamo.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static partial class EnumHelper
{
    /// <summary>
    /// GET WITHOUT NOPE (parse string, not numeric), USE METHOD WITH MORE ARGS
    /// Can be use only for int enums
    /// </summary>
    /// <typeparam name="T"></typeparam>
    #region GetValues - unlike GetAllValues in EnumHelper.cs can exclude Nope,Shared, etc.
    ///
    public static List<T> GetValues<T>()
       where T : struct
    {
        return GetValues<T>(false, true);
    }
    /// <summary>
    /// Get all values expect of Nope/None
    /// </summary>
    /// <typeparam name = "T"></typeparam>
    /// <param name = "type"></param>
    public static List<T> GetValues<T>(bool IncludeNope, bool IncludeShared)
        where T : struct
    {
        var type = typeof(T);
        var values = Enum.GetValues(type).Cast<T>().ToList();
        T nope;
        if (!IncludeNope)
        {
            if (Enum.TryParse<T>(CodeElementsConstants.NopeValue, out nope))
            {
                values.Remove(nope);
            }
        }

        if (!IncludeShared)
        {
            if (type.Name == "MySites")
            {
                if (Enum.TryParse<T>("Shared", out nope))
                {
                    values.Remove(nope);
                }
            }
            else
            {
                if (Enum.TryParse<T>("Sha", out nope))
                {
                    values.Remove(nope);
                }
            }
        }

        if (Enum.TryParse<T>(CodeElementsConstants.NoneValue, out nope))
        {
            values.Remove(nope);
        }

        return values;
    } 
    #endregion

    public static List<string> GetFlags<T>(T key) where T : Enum
    {
        List<string> ls = new List<string>();
        var v = Enum.GetValues(typeof(T));

        foreach (Enum item in v)
        {
            if (key.HasFlag(item))
            {
                ls.Add(item.ToString());
            }
        }
        return ls;
    }

    
}
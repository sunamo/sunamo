using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    public partial class Exceptions
    {
    

    #region For easy copy in SunamoException project

    public static object KeyNotFound<T, U>(string v, IDictionary<T, U> en, string dictName, T key)
    {
        if (!en.ContainsKey(key))
        {
            return key + " "+SunamoPageHelperSunamo.i18n(XlfKeys.isNotExistsInDictionary)+" " + dictName;
        }
        return null;
    }
  

    

    
    

    /// <summary>
    /// Verify whether A2 contains A3
    /// </summary>
    /// <param name="before"></param>
    /// <param name="originalText"></param>
    /// <param name="shouldContains"></param>
    public static string NotContains(string before, string originalText, params string[] shouldContains)
    {
        List<string> notContained = new List<string>();
        foreach (var item in shouldContains)
        {
            if (!originalText.Contains(item))
            {
                notContained.Add(item);
            }
        }

        if (notContained.Count == 0)
        {
            return null;
        }
        return CheckBefore(before) + originalText + " dont contains: " + SH.Join(notContained, AllStrings.comma);
    }

   
     
  

    public static string HasNotKeyDictionary<Key, Value>(string v, string nameDict, IDictionary<Key, Value> qsDict,  Key remains)
    {
        if (!qsDict.ContainsKey(remains))
        {
            return CheckBefore(v) + nameDict + " does not contains key " + remains;
        }
        return null;
    }

    

    public static string BadFormatOfElementInList(string before, object elVal, string listName)
    {
        return before + SunamoPageHelperSunamo.i18n(XlfKeys.BadFormatOfElement)+" " + SH.NullToStringOrDefault(elVal) + " in list " + listName; 
    }

    public static string IsEmpty(string before, IEnumerable folders, string colName, string additionalMessage)
    {
        if (folders.Count() == 0)
        {
            return before + colName + " has no elements. " + additionalMessage;
        }
        return null;
    }

    public static string NotInt(string before, string what, object value)
    {
        if (!BTS.IsInt(value.ToString()))
        {
            return before + what + " is not with value " + value + " valid integer number";
        }
        return null;
    }

    public static object RepeatAfterTimeXTimesFailed(string before, int times, int timeoutInMs, string address)
    {
        return before + $"Loading uri {address} failed {times} ({timeoutInMs} ms timeout)";
    }

    
    #endregion
}
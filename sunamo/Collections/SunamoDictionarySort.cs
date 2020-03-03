using System.Collections.Generic;
public class SunamoDictionarySort<T, U> : Dictionary<T, U>
{
    private DictionarySort<T, U> _ss = new DictionarySort<T, U>();

    /// <summary>
    /// sezareno a->z, lomítko první, pak čísla, pak písmena - vše standardně. Porovnává se tak bez volání Reverse
    /// </summary>
    /// <param name="sl"></param>
    
    public Dictionary<T, List<U>> RemoveWhereInValuesIsOnlyOneObject(Dictionary<T, List<U>> sl)
    {
        Dictionary<T, List<U>> vr = new Dictionary<T, List<U>>();
        foreach (KeyValuePair<T, List<U>> item in sl)
        {
            if (item.Value.Count != 1)
            {
                vr.Add(item.Key, item.Value);
            }
        }
        return vr;
    }
}

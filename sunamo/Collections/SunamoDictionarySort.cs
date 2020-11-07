using System.Collections.Generic;
public class SunamoDictionarySort<T, U> : Dictionary<T, U>
{
    private DictionarySort<T, U> _ss = new DictionarySort<T, U>();

    /// <summary>
    /// Sorting a-z. Slash as first, then numbers, then letters - all in standard. 
    /// No Reserve() calling
    /// </summary>
    /// <param name="sl"></param>
    public void SortByKeysDesc()
    {
        Dictionary<T, U> sl = DictionaryHelper.ReturnsCopy<T, U>(this);
        List<T> klice = _ss.ReturnKeys(sl);
        klice.Sort();
        this.Clear();
        foreach (T item in klice)
        {
            this.Add(item, sl[item]);
        }
    }

    /// <summary>
    /// sezareno a->z, lomítko první, pak čísla, pak písmena - vše standardně. Porovnává se tak bez volání Reverse
    /// </summary>
    public void SortByValuesDesc()
    {
        Dictionary<T, U> sl = DictionaryHelper.ReturnsCopy<T, U>(this);
        List<T> klice = _ss.ReturnKeys(sl);
        List<U> hodnoty = _ss.ReturnValues(sl);
        hodnoty.Sort();
        this.Clear();

        List<T> pridane = new List<T>();
        foreach (U item in hodnoty)
        {
            T t = _ss.KeyFromValue(pridane, this.Count, sl, item);
            pridane.Add(t);
            this.Add(t, item);
            //vr.Add(t, item);
        }
    }

    private Dictionary<T, U> ToDictionary()
    {
        return this;
    }

    /// <summary>
    /// z-a, then numbers 9-0, then slash. Call Reverse()
    /// </summary>
    /// <param name="sl"></param>
    public void SortByKeyAsc()
    {
        Dictionary<T, U> sl = DictionaryHelper.ReturnsCopy<T, U>(this);
        List<T> klice = _ss.ReturnKeys(this);
        //List<U> hodnoty = VratHodnoty(sl);
        klice.Sort();
        klice.Reverse();
        //Dictionary<T, U> vr = new Dictionary<T, U>();
        this.Clear();
        foreach (T item in klice)
        {
            this.Add(item, sl[item]);
        }
    }

    /// <summary>
    /// z-a, then numbers 9-0, then slash. Call Reverse()
    /// </summary>
    public void SortByValuesAsc()
    {
        Dictionary<T, U> sl = DictionaryHelper.ReturnsCopy<T, U>(this);
        List<T> klice = _ss.ReturnKeys(sl);
        List<U> hodnoty = _ss.ReturnValues(sl);
        hodnoty.Sort();
        hodnoty.Reverse();
        this.Clear();

        foreach (U item in hodnoty)
        {
            T t = _ss.KeyFromValue(this.Count, sl, item);
            // Přidám do this místo do vr
            this.Add(t, item);
        }
    }

    /// <summary>
    /// 
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
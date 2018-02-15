using System.Collections.Generic;
public class SunamoDictionarySort<T, U> : Dictionary<T, U>
{
    DictionarySort<T,U> ss = new DictionarySort<T,U>();

    /// <summary>
    /// sezareno a->z, lomítko první, pak čísla, pak písmena - vše standardně. Porovnává se tak bez volání Reverse
    /// </summary>
    /// <param name="sl"></param>
    /// <returns></returns>
    public void SortByKeysDesc()
    {
        Dictionary<T, U> sl = DictionaryHelper.ReturnsCopy<T, U>(this);
        List<T> klice = ss.ReturnKeys(sl);
        //List<U> hodnoty = VratHodnoty(sl);
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
        // Vytvořím kopii sl
        Dictionary<T, U> sl = DictionaryHelper.ReturnsCopy<T, U>(this);
        List<T> klice =  ss.ReturnKeys(sl);
        List<U> hodnoty = ss.ReturnValues(sl);
        hodnoty.Sort();
        // Vyčistím this, abych do něj mohl zapisovat
        this.Clear();

        List<T> pridane = new List<T>();
        foreach (U item in hodnoty)
        {
            T t = ss.GetKeyFromValue(pridane, this.Count, sl, item);
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
    /// sezareno z->a, pak čísla od největších k nejmenším, lomítka až poté. Volá se reverse
    /// </summary>
    /// <param name="sl"></param>
    /// <returns></returns>
    public void SortByKeyAsc()
    {
        Dictionary<T, U> sl = DictionaryHelper.ReturnsCopy<T, U>(this);
        List<T> klice = ss.ReturnKeys(this);
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
    /// sezareno z->a, pak čísla od největších k nejmenším, lomítka až poté. Volá se reverse
    /// </summary>
    public void SortByValuesAsc()
    {
        // Vytvořím kopii sl
        Dictionary<T, U> sl = DictionaryHelper.ReturnsCopy<T, U>(this);
        List<T> klice = ss.ReturnKeys(sl);
        List<U> hodnoty = ss.ReturnValues(sl);
        hodnoty.Sort();
        hodnoty.Reverse();
        // Vyčistím this, abych do něj mohl zapisovat
        this.Clear();

        foreach (U item in hodnoty)
        {
            T t = ss.ReturnKeyFromValue(this.Count, sl, item);
            // Přidám do this místo do vr
            this.Add(t, item);
            //vr.Add(t, item);
        }

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sl"></param>
    /// <returns></returns>
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

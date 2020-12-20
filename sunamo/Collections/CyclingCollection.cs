
using System.Collections.Generic;
using System;
using System.Text;

/// <summary>
/// 
/// </summary>
public class CyclingCollection<T> //: IStatusBroadcaster
{
    public bool back = false;

    #region DPP
    /// <summary>
    /// 
    /// </summary>
    public List<T> c = new List<T>();
    private int _index = 0;
    private int index
    {
        get
        {
            if (_index < 0)
            {
                _index = 0;
            }
            else if (_index > c.Count - 1)
            {
                _index = c.Count - 1;
            }
            return _index;
        }
        set
        {
            if (value < 0)
            {
                value = 0;
            }
            _index = value;
        }
    }

    /// <summary>
    /// Whether make space in formatting actual showing
    /// </summary>
    private bool _MakesSpaces;
    public event Action Change;

    private EventArgs _ea = EventArgs.Empty;
    public bool Cycling = true;
    #endregion

    public CyclingCollection(bool Cycling)
    {
        this.Cycling = Cycling;
    }

    public CyclingCollection()
    {
    }

    public void Add(T t)
    {
        this.c.Add(t);
        _index++;
        OnChange();
    }

    public void AddRange(IEnumerable<T> k)
    {
        //t.AddRange(k);
        foreach (T item in k)
        {
            c.Add(item);
            _index++;
        }
        OnChange();
    }

    public void Clear()
    {
        c.Clear();
        _index = 0;
        OnChange();
    }

    public T SetIretation(int ir)
    {
        index = ValidateIndex(ir);
        OnChange();
        return GetIretation;
    }

    private int ValidateIndex(int ir)
    {
        if (ir < 0)
        {
            ir = c.Count - 1;
        }
        else if (ir >= c.Count)
        {
            ir = 0;
        }

        return ir;
    }

    public void SetIretationWithoutEvent(int p)
    {
        index = p;
    }

    public int ActualIndex
    {
        get
        {
            return index;
        }
    }

    public bool MakesSpaces
    {
        get
        {
            return _MakesSpaces;
        }
        set
        {
            _MakesSpaces = value;
            OnChange();
        }
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(ActualIndex + 1);
        if (_MakesSpaces)
        {
            sb.Append(AllStrings.space);
        }
        sb.Append(AllStrings.slash);
        if (_MakesSpaces)
        {
            sb.Append(AllStrings.space);
        }
        sb.Append(c.Count.ToString());
        return sb.ToString();
    }

    public T GetIterationSimple
    {
        get
        {
            if (c.Count == 0)
            {
                return default(T);
            }
            return c[index];
        }
    }

    /// <summary>
    /// If can't be obtained, try to get element previous or next.
    /// </summary>
    public T GetIretation
    {
        get
        {
            T t2 = default(T);
            int dex = Math.Abs(index);
            if (c.Count > dex && c.Count >= dex)
            {
                t2 = c[dex];
            }
            else
            {
                dex = Math.Abs(++index);
                if (c.Count > dex && c.Count >= dex)
                {
                    t2 = c[dex];
                }
                else
                {
                    index--;
                    dex = Math.Abs(--index);
                    if (c.Count > dex && c.Count >= dex)
                    {
                        t2 = c[dex];
                    }
                    else
                    {
                        if (c.Count > 0)
                        {
                            t2 = c[0];
                        }
                        else
                        {
                            OnNewStatus(sess.i18n(XlfKeys.UnableToLoadElementAddSomeAndTryAgain));
                        }
                    }
                }
            }

            return t2;
        }
    }

    #region Simply moving about 1
    public T Before()
    {
        back = true;
        if (Cycling)
        {
            if (index == 0)
            {
                index = c.Count - 1;
            }
            else
            {
                index--;
            }

            //OnChange();
        }
        else
        {
            if (index != 0)
            {
                index--;
                //OnChange();
            }
        }
        OnChange();
        return GetIretation;
    }

    public T Next()
    {
        back = false;
        if (Cycling)
        {
            if (index == c.Count - 1)
            {
                index = 0;
            }
            else
            {
                index++;
            }
            //OnChange();
        }
        else
        {
            if (index != c.Count - 1)
            {
                index++;
                //OnChange();
            }
        }
        OnChange();
        return GetIretation;
    }
    #endregion

    #region Moving about X elements
    public T Before(int pocet)
    {
        if (pocet > c.Count)
        {
            return GetIretation;
        }
        index -= pocet;
        int dex = (index);

        if (dex == 0)
        {
        }
        else if (dex < 0)
        {
            int odecist = Math.Abs(dex);
            int vNovem = (c.Count - odecist);
            index = vNovem;
        }
        else
        {
            //index-= pocet;
            index = dex;
        }

        OnChange();
        return GetIretation;
    }
    public T Next(int pocet)
    {
        if (pocet > c.Count)
        {
            return GetIretation;
        }
        index += pocet;
        int dex = (index);
        if (dex == 0)
        {
        }
        else if (dex > c.Count)
        {
            // Zjistim o kolik a tolik posunu i v novem
            int vNovem = dex - c.Count;
            index = vNovem;
        }
        else
        {
            // 
            index = dex;
        }
        OnChange();
        return GetIretation;
    }
    #endregion

    public void ReplaceOnce(T p, T nove)
    {
        int dex = c.IndexOf(p);
        c.RemoveAt(dex);
        c.Insert(dex, nove);
    }

    #region IStatusBroadcaster Members

    public void OnChange()
    {
        if (Change != null)
        {
            Change();
        }
    }

    public event Action<string> NewStatus;

    public void OnNewStatus(string s, params object[] p)
    {
        if (NewStatus != null)
        {
            NewStatus(string.Format(s, p));
        }
    }

    #endregion
}
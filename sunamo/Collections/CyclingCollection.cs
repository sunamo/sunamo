
using System.Collections.Generic;
using System;
using System.Text;

/// <summary>
/// KolekceObihajici
/// Pro kolekci prvku, ktere lze v nekonecnem cyklu prochaet tam, pozpatku i nahodne
/// TS dava akt. stav iretace - lze nast. i prid. mezer
/// Lze pridat jeden prvek/vice, ma M pro vraceni aktualni, prechod na D/P. Posouvat se lze i po nekolika prvcich.
/// </summary>
public class CyclingCollection<T> //: IStatusBroadcaster
{
    public bool back = false;

    #region DPP
    /// <summary>
    /// PPk pro prvky
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
    /// <summary>
    /// Pro jak. operaci, ktera se tu stane
    /// Zde se vyvolava pri pridani, posunuti o jedno nebo vice, skok na jiny soubor a zmena formatovani. 
    /// Must be Action to easy import without sunamo assembly
    /// </summary>
    public event Action Change;
    /// <summary>
    /// Pr EA
    /// </summary>
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

    /// <summary>
    /// Prida A1 do this.t.
    /// VU Zmena
    /// </summary>
    /// <param name="t"></param>
    public void Add(T t)
    {
        this.c.Add(t);
        _index++;
        OnChange();
    }

    /// <summary>
    /// Prida vsechny polozky v A1 do this.t.
    /// VU Zmena
    /// </summary>
    /// <param name="k"></param>
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

    /// <summary>
    /// Vycistim pole obrazku.
    /// VU Zmena
    /// </summary>
    public void Clear()
    {
        c.Clear();
        _index = 0;
        OnChange();
    }

    /// <summary>
    /// VU Zmena
    /// Nastavi novy index, G prvek na tomto indexu a projevi i do TR:
    /// </summary>
    /// <param name="ir"></param>
    
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

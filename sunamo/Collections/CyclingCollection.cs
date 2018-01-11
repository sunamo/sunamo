
using System.Collections.Generic;
using System;
using System.Text;

/// <summary>
/// KolekceObihajici
/// Pro kolekci prvku, ktere lze v nekonecnem cyklu prochaet tam, pozpatku i nahodne
/// TS dava akt. stav iretace - lze nast. i prid. mezer
/// Lze pridat jeden prvek/vice, ma M pro vraceni aktualni, prechod na D/P. Posouvat se lze i po nekolika prvcich.
/// </summary>
public class CyclingCollection<T> : IStatusBroadcaster
{
    public bool back = false;

    #region DPP
    /// <summary>
    /// PPk pro prvky
    /// </summary>
    public List<T> t = new List<T>();
    int _index = 0;
    int index
    {
        get
        {
            if (_index < 0)
            {
                _index = 0;
            }
            else if (_index > t.Count - 1)
            {
                _index = t.Count - 1;
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
    /// Zda se maji delat mezery
    /// </summary>
    bool _MakesSpaces;
    /// <summary>
    /// Pro jak. operaci, ktera se tu stane
    /// Zde se vyvolava pri pridani, posunuti o jedno nebo vice, skok na jiny soubor a zmena formatovani. 
    /// </summary>
    public event VoidVoid Change;
    /// <summary>
    /// Pr EA
    /// </summary>
    EventArgs ea = EventArgs.Empty;
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
        this.t.Add(t);
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
            t.Add(item);
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
        t.Clear();
        _index = 0;
        OnChange();
    }

    /// <summary>
    /// VU Zmena
    /// Nastavi novy index, G prvek na tomto indexu a projevi i do TR:
    /// </summary>
    /// <param name="ir"></param>
    /// <returns></returns>
    public T SetIretation(int ir)
    {
        index = ir;
        OnChange();
        return GetIretation;
    }

    public void SetIretationWithoutEvent(int p)
    {
        index = p;
    }

    /// <summary>
    /// G akt. index
    /// </summary>
    public int ActualIndex
    {
        get
        {
            return index;
        }
    }

    

    /// <summary>
    /// GS Zda se maji v akt. iretaci  zobr. merzery mezi tokeny.
    /// VU Zmena
    /// </summary>
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

    /// <summary>
    /// G kt. prubeh irerace, napr. pro zobrazeni v statusu nebo nekde v txt.
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(ActualIndex + 1);
        if (_MakesSpaces)
        {
            sb.Append(" ");
        }
        sb.Append("/");
        if (_MakesSpaces)
        {
            sb.Append(" ");
        }
        sb.Append(t.Count.ToString());
        return sb.ToString();
    }

    public T GetIterationSimple
    {
        get
        {
            if (t.Count == 0)
            {
                return default(T);
            }
            return t[index];
        }
    }

    /// <summary>
    /// G O na akt. indexu. Pracuje s abs. hodnotou - pokud se nepodazi ziskat, zkusi prvek pred a vzad.
    /// </summary>
    public T GetIretation
    {
        get
        {
            T t2 = default(T);
            int dex = Math.Abs(index);
            if (t.Count > dex && t.Count >= dex)
            {
                t2 = t[dex];
            }
            else
	        {
                dex = Math.Abs(++index);
                if (t.Count > dex && t.Count >= dex)
            {
                t2 = t[dex];
            }
                    
                
                else
                {
                    
                        index--;
                        dex = Math.Abs(--index);
                        if (t.Count > dex && t.Count >= dex)
                        {
                            t2 = t[dex];
                        }
                        else
                        {
                            if (t.Count > 0)
                            {
                                t2 = t[0];
                            }
                            else
                            {
                                OnNewStatus("Nepodarilo se nacist prvek, p�idejte n�jak� a akci opakujte");
                            }
                        }
                        
                }
            }
        
            return t2;
        }
    }

    #region Jednoduche posouvani o 1
    /// <summary>
    /// Presune se na predchozi prvek a VU.
    /// </summary>
    /// <returns></returns>
    public T Before()
    {
        back = true;
        if (Cycling)
        {
            if (index == 0)
            {
                index = t.Count - 1;

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

    /// <summary>
    /// Presune se na dalsi prvek a VU.
    /// </summary>
    /// <returns></returns>
    public T Next()
    {
        back = false;
        if (Cycling)
        {
            if (index == t.Count - 1)
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
            if (index != t.Count - 1)
            {
                index++;
                //OnChange();
            }
        }
        OnChange();
        return GetIretation;
    }
    #endregion

    #region Posouvani o lib. mnozstvi
    /// <summary>
    /// Presune se zpet o A1 prvku
    /// </summary>
    /// <param name="pocet"></param>
    /// <returns></returns>
    public T Before(int pocet)
    {
        if (pocet > t.Count)
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
            int vNovem = (t.Count - odecist);
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

    /// <summary>
    /// Presune se o dalsich A1 prvku
    /// </summary>
    /// <param name="pocet"></param>
    /// <returns></returns>
    public T Next(int pocet)
    {
        if (pocet > t.Count)
        {
            return GetIretation;
        }
        index += pocet;
        int dex = (index);
        if (dex == 0)
        {

        }
        else if (dex > t.Count)
        {
            // Zjistim o kolik a tolik posunu i v novem
            int vNovem = dex - t.Count;
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
        int dex = t.IndexOf(p);
        t.RemoveAt(dex);
        t.Insert(dex, nove);

    }

    #region IStatusBroadcaster Members

    public void OnChange()
    {
        if (Change != null)
        {
            Change();
        }
    }

    public event VoidObjectParamsObjects NewStatus;

    public void OnNewStatus(string s, params object[] p)
    {
        if (NewStatus != null)
        {
            NewStatus(string.Format(s, p));
        }
    }

    #endregion

    
}

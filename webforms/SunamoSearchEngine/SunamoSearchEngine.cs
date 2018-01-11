using System.Collections;
/// <summary>
/// Umí i stránkování
/// Prvně musíš ve webové aplikaci, kde chceš používat, App_AppWords a App_AppSearchIndex. Samořejmě že nahradíš název App za správný název aplikace.
/// třída nemůže být statická, protože ji využívá více webů simultálně
/// </summary>
using System.Collections.Generic;
public abstract class SunamoSearchEngine
{
    protected string tableWords = null;
    protected string tableSearchIndex = null;
    // 
    protected SunamoSearchEngine(string tableWords, string tableSearchIndex)
    {
        this.tableWords = tableWords;
        this.tableSearchIndex = tableSearchIndex;
    }

    protected void ReturnWordsToUnregisterAndRegister(string s, string type, int idEntity, out List<long> unregister, out List<string> register)
    {
        // Při rozdělování automatický rozdělit pomocí interpunkčních znaků
        List<string> slova = new List<string>( SH.SplitBySpaceAndPunctuationCharsAndWhiteSpaces(s));
        for (int i = slova.Count - 1; i >= 0; i--)
        {
            if (SH.ContainsOtherChatThanLetterAndDigit(slova[i]))
            {
                slova.RemoveAt(i);
            }
        }
        List<string> slovaNew =  CA.ToLower(slova);
        List<long> slovaOldIndexy = GetWordsIDs(type, idEntity);
        List<string> slovaOld = GetWords(slovaOldIndexy);
        int sn = slovaNew.Count;
        for (int i = sn - 1; i >= 0; i--)
        {
            string item = slovaNew[i];
            int dex = slovaOld.IndexOf(item);
            if (dex != -1)
            {
                slovaNew.RemoveAt(i);
                slovaOld.RemoveAt(dex);
                slovaOldIndexy.RemoveAt(dex);
            }
        }

        unregister = slovaOldIndexy;
        register = slovaNew;
    }

    private List<string> GetWords(List<long> p)
    {
        List<string> vr = new List<string>(p.Count);
        foreach (long item in p)
        {
            vr.Add(MSStoredProceduresI.ci.SelectCellDataTableStringOneRow(tableWords, "Word", "ID", item));
        }
        return vr;
    }

    private List<long> GetWordsIDs(string type, int idEntity)
    {
        IList il = null;
        
        il = MSStoredProceduresI.ci.SelectValuesOfColumnAllRowsNumeric(tableSearchIndex, "IDWord", AB.Get("TableChar", type), AB.Get("EntityID", idEntity));
        if (il.Count == 0)
        {
            return new List<long>();
        }
        if (il[0].GetType() == typeof(int))
        {
            return CA.ToLong(il);    
        }
        return (List<long>)il;
    }

    /// <summary>
    /// POuží
    /// </summary>
    /// <param name="type"></param>
    /// <param name="idEntity"></param>
    /// <param name="s"></param>
    protected abstract void UnregisterOldWords(string tableChar, int idEntity, List<long> s);

    protected abstract void RegisterNewWords(string tableChar, int idEntity, List<string> s);

    public void DeleteFromIndex(string tableChar, int idEntity)
    {
        string newString = "";
        List<long> unregister = null;
        List<string> register = null;
        ReturnWordsToUnregisterAndRegister(newString, tableChar, idEntity, out unregister, out register);
        UnregisterOldWords(tableChar, idEntity, unregister);
        RegisterNewWords(tableChar, idEntity, register);
    }

    /// <summary>
    /// Vstupní metoda, žádné jiné metody by sji ani neměl potřebovat volat.
    /// </summary>
    /// <param name="tableChar"></param>
    /// <param name="idEntity"></param>
    /// <param name="newString"></param>
    public void RegisterNewWords(string tableChar, int idEntity, string newString, bool onlyRegister)
    {
        if (onlyRegister)
        {
            if (newString == "")
            {
                onlyRegister = false;
            }
        }
        List<string> register = null;
        if (onlyRegister)
        {
            register = new List<string>(SH.SplitBySpaceAndPunctuationCharsAndWhiteSpaces(newString.ToLower())); ;
        }
        else
        {
            List<long> unregister = null;
            ReturnWordsToUnregisterAndRegister(newString, tableChar, idEntity, out unregister, out register);
            UnregisterOldWords(tableChar, idEntity, unregister);
        }

        RegisterNewWords(tableChar, idEntity, register);
    }

    public abstract List<WordsCountSearchTargetInt> Search(string searchTerm, object o, out string[] slova);    
}

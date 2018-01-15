using System;
using System.Collections.Generic;
using System.Data;

public abstract class SunamoSearchEngineTInt : SunamoSearchEngine
{
    protected Dictionary<string, int> slovaIndexy = new Dictionary<string, int>();

    public SunamoSearchEngineTInt(string tableWords, string tableSearchIndex)
        : base(tableWords, tableSearchIndex)
    {

    }

    bool rebuild = false;

    private void Rebuild(string tableWords)
    {
        if (!rebuild)
        {
            rebuild = true;
            slovaIndexy.Clear();
            DataTable dt = MSStoredProceduresI.ci.SelectAllRowsOfColumns(tableWords, "ID,Word");
            foreach (DataRow item in dt.Rows)
            {
                string key = item.ItemArray[1].ToString();
                slovaIndexy.Add(key, int.Parse(item.ItemArray[0].ToString()));
            }
            rebuild = false;
        }
    
    }

    /// <summary>
    /// DO této metody se nikdy nesmí předat null
    /// </summary>
    /// <param name="searchTerm"></param>
    /// <returns></returns>
    protected string[] GetSearchWords(string searchTerm)
    {
        string e = searchTerm.ToLower();
        //e = SH.TextWithoutDiacritic(e);
        string[] slova = SH.SplitBySpaceAndPunctuationCharsAndWhiteSpaces(e);
        //slova = CA.ToLower(slova);
        return slova;
    }

    protected override void UnregisterOldWords(string tableChar, int idEntity, List<long> s)
    {
        // Při mazání z tabulky SearchIndex u každého výmazu kontrolovat zda bude existovat ještě nějaký jiný odkaz na slovo v tabulce Words a pokud ne, slovo úplně vymazat z tabulky Words
        foreach (long item in s)
        {
            MSStoredProceduresI.ci.DeleteOneRow(tableSearchIndex, AB.Get("IDWord", item), AB.Get("TableChar", tableChar), AB.Get("EntityID", idEntity));
            if (!MSStoredProceduresI.ci.SelectExists(tableSearchIndex, "IDWord", item))
            {
                MSStoredProceduresI.ci.DeleteOneRow(tableWords, "ID", item);
            }
        }
        Rebuild(tableWords);
    }

    protected override void RegisterNewWords(string tableChar, int idEntity, List<string> s)
    {
        foreach (string item in s)
        {
            if (item.Trim() == "")
            {
                continue;
            }
            if (item.Length > Constants.MaxLengthColumnWordInTablesWords)
            {
                continue;
            }
            string item2 = MSStoredProceduresI.ConvertToVarChar(item);
            if (slovaIndexy.Count != MSStoredProceduresI.ci.SelectCount(tableWords))
            {
                Rebuild(tableWords);
            }
            int idWord = -1;// MSStoredProceduresIRemote.ci.SelectCellDataTableIntOneRow(false, tableWords,  "ID", "Word", item2);
            if (slovaIndexy.ContainsKey(item2))
            {
                idWord = slovaIndexy[item2];
            }
            if (idWord == -1)
            {
                ITableRowWordInt w = CreateInstanceTableRowWord();
                w.word = item2;
                idWord = w.InsertToTable();
                if (!rebuild)
                {
                    slovaIndexy.Add(item2, idWord);
                }
                
            }
            ITableRowSearchIndexInt si = CreateInstanceTableRowSearchIndex();
            si.entityID = idEntity;
            si.iDWord = idWord;
            si.tableChar = tableChar;
            si.InsertToTable3(idWord);
        }
    }

    /// <summary>
    /// Ačkoliv A2 je string, vkládá se tam vždy jen 1 písmenko(char). Je to proto, že pokud bych jich tam měl více, duplikovali by se mi ve výsledku nejspíš výsledky které by obsahovalo jen slovo 1 a slvoo 2
    /// </summary>
    /// <param name="slova"></param>
    /// <param name="onlyEntityChar"></param>
    /// <returns></returns>
    protected List<SunamoSearchDataInt> GetFoundedEntities(string[] slova, string onlyEntityChar)
    {
        List<SunamoSearchDataInt> f = new List<SunamoSearchDataInt>();

        if (onlyEntityChar.Length == 1)
        {
                //int i = 0;
                foreach (string slovo in slova)
                {
                    int idWord = GetIDOfWord(slovo);
                    WordInt w = new WordInt();
                    w.id = idWord;
                    w.word = slovo;
                    // Zjistím všechny výskyty daného slova
                    DataTable dt = MSStoredProceduresI.ci.SelectDataTableSelective(tableSearchIndex, "TableChar,EntityID", AB.Get("IDWord", idWord), AB.Get("TableChar", onlyEntityChar));
                    foreach (DataRow item in dt.Rows)
                    {
                        object[] o = item.ItemArray;
                        string asiTableChar = MSTableRowParse.GetString(o, 0);
                        int asiEntityID = MSTableRowParse.GetInt(o, 1);
                        int dex = -1;
                        int fc = f.Count;
                        for (int r = 0; r < fc; r++)
                        {
                            SunamoSearchDataInt ssd = f[r];
                            if (ssd.idEntity == asiEntityID && ssd.tableChar == asiTableChar)
                            {
                                dex = r;
                                break;
                            }
                        }

                        if (dex == -1)
                        {
                            SunamoSearchDataInt ssd = new SunamoSearchDataInt();
                            ssd.idEntity = asiEntityID;
                            ssd.tableChar = asiTableChar;
                            //////DebugLogger.Instance.Write(asi.TableChar + asi.entityID);
                            ssd.Add(w);
                            f.Add(ssd);
                            //f.Add(ssd);
                        }
                        else
                        {
                            SunamoSearchDataInt ssd = f[dex];
                            ssd.Add(w);
                            //DebugLogger.Instance.Write("Přidávám " + w.word + " k " + ssd.tableChar + " " + ssd.idEntity);
                        }
                    }
                    //i++;
                }
            
        }
        else if (onlyEntityChar.Length > 1)
        {
            throw new Exception("Do 2. parametru metody SunamoSearchEngineTInt.GetFoundedEntities lze vložit pouze 1 písmenko");
        }
        else if (onlyEntityChar.Length == 0)
        {
            //int i = 0;
            foreach (string slovo in slova)
            {
                int idWord = GetIDOfWord(slovo);
                WordInt w = new WordInt();
                w.id = idWord;
                w.word = slovo;
                DataTable dt = MSStoredProceduresI.ci.SelectDataTableSelective(tableSearchIndex, "TableChar,EntityID", AB.Get("IDWord", idWord));
                foreach (DataRow item in dt.Rows)
                {
                    //TableRowLyrSearchIndex asi = new TableRowLyrSearchIndex(item.ItemArray);
                    object[] o = item.ItemArray;
                    string asiTableChar = MSTableRowParse.GetString(o, 0);
                    int asiEntityID = MSTableRowParse.GetInt(o, 1);
                    int dex = -1;
                    int fc = f.Count;
                    for (int r = 0; r < fc; r++)
                    {
                        SunamoSearchDataInt ssd = f[r];
                        if (ssd.idEntity == asiEntityID && ssd.tableChar == asiTableChar)
                        {
                            dex = r;
                            break;
                        }
                    }

                    if (dex == -1)
                    {
                        SunamoSearchDataInt ssd = new SunamoSearchDataInt();
                        ssd.idEntity = asiEntityID;
                        ssd.tableChar = asiTableChar;
                        //////DebugLogger.Instance.Write(asi.TableChar + asi.entityID);
                        ssd.Add(w);
                        f.Add(ssd);
                        //f.Add(ssd);
                    }
                    else
                    {
                        SunamoSearchDataInt ssd = f[dex];
                        ssd.Add(w);
                        //DebugLogger.Instance.Write("Přidávám " + w.word + " k " + ssd.tableChar + " " + ssd.idEntity);
                    }
                }
                //i++;
            }
        }
        else
        {
            // Nikdy se nespustí
            foreach (var item2 in onlyEntityChar)
            {
                //int i = 0;
                foreach (string slovo in slova)
                {
                    int idWord = GetIDOfWord(slovo);
                    WordInt w = new WordInt();
                    w.id = idWord;
                    w.word = slovo;
                    // Zjistím všechny výskyty daného slova
                    DataTable dt = MSStoredProceduresI.ci.SelectDataTableSelective(tableSearchIndex, "TableChar,EntityID", AB.Get("IDWord", idWord));
                    foreach (DataRow item in dt.Rows)
                    {

                        object[] o = item.ItemArray;
                        string asiTableChar = MSTableRowParse.GetString(o, 0);
                        int asiEntityID = MSTableRowParse.GetInt(o, 1);
                        int dex = -1;
                        int fc = f.Count;
                        for (int r = 0; r < fc; r++)
                        {
                            SunamoSearchDataInt ssd = f[r];
                            if (ssd.idEntity == asiEntityID && ssd.tableChar == asiTableChar)
                            {
                                dex = r;
                                break;
                            }
                        }

                        if (dex == -1)
                        {
                            SunamoSearchDataInt ssd = new SunamoSearchDataInt();
                            ssd.idEntity = asiEntityID;
                            ssd.tableChar = asiTableChar;
                            //////DebugLogger.Instance.Write(asi.TableChar + asi.entityID);
                            ssd.Add(w);
                            f.Add(ssd);
                            //f.Add(ssd);
                        }
                        else
                        {
                            SunamoSearchDataInt ssd = f[dex];
                            ssd.Add(w);
                            //DebugLogger.Instance.Write("Přidávám " + w.word + " k " + ssd.tableChar + " " + ssd.idEntity);
                        }
                    }
                    //i++;
                }
            }
        }
        return f;
    }

    protected abstract ITableRowWordInt CreateInstanceTableRowWord();
    protected abstract ITableRowSearchIndexInt CreateInstanceTableRowSearchIndex();
    protected abstract int GetIDOfWord(string slovo);

}

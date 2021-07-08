using sunamo.Constants;
using sunamo.Generators.Text;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
/// <summary>
/// In Comparing
/// </summary>
public class TextOutputGenerator
{
    private readonly static string s_znakNadpisu = AllStrings.asterisk;
    public TextBuilder sb = new TextBuilder();
    public string prependEveryNoWhite
    {
        get => sb.prependEveryNoWhite;
        set => sb.prependEveryNoWhite = value;
    }

    #region Static texts
    /// <summary>
    /// 
    /// </summary>
    public void EndRunTime()
    {
        sb.AppendLine(Messages.AppWillBeTerminated);
    }

    /// <summary>
    /// Pouze vypíše "Az budete mit vstupní data, spusťte program znovu."
    /// </summary>
    public void NoData()
    {
        sb.AppendLine(Messages.NoData);
    }

    
    #endregion

    #region Templates

    /// <summary>
    /// Napíše nadpis A1 do konzole 
    /// </summary>
    /// <param name="text"></param>
    public void StartRunTime(string text)
    {
        int delkaTextu = text.Length;
        string hvezdicky = "";
        hvezdicky = new string(s_znakNadpisu[0], delkaTextu);
        //hvezdicky.PadLeft(delkaTextu, znakNadpisu[0]);
        sb.AppendLine(hvezdicky);
        sb.AppendLine(text);
        sb.AppendLine(hvezdicky);
    }

    public void CountEvery<T>(IEnumerable<KeyValuePair<T, int>> eq)
    {
        foreach (var item in eq)
        {
            AppendLine(item.Key + AllStrings.cs + item.Value + "x");
        }
    }
    #endregion

    
   

    #region AppendLine
    public void AppendLine()
    {
        AppendLine(string.Empty);
    }

    public void AppendLine(StringBuilder text)
    {
        sb.AppendLine(text.ToString());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void AppendLine(string text)
    {
        sb.AppendLine(text);
    }

    public void AppendLineFormat(string text, params object[] p)
    {
        sb.AppendLine();

        AppendLine(SH.Format2(text, p));
    }

    public void AppendFormat(string text, params object[] p)
    {
        AppendLine(SH.Format2(text, p));
    }
    #endregion


    #region Other adding methods
    public void Header(string v)
    {
        sb.AppendLine();
        AppendLine(v);
        sb.AppendLine();
    }

    public void SingleCharLine(char paddingChar, int v)
    {
        sb.AppendLine(string.Empty.PadLeft(v, paddingChar));
    } 
    #endregion

    public override string ToString()
    {
        return sb.ToString();
    }

    

    #region List
    public void ListObject(IEnumerable files1)
    {
        List(CA.ToListString(files1));
    }

    /// <summary>
    /// If you have StringBuilder, use Paragraph()
    /// </summary>
    /// <param name="files1"></param>
    public void List(IEnumerable<string> files1)
    {
        List<string>(files1);
    }

    public void List<Value>(IEnumerable<Value> files1)
    {
        if (files1.Count() == 0)
        {
            sb.AppendLine();
        }
        else
        {
            foreach (var item in files1)
            {
                AppendLine(item.ToString());
            }
            sb.AppendLine();
        }
    }

    public void List<Header, Value>(IEnumerable<Value> files1, Header header) where Header : IEnumerable<char>
    {
        List<Header, Value>(files1, header, true, false);
    }

    public void List(IEnumerable<string> files1, string header)
    {
        List(files1, header, true, false);
    }


    public void List<Header, Value>(IEnumerable<Value> files1, Header header, bool headerWrappedEmptyLines, bool insertCount) where Header : IEnumerable<char>
    {
        if (insertCount)
        {
            header = (Header)((IEnumerable<char>)CA.JoinIEnumerable<char>(header, " (" + files1.Count() + AllStrings.rb));
        }
        if (headerWrappedEmptyLines)
        {
            sb.AppendLine();
        }
        sb.AppendLine(header + AllStrings.colon);
        if (headerWrappedEmptyLines)
        {
            sb.AppendLine();
        }
        List(files1);
    }
    #endregion

    #region Paragraph
    public void Paragraph(StringBuilder wrongNumberOfParts, string header)
    {
        string text = wrongNumberOfParts.ToString().Trim();
        Paragraph(text, header);
    }

    /// <summary>
    /// For ordinary text use Append*
    /// </summary>
    /// <param name="text"></param>
    /// <param name="header"></param>
    public void Paragraph(string text, string header)
    {
        if (text != string.Empty)
        {
            sb.AppendLine(header + AllStrings.colon);
            sb.AppendLine(text);
            sb.AppendLine();
        }
    } 
    #endregion

    

    

    public void Undo()
    {
        sb.Undo();
    }

    #region Dictionary
    public void Dictionary(Dictionary<string, int> charEntity, string delimiter)
    {
        foreach (var item in charEntity)
        {
            sb.AppendLine(item.Key + delimiter + item.Value);
        }
    }

    public void DictionaryKeyValuePair<T1, T2>(IOrderedEnumerable<KeyValuePair<T1, T2>> ordered)
    {
        
        foreach (var item in ordered)
        {
            sb.AppendLine(item.Key + AllStrings.space + item.Value);
        }

    }

    public void Dictionary(Dictionary<string, List<string>> ls)
    {
        foreach (var item in ls)
        {
            List(item.Value, item.Key);
        }
    }

    public void Dictionary<Header, Value>(Dictionary<Header, List<Value>> ls, bool onlyCountInValue = false) where Header : IEnumerable<char>
    {
        if (onlyCountInValue)
        {
            List<string> d = new List<string>(ls.Count);
            foreach (var item in ls)
            {
                d.Add(item.Key + AllStrings.space + item.Value.Count());
            }
            List(d);
        }
        else
        {
            foreach (var item in ls)
            {
                List<Header, Value>(item.Value, item.Key);
            }
        }
    }

    public void Dictionary(Dictionary<string, string> v)
    {
        foreach (var item in v)
        {
            sb.AppendLine(SF.PrepareToSerialization(item.Key, item.Value));
        }
    }

    public void Dictionary<T1, T2>(Dictionary<T1, T2> d, string deli = AllStrings.verbar)
    {
        //StringBuilder sb = new StringBuilder();
        foreach (var item in d)
        {
            sb.AppendLine(SF.PrepareToSerializationExplicitString(CA.ToList<object>(item.Key, item.Value), deli));
        }

    } 
    #endregion
}
﻿using System.IO;
using System.Text;
using System;
/// <summary>
/// InstantSB(can specify own delimiter, check whether dont exists)
/// TextBuilder(implements Undo, save to Sb or List)
/// HtmlSB(Same as InstantSB, use br)
/// </summary>
public class InstantSB //: StringWriter
{
    private StringBuilder _sb = new StringBuilder();
    private string _tokensDelimiter;

    public InstantSB(string znak)
    {
        _tokensDelimiter = znak;
    }

    public override string ToString()
    {
        string vratit = _sb.ToString();
        return vratit;
    }



    /// <summary>
    /// Nep�ipisuje se k celkov�mu v�stupu ,proto vrac� sv�j valstn�.
    /// </summary>
    /// <param name="polo�ky"></param>
    
    public void AddItems(params object[] polozky)
    {
        foreach (object var in polozky)
        {
            AddItem(var);
        }
    }

    /// <summary>
    /// Append without token delimiter
    /// </summary>
    /// <param name="o"></param>
    public void EndLine(object o)
    {
        string s = o.ToString();
        if (s != _tokensDelimiter && s != "")
        {
            _sb.Append(s);
        }
    }

    /// <summary>
    /// Jen vol� metodu AddItem s A1 s NL
    /// </summary>
    /// <param name="p"></param>
    public void AppendLine(string p)
    {
        EndLine((object)(p + Environment.NewLine));
    }

    public void AppendLine()
    {
        EndLine((object)Environment.NewLine);
    }

    public void RemoveEndDelimiter()
    {
        _sb.Remove(_sb.Length - _tokensDelimiter.Length, _tokensDelimiter.Length);
    }
}

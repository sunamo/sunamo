using System.IO;
using System.Text;
using System;
/// <summary>
/// InstantSB(can specify own delimiter, check whether dont exists)
/// TextBuilder(implements Undo, save to Sb or List)
/// HtmlSB(Same as InstantSB, use br)
/// </summary>
public class InstantSB //: StringWriter
{
    public StringBuilder sb = new StringBuilder();
    private string _tokensDelimiter;

    public InstantSB(string znak)
    {
        _tokensDelimiter = znak;
    }

    public int Length => sb.Length;

    public override string ToString()
    {
        string vratit = sb.ToString();
        return vratit;
    }



    /// <summary>
    /// Nep�ipisuje se k celkov�mu v�stupu ,proto vrac� sv�j valstn�.
    /// </summary>
    /// <param name="polo�ky"></param>
    public void AddItem(object var)
    {
        string s = var.ToString();
        if (s != _tokensDelimiter && s != "")
        {
            sb.Append(s + _tokensDelimiter);
        }
    }

    public void AddRaw(object tab)
    {
        sb.Append(tab.ToString());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="polozky"></param>
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
            sb.Append(s);
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
        sb.Remove(sb.Length - _tokensDelimiter.Length, _tokensDelimiter.Length);
    }

    public void Clear()
    {
        sb.Clear();
    }
}
/// <summary>
/// SB, kter� po ka�d� p�id. polo�ce p�id� znak.
/// </summary>
using System.IO;
using System.Text;
using System;
public class InstantSB //: StringWriter
{
    StringBuilder sb = new StringBuilder();
    string tokensDelimiter;
    public InstantSB(string znak)
    {
        this.tokensDelimiter = znak;
    }

    public override string ToString()
    {
        string vratit = sb.ToString();
        return vratit;
    }

    /// <summary>
    /// Nep�ipisuje se k celkov�mu v�stupu ,proto vrac� sv�j valstn�.
    /// </summary>
    /// <param name="polo�ky"></param>
    /// <returns></returns>
    public void AddItem(object var)
    {
        string s = var.ToString();
        if (s != tokensDelimiter && s != "")
        {
            sb.Append(s + tokensDelimiter);   
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="polozky"></param>
    /// <returns></returns>
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
        if (s != tokensDelimiter && s != "")
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



    
}

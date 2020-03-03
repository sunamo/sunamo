using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class FoundedCodeElement : IComparable<FoundedCodeElement>
{
    public FoundedCodeElement(int line, int from, int length)
    {
        this.Lenght = length;
        this.Line = line;
        this.From = from;
    }

    public int Line;
    /// <summary>
    /// Is -1 if location isnt known (search in content and so)
    /// </summary>
    public int From;
    public int Lenght;

    public int CompareTo(FoundedCodeElement other)
    {
        return SunamoComparer.Integer.Instance.Asc(Line, other.Line);
    }
}
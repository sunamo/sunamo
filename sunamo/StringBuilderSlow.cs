using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class StringBuilderDebug
{
    //StringBuilder sb = new StringBuilder();
    string s = string.Empty;
    public int Length => s.Length;

    public void AppendLine(string l)
    {
        //sb.AppendLine(l);
        s += l + Environment.NewLine;
    }

    public void Clear()
    {
        s = string.Empty;
    }
    public void Append(string v)
    {
        //sb.Append(v);
        s += v;
    }

    public void Remove(int v, int length)
    {

    }

    public override string ToString()
    {
        return s;

    }
}
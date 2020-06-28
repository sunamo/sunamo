using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Joiner<T>
{
    List<T> list = null;

    string joinWith = null;
    public Joiner(int capacity = int.MinValue) : this(AllStrings.cs)
    {

    }


    public Joiner(string joinWith, int capacity = 5)
    {
        this.joinWith = joinWith;
        list = new List<T>(capacity);
    }
    public override string ToString()
    {
        return SH.Join(joinWith, list);

    }

    public void Add(T appName)
    {
        list.Add(appName);
    }
}
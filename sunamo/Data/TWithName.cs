using System;
using System.Collections.Generic;
using System.Text;


public class TWithName<T>
{
    public T t = default(T);
    public string name = string.Empty;

    public override string ToString()
    {
        return name;
    }
}


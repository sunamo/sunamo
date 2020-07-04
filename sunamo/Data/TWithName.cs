using System;
using System.Collections.Generic;
using System.Text;


public class TWithName<T>
{
    public T t = default(T);
    /// <summary>
    /// Just first 5. letters
    /// </summary>
    public string name = string.Empty;

    public override string ToString()
    {
        return name;
    }

    public static TWithName<T> Get(string nameCb)
    {
        return new TWithName<T>() { name = nameCb };
    }
}

public class TWithName
{
    public static TWithName<object> Get(string nameCb)
    {
        return new TWithName<object>() { name = nameCb };
    }
}
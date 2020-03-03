using System;

public class TU<T, U>
{
    public T Key = default(T);
    public U Value = default(U);

    public TU()
    {
    }

    public TU(T key, U value)
    {
        this.Key = key;
        this.Value = value;
    }

    public static TU<T, U> Get(T key, U value)
    {
        return new TU<T, U>(key, value);
    }
}
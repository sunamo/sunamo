public class AB
{
    public string A = null;
    public object B = null;

    public AB(string a, object b)
    {
        A = a;
        B = b;
    }

    /// <summary>
    /// Ginstantion O AB
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static AB Get(string a, object b)
    {
        return new AB(a, b);
    }
}

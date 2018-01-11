/// <summary>
/// Je to zkratka AB Property - obsahuje vlastnosti místo veřejných proměnných
/// </summary>
public class ABP
{
    string a = null;
    object b = null;

    public string A
    {
        get
        {
            return a;
        }
        set
        {
            a = value;
        }
    }

    public object B
    {
        get
        {
            return b;
        }
        set
        {
            b = value;
        }
    }

    public ABP(string a, object b)
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
    public static ABP Get(string a, object b)
    {
        return new ABP(a, b);
    }
}

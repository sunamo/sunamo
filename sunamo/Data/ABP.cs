/// <summary>
/// Je to zkratka AB Property - obsahuje vlastnosti místo veřejných proměnných
/// </summary>
public class ABP
{
    private string _a = null;
    private object _b = null;

    public string A
    {
        get
        {
            return _a;
        }
        set
        {
            _a = value;
        }
    }

    public object B
    {
        get
        {
            return _b;
        }
        set
        {
            _b = value;
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
    public static ABP Get(string a, object b)
    {
        return new ABP(a, b);
    }
}
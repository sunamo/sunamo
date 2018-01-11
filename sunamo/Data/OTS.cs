public class OTS
{
    public object A = null;
    public object B = null;

    public static OTS Get(object a, object b)
    {
        OTS ots = new OTS();
        ots.A = a;
        ots.B = b;
        return ots;
    }

    public override string ToString()
    {
        return A.ToString() + " " + B.ToString();
    }
}

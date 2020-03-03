using System.Collections.Generic;

public partial class LinearHelper
{
    /// <summary>
    /// Do A2 zadej číslo do kterého se bude počítat včetně.
    /// </summary>
    /// <param name = "from"></param>
    /// <param name = "to"></param>
    public static List<string> GetStringListFromTo(int from, int to)
    {
        List<string> vr = new List<string>();
        to++;
        for (; from < to; from++)
        {
            vr.Add(from.ToString());
        }

        return vr;
    }
}
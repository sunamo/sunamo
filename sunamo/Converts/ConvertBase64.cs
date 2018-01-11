using System;
using System.Text;
public class ConvertBase64
{
    public static string To(string s)
    {
        return Convert.ToBase64String(Encoding.Unicode.GetBytes(s));
    }

    public static string From(string s)
    {
        try
        {
            string vr = Encoding.Unicode.GetString( Convert.FromBase64String(s));
            return vr;
        }
        catch (Exception)
        {

            return s;
        }
    }
}

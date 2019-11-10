using System;
using System.Text;
public class ConvertBase64
{
    //public static string To(string s)
    //{
    //    return Convert.ToBase64String(Encoding.Unicode.GetBytes(s));
    //}

    //public static string From(string s)
    //{
    //    try
    //    {
    //        string vr = Encoding.Unicode.GetString(Convert.FromBase64String(s));
    //        return vr;
    //    }
    //    catch (Exception ex)
    //    {
    //        return s;
    //    }
    //}

    public static string To(string plainText)
    {
        var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
        return System.Convert.ToBase64String(plainTextBytes);
    }

    public static string From(string base64EncodedData)
    {
        var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
        return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
    }
}

using System;
using System.Text;
public class ConvertBase64
{
    public static string To(string plainText)
    {
        var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
        return System.Convert.ToBase64String(plainTextBytes);
    }

    public static string From(string base64EncodedData)
    {
        var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
        string r = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        return r;
    }
}
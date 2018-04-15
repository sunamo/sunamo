using System;
using System.Runtime.CompilerServices;
using System.Text;
/// <summary>
/// Text Templates
/// </summary>
public class TT
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string NameValue(string name, string value)
    {
        return name.TrimEnd(':') + ": " + value;
    }

    public static string NameValue(ABC winrar, string delimiter)
    {
        StringBuilder builder = new StringBuilder();
        foreach (var item in winrar)
        {
            builder.Append(TT.NameValue(item.A, item.B.ToString()) + delimiter);
        }
        return builder.ToString();
    }
}

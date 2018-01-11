using System.Runtime.CompilerServices;
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
}

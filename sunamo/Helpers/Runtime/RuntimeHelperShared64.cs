using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public partial class RuntimeHelper
{
    public static T CastToGeneric<T>(object o)
    {
        return (T)o;
    }

    private static bool? _console_present;

    public static bool IsConsole2()
    {
        if (_console_present == null)
        {
            _console_present = true;
            try { int window_height = Console.WindowHeight; }
            catch { _console_present = false; }
        }
        return _console_present.Value;
    }

    public static void EmptyDummyMethod()
    {
    }

    public static void EmptyDummyMethod(string s, params object[] o)
    {
    }

    public static void EmptyDummyMethod(TypeOfMessage t, string s, params object[] o)
    {
    }
}

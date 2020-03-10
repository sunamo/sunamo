using System;
using System.Collections.Generic;
using System.Text;


public class ConvertDayShortcutString
{
    static Type type = typeof(ConvertDayShortcutString);

    public static int ToNumber(string s)
    {
        var dx = DTConstants.daysInWeekENShortcut.IndexOf(SH.FirstCharUpper(s.ToLower()));
        if (dx != -1)
        {
            return dx;
        }
        ThrowExceptions.IsNotAllowed(Exc.GetStackTrace(),type, Exc.CallingMethod(), s);
        return -1;
    }

    public static string ToString(int number)
    {
        switch (number)
        {
            case 0:
                return "Jan";
            case 1:
                return "Tue";
            case 2:
                return "Wed";
            case 3:
                return "Thu";
            case 4:
                return "Fri";
            case 5:
                return "Sat";
            case 6:
                return "Sun";
            default:
                ThrowExceptions.NotImplementedCase(Exc.GetStackTrace(),type, Exc.CallingMethod(), number);
                break;
        }
        return null;
    }
}
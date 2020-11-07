using System;
public class ConvertMonthNumberString //: IConvertNumberString
{
    /// <summary>
    /// A1 is full name of month in EN
    /// </summary>
    /// <param name="s"></param>
    public static int ToNumber(string s)
    {
        switch (s)
        {
            case XlfKeys.January:
                return 1;
            case XlfKeys.February:
                return 2;
            case XlfKeys.March:
                return 3;
            case XlfKeys.April:
                return 4;
            case "May":
                return 5;
            case XlfKeys.June:
                return 6;
            case XlfKeys.July:
                return 7;
            case XlfKeys.August:
                return 8;
            case XlfKeys.September:
                return 9;
            case XlfKeys.October:
                return 10;
            case XlfKeys.November:
                return 11;
            case XlfKeys.December:
                return 12;
        }
        ThrowExceptions.Custom(Exc.GetStackTrace(), type, Exc.CallingMethod(),"\u0160patn\u00FD anglick\u00FD n\u00E1zev m\u011Bs\u00EDce " + s + " metod\u011B ConvertMonthNumberString.ToNumber()");
        return 0;
    }

    static Type type = typeof(ConvertMonthNumberString);

    public static string ToString(int number)
    {
        switch (number)
        {
            case 1:
                return XlfKeys.January;
            case 2:
                return XlfKeys.February;
            case 3:
                return XlfKeys.March;
            case 4:
                return XlfKeys.April;
            case 5:
                return "May";
            case 6:
                return XlfKeys.June;
            case 7:
                return XlfKeys.July;
            case 8:
                return XlfKeys.August;
            case 9:
                return XlfKeys.September;
            case 10:
                return XlfKeys.October;
            case 11:
                return XlfKeys.November;
            case 12:
                return XlfKeys.December;

            default:
                break;
        }
        return null;
    }
}
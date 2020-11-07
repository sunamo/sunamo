using System;
public static class ConvertMonthShortcutFullName //: IConvertShortcutFullName
{
static Type type = typeof(ConvertMonthShortcutFullName);
    public static string FromShortcut(string shortcut)
    {
        switch (shortcut)
        {
            case "Jan":
                return XlfKeys.January;
            case "Feb":
                return XlfKeys.February;
            case "Mar":
                return XlfKeys.March;
            case "Apr":
                return XlfKeys.April;
            case "May":
                return "May";
            case "Jun":
                return XlfKeys.June;
            case "Jul":
                return XlfKeys.July;
            case "Aug":
                return XlfKeys.August;
            case "Sep":
                return XlfKeys.September;
            case "Oct":
                return XlfKeys.October;
            case "Nov":
                return XlfKeys.November;
            case "Dec":
                return XlfKeys.December;
            default:
                break;
        }
        return null;
    }
    public static string ToShortcut(string fullName)
    {
        switch (fullName)
        {
            case XlfKeys.January:
                return "Jan";
            case XlfKeys.February:
                return "Feb";
            case XlfKeys.March:
                return "Mar";
            case XlfKeys.April:
                return "Apr";
            case "May":
                return "May";
            case XlfKeys.June:
                return "Jun";
            case XlfKeys.July:
                return "Jul";
            case XlfKeys.August:
                return "Aug";
            case XlfKeys.September:
                return "Sep";
            case XlfKeys.October:
                return "Oct";
            case XlfKeys.November:
                return "Nov";
            case XlfKeys.December:
                return "Dec";
            default:
                break;
        }
        return null;
    }
}
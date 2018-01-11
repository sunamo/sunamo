using System;
public static class ConvertMonthShortcutFullName //: IConvertShortcutFullName
{
    public static string FromShortcut(string shortcut)
    {
        switch (shortcut)
        {
            case "Jan":
                return "January";
            case "Feb":
                return "February";
            case "Mar":
                return "March";
            case "Apr":
                return "April";
            case "May":
                return "May";
            case "Jun":
                return "June";
            case "Jul":
                return "July";
            case "Aug":
                return "August";
            case "Sep":
                return "September";
            case "Oct":
                return "October";
            case "Nov":
                return "November";
            case "Dec":
                return "December";

            default:
                break;
        }
        //throw new Exception("Neznámý název měsíce");
        return null;
    }

    public static string ToShortcut(string fullName)
    {
        switch (fullName)
        {
            case "January":
                return "Jan";
            case "February":
                return "Feb";
            case "March":
                return "Mar";
            case "April":
                return "Apr";
            case "May":
                return "May";
            case "June":
                return "Jun";
            case "July":
                return "Jul";
            case "August":
                return "Aug";
            case "September":
                return "Sep";
            case "October":
                return "Oct";
            case "November":
                return "Nov";
            case "December":
                return "Dec";

            default:
                break;
        }
        //throw new Exception("Neznámý název měsíce");
        return null;
    }
}

using System;
public class ConvertMonthNumberString //: IConvertNumberString
{
    /// <summary>
    /// A1 je název měsíce v AJ
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static int ToNumber(string s)
    {
        switch (s)
        {
            case "January":
                return 1;
            case "February":
                return 2;
            case "March":
                return 3;
            case "April":
                return 4;
            case "May":
                return 5;
            case "June":
                return 6;
            case "July":
                return 7;
            case "August":
                return 8;
            case "September":
                return 9;
            case "October":
                return 10;
            case "November":
                return 11;
            case "December":
                return 12;

        }
        throw new Exception("Špatný anglický název měsíce (" + s + ") v metodě ConvertMonthNumberString.ToNumber()");
    }

    /// <summary>
    /// Vrací anglický název měsíce
    /// </summary>
    /// <param name="number"></param>
    /// <returns></returns>
    public static string ToString(int number)
    {
        switch (number)
        {
            case 1:
                return "January";
            case 2:
                return "February";
            case 3:
                return "March";
            case 4:
                return "April";
            case 5:
                return "May";
            case 6:
                return "June";
            case 7:
                return "July";
            case 8:
                return "August";
            case 9:
                return "September";
            case 10:
                return "October";
            case 11:
                return "November";
            case 12:
                return "December";

            default:
                break;
        }
        //return "Neplatné číslo měsíce";
        return null;
    }
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

public partial class DTHelperEn
{
    #region Parse
    #region Only Date
    /// <summary>
    /// return MinValue when fail
    /// </summary>
    /// <param name="input"></param>
    
    public static DateTime CalculateStartOfPeriod(string AddedAgo)
    {
        int days = -1;
        int number = -1;

        var arg = SH.SplitNone(AddedAgo, AllStrings.us);
        if (arg.Length() == 2)
        {
            TryParse.Integer dt = new TryParse.Integer();
            if (dt.TryParseInt(arg[0].ToString()))
            {
                number = dt.lastInt;
            }
            else
            {
                number = 1;
            }

            switch (arg[1])
            {
                case "days":
                    days = number;
                    break;
                case "weeks":
                    days = 7 * number;
                    break;
                case "years":
                    days = 365 * number;
                    break;
                case "months":
                    days = 31 * number;
                    break;
                default:
                    days = 1;
                    break;
            }
        }
        days *= -1;
        return DateTime.Today.AddDays(days);
    }
    #endregion
}
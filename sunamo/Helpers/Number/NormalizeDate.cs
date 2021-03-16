using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class NormalizeDate
{
    public static DateTime From(string s)
    {
        bool december = false;

        if (s.StartsWith(AllStrings.dash))
        {
            december = true;
            s = s.TrimStart(AllChars.dash);
        }

        var y = s.Substring(0, 2);
        var m = s.Substring(2, 1);
        var d = s.Substring(3, 2);

        var firstLetter = dp[0];
    }

    public static string To(DateTime dt)
    {
        bool timesMinus1 = false;
        bool addFour = false;

        var y = DTHelperGeneral.ShortYear(dt.Year);
        // months never start with zero
        var m = dt.Month;
        var ms2 = m.ToString();
        if (m > 10)
        {
            var ms = NH.MakeUpTo2NumbersToZero(m);

            if (ms[0] == '1')
            {
                if (ms[0] == '2')
                {
                    timesMinus1 = true;
                }
                addFour = true;
                ms2 = ms[1].ToString();
            }
        }

        /*
Na prvním místě může být 0,1,2,3
        listopad = 4,5,6,7
        prosinec = -4,5,6,7
         */
        var d = NH.MakeUpTo2NumbersToZero(dt.Day);
        var firstChar = d[0];

        StringBuilder sb = new StringBuilder();
        if (timesMinus1)
        {
            sb.Append(AllChars.dash);
        }
        int firstChar2 = int.Parse(firstChar.ToString());
        if (addFour)
        {
            firstChar2 += 4;
        }
        sb.Append(y);
        sb.Append(ms2);
        sb.Append(firstChar2);
        sb.AppendLine(d[1].ToString());

        var result = sb.ToString();
        return result;
    }
}

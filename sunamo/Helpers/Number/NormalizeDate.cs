﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class NormalizeDate
{
    public static DateTime From(short sh)
    {
        var s = sh.ToString();
        bool december = false;
        s = s.Trim();

        if (s.StartsWith(AllStrings.dash))
        {
            december = true;
            s = s.TrimStart(AllChars.dash);
        }

        var y = s.Substring(0, 2);
        var m = s.Substring(2, 1);
        var d = s.Substring(3, 2);

        var firstLetter = int.Parse( d[0].ToString());
        if (firstLetter > 3)
        {
            m = "1" + m;
            var f = (firstLetter - 4).ToString()[0];
            var s2 = d[1].ToString();
            d = f + s2;
        }
        else if (m == "0")
        {
            m = "10";
        }

        var longYear = DTHelperGeneral.LongYear(y);

        DateTime dt = new DateTime(int.Parse(longYear), int.Parse(m), int.Parse(d));
        return dt;
    }

    public static short To(DateTime dt)
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
        else if (m == 10)
        {
            ms2 = "0";
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
        sb.Append(d[1].ToString());

        var result = sb.ToString();
        return short.Parse( result);
    }
}
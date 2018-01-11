using System;
using System.Collections.Generic;
public class Timestamp
{
    public static string Get(DateTime dtTo4)
    {
        return " T" + SH.JoinMakeUpTo2NumbersToZero('_', dtTo4.Hour, dtTo4.Minute, dtTo4.Second);
    }



    public static string[] GetAllTimeStamps(string p)
    {
        List<string> vr = new List<string>(); 
        string[] s = SH.Split(p, ' ', '.');
        foreach (var item in s)
        {
            if (item.Length == 9)
            {
                var ch0 = item[0];
                var ch1 = item[1];
                var ch2 = item[2];
                var ch4 = item[4];
                var ch5 = item[5];
                var ch7 = item[7];
                var ch8 = item[8];
                if (ch0 == 'T' &&char.IsDigit(ch1) && char.IsDigit(ch2) && char.IsDigit(ch4) && char.IsDigit(ch5) && char.IsDigit(ch7) && char.IsDigit(ch8))
                {
                    vr.Add(item);
                }
            }
        }
        return vr.ToArray();
    }
}

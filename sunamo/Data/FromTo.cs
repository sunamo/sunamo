using sunamo;
using System;
using System.Collections.Generic;
using System.Diagnostics;
public class FromTo : IParser
{
    public FromTo()
    {

    }

    public FromTo(int from, int to)
    {
        this.from = from;
        this.to = to;
    }

    public int from = 0;
    public int to = 0;

    public void Parse(string input)
    {
        List<string> v = null;
        if (input.Contains(AllStrings.dash))
        {
            v = SH.Split(input, AllChars.dash);
        }
        else 
        {
            v = CA.ToListString(input);
        }
        int v0 = ReturnSecondsFromTimeFormat(v[0]);
        from = v0;
        if (CA.HasIndex(1, v))
        {
            int v1 = ReturnSecondsFromTimeFormat(v[1]);
            to = v1;
        }

        if (from < 0)
        {

        }

        if (from > 0)
        {

        }
    }

    private int ReturnSecondsFromTimeFormat(string v)
    {
        int result = 0;
        if (v.Contains(AllStrings.colon))
        {
            var parts = SH.SplitToIntList(v, AllStrings.colon);
            result += parts[0] * (int)DTConstants.secondsInHour;
            result += parts[1] * (int)DTConstants.secondsInMinute;
        }
        else
        {
            result += int.Parse(v) * (int)DTConstants.secondsInHour;
        }
        return result;
    }

    public override string ToString()
    {
        if (to != 0)
        {
            return $"{DTHelperCs.ToShortTimeFromSeconds(from)}-{DTHelperCs.ToShortTimeFromSeconds(to)}";
        }
        return $"{DTHelperCs.ToShortTimeFromSeconds(from)}";
    }
}

public class FromToWord
{
    public int from = 0;
    public int to = 0;
    public string word = "";
}

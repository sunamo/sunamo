using sunamo;
using System;
using System.Collections.Generic;
using System.Diagnostics;
/// <summary>
/// Must have always entered both from and to
/// None of event could have unlimited time!
/// </summary>
public class FromTo : IParser
{
    bool empty = false;

    public FromTo()
    {
    }

    /// <summary>
    /// Use Empty contstant outside of class
    /// </summary>
    /// <param name="empty"></param>
    private FromTo(bool empty)
    {
        this.empty = empty;
    }

    public FromTo(int from, int to, bool useDateTime = true)
    {
        this.from = from;
        this.to = to;
        this.useDateTime = useDateTime;
    }

    public bool useDateTime = true;

    public int from = 0;
    public int to = 0;
    public static FromTo Empty = new FromTo(true);

    /// <summary>
    /// After it could be called IsFilledWithData
    /// </summary>
    /// <param name="input"></param>
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
    }

    public bool IsFilledWithData()
    {
        return from != 0 && to != 0;
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
        if (empty)
        {
            return string.Empty;
        }
        else
        {
            if (useDateTime)
            {
                if (to != 0)
                {
                    return $"{DTHelperCs.ToShortTimeFromSeconds(from)}-{DTHelperCs.ToShortTimeFromSeconds(to)}";
                }
                return $"{DTHelperCs.ToShortTimeFromSeconds(from)}";
            }
            return from + "-" + to;
        }
    }
}

public class FromToWord
{
    public int from = 0;
    public int to = 0;
    public string word = "";
}

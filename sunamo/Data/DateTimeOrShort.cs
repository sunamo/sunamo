using sunamo.Essential;
using System;
/// <summary>
/// 
/// </summary>
public class DateTimeOrShort
{
    public short Item1;
    public DateTime Item2;
    bool useDt = false;

    public object Value { get
        {
            if (ThisApp.useShortAsDt && !useDt)
            {
                return Item1;
            }
            else
            {
                return Item2;
            }
        }
}

    public static DateTimeOrShort Sh(DateTime v)
    {
        return Sh(NormalizeDate.To(v));
    }

    public static DateTimeOrShort Sh(short v)
    {
        DateTimeOrShort d = new DateTimeOrShort();
        d.Item1 = v;
        return d;
    }

    public static DateTimeOrShort Dt(DateTime item2)
    {
        DateTimeOrShort d = new DateTimeOrShort();
        d.useDt = true;
        d.Item2 = item2;
        return d;
    }
}
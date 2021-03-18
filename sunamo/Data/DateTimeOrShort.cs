using sunamo.Essential;
using System;
/// <summary>
/// 
/// </summary>
public class DateTimeOrShort
{
    public short Item1;
    public DateTime Item2;

    public object Value { get
        {
            if (ThisApp.useShortAsDt)
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
}
using System;
using System.Windows;


public class SizeH
{
    static Type type = typeof(SizeH);

    public static Size Divide(Size s, double div)
    {
        return new Size(s.Width / div, s.Height / div);
    }

    public static Size Multiply(Size s, double mul)
    {
        return new Size(s.Width * mul, s.Height * mul);
    }

    public static Size Multiply(Size s, int dpiXPrinter, int dpiYPrinter)
    {
        return new Size(s.Width * dpiXPrinter, s.Height * dpiYPrinter);
    }

    public static SunamoSize ShringUnder(object init2, object max2)
    {
        var init = CastSize(init2);
        var max = CastSize(max2);


        if (AtLeastOneDimensionOfFirstLargerThanSecond( init, max,false))
        {
            while (true)
            {
                init.Width *= 0.95;
                init.Height *= 0.95;

                if (AtLeastOneDimensionOfFirstLargerThanSecond(init, max, true))
                {
                    break;
                }
            }
        }

        return init;
    }

    public static bool AtLeastOneDimensionOfFirstLargerThanSecond( object init2, object max2, bool allMustBeLower)
    {
        var init = CastSize(init2);
        var max = CastSize(max2);

        var b1 = init.Width > max.Width;
        var b2 = init.Height > max.Height;

        if (init.IsNegativeOrZero())
        {
            // In if is often break, return true to quit from cycle
            return true;
        }

        if (allMustBeLower)
        {
            var vr = !b1 && !b2;

            return vr;
        }

        return b1 || b2;
    }

    public static SunamoSize CastSize(object s)
    {
        var t = s.GetType();
        if (t == typeof(DesktopSize))
        {
            return (DesktopSize)s;
        }
        else if (t == typeof(SunamoSize))
        {
            return (SunamoSize)s;
        }
        else if (t == typeof(System.Windows.Size))
        {
            var c = (System.Windows.Size)s;
            return c.ToSunamo();
        }
        else if (t == typeof(System.Drawing.Size))
        {
            var c = (System.Drawing.Size)s;
            return c.ToSunamo();
        }
        else if (t == typeof(System.Drawing.SizeF))
        {
            var c = (System.Drawing.SizeF)s;
            return c.ToSunamo();
        }
        else
        {
            ThrowExceptions.NotImplementedCase(Exc.GetStackTrace(),type, Exc.CallingMethod(), t);
        }
        return null;
    }

    public static bool OneDimensionOfFirstLargerThanSecond(object renderSize, object maxSize)
    {
        var s1 = CastSize(renderSize);
        var s2 = CastSize(maxSize);

        bool l1 = s1.Width > s2.Width;
        bool l2 = s1.Height > s2.Height;

        if ((l1 && !l2) || !l1 && l2)
        {
            return true;
        }
        return false;
    }

    public static SunamoSize EnlargeUnder(object init2, object max2)
    {
        var init = CastSize(init2);
        var max = CastSize(max2);

        if (AtLeastOneDimensionOfFirstLargerThanSecond(max, init, false))
        {
            while (true)
            {
                init.Width *= 1.05;
                init.Height *= 1.05;

                if (AtLeastOneDimensionOfFirstLargerThanSecond( init, max, false))
                {
                    // Init is in both direction larger
                    init.Width *= 0.95;
                    init.Height *= 0.95;

                    break;
                }
            }
        }
        return init;
    }
}
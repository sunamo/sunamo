using System;
using System.Windows;

public class SizeH
{
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
}

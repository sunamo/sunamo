using System;
using System.Windows;
using Windows.Foundation;
using Windows.Graphics.Display;
using Windows.UI.Xaml.Controls;

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
    public static Size Plus(Size s, int v)
    {
        return new Size(s.Width + v, s.Height + v);
    }
    public static Size Minus(Size s, int v)
    {
        return new Size(s.Width - v, s.Height - v);
    }
    public static double OverallWidth(TextBlock tbKeywords)
    {
        return tbKeywords.Margin.Left + tbKeywords.Padding.Left + tbKeywords.ActualWidth + tbKeywords.Padding.Right + tbKeywords.Margin.Right;
    }

    public static double OverallWidth(Button tbKeywords)
    {
        return tbKeywords.Margin.Left + tbKeywords.Padding.Left + tbKeywords.ActualWidth + tbKeywords.Padding.Right + tbKeywords.Margin.Right;
    }

    public static double MarginLR(StackPanel tbKeywords)
    {
        return tbKeywords.Margin.Left + tbKeywords.Margin.Right;
    }

    public static double OverallTB(ProgressBar pbTop)
    {
        return MarginTB(pbTop) + PaddingTB(pbTop) + pbTop.Height;
    }

    

    private static double PaddingTB(ProgressBar pbTop)
    {
        return pbTop.Padding.Top + pbTop.Padding.Bottom;
    }

    private static double MarginTB(ProgressBar pbTop)
    {
        return pbTop.Margin.Top + pbTop.Margin.Bottom;
    }

    public static double OverallTB(StackPanel spKeywords)
    {
        return MarginTB(spKeywords) + PaddingTB(spKeywords) + spKeywords.Height;
    }

    /// <summary>
    /// 0
    /// </summary>
    /// <param name="spKeywords"></param>
    /// <returns></returns>
    private static double PaddingTB(StackPanel spKeywords)
    {
        return spKeywords.Padding.Top + spKeywords.Padding.Bottom;
    }

    private static double MarginTB(StackPanel spKeywords)
    {
        return spKeywords.Margin.Top + spKeywords.Margin.Bottom;
    }

    public static double PaddingTB(ListView lvApps)
    {
        return lvApps.Padding.Top + lvApps.Padding.Bottom;
    }

    public static double MarginTB(ListView lvApps)
    {
        return lvApps.Margin.Top + lvApps.Margin.Bottom;
    }

    public static double RecalculateSizeWithScaleFactor(double s)
    {
        var scaleFactor = DisplayInformation.GetForCurrentView().RawPixelsPerViewPixel;
        return s * scaleFactor;
    }
}

using shared.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

public  class ColorHelper
    {
    #region Mono
    public static bool IsColorLight(Color clr)
    {
        // Bude 0 pro černou barvu, 254.99999999999997 pro bílou
        double dd = .222 * clr.R + .707 * clr.G + .071 * clr.B;
        return dd > 128;
    }

    public static System.Drawing.Color ConvertColorFromWindowsMediaToDrawing(Color v)
    {
        return System.Drawing.Color.FromArgb(v.A, v.R, v.G, v.B);
    }

    public static bool IsColorSimilar(System.Windows.Media.Color a, System.Windows.Media.Color b, int threshold = 50)
    {
        int r = (int)a.R - b.R;
        int g = (int)a.G - b.G;
        int b2 = (int)a.B - b.B;
        return (r * r + g * g + b2 * b2) <= threshold * threshold;
    }

    public static PixelColor PixelColorFromDrawingColor(System.Windows.Media.Color color, byte? alpha)
    {
        if (alpha == null)
        {
            alpha = color.A;
        }
        PixelColor white2 = new PixelColor() { Alpha = alpha.Value, Red = color.R, Green = color.G, Blue = color.B };
        return white2;
    }
    #endregion

    #region Mono
    public static WriteableBitmap SwapColor(BitmapSource bi, PixelColor bgPixelColor, PixelColor fgPixelColorFg, PixelColor definitelyFgPixelColor)
    {
        PixelColor balckZero = DrawingColorHelper.PixelColorFromDrawingColor(Color.Black, 0);
        WriteableBitmap wb = new WriteableBitmap(bi);
        var pxs = BitmapSourceHelper.GetPixels(bi);
        var first = pxs[0, 0];
        for (int i = 0; i < pxs.GetLength(0); i++)
        {
            for (int y = 0; y < pxs.GetLength(1); y++)
            {

                var pxsi = pxs[i, y];
#if DEBUG
                //DebugLogger.Instance.Write(pxsi.Alpha + "-" + pxsi.Red + "-" + pxsi.Green + "-" + pxsi.Blue);
#endif

                bool b = shared.ColorHelper.IsColorSame(bgPixelColor, pxsi) || shared.ColorHelper.IsColorSame(balckZero, pxsi);
                bool b1 = shared.ColorHelper.IsColorSame(definitelyFgPixelColor, pxsi);
                bool b2 = shared.ColorHelper.IsColorSame(fgPixelColorFg, pxsi);
                if (!b)
                {
                    if (b1 || b2)
                    {


                        if (b2)
                        {
                            pxs[i, y] = definitelyFgPixelColor;
                        }
                        else
                        {
                            pxs[i, y] = fgPixelColorFg;
                        }
                    }
                }

            }
        }

        BitmapSourceHelper.PutPixels(wb, pxs, 0, 0);
        return wb;
    }

    public static WriteableBitmap ReplaceAlpha(BitmapSource bi, PixelColor bgPixelColor)
    {
        PixelColor balckZero = DrawingColorHelper.PixelColorFromDrawingColor(System.Drawing.Color.Black, 0);
        WriteableBitmap wb = new WriteableBitmap(bi);
        var pxs = BitmapSourceHelper.GetPixels(bi);
        var first = pxs[0, 0];
        for (int i = 0; i < pxs.GetLength(0); i++)
        {
            for (int y = 0; y < pxs.GetLength(1); y++)
            {

                var pxsi = pxs[i, y];
                if (pxsi.Alpha < 255)
                {

                    pxs[i, y].Alpha = 0;
                }

            }
        }

        BitmapSourceHelper.PutPixels(wb, pxs, 0, 0);
        return wb;
    }
    #endregion
}


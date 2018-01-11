using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
/// <summary>
/// Posloupnost je BitmapImage (sealed) -> BitmapSource (abstract) -> ImageSource (abstract)
/// </summary>
public static class BitmapSourceHelper
{
#if UNSAFE
  public static  unsafe static void CopyPixels(this BitmapSource source, PixelColor[,] pixels, int stride, int offset)
  {
    fixed(PixelColor* buffer = &pixels[0, 0])
      source.CopyPixels(
        new Int32Rect(0, 0, source.PixelWidth, source.PixelHeight),
        (IntPtr)(buffer + offset),
        pixels.GetLength(0) * pixels.GetLength(1) * sizeof(PixelColor),
        stride);
  }
#else
    public static void CopyPixels(this BitmapSource source, PixelColor[,] pixels, int stride, int offset)
    {
        var height = source.PixelHeight;
        var width = source.PixelWidth;
        var pixelBytes = new byte[height * width * 4];
        source.CopyPixels(pixelBytes, stride, 0);
        int y0 = offset / width;
        int x0 = offset - width * y0;
        for (int y = 0; y < height; y++)
            for (int x = 0; x < width; x++)
                pixels[x + x0, y + y0] = new PixelColor
                {
                    Blue = pixelBytes[(y * width + x) * 4 + 0],
                    Green = pixelBytes[(y * width + x) * 4 + 1],
                    Red = pixelBytes[(y * width + x) * 4 + 2],
                    Alpha = pixelBytes[(y * width + x) * 4 + 3],
                };
    }
#endif



    public static PixelColor[,] GetPixels(BitmapSource source)
    {
        if (source.Format != PixelFormats.Bgra32)
            source = new FormatConvertedBitmap(source, PixelFormats.Bgra32, null, 0);

        int width = source.PixelWidth;
        int height = source.PixelHeight;
        byte[] byteArray = new byte[width  * height *4];
        //int nStride = (source.PixelWidth * source.Format.BitsPerPixel + 7) / 8;
        source.CopyPixels(byteArray, width *4, 0);
        PixelColor[,] vr = new PixelColor[width, height];
        var colorArray = new PixelColor[byteArray.Length / 4 ];
        for (var i = 0; i < byteArray.Length; i += 4)
        {
            var color = new PixelColor { Alpha = byteArray[i + 0], Red = byteArray[i + 1], Green = byteArray[i + 2], Blue = byteArray[i + 3] };
            
            colorArray[i / 4] = color;
        }

        vr = CA.OneDimensionArrayToTwoDirection<PixelColor>(colorArray, source.PixelWidth);
        return vr;
    }

    public static void PutPixels(WriteableBitmap bitmap, PixelColor[,] pixels, int x, int y)
    {
        int width = pixels.GetLength(1);
        int height = pixels.GetLength(0);
        bitmap.WritePixels(new Int32Rect(0, 0, width, height), pixels, width * 4, x, y);
    }
}




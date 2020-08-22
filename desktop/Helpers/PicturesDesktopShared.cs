using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

public partial class PicturesDesktop
{
    /// <summary>
    /// A1 must be BitmapSource, not ImageSource
    /// Not use Lunapic or my code to create favicon. Always download image from net
    /// </summary>
    /// <param name="sourceImage"></param>
    /// <param name="transparentColor"></param>
    public static BitmapSource MakeTransparentBitmap(BitmapSource sourceImage, System.Windows.Media.Color transparentColor)
    {
        if (sourceImage.Format != PixelFormats.Bgra32) //if input is not ARGB format convert to ARGB firstly
        {
            sourceImage = new FormatConvertedBitmap(sourceImage, PixelFormats.Bgra32, null, 0.0);
        }

        int stride = (sourceImage.PixelWidth * sourceImage.Format.BitsPerPixel) / 8;

        byte[] pixels = new byte[sourceImage.PixelHeight * stride];

        sourceImage.CopyPixels(pixels, stride, 0);

        byte red = transparentColor.R;
        byte green = transparentColor.G;
        byte blue = transparentColor.B;
        for (int i = 0; i < sourceImage.PixelHeight * stride; i += (sourceImage.Format.BitsPerPixel / 8))
        {

            if (pixels[i] == blue
            && pixels[i + 1] == green
            && pixels[i + 2] == red)
            {
                pixels[i + 3] = 0;
            }

        }

        BitmapSource newImage
            = BitmapSource.Create(sourceImage.PixelWidth, sourceImage.PixelHeight,
                            sourceImage.DpiX, sourceImage.DpiY, PixelFormats.Bgra32, sourceImage.Palette, pixels, stride);

        return newImage;
    }
}
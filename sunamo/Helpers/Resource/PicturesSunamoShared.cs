using sunamo.Data;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public partial class PicturesSunamo
{
    private static List<string> s_supportedExtensionForResize = new List<string> {
        "png", "jpg", "jpeg", "gif"
    };

    public static string ExtensionFromImage(Image mg)
    {
        var imf2 = mg.RawFormat;
        var imf = imf2.Guid;
        if (imf == ImageFormat.Jpeg.Guid)
        {
            return AllExtensions.jpg;
        }
        else if (imf == ImageFormat.Gif.Guid)
        {
            return AllExtensions.gif;
        }
        else if (imf == ImageFormat.Bmp.Guid)
        {
            return AllExtensions.bmp;
        }
        else if (imf == ImageFormat.Icon.Guid)
        {
            return AllExtensions.ico;
        }
        else if (imf == ImageFormat.Tiff.Guid)
        {
            return AllExtensions.tiff;
        }
        else if (imf == ImageFormat.Wmf.Guid)
        {
            return AllExtensions.wmf;
        }
        else if (imf == ImageFormat.Emf.Guid)
        {
            return AllExtensions.emf;
        }
        else if (imf == ImageFormat.Exif.Guid)
        {
            return AllExtensions.exif;
        }
        else if (imf == ImageFormat.MemoryBmp.Guid)
        {
            return AllExtensions.bmp;
        }
        else 
        {
            ThrowExceptions.NotImplementedCase(Exc.GetStackTrace(), type, Exc.CallingMethod(), imf);
        }
        return null;
    }

    static Type type = typeof(PicturesSunamo);

    /// <summary>
    /// Vypočte optimální šířku v případě že obrázek je postaven na výšku.
    /// </summary>
    /// <param name="p"></param>
    /// <param name="p_2"></param>
    /// <param name="p_3"></param>
    public static SunamoSize CalculateOptimalSizeHeight(int width, int height, int maxHeight)
    {
        SunamoSize vr = new SunamoSize(width, height);
        int vyskaSloupce = maxHeight;
        if (height > vyskaSloupce)
        {
            vr.Height = vyskaSloupce;

            // mohl by ses ještě rozhodovat jestli round, nebo floor, nebo ceil
            vr.Width = vyskaSloupce * width / height;
        }

        return vr;
    }


    public static bool GetImageFormatFromExtension1(string filePath, out string ext)
    {
        ext = FS.GetExtension(filePath).TrimStart(AllChars.dot).ToLower();

        if (PicturesSunamo.IsSupportedResizeForExtension(ext))
        {
            return true;
        }
        return false;
    }

    public static ImageFormats GetImageFormatsFromExtension(string filePath)
    {
        string ext = FS.GetExtension(filePath).TrimStart(AllChars.dot).ToLower();
        return GetImageFormatsFromExtension2(ext);
    }

    private static bool IsSupportedResizeForExtension(string ext)
    {
        if (ext == "jpg")
        {
            ext = "jpeg";
        }
        for (int i = 0; i < s_supportedExtensionForResize.Count; i++)
        {
            if (s_supportedExtensionForResize[i] == ext)
            {
                return true;
            }
        }
        return false;
    }

    public static ImageFormats GetImageFormatsFromExtension2(string ext)
    {
        if (ext == "")
        {
            return ImageFormats.None;
        }
        if (!IsSupportedResizeForExtension(ext))
        {
            return ImageFormats.None;
        }
        return (ImageFormats)Enum.Parse(typeof(ImageFormats), ext, true);
    }

    public static SunamoSize CalculateOptimalSize(int width, int height, int maxWidth)
    {
        return CalculateOptimalSizeWpf(width, height, maxWidth);
    }

    public static SunamoSize CalculateOptimalSizeWpf(double width, double height, int maxWidth)
    {
        SunamoSize vr = new SunamoSize(width, height);
        int sirkaSloupce = maxWidth;
        if (width > sirkaSloupce)
        {
            vr.Width = sirkaSloupce;

            // mohl by ses ještě rozhodovat jestli round, nebo floor, nebo ceil
            vr.Height = sirkaSloupce * height / width;
        }

        return vr;
    }
}
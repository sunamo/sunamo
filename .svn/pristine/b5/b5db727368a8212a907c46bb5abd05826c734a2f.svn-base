using sunamo.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sunamo
{
    public class Pictures
    {
        static string[] supportedExtensionForResize = new string[] {
        "png", "jpg", "jpeg", "gif"
    };

        public static bool GetImageFormatFromExtension1(string filePath, out string ext)
        {
            ext = FS.GetExtension(filePath).TrimStart('.').ToLower();

            if (Pictures.IsSupportedResizeForExtension(ext))
            {
                return true;
            }
            return false;
        }

        public static ImageFormats GetImageFormatsFromExtension(string filePath)
        {
            string ext = FS.GetExtension(filePath).TrimStart('.').ToLower();
            return GetImageFormatsFromExtension2(ext);
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

        private static bool IsSupportedResizeForExtension(string ext)
        {
            if (ext == "jpg")
            {
                ext = "jpeg";
            }
            for (int i = 0; i < supportedExtensionForResize.Length; i++)
            {
                if (supportedExtensionForResize[i] == ext)
                {
                    return true;
                }
            }
            return false;
        }

        public static SunamoSize CalculateOptimalSize(int width, int height, int maxWidth)
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

        /// <summary>
        /// Vypočte optimální šířku v případě že obrázek je postaven na výšku.
        /// </summary>
        /// <param name="p"></param>
        /// <param name="p_2"></param>
        /// <param name="p_3"></param>
        /// <returns></returns>
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
    }
}

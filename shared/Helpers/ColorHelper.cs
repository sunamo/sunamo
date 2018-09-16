using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Imaging;


namespace shared
{

    public partial class ColorHelper
    {
        public static System.Drawing.Color GetColorFromBytes(byte r, byte g, byte b)
        {
            //System.Drawing.Color c = new System.Drawing.Color();
            return System.Drawing.Color.FromArgb(r, g, b);
        }

        public static string RandomColorHex(bool light)
        {
            int r = RandomHelper.RandomColorPart(light);
            int g = RandomHelper.RandomColorPart(light);
            int b = RandomHelper.RandomColorPart(light);
            return StringHexColorConverter.ConvertToWoAlpha(r, g, b);
        }

        public static object FromRgb(byte current_R, byte current_G, byte current_B)
        {
            return Color.FromRgb(current_R, current_G, current_B);
        }





        public static bool IsColorSimilar(System.Drawing.Color a, System.Drawing.Color b, int threshold = 50)
        {
            int r = (int)a.R - b.R;
            int g = (int)a.G - b.G;
            int b2 = (int)a.B - b.B;
            return (r * r + g * g + b2 * b2) <= threshold * threshold;
        }

        public static bool IsColorSimilar(PixelColor a, PixelColor b, int threshold = 50)
        {
            int r = (int)a.Red - b.Red;
            int g = (int)a.Green - b.Green;
            int b2 = (int)a.Blue - b.Blue;
            return (r * r + g * g + b2 * b2) <= threshold * threshold;
        }

        public static bool IsColorSame(PixelColor first, PixelColor pxsi)
        {
            return first.Red == pxsi.Red && first.Green == pxsi.Green && first.Blue == pxsi.Blue;
        }


    }

}
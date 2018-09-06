using sunamo.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Media;


    /// <summary>
    /// 
    /// </summary>
    public static class ColorH
    {
        public static PixelColor PixelColorFromColor( Color color, byte? alpha)
        {
            if (alpha == null)
            {
                alpha = color.A;
            }
            PixelColor white2 = new PixelColor() { Alpha = alpha.Value, Red = color.R, Green = color.G, Blue = color.B };
            return white2;
        }

        public static Color GetOpaqueColor(byte r, byte g, byte b)
        {
            Color c = new Color();
            c.A = 255;
            c.R = r;
            c.G = g;
            c.B = b;
            return c;
        }

        public static Color RandomColor(bool light)
        {
            return GetOpaqueColor(RandomHelper.RandomColorPart(light), RandomHelper.RandomColorPart(light), RandomHelper.RandomColorPart(light));
        }

        public static SolidColorBrush RandomLightBrush(ColorComponent shade)
        {
            byte r = 0;
            byte g = 0;
            byte b = 0;
            switch (shade)
            {
                case ColorComponent.Red:
                    r = 255;
                    g = b = RandomHelper.RandomByte(200, 250);
                    break;
                case ColorComponent.Green:
                    g = 255;
                    g = r = RandomHelper.RandomByte(200, 250);
                    break;
                case ColorComponent.Blue:
                    b = 255;
                    g = r = RandomHelper.RandomByte(200, 250);
                    break;
                case ColorComponent.None:
                default:
                    r = g = b = 255;
                    break;
            }

            return new SolidColorBrush(GetColorWithAlpha(r, g, b, 150));
        }

        public static SolidColorBrush RandomBrush(bool light, ColorComponent into)
        {
            byte r = RandomHelper.RandomColorPart(light, 0);
            byte g = RandomHelper.RandomColorPart(light, 0);
            byte b = RandomHelper.RandomColorPart(light, 0);
            switch (into)
            {
                case ColorComponent.Red:
                    r += 127;
                    break;
                case ColorComponent.Green:
                    g += 127;
                    break;
                case ColorComponent.Blue:
                    b += 127;
                    break;
                case ColorComponent.None:
                    r += 127;
                    g += 127;
                    b += 127;
                    break;
                default:
                    throw new Exception("Not implemented case in ColorHelperApps.RandomBrush");
            }
            return new SolidColorBrush(GetOpaqueColor(r, g, b));
        }

        public static Color GetColorWithAlpha( byte r, byte g, byte b, byte a)
        {
            Color white2 = new Color { A = a, R = r, G = g, B = b };
            return white2;
        }

        public static Color GetColorWithAlpha(Color color, byte? alpha)
        {
            if (alpha == null)
            {
                alpha = color.A;
            }
            Color white2 = new Color { A = alpha.Value, R = color.R, G = color.G, B = color.B };
            return white2;
        }

        public static bool IsColorSame(Color first, Color pxsi)
        {
            return first.R == pxsi.R && first.G == pxsi.G && first.B == pxsi.B;
        }

        public static bool IsColorSame(PixelColor first, PixelColor pxsi)
        {
            return first.Red == pxsi.Red && first.Green == pxsi.Green && first.Blue == pxsi.Blue;
        }
#if DEBUG
        public static void DebugWrite(Color c)
        {

            //DebugLoggerApps.Instance.Write("A: " + c.A + " R: " + c.R + " G: " + c.G + " : " + c.B);
        }
#endif
    }


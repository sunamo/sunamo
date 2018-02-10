using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace sunamo.Values
{
    public class PixelColors
    {
        private static PixelColor GetPixelColor(Color color)
        {
            return new PixelColor { Alpha = color.A, Blue = color.B, Green = color.G, Red = color.R };
        }

        public static readonly PixelColor LightCoral;

        static PixelColors()
        {

        }
    }
}

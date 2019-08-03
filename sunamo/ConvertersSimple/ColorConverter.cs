using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace sunamo.ConvertersSimple
{
    /// <summary>
    /// Může se využít například když chceš ukládat barvu na disk
    /// </summary>
    public static class ColorConverter //: ISimpleConverter<Color, string>
    {
        public static Color ConvertTo(string u)
        {
            var d2 = SF.GetAllElementsLine(u);
            var d = CA.ToInt(d2);
            return Color.FromArgb((byte)d[0], (byte)d[1], (byte)d[2], (byte)d[3]);
        }

        public static string ConvertFrom(Color t)
        {
            return SF.PrepareToSerialization(CA.ToListString(t.A, t.R, t.G, t.B));
        }
    }
}

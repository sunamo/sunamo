using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;


namespace apps
{
    /// <summary>
    /// Může se využít například když chceš ukládat barvu na disk
    /// </summary>
    public static class ColorConverter //: ISimpleConverter<Color, string>
    {

        public static Color ConvertTo(string u)
        {
            string[] d2 = SF.GetAllElementsLine(u);
            int[] d = sunamo.BT.CastArrayStringToInt(d2);
            return Color.FromArgb((byte)d[0], (byte)d[1], (byte)d[2], (byte)d[3]);
        }

        public static string ConvertFrom(Color t)
        {
            return SF.PrepareToSerialization(t.A, t.R, t.G, t.B);
        }
    }
}

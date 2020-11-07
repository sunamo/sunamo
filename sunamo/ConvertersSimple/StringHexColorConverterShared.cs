using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static partial class StringHexColorConverter{ 
public static Color? ConvertFrom2(string hex)
    {
        var v = Utils.FromHex(hex);
        if (v.Count() == 3)
        {
            return Color.FromArgb(v[0], v[1], v[2]);
        }
        else if (v.Count() == 4)
        {
            return Color.FromArgb(v[0], v[1], v[2], v[3]);
        }
        return null;
    }


    public static string ConvertToWoAlpha(byte r, byte g, byte b)
    {
        //return SH.Format2("#{0:X2}{1:X2}{2:X2}", r, g,b);
        return "#" + ByteArrayToString(new byte[]{r, g, b});
    }
public static string ConvertToWoAlpha(SunamoColor u)
    {
        return SH.Format2("#{0:X2}{1:X2}{2:X2}", u.R, u.G, u.B);
    }
public static string ConvertToWoAlpha(int r, int g, int b)
    {
        return ConvertToWoAlpha((byte)r, (byte)g, (byte)b);
    }

public static string ByteArrayToString(byte[] ba)
    {
        string hex = BitConverter.ToString(ba);
        return hex.Replace(AllStrings.dash, "");
    }

public static string ConvertTo(SunamoColor u)
    {
        return SH.Format2("#{0:X2}{1:X2}{2:X2}{3:X2}", u.A, u.R, u.G, u.B);
    }
}
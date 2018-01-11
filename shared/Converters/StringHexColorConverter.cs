using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public static class StringHexColorConverter //: ISimpleConverter<string, Color>
{
    static ColorConverter cc = new ColorConverter();

    public static string ConvertToWoAlpha(byte r, byte g, byte b)
    {
        //return string.Format("#{0:X2}{1:X2}{2:X2}", r, g,b);
        return "#" + ByteArrayToString(new byte[] { r, g, b });
    }

    public static string ByteArrayToString(byte[] ba)
    {
        string hex = BitConverter.ToString(ba);
        return hex.Replace("-", "");
    }

    public static string ConvertToWoAlpha(Color u)
    {
        return string.Format("#{0:X2}{1:X2}{2:X2}", u.R, u.G, u.B);
    }

    public static string ConvertTo(Color u)
    {
        return string.Format("#{0:X2}{1:X2}{2:X2}{3:X2}", u.A, u.R, u.G, u.B);
    }

    /// <summary>
    /// Může se zadávat jak s # tak bez - používá se metoda TrimStart
    /// Tato metoda je nějaká divná asi, kdyby nefungovala, použij místo ní třídu BrushConverter a metodu ConvertFrom
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    public static Color ConvertFrom(string t)
    {
        //Color vr = new Color();
        t = t.TrimStart('#');
        if (t.Length == 8)
        {
            return Color.FromArgb(GetGroup(0, t), GetGroup(1, t), GetGroup(2, t), GetGroup(3, t));
        }
        else if (t.Length == 6)
        {
            return Color.FromArgb(GetGroup(0, t), GetGroup(1, t), GetGroup(2, t));
        }
        return Color.Black;
    }

    /// <summary>
    /// Fungující metoda, která narozdíl od metody ConvertFrom používá BCL třídu ColorConverter
    /// Nevýhoda je ta že se výsleedek musí přetypovat na typ Color a to trvá taky nějaký čas.
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    public static Color ConvertFrom2(string t)
    {
        return (Color)cc.ConvertFromString(t);
    }

    private static byte GetGroup(int p, string t)
    {
        string s = "";
        if (p == 0)
        {
            s = t[0].ToString() + t[1].ToString();
        }
        else if (p == 1)
        {
            s = t[2].ToString() + t[3].ToString();
        }
        else if (p == 2)
        {
            s = t[4].ToString() + t[5].ToString();
        }
        else
        {
            s = t[6].ToString() + t[7].ToString();
        }
        return Convert.ToByte(s, 16);
    }

    public static string ConvertToWoAlpha(int r, int g, int b)
    {
        return ConvertToWoAlpha((byte)r, (byte)g, (byte)b);
    }
}


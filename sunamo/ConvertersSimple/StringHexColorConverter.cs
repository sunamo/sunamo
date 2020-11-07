
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public static partial class StringHexColorConverter //: ISimpleConverter<string, Color>
{

    /// <summary>
    /// Can be entered with or without # - is used TrimStart()
    /// </summary>
    /// <param name = "t"></param>
    public static Color? ConvertFrom(string t)
    {
        //TODO: Write unit test for it - Tato metoda je nějaká divná asi, kdyby nefungovala, použij místo ní třídu BrushConverter a metodu ConvertFrom

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
        // earlier time Color.Black
        return null;
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
}
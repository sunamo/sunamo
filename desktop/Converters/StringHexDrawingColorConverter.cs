using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace desktop.Converters
{
    public class StringHexDrawingColorConverter
    {
        public static string ConvertTo(Color u)
        {
            return string.Format("#{0:X2}{1:X2}{2:X2}{3:X2}", u.A, u.R, u.G, u.B);
        }

        public static Color ConvertFrom(string t)
        {
            Color vr = Color.Black;
            t = t.TrimStart('#');
            if (t.Length == 8)
            {
                vr = System.Drawing.Color.FromArgb(GetGroup(0, t), GetGroup(1, t), GetGroup(2, t), GetGroup(3, t));
                
            }
            else if (t.Length == 6)
            {
                vr = System.Drawing.Color.FromArgb(255, GetGroup(1, t), GetGroup(2, t), GetGroup(3, t));
            }
            else
            {
                return Color.Black;
            }
            return vr;
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
}

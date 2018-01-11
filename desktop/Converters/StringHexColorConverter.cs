using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;


namespace desktop
{
    public static class StringHexWindowsMediaColorConverter //: ISimpleConverter<string, Color>
    {

        public static string ConvertTo(Color u)
        {
            return string.Format("#{0:X2}{1:X2}{2:X2}{3:X2}", u.A, u.R, u.G, u.B);
        }

        public static Color ConvertFrom(string t)
        {
            Color vr = new Color();
            t = t.TrimStart('#');
            if (t.Length == 8)
            {
                vr.A = GetGroup(0,t);
                vr.R = GetGroup(1, t);
                vr.G = GetGroup(2, t);
                vr.B = GetGroup(3, t);
            }
            else if (t.Length == 6)
            {
                vr.R = GetGroup(0, t);
                vr.G = GetGroup(1, t);
                vr.B = GetGroup(2, t);
            }
            else
            {
                return Colors.Black;
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

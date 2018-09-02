using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;

using System.Reflection;
using System.Diagnostics;

namespace apps
{
    public class KnownColorsHexColorConverter //: ISimpleConverter<ColorConverter, string>
    {
        static Dictionary<string, Color> hexKnownColors = new Dictionary<string, Color>();
        static Dictionary<string, Color> stringKnownColors = new Dictionary<string, Color>();

        static KnownColorsHexColorConverter()
        {
            IEnumerable<PropertyInfo> f = typeof(Colors).GetRuntimeProperties();
            foreach (var item in f)
            {
                Color c = (Color)item.GetValue(null);
                string d = item.Name;
                if (!stringKnownColors.ContainsKey(d))
                {
                stringKnownColors.Add(d, c);    
                }
                char[] b = c.ToString().ToCharArray();
                b[1] = 'F';
                b[2] = 'F';
                String a = new string(b);
                if (!hexKnownColors.ContainsKey(a))
                {
                    hexKnownColors.Add(a, c);
                }
                
            }
        }

        public  static Color ConvertTo(string u)
        {
            Color e = Colors.AliceBlue;
            if (u.StartsWith("#"))
            {
                if (u.Length == 7)
                {
                    //
                    u = "#FF" + u.Substring(1);
                }
                else if(u.Length == 9)
                {
                    u = "#FF" + u.Substring(3);
                }
                else if (u.Length == 4)
                {
                    //
                    u = "#FF" + u[1] + u[1] + u[2] + u[2] + u[3] + u[3];
                }
                if (u == "#FFFFFFFF")
                {
                    return Colors.LightBlue;
                }
                if (hexKnownColors.ContainsKey(u))
                {
                    return hexKnownColors[u];
                }
                else
                { 
                    e = StringHexColorConverter.ConvertFrom(u);
                    return Color.FromArgb(e.A, e.R, e.G, e.B);
                }
                return stringKnownColors["Blue"];    
            }
            if (stringKnownColors.ContainsKey(u))
            {
                return stringKnownColors[u];
            }
#if DEBUG
            Debug.WriteLine("Blue");
#endif
            return stringKnownColors["LightBlue"];
            //return (Color)typeof(Colors).GetRuntimeProperty(u).GetValue(null);
        }

        public static string ConvertFrom(Color t)
        {
            return StringHexColorConverter.ConvertTo(t);
        }
    }
}

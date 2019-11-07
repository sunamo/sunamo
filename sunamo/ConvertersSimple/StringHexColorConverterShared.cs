using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static partial class StringHexColorConverter{ 
public static Color ConvertFrom2(string hex)
    {
        var v = Utils.FromHex(hex);
        if (v.Count() == 3)
        {
            return Color.FromArgb(v[0], v[1], v[2]);
        }

        return Color.FromArgb(v[0], v[1], v[2], v[3]);
    }
}
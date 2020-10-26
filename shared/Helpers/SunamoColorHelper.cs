using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class SunamoColorHelper
{
    public static SunamoColor Parse(string s)
    {
        s = s.TrimStart(AllChars.num);

        var cn = EnumHelper.Parse<KnownColor>(s, KnownColor.Control);

        if (cn != KnownColor.Control)
        {
            return Color.FromKnownColor(cn).ToSunamoColor();
        }
        else if (HexHelper.IsInHexFormat(s))
        {
            var c = StringHexColorConverter.ConvertFrom(s);

            if (c.HasValue)
            {
                //SunamoColor;
                System.Drawing.Color c2 = c.Value;
                return c2.ToSunamoColor();
            }
            else
            {
                c = StringHexColorConverter.ConvertFrom2(s);
                if (c.HasValue)
                {
                    return c.Value.ToSunamoColor();
                }
                else
                {
                    return null;
                }
            }
            
        }
        return null;
    }
}
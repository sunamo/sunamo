using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public static class SystemDrawingSizeFExtension
{
    public static SunamoSize ToSunamo(this SizeF s)
    {
        return new SunamoSize(s.Width, s.Height);
    }
}
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public static class SystemDrawingColorExtensions
{
    public static SunamoColor ToSunamoColor(this Color c)
    {
        SunamoColor r = new SunamoColor(c.A, c.R, c.G, c.B);
        return r;
    }
}
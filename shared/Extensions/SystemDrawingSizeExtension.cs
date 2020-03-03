using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public static class SystemDrawingSizeExtension
{
    public static SunamoSize ToSunamo(this Size s)
    {
        return new SunamoSize(s.Width, s.Height);
    }
}
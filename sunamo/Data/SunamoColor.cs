using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class SunamoColor
{
    

    public byte A { get; set; }
    public byte R { get; set; }
    public byte G { get; set; }
    public byte B { get; set; }

    public SunamoColor()
    {

    }

    public SunamoColor(byte a, byte r, byte g, byte b)
    {
        A = a;
        R = r;
        G = g;
        B = b;
    }

    public override string ToString()
    {
        // System.Windows.Media.Color = #00000000
        // System.Drawing.Color = Color [A=0, R=0, G=0, B=0]

        return StringHexColorConverter.ConvertTo(this);
    }
}
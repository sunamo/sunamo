using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class SunamoSize
{
    public double Width { get; set; }
    public double Height { get; set; }

    public SunamoSize()
    {
    }

    public SunamoSize(double width, double height)
    {
        Width = width;
        Height = height;
    }

    public bool IsNegativeOrZero()
    {
        bool w = Width <= 0;
        bool h = Height <= 0;
        return w || h;
    }
}


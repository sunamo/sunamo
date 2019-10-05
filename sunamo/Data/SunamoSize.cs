using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class SunamoSize : IParser
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

    public void Parse(string input)
    {
        var d = ParserTwoValues.ParseDouble(AllStrings.comma, input);
        Width = d[0];
        Height = d[1];
    }

    public override string ToString()
    {
        return ParserTwoValues.ToString(AllStrings.comma, Width.ToString(), Height.ToString());
    }
}


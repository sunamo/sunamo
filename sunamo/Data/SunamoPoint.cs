using System;
using System.Collections.Generic;
using System.Text;


public class SunamoPoint
{
    public double X { get; set; }
    public double Y { get; set; }

    public SunamoPoint()
    {
    }

    public SunamoPoint(double x, double y)
    {
        X = x;
        Y = y;
    }

    public void Parse(string input)
    {
        var d = ParserTwoValues.ParseDouble(AllStrings.comma, input);
        X = d[0];
        Y = d[1];
    }

    public override string ToString()
    {
        return ParserTwoValues.ToString(AllStrings.comma, X.ToString(), Y.ToString());
    }
}
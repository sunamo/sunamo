using sunamo.Generators;
using System;
using System.Collections.Generic;
using System.Text;

namespace sunamo.Data
{
    public class DoubleXY
    {
        public DoubleXY(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }

        public double X = 0;
        public double Y = 0;

        public override string ToString()
        {
            return TextTemplates.AxisXY(X, Y);
        }
    }
}

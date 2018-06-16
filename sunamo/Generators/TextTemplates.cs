using System;
using System.Collections.Generic;
using System.Text;

namespace sunamo.Generators
{
    public class TextTemplates
    {
        public static string WidthHeight(double width, double height)
        {
            return $"Width: {width} Height: {height}";
        }

        internal static string AxisXY(double x, double y)
        {
            return $"X: {x} Y: {y}";
        }
    }
}

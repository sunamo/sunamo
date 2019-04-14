using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace csGeoTools.Utilites
{
    class MathUtilities
    {
        public static Double ToDegrees(double radians)
        {
            return radians * (180.0 / Math.PI);
        }

        public static Double ToRadians(double degrees)
        {
            return Math.PI * degrees / 180.0;
        }
    }
}

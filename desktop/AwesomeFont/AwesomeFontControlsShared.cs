using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace desktop.AwesomeFont
{
    public static partial  class AwesomeFontControls
    {
        public static double ReturnFontSizeForTextNextToAwesomeIconWithSize(double h)
        {
            var r = h - 20 - 5;
            return r;
        }
    }
}
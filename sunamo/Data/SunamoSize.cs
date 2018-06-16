using sunamo.Generators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sunamo.Data
{
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

        public override string ToString()
        {
            return TextTemplates.WidthHeight(Width, Height);
        }
    }
}

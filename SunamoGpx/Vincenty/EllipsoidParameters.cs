using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace csGeoTools.Vincenty
{
    public class EllipsoidParameters
    {
        public Double equatorialAxis { get; private set; }
        public Double polarAxis { get; private set; }
        public Double inverseFlattening { get; private set; }
        
        public EllipsoidParameters(Double a, Double b, Double f)
        {
            this.equatorialAxis = a;
            this.polarAxis = b;
            this.inverseFlattening = f;
        }

        public Double GetFlattening()
        {
            return 1 / inverseFlattening;
        }
    }
}

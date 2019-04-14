using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace csGeoTools
{
    public class FixedBearing : Bearing
    {
        protected Double degrees;
        
        public FixedBearing(Double decimalDegrees)
        {
            this.degrees = decimalDegrees % 360;
            if (this.degrees < 0)
            {
                this.degrees += 360;
            }
        }

	    override protected Double GetBearingInDecimalDegrees()
        {
		    return degrees;
	    }
    }
}

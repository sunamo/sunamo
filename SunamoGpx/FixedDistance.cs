using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace csGeoTools
{
    public class FixedDistance : Distance
    {
        protected Double meters;
        
        public FixedDistance(Double meters)
        {
            this.meters = meters;
        }

        override protected Double GetDistanceInMeters()
        {
            return meters;
        }
    }
}

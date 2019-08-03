using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace sunamo.Helpers.Number
{
    public class PercentCalculator
    {
        public double onePercent = 0;
        public double last = 0;
        private double _overallSum;
        private double _hundredPercent = 100d;

        public PercentCalculator(double overallSum)
        {
            onePercent = _hundredPercent / overallSum;
            _overallSum = overallSum;
        }

        private int _sum = 0;

        /// <summary>
        /// Is automatically called with PercentFor with last 
        /// </summary>
        public void ResetComputedSum()
        {
            _sum = 0;
        }

        public int PercentFor(double value, bool last)
        {
            // cannot divide by zero
            if (_overallSum == 0)
            {
                return 0;
            }

            double quocient = value / _overallSum;

            int result = (int)(_hundredPercent * quocient);
            _sum += result;
            if (last)
            {
                int diff = _sum - 100;
                if (_sum != 0)
                {
                    result -= diff;
                }
                ResetComputedSum();
            }

            if (result == -2147483648)
            {
                Debugger.Break();
            }
            return result;
        }
    }
}

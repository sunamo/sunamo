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
        double overallSum;
        double hundredPercent = 100d;

        public PercentCalculator(double overallSum)
        {
            onePercent = hundredPercent / overallSum;
            this.overallSum = overallSum;
        }

        int sum = 0;

        /// <summary>
        /// Is automatically called with PercentFor with last 
        /// </summary>
        public void ResetComputedSum()
        {
            sum = 0;
        }
        
        public int PercentFor(double value, bool last)
        {
            // cannot divide by zero
            if (overallSum == 0)
            {
                return 0;
            }

            double quocient = value / overallSum;
            
            int result = (int)(hundredPercent * quocient);
            sum += result;
            if (last)
            {
                int diff = sum - 100;
                if (sum != 0)
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

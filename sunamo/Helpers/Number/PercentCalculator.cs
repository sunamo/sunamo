﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace sunamo.Helpers.Number
{
    /// <summary>
    /// Normálně se volá 100x DonePartially()
    /// </summary>
    public class PercentCalculator
    {
        public double onePercent = 0;
        public double last = 0;
        public double _overallSum;
        private double _hundredPercent = 100d;

        public void AddOnePercent()
        {
            last += onePercent;
        }

        public void AddOne()
        {
            last += 1;
        }

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

            Func<string,short> d = short.Parse;
        }

        /// <summary>
        /// Was used for generating text output with inBothCount, files1Count, files2Count 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="last"></param>
        /// <returns></returns>
        public int PercentFor(double value, bool last)
        {
            // cannot divide by zero
            if (_overallSum == 0)
            {
                return 0;
            }

            // value - 
            // 
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
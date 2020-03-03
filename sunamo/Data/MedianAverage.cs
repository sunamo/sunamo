using System;
using System.Collections.Generic;
using System.Text;

namespace sunamo.Data
{
    public class MedianAverage<T>
    {
        public T median;
        public T average;
        public T min;
        public T max;
        public int count;

        public override string ToString()
        {
            return $"Count: {count}, Median: {median}, Average: {average}, Min: {min}, Max: {max}";
        }
    }
}
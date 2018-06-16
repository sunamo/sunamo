using System;
using System.Collections.Generic;
using System.Text;

// Extension class can't be in namespace
public static class ListExtensions
    {
        public static List<T> ToList<T>(this List<T> list)
        {
            return new List<T>(list);
        }
    }


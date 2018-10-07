﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sunamo
{
    public class CharHelper
    {
        public static string OnlyDigits(string v)
        {
            return OnlyAccepted(v, char.IsDigit);
        }

        private static string OnlyAccepted(string v, Func<char, bool> isDigit)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in v)
            {
                if (isDigit.Invoke(item))
                {
                    sb.Append(item);
                }
            }
            return sb.ToString();
        }

        internal static bool IsGeneric(char c)
        {
            return CA.IsEqualToAnyElement<char>(c, AllChars.generalChars);
        }

        internal static bool IsSpecial(char c)
        {
            return CA.IsEqualToAnyElement<char>(c, AllChars.specialChars);
        }
    }
}

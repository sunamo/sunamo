using System;
using System.Collections.Generic;
using System.Text;

namespace sunamo.Extensions
{
    public static class StringBuilderExtensions
    {
        /// <summary>
        /// Not implemented 
        /// must do it via string, because stringbuilder cant return only part of result
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="e"></param>
        public static void TrimEnd(this StringBuilder sb, string e)
        {
            //while (sb.)
            //{

            //}
            
        }

        public static void Trim(this StringBuilder sb)
        {
            var length = sb.Length;
            for (int i = length - 1; i >= 0; i--)
            {
                if (char.IsWhiteSpace(sb[i]))
                {
                    sb.Remove(i, 1);
                }
                else
                {
                    break;
                }
            }

            length = sb.Length;
            for (int i = 0; i < length; i++)
            {
                if (char.IsWhiteSpace(sb[i]))
                {
                    sb.Remove(i, 1);
                }
                else
                {
                    break;
                }
            }
        }
    }
}
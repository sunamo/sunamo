using System;
using System.Collections.Generic;
using System.Text;


    public static class StringBuilderExtensions
    {
        public static bool Contains(this StringBuilder haystack, string needle)
        {
            return haystack.IndexOf(needle) != -1;
        }
        public static int IndexOf(this StringBuilder haystack, string needle)
        {
            if (haystack == null || needle == null)
                throw new ArgumentNullException();
            if (needle.Length == 0)
                return 0;//empty strings are everywhere!
            if (needle.Length == 1)//can't beat just spinning through for it
            {
                char c = needle[0];
                for (int idx = 0; idx != haystack.Length; ++idx)
                    if (haystack[idx] == c)
                        return idx;
                return -1;
            }
            int m = 0;
            int i = 0;
            int[] T = KMPTable(needle);
            while (m + i < haystack.Length)
            {
                if (needle[i] == haystack[m + i])
                {
                    if (i == needle.Length - 1)
                        return m == needle.Length ? -1 : m;//match -1 = failure to find conventional in .NET
                    ++i;
                }
                else
                {
                    m = m + i - T[i];
                    i = T[i] > -1 ? T[i] : 0;
                }
            }
            return -1;
        }

        private static int[] KMPTable(string sought)
        {
            int[] table = new int[sought.Length];
            int pos = 2;
            int cnd = 0;
            table[0] = -1;
            table[1] = 0;
            while (pos < table.Length)
                if (sought[pos - 1] == sought[cnd])
                    table[pos++] = ++cnd;
                else if (cnd > 0)
                    cnd = table[cnd];
                else
                    table[pos++] = 0;
            return table;
        }
    

    
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

        #region For easy copy
        public static void TrimEnd(this StringBuilder sb)
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

        } 
        #endregion

        public static void TrimStart(this StringBuilder sb)
        {
            var length = sb.Length;
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

        public static void Trim(this StringBuilder sb)
        {
            TrimEnd(sb);
            TrimStart(sb);
        }
    }

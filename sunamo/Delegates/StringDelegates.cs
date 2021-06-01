using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace sunamo.Delegates
{
    public class StringDelegates
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Contains(string input, string value)
        {
            return input.Contains(value);
        }

        public static bool IsNumber(string input, string value, bool invert)
        {
            input = SH.ReplaceAll(input, "", AllLists.numberPoints.ToArray());
            long l = 0;
            return BTS.Invert(long.TryParse(input, out l), invert);
        }
    }
}
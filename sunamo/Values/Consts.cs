using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sunamo.Values
{
    public partial class Consts
    {
        public const string @sunamo = "sunamo";

        public const string UncLongPath = @"\\?\";

        public const string HttpLocalhostSlash = "http://localhost/";
        public const string HttpSunamoCzSlash = "http://www.sunamo.cz/";
        public readonly static string localhost = "localhost";

        public static string HttpWwwCzSlash = "http://www.sunamo.cz/";
        public static string HttpCzSlash = "http://sunamo.cz/";
        public static string HttpWwwCz = "http://www.sunamo.cz";

        public static string Cz = "http://sunamo.cz";
        public static string WwwCz = "http://www.sunamo.cz";

        public static string CzSlash = "http://sunamo.cz/";
        public static string DotCzSlash = ".sunamo.cz/";
        public static string DotCz = ".sunamo.cz";

        public static readonly Type tString = typeof(string);
        public static readonly Type tInt = typeof(int);
        public static readonly Type tDateTime = typeof(DateTime);
        public static readonly Type tDouble = typeof(double);
        public static readonly Type tFloat = typeof(float);
        public static readonly Type tChar = typeof(char);
        public static readonly Type tBool = typeof(bool);
        public static readonly Type tByte = typeof(byte);
        public static readonly Type tShort = typeof(short);
        public static readonly Type tBinary = typeof(byte[]);
        public static readonly Type tLong = typeof(long);
        public static readonly Type tDecimal = typeof(decimal);
        public static readonly Type tSbyte = typeof(sbyte);
        public static readonly Type tUshort = typeof(ushort);
        public static readonly Type tUint = typeof(uint);
        public static readonly Type uUlong = typeof(ulong);

        public static readonly string[] numberPoints = new string[] { ",", "." };

        #region Names here must be the same as in AllChars
        public const string bs = "\\";
        public const string tab = "\t";
        public const string nl = "\n";
        public const string cr = "\t"; 
        #endregion

    }
}

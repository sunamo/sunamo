using System;
using System.Collections.Generic;
using System.Text;


    public static partial class Consts
    {
    public static readonly Type tObject = typeof(object);
    public static readonly Type tString = typeof(string);
    public static readonly Type tStringBuilder = typeof(StringBuilder);
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
    public static readonly Type tUlong = typeof(ulong);
    

    public static readonly List<Type> allBasicTypes = CA.ToList<Type>(tObject, tString, tStringBuilder, tInt, tDateTime,
        tDouble, tFloat, tChar, tBinary, tByte, tShort, tBinary, tLong, tDecimal, tSbyte, tUshort, tUint, tUlong);

    public const long kB = 1024;
        /// <summary>
        /// Dot space
        /// </summary>
        public const string ds = ": ";
        /// <summary>
        /// "x "
        /// </summary>
        public const string xs = "x ";
        public const string Exception = "Exception" + ": ";

    public static readonly List<string> BasicImageExtensions = null;
        public const string spaces4 = "    ";
        public static List<string> cssTemplatesSites = null;

        static Consts()
        {
            cssTemplatesSites = new List<string>(CA.ToListString("justfreetemplates.com", "templatemo.com", "free-css.com", "templated.co", "w3layouts.com"));
        BasicImageExtensions = CA.ToList<string>(AllExtensions.png,
            AllExtensions.bmp,
            AllExtensions.jpg);
    }

        public const string nulled = "(null)";

        public const string HttpLocalhostSlash = "https://localhost" + "/";
        public const string HttpSunamoCzSlash = "https://www.sunamo.cz" + "/";
        public readonly static string localhost = "localhost";

        public static string HttpWwwCzSlash = "https://www.sunamo.cz" + "/";
        public static string HttpCzSlash = "https://sunamo.cz" + "/";
        public static string HttpWwwCz = "https://www.sunamo.cz";
    public const string httpLocalhost = "https://localhost/";

    public const string scz = "sunamo.cz";

        public static string Cz = "https://sunamo.cz";
        public static string WwwCz = "https://www.sunamo.cz";

        public static string CzSlash = "https://sunamo.cz" + "/";
        public static string DotCzSlash = "." + "sunamo.cz" + "/";
        public static string DotCz = ".sunamo.cz";

        public static string http = "https://";
        public const double zeroDouble = 0;
        public const int zeroInt = 0;
        public const float zeroFloat = 0;


        public static readonly string[] numberPoints = new string[] { AllStrings.comma, AllStrings.dot };

        #region Names here must be the same as in AllChars
        public const string bs = AllStrings.bs;
        public const string tab = "\t";
        public const string nl = "\n";
        public const string cr = "\t";
        #endregion

        /// <summary>
        /// \\?\
        /// </summary>
        public const string UncLongPath = @"\\?\";
        public const string @sunamo = "sunamo";

        
    }

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sunamo.Values
{
    public partial class Consts
    {
        public const long kB = 1024;
        /// <summary>
        /// Dot space
        /// </summary>
        public const string ds = ": ";
        /// <summary>
        /// "x "
        /// </summary>
        public const string xs = "x ";
        

        public static readonly List<string> BasicImageExtensions = CA.ToList<string>(AllExtensions.png,
            AllExtensions.bmp,
            AllExtensions.jpg);

        public static List<string> cssTemplatesSites = null;

        static Consts()
        {
            cssTemplatesSites = new List<string>(CA.ToEnumerable("justfreetemplates.com", "templatemo.com", "free-css.com", "templated.co", "w3layouts.com"));
        }

        public const string nulled = "(null)";

        

        

        public const string HttpLocalhostSlash = "http://localhost/";
        public const string HttpSunamoCzSlash = "http://www.sunamo.cz/";
        public readonly static string localhost = "localhost";

        public static string HttpWwwCzSlash = "http://www.sunamo.cz/";
        public static string HttpCzSlash = "http://sunamo.cz/";
        public static string HttpWwwCz = "http://www.sunamo.cz";

        public const string scz = "sunamo.cz";

        public static string Cz = "http://sunamo.cz";
        public static string WwwCz = "http://www.sunamo.cz";

        public static string CzSlash = "http://sunamo.cz/";
        public static string DotCzSlash = ".sunamo.cz/";
        public static string DotCz = ".sunamo.cz";

        public static string http = "http://";
        public const double zeroDouble = 0;
        public const int zeroInt = 0;
        public const float zeroFloat = 0;
        

        public static readonly string[] numberPoints = new string[] { ",", "." };

        #region Names here must be the same as in AllChars
        public const string bs = "\\";
        public const string tab = "\t";
        public const string nl = "\n";
        public const string cr = "\t"; 
        #endregion

    }
}

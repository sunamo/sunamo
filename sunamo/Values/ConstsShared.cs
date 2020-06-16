using System;
using System.Collections.Generic;
using System.Text;


/// <summary>
/// Name is Consts because all there must be consts, not static readonly etc.
/// </summary>
    public static partial class Consts
    {
    public const string slashLocalhost = AllStrings.slash + Consts.localhost;
    public const string slashScz = AllStrings.slash + Consts.Cz;
    public const string dotScz = ".sunamo.cz";
    public const string dotSczSlash = ".sunamo.cz/";
    /// <summary>
    /// Min age is 18 due to GDPR - below 18 is needed parent agreement of child
    /// </summary>
    public const int MinAge = 18;
    /// <summary>
    /// Must be also in Consts, not only in SqlServerHelper due to use in sunamo project
    /// </summary>
    public static readonly DateTime DateTimeMinVal = new DateTime(1900, 1, 1);
    public static readonly DateTime DateTimeMaxVal = new DateTime(2079, 6, 6);
    public const string Nope = "Nope";
    public const string transformTo = "->";

    static Consts()
    {
        
    }

    public const string fnReplacement = "{filename}";

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

   
    public const string spaces4 = "    ";
        


        public const string nulled = "(null)";

        public const string HttpLocalhostSlash = "https://localhost" + "/";
        public const string HttpSunamoCzSlash = "https://sunamo.cz" + "/";
    /// <summary>
    /// localhost
    /// </summary>
        public const string localhost = "localhost";

        public const string HttpWwwCzSlash = "https://sunamo.cz" + "/";
        public const string HttpCzSlash = "https://sunamo.cz" + "/";
        public const string HttpWwwCz = "https://sunamo.cz";
    public const string httpLocalhost = "https://localhost/";

    /// <summary>
    /// sunamo.cz
    /// Without slash
    /// </summary>
    public const string Cz = "sunamo.cz";
        public const string WwwCz = "sunamo.cz";

        public const string CzSlash = "sunamo.cz" + "/";
        public const string DotCzSlash = "." + "sunamo.cz" + "/";
        public const string DotCz = ".sunamo.cz";

    /// <summary>
    /// http://
    /// </summary>
    public const string http = "http://";
    /// <summary>
    /// https://
    /// </summary>
    public const string https = "https://";
    
    public const double zeroDouble = 0;
        public const int zeroInt = 0;
        public const float zeroFloat = 0;


        

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
    public const string sunamocz = "sunamocz";
    public const string stringEmpty = "";
}
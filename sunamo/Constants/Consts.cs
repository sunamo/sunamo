using System;
using System.Collections.Generic;
using System.Text;

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

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;

namespace apps
{
    public class Consts
    {
        public static readonly Size dialogSize = new Size(1024, 256);

        /// <summary>
        /// Inicializuje W i H velikostí double.MaxValue
        /// </summary>
        public static readonly Size maxSize = new Size(double.PositiveInfinity, double.PositiveInfinity);
        public static Size actualSize = new Size(1024, 768);
        public const string HttpLocalhostSlash = "http://localhost/";
        public const string HttpSunamoCzSlash = "http://www.sunamo.cz/";
    }
}

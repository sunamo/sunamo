using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace csGeoTools.Parsers.Gpx
{
    static class DateFormats
    {
        public static string[] KNOWN_FORMATS = new string[]
        {
            "yyyy-MM-dd'T'HH:mm:ss.f'Z'",
            "yyyy-MM-dd'T'HH:mm:ss.fffffff'Z'",
            "yyyy-MM-dd'T'HH:mm:ss'Z'",
            "yyyy-MM-dd HH:mm:ss.f z",
            "yyyy-MM-dd HH:mm:ss z",
            "yyyy-MM-dd z",
            "yyyy-MM-dd"
        };
    }
}

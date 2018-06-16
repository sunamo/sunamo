using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Extension class can't be in namespace
public static class UriExtensions
    {
        public static string SchemeDelimiter(this Uri uri)
        {
            return "://";
        }
    }


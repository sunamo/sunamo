using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sunamo.Essential
{
    public static class CleanUp
    {
        public static void Streams(Stream stream, FileStream fileStream)
        {
            if (stream != null)
            {
                stream.Dispose();
            }
            if (fileStream != null)
            {
                fileStream.Dispose();
            }
        }
    }
}

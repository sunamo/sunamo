using System;
using System.Collections.Generic;
using System.Text;

namespace sunamo.Parsing
{
    public class ParseDefault
    {
        public class Byte
        {
            public byte ParseByte(string p, byte def)
            {
                byte p2;
                if (byte.TryParse(p, out p2))
                {
                    return p2;
                }
                return def;
            }
        }

        public class Integer
        {
            /// <summary>
            /// Vrátí A2 pokud se nepodaří vyparsovat
            /// </summary>
            /// <param name="p"></param>
            /// <param name="def"></param>
            public int ParseInt(string p, int def)
            {
                int p2;
                if (int.TryParse(p, out p2))
                {
                    return p2;
                }
                return def;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;


public class Parse
{
    public class Byte
    {
        public byte ParseByte(string p)
        {
            byte b;
            if (byte.TryParse(p, out b))
            {
                return b;
            }
            return 0;
        }
    }

    public class Double
    {
        /// <summary>
        /// Vrátí -1 v případě že se nepodaří vyparsovat
        /// </summary>
        /// <param name="p"></param>
        
        public short ParseShort(string d)
        {
            short s = 0;
            if (short.TryParse(d, out s))
            {
                return s;
            }
            return -1;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace sunamo.Data
{
    public class SerializeContentArgs
    {
        /// <summary>
        /// Must be property - I can forget change value on three occurences. 
        /// </summary>
        public  char separatorChar
        {
            get
            {
                return separatorString[0];
            }
        }
        public  string separatorString = "|";

        public  int keyCodeSeparator
        {
            get
            {
                return (int)separatorChar;
            }
        }

    }
}

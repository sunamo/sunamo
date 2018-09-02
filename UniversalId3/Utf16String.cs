using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversalId3
{
    /* Convention
     * Store the string without BOM in the System.string
     */
    public class Utf16String
    {
        public static byte[] GetBytes(string str, bool emitBigEndian, bool emitBom)
        {
            str = RemoveBom(str);

            UnicodeEncoding unicodeEncoding = new UnicodeEncoding(emitBigEndian, true);
            byte[] stringBytes = unicodeEncoding.GetBytes(str);

            if (emitBom)
            {
                byte[] bom = unicodeEncoding.GetPreamble();
                return Functions.Concatenate(bom, stringBytes);
            }
            else
            {
                return stringBytes;
            }
        }

        /// <summary>
        /// Returns a string from the buffer untill null char is found
        /// </summary>
        /// <param name="buffer">The buffer in which string is stored</param>
        /// <param name="startIndex">start index</param>
        /// <param name="count">number of bytes to read from buffer</param>
        /// <param name="bigEndian">true if string is stored in big endian. 
        /// If BOM is present this parameter is ignored</param>
        /// <returns></returns>
        public static string GetString(byte[] buffer, int startIndex, int count, bool bigEndian = false)
        {
            string result = string.Empty;

            if (count == 0 || startIndex >= buffer.Length)
                return result;

            if (startIndex < 0)
            {
                throw new ArgumentException("start index can't be less than zero", "startIndex");
            }

            byte firstByte = buffer[startIndex];
            byte secondByte = buffer[startIndex + 1];

            bool littleEndianBom = (firstByte == 0xFF && secondByte == 0xFE);
            bool bigEndianBom = (firstByte == 0xFE && secondByte == 0xFF);

            if (!littleEndianBom && !bigEndianBom)
            {
#if DEBUG
                Debug.WriteLine("Error: unable to determine UTF-16 endianness, using little endian by default");
#endif
            }
            else
            {
                startIndex += 2;
                count -= 2;
            }

            bool decodeAsBigEndian = (bigEndianBom || bigEndian);
            UnicodeEncoding unicodeEncoding = new UnicodeEncoding(decodeAsBigEndian, false);

            int nStringBytes = 0;

            for (int i = 0; i + 1 < count; i = i + 2)
            {
                byte b1 = buffer[startIndex + i];
                byte b2 = buffer[startIndex + i + 1];

                if(b1 == 0 && b2 == 0)
                {
                    break;
                }

                if (b1 == 0 && b2 == 0)
                    break;
                nStringBytes += 2;
            }

            result = unicodeEncoding.GetString(buffer, startIndex, nStringBytes);
            return result;
        }

        // Removes one byte order mark
        public static string RemoveBom(string str)
        {
            if (str.Length == 0)
                return str;

            char firstChar = str[0];
            if (firstChar == 0xFFFE || firstChar == 0xFEFF)
            {
                // this is byte order mark
                return str.Substring(1);
            }
            return str;
        }
    }
}

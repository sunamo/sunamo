using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversalId3
{
    // Utility functions
    public static class Functions
    {
        public static int BigEndianToInt(byte[] buf)
        {
            return (buf[0] << 24) | (buf[1] << 16) | (buf[2] << 8) | buf[3];
        }

        public static byte[] IntToBigEndianBytes(int x)
        {
            byte[] bytes = BitConverter.GetBytes(x);
            Array.Reverse(bytes);
            return bytes;
        }

        public static string GetString(byte[] bytes, int index, int count, TextEncoding textEncoding)
        {
            
            string result = string.Empty;

            if (count == 0 || index == count)
                return result;

            int stringLength = 0;
            switch (textEncoding)
            {
                case TextEncoding.Ascii:
                case TextEncoding.Utf8:
                    for (int i = 0; i < count; i++)
                    {
                        if (bytes[index + i] == 0)
                            break;
                        stringLength++;
                    }
                    result = Encoding.UTF8.GetString(bytes, index, stringLength);
                    break;

                case TextEncoding.Utf16Bom:
                    result = Utf16String.GetString(bytes, index, count);
                    break;

                case TextEncoding.Utf16Be:
                    result = Utf16String.GetString(bytes, index, count, true);
                    break;

                default:
#if DEBUG
                    Debug.WriteLine("Unknown encoding found");
#endif
                    break;
            }
            return result;
        }

        // returns the number of bytes taken by string representation upto (including) $00 ($00)
        public static int GetStringBytesCount(byte[] bytes, int index, int count, TextEncoding textEncoding)
        {
            int bytesCount = 0;

            switch (textEncoding)
            {
                case TextEncoding.Ascii:
                case TextEncoding.Utf8:
                    for (int i = 0; i < count; i++)
                    {
                        bytesCount++;
                        if (bytes[index + i] == 0)
                            break;
                    }
                    break;

                case TextEncoding.Utf16Bom:
                case TextEncoding.Utf16Be:
                    for (int i = 0; i + 1 < count; i = i + 2)
                    {
                        bytesCount += 2;
                        if (bytes[index + i] == 0 && bytes[index + i + 1] == 0)
                            break;
                    }
                    break;

                default:
                    break;
            }
            return bytesCount;
        }

        public static byte[] Concatenate(byte[] arr1, byte[] arr2)
        {
            byte[] arr = new byte[arr1.Length + arr2.Length];
            Array.Copy(arr1, arr, arr1.Length);
            Array.Copy(arr2, 0, arr, arr1.Length, arr2.Length);
            return arr;
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace sunamo.Helpers
{
    public class EncodingHelper
    {
        public static Encoding DetectEncoding(byte[] bom)
        {
            if (bom.Length > 3)
            {
                byte first = bom[0];
                byte second = bom[1];
                byte third = bom[2];
                // Analyze the BOM
                if (first == 0x2b && second == 0x2f && third == 0x76) return Encoding.UTF7;
                if (first == 0xef && second == 0xbb && third == 0xbf) return Encoding.UTF8;
                if (first == 0xff && second == 0xfe) return Encoding.Unicode; //UTF-16LE
                if (first == 0xfe && second == 0xff) return Encoding.BigEndianUnicode; //UTF-16BE
                if (first == 0 && second == 0 && third == 0xfe && bom[3] == 0xff) return Encoding.UTF32;
            }
            return Encoding.ASCII;
        }

        static Dictionary<string, bool> TestBinaryFile(string folderPath)
        {
            Dictionary<string, bool> output = new Dictionary<string, bool>();
            foreach (string filePath in FS.GetFiles(folderPath, true))
            {
                output.Add(filePath, isBinary(filePath));
            }
            return output;
        }

        public static bool isBinary(string path)
        {
            long length = FS.GetFileSize(path);
            if (length == 0) return false;

            using (StreamReader stream = new StreamReader(path))
            {
                int ch;
                while ((ch = stream.Read()) != -1)
                {
                    if (isControlChar(ch))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static bool isControlChar(int ch)
        {
            return (ch > Chars.NUL && ch < Chars.BS)
                || (ch > Chars.CR && ch < Chars.SUB);
        }

        public static class Chars
        {
            public static char NUL = (char)0; // Null char
            public static char BS = (char)8; // Back Space
            public static char CR = (char)13; // Carriage Return
            public static char SUB = (char)26; // Substitute
        }

        public static string ConvertTo(int destEncCodepage, byte[] input, string badCharsReplaceFor)
        {
            Encoding srcEnc = DetectEncoding(input);

            Encoding destEnc = Encoding.GetEncoding(destEncCodepage, new EncoderReplacementFallback(badCharsReplaceFor), new DecoderReplacementFallback(badCharsReplaceFor));

            return destEnc.GetString( Encoding.Convert(srcEnc, destEnc, input));
        }
    }
}

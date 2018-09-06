using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;
using System.Runtime.InteropServices.WindowsRuntime;

namespace UniversalId3
{
    // for the version ID3v2.4.0
    public enum TextEncoding
    {
        Ascii = 0,          // [ISO-8859-1] $20 - $FF terminated with $00
        Utf16Bom = 1,       // Byter Order Mark must be present, terminated with $00 00
        Utf16Be = 2,        // Big endian, no BOM, terminated with $00 00
        Utf8 = 3            // Utf-8, terminated with $00
    }

    public class ID3
    {
        

        const int HEADER_SIZE = 10;

        private List<Frame> _frames = new List<Frame>();    // sequential list of frames

        private TagHeader _tagHeader = null;        // ID3v2 tag header
        StorageFile _file = null;                   // current file to read/write tags

        bool _supported = true;                 // set to false if the version not supported

        public ID3() { }

        public bool IsSupported()
        {
           // Windows.UI.ColorH.
            return _supported;
        }




    }
}
/*
 * private List<string> _textFrameIds = new List<string>{ "TALB", "TBPM", "TCOM", "TCON", "TCOP", 
            "TDAT", "TDLY", "TENC", "TEXT", "TFLT", "TIME", "TIT1", "TIT2", "TIT3", "TKEY", "TLAN", "TOAL", 
            "TOFN", "TOLY", "TOPE", "TORY", "TOWN", "TPE1", "TPE2", "TPE3", "TPE4", "TPOS", "TPUB", "TRCK", 
            "TRDA", "TRSN", "TRSO", "TSIZ", "TSRC", "TSSE", "TYER"};
 * //// if this is text frame
 foreach (var pair in _keyValuePairs)
            {
                Debug.WriteLine(pair.Key + ": " + pair.Value);
            }
 * //Stream stream = new MemoryStream(data, i, data.Length - i);
*/

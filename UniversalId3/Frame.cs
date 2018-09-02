using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;

namespace UniversalId3
{
    // version 2.3.0
    /* 
    * Frame ID   $xx xx xx xx  (four characters)     
    * Size       $xx xx xx xx  (Big endian)
    * Flags      $xx xx
    */
    public class Frame
    {
        string Id;
        byte[] Flags;
        byte[] Data;
        int Size;

        public Frame(string frameId)
        {
            this.Id = frameId;
            this.Flags = new byte[2] { 0, 0 };
            this.Data = new byte[0];    // should never be null, because Size property depends on it
        }



        /*
         *  Frame ID   $xx xx xx xx  (four characters)     
         *  Size       $xx xx xx xx     
         *  Flags      $xx xx
         */
        public static Frame ReadFrame(BinaryReader binaryReader)
        {
            string frameId = string.Empty;

            try
            {
                char[] frameIdChars = binaryReader.ReadChars(4);    // utf-8

                // check if padding reached
                if (frameIdChars[0] != 0)
                {
                    frameId = new String(frameIdChars);
                }
            }
            catch (Exception)
            {
                // System.Text.Encoding.ThrowCharsOverflow exception is thrown
            }

            if (String.IsNullOrEmpty(frameId))
                return null;

            byte[] headerSizeBytes = binaryReader.ReadBytes(4);
            int frameSize = Functions.BigEndianToInt(headerSizeBytes);

            byte[] flags = binaryReader.ReadBytes(2);

            if(flags[0] != 0 || flags[1] != 0)
            {
#if DEBUG
                Debug.WriteLine("Frame Flags are not zero");
#endif
            }

            Frame frame = new Frame(frameId);
            frame.Flags = flags;
            frame.Data = binaryReader.ReadBytes(frameSize);

            return frame;
        }

        public static void WriteFrame(Frame frame, DataWriter dataWriter)
        {
            // 4 bytes id
            byte[] frameIdBytes = Encoding.UTF8.GetBytes(frame.Id);
            Debug.Assert(frameIdBytes.Length == 4);
            dataWriter.WriteBytes(frameIdBytes);

            // 4 bytes size in big endian
            byte[] frameSizeBytes = Functions.IntToBigEndianBytes(frame.Size);
            dataWriter.WriteBytes(frameSizeBytes);

            // 2 bytes flags
            dataWriter.WriteBytes(frame.Flags);

            // data
            dataWriter.WriteBytes(frame.Data);
        }
    }

    /*
     * ID3v2/file identifier      "ID3"     
     * ID3v2 version              $03 00     
     * ID3v2 flags                %abc00000     
     * ID3v2 size             4 * %0xxxxxxx
     */
    public class TagHeader
    {
        public string Id { get; set; }
        public byte[] Version { get; set; }
        public byte Flags { get; set; }
        public int Size { get; set; }

        public byte MajorVersion { get { return Version[0]; } }
    }
}

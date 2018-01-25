using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace sunamo.PInvoke
{
    public class IconExtractor
    {
        public const uint SHGFI_ICON = 0x000000100;
        public const uint SHGFI_USEFILEATTRIBUTES = 0x000000010;
        public const uint SHGFI_OPENICON = 0x000000002;
        public const uint SHGFI_SMALLICON = 0x000000001;
        public const uint SHGFI_LARGEICON = 0x000000000;
        public const uint FILE_ATTRIBUTE_DIRECTORY = 0x00000010;

        public static Icon GetSmallIcon(string fileName, bool file)
        {
            return GetIcon(fileName, file, SHGFI_SMALLICON);
        }

        public static Icon GetLargeIcon(string fileName, bool file)
        {
            return GetIcon(fileName, file, SHGFI_LARGEICON);
        }

        private static Icon GetIcon(string fileName, bool file, uint flags)
        {
            if (file)
            {
                if (!File.Exists(fileName))
                {
                    return null;
                }
            }
            else
            {
                if (!Directory.Exists(fileName))
                {
                    return null;
                }
            }
            SHFILEINFO shinfo = new SHFILEINFO();
            IntPtr hImgSmall = W32.SHGetFileInfo(fileName, 0, out shinfo, (uint)Marshal.SizeOf(shinfo), SHGFI_ICON | flags);

            Icon icon = (Icon)System.Drawing.Icon.FromHandle(shinfo.hIcon).Clone();
            W32.DestroyIcon(shinfo.hIcon);
            return icon;
        }
    }
}

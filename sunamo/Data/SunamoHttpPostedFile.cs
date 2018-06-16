using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sunamo.Data
{
    public class SunamoHttpPostedFile
    {
        public SunamoHttpPostedFile()
        {

        }

        public SunamoHttpPostedFile(long ContentLength, string ContentType, string FileName, Stream InputStream)
        {
            this.ContentLength = ContentLength;
            this.ContentType = ContentType;

            MemoryStream ms = new MemoryStream();
            sunamo.FS.CopyStream(InputStream, ms);

            this.Bytes = ms.ToArray();
            this.FileName = FileName;
        }

        public SunamoHttpPostedFile(long ContentLength, string ContentType, string FileName, byte[] InputStream)
        {
            this.ContentLength = ContentLength;
            this.ContentType = ContentType;
            this.Bytes = InputStream;
            this.FileName = FileName;
        }

        public long ContentLength { get; set; }
        public string ContentType { get; set; }
        //public Stream InputStream { get; set; }
        public byte[] Bytes { get; set; }

        public string FileName
        {
            get; set;
        }

        public void SaveAs(string filename)
        {
            //sunamo.FS.SaveStream(filename, InputStream);
            File.WriteAllBytes(filename, Bytes);
        }
    }
}

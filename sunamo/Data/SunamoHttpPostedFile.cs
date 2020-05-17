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
            FS.CopyStream(InputStream, ms);

            this.Bytes = ms.ToArray().ToList();
            this.FileName = FileName;
        }

        public SunamoHttpPostedFile(long ContentLength, string ContentType, string FileName, List<byte> InputStream)
        {
            this.ContentLength = ContentLength;
            this.ContentType = ContentType;
            this.Bytes = InputStream;
            this.FileName = FileName;
        }

        public long ContentLength { get; set; }
        public string ContentType { get; set; }
        //public Stream InputStream { get; set; }
        public List<byte> Bytes { get; set; }

        public string FileName
        {
            get; set;
        }

        public void SaveAs(string filename)
        {
            //FS.SaveStream(filename, InputStream);
            TF.WriteAllBytes(filename, Bytes);
        }
    }
}
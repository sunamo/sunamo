using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sunamo.Data
{
    public class PhotoToUpload
    {
        public string path { get; set; }
        public string pathSanitized { get; set; }
        //public int size { get; set; }
        public DateTime DateTaken { get; set; }
        //public string idPhoto { get; set; }
        public int originalSize { get; set; }
    }
}

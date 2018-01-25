using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace sunamo.Data
{
    public class BitmapImageWithPath
    {
        public string path = "";
        public BitmapImage image = null;

        public BitmapImageWithPath(string path, BitmapImage image)
        {
            this.path = path;
            this.image = image;
        }

        public override string ToString()
        {
            return path;
        }
    }
}

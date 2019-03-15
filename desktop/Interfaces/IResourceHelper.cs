using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Media.Imaging;

namespace sunamo.Interfaces
{
    public interface IResourceHelper
    {
        string GetString(string name);
        BitmapImage GetBitmapImageSource(string name);
        Stream GetStream(string name);
    }
}

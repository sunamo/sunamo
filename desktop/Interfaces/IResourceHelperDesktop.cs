using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Media.Imaging;


    public interface IResourceHelperDesktop : IResourceHelper
    {
        
        BitmapImage GetBitmapImageSource(string name);
        
    }
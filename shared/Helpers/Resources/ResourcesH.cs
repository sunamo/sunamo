using sunamo.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Resources;


/// <summary>
/// Must be in desktop due to BitmapImage
/// Dont require any initialization steps
/// Path is entered like FS from project root
/// </summary>
public class ResourcesH : IResourceHelper
    {
    /*
            // Navigate to xaml page
            Uri uri = new Uri("/Resources/Resource.txt", UriKind.Relative);
            // absolute
            Uri uriJpg = new Uri("pack://application:,,,/Wpf.Tests;component/Resources/Resource.jpg", UriKind.Absolute);
            StreamResourceInfo info = Application.GetResourceStream(uri);
            txtResource.Text = Encoding.UTF8.GetString(FS.StreamToArrayBytes(info.Stream));
            imgResource.Source = new BitmapImage(uriJpg);
     */

    public static ResourcesH ci = new ResourcesH();

        private ResourcesH()
        {

        }

        public Uri GetRelativeUri(string name)
        {
            return new Uri(SH.PrefixIfNotStartedWith(AllStrings.slash, name), UriKind.Relative);
        }

        public BitmapImage GetBitmapImageSource(string name)
        {
            return new BitmapImage(GetRelativeUri(name));
        }

        public string GetString(string name)
        {
            return Encoding.UTF8.GetString(FS.StreamToArrayBytes(GetStream(name)));
        }

    public Stream GetStream(string name)
    {
        StreamResourceInfo info = Application.GetResourceStream(GetRelativeUri(name));
        return info.Stream;
    }
}

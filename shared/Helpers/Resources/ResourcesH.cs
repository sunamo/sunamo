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
/// CANT BE USED WITH UNIT TESTS. USE EmbeddedResourcesH
/// 
/// Dont require any initialization steps
/// Path is entered like FS from project root
/// Must be in shared - required PresentationFramework.dll
/// 
/// Cant load data from other than entry assembly
/// 
/// Assembly.GetEntryAssembly() returns null. Set the Application.ResourceAssembly property or use the pack://application:,,,/assemblyname;component/ syntax to specify the assembly to load the resource from.'
/// Set Application.ResourceAssembly in unit tests is not allowed
/// In non unit test app is not needed set Application.ResourceAssembly - automatically loading entry assembly (like AllProjectsSearch)
/// </summary>
public class ResourcesH : IResourceHelper
    {
    /*
     * 
            // Navigate to xaml page
            Uri uri = new Uri("/Resources/Resource.txt", UriKind.Relative);
            // absolute
            Uri uriJpg = new Uri("pack://application:,,,/Wpf.Tests;component/Resources/Resource.jpg", UriKind.Absolute);
            imgResource.Source = new BitmapImage(uriJpg);

            StreamResourceInfo info = Application.GetResourceStream(uri);
            txtResource.Text = Encoding.UTF8.GetString(FS.StreamToArrayBytes(info.Stream));
            
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
        var uri = GetRelativeUri(name);
        StreamResourceInfo info = Application.GetResourceStream(uri);
        return info.Stream;
    }
}

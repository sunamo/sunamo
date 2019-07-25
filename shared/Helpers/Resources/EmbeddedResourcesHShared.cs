using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

public class EmbeddedResourcesHShared : EmbeddedResourcesH
{
    //public static EmbeddedResourcesHShared ci = new EmbeddedResourcesHShared()

    public EmbeddedResourcesHShared(Assembly _entryAssembly, string defaultNamespace) : base(_entryAssembly, defaultNamespace)
    {

    }

    public BitmapImage GetBitmapImageSource(string name)
    {
        var imageSource = new BitmapImage();

        using (var stream = entryAssembly.GetManifestResourceStream(GetResourceName(name)))
        {
            imageSource.BeginInit();
            imageSource.StreamSource = stream;
            imageSource.EndInit();
        }

        return imageSource;
    }
}


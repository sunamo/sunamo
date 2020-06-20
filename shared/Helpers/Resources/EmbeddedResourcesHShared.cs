using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using sunamo.Essential;

public class EmbeddedResourcesHShared : EmbeddedResourcesH
{
    public static EmbeddedResourcesHShared ciShared = null;

    static EmbeddedResourcesHShared()
    {
        ciShared = new EmbeddedResourcesHShared();
        ci = ciShared;
    }

    private EmbeddedResourcesHShared()
    {
        _entryAssembly = RH.AssemblyWithName(ThisApp.Name);
        //Assembly.GetAssembly()

        _defaultNamespace = ThisApp.Name;
    }

    public EmbeddedResourcesHShared(Assembly _entryAssembly, string defaultNamespace) : base(_entryAssembly, defaultNamespace)
    {

    }

    public BitmapImage GetBitmapImageSource(string name)
    {
        var imageSource = new BitmapImage();

        var resName = GetResourceName(name);
        using (var stream = entryAssembly.GetManifestResourceStream(resName))
        {
            imageSource.BeginInit();
            imageSource.StreamSource = stream;
            imageSource.EndInit();
        }

        return imageSource;
    }

    /// <summary>
    /// When is ns different (Dummy Selling app) than name of uc (Dummy UC) must use this
    /// Normally used ThisApp.Name as ns
    /// </summary>
    /// <param name="_entryAssembly"></param>
    /// <param name="defaultNamespace"></param>
    /// <param name="v"></param>
    /// <returns></returns>
    public ImageSource GetAppIcon(Assembly _entryAssembly, string defaultNamespace, string v)
    {
        var er = new EmbeddedResourcesHShared(_entryAssembly, defaultNamespace);
        return er.GetBitmapImageSource(v);
    }

    /// <summary>
    /// ALways take from ciShared ()
    /// 
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    public ImageSource GetAppIcon(string v)
    {
        var ims = ciShared.GetBitmapImageSource(v);
        return ims;
    }
}
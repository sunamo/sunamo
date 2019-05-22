using sunamo.Essential;
using sunamo.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

/// <summary>
/// Must be in desktop due to BitmapImage
/// </summary>
public class EmbeddedResourcesH : IResourceHelper
    {
    /// <summary>
    /// For entry assembly
    /// </summary>
        public static EmbeddedResourcesH ci = new EmbeddedResourcesH();

    /// <summary>
    /// 
    /// </summary>
    private EmbeddedResourcesH()
        {
        defaultNamespace = ThisApp.Name;
        }

    /// <summary>
    /// public to use in assembly like SunamoNTextCat
    /// </summary>
    /// <param name="_entryAssembly"></param>
    public EmbeddedResourcesH(Assembly _entryAssembly, string defaultNamespace)
    {
        this. _entryAssembly = _entryAssembly;
        this.defaultNamespace = defaultNamespace;
    }

           Assembly _entryAssembly = null;
     string defaultNamespace;

     Assembly entryAssembly
    {
        get
        {
            if (_entryAssembly == null)
            {
                _entryAssembly = Assembly.GetEntryAssembly();
            }
            return _entryAssembly;
        }
    }

        public string GetResourceName(string name)
        {
            name = SH.Join(AllChars.dot, defaultNamespace, SH.ReplaceAll( name, AllStrings.dot, AllStrings.slash));
        return name;
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

        public string GetString(string name)
        {

            return Encoding.UTF8.GetString(FS.StreamToArrayBytes(GetStream(name)));
        }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public Stream GetStream(string name)
    {
        var s = GetResourceName(name);
        var vr = entryAssembly.GetManifestResourceStream(s);
        return vr;
    }
}

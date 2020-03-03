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


public class EmbeddedResourcesH : IResourceHelper
    {
        public static EmbeddedResourcesH ci = new EmbeddedResourcesH();

        private EmbeddedResourcesH()
        {

        }

        static Assembly _entryAssembly = null;

    static Assembly entryAssembly
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

        string GetResourceName(string name)
        {
            name = SH.Join(AllChars.dot, ThisApp.Name, SH.ReplaceAll( name, ".", "/"));
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

    public Stream GetStream(string name)
    {
        return entryAssembly.GetManifestResourceStream(GetResourceName(name));
    }
}
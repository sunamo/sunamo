using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace sunamo.Helpers
{
    /// <summary>
    /// Load from files *.resources and *.resx. Nothing else
    /// usage: ThisApp.Resources = ResourcesHelper.Create("sunamo.Properties.Resources", typeof(ResourcesHelper).Assembly)
    /// 
    /// When change joined file, change of content will be update also in *.resx
    /// </summary>
    public class ResourcesHelper
    {
        #region For easy copy
        private ResourceManager _rm = null;

        private ResourcesHelper()
        {
        }

        /// <summary>
        /// A1 - file without extension and lang specifier but with Name 
        /// MyApp.MyResource.en-US.resx is MyApp.MyResource
        /// </summary>
        /// <param name="executingAssembly"></param>
        public static ResourcesHelper Create(string resourceClass, Assembly sunamoAssembly)
        {
            ResourcesHelper resourcesHelper = new ResourcesHelper();
            resourcesHelper._rm = new ResourceManager(resourceClass, sunamoAssembly);
            return resourcesHelper;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string GetString(string name)
        {
            return _rm.GetString(name);
        }

        public string GetByteArrayAsString(string name)
        {
            var ba = _rm.GetObject(name);
            //var ab = FS.StreamToArrayBytes((Stream)ba);
            return Encoding.UTF8.GetString((byte[])ba);
        } 
        #endregion
    }
}
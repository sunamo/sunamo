using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace sunamo.Helpers
{
    /// <summary>
    /// Load from files *.resources
    /// usage: ThisApp.Resources = ResourcesHelper.Create("sunamo.Properties.Resources", typeof(ResourcesHelper).Assembly)
    /// </summary>
    public class ResourcesHelper
    {
        private ResourceManager _rm = null;

        private ResourcesHelper()
        {
        }

        /// <summary>
        /// A1 - file without extension and lang specifier but with Name MyApp.MyResource.en-US.resx is MyApp.MyResource
        /// </summary>
        /// <param name="executingAssembly"></param>
        /// <returns></returns>
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
    }
}

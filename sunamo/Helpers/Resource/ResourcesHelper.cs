using sunamo.Enums;
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
    /// </summary>
    public  class ResourcesHelper
    {
        private ResourceManager rm = null;

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
            resourcesHelper.rm = new ResourceManager(typeof(Properties.Resources));
            return resourcesHelper;
        }

        public static ResourcesHelper Create(Type resourcesHelperType)
        {
            return Create(resourcesHelperType.FullName, resourcesHelperType.Assembly);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string GetString(string name)
        {
            return rm.GetString(name);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string GetString(ResourceKeys name)
        {
            return rm.GetString(name.ToString());
        }
    }
}

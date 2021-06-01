using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

public class ResourcesHelperXlf
{
    #region For easy copy
    private ResourceManager _rm = null;

    private ResourcesHelperXlf()
    {
    }

    /// <summary>
    /// A1 - file without extension and lang specifier but with Name 
    /// MyApp.MyResource.en-US.resx is MyApp.MyResource
    /// </summary>
    /// <param name="executingAssembly"></param>
    public static ResourcesHelperXlf Create(string resourceClass, Assembly sunamoAssembly)
    {
        ResourcesHelperXlf resourcesHelper = new ResourcesHelperXlf();
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
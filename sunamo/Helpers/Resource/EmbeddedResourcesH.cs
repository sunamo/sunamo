using sunamo;
using sunamo.Essential;
using sunamo.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


/// <summary>
/// 
/// Require assembly and default namespace. 
/// Content is referred like with ResourcesH - with fs path
/// </summary>
public class EmbeddedResourcesH : IResourceHelper
    {
    /*usage:
uri = new Uri("Wpf.Tests.Resources.EmbeddedResource.txt", UriKind.Relative);
GetString(uri.ToString()) - the same string as passed in ctor Uri

     */

    /// <summary>
    /// For entry assembly
    /// </summary>
    public static EmbeddedResourcesH ci = new EmbeddedResourcesH();

    /// <summary>
    /// 
    /// </summary>
    private EmbeddedResourcesH()
        {
        _entryAssembly = RH.AssemblyWithName(ThisApp.Name);
        //Assembly.GetAssembly()
            
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

    protected Assembly entryAssembly
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
            name = SH.Join(AllChars.dot, defaultNamespace, SH.ReplaceAll( name.TrimStart(AllChars.slash), AllStrings.dot, AllStrings.slash));
        return name;
        }

       

    /// <summary>
    /// If it's file, return its content
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
        public string GetString(string name)
        {
        var s = GetStream(name);

            return Encoding.UTF8.GetString(FS.StreamToArrayBytes(s));
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

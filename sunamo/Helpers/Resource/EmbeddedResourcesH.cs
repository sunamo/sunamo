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
    public static EmbeddedResourcesH ci = null;

    /// <summary>
    /// 
    /// </summary>
    protected EmbeddedResourcesH()
    {

    }

    /// <summary>
    /// public to use in assembly like SunamoNTextCat
    /// A2 is name of project, therefore don't insert typeResourcesSunamo.Namespace
    /// </summary>
    /// <param name="_entryAssembly"></param>
    public EmbeddedResourcesH(Assembly _entryAssembly, string defaultNamespace)
    {
        this._entryAssembly = _entryAssembly;
        _defaultNamespace = defaultNamespace;
    }

    protected Assembly _entryAssembly = null;
    protected string _defaultNamespace;

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
        name = SH.Join(AllChars.dot, _defaultNamespace, SH.ReplaceAll(name.TrimStart(AllChars.slash), AllStrings.dot, AllStrings.slash));
        return name;
    }

    /// <summary>
    /// If it's file, return its content
    /// Its for getting string from file, never from resx or another in code variable
    /// </summary>
    /// <param name="name"></param>
    public string GetString(string name)
    {
        var s = GetStream(name);

        return Encoding.UTF8.GetString(FS.StreamToArrayBytes(s));
    }

    /// <summary>
    /// Resources/tidy_config.txt (no assembly)
    /// </summary>
    /// <param name="name"></param>
    public Stream GetStream(string name)
    {
        var s = GetResourceName(name);
        var vr = entryAssembly.GetManifestResourceStream(s);
        return vr;
    }
}

   
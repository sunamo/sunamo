namespace sunamo.Properties {
    using System;
    using System.Reflection;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("sunamo.Properties.Resources", typeof(Resources).GetTypeInfo().Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;/body&gt;
        ///&lt;/html&gt;.
        /// </summary>
        internal static string Html5_boilerplate_end {
            get {
                return ResourceManager.GetString("Html5-boilerplate-end", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;!DOCTYPE html&gt;
        ///&lt;!--[if lt IE 7]&gt;      &lt;html class=&quot;no-js lt-ie9 lt-ie8 lt-ie7&quot;&gt; &lt;![endif]--&gt;
        ///&lt;!--[if IE 7]&gt;         &lt;html class=&quot;no-js lt-ie9 lt-ie8&quot;&gt; &lt;![endif]--&gt;
        ///&lt;!--[if IE 8]&gt;         &lt;html class=&quot;no-js lt-ie9&quot;&gt; &lt;![endif]--&gt;
        ///&lt;!--[if gt IE 8]&gt;&lt;!--&gt; &lt;html class=&quot;no-js&quot;&gt; &lt;!--&lt;![endif]--&gt;
        ///    &lt;head&gt;
        ///        &lt;meta charset=&quot;utf-8&quot;&gt;
        ///        &lt;meta http-equiv=&quot;X-UA-Compatible&quot; content=&quot;IE=edge&quot;&gt;
        ///        &lt;title&gt;&lt;/title&gt;
        ///        &lt;meta name=&quot;description&quot; content=&quot;&quot;&gt;
        ///        &lt;meta name=&quot;viewport&quot; content=&quot; [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string Html5_boilerplate_start {
            get {
                return ResourceManager.GetString("Html5-boilerplate-start", resourceCulture);
            }
        }
    }
}

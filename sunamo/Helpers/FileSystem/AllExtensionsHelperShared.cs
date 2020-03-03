
using sunamo;
using sunamo.Enums;
using System.Collections.Generic;
using System.Linq;

public partial class AllExtensionsHelper
{
    /// <summary>
    /// With dot
    /// </summary>
    public static Dictionary<TypeOfExtension, List<string>> extensionsByType = null;
    /// <summary>
    /// With dot
    /// </summary>
     //public static Dictionary<string, TypeOfExtension> allExtensions = new Dictionary<string, TypeOfExtension>();
    public static Dictionary<TypeOfExtension, List<string>> extensionsByTypeWithoutDot = null;
    public static Dictionary<string, TypeOfExtension> allExtensionsWithoutDot = null;

    static AllExtensionsHelper()
    {
        // Must call Initialize here, not in Loaded of Window. when I run auto code in debug, it wont be initialized as is needed.
        Initialize();
    }

    public static void Initialize()
    {
        


        if (extensionsByType == null)
        {
            extensionsByType = new Dictionary<TypeOfExtension, List<string>>();
            extensionsByTypeWithoutDot = new Dictionary<TypeOfExtension, List<string>>();
            allExtensionsWithoutDot = new Dictionary<string, TypeOfExtension>();

            AllExtensions ae = new AllExtensions();
            var exts = RH.GetConsts(typeof(AllExtensions));
            foreach (var item in exts)
            {
                string extWithDot = item.GetValue(ae).ToString();
                string extWithoutDot = extWithDot.Substring(1);
                var v1 = item.CustomAttributes.First();
                TypeOfExtension toe = (TypeOfExtension)v1.ConstructorArguments.First().Value;
                //allExtensions.Add(extWithDot, toe);
                allExtensionsWithoutDot.Add(extWithoutDot, toe);
                if (!extensionsByType.ContainsKey(toe))
                {
                    List<string> extensions = new List<string>();
                    extensions.Add(extWithDot);
                    extensionsByType.Add(toe, extensions);
                    List<string> extensionsWithoutDot = new List<string>();
                    extensionsWithoutDot.Add(extWithoutDot);
                    extensionsByTypeWithoutDot.Add(toe, extensionsWithoutDot);
                }
                else
                {
                    extensionsByType[toe].Add(extWithDot);
                    extensionsByTypeWithoutDot[toe].Add(extWithoutDot);
                }
            }
        }
    }

    /// <summary>
    /// When can't be found, return other
    /// Default was WithDot
    /// </summary>
    /// <param name = "p"></param>
    public static TypeOfExtension FindTypeWithoutDot(string p)
    {
        if (p != "")
        {
            if (allExtensionsWithoutDot.ContainsKey(p))
            {
                return allExtensionsWithoutDot[p];
            }
        }

        return TypeOfExtension.other;
    }

    /// <summary>
    /// A1 can be with or without dot
    /// </summary>
    /// <param name="ext"></param>
    public static bool IsContained(string p)
    {
        p = p.TrimStart(AllChars.dot);
        return allExtensionsWithoutDot.ContainsKey(p);
    }

    /// <summary>
    /// When can't be found, return other
    /// Was default
    /// </summary>
    /// <param name = "p"></param>
    public static TypeOfExtension FindTypeWithDot(string p)
    {
        if (p != "")
        {
            p = p.Substring(1);
            if (allExtensionsWithoutDot.ContainsKey(p))
            {
                return allExtensionsWithoutDot[p];
            }
        }

        return TypeOfExtension.other;
    }
}
using sunamo.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sunamo.Helpers
{
    public class AllExtensionsHelper
    {
        public static Dictionary<TypeOfExtension, List<string>> extensionsByType = new Dictionary<TypeOfExtension, List<string>>();
        public static Dictionary<string, TypeOfExtension> allExtensions = new Dictionary<string, TypeOfExtension>();
        public static Dictionary<TypeOfExtension, List<string>> extensionsByTypeWithoutDot = new Dictionary<TypeOfExtension, List<string>>();
        public static Dictionary<string, TypeOfExtension> allExtensionsWithoutDot = new Dictionary<string, TypeOfExtension>();

        public static void Initialize()
        {
            if (extensionsByType.Count == 0)
            {
                AllExtensions ae = new AllExtensions();
                var exts = sunamo.RH.GetConstants(typeof(AllExtensions));
                foreach (var item in exts)
                {
                    string o = item.GetValue(ae).ToString();
                    string oWithoutDot = o.Substring(1);
                    var v1 = item.CustomAttributes.First();
                    TypeOfExtension toe = (TypeOfExtension)v1.ConstructorArguments.First().Value;
                    allExtensions.Add(o, toe);
                    allExtensionsWithoutDot.Add(oWithoutDot, toe);
                    if (!extensionsByType.ContainsKey(toe))
                    {
                        List<string> extensions = new List<string>();
                        extensions.Add(o);
                        extensionsByType.Add(toe, extensions);
                        List<string> extensionsWithoutDot = new List<string>();
                        extensionsWithoutDot.Add(oWithoutDot);
                        extensionsByTypeWithoutDot.Add(toe, extensionsWithoutDot);
                    }
                    else
                    {
                        extensionsByType[toe].Add(o);
                        extensionsByTypeWithoutDot[toe].Add(oWithoutDot);
                    }
                }
            }
        }

        public static TypeOfExtension FindType(string p)
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
}


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
        /// <summary>
        /// With dot
        /// </summary>
        public static Dictionary<TypeOfExtension, List<string>> extensionsByType = new Dictionary<TypeOfExtension, List<string>>();
        /// <summary>
        /// With dot
        /// </summary>
        //public static Dictionary<string, TypeOfExtension> allExtensions = new Dictionary<string, TypeOfExtension>();
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
        /// <param name="p"></param>
        /// <returns></returns>
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
        /// When can't be found, return other
        /// Was default
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
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
}


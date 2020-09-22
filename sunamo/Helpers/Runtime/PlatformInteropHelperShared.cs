using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

public partial class PlatformInteropHelper
{
    static bool? isUwp = null;

    #region For easy copy
    public static bool IsSellingApp()
    {
        return RH.ExistsClass("SellingHelper");
    } 
    #endregion

    /// <summary>
    /// Working excellent 11-3-19
    /// </summary>
    public static bool IsUwpWindowsStoreApp()
    {
        if (isUwp.HasValue)
        {
            return isUwp.Value;
        }

        var ass = AppDomain.CurrentDomain.GetAssemblies();
        foreach (var item in ass)
        {
            Type[] types = null;
            try
            {
                types = item.GetTypes();
            }
            catch (Exception ex)
            {
            }

            if (types != null)
            {
                foreach (var type in types)
                {
                    if (type.Namespace != null)
                    {
                        if (type.Namespace.StartsWith("Windows.UI"))
                        {
                            isUwp = true;
                            break;
                        }
                    }
                }

                if (isUwp.HasValue)
                {
                    break;
                }
            }
        }

        if (!isUwp.HasValue)
        {
            isUwp = false;
        }

        return isUwp.Value;
    }

    public static Type GetTypeOfResources()
    {
        return typeof(Resources.ResourcesDuo);
    }
}
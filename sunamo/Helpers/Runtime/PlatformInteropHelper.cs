using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;


public class PlatformInteropHelper
{
    /// <summary>
    /// Wpf.Tests = .NET Framework 4.8.4018.0
    /// ConsoleStandardApp2 = .NET Core 3.0.0
    /// GeoCachingTool = .NET Core 4.6.00001.0
    /// </summary>
    /// <returns></returns>
    public static bool IsUseStandardProject()
    {
        // Return one of three values:
        var result = RuntimeInformation.FrameworkDescription;
        if (result.StartsWith( RuntimeFrameworks.netCore))
        {
            return true;
        }
        return false;
    }

    public static Type GetTypeOfResources()
    {
        if (IsUseStandardProject())
        {
            return typeof(standard.ResourcesStandard);
        }
        else
        {
            return typeof(sunamo.Properties.Resources);
        }
    }

    static bool? isUwp = null;

    /// <summary>
    /// Working excellent 11-3-19
    /// </summary>
    /// <returns></returns>
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

    //internal static AppDataBase<StorageFolder, StorageFile> AppData()
    //{
    //    if (IsUwpWindowsStoreApp())
    //    {
    //        return AppDataApps.
    //    }
    //}
}


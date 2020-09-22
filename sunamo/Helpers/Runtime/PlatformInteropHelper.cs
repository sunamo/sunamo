using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
public partial class PlatformInteropHelper
{
    /// <summary>
    /// Wpf.Tests = .NET Framework 4.8.4018.0
    /// ConsoleStandardApp2 = .NET Core 3.0.0
    /// GeoCachingTool = .NET Core 4.6.00001.0
    /// </summary>
    public static bool IsUseStandardProject()
    {
        // Return one of three values:
        var result = RuntimeInformation.FrameworkDescription;
        if (result.StartsWith(RuntimeFrameworks.netCore))
        {
            return true;
        }

        return false;
    }

    //public static AppDataBase<StorageFolder, StorageFile> AppData()
    //{
    //    if (IsUwpWindowsStoreApp())
    //    {
    //        return AppData.
    //    }
    //}
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class PlatformInteropHelperXlf
{
    #region For easy copy
    public static bool IsSellingApp()
    {
        return RHXlf.ExistsClass("SellingHelper");
    }
    #endregion
}
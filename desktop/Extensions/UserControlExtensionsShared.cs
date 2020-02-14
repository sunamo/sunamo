using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

public static partial class UserControlExtensions{ 
public static void MakeScreenshot(this UserControl uc)
    {
        FrameworkElementHelper.CreateBitmapFromVisual(null, null);
    }
}
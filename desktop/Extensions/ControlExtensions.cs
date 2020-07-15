using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

public static class ControlExtensions
{
    public static void MakeScreenshot(this Control uc)
    {
        FrameworkElementHelper.CreateBitmapFromVisual(null, null);
    }
}
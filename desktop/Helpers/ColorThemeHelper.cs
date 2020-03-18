using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using sunamo.Enums;

public class ColorThemeHelper
{
    static Type type = typeof(ColorThemeHelper);

    public static void ApplyColorTheme(Border border, ColorTheme ct)
    {
        ThrowExceptions.NotImplementedMethod(Exc.GetStackTrace(), type, Exc.CallingMethod());
    }
}
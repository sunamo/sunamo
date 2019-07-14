using sunamo.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static partial class SunamoSizeExtensions
{
    public static System.Windows.Size ToSystemWindows(this SunamoSize ss)
    {
        return new System.Windows.Size(ss.Width, ss.Height);
    }
}


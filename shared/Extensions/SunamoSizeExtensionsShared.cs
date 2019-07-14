using sunamo.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static partial class SunamoSizeExtensions{ 
public static System.Drawing.Size ToSystemDrawing(this SunamoSize ss)
    {
        return new System.Drawing.Size((int)ss.Width, (int)ss.Height);
    }
}
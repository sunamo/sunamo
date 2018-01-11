using sunamo.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shared.Extensions
{
    public static class SunamoSizeExtensions
    {
        public static System.Windows.Size ToSystemWindows(this sunamo.Data.SunamoSize ss)
        {
            return new System.Windows.Size(ss.Width, ss.Height);
        }

        public static System.Drawing.Size ToSystemDrawing(this sunamo.Data.SunamoSize ss)
        {
            return new System.Drawing.Size((int)ss.Width, (int)ss.Height);
        }
    }
}

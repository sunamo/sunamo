using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace sunamo.Extensions
{
    public static class SizeExtensions
    {
        /// <summary>
        /// Může se použít jen pokud není velikost nekonečná
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static System.Drawing.Size ToDrawing(this Size s)
        {
            return new System.Drawing.Size((int)s.Width, (int)s.Height);
        }
    }
}

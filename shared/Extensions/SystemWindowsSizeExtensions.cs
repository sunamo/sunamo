using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


    /// <summary>
    /// Use only SunamoSize/DesktopSize (compatible) and put not extensions here
    /// </summary>
    public static class SystemWindowsSizeExtensions
    {
        /// <summary>
        /// Může se použít jen pokud není velikost nekonečná
        /// </summary>
        /// <param name="s"></param>
        public static System.Drawing.Size ToDrawing(this Size s)
        {
            return new System.Drawing.Size((int)s.Width, (int)s.Height);
        }

        public static SunamoSize ToSunamo(this Size s)
        {
            return new SunamoSize(s.Width, s.Height);
        }
    }
using sunamo.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public partial class PicturesSunamo{ 
/// <summary>
        /// Vypočte optimální šířku v případě že obrázek je postaven na výšku.
        /// </summary>
        /// <param name="p"></param>
        /// <param name="p_2"></param>
        /// <param name="p_3"></param>
        /// <returns></returns>
        public static SunamoSize CalculateOptimalSizeHeight(int width, int height, int maxHeight)
        {
            SunamoSize vr = new SunamoSize(width, height);
            int vyskaSloupce = maxHeight;
            if (height > vyskaSloupce)
            {
                vr.Height = vyskaSloupce;

                // mohl by ses ještě rozhodovat jestli round, nebo floor, nebo ceil
                vr.Width = vyskaSloupce * width / height;
            }

            return vr;
        }

}
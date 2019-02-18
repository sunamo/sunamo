using sunamo.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    public partial class PicturesSunamo
    {


        

        

        public static SunamoSize CalculateOptimalSize(int width, int height, int maxWidth)
        {
            SunamoSize vr = new SunamoSize(width, height);
            int sirkaSloupce = maxWidth;
            if (width > sirkaSloupce)
            {
                vr.Width = sirkaSloupce;

                // mohl by ses ještě rozhodovat jestli round, nebo floor, nebo ceil
                vr.Height = sirkaSloupce * height / width;
            }

            return vr;
        }
    }
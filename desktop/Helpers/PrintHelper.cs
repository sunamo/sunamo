using sunamo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace desktop
{
    public enum FormatOfPaper
    {
        A,
        B,
        C
    }

    public enum LengthUnit
    {
        Mm,
        In
    }

    public class PrintHelper
    {
        public static Size GetPixelSizeForPaper(int dpiXPrinter, int dpiYPrinter, FormatOfPaper fp, int size, LandscapePortrait lp)
        {
            Size sizeInOfPaper = SizeOfPaper.GetPaperSize(fp.ToString() + size, LengthUnit.In, lp);
            sizeInOfPaper = SizeH.Multiply(sizeInOfPaper, dpiXPrinter, dpiYPrinter);
            return SizeH.Divide(sizeInOfPaper, 2);
        }
    }

    public static class SizeOfPaper
    {
        const double mmInInch = 25.4d;

        /// <summary>
        /// V režimu Portrait pouze
        /// </summary>
        static Dictionary<string, Size> papersInMm = new Dictionary<string, Size>();

        static SizeOfPaper()
        {
            papersInMm.Add("A4", new Size(210, 297));
        }

        public static Size GetPaperSize(string a4, LengthUnit lu, LandscapePortrait lp)
        {
            if (papersInMm.ContainsKey(a4))
            {
                Size vr = papersInMm[a4];
                if (lp == LandscapePortrait.Landscape)
                {
                    vr = new Size(vr.Height, vr.Width);
                }
                if (lu == LengthUnit.Mm)
                {
                    return vr;
                }
                else if (lu == LengthUnit.In)
                {
                    return SizeH.Divide(vr, mmInInch);
                }
            }
            else
            {

            }
            throw new Exception("NI SizeOfPaper.GetPaperSize()");
        }
    }
}

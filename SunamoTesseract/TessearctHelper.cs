using sunamo;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunamoTesseract
{
    public class TessearctHelper
    {
        public static string ParseText(string path)
        {
            var va = "TESSDATA_PREFIX";
            Environment.SetEnvironmentVariable(va, @"d:\Documents\GitHub\How-to-use-tesseract-ocr-4.0-with-csharp\tesseract-master.1153\tessdata\");
            var env = Environment.GetEnvironmentVariable(va);


            var solutionDirectory = string.Empty;
            //solutionDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).FullName;
            solutionDirectory = @"d:\Documents\GitHub\How-to-use-tesseract-ocr-4.0-with-csharp\";

            var tesseractPath = solutionDirectory + @"tesseract-master.1153";

            byte[] imageFile = null;
            string ext = FS.GetExtension(path);

            if (ext != AllExtensions.tiff)
            {
                MemoryStream ms = new MemoryStream();
                Bitmap bmp = new Bitmap(path);
                bmp.Save(ms, ImageFormat.Tiff);
                imageFile = ms.ToArray();
            }
            else
            {
                imageFile = File.ReadAllBytes(path);
            }
            
            var text = TessearctHelper.ParseText(tesseractPath, imageFile, "eng").Trim(); ;
            return text;
        }

        private static string ParseText(string tesseractPath, byte[] imageFile, params string[] lang)
        {
            string output = string.Empty;
            var tempOutputFile = Path.GetTempPath() + Guid.NewGuid();
            var tempImageFile = Path.GetTempFileName();

            try
            {
                File.WriteAllBytes(tempImageFile, imageFile);

                ProcessStartInfo info = new ProcessStartInfo();
                info.WorkingDirectory = tesseractPath;
                info.WindowStyle = ProcessWindowStyle.Hidden;
                info.UseShellExecute = false;
                info.FileName = "cmd.exe";
                info.Arguments =
                    "/c tesseract.exe " +
                    // Image file.
                    tempImageFile + " " +
                    // Output file (tesseract add '.txt' at the end)
                    tempOutputFile +
                    // Languages.
                    " -l " + string.Join("+", lang);

                // Start tesseract.
                Process process = Process.Start(info);
                process.WaitForExit();
                if (process.ExitCode == 0)
                {
                    // Exit code: success.
                    output = File.ReadAllText(tempOutputFile + ".txt");
                }
                else
                {
                    throw new Exception("Error. Tesseract stopped with an error code = " + process.ExitCode);
                }
            }
            finally
            {
                File.Delete(tempImageFile);
                File.Delete(tempOutputFile + ".txt");
            }
            return output;
        }
    }
}

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
    /// <summary>
    /// Denik ostravaka:
    /// nemůžu to získat z webu, není to rozdělené na kategorie, příspěvky nejsou někdy v knize
    ///musi to byt naskenovane, nejlepe po jednom při focení nebo skenování po více to nikdy není rovné
    ///skenovane bez roztřesení
    ///musí to být rovně, irfanview to umí narovnat
    ///nedává to moc dobře tečky, musí se překrýt bílou, jinak to hází komplet nesmyslné texty
    /// </summary>
    public class TessearctHelper
    {
        /// <summary>
        /// For A1 can use GetImageFile
        /// </summary>
        /// <param name="imageFile"></param>
        /// <param name="lang"></param>
        public static string ParseText(byte[] imageFile, params string[] lang)
        {
            var tesseractPath = GetTesseractPath();
            return ParseText(tesseractPath, imageFile, lang);
        }

        /// <summary>
        /// lang: cse, eng
        /// </summary>
        /// <param name="path"></param>
        /// <param name="lang"></param>
        public static string ParseText(string path, string lang = "ces")
        {
            var va = "TESSDATA_PREFIX";
            Environment.SetEnvironmentVariable(va, @"d:\Documents\BitBucket\How-to-use-tesseract-ocr-4.0-with-csharp\tesseract-master.1153\tessdata\");
            var env = Environment.GetEnvironmentVariable(va);

            var tesseractPath = string.Empty;
            tesseractPath = GetTesseractPath();

            byte[] imageFile = null;
            string ext = FS.GetExtension(path);

            if (false) // ext != AllExtensions.tiff
            {
                Bitmap bmp = new Bitmap(path);
                imageFile = GetImageFile(bmp).ToArray();
            }
            else
            {
                imageFile = File.ReadAllBytes(path);
            }

            var text = TessearctHelper.ParseText(tesseractPath, imageFile, lang).Trim(); ;
            return text;
        }

        private static byte[] GetImageFile(Bitmap bmp)
        {
            MemoryStream ms = new MemoryStream();
            
            bmp.Save(ms, ImageFormat.Tiff);
            return ms.ToArray();
        }

        private static string GetTesseractPath()
        {
            string tesseractPath;
            var solutionDirectory = string.Empty;
            //solutionDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).FullName;
            solutionDirectory = @"d:\Documents\BitBucket\How-to-use-tesseract-ocr-4.0-with-csharp\";

            tesseractPath = solutionDirectory + @"tesseract-master.1153";
            return tesseractPath;
        }

        public static void Test()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var testFiles = FS.GetFiles(@"d:\Documents\BitBucket\How-to-use-tesseract-ocr-4.0-with-csharp\samples\a\");
            var a = new TesseractArgs { lang = TessearactLang.ces, inputFiles = testFiles, writingOnConsole = true, outputFiles = null };
            ProcessFiles(a);

            stopwatch.Stop();
            Console.WriteLine("Duration: " + stopwatch.Elapsed);
            Console.WriteLine("Press enter to continue...");
            Console.ReadLine();

            string output = string.Empty;
            var tempOutputFile = Path.GetTempPath() + Guid.NewGuid();
            var tempImageFile = Path.GetTempFileName();
        }

        public static void ProcessFiles(TesseractArgs a)
        {
            var tesseractPath = GetTesseractPath();

            var maxDegreeOfParallelism = Environment.ProcessorCount;
            Parallel.ForEach(a.inputFiles, new ParallelOptions { MaxDegreeOfParallelism = maxDegreeOfParallelism },
                (fileName) =>
                {
                    var imageFile = File.ReadAllBytes(fileName);
                    var text = ParseText(tesseractPath, imageFile, a.lang.ToString());

                    if (a.outputFiles != null)
                    {
                        TF.SaveFile(text, a.outputFiles[fileName]);
                    }

                    if (a.writingOnConsole)
                    {
                        Console.WriteLine("File:" + fileName + "\n" + text + "\n");
                    }
                });
        }

        static Type type = typeof(TessearctHelper);

        private static string ParseText(string tesseractPath, byte[] imageFile, params string[] lang)
        {
            foreach (var item in lang)
            {
                if (item == "enu")
                {
                    ThrowExceptions.Custom(Exc.GetStackTrace(), type, Exc.CallingMethod(), "Use right eng, not enu!!!");
                    return null;
                }
            }

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
                    "/" + "c tesseract.exe" + " " +
                    // Image file.
                    tempImageFile + AllStrings.space +
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
                    ThrowExceptions.Custom(Exc.GetStackTrace(), type, Exc.CallingMethod(),"Error. Tesseract stopped with an error code" + " " + "" + " " + process.ExitCode);
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
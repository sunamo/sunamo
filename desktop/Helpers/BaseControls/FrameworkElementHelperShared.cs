using sunamo;
using sunamo.Essential;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

public partial class FrameworkElementHelper{
    private static string SaveScreenshot(Visual target, string fn)
    {
        string appName = ThisApp.Name;
        string projectName = ThisApp.Project;
        fn = PathToScreenshot(fn, appName, projectName);

        Rect bounds = VisualTreeHelper.GetDescendantBounds(target);

        RenderTargetBitmap renderTarget = new RenderTargetBitmap((Int32)bounds.Width, (Int32)bounds.Height, 96, 96, PixelFormats.Pbgra32);

        DrawingVisual visual = new DrawingVisual();

        using (DrawingContext context = visual.RenderOpen())
        {
            VisualBrush visualBrush = new VisualBrush(target);
            context.DrawRectangle(visualBrush, null, new Rect(new Point(), bounds.Size));
        }

        renderTarget.Render(visual);
        PngBitmapEncoder bitmapEncoder = new PngBitmapEncoder();
        bitmapEncoder.Frames.Add(BitmapFrame.Create(renderTarget));
        using (Stream stm = File.Create(fn))
        {
            FS.CreateUpfoldersPsysicallyUnlessThere(fn);
            bitmapEncoder.Save(stm);

            // Cant be, otherwise could be visible on another screenshot
            //ThisApp.SetStatus(TypeOfMessage.Success, "File written to " + fn);
        }

        return fn;
    }

    /// <summary>
    /// If A1 will be fw, will set Margin
    /// </summary>
    /// <param name = "o"></param>
    /// <param name = "allSides"></param>
    public static void SetMargin(object o, double allSides)
    {
        var fw = (FrameworkElement)o;
        if (fw != null)
        {
            fw.Margin = new Thickness(allSides, allSides, allSides, allSides);
        }
    }

public static void SetMargin3(object o, double allSides)
    {
        var fw = (FrameworkElement)o;
        if (fw != null)
        {
            fw.Margin = new Thickness(allSides, allSides, allSides, allSides);
        }
    }

public static void SetAll3Widths(FrameworkElement fe, double w)
    {
        fe.Width = fe.MaxWidth = fe.MinWidth = w;
    }

public static void CreateBitmapFromVisual(object o, RoutedEventArgs e)
    {
        Visual target = null;
        string fn = null;
        UserControl uc = null;

        target = (Window)WpfApp.mp;

        //if (saveSingle.HasValue)
        //{
            //if (saveSingle.Value)
            //{
                fn = WpfApp.mp.actual.GetType().Name;
                SaveScreenshot(target, fn);

                var modeType = target.GetType().Assembly.GetTypes().Where(d => d.Name == "Mode").Single();
                var names = Enum.GetNames(modeType);
                var names_ctor = Enum.GetNames(typeof(MainWindow_Ctor.Mode));

        var skipThese = CA.ToList<string>("MoveToPa", "SearchCodeElements");

                bool anotherSetMode = false;

                foreach (var item in names)
                {
                string _item = item.ToString();
                    if (!names_ctor.Contains(_item))
                    {
                    
                if (!anotherSetMode)
                {
                    if (WpfApp.mp.ModeString == _item)
                    {
                        anotherSetMode = true;
                        
                    }
                    continue;
                }
                if (CA.IsEqualToAnyElement<string>( _item, skipThese ))
                {
                    continue;
                }

                        try
                        {
                            WpfApp.mp.SetMode(item);
                        //var iuc = (IUserControl)WpfApp.mp.actual;
                        break;

                            //iuc.uc_Loaded(null, null);
                            //FrameworkElementHelper.CreateBitmapFromVisual(null, null);
                        }
                        catch (Exception)
                        {
                        }
                    }
                //}
            //}
            //else
            //{
                

            //    saveSingle = false;
            //}
        }
    }

public static string PathToScreenshot(string fn, string appName, string projectName)
    {
        fn = FS.Combine(@"d:\vs\" + appName, projectName, FolderConsts.screenshots, fn + ".png");
        return fn;
    }
}
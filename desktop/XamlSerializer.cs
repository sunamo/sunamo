using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;
namespace desktop
{
    public class XamlSerializer
    {
static Type type = typeof(XamlSerializer);
        
        Window w = null;
        string path = null;
        /// <summary>
        /// Is Used from Name, A1 is only for showing Exception
        /// </summary>
        /// <param name="nameWindow"></param>
        /// <param name="w"></param>
        public XamlSerializer( Window w)
        {
            var name = w.GetType().Name;
            //ThrowExceptions.NameIsNotSetted(Exc.GetStackTrace(),type, "ctor", nameWindow, w.Name);
            this.w = w;
            path = AppData.ci.GetFile(AppFolders.Controls, name);
            w.Loaded += new RoutedEventHandler(MainWindow_Loaded);
            w.Closing += new CancelEventHandler(MainWindow_Closing);
        }
        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadExternalXaml();
        }
        void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            SaveExternalXaml();
        }
        public void LoadExternalXaml()
        {
            if ( FS.ExistsFile(path))
            {
                using (FileStream stream = new FileStream(path, FileMode.Open))
                {
                    w.Content = XamlReader.Load(stream);
                }
            }
        }
        public void SaveExternalXaml()
        {
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                XamlWriter.Save(w.Content, stream);
            }
        }
    }
}
using sunamo;
using forms.Essential;
using System;
using System.IO;
using System.Windows.Controls;

namespace forms.Essential
{ }
 
// TODO: Later uncomment, now I can't because have ComboBox and sunamo.AppData must be without namespace
//namespace forms
//{
//    /// <summary>
//    /// 
//    /// </summary>
//    public class AppData
//    {
//        static AppData()
//        {
//            //CreateAppFoldersIfDontExists();
//        }

//        public static void LoadFiles(AppFolders ap, string mask, ComboBox cb)
//        {
//            cb.Items.Clear();
//            List<string> files = FS.GetFiles(AppData.ci.GetFolder(ap), mask);
//            files = FS.OnlyNames(files);
//            foreach (var item in files)
//            {
//                cb.Items.Add(item);
//            }

//        }
//    }
        
//}

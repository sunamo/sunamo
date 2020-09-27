using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace desktop
{
    /// <summary>
    /// 
    /// </summary>
    public partial class CyclingImageViewer : UserControl, IStatusBroadcasterAppend
    {
        public StringString OperationAfterEnter = null;
        CyclingCollection<string> imagesPath = new CyclingCollection<string>();
        string act = "";
        public string ActualFile
        {
            get
            {
                return act;
            }
            set
            {
                act = value;
                LoadImage(act);
            }
        }

        public bool IsLoadedAnyImage()
        {
            return imagesPath.c.Count != 0;
        }

        public List<string> AllFiles
        {
            get
            {
                return imagesPath.c;
            }
        }
        public BitmapImage ActualImage = null;
        public bool MakesSpaces
        {
            get
            {
                return imagesPath.MakesSpaces;
            }
            set
            {
                imagesPath.MakesSpaces = value;
            }
        }

        public CyclingImageViewer()
        {
            InitializeComponent();
        }

        public void KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                Before();
                OnNewStatus(sess.i18n(XlfKeys.MovedBackToPhoto) + " " + imagesPath.ToString());
            }
            else if (e.Key == Key.Right)
            {
                Next();
                OnNewStatus(sess.i18n(XlfKeys.MovedForwardToPhoto) + " " + imagesPath.ToString());
            }
            else if (e.Key == Key.Enter)
            {
                string copy = string.Copy(ActualFile);
                string b = OperationAfterEnter.Invoke(ActualFile);
                Next();
                if (b == "success")
                {
                    OnNewStatus("Byl zmenšen obrázek {0} a nastaven obrázek v dalším pořadí - {1} ({{})", FS.GetFileName(copy), FS.GetFileName(ActualFile), imagesPath.ToString());    
                }
                
            }
        }

        public void ClearCollection()
        {
            imagesPath.Clear();
            OnNewStatus(sess.i18n(XlfKeys.ImageCollectionDeleted) + ".");
        }

        public void AddImages(List<string> value)
        {
            if (value.Count > 0)
            {
                imagesPath.AddRange(value);
                ActualFile = imagesPath.SetIretation(0);
            }
            else
            {
                OnNewStatusAppend(sess.i18n(XlfKeys.NoMoreImagesLoadedBecauseTheSpecifiedFolderDidNotContainAnyImages) + ".");
            }
        }

        public void Next()
        {
            ActualFile = imagesPath.Next();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        private void LoadImage(string value)
        {
            ActualImage = new BitmapImage(new Uri(value));
            imgImage.Source = ActualImage;
        }

        public void Before()
        {
            ActualFile = imagesPath.Before();
        }

        public event VoidObjectParamsObjects NewStatus;

        public void OnNewStatus(string s, params object[] p)
        {
            NewStatus(s, p);
        }

        public event VoidObjectParamsObjects NewStatusAppend;

        public void OnNewStatusAppend(string s, params object[] o)
        {
            NewStatusAppend(s, o);
        }
    }
}
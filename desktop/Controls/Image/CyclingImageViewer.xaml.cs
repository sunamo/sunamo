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
    /// Interaction logic for CyclingImageViewer.xaml
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
            return imagesPath.t.Count != 0;
        }

        public List<string> AllFiles
        {
            get
            {
                return imagesPath.t;
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
                OnNewStatus("Bylo přesunuto zpět na fotku " + imagesPath.ToString());
            }
            else if (e.Key == Key.Right)
            {
                Next();
                OnNewStatus("Bylo přesunuto vpřed na fotku " + imagesPath.ToString());
            }
            else if (e.Key == Key.Enter)
            {
                string copy = string.Copy(ActualFile);
                string b = OperationAfterEnter.Invoke(ActualFile);
                Next();
                if (b == "success")
                {
                    OnNewStatus("Byl zmenšen obrázek {0} a nastaven obrázek v dalším pořadí - {1} ({2})", System.IO.Path.GetFileName(copy), System.IO.Path.GetFileName(ActualFile), imagesPath.ToString());    
                }
                
            }
        }

        public void ClearCollection()
        {
            imagesPath.Clear();
            OnNewStatus("Kolekce obrázků byla vymazána.");
        }

        public void AddImages(List<string> value)
        {
            if (value.Count > 0)
            {
                imagesPath.AddRange(value);
                ActualFile = imagesPath.SetIretation(0);
                OnNewStatusAppend("Místo toho bylo načteno {0} nových obrázků.", value.Count);
            }
            else
            {
                OnNewStatusAppend("Nebyly načteny další obrázky, protože zadaná složka žádné obrázky neobsahovala.");
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

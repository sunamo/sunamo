using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using HtmlAgilityPack;

namespace CefSunamo
{
    /// <summary>
    /// Interaction logic for CefBrowser.xaml
    /// </summary>
    public partial class CefBrowser : UserControl, ISunamoBrowser<Control>
    {
        public CefBrowser()
        {
            InitializeComponent();
        }

        public HtmlDocument HtmlDocument
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public Uri Source
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public event VoidVoid CloseButtonClick;
        public event VoidVoid CustomButtonClick;
        public event VoidString LoadCompleted;
        public event VoidT<Control> ShowPopup;

        public Task<HtmlDocument> GetHtmlDocument()
        {
            throw new NotImplementedException();
        }
    }
}

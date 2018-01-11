using sunamo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


public partial class WebBrowserWF : Form
    {
        Uri uri = null;
        bool canGoBack = false;
        bool canGoNext = false;
        List<Uri> lastUri = new List<Uri>();
        int actualIndex = 0;
        public event UriEventHandler CustomButtonClick;
        public event VoidVoid CloseButtonClick;
        string homeAdressWithoutHttp = null;
        public event WebBrowserNavigatedEventHandler LoadCompleted;
        bool reload = false;
        List<bool> backnext = new List<bool>();

        public WebBrowserWF(string TextCustomButton, string homeAdressWithoutHttp)
        {
            InitializeComponent();

        }

        void webView_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
        }

        private void btnBack_Click_1(object sender, EventArgs e)
        {
            if (canGoBack)
            {
                reload = false;
                backnext.Add(false);
                actualIndex--;
                webView.Navigate(lastUri[actualIndex]);
            }
        }

        private void btnNext_Click_1(object sender, EventArgs e)
        {
            if (canGoNext)
            {
                reload = false;
                backnext.Add(true);
                actualIndex++;
                webView.Navigate(lastUri[actualIndex]);
            }
        }

        private void btnReload_Click_1(object sender, EventArgs e)
        {
            reload = true;
            backnext.Add(false);
            webView.Navigate(uri);
        }

        private void btnHome_Click_1(object sender, EventArgs e)
        {
            reload = true;
            backnext.Clear();
            lastUri.Clear();
            backnext.Add(false);
            NavigateHome();
        }

        private void NavigateHome()
        {
            reload = false;
            backnext.Add(false);
            webView.Navigate(new Uri("http://" + homeAdressWithoutHttp));
        }

        private void btnCustom_Click_1(object sender, EventArgs e)
        {
            CustomButtonClick(webView, new UriEventArgs(uri));
        }

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            CloseButtonClick();
        }

        private void txtAddress_KeyUp_1(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                Uri uriOut = null;
                if (Uri.TryCreate(txtAddress.Text, UriKind.Absolute, out uriOut))
                {
                    reload = false;
                    actualIndex++;
                    webView.Navigate(uriOut);
                }
            }
        }
    }


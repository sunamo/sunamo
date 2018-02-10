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

            //this.homeAdressWithoutHttp = homeAdressWithoutHttp;
            //btnBack.Image = DrawingImagesHelper.MsAppx(true, AppPics.Previous);
            //btnNext.Image = DrawingImagesHelper.MsAppx(true, AppPics.Next);
            //btnReload.Image = DrawingImagesHelper.MsAppx(false, AppPics.Reload);
            //btnHome.Image = DrawingImagesHelper.MsAppx(false, AppPics.Home);
            //btnClose.Image = DrawingImagesHelper.MsAppx(false, AppPics.Logout);

            //if (TextCustomButton != "")
            //{
            //    btnCustom.Text = TextCustomButton;
            //    btnCustom.Visible = true;

            //    //btnCustom.Content = ImagesHelper.MsAppx(true, AppPics.BoardPin);
            //}
            //else
            //{
            //    btnCustom.Visible = false;
            //}

            //webView.Navigated += webView_Navigated;
            //NavigateHome();
        }

        void webView_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            //if (!reload)
            //{
            //    uri = e.Url;
            //    txtAddress.Text = uri.ToString();

            //    lastUri.Add(uri);

            //    if (actualIndex != 0)
            //    {
            //        btnBack.Image = DrawingImagesHelper.MsAppx(false, AppPics.Previous);
            //        canGoBack = true;
            //    }
            //    else
            //    {
            //        btnBack.Image = DrawingImagesHelper.MsAppx(true, AppPics.Previous);
            //        canGoBack = false;
            //    }
            //    if (actualIndex == lastUri.Count - 1 || (backnext[backnext.Count - 1] == true && backnext[backnext.Count - 2] == false))
            //    {
            //        btnNext.Image = DrawingImagesHelper.MsAppx(true, AppPics.Next);
            //        canGoNext = false;
            //    }
            //    else
            //    {
            //        btnNext.Image = DrawingImagesHelper.MsAppx(false, AppPics.Next);
            //        canGoNext = true;
            //    }
            //    LoadCompleted(sender, e);
            //}
        }

        //public void EnableCustomButton(bool enable)
        //{
        //    btnCustom.Enabled = enable;
        //    btnCustom.Image = DrawingImagesHelper.MsAppx(!enable, AppPics.BoardPin);
        //}

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


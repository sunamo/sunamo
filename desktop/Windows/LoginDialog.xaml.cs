using sunamo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace desktop
{
    /// <summary>
    /// Interaction logic for LoginDialog.xaml
    /// </summary>
    public partial class LoginDialog : Window
    {
        public string Login
        {
            get
            {
                return txtLogin.Text;
            }
        }

        public string Heslo
        {
            get
            {
                return txtHeslo.Password;
            }
        }

        StorageApplicationData storageApplicationData = StorageApplicationData.NoWhere;
        const string h = "h";
        const string l = "l";
        const string s = "s";
        bool loginClicked = false;
        string iniCredSection = "Cred";

        public LoginDialog(string salt)
        {
            InitializeComponent();
            chbAutoLogin.Checked += chbAutoLogin_Checked;
            chbRememberLogin.Unchecked += chbRememberLogin_Unchecked;
        }

        void chbRememberLogin_Unchecked(object sender, RoutedEventArgs e)
        {
            chbAutoLogin.IsChecked = false;
        }

        

        void chbAutoLogin_Checked(object sender, RoutedEventArgs e)
        {
                chbRememberLogin.IsChecked = true;
            //}
        }

        public LoginDialog(string salt, StorageApplicationData storageApplicationData)
            : this(salt) 
        {
            this.storageApplicationData = storageApplicationData;
            if (storageApplicationData == StorageApplicationData.Registry)
            {
                string salt2 = RA.ReturnValueString(s);
                if (salt2 == "")
                {
                    RA.WriteToKeyString(s, salt);
                    salt2 = salt;
                }

                string encryptedH = RA.ReturnValueString(h);
                if (encryptedH != "")
                {
                    this.txtHeslo.Password = Unsafe.ToInsecureString(ProtectedDataHelper.DecryptString(salt2, encryptedH));
                }
                
                this.txtLogin.Text = RA.ReturnValueString(l);
                
                
            }
            else if (storageApplicationData == StorageApplicationData.TextFile)
            {
                //IniFile ini = IniFile.InStartupPath();
                string salt2 = TF.ReadFile( AppData.GetFile(AppFolders.Settings, "s.txt"));
                if (salt2 == "")
                {
                    TF.SaveFile(salt, AppData.GetFile(AppFolders.Settings, "s.txt"));
                    salt2 = salt;
                }

                string encryptedH = TF.ReadFile( AppData.GetFile(AppFolders.Settings, "h.txt"));
                if (encryptedH != "")
                {
                    this.txtHeslo.Password = Unsafe.ToInsecureString(ProtectedDataHelper.DecryptString(salt2, encryptedH));
                }

                this.txtLogin.Text = TF.ReadFile( AppData.GetFile(AppFolders.Settings, "l.txt"));
            }
            else if (storageApplicationData == StorageApplicationData.Config)
            {
                ThrowExceptionConfigNotSupported();
            }
            else if(storageApplicationData == StorageApplicationData.NoWhere)
            {
                // Nedělej nic, uživatel si nepřeje ukládat credentials
            }
            else
            {
                ThrowExceptions.NotImplementedCase(MethodBase.GetCurrentMethod(), "");
            }

            if (txtLogin.Text != "")
            {
                this.chbRememberLogin.IsChecked = txtLogin.Text != "";
                this.chbAutoLogin.IsChecked = txtHeslo.Password != "";
            }
            else
            {
                this.chbRememberLogin.IsChecked = false;
                this.chbAutoLogin.IsChecked = false;
            }
        }

        private static void ThrowExceptionConfigNotSupported()
        {
            throw new NotImplementedException("Ukládání nastavení do app.config nebo web.config zatím není podporováno");
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            base.OnClosing(e);

            DialogResult = loginClicked;
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (storageApplicationData == StorageApplicationData.Registry)
            {
                if ((bool)chbRememberLogin.IsChecked)
                {
                    RA.WriteToKeyString(l, this.txtLogin.Text);
                    if ((bool)this.chbAutoLogin.IsChecked)
                    {
                        RA.WriteToKeyString(h, ProtectedDataHelper.EncryptString(RA.ReturnValueString(s), Unsafe.ToSecureString( this.txtHeslo.Password)));
                    }
                    else
                    {
                        RA.WriteToKeyString(h, "");
                    }
                }
                else
                {
                    RA.WriteToKeyString(h, "");
                    RA.WriteToKeyString(l, "");
                }
            }
            else if (storageApplicationData == StorageApplicationData.TextFile)
            {
                if ((bool)chbRememberLogin.IsChecked)
                {
                    TF.SaveFile(this.txtLogin.Text, AppData.GetFile(AppFolders.Settings, "l.txt"));
                    if ((bool)this.chbAutoLogin.IsChecked)
                    {
                        TF.SaveFile( ProtectedDataHelper.EncryptString(TF.ReadFile(AppData.GetFile(AppFolders.Settings, "s.txt")), Unsafe.ToSecureString(this.txtHeslo.Password)), AppData.GetFile(AppFolders.Settings, "h.txt"));
                    }
                    else
                    {
                        TF.SaveFile("", AppData.GetFile(AppFolders.Settings, "h.txt"));
                    }
                }
                else
                {
                    TF.SaveFile("", AppData.GetFile(AppFolders.Settings, "l.txt"));
                    TF.SaveFile("", AppData.GetFile(AppFolders.Settings, "h.txt"));
                }
            }
            else if (storageApplicationData == StorageApplicationData.Config)
            {
                ThrowExceptionConfigNotSupported();
            }
            if (storageApplicationData != StorageApplicationData.NoWhere)
            {
                if (txtLogin.Text.Trim() != "" && txtHeslo.Password.Trim() != "")
                {
                    loginClicked = true;
                    Close();
                }
                else
                {
                    loginClicked = false;
                }
            }
            else
            {
                loginClicked = true;
                Close();
            }
        }

        private void btnForgetLoginAndPassword_Click(object sender, RoutedEventArgs e)
        {
            txtLogin.Text = "";
            txtHeslo.Password = "";
            if (storageApplicationData == StorageApplicationData.Config)
            {
                ThrowExceptionConfigNotSupported();
            }
            else if (storageApplicationData == StorageApplicationData.Registry)
            {
                RA.WriteToKeyString(l, "");
                RA.WriteToKeyString(h, "");
            }
            else if (storageApplicationData == StorageApplicationData.NoWhere)
            {
                // Nedělej nic, data nebyly nikde uloženy
            }
            else if (storageApplicationData == StorageApplicationData.TextFile)
            {
                TF.SaveFile("", AppData.GetFile(AppFolders.Settings, "l.txt"));
                TF.SaveFile("", AppData.GetFile(AppFolders.Settings, "h.txt"));
            }
            else
            {
                ThrowExceptions.NotImplementedCase(MethodBase.GetCurrentMethod(), "");
            }
        }

        private void btnForgetPassword_Click(object sender, RoutedEventArgs e)
        {
            txtHeslo.Password = "";
            if (storageApplicationData == StorageApplicationData.Config)
            {
                ThrowExceptionConfigNotSupported();
            }
            else if (storageApplicationData == StorageApplicationData.Registry)
            {
                RA.WriteToKeyString(h, "");
            }
            else if (storageApplicationData == StorageApplicationData.TextFile)
            {
                TF.SaveFile("", AppData.GetFile(AppFolders.Settings, "h.txt"));
            }
            else if (storageApplicationData == StorageApplicationData.NoWhere)
            {
                // Nedělej nic, data nebyly nikde uloženy
            }
            else
            {
                ThrowExceptions.NotImplementedCase(MethodBase.GetCurrentMethod(), "");
            }
        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

namespace desktop.Controls.Input
{
    /// <summary>
    /// Interaction logic for LoginDialog.xaml
    /// </summary>
    public partial class LoginDialog : UserControl, IUserControlWithSizeChange
    {
      

        static Type type = typeof(Type);
        public List<TextBlock> tbc;
        public List<CheckBox> chbc;
        public List<TextBox> txtc;
        public List<Button> btnc;
        public List<SunamoPasswordBox> spbc;
        public List<Grid> gridc;
        //public List<PasswordBox> pwbc;

        private void LoginDialog_Loaded(object sender, RoutedEventArgs e)
        {
            txtHeslo.Init(true);

            SizeChanged += LoginDialog_SizeChanged;

            tbc = CA.ToList<TextBlock>(tbLogin, tbPw);
            chbc = CA.ToList<CheckBox>(chbAutoLogin, chbRememberLogin);
            txtc = CA.ToList<TextBox>(txtLogin);
            btnc = CA.ToList<Button>(btnForgetLoginAndPassword, btnForgetPassword, btnLogin);
            // txtHeslo cant be, is around gridSpb
            // is empty - cant processed
            //spbc = CA.ToList<SunamoPasswordBox>();
            gridc = CA.ToList<Grid>(gridSpb);
            //pwbc = CA.ToList<PasswordBox>(txtHeslo);

            ResourceDictionaryStyles.Padding10(tbc);
            ResourceDictionaryStyles.Margin10(chbc);
            ResourceDictionaryStyles.Margin10(txtc);
            ResourceDictionaryStyles.Margin10(btnc);
            
            //ResourceDictionaryStyles.Margin10(spbc);
            ResourceDictionaryStyles.Margin10(gridc);

            txtHeslo.txtPassword.HorizontalAlignment = HorizontalAlignment.Stretch;
            txtHeslo.spShowPassword.HorizontalAlignment = HorizontalAlignment.Stretch;


            //ResourceDictionaryStyles.Margin(10,pwbc);
        }

        private void LoginDialog_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            // All in OnSizeChanged
        }

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
        string title = null;
        public string Title => title;

        public bool? DialogResult
        {
            set
            {
                if (ChangeDialogResult != null)
                {
                    ChangeDialogResult(value);
                }
            }
        }

        public string LoginEnigma;
        public string PwEnigma;

        StorageApplicationData storageApplicationData = StorageApplicationData.NoWhere;
        const string h = "h";
        const string l = "l";
        const string s = "s";
        bool loginClicked = false;
        string iniCredSection = sess.i18n(XlfKeys.Cred);

        /// <summary>
        /// A1 = RandomHelper.RandomString(10)
        /// Set also CryptDelegates from shared.unsafe
        /// 
        /// This must be private
        /// </summary>
        private LoginDialog(string salt)
        {
            InitializeComponent();
            chbAutoLogin.Checked += chbAutoLogin_Checked;
            chbRememberLogin.Unchecked += chbRememberLogin_Unchecked;

            tbLogin.Text = sess.i18n(XlfKeys.Login) + AllStrings.cs2;
            tbPw.Text = sess.i18n(XlfKeys.Password) + AllStrings.cs2;
            chbRememberLogin.Content = sess.i18n(XlfKeys.RememberLogin);
            chbAutoLogin.Content = sess.i18n(XlfKeys.AutoLogin);

            btnLogin.Content = sess.i18n(XlfKeys.Login);
            btnForgetPassword.Content = sess.i18n(XlfKeys.ForgetPassword);
            btnForgetLoginAndPassword.Content = sess.i18n(XlfKeys.ForgetLoginAndPassword);

            Loaded += LoginDialog_Loaded;
        }

        void chbRememberLogin_Unchecked(object sender, RoutedEventArgs e)
        {
            chbAutoLogin.IsChecked = false;
        }

        void chbAutoLogin_Checked(object sender, RoutedEventArgs e)
        {
            chbRememberLogin.IsChecked = true;
        }

        public CryptDelegates cryptDelegates = null;

        public event VoidBoolNullable ChangeDialogResult;

        /// <summary>
        /// Set also CryptDelegates from shared.unsafe
        /// </summary>
        /// <param name="salt"></param>
        /// <param name="storageApplicationData"></param>
        public LoginDialog(string salt, StorageApplicationData storageApplicationData, CryptDelegates c)
            : this(salt)
        {
            cryptDelegates = c;

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
                    this.txtHeslo.Password = cryptDelegates.decryptString(salt2, encryptedH);
                }

                this.txtLogin.Text = RA.ReturnValueString(l);


            }
            else if (storageApplicationData == StorageApplicationData.TextFile)
            {
                //IniFile ini = IniFile.InStartupPath();
                string salt2 = TF.ReadFile(AppData.ci.GetFile(AppFolders.Settings, "s.txt"));
                if (salt2 == "")
                {
                    TF.SaveFile(salt, AppData.ci.GetFile(AppFolders.Settings, "s.txt"));
                    salt2 = salt;
                }

                string encryptedH = TF.ReadFile(AppData.ci.GetFile(AppFolders.Settings, "h.txt"));
                if (encryptedH != "")
                {
                    this.txtHeslo.Password = cryptDelegates.decryptString(salt2, encryptedH);
                }

                this.txtLogin.Text = TF.ReadFile(AppData.ci.GetFile(AppFolders.Settings, "l.txt"));
            }
            else if (storageApplicationData == StorageApplicationData.Config)
            {
                ThrowExceptionConfigNotSupported();
            }
            else if (storageApplicationData == StorageApplicationData.NoWhere)
            {
                // Nedělej nic, uživatel si nepřeje ukládat credentials
            }
            else
            {
                ThrowExceptions.NotImplementedCase(Exc.GetStackTrace(), type, Exc.CallingMethod(), storageApplicationData);
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
            ThrowExceptions.Custom(Exc.GetStackTrace(), type, Exc.CallingMethod(), sess.i18n(XlfKeys.SavingSettingsToAppConfigOrWebConfigIsNotYetSupported));
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            //if (cryptDelegates != null)
            //{
            //    LoginEnigma = cryptDelegates.encryptString(null, txtLogin.Text);
            //    PwEnigma = cryptDelegates.encryptString(null, txtHeslo.Password);
            //}

            if (storageApplicationData == StorageApplicationData.Registry)
            {
                if ((bool)chbRememberLogin.IsChecked)
                {
                    RA.WriteToKeyString(l, this.txtLogin.Text);
                    if ((bool)this.chbAutoLogin.IsChecked)
                    {
                        RA.WriteToKeyString(h, cryptDelegates.encryptString(RA.ReturnValueString(s), this.txtHeslo.Password));
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
                    TF.SaveFile(this.txtLogin.Text, AppData.ci.GetFile(AppFolders.Settings, "l.txt"));
                    if ((bool)this.chbAutoLogin.IsChecked)
                    {
                        TF.SaveFile(cryptDelegates.encryptString(TF.ReadFile(AppData.ci.GetFile(AppFolders.Settings, "s.txt")), this.txtHeslo.Password), AppData.ci.GetFile(AppFolders.Settings, "h.txt"));
                    }
                    else
                    {
                        TF.SaveFile("", AppData.ci.GetFile(AppFolders.Settings, "h.txt"));
                    }
                }
                else
                {
                    TF.SaveFile("", AppData.ci.GetFile(AppFolders.Settings, "l.txt"));
                    TF.SaveFile("", AppData.ci.GetFile(AppFolders.Settings, "h.txt"));
                }
            }
            else if (storageApplicationData == StorageApplicationData.Config)
            {
                ThrowExceptionConfigNotSupported();
            }
            else //if (storageApplicationData != StorageApplicationData.NoWhere)
            {
                // Musím nastavit loginClicked na true nebo false. false je znamení že musím hned zobrazovat v selling dialog 
                if (txtLogin.Text.Trim() != "" && txtHeslo.Password.Trim() != "")
                {
                    loginClicked = true;

                }
                else
                {
                    loginClicked = false;
                }
            }
            DialogResult = true;
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
                TF.SaveFile("", AppData.ci.GetFile(AppFolders.Settings, "l.txt"));
                TF.SaveFile("", AppData.ci.GetFile(AppFolders.Settings, "h.txt"));
            }
            else
            {
                ThrowExceptions.NotImplementedCase(Exc.GetStackTrace(), MethodBase.GetCurrentMethod(), "", storageApplicationData);
            }

            // For sure set loginClicked for default value
            loginClicked = false;
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
                TF.SaveFile("", AppData.ci.GetFile(AppFolders.Settings, "h.txt"));
            }
            else if (storageApplicationData == StorageApplicationData.NoWhere)
            {
                // Nedělej nic, data nebyly nikde uloženy
            }
            else
            {
                ThrowExceptions.NotImplementedCase(Exc.GetStackTrace(), MethodBase.GetCurrentMethod(), "", storageApplicationData);
            }

            // For sure set loginClicked for default value
            loginClicked = false;
        }

        public void Init()
        {
            title = sess.i18n(XlfKeys.EnterLoginCredentials);
        }

        public void Accept(object input)
        {
            ThrowExceptions.NotImplementedMethod(Exc.GetStackTrace(), type, Exc.CallingMethod());
        }

        public void FocusOnMainElement()
        {
            // Nemůžu tu nastavovat, FocusOnMainElement se volá automaticky, tím pádem se nastaví žluté obtažení ale nefunguje kurzor. Když kliknu do txtPassword mají zvýraznění oba 2
            //txtLogin.Focus();
        }

        public void OnSizeChanged(DesktopSize e)
        {
            txtHeslo.OnSizeChanged(new DesktopSize(e.Width, e.Height));

        }
    }
}
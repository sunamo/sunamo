using sunamo;
using sunamo.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace apps.Popups
{
    public sealed partial class LoginDialog : UserControl, IPopupResponsive, IPopupDialogResult
    {
        public event VoidBoolNullable ChangeDialogResult;
        public static string LoadedPassword = null;
        public static string LoadedLogin = null;
        StorageApplicationData storageApplicationData = StorageApplicationData.NoWhere;
        const string h = "h";
        const string l = "l";
        const string s = "s";
        bool loginClicked = false;
        string iniCredSection = "Cred";
        string salt = "";

        private LoginDialog()
        {
            this.InitializeComponent();
        }

        private void OnClickOK(object sender, RoutedEventArgs e)
        {
            if (ChangeDialogResult != null)
            {
                ChangeDialogResult(true);
            }
        }

        private void OnClickCancel(object sender, RoutedEventArgs e)
        {
            if (ChangeDialogResult != null)
            {
                ChangeDialogResult(false);
            }
        }

        public Brush PopupBorderBrush
        {
            set { border.BorderBrush = value; }
        }

        public Brush BackgroundBrush
        {
            set { border.Background = value; }
        }

        public Size MaxContentSize
        {
            get
            {
                return FrameworkElementHelper.GetMaxContentSize(this);
            }
            set
            {
                FrameworkElementHelper.SetMaxContentSize(this, value);
            }
        }

        public static string li
        {
            get
            {
                return LoadedLogin;
                
            }
        }

        public static string pw
        {
            get
            {
                return LoadedPassword;
            }
        }

        public  string Login
        {
            get
            {
                return txtLogin.Text;
            }
        }

        public  string Password
        {
            get
            {
                return txtHeslo.Password;
            }
        }

        /// <summary>
        /// Instead this use OnClickOk and OnClickCancel methods
        /// </summary>
        public bool? DialogResult
        {
            set
            {
                throw new NotImplementedException();
            }
        }
        
        public LoginDialog(string salt) : this()
        {
            this.salt = salt;
            chbAutoLogin.Checked += chbAutoLogin_Checked;
            chbRememberLogin.Unchecked += chbRememberLogin_Unchecked;
        }

        public LoginDialog(string salt, StorageApplicationData storageApplicationData, Brush borderBrush)
            : this(salt)
        {
            this.storageApplicationData = storageApplicationData;
            this.PopupBorderBrush = borderBrush;

            Initialize();
        }

        void chbRememberLogin_Unchecked(object sender, RoutedEventArgs e)
        {
            chbAutoLogin.IsChecked = false;
        }

        void chbAutoLogin_Checked(object sender, RoutedEventArgs e)
        {
            chbRememberLogin.IsChecked = true;
        }


        static StorageFile loginSf;
        static StorageFile passwordSf;


        /// <summary>
        /// Load login and password from file
        /// </summary>
        /// <param name="salt"></param>
        /// <returns></returns>
        public async static Task GetLoginAndPassword(string salt)
        {

            // Load password
            IBuffer encryptedH = await FileIO.ReadBufferAsync(passwordSf);
            if (encryptedH != null)
            {
                LoadedPassword = await ProtectedDataHelper.ToInsecureString(encryptedH);
            }

            // Načtu už. jméno
            LoadedLogin = await TF.ReadFile(loginSf);
        }

        /// <summary>
        /// Initiate controls
        /// </summary>
        /// <returns></returns>
        public async Task Initialize()
        {
            if (storageApplicationData == StorageApplicationData.TextFile)
            {
                await GetLoginAndPassword(salt);
                this.txtLogin.Text = Login;
                this.txtHeslo.Password = LoadedPassword;
            }
            else if (storageApplicationData == StorageApplicationData.NoWhere)
            {
                // Do nothing, user don't want save credentials
            }
            else
            {
                ThrowExceptionSavingConfigInOtherWayIsntSupportedInWindowsStoreAppsNotSupported();
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

        /// <summary>
        /// Get files to read
        /// </summary>
        /// <returns></returns>
        private static async Task<StorageFile[]> GetStorageFiles()
        {
            StorageFile[] vr = new StorageFile[3];
            vr[0] = await AppData.GetFile(AppFolders.Settings, "l.txt");
            vr[1] = await AppData.GetFile(AppFolders.Settings, "s.txt");
            vr[2] = await AppData.GetFile(AppFolders.Settings, "h.txt");
            return vr;
        }

        private static void ThrowExceptionSavingConfigInOtherWayIsntSupportedInWindowsStoreAppsNotSupported()
        {//Ukládání nastavení jinde než do textového souboru zatím není podporováno ve Windows Store Apps
            throw new NotImplementedException("Saving into other way than text file isn't supported in Windows Store apps yet");
        }



        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (storageApplicationData == StorageApplicationData.TextFile)
            {
                StorageFile loginSf, saltSf, passwordSf;
                StorageFile[] sfa = await GetStorageFiles();
                loginSf = sfa[0];
                saltSf = sfa[1];
                passwordSf = sfa[2];

                if ((bool)chbRememberLogin.IsChecked)
                {
                    await TF.SaveFile(this.txtLogin.Text, await AppData.GetFile(AppFolders.Settings, "l.txt"));
                    if ((bool)this.chbAutoLogin.IsChecked)
                    {
                        ProtectedDataHelper.SaveSecureToDisc(await TF.ReadFile(saltSf), this.txtHeslo.Password, passwordSf);
                    }
                    else
                    {
                        await TF.SaveFile("",passwordSf);
                    }
                }
                else
                {
                    await TF.SaveFile("", loginSf);
                    await TF.SaveFile("", passwordSf);
                }
            }
            else
            {
                ThrowExceptionSavingConfigInOtherWayIsntSupportedInWindowsStoreAppsNotSupported();
            }

            if (storageApplicationData != StorageApplicationData.NoWhere)
            {
                if (txtLogin.Text.Trim() != "" && txtHeslo.Password.Trim() != "")
                {
                    ChangeDialogResult(true);
                }
                else
                {
                    ChangeDialogResult(false);
                }
            }
            else
            {
                ChangeDialogResult(true);
            }
        }

        private async void btnForgetLoginAndPassword_Click(object sender, RoutedEventArgs e)
        {
            txtLogin.Text = "";
            txtHeslo.Password = "";
            if (storageApplicationData == StorageApplicationData.NoWhere)
            {
                // Nedělej nic, data nebyly nikde uloženy
            }
            else if (storageApplicationData == StorageApplicationData.TextFile)
            {
                StorageFile loginSf, saltSf, passwordSf;
                StorageFile[] sfa = await GetStorageFiles();
                loginSf = sfa[0];
                saltSf = sfa[1];
                passwordSf = sfa[2];
                await TF.SaveFile("", loginSf);
                await TF.SaveFile("", passwordSf);
            }
            else
            {
                ThrowExceptionSavingConfigInOtherWayIsntSupportedInWindowsStoreAppsNotSupported();
            }
        }

        private async void btnForgetPassword_Click(object sender, RoutedEventArgs e)
        {
            txtHeslo.Password = "";
            if (storageApplicationData == StorageApplicationData.TextFile)
            {
                StorageFile loginSf, saltSf, passwordSf;
                StorageFile[] sfa = await GetStorageFiles();
                loginSf = sfa[0];
                saltSf = sfa[1];
                passwordSf = sfa[2];
                await TF.SaveFile("", passwordSf);
            }
            else if (storageApplicationData == StorageApplicationData.NoWhere)
            {
                // Nedělej nic, data nebyly nikde uloženy
            }
            else
            {
                ThrowExceptionSavingConfigInOtherWayIsntSupportedInWindowsStoreAppsNotSupported();
            }
        }

        public void ApplyColorTheme(ColorTheme ct)
        {
            ColorThemeHelper.ApplyColorTheme(border, ct);
        }

        private void btnCloseDialog_Click(object sender, RoutedEventArgs e)
        {
            ChangeDialogResult(null);
        }
    }
}

using apps.Popups;
using sunamo.Essential;
using sunamo.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace apps
{
    public class SunamoCzLoginManager
    {
        public bool IsUserLogined()
        {
            return idUser != -1 && sc != "";
        }

        static IEssentialMainPageWithLogin mp = null;
        public event VoidVoid LoginSuccessful;
        public event VoidVoid LoginFailed;
        /// <summary>
        /// Bude empty pokud uživatel nebude přihlášený. Nicméně radši vždy kontroluj na idUser
        /// </summary>
        public string sc = "";
        /// <summary>
        /// Bude -1 pokud uživatel nebude přihlášený
        /// </summary>
        public int idUser = -1;

        void OnLoginSuccessful()
        {
            if (LoginSuccessful != null)
            {
                LoginSuccessful();
            }
        }

        void OnLoginFailed()
        {
            if (LoginFailed != null)
            {
                LoginFailed();
            }
        }

        public async Task PairLoginAndPassword(IEssentialMainPageWithLogin mp2, bool zobrazitLoginDialog, string login, string password, Brush borderBrush, JavascriptSerialization js, string adresaWebu)
        {
            mp = mp2;
            string sc2 = "";
            int idUser2 = -1;

            if (!string.IsNullOrEmpty(login) && !string.IsNullOrEmpty(password))
            {
                if (zobrazitLoginDialog)
                {
                    mp.loginDialog = new LoginDialog(RandomHelper.RandomStringWithoutSpecial(10), StorageApplicationData.TextFile, borderBrush);
                    mp.loginDialog.ChangeDialogResult += LoginDialog_ChangeDialogResult;

                    mp.popup = PopupHelper.GetPopupResponsive( mp.loginDialog, true, borderBrush);
                    

                    return;
                }
                string s = await DoWebRequest(adresaWebu + "PairLoginAndPassword.ashx?Login=" +  login + "&Pw=" +  password);

                if (s.StartsWith("{"))
                {
                    ExternalLoginResult elr = js.Deserialize<ExternalLoginResult>(s);
                    sc2 = elr.Sc;
                    idUser2 = elr.IdUser;
                    OnLoginSuccessful();
                    await apps.Essential.ThisApp.SetStatus(TypeOfMessage.Success, RL.GetString("SuccessfullyLoginedToSpace") + adresaWebu);
                }
                else
                {

                    await apps.Essential.ThisApp.SetStatus(AspNet.IsStatusMessage(s), s);
                    OnLoginFailed();
                }
            }
            else
            {
                if (zobrazitLoginDialog)
                {
                    mp.loginDialog = new LoginDialog(RandomHelper.RandomStringWithoutSpecial(10), StorageApplicationData.TextFile, borderBrush);
                    mp.loginDialog.ChangeDialogResult += LoginDialog_ChangeDialogResult;

                    mp.popup = PopupHelper.GetPopupResponsive(mp.loginDialog, true, borderBrush);
                    
                    return;
                }
                
                //await ThisApp.SetStatus(TypeOfMessage.Error, RL.GetString( "PasswordOrLoginNotEntered"));
                OnLoginFailed();
            }
            sc = sc2;
            idUser = idUser2;
        }

        private async Task<string> DoWebRequest(string uri)
        {
            return await HttpClientHelper.GetResponseText(uri, HttpMethod.Get, new HttpRequestData());
        }

        private async void LoginDialog_ChangeDialogResult(bool? b)
        {
            if (b.HasValue)
            {
                if (b.Value)
                {
                    await mp.Dialog_ClickOK(null, null);
                }
                else
                {
                    mp.Dialog_ClickCancel(null, null);
                }
            }
            else
            {
                mp.Dialog_ClickClose(null, null);
            }
        }
    }
}

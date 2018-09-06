using apps.PopupsNoResponsive;
using sunamo;
using sunamo.Essential;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls.Primitives;

namespace apps
{
    class DW
    {
        private static async Task<bool> ShowMessageDialog(string text, string typZpravy)
        {
            MessageDialog md = new MessageDialog(text, ThisApp.Name + " - " + typZpravy);
            md.Options = MessageDialogOptions.None;
            var d = await md.ShowAsync();
            return true;
        }

        public static async Task<bool> ErrorAwait(string text)
        {
            return await ShowMessageDialog(text, RL.GetString( "Error"));
        }

        public static async Task<bool> WarningAwait(string text)
        {
            return await ShowMessageDialog(text, RL.GetString( "Warning"));
        }

        public static async Task<bool> InfoAwait(string text)
        {
            return await ShowMessageDialog(text, RL.GetString( "Information"));
        }

        public static async Task<bool> SuccessAwait(string text)
        {
            return await ShowMessageDialog(text, RL.GetString( "Success"));
        }



    }
}

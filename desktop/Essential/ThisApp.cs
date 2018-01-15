using sunamo;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace desktop.Essential
{
    public  class ThisApp : shared.Essential.ThisApp
    {
        public static string SQLExpressInstanceName()
        {
            return Environment.MachineName + "\\SQLExpress";
        }

#if DEBUG
        public static void WriteDebug(string v)
        {
            Debug.WriteLine(v);
        }
#endif

        public async static Task SetStatus(TypeOfMessage st, string status, bool alsoLb = true)
        {
#if DEBUG
            WriteDebug(status);
#endif
            //var tsc = new TaskCompletionSource();
            if (alsoLb)
            {
                if (beforeMessage == status)
                {
#if DEBUG
                    //Debugger.Break();
#else
                    return;
#endif
                }
            }
            beforeMessage = status;
            status = DTHelper.TimeToStringAngularTime(DateTime.Now) + " " + status;
            LogMessageAbstract<Color, string> lmn = null;
            if (alsoLb)
            {
                lmn = await mp.Logger.Add(st, status);
                //await lbLogs.RefreshLb(ls.messagesActualSession2);
            }
            else
            {
                lmn = await new LogMessage().Initialize(DateTime.Now, st, status, mp.Logger.GetBackgroundBrushOfTypeOfMessage(st));
            }
            await mp.SetStatus(lmn, alsoLb);

        }

        public async static Task SetStatusToTextBlock(TypeOfMessage st, string status)
        {
            Color fg = mp.Logger.GetForegroundBrushOfTypeOfMessage(st);
            if (st == TypeOfMessage.Error || st == TypeOfMessage.Warning)
            {
                await SetForeground(tbLastErrorOrWarning, fg);
                await SetText(tbLastErrorOrWarning, status);
            }
            else
            {
                await SetForeground(tbLastOtherMessage, fg);
                await SetText(tbLastOtherMessage, status);
            }
        }

        public async static Task SetForeground(TextBlock tbLastOtherMessage, Color color)
        {
            await cd.InvokeAsync( () =>
            {
                tbLastOtherMessage.Foreground = new SolidColorBrush(color);
            }, cdp);
        }

        public async static Task SetText(TextBlock lblStatusDownload, string status)
        {

            await cd.InvokeAsync(() =>
            {
                lblStatusDownload.Text = status;
            }, cdp);
        }

        static string beforeMessage = "";
        public static IEssentialMainPage mp = null;
        public static TextBlock tbLastErrorOrWarning = null;
        public static TextBlock tbLastOtherMessage = null;
        public static Dispatcher cd = null;
        public static DispatcherPriority cdp = DispatcherPriority.Normal;
        public static Langs l = Langs.en;
    }
}

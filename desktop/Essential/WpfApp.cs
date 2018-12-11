using shared;
using sunamo;
using sunamo.Essential;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;


namespace desktop.Essential
{
    public  class WpfApp 
    {
        static DependencyProperty[] props = new DependencyProperty[] { TextBlock.ForegroundProperty, TextBlock.TextProperty };

        public static string SQLExpressInstanceName()
        {
            return Environment.MachineName;
        }

#if DEBUG
        public static void WriteDebug(string v)
        {
            Debug.WriteLine(v);
        }
#endif


        // TODO: Pozustatek z apps, nikde ani neloguje, rozhodnout co s ni
//        public  static void SetStatus(TypeOfMessage st, string status, bool alsoLb = true)
//        {
//#if DEBUG
//            WriteDebug(status);
//#endif
//            //var tsc = new TaskCompletionSource();
//            if (alsoLb)
//            {
//                if (beforeMessage == status)
//                {
//#if DEBUG
//                    //Debugger.Break();
//#else
//                    return;
//#endif
//                }
//            }
//            beforeMessage = status;
//            status = DTHelper.TimeToStringAngularTime(DateTime.Now) + " " + status;
//        }

            

        public static void SaveReferenceToTextBlockStatus(bool restore, TextBlock tbTemporaryLastErrorOrWarning, TextBlock tbTemporaryLastOtherMessage)
        {
            if (restore)
            {
                tbLastErrorOrWarning = tbLastErrorOrWarningSaved;
                tbLastOtherMessage = tbLastOtherMessageSaved;
            }
            else
            {
                tbLastErrorOrWarningSaved = tbLastErrorOrWarning;
                tbLastOtherMessageSaved = tbLastOtherMessage;
            }

            if (!restore)
            {
                tbLastErrorOrWarning = tbTemporaryLastErrorOrWarning;
                tbLastOtherMessage = tbTemporaryLastOtherMessage;
            }
        }

        #region Sync
        private static void SetStatus(TypeOfMessage st, string status)
        {
            status = DateTime.Now.ToShortTimeString() + " " + status;
            Color fg = StatusHelper.GetForegroundBrushOfTypeOfMessage(st);

            if (st == TypeOfMessage.Error || st == TypeOfMessage.Warning)
            {
                SetForeground(tbLastErrorOrWarning, fg);
                SetText(tbLastErrorOrWarning, status);

                if (lbLogsErrors != null)
                {
                    TextBlock txt = DependencyObjectHelper.CreatedWithCopiedValues<TextBlock>(tbLastErrorOrWarning, props);
                    txt.ToolTip = tbLastErrorOrWarning.Text;
                    lbLogsErrors.Children.Insert(0, txt);
                }
            }
            else
            {
                SetForeground(tbLastOtherMessage, fg);
                SetText(tbLastOtherMessage, status);

                if (lbLogsOthers != null)
                {
                    TextBlock txt = DependencyObjectHelper.CreatedWithCopiedValues<TextBlock>(tbLastOtherMessage, props);
                    txt.ToolTip = tbLastOtherMessage.Text;
                    lbLogsOthers.Children.Insert(0, txt);
                }
            }
        }

        public static void EnableDesktopLogging(bool v)
        {
            if (v)
            {
                ThisApp.StatusSetted += ThisApp_StatusSetted;
            }
            else
            {
                ThisApp.StatusSetted -= ThisApp_StatusSetted;
            }
        }

        private static void ThisApp_StatusSetted(TypeOfMessage t, string message)
        {
            SetStatus(t, message);
        }

        private static void SetForeground(TextBlock tbLastOtherMessage, Color color)
        {
            tbLastOtherMessage.Foreground = new SolidColorBrush(color);
        }

        private static void SetText(TextBlock lblStatusDownload, string status)
        {
            lblStatusDownload.Text = status;
        } 
        #endregion

        #region Async
        // TODO: Rename to SetStatusAsync and merge with commented method SetStatus here
        public async static Task SetStatusToTextBlock(TypeOfMessage st, string status)
        {
            Color fg = Colors.Black;

            if (st == TypeOfMessage.Error || st == TypeOfMessage.Warning)
            {
                await SetForegroundAsync(tbLastErrorOrWarning, fg);
                await SetTextAsync(tbLastErrorOrWarning, status);
            }
            else
            {
                await SetForegroundAsync(tbLastOtherMessage, fg);
                await SetTextAsync(tbLastOtherMessage, status);
            }
        }

        public async static Task SetForegroundAsync(TextBlock tbLastOtherMessage, Color color)
        {
            await cd.InvokeAsync(() =>
           {
               tbLastOtherMessage.Foreground = new SolidColorBrush(color);
           }, cdp);
        }

        public async static Task SetTextAsync(TextBlock lblStatusDownload, string status)
        {

            await cd.InvokeAsync(() =>
            {
                lblStatusDownload.Text = status;
            }, cdp);
        } 
        #endregion


        public static IEssentialMainPage mp = null;
        public static TextBlock tbLastErrorOrWarning = null;
        public static TextBlock tbLastOtherMessage = null;
        static TextBlock tbLastErrorOrWarningSaved = null;
        static TextBlock tbLastOtherMessageSaved = null;
        static StackPanel lbLogsOthers = null;
        static StackPanel lbLogsErrors = null;
        public static Dispatcher cd = null;
        public static DispatcherPriority cdp = DispatcherPriority.Normal;

        public static void SaveReferenceToLogsStackPanel(StackPanel _lbLogsOthers, StackPanel _lbLogsErrors)
        {
            lbLogsErrors = _lbLogsErrors;
            lbLogsOthers = _lbLogsOthers;
        }
    }
}

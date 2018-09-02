using sunamo;
using sunamo.LoggerAbstract;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace apps.Essential
{
    public  class ThisApp : sunamo.Essential.ThisApp
    {


        public static IEssentialMainPage mp = null;
        public static bool withTime = true;
        
        public static CoreDispatcher cd = null;
        public static CoreDispatcherPriority cdp = CoreDispatcherPriority.Normal;


#if DEBUG
        public static void WriteDebug(string v)
        {
            Debug.WriteLine(v);
        }
#endif

        public async static Task SetStatus( TimeMeter tm, string operation)
        {
            operation = tm.Stop(operation);
            if (operation != null)
            {
                await SetStatus( TypeOfMessage.Information, operation, true);
            }
        }

        public static TextBlock tbLastOtherMessage;
        public static TextBlock tbLastErrorOrWarning;

        public async static Task SetStatusToTextBlock(TypeOfMessage st, string status)
        {
            Color fg = mp.lsg.GetForegroundBrushOfTypeOfMessage(st);
            if (st == TypeOfMessage.Error || st == TypeOfMessage.Warning)
            {
                await SetForeground(tbLastErrorOrWarning,  fg);
                await SetText(tbLastErrorOrWarning, status);
            }
            else
            {
                await SetForeground(tbLastOtherMessage, fg);
                await SetText(tbLastOtherMessage, status);
            }
        }



        public async static Task SetStatus(TypeOfMessage st, string status, bool alsoLb = true)
        {
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
//            LogMessageAbstract<Color, StorageFile> lmn = null;
//            if (alsoLb)
//            {
//                lmn = await mp.lsg.Add(st, status);
//                //await lbLogs.RefreshLb(ls.messagesActualSession2);
//            }
//            else
//            {
//                lmn = await new LogMessage().Initialize(DateTime.Now, st, status, mp.lsg.GetBackgroundBrushOfTypeOfMessage(st));
//            }
//            await mp.SetStatus(lmn, alsoLb);

        }

        public async static Task SetForeground(TextBlock tbLastOtherMessage, Color color)
        {
            await cd.RunAsync(cdp, () =>
            {
                tbLastOtherMessage.Foreground = new SolidColorBrush(color);
            });
        }

        public async static Task SetText(TextBlock lblStatusDownload, string status)
        {
            
            await cd.RunAsync(cdp, () =>
            {
                lblStatusDownload.Text = status;
            });
        }

        public async static void SetIsEnabled(Control uie, bool? v)
        {
            if (v.HasValue)
            {
                await cd.RunAsync(cdp, () =>
                {
                    uie.IsEnabled = v.Value;
                });
            }
        }

        public async static void SetVisibility(UIElement uie, Visibility? v)
        {
            if (v.HasValue)
            {
                await cd.RunAsync(cdp, () =>
                {
                    uie.Visibility = v.Value;
                });
            }
        }

        public async static void SetBorderBrush(Border borderDoManagePhotogallery, SolidColorBrush Value)
        {
            if(Value != null)
            {
                await cd.RunAsync(cdp, () =>
                {
                    borderDoManagePhotogallery.BorderBrush = Value;
                });
            }
        }

        /// <summary>
        /// POkud se nepodaří získat vrátí null
        /// </summary>
        /// <param name="borderDoManagePhotogallery"></param>
        /// <returns></returns>
        public async static Task<SolidColorBrush> GetBorderBrush(Border borderDoManagePhotogallery)
        {
            SolidColorBrush vr = null;
             await cd.RunAsync(cdp, () =>
            {
                vr = (SolidColorBrush)borderDoManagePhotogallery.BorderBrush;
            });
            return vr;
        }
    }
}

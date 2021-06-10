using System;
using System.Collections;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml.Linq;
using sunamo.Essential;

public partial class WpfApp{
    /// <summary>
    ///  for this + registers handlers in App attention, on whole yesterday and partially today I spend many time where is erros because Exceptions windows is disabled 
    /// </summary>
    static bool breakAt = true;
    static bool handled = true;
    static bool initialized = false;
    static bool attached = false;

     

    public static void Init()
    {
#if MB
        ShowMb("Init WpfApp");
#endif
        if (!initialized)
        {
            CA.dCount = new Func<IEnumerable, int>(r => WpfApp.DispatcherAction<IEnumerable, int>(dCount, r));
            //CA.dFirstOrNull = new Func<IEnumerable, object>(r => WpfApp.DispatcherAction<IEnumerable, object>(dFirstOrNull, r));

#if MB
            ShowMb("inside if");
#endif
            initialized = true;
           
            Application.Current.ShutdownMode = ShutdownMode.OnLastWindowClose;

#if MB
            ShowMb("attach in release");
            AttachHandlers();
#endif

            //throw new Exception("avbc");

            // change ! by needs
            if (!Debugger.IsAttached)
            {
#if MB
                ShowMb("Debugger wasnt attached");
#endif
                AttachHandlers();
            }
            else
            {
#if MB
                ShowMb("Debugger was attached, no exceptions handlers is attached");
#endif
            }
        }
    }

    #region MyRegion
    private static int dCount(IEnumerable arg)
    {
        int i = 0;
        foreach (var item in arg)
        {
            i++;
        }

        return i;
    }

    //private static object dFirstOrNull(IEnumerable arg)
    //{
    //    int i = 0;
    //    foreach (var item in arg)
    //    {
    //        return item;
    //    }

    //    return null;
    //}

    private static T2 DispatcherAction<T1, T2>(Func<T1, T2> count, T1 t1)
    {
        T2 result = WpfApp.cd.Invoke(() => count(t1));
        return result;
    }
    #endregion

    private static void AttachHandlers()
    {
        if (!attached)
        {
#if MB
            ShowMb("!attached");
#endif 
            attached = true;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            // Is handled also in Current_DispatcherUnhandledException , then will be opened two windows
            //Dispatcher.UnhandledException += Dispatcher_UnhandledException;
            Application.Current.DispatcherUnhandledException += Current_DispatcherUnhandledException;
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;

#if MB
            ShowMb("!attached finished");
#endif 
        }
        else
        {
#if MB
            ShowMb("attached !!!");
#endif 
        }
    }

    

    static void DebuggerIsAttached()
    {
        //ShowMb("DebuggerIsAttached");
        //Debugger.Break();
    }

    private static bool IsSomethingNull(string handler)
    {
#if MB
        // Here must be ShowMb, not TranslateDictionary.ShowMb, which could not be attached here!
        ShowMb("IsSomethingNull " + handler);
#endif
        WpfApp.cd = Application.Current.Dispatcher;
        WpfApp.cdp = System.Windows.Threading.DispatcherPriority.Normal;

        //return false;

        //StringBuilder sb = new StringBuilder();
        ShowMb(DesktopNotTranslateAble.EnteringIsSomethingNull);
        bool vr = false;
        bool vr2 = false;

        if (WpfApp.cd == null)
        {
            vr = true;
        }

        if (PD.delShowMb == null)
        {
            vr2 = true;
        }

        if (vr || vr2)
        {
            bool run = false;
            if (vr)
            {
                ShowMb(DesktopNotTranslateAble.WpfAppCdWasNull);
                run = true;
            }
            if (vr2)
            {
                ShowMb(DesktopNotTranslateAble.PDDelShowMbWasNull);
                run = true;
            }

            Exception ex = new Exception();

            try
            {
                ShowMb(DesktopNotTranslateAble.EmptyTryBlock);
            }
            catch (Exception ex2)
            {
                ex = ex2;
                ShowMb(DesktopNotTranslateAble.CatchBlockFromEmptyTryBlock);
            }

            //sb.AppendLine("Is my computer");
            //run = WindowsSecurityHelper.IsMyComputer();

            if (run)
            {
                 string nl = Environment.NewLine;
                var err = handler + nl + Exc.GetStackTrace() +nl + Exceptions.TextOfExceptions(ex);
                Debug.WriteLine(err);
                
#if MB
                ShowMb(handler + " " + DesktopNotTranslateAble.SomethingIsNullProbablyWpfAppCdIntoClipboardAndDebugWasCopiedStacktrace + ".");
#endif
                WpfApp.ShowMb(err);
                Clipboard.SetText(err);
            }
            
        }
        else
        {
#if MB
            ShowMb(handler + " Everything is ok");
#endif
        }

        return vr;
    }

    

    /// <summary>
    /// A2 = name of calling method (like Current_DispatcherUnhandledException)
    /// </summary>
    /// <param name="e"></param>
    /// <param name="n"></param>
    public static void ShowExceptionWindow(EventArgs e, string n)
    {
        var dump = WindowHelper.ShowExceptionWindow(e, n);

        WriterEventLog.WriteToMainAppLog(n + Environment.NewLine + dump, System.Diagnostics.EventLogEntryType.Error, Exc.CallingMethod());
    }










    private static void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
    {
        if (IsSomethingNull("TaskScheduler_UnobservedTaskException"))
        {
            return;
        }

        var typeExc = e.Exception.GetType();
        var t = typeExc.Name;

        //https://stackoverflow.com/a/7883087/9327173
        e.SetObserved();

        //DebugLogger.Instance.WriteLine(t);

        WpfApp.cd.Invoke(() =>
        {
            if (!Debugger.IsAttached)
            {
                ShowExceptionWindow(e, "TaskScheduler_UnobservedTaskException");
            }
            else { if (breakAt) { DebuggerIsAttached(); } }
        }
        );
    }

    /// <summary>
    /// 3
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private static void Current_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
    {
        // cd je null
        if (IsSomethingNull("Current_DispatcherUnhandledException"))
        {
            return;
        }

        e.Handled = handled;
        WpfApp.cd.Invoke(() =>
        {
            if (!Debugger.IsAttached)
            {
                e.Handled = true;
                ShowExceptionWindow(e, "Current_DispatcherUnhandledException");
            }
            else { if (breakAt) { DebuggerIsAttached(); } }

        }
        );
    }

    /// <summary>
    /// 2
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
        if (IsSomethingNull("CurrentDomain_UnhandledException"))
        {
            return;
        }

        WpfApp.cd.Invoke(() =>
        {
            if (!Debugger.IsAttached)
            {
                ShowExceptionWindow(e, "CurrentDomain_UnhandledException");
            }
            else { if (breakAt) { DebuggerIsAttached(); } }
        }
        );
    }
}
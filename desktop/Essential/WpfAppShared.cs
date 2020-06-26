using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
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
        //MessageBox.Show("Init WpfApp");
        if (!initialized)
        {
            //MessageBox.Show("inside if");
            initialized = true;
            Application.Current.ShutdownMode = ShutdownMode.OnLastWindowClose;

#if !DEBUG
            //MessageBox.Show("attach in release");
            AttachHandlers();
#endif

            // change ! by needs
            if (!Debugger.IsAttached)
            {
                //MessageBox.Show("Debugger wasnt attached");
                AttachHandlers();
            }
        }
    }

    private static void AttachHandlers()
    {
        if (!attached)
        {
            attached = true;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            // Is handled also in Current_DispatcherUnhandledException , then will be opened two windows
            //Dispatcher.UnhandledException += Dispatcher_UnhandledException;
            Application.Current.DispatcherUnhandledException += Current_DispatcherUnhandledException;
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;
        }
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

    static void DebuggerIsAttached()
    {
        //MessageBox.Show("DebuggerIsAttached");
        //Debugger.Break();
    }

    private static bool IsSomethingNull(string handler)
    {
        bool vr = false;

        if (WpfApp.cd == null)
        {
            vr = true;
        }

        if (vr)
        {
            var d = Exc.GetStackTrace();
            try
            {
                if (WindowsSecurityHelper.IsMyComputer())
                {
                    Clipboard.SetText(d);
                }
                
            }
            catch (Exception)
            {
                /*
Application: UsedCarComparing.exe
Framework Version: v4.0.30319
Description: The process was terminated due to an unhandled exception.
Exception Info: System.Runtime.InteropServices.COMException
   at System.Runtime.InteropServices.Marshal.ThrowExceptionForHRInternal(Int32, IntPtr)
   at System.Windows.Clipboard.CriticalSetDataObject(System.Object, Boolean)
   at System.Windows.Clipboard.SetDataInternal(System.String, System.Object)
   at System.Windows.Clipboard.SetText(System.String, System.Windows.TextDataFormat)
   at System.Windows.Clipboard.SetText(System.String)
   at WpfApp.IsSomethingNull(System.String)
   at WpfApp.Current_DispatcherUnhandledException(System.Object, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs)
   at System.Windows.Threading.Dispatcher.CatchException(System.Exception)
   at System.Windows.Threading.Dispatcher.CatchExceptionStatic(System.Object, System.Exception)
   at System.Windows.Threading.ExceptionWrapper.CatchException(System.Object, System.Exception, System.Delegate)
   at System.Windows.Threading.ExceptionWrapper.TryCatchWhen(System.Object, System.Delegate, System.Object, Int32, System.Delegate)
   at System.Windows.Threading.Dispatcher.LegacyInvokeImpl(System.Windows.Threading.DispatcherPriority, System.TimeSpan, System.Delegate, System.Object, Int32)
   at MS.Win32.HwndSubclass.SubclassWndProc(IntPtr, Int32, IntPtr, IntPtr)
                */
            }
            MessageBox.Show(handler +  " Something is null (probably WpfApp.cd). Into clipboard was copied stacktrace.");
        }
        else
        {
            //MessageBox.Show(handler + " Everything is ok");
        }

        return vr;
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

    public static void ShowExceptionWindow(EventArgs e, string n)
    {
        var d = WindowHelper.ShowExceptionWindow(e, n);


        WriterEventLog.WriteToMainAppLog(n + Environment.NewLine + d, System.Diagnostics.EventLogEntryType.Error, Exc.CallingMethod());
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
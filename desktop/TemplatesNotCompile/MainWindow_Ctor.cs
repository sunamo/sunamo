using ConfigurableWindow.Shared;
using desktop.AwesomeFont;
using desktop.UserControls;
using sunamo;
using sunamo.Essential;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

public partial class MainWindow_Ctor : Window, IEssentialMainWindow, IHideToTray, IConfigurableWindow
{
    static Type type = typeof(MainWindow_Ctor);
    Mode mode = Mode.Empty; public string ModeString { get => mode.ToString(); }
    EmptyUC emptyUC = null;
    LogUC logUC = null;
    UserControl _actual = new UserControl(); public UserControl actual { get => _actual; set => _actual = value; }
    IUserControl userControl = null;
    IUserControlWithMenuItemsList userControlWithMenuItems;
    IUserControlClosing userControlClosing;
    IKeysHandler keysHandler;
    List<MenuItem> previouslyRegisteredMenuItems = new List<MenuItem>();
    dynamic Instance = null;

    #region MyRegion
    MenuItem miGenerateScreenshot = null;
    MenuItem miAlwaysOnTop = null;
    Grid grid;
    MenuItem miUC;

    #region Implicitly in Window
    dynamic Dispatcher = null;
    TextBlock tbLastErrorOrWarning;
    TextBlock tbLastOtherMessage;
    string Title = null;
    #endregion
    #endregion

    public ApplicationDataContainer data { get; set; }
    public ConfigurableWindowWrapper configurableWindowWrapper { get; set; }
    public bool CancelClosing { get; set; }
    public WindowWithUserControl windowWithUserControl { get; set; }

    public MainWindow_Ctor()
    {
        // In ctor can be only InitializeComponent, all everything must be in Loaded. Use template as exists in MainWindow_Ctor

        Loaded += MainWindow_Loaded;
    }

    public void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
        /*
             * 1) Clipboard and Xlf
             * 2) Initialize logging
             * 3) Initialize base properties of every app
             * 4) Initialize helpers of app
             * 5) Set modes
             * 6) Attach handlers
             */

        #region 1) ThisApp.Name, Check for already running, required conditions, Clipboard, AppData and Xlf
        string appName = "";
        ThisApp.Name = appName;
        WpfApp.Init();
#if !DEBUG
            if (PH.IsAlreadyRunning(ThisApp.Name))
            {
                SetCancelClosing(false);
            
                MessageBox.Show("Please use app in tray");
                
                Close();
            }
#endif



        ClipboardHelper.Instance = ClipboardHelperWin.Instance;
        AppData.ci.CreateAppFoldersIfDontExists();
        //CryptHelper.ApplyCryptData(CryptHelper.RijndaelBytes.Instance, CryptDataWrapper.rijn);

        XlfResourcesH.SaveResouresToRLSunamo();
        #endregion

        // All initialization must be after #region Initialize base properties of every app 

        #region 2) Initialize logging
#if DEBUG
        sunamo.Essential.InitApp.TemplateLogger = sunamo.Essential.DebugTemplateLogger.Instance;
        sunamo.Essential.InitApp.Logger = DebugLogger.Instance;
        sunamo.Essential.InitApp.TypedLogger = sunamo.Essential.TypedDebugLogger.Instance;
#else
        //sunamo.Essential.InitApp.TemplateLogger = sunamo.Essential.SunamoTemplateLogger.Instance;
        // sunamo.Essential.InitApp.Logger = SunamoLogger.Instance;
        // sunamo.Essential.InitApp.TypedLogger = TypedSunamoLogger.Instance;

        // For console always write only to console. When I need write also to event log, must do it separately
        //CmdApp.EnableConsoleLogging(true);
        // InitApp.Logger = ConsoleLogger.Instance;
        //     InitApp.TemplateLogger = ConsoleTemplateLogger.Instance;
        //     InitApp.TypedLogger = TypedConsoleLogger.Instance;
#endif

        WpfApp.EnableDesktopLogging(true);

        // 1st LogUC must be before Empty
        SetMode(Mode.LogUC);
        WpfApp.SaveReferenceToTextBlockStatus(false, tbLastErrorOrWarning, tbLastOtherMessage);
        WpfApp.SaveReferenceToLogsStackPanel(logUC.lbLogsOthers.lbLogs, logUC.lbLogsErrors.lbLogs);
        #endregion

        #region 3) Initialize base properties of app
        Instance = this;

        WpfApp.cd = Dispatcher;

        // Important to shut down app
        WpfApp.mp = this;

        WriterEventLog.CreateMainAppLog(ThisApp.Name);

        // Must be initialize right after data set
        // ApplicationDataContainer Must be before configurableWindowWrapper
        data = new ApplicationDataContainer();
        Name = ThisApp.Name;
        data.Add(this);

#if !DEBUG
            if (PH.IsAlreadyRunning(ThisApp.Name))
            {
                SetCancelClosing(false);
                //MessageBox.Show("Please use app in tray");
                Close();
            }
#endif

        configurableWindowWrapper = new ConfigurableWindowWrapper(this, miAlwaysOnTop);


        Title = appName;
        #endregion

        #region 4) Initialize helpers, SQL of app

        #endregion

        #region 5) Set modes


        // 2nd Edit only in #if
        SetMode(Mode.Empty);

#if DEBUG
        //3rd in debug show uc
        SetMode(Mode.Empty);
#endif
        #endregion

        #region 6) Attach handlers
        PreviewKeyDown += MainWindow_PreviewKeyDown;
        #endregion

        #region 7) Notify icon
        //SetCancelClosing(true);
        //// .ico must be set up to Resource
        //NotifyIconHelper.Create(SetCancelClosing, ResourcesH.ci.GetStream(ThisApp.Name + ".ico"), delegate (object sen, EventArgs args)
        //{
        //    this.Show();
        //    this.BringIntoView();
        //    var beforeTopMost = this.Topmost;
        //    this.Topmost = true;
        //    this.Topmost = beforeTopMost;

        //    // WindowState should be loaded from configuration
        //    //this.WindowState = WindowState.Normal;
        //}, forms.ContextMenuHelper.Get(null), WpfApp.Shutdown);
        #endregion

        #region 8) App-specific testing

        #endregion

        #region 9) Set up UI of app
        miGenerateScreenshot.Header = "Generate screenshot"; 
        miGenerateScreenshot.Click += FrameworkElementHelper.CreateBitmapFromVisual; if (!RuntimeHelper.IsAdminUser())
        {
            miGenerateScreenshot.Visibility = Visibility.Collapsed;
        }

        SetAwesomeIcons();
        #endregion

        #region 10) Login, Load data

        #endregion
    }

    #region MyRegion
    // Only for working with notify, but always insert block with userControlClosing
    //    protected override void OnClosing(CancelEventArgs e)
    //    {
    //#if !DEBUG
    //        e.Cancel = GetCancelClosing();
    //        WindowState = WindowState.Minimized;
    //#endif
    //        CheckMenuItemTopMost();

    //if (userControlClosing != null)
    //    {
    //        userControlClosing.OnClosing();
    //    }

    //        base.OnClosing(e);
    //    } 
    #endregion

    private void MainWindow_PreviewKeyDown(object sender, KeyEventArgs e)
    {
        if (keysHandler != null)
        {
            if (keysHandler.HandleKey(e))
            {
                e.Handled = true;
            }
            else if (e.Key == Key.PrintScreen)
            {
                actual.MakeScreenshot();
            }
            else
            {

            }


        }
    }

    void MiAlwaysOnTop_Click(object sender, RoutedEventArgs e)
    {
        Topmost = miAlwaysOnTop.IsChecked;
        CheckMenuItemTopMost();
    }

    private void CheckMenuItemTopMost(bool? topMost = null)
    {
        if (topMost.HasValue)
        {
            Topmost = topMost.Value;
        }

        miAlwaysOnTop.IsChecked = Topmost;
    }

    protected override void OnSourceInitialized(EventArgs e)
    {
        base.OnSourceInitialized(e);

        ConfigurableWindowHelper.SourceInitialized(configurableWindowWrapper);
    }

    protected override void OnRenderSizeChanged(SizeChangedInfo info)
    {
        base.OnRenderSizeChanged(info);

        ConfigurableWindowHelper.RenderSizeChanged(configurableWindowWrapper);
    }

    public const string awesomeFontPath = "/Fonts/FontAwesome.otf#FontAwesome";

    void SetAwesomeIcons()
    {
        //AwesomeFontControls.SetAwesomeFontSymbol(tbAwesome, "\uf133");
    }

    public IConfigurableWindowSettings CreateSettings()
    {
        return new ConfigurableWindow.Shared.WindowConfigSettings(this, data);
    }

    public void SetMode(object mode2)
    {

        var mode = EnumHelper.Parse<Mode>(mode2.ToString(), Mode.Empty);
        if (userControlClosing != null)
        {
            userControlClosing.OnClosing();
        }

        this.Topmost = false;
        #region After arrange I have to newly unregister
        //if (result != null)
        //{
        //    result.Finished -= Result_Finished;
        //}

        //if (userControlInWindow != null)
        //{
        //    userControlInWindow.ChangeDialogResult -= UserControlInWindow_ChangeDialogResult;
        //}
        #endregion

        this.mode = mode;
        grid.Children.Remove(actual);

        switch (mode)
        {
            #region Shared UC
            case Mode.Empty:
                if (emptyUC == null)
                {
                    emptyUC = new EmptyUC();
                }
                actual = emptyUC;
                break;
            case Mode.LogUC:
                if (logUC == null)
                {
                    logUC = new LogUC();
                }
                actual = logUC;
                break;
            #endregion
            default:
                break;
        }

        // Here I can use (IUserControl) because every have to be IUserControl
        userControl = (IUserControl)actual;
        userControl.Init();

        userControlWithMenuItems = actual as IUserControlWithMenuItemsList;
        userControlClosing = actual as IUserControlClosing;
        keysHandler = actual as IKeysHandler;
        ThrowExceptions.WasNotKeysHandler(Exc.GetStackTrace(),type, Exc.CallingMethod(), userControl.Title, keysHandler);


        #region On start I have to unregister
        previouslyRegisteredMenuItems.ForEach(menuItem => miUC.Items.Remove(menuItem));

        var pMode = "userControlWithMenuItems " + mode;

        if (userControlWithMenuItems != null)
        {
            // keep long name due to copy to new selling apps
            miUC.Visibility = System.Windows.Visibility.Visible;
            miUC.Header = userControl.Title;
            previouslyRegisteredMenuItems = userControlWithMenuItems.MenuItems();
            foreach (var item in previouslyRegisteredMenuItems)
            {
                if (item.Parent != null)
                {
                    ((Menu)item.Parent).Items.Remove(item);
                }
                miUC.Items.Add(item);
            }
            miUC.UpdateLayout();
        }
        else
        {
            miUC.Visibility = System.Windows.Visibility.Collapsed;
        }
        #endregion

        grid.Children.Add(actual);
        Grid.SetRow(actual, 1);

        MainWindow_SizeChanged(null, null);
    }

    private void MainWindow_SizeChanged(object p1, object p2)
    {

    }

    public bool GetCancelClosing()
    {
        if (CancelClosing)
        {
            Hide();
        }
        return CancelClosing;
    }

    public void SetCancelClosing(bool b)
    {
        CancelClosing = b;
    }

    
}
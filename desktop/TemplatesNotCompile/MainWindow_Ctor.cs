using ConfigurableWindow.Shared;
using desktop.UserControls;
using sunamo.Essential;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

public class MainWindow_Ctor : Window, IEssentialMainWindow, IHideToTray, IConfigurableWindow
{
    ConfigurableWindowWrapper configurableWindowWrapper = null;
    
    
    ApplicationDataContainer data = null;
    Mode mode = Mode.Empty;
    EmptyUC emptyUC = null;
    LogUC logUC = null;
    UserControl actual = new UserControl();
    IUserControl userControl = null;
    IUserControlWithMenuItemsList userControlWithMenuItems;
    List<MenuItem> previouslyRegisteredMenuItems = new List<MenuItem>();

    #region MyRegion
    Grid grid;
    MenuItem miUC;
    dynamic Instance = null;
    #region Implicitly in Window
    dynamic Dispatcher = null;
    TextBlock tbLastErrorOrWarning;
    TextBlock tbLastOtherMessage;
    string Title = null;
    #endregion

    public bool CancelClosing { get; set; }
    public WindowWithUserControl windowWithUserControl { get; set; }
    #endregion

    public MainWindow_Ctor()
    {
        // In ctor can be only InitializeComponent, all everything must be in Loaded. Use template as exists in MainWindow_Ctor
    }

    public void MainWindow_Loaded()
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
        CmdApp.EnableConsoleLogging(true);
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

        configurableWindowWrapper = new ConfigurableWindowWrapper(this);


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

        #endregion

        #region 7) Notify icon

        #endregion

        #region 8) App-specific testing

        #endregion

        #region 9) Set up UI of app

        #endregion

        #region 10) Login, Load data

        #endregion
    }

    protected override void OnRenderSizeChanged(SizeChangedInfo info)
    {
        base.OnRenderSizeChanged(info);

        if (configurableWindowWrapper != null)
        {
            if (configurableWindowWrapper._isLoaded && this.WindowState == WindowState.Normal)
            {
                configurableWindowWrapper._settings.WindowSize = this.RenderSize;
            }
        }
    }

    public IConfigurableWindowSettings CreateSettings()
    {
        return new ConfigurableWindow.Shared.WindowConfigSettings(this, data);
    }
    private void SetMode(Mode mode)
    {
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

        #region On start I have to unregister
        previouslyRegisteredMenuItems.ForEach(menuItem => miUC.Items.Remove(menuItem));

        var pMode = "userControlWithMenuItems " + mode;

        if (userControlWithMenuItems != null)
        {
            miUC.Visibility = Visibility.Visible;
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
            miUC.Visibility = Visibility.Collapsed;
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
        return CancelClosing;
    }

    public void SetCancelClosing(bool b)
    {
        CancelClosing = b;
    }

    enum Mode
    {
        // Empty First mode in every app
        Empty,

        // Then Modes of app

        // then shared UC for every app
        LogUC,
        CheckBoxListMode
    }
}


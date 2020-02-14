using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using sunamo;
using sunamo.Interfaces;

namespace desktop.UserControls
{
    /// <summary>
    /// Interaction logic for LogUC.xaml
    /// </summary>
    public partial class LogUC : UserControl, IUserControl, IWindowOpener, IUserControlShared, IKeysHandler
    {
        public LogUC()
        {
            InitializeComponent();

            uc_Loaded(null, null);
        }

        public string Title => "Logs";
        bool initialized = false;
        public WindowWithUserControl windowWithUserControl { get => windowOpenerMain.windowWithUserControl; set => windowOpenerMain.windowWithUserControl = value; }

        IKeysHandler keyHandlerMain = null;
        IEssentialMainWindow mainControl = null;
        IWindowOpener windowOpenerMain = null;

        public IEssentialMainWindow MainControl
        {
            get { return mainControl; }
            set
            {
                mainControl = value;

                if (value is IKeysHandler)
                {
                    keyHandlerMain = (IKeysHandler)value;
                }
                if (value is IWindowOpener)
                {
                    windowOpenerMain = (IWindowOpener)value;
                }
            }
        }

        public bool HandleKey(KeyEventArgs e)
        {
            if (keyHandlerMain != null)
            {
                if (keyHandlerMain.HandleKey(e))
                {
                    //return true;
                }
            }

            return false;
        }

        public void Init()
        {
            if (!initialized)
            {
                initialized = true;


            }
        }

        public void uc_Loaded(object sender, RoutedEventArgs e)
        {
            
        }
    }
}

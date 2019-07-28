using System.Windows.Controls;
using System.Windows.Input;
using sunamo;
using sunamo.Interfaces;

namespace desktop.UserControls
{
    /// <summary>
    /// Interaction logic for LogUC.xaml
    /// </summary>
    public partial class LogUC : UserControl, IUserControl, IWindowOpener, IUserControlShared, IKeysHandler<KeyEventArgs>
    {
        public LogUC()
        {
            InitializeComponent();
        }

        public string Title => "Logs";

        public WindowWithUserControl windowWithUserControl { get => windowOpenerMain.windowWithUserControl; set => windowOpenerMain.windowWithUserControl = value; }

        IKeysHandler<KeyEventArgs> keyHandlerMain = null;
        IEssentialMainWindow mainControl = null;
        IWindowOpener windowOpenerMain = null;

        public IEssentialMainWindow MainControl
        {
            get { return mainControl; }
            set
            {
                mainControl = value;
                //if (RH.IsOrIsDeriveFromBaseClass(typeof(IKeysHandler<KeyEventArgs>), mainControl.GetType()))
                //{

                //}
                if (value is IKeysHandler<KeyEventArgs>)
                {
                    keyHandlerMain = (IKeysHandler<KeyEventArgs>)value;
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

        }
    }
}

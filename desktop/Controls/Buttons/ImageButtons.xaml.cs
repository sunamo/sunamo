using desktop.AwesomeFont;
using sunamo.Essential;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace desktop.Controls.Buttons
{
    /// <summary>
    /// Contains all buttons which show only AwesomeIcon
    /// </summary>
    public partial class ImageButtons : UserControl
    {
        static Type type = typeof(ImageButtons);

        public event VoidString Added;
        EnterOneValueWindow eov = null;
        public event VoidVoid CopyToClipboard;
        public event VoidVoid ClearAll;



        public const string awesomeFontPath = "/Fonts/FontAwesome.otf#FontAwesome";

        async void SetAwesomeIcons()
        {
            // In serie how is written in xaml
            await AwesomeFontControls.SetAwesomeFontSymbol(btnCopyToClipboard, "\uf0c5");
            await AwesomeFontControls.SetAwesomeFontSymbol(btnClear, "\uf00d");
            await AwesomeFontControls.SetAwesomeFontSymbol(btnAdd, "\uf055");
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            ClearAll();
        }

        private void BtnCopyToClipboard_Click(object sender, RoutedEventArgs e)
        {
            CopyToClipboard();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            eov = new EnterOneValueWindow("item to insert");
            eov.enterOneValueUC.ChangeDialogResult += EnterOneValueUC_ChangeDialogResult;
            eov.ShowDialog();
        }

        private void EnterOneValueUC_ChangeDialogResult(bool? b)
        {
            if (b.HasValue && b.Value)
            {
                Added(eov.enterOneValueUC.txtEnteredText.Text);
            }
        }

        /// <summary>
        /// A1 can be VoidVoid or bool(Visibility). 
        /// </summary>
        /// <param name="copyToClipboard"></param>
        /// <param name="clear"></param>
        public ImageButtons()
        {
            InitializeComponent();

            SetAwesomeIcons();
        }

        /// <summary>
        /// 1. handlers RoutedEventHandler directly to A1
        /// 2. bool and set handlers, elements has public FieldModifier
        /// </summary>
        /// <param name="copyToClipboard"></param>
        /// <param name="clear"></param>
        public void Init(object copyToClipboard, object clear, object add)
        {
            SetVisibility(btnCopyToClipboard, copyToClipboard);
            SetVisibility(btnClear, clear);
            SetVisibility(btnAdd, add);
        }

        private void SetVisibility(Button btn, object copyToClipboard)
        {
            string methodName = "SetVisibility";

            if (copyToClipboard == null)
            {
                btn.Visibility = Visibility.Collapsed;
            }
            else
            {
                var t = copyToClipboard.GetType();
                if (t == typeof(bool))
                {
                    UIElementHelper.SetVisibility((bool)copyToClipboard, btn);
                }
                else if (t == typeof(RoutedEventHandler))
                {
                    var d = (RoutedEventHandler)copyToClipboard;
                    btn.Click += d;
                }
                else
                {
                    ThrowExceptions.NotImplementedCase(type, methodName);
                }
            }
        }

        
    }
}

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

namespace desktop
{
    /// <summary>
    /// Interaction logic for DialogLogin2.xaml
    /// </summary>
    public partial class LoginUc : UserControl, IUserControlInWindow
    {
        bool internalSaveLogic = false;
        const string h = "h";
        const string l = "l";

        public LoginUc()
        {
            InitializeComponent();
        }

        /// <summary>
        /// A1 je vhodné tehdy když například pouštím python skripty, ve kterých nemůžu ověřit zda se mi podařilo nalogovat
        /// </summary>
        /// <param name="internalSaveLogic"></param>
        public LoginUc(bool internalSaveLogic)
            : this()
        {
            this.internalSaveLogic = internalSaveLogic;
            if (internalSaveLogic)
            {
                this.txtHeslo.Text = RA.ReturnValueString(h);
                this.txtLogin.Text = RA.ReturnValueString(l);
                this.chbUlozHeslo.IsChecked = this.txtHeslo.Text != "";
            }
        }

        private void btnLetsLogin_Click(object sender, RoutedEventArgs e)
        {
            if (internalSaveLogic)
            {
                RA.WriteToKeyString(h, "");
                RA.WriteToKeyString(l, this.txtLogin.Text);

                if (this.chbUlozHeslo.IsChecked.Value)
                {
                    RA.WriteToKeyString(h, this.txtHeslo.Text);
                }

                if (txtLogin.Text.Trim() != "" && txtHeslo.Text.Trim() != "")
                {
                    DialogResult = true;
                }
                else
                {
                    ChangeDialogResult(false);
                }

            }
            else
            {
                ChangeDialogResult(true);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            ChangeDialogResult( false);
        }

        public bool HaveLoginedData()
        {
            string he = RA.ReturnValueString(h);
            string lo = RA.ReturnValueString(l);

            return he != "" && lo != "";
        }

        public event VoidBoolNullable ChangeDialogResult;


        public bool? DialogResult
        {
            set
            {
                ChangeDialogResult(value);
            }
        }

        public Size UcSize
        {
            get { return this.DesiredSize; }
        }

        public String Login
        {
            get
            {
                return txtLogin.Text;
            }
        }

        public string Heslo
        {
            get
            {
                return txtHeslo.Text;
            }
        }
    }
}

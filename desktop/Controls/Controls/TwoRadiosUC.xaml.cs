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

namespace desktop.Controls.Controls
{
    /// <summary>
    /// Interaction logic for TwoRadiosUC.xaml
    /// </summary>
    public partial class TwoRadiosUC : UserControl
    {
        public static Type type = typeof(TwoRadiosUC);

        public TwoRadiosUC(TwoState addRemove)
        {
            InitializeComponent();

            switch (addRemove)
            {
                case TwoState.TrueFalse:
                    rb1.Content = "True";
                    rb2.Content = "False";
                    break;
                case TwoState.AddRemove:
                    rb1.Content = "Add";
                    rb2.Content = "Remove";
                    break;
                case TwoState.AcceptDecline:
                    rb1.Content = "Accept";
                    rb2.Content = "Decline";
                    break;
                default:
                    break;
            }

            Tag = "TwoRadiosUC";
        }

        public static bool validated
        {
            get
            {
                return SelectFolder.validated;
            }
            set
            {
                TwoRadiosUC.validated = value;
            }
        }

        internal object GetBool()
        {
            if (rb1.IsCheckedSimple())
            {
                return true;
            }
            return false;
        }

        public  void Validate(object tb, TwoRadiosUC control)
        {
            validated = BTS.GetValueOfNullable( rb1.IsChecked) || BTS.GetValueOfNullable( rb2.IsChecked);
        }

        public void Validate(object tbFolder)
        {
            Validate(tbFolder, this);
        }
    }
}

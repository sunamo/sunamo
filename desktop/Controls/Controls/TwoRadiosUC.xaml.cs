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
    /// 
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
                    rb2.Content = sess.i18n(XlfKeys.Remove);
                    break;
                case TwoState.AcceptDecline:
                    rb1.Content = sess.i18n(XlfKeys.Accept);
                    rb2.Content = sess.i18n(XlfKeys.Decline);
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
                return TwoRadiosUC.validated;
            }
            set
            {
                TwoRadiosUC.validated = value;
            }
        }

        public object GetBool()
        {
            if (rb1.IsCheckedSimple())
            {
                return true;
            }
            return false;
        }

        public  bool Validate(object tb, TwoRadiosUC control, ref ValidateData d)
        {
            if (d == null)
            {
                d = new ValidateData();
            }
            validated = BTS.GetValueOfNullable( rb1.IsChecked) || BTS.GetValueOfNullable( rb2.IsChecked);
            return validated;
        }

        public bool Validate(object tbFolder, ref ValidateData d)
        {
            Validate(tbFolder, this, ref d);
            return validated;
        }
    }
}
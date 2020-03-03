using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Interaction logic for EnterValueForm.xaml
    /// </summary>
    public partial class EnterValueForm : Window, IResult
    {
        public EnterValueForm(string whatEnter)
        {
            InitializeComponent();
            enterOneValueUC.Init(whatEnter);
            enterOneValueUC.Finished += EnterOneValueUC_Finished;
        }

        private void EnterOneValueUC_Finished(object o)
        {
            Finished(o);
        }

        public event VoidObject Finished;
    }
}
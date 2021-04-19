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

namespace desktop.Controls.Result
{
    /// <summary>
    /// 
    /// </summary>
    public partial class NoResultsFound : UserControl
    {
        public NoResultsFound()
        {
            InitializeComponent();

            if (string.IsNullOrEmpty(tbNoResultsFound.Text))
            {
                tbNoResultsFound.Text = sess.i18n(XlfKeys.NoResultsFound);
            }
        }
        public NoResultsFound(string what) : this()
        {
            tbNoResultsFound.Text = "No " + what + " found";
        }
    }
}
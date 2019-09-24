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

namespace desktop.Controls.Collections
{
    /// <summary>
    /// Interaction logic for RadioButtonsList.xaml
    /// </summary>
    public partial class RadioButtonsList : UserControl
    {
        public event Action<object> ClickedWithTag; 

        public RadioButtonsList()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Must be ControlInitData[], not object[]
        /// 
        /// </summary>
        /// <param name="contents"></param>
        public void AddRadioButtons(bool addHandler, params ControlInitData[] contents)
        {
            foreach (var content in contents)
            {
                if (addHandler)
                {
                    content.OnClick = RbClicked;
                }
                AddRadioButton(content);
            }
        }

        public void AddRadioButton(ControlInitData d)
        {
            spRbs.Children.Add(RadioButtonHelper.Get(d));
        }

        public void RbClicked(object o, RoutedEventArgs e)
        {
            if (ClickedWithTag != null)
            {
                var rb = (RadioButton)o;
                ClickedWithTag(rb.Tag);
            }
        }
    }
}

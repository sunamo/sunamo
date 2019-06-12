using desktop.Helpers.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// for ChangeDialogResult return always null - must check returned values through via CheckedIndexes()
    /// </summary>
    public partial class CheckBoxListUC : UserControl, IUserControlInWindow
    {
        //public ObservableCollection<ABT<string, bool>> TheList { get; set; }
        public bool? DialogResult { set => throw new NotImplementedException(); }

        public ObservableCollection<CheckBox> chbAdded { get; set; }

        public event VoidBoolNullable ChangeDialogResult;

        public CheckBoxListUC()
        {
            InitializeComponent();
        }

        public void Init(List<string> list)
        { 
            chbAdded = new ObservableCollection<CheckBox>();
            //TheList = new ObservableCollection<ABT<string, bool>>();

            foreach (var item in list)
            {
                var chb = CheckBoxHelper.Get(item);
                chb.Tag = ControlNameGenerator.GetSeries(chb.GetType());
                //chb.Checked += Chb_Click;
                //chb.Unchecked += Chb_Click;
                chbAdded.Add(chb);

                //TheList.Add(new ABT<string, bool>(item, false));
            }

            this.DataContext = this;
        }

        private void Chb_Click(object sender, RoutedEventArgs e)
        {
            
        }

        public IEnumerable<int> CheckedIndexes()
        {
            return CheckBoxHelper.CheckedIndexes(chbAdded);
        }

        public void Accept(object input)
        {
            throw new NotImplementedException();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            s(sender,true);

            ChangeDialogResult(null);
        }

        /// <summary>
        /// Save IsChecked to elements in chbAdded
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="b"></param>
        private void s(object sender, bool b)
        {
            var s = ((FrameworkElement)sender);
            var name = s.Tag;
            var where = chbAdded.Where(d => d.Tag == s.Tag);

            foreach (var item in where)
            {
                item.IsChecked = b;
            }
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            s(sender, false);
            ChangeDialogResult(null);
        }
    }
}

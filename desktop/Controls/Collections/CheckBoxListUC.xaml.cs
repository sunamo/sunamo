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
    /// Interaction logic for CheckBoxListUC.xaml
    /// </summary>
    public partial class CheckBoxListUC : UserControl, IUserControlInWindow
    {
        public ObservableCollection<ABT<string, bool>> TheList { get; set; }
        public bool? DialogResult { set => throw new NotImplementedException(); }

        List<CheckBox> chbAdded = new List<CheckBox>();

        public event VoidBoolNullable ChangeDialogResult;

        public CheckBoxListUC()
        {
            InitializeComponent();
        }

        public void Init(List<string> list)
        {
            TheList = new ObservableCollection<ABT<string, bool>>();

            foreach (var item in list)
            {
                var chb = CheckBoxHelper.Get(item);
                chbAdded.Add(chb);

                TheList.Add(new ABT<string, bool>(item, false));
            }

            this.DataContext = this;
        }

        public IEnumerable<int> CheckedIndexes()
        {
            return CheckBoxHelper.CheckedIndexes(chbAdded);
        }

        public void Accept(object input)
        {
            throw new NotImplementedException();
        }
    }
}

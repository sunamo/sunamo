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
        #region IUserControlInWindow implementation
        public bool? DialogResult { set => ChangeDialogResult(value); }
        public void Accept(object input)
        {

        }
        public event VoidBoolNullable ChangeDialogResult;
        #endregion

        public event Action<object, string, object> CollectionChanged;
        public NotifyChangesCollection<CheckBox> l = null;
        public bool onCheck = true;
        public bool onUnCheck = true;


        public void EventOn(bool onCheck, bool onUnCheck, bool onAdd, bool onRemove, bool onClear)
        {
            this.onCheck = onCheck;
            this.onUnCheck = onUnCheck;
            l.EventOn(onAdd, onRemove, onClear);
        }

        public CheckBoxListUC()
        {
            InitializeComponent();
        }

        /// <summary>
        /// A1 can be null
        /// </summary>
        /// <param name="list"></param>
        public void Init(List<string> list = null)
        {
            l = new NotifyChangesCollection<CheckBox>(this, new ObservableCollection<CheckBox>());
            l.CollectionChanged += L_CollectionChanged;

            

            colButtons.SelectAll += ColButtons_SelectAll;
            colButtons.UnselectAll += ColButtons_UnselectAll;

            lb.ItemsSource = l.l;

            if (list != null)
            {
                foreach (var item in list)
                {
                    AddCheckbox(item);
                }
            }

            this.DataContext = this;

            l.CollectionChanged += L_CollectionChanged1;
        }

        /// <summary>
        /// visible: add, selectAll, deselectAll
        /// </summary>
        public void DefaultButtonsInit()
        {
            colButtons.Init(false, false, new VoidString(ColButtons_Added), true, true);
        }

        public void HideAllButtons()
        {
            colButtons.Init(false, false, false, false, false);
        }

        private void L_CollectionChanged1(object o, string operation, object data)
        {
            if (CollectionChanged != null)
            {
                CollectionChanged(o, operation, data);
            }
        }

        private void L_CollectionChanged(object o, string operation, object data)
        {
            DialogResult = CheckedIndexes().Count() > 0;
        }

        private void ColButtons_UnselectAll()
        {
            SelectAll(false);
        }

        private void ColButtons_SelectAll()
        {
            SelectAll(true);
        }

        void SelectAll(bool b)
        {
            foreach (var item in l.l)
            {
                item.IsChecked = b;
            }
        }

        public void AddCheckbox(string item)
        {
            var chb = CheckBoxHelper.Get(item);
            
            //chb.Checked += Chb_Click;
            //chb.Unchecked += Chb_Click;
            l.l.Add(chb);
        }

        private void ColButtons_Added(string s)
        {
            AddCheckbox(s);
        }

        public IEnumerable<int> CheckedIndexes()
        {
            return CheckBoxHelper.CheckedIndexes(l.l);
        }

        

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            s(sender,true);

            l.OnCollectionChanged(CheckBoxListOperations.Check, sender);
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
            var where = l.l.Where(d => d.Tag == s.Tag);

            foreach (var item in where)
            {
                item.IsChecked = b;
            }
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            s(sender, false);
            l.OnCollectionChanged(CheckBoxListOperations.UnCheck, sender);
        }

        
    }

    public class CheckBoxListOperations
    {
        public const string Check = "Check";
        public const string UnCheck = "UnCheck";
    }
}

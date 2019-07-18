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
    public partial class CheckBoxListUC : UserControl, IUserControlInWindow, IUserControlWithSizeChange
    {
        #region IUserControlInWindow implementation
        public bool? DialogResult
        {
            set
            {
                // because ChangeDialogResult is nowhere use and is set in CheckedIndexes, check for null
                if (ChangeDialogResult != null)
                {
                    ChangeDialogResult(value);
                }
            }
        }

        public string Title => "Check box list";

        public void Accept(object input)
        {

        }

        /// <summary>
        /// For now is usage nowhere
        /// </summary>
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

            Loaded += CheckBoxListUC_Loaded;
        }

        private void CheckBoxListUC_Loaded(object sender, RoutedEventArgs e)
        {
            //OnSizeChanged(new DesktopSize(ActualWidth, ActualHeight));
        }

        /// <summary>
        /// A1 can be null
        /// </summary>
        /// <param name="list"></param>
        public void Init(List<string> list = null, bool defChecked = false)
        {
            colButtons.MaxHeight = 16;

            l = new NotifyChangesCollection<CheckBox>(this, new ObservableCollection<CheckBox>());
            l.CollectionChanged += L_CollectionChanged;

            

            colButtons.SelectAll += ColButtons_SelectAll;
            colButtons.UnselectAll += ColButtons_UnselectAll;

            lb.ItemsSource = l.l;

            if (list != null)
            {
                foreach (var item in list)
                {
                    AddCheckbox(item, defChecked);
                }
            }

            this.DataContext = this;

            l.CollectionChanged += L_CollectionChanged;

            SizeChanged += CheckBoxListUC_SizeChanged;
        }

        private void CheckBoxListUC_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            // Cant be, otherwise set wrong size into checkbox and button will be out of window
            //OnSizeChanged(new DesktopSize( e.NewSize.Width, e.NewSize.Height));
            
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

        
        private void L_CollectionChanged(object o, string operation, object data)
        {
            if (CollectionChanged != null)
            {
                CollectionChanged(o, operation, data);
            }
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

        public void AddCheckbox(string input, bool defChecked)
        {
            var lines = SH.GetLines(input);

            foreach (var item in lines)
            {
                var contents = l.Select(r => r.Content);
                var contentString = CA.ToListString(contents);
                if (CA.IsEqualToAnyElement<object>(item, contentString))
                {
                    continue;
                }
                var chb = CheckBoxHelper.Get(item);
                chb.IsChecked = defChecked;
                //chb.Checked += Chb_Click;
                //chb.Unchecked += Chb_Click;
                l.Add(chb);
            }
        }

        private void ColButtons_Added(string s)
        {
            AddCheckbox(s, true);
        }

        public IEnumerable<int> CheckedIndexes()
        {
            return CheckBoxHelper.CheckedIndexes(l.l);
        }

        public IEnumerable<object> CheckedContent()
        {
            return CheckBoxHelper.CheckedContent(l.l);
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            s(sender,true);

            if (onCheck)
            {
                l.OnCollectionChanged(CheckBoxListOperations.Check, sender); 
            }
        }

        internal List<string> AllContent()
        {
            throw new NotImplementedException();
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
            Tag = sender;
            var where = l.Where(d => d.Tag == s.Tag);

            foreach (var item in where)
            {
                item.IsChecked = b;
            }
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            s(sender, false);
            if (onUnCheck)
            {
                l.OnCollectionChanged(CheckBoxListOperations.UnCheck, sender); 
            }
        }

        public void OnSizeChanged(DesktopSize s)
        {
            var firstButton = colButtons.HeightOfFirstVisibleButton();
            var h = s.Height - firstButton;
            //r0.Height = new GridLength(h);
            lb.Height = lb.MaxHeight = lb.MinHeight = h;
            lb.InvalidateVisual();

            
            
            DebugLogger.Instance.WriteArgs("Height", h, "First button", firstButton, "sp", colButtons.sp.ActualHeight, "colButtons", colButtons.ActualHeight);

        }

        public void Init()
        {
            Init(null, false);
        }
    }

    public class CheckBoxListOperations
    {
        public const string Check = "Check";
        public const string UnCheck = "UnCheck";
    }
}

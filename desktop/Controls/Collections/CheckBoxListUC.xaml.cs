using desktop.Helpers.Controls;
using sunamo.Essential;
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
    /// Is Three controls to checkboxes:
    /// CheckBoxListUC
    /// TwoWayTable
    /// SunamoGridView
    /// 
    /// No one in ListBox/ListView
    /// </summary>
    public partial class CheckBoxListUC : UserControl
        , IUserControlInWindow, IUserControlWithSizeChange
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

        public string Title => RLData.en["CheckBoxList"];

        public void Accept(object input)
        {

        }

        /// <summary>
        /// For now is usage nowhere
        /// </summary>
        public event VoidBoolNullable ChangeDialogResult;
        #endregion

        public int Count => lb.Items.Count;

        /// <summary>
        /// Args are: object sender, string operation, object data
        /// </summary>
        public event Action<object, string, object> CollectionChanged;
        public NotifyChangesCollection<CheckBox> l = null;
        /// <summary>
        /// Whether have raise CollectionChanged after check 
        /// </summary>
        public bool? onCheck = true;
        public bool onUnCheck = true;
        public bool initialized = false;

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
        /// A2 can be null
        /// Into A1.add can be CheckBoxListUC.ColButtons_Added
        /// A1 can be null but then is needed to call DefaultButtonsInit,HideAllButtons, etc.
        /// </summary>
        /// <param name="list"></param>
        public void Init(ImageButtonsInit i, List<string> list = null, bool defChecked = false)
        {
            if (!initialized)
            {
                initialized = true;

                colButtons.MaxHeight = 16;

                if (i != null)
                {
                    colButtons.Init(i);
                }

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

                SizeChanged += CheckBoxListUC_SizeChanged;
            }
        }

        public  List<string> AllContentString()
        {
            var ac = AllContent();
            List<string> result = new List<string>();
            foreach (var item in ac)
            {
                List<TextBlock> textboxes = null; //VisualTreeHelpers.FindDescendents<TextBlock>(item);
                var first = textboxes.First();
                result.Add(first.Text);
            }

            return result;
        }

        public static string ContentOfTextBlock(StackPanel key)
        {
            var first = key.Children.FirstOrNull();
            var tb = first as TextBlock;
            return tb.Text;
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
            colButtons.Init(new ImageButtonsInit(false, false, new VoidString(ColButtons_Added), true, true));
        }

        public void HideAllButtons()
        {
            colButtons.Init(ImageButtonsInit.HideAllButtons);
        }


        /// <summary>
        /// Args are: object sender, string operation, object data
        /// </summary>
        /// <param name="o"></param>
        /// <param name="operation"></param>
        /// <param name="data"></param>
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
                var chb = CheckBoxHelper.Get(new ControlInitData { text = item });
                chb.IsChecked = defChecked;
                // Must handling Checked / Unchecked, otherwise won't working dialogbuttons and cant exit dialog
                chb.Checked += CheckBox_Checked;
                chb.Unchecked += CheckBox_Unchecked;
                l.Add(chb);
            }
        }

        private void ColButtons_Added(string s)
        {
            AddCheckbox(s, true);
        }

        public IEnumerable<int> CheckedIndexes()
        {
            return CheckBoxListHelper.CheckedIndexes(l.l);
        }

        /// <summary>
        /// Apply method CheckBoxListUC.ContentOfTextBlock if it!s possible (lower hardware consumptation)
        /// </summary>
        /// <returns></returns>
        public IEnumerable<StackPanel> CheckedContent()
        {
            return CheckBoxListHelper.CheckedContent(l.l);
        }

        public List<string> CheckedStrings()
        {
            return CheckBoxListHelper.CheckedStrings(l.l);
        }

        public Dictionary<StackPanel, bool> AllContentDict()
        {
            return CheckBoxListHelper.CheckedContentDict(l.l);
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            s(sender, true);

            if (onCheck.HasValue && onCheck.Value)
            {
                l.OnCollectionChanged(CheckBoxListOperations.Check, sender);
            }
        }

        public List<StackPanel> AllContent()
        {
            return CheckBoxListHelper.AllContent(l.l);
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

        /// <summary>
        /// new DesktopSize( columnGrowing.ActualWidth, rowGrowing.ActualHeight)
        /// </summary>
        /// <param name="s"></param>
        public void OnSizeChanged(DesktopSize s)
        {
            if (Visibility != Visibility.Collapsed)
            {
                var firstButton = colButtons.HeightOfFirstVisibleButton();
                var h = s.Height - firstButton;
                //r0.Height = new GridLength(h);
                lb.Height = lb.MaxHeight = lb.MinHeight = h;
                //lb.InvalidateVisual();
                //lb.Invali

                lb.UpdateLayout();

                ////DebugLogger.Instance.WriteArgs("Height", h, "First button", firstButton, "sp", colButtons.sp.ActualHeight, "colButtons", colButtons.ActualHeight);

                ThisApp.SetStatus(TypeOfMessage.Appeal, SH.Join(" , ", h, lb.ActualHeight.ToString(), lb.Height));
            }
        }

        /// <summary>
        /// THEN MUST BE CALLED HideAllButtons(), DefaultButtons() atd.
        /// </summary>
        /// <param name="i"></param>
        public void Init()
        {
            Init(null, null, false);
        }
    }

    public class CheckBoxListOperations
    {
        public const string Check = "Check";
        public const string UnCheck = "UnCheck";
    }
}

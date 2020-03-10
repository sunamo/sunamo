using desktop.Helpers.Controls;
using sunamo;
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
        , IControlWithResultDebug, IUserControlWithSizeChange,IUserControl, IKeysHandler
    {
        public static Type type = typeof(CheckBoxListUC);
        #region IControlWithResult implementation
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

        public void FocusOnMainElement()
        {

        }

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

            Loaded += uc_Loaded;
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
                else
                {
                    colButtons.Init(new ImageButtonsInit { });
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
                AddCheckbox(chb);
            }
        }

        public void AddCheckbox(CheckBox chb)
        {
            
            // Must handling Checked / Unchecked, otherwise won't working dialogbuttons and cant exit dialog
            chb.Checked += CheckBox_Checked;
            chb.Unchecked += CheckBox_Unchecked;
            l.Add(chb);
        }

        int dexLastChecked = -1;

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

        //public void CheckBox_Click(object sender, RoutedEventArgs e, Checkboxes chb2)
        //{
        //    if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
        //    {
        //        var chb = (CheckBox)sender;
        //        var lastId2 = BTS.ParseInt(chb.Tag.ToString());
        //        GridView2_MultiCheck(lastId, lastId2, chb2);
        //    }
        //    else
        //    {
        //        var chb = (CheckBox)sender;
        //        lastId = BTS.ParseInt(chb.Tag.ToString());
        //    }
        //}

        //public void GridView2_MultiCheck(int arg1, int arg2, Checkboxes chb2)
        //{
        //    var p = NH.Sort<int>(arg1, arg2);
        //    p[1]++;
        //    // is already checked actully, so i dont negate
        //    var col = ((ObservableCollection<T>)lstViewXamlColumns.ItemsSource);
        //    var first = col.First(d => d.Id == arg1);

        //    bool setUp = false;
        //    switch (chb2)
        //    {
        //        case Checkboxes.IsChecked:
        //            setUp = first.IsChecked;
        //            break;
        //        case Checkboxes.IsSelected:
        //            setUp = first.IsSelected;
        //            break;
        //        default:
        //            ThrowExceptions.NotImplementedCase(Exc.GetStackTrace(),type, Exc.CallingMethod());
        //            break;
        //    }

        //    for (int i = p[0]; i < p[1]; i++)
        //    {
        //        first = col.FirstOrDefault(d => d.Id == i);
        //        if (!EqualityComparer<T>.Default.Equals(default(T), first))
        //        {
        //            switch (chb2)
        //            {
        //                case Checkboxes.IsChecked:
        //                    first.IsChecked = setUp;
        //                    break;
        //                case Checkboxes.IsSelected:
        //                    first.IsSelected = setUp;
        //                    break;
        //                default:
        //                    break;
        //            }

        //        }

        //    }
        //}

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            MultiCheck(sender);
            s(sender, true);

            if (onCheck.HasValue && onCheck.Value)
            {
                l.OnCollectionChanged(CheckBoxListOperations.Check, sender);
            }
        }

        private void MultiCheck(object sender)
        {
            var UIElement = (CheckBox)sender;

            var actChecked = lb.Items.IndexOf(UIElement);

            if (actChecked != -1)
            {
                if (KeyboardHelper.IsModifier(Key.LeftShift))
                {
                    if (dexLastChecked != -1)
                    {
                        var p = NH.Sort<int>(actChecked, dexLastChecked);
                        p[1]++;

                        for (int i = p[0]; i < p[1]; i++)
                        {
                            l[i].IsChecked = UIElement.IsChecked;
                        }
                    }
                }

                dexLastChecked = actChecked;
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
            MultiCheck(sender);
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
                if (h >= 0)
                {
                    //r0.Height = new GridLength(h);
                    lb.Height = lb.MaxHeight = lb.MinHeight = h;
                    //lb.InvalidateVisual();
                    //lb.Invali

                    lb.UpdateLayout();

                    ////////DebugLogger.Instance.WriteArgs("Height", h, "First button", firstButton, "sp", colButtons.sp.ActualHeight, "colButtons", colButtons.ActualHeight);

                    ThisApp.SetStatus(TypeOfMessage.Appeal, SH.Join(" , ", h, lb.ActualHeight.ToString(), lb.Height));
                }
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

        public int CountOfHandlersChangeDialogResult()
        {
            return RuntimeHelper.GetInvocationList(ChangeDialogResult).Count;
        }

        public void AttachChangeDialogResult(VoidBoolNullable a, bool throwException = true)
        {
            RuntimeHelper.AttachChangeDialogResult(this, a, throwException);
        }

        public void uc_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        public bool HandleKey(KeyEventArgs e)
        {
            return false;
        }
    }

    public class CheckBoxListOperations
    {
        public const string Check = "Check";
        public const string UnCheck = "UnCheck";
    }
}
using desktop.Helpers.Controls;
using sunamo;
using sunamo.Essential;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
        //dynamic searchTextBox = new object();
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

        public string Title => sess.i18n(XlfKeys.CheckBoxList);

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
        public event Action<object, ListOperation, object> CollectionChanged;
        public NotifyChangesCollection<NotifyPropertyChangedWrapper<CheckBox>> l = null;
        /// <summary>
        /// Whether have raise CollectionChanged after check 
        /// </summary>
        
        public bool initialized = false;

        EventOnArgs eoa = null;

        bool eventOn = false;

        /// <summary>
        /// Must be called after Init
        /// </summary>
        /// <param name="e"></param>
        public void EventOn(EventOnArgs e)
        {
            if (!eventOn)
            {
                eventOn = true;
                this.eoa = e;
                l.EventOn(e);

                if (list != null)
                {
                    foreach (var item in list)
                    {
                        AddCheckbox(item, defChecked);
                    }
                }
            }
        }

        public CheckBoxListUC()
        {
            InitializeComponent();

            Loaded += uc_Loaded;
        }

        public void Clear()
        {
            l.l.Clear();
            lb.ItemsSource = l.l;
        }

        IList<string> list = null;
        bool defChecked = false;

        /// <summary>
        /// A2 can be null
        /// Into A1.add can be CheckBoxListUC.ColButtons_Added
        /// A1 can be null but then is needed to call () which contains Buttons in name like DefaultButtonsInit,HideAllButtons, etc.
        /// </summary>
        /// <param name="list"></param>
        public void Init(ImageButtonsInit i, IList<string> list = null, EventOnArgs e = null, bool defChecked = false)
        {
            if (!initialized)
            {
                

                initialized = true;

                colButtons.MaxHeight = 16 + 2 * 10;

                if (i != null)
                {
                    colButtons.Init(i);
                }
                else
                {
                    colButtons.Init(new ImageButtonsInit { });
                }

                l = new NotifyChangesCollection<NotifyPropertyChangedWrapper<CheckBox>>(this, new ObservableCollection<NotifyPropertyChangedWrapper<CheckBox>>());
                l.CollectionChanged += L_CollectionChanged;

                colButtons.SelectAll += ColButtons_SelectAll;
                colButtons.UnselectAll += ColButtons_UnselectAll;

                //var iso = NotifyPropertyHelper.InnerObjectsOfNotifyPropertyChangedWrapper<CheckBox>(l.l); ;
                lb.ItemsSource = l.l;

                this.list = list;
                this.defChecked = defChecked;
                
                this.DataContext = this;

                SizeChanged += CheckBoxListUC_SizeChanged;
            }

            EventOn(e);
        }

        public  List<string> AllContentString()
        {
            var ac = AllContent();
            List<string> result = new List<string>();
            string text = null;

            foreach (var item in ac)
            {
                //null; //
                //IEnumerable<TextBlock> textboxes = null;
                ////textboxes = VisualTreeHelpers.FindDescendents<TextBlock>(item);

                ////textboxes = item.Children.Where(i=>i);

                //var first = textboxes.First();
                //text = first.Text;

                text = ContentControlHelper.ExtractContent(item);

                result.Add(text);
            }

            return result;
        }

        public static string ContentOfTextBlock(StackPanel key)
        {
            var d = WpfApp.cd;

            UIElementCollection children = PanelHelper.Children(key, d);
            var first = children.dFirstOrNull(d);
            var tb = first as TextBlock;

            IH.tb = tb;
            return d.Invoke<string>(IH.getTextOfTextBlock);
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
        private void L_CollectionChanged(object o, ListOperation operation, object data)
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
                item.o.IsChecked = b;
            }
        }

        private void NotifyWrapper_PropertyChanged(CheckBox obj)
        {

        }

        #region AddCheckbox - everything is private, wiht object input, bool defChecked is even important in that. It must be added to underlying NotifyChangesCollection<NotifyPropertyChangedWrapper<CheckBox>> l and l.l set as ItemsSource
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="defChecked"></param>
        /// <returns></returns>
        private NotifyPropertyChangedWrapper<CheckBox> AddCheckbox(object input, bool defChecked)
        {
            var lines = SH.GetLines(input.ToString());
            NotifyPropertyChangedWrapper<CheckBox> notifyWrapper = null;

            foreach (var item in lines)
            {
                var contents = l.Select(r => r.o.Content);
                var contents2 = new List<string>(contents.Count());

                StackPanel sp = null;

                foreach (var item2 in contents)
                {
                    sp = (StackPanel)item2;
                    contents2.Add(CheckBoxListUC.ContentOfTextBlock(sp));
                }

                var contentString = CA.ToListString(contents2);
                if (CA.IsEqualToAnyElement<object>(item, contentString))
                {
                    continue;
                }

                var chb = CheckBoxHelper.Get(new ControlInitData { text = item });

                notifyWrapper = new NotifyPropertyChangedWrapper<CheckBox>(chb, FrameworkElement.VisibilityProperty);
                NotifyPropertyHelper.CheckBox(notifyWrapper);

                notifyWrapper.o.IsChecked = defChecked;
                AddCheckbox(notifyWrapper);
            }
            return notifyWrapper;
        }


        public void AddCheckbox(NotifyPropertyChangedWrapper<CheckBox> n)
        {
            var chb = n.o;

            // Must handling Checked / Unchecked, otherwise won't working dialogbuttons and cant exit dialog
            chb.Checked += CheckBox_Checked;
            chb.Unchecked += CheckBox_Unchecked;
            l.Add(n);
        } 
        #endregion

        int dexLastChecked = -1;

        public void ColButtons_Added(string s)
        {
            AddCheckbox(s, true);
        }

        public IEnumerable<int> CheckedIndexes()
        {
            var innerObjects = NotifyPropertyHelper.InnerObjectsOfNotifyPropertyChangedWrapper<CheckBox>(l.l);
            return CheckBoxListHelper.CheckedIndexes(innerObjects);
        }

        /// <summary>
        /// Apply method CheckBoxListUC.ContentOfTextBlock if it!s possible (lower hardware consumptation)
        /// </summary>
        public IEnumerable<StackPanel> CheckedContent()
        {
            var innerObjects = NotifyPropertyHelper.InnerObjectsOfNotifyPropertyChangedWrapper<CheckBox>(l.l);
            return CheckBoxListHelper.CheckedContent(innerObjects);
        }

        public List<string> CheckedStrings()
        {
            var innerObjects = NotifyPropertyHelper.InnerObjectsOfNotifyPropertyChangedWrapper<CheckBox>(l.l);
            return CheckBoxListHelper.CheckedStrings(innerObjects);
        }

        public Dictionary<StackPanel, bool> AllContentDict()
        {
            var innerObjects = NotifyPropertyHelper.InnerObjectsOfNotifyPropertyChangedWrapper<CheckBox>(l.l);
            return CheckBoxListHelper.CheckedContentDict(innerObjects);
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
        //    var col = ((ObservableCollection<T>)lstViewXamlColumnNames.ItemsSource);
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

            if (eoa.onCheck)
            {
                l.OnCollectionChanged(ListOperation.Checked, sender);
            }
        }

        private void MultiCheck(object sender)
        {
            var UIElement = (CheckBox)sender;

            var actChecked = lb.Items.IndexOf(UIElement);

            if (actChecked == -1)
            {
                var sp2 = (StackPanel)UIElement.Content;
                var t =  ContentControlHelper.ExtractContent(sp2);
                int i = 0;

                foreach (var item in lb.Items)
                {
                    var chb2 = (NotifyPropertyChangedWrapper<CheckBox>)item;
                    var sp = (StackPanel)chb2.o.Content;
                    var ts = ContentControlHelper.ExtractContent(sp);

                    if (ts != string.Empty)
                    {

                    }

                    if (t != string.Empty)
                    {

                    }

                    if (ts == t)
                    {
                        actChecked = i;
                        break;
                    }
                    i++;
                }
            }

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
                            var ich2 = l[i].o.GetValue(CheckBox.IsCheckedProperty);

                            l[i].o.IsChecked = UIElement.IsChecked;

                            var ich = l[i].o.GetValue(CheckBox.IsCheckedProperty);
                            int s = 0;
                        }
                    }
                }

                dexLastChecked = actChecked;
            }
        }

        public List<StackPanel> AllContent()
        {
            var innerObjects = NotifyPropertyHelper.InnerObjectsOfNotifyPropertyChangedWrapper<CheckBox>(l.l);
            return CheckBoxListHelper.AllContent(innerObjects);
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

            if (Tag != null)
            {
                if (RH.IsOrIsDeriveFromBaseClass(Tag.GetType(), typeof(FrameworkElementTag)))
                {
                    var frameworkElementTag = (FrameworkElementTag)Tag;
                    frameworkElementTag.tagCheckBoxListUC = sender;
                }
                else
                {
                    FrameworkElementTag ft = new FrameworkElementTag();
                    ft.tagCheckBoxListUC = sender;
                    ft.Tag = Tag;
                    Tag = ft;
                }
            }
            else
            {
                Tag = sender;
            }
            
            var where = l.Where(d => d.o.Tag == s.Tag);

            foreach (var item in where)
            {
                // Uložím do 
                item.o.IsChecked = b;
            }
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            MultiCheck(sender);
            s(sender, false);
            if (eoa.onUnCheck)
            {
                l.OnCollectionChanged(ListOperation.Unchecked, sender);
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

                    //////////DebugLogger.Instance.WriteArgs("Height", h, "First button", firstButton, "sp", colButtons.sp.ActualHeight, "colButtons", colButtons.ActualHeight);

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
            Init(null, null, EventOnArgs.allFalseOnlyCheckOn, false);
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
            searchTextBox.OnSearch += SearchTextBox_OnSearch;
            searchTextBox.ShowSectionButton = false;
        }

        private void SearchTextBox_OnSearch(SearchTextBox.SearchEventArgs e)
        {
            var k = e.Keyword;

            DoSearch(k);
        }

        private void DoSearch(string k)
        {
            /*
Here its is not possible with set up visibility
             * */

            if (k.Trim() == string.Empty)
            {
                foreach (var item in l)
                {
                    item.IsActive = true;
                }
            }
            else
            {

                foreach (var item in l)
                {
                    var o = item.o;
                    var sp = (StackPanel)o.Content;
                    var c = CheckBoxListUC.ContentOfTextBlock(sp);

                    if (c.Contains(k))
                    {
                        item.IsActive = true;
                    }
                    else
                    {
                        //o.Visibility = Visibility.Collapsed;
                        //sp.Visibility = Visibility.Collapsed;
                        item.IsActive = false;
                    }
                }
            }

            //lb.UpdateLayout();
        }

        public bool HandleKey(KeyEventArgs e)
        {
            return false;
        }
    }
}
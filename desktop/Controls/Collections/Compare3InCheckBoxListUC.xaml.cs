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
    /// Interaction logic for Compare3InCheckBoxListUC.xaml
    /// </summary>
    public partial class Compare3InCheckBoxListUC : UserControl, IUserControl, IControlWithResult, IUserControlWithMenuItemsList, IUserControlWithSizeChange
    {
        List<CheckBoxListUC> chbls = null;

        public Compare3InCheckBoxListUC()
        {
            InitializeComponent();

            Loaded += uc_Loaded;
        }

       

        public void FocusOnMainElement()
        {

        }

        private void Compare3InCheckBoxListUC_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            foreach (CheckBoxListUC item in chbls)
            {
                if (item.ActualHeight == 0)
                {
                    continue;
                }
                //var size = item.ActualHeight;
                var sp = item.lb;
                sp.Height = sp.MaxHeight = sp.MinHeight = item.ActualHeight - r0.ActualHeight;
                sp.UpdateLayout();

            }
            //r1.Height. = this.ActualHeight - r0.ActualHeight; 
        }

        public bool? DialogResult { set => ChangeDialogResult(value); }

        public string Title => RLData.en["DecideWhichToProcess"];

        public event VoidBoolNullable ChangeDialogResult;

        public void Accept(object input)
        {

        }

        string left, right, both = null;

        /// <summary>
        /// If I want save state to files, must be set up all four
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <param name="both"></param>
        /// <param name="autoNo"></param>
        public void Init(string left, string right, string both)
        {
            this.left = left;
            this.right = right;
            this.both = both;

        }

        public void Init(List<string> left, List<string> right)
        {
            var both = CA.CompareList(left, right);
            Init(left, right, both);
        }


        /// <summary>
        /// A1-4 can be null
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <param name="both"></param>
        /// <param name="autoNo"></param>
        public void Init(List<string> left, List<string> right, List<string> both)
        {
            chbls = CA.ToList<CheckBoxListUC>(chblAutoYes, chblManuallyYes, chblManuallyNo);

            foreach (var item in chbls)
            {
                item.HideAllButtons();
            }

            // First must call Init due to create instance of NotifyChangesCollection
            chblAutoYes.Init(null, left,false);
            chblManuallyYes.Init(null, right);
            chblManuallyNo.Init(null, both);
            

            #region Must init before to avoid raise breakpoints
            chblAutoYes.EventOn(false, true, false, false, false);
            chblManuallyYes.EventOn(false, true, false, false, false);
            chblManuallyNo.EventOn(true, false, false, false, false);
            
            #endregion

            chblAutoYes.DefaultButtonsInit();
            chblManuallyYes.HideAllButtons();
            chblManuallyNo.HideAllButtons();
            

            chblAutoYes.CollectionChanged += ChblAutoYes_CollectionChanged;
            chblManuallyYes.CollectionChanged += ChblManuallyYes_CollectionChanged;
            chblManuallyNo.CollectionChanged += ChblManuallyNo_CollectionChanged;
            

            SizeChanged += Compare3InCheckBoxListUC_SizeChanged;

            Compare3InCheckBoxListUC_SizeChanged(null, null);
        }

        /// <summary>
        /// Only check
        /// </summary>
        private void ChblManuallyNo_CollectionChanged(object sender, string operation, object data)
        {
            MoveCheckBox(sender, chblManuallyNo, chblManuallyYes);
        }

        /// <summary>
        /// Only uncheck
        /// </summary>
        private void ChblManuallyYes_CollectionChanged(object sender, string operation, object data)
        {
            MoveCheckBox(sender, chblManuallyYes, chblManuallyNo);
        }

        /// <summary>
        /// Only uncheck
        /// </summary>
        /// <param name="o"></param>
        private void ChblAutoYes_CollectionChanged(object sender, string operation, object data)
        {
            MoveCheckBox(sender, chblAutoYes, chblManuallyNo);
        }

        int moved = 0;

        private void MoveCheckBox(object sender, CheckBoxListUC from, CheckBoxListUC to)
        {
            moved++;

            var fromName = from.Name;
            var toName = to.Name;

            var c = ch(sender);
            var con = c.Content;
            for (int i = from.l.l.Count - 1; i >= 0; i--)
            {
                if (from.l.l[i].Tag == c.Tag)
                {
                    from.l.l.RemoveAt(i);
                    break;
                }
            }

            //from.l.l.Remove(c);

            c.Content = con;
            // Tag is transfer OK
            //var tag = c.Tag;

            // Switch of IsChecked will be perfomed after click
            to.l.l.Add(c);


            //if (moved % 5 == 0)
            //{
            //    Save(null, null);
            //}


        }

        CheckBox ch(object o)
        {
            var casted = (CheckBoxListUC)o;
            var chb = (CheckBox)casted.Tag;
            return chb;
        }

        /// <summary>
        /// Must be due to IUserControl
        /// </summary>
        public void Init()
        {

        }

        public List<MenuItem> MenuItems()
        {
            //MenuItem mi = MenuItemHelper.Get(Title);

            //MenuItem miSave = MenuItemHelper.Get(new ControlInitData { text = RLData.en["Save"], OnClick = Save });
            //mi.Items.Add(miSave);

            return CA.ToList<MenuItem>();
        }

        public void OnSizeChanged(DesktopSize maxSize)
        {
            chblAutoYes.OnSizeChanged(maxSize);
            chblManuallyYes.OnSizeChanged(maxSize);
            chblManuallyNo.OnSizeChanged(maxSize);
            
        }

        public void uc_Loaded(object sender, RoutedEventArgs e)
        {
            
        }
    }
}

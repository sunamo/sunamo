using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// 
    /// </summary>
    public partial class CompareInCheckBoxListUC : UserControl, IUserControl, IControlWithResult, IUserControlWithMenuItemsList, IUserControlWithSizeChange
    {
        List<CheckBoxListUC> chbls = null;

        public CompareInCheckBoxListUC()
        {
            InitializeComponent();

            Loaded += uc_Loaded;
        }

        public void FocusOnMainElement()
        {

        }

        private void CompareInCheckBoxListUC_SizeChanged(object sender, SizeChangedEventArgs e)
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

        public string Title => sess.i18n(XlfKeys.DecideWhichToProcess);

        public event VoidBoolNullable ChangeDialogResult;

        public void Accept(object input)
        {
            
        }

        string autoYes, manuallyYes, manuallyNo, autoNo = null;

        /// <summary>
        /// Only check
        /// </summary>
        /// <param name="o"></param>
        private void ChblAutoNo_CollectionChanged(object sender, ListOperation operation, object data)
        {
            MoveCheckBox(sender, chblAutoNo, chblManuallyYes);
        }

        /// <summary>
        /// Only check
        /// </summary>
        private void ChblManuallyNo_CollectionChanged(object sender, ListOperation operation, object data)
        {
            MoveCheckBox(sender, chblManuallyNo, chblManuallyYes);
        }

        /// <summary>
        /// Only uncheck
        /// </summary>
        private void ChblManuallyYes_CollectionChanged(object sender, ListOperation operation, object data)
        {
            MoveCheckBox(sender, chblManuallyYes, chblManuallyNo);
        }

        /// <summary>
        /// Only uncheck
        /// </summary>
        /// <param name="o"></param>
        private void ChblAutoYes_CollectionChanged(object sender, ListOperation operation, object data)
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
            var con = c.o.Content;
            for (int i = from.l.l.Count - 1; i >= 0; i--)
            {
                if (from.l.l[i].o.Tag == c.o.Tag)
                {
                    from.l.l.RemoveAt(i);
                    break;
                }
            }

            //from.l.l.Remove(c);

            c.o.Content = con;
            // Tag is transfer OK
            //var tag = c.Tag;

            // Switch of IsChecked will be perfomed after click
            to.l.l.Add(c);

            
                if (moved % 5 == 0)
                {
                    Save(null, null);
                }
        }

        NotifyPropertyChangedWrapper<CheckBox> ch(object o)
        {
            var casted = (CheckBoxListUC)o;
            var chb = (NotifyPropertyChangedWrapper<CheckBox>)casted.Tag;

            // Debugger because I have changed from CheckBox to NotifyPropertyChangedWrapper< CheckBox>
            Debugger.Break();

            return chb;
        }


        private void Save(object sender, RoutedEventArgs e)
        {
            TF.SaveLines(chblAutoYes.AllContentString(), autoYes);
            TF.SaveLines(chblManuallyYes.AllContentString(), manuallyYes);
            TF.SaveLines(chblManuallyNo.AllContentString(), manuallyNo);
            TF.SaveLines(chblAutoNo.AllContentString(), autoNo);
        }

       
        public List<string> ExcludeToProcess()
        {
            List<string> resultNo = new List<string>(chblManuallyNo.Count + chblManuallyYes.Count);
            return resultNo;
        }

        public List<string> IncludeToProcess()
        {
            List<string> result = new List<string>(chblManuallyYes.Count + chblAutoYes.Count);

            return result;
        }

        /// <summary>
        /// If I want save state to files, must be set up all four
        /// </summary>
        /// <param name="autoYes"></param>
        /// <param name="manuallyYes"></param>
        /// <param name="manuallyNo"></param>
        /// <param name="autoNo"></param>
        public void Init(string autoYes, string manuallyYes, string manuallyNo, string autoNo)
        {
            this.autoYes = autoYes;
            this.manuallyYes = manuallyYes;
            this.manuallyNo = manuallyNo;
            this.autoNo = autoNo;

            Init(TF.ReadAllLines(autoYes), TF.ReadAllLines(manuallyYes), TF.ReadAllLines(manuallyNo), TF.ReadAllLines(autoNo));
        }

        public void Init(IList<string> autoYes, IList<string> autoNo)
        {
            Init(autoYes, null, null, autoNo);
        }

        /// <summary>
        /// A1-4 can be null
        /// </summary>
        /// <param name="autoYes"></param>
        /// <param name="manuallyYes"></param>
        /// <param name="manuallyNo"></param>
        /// <param name="autoNo"></param>
        public void Init(IList<string> autoYes, IList<string> manuallyYes, IList<string> manuallyNo, IList<string> autoNo)
        {
            chbls = CA.ToList<CheckBoxListUC>(chblAutoYes, chblManuallyYes, chblManuallyNo, chblAutoNo);

            foreach (var item in chbls)
            {
                item.HideAllButtons();
            }

            // First must call Init due to create instance of NotifyChangesCollection
            chblAutoYes.Init(null, autoYes, EventOnArgs.allFalseOnlyCheckOn, true);
            chblManuallyYes.Init(null, manuallyYes, EventOnArgs.allFalseOnlyCheckOn);
            chblManuallyNo.Init(null, manuallyNo, EventOnArgs.allFalseOnlyCheckOn);
            chblAutoNo.Init(null, autoNo, EventOnArgs.allFalseOnlyCheckOn, false);

            #region Must init before to avoid raise breakpoints
            chblAutoYes.EventOn(new EventOnArgs(false, true, false, false, false, false));
            chblManuallyYes.EventOn(new EventOnArgs( false, true, false, false, false, false));
            chblManuallyNo.EventOn(new EventOnArgs(true, false, false, false, false, false));
            chblAutoNo.EventOn(new EventOnArgs(true, false, false, false, false, false));
            #endregion

            chblAutoYes.DefaultButtonsInit();
            chblManuallyYes.HideAllButtons();
            chblManuallyNo.HideAllButtons();
            chblAutoNo.HideAllButtons();

            chblAutoYes.CollectionChanged += ChblAutoYes_CollectionChanged;
            chblManuallyYes.CollectionChanged += ChblManuallyYes_CollectionChanged;
            chblManuallyNo.CollectionChanged += ChblManuallyNo_CollectionChanged;
            chblAutoNo.CollectionChanged += ChblAutoNo_CollectionChanged;

            SizeChanged += CompareInCheckBoxListUC_SizeChanged;

            CompareInCheckBoxListUC_SizeChanged(null, null);
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

            MenuItem miSave = MenuItemHelper.Get(new ControlInitData { text = sess.i18n(XlfKeys.Save), OnClick = Save });
            //mi.Items.Add(miSave);

            return CA.ToList<MenuItem>(miSave);
        }

        public void OnSizeChanged(DesktopSize maxSize)
        {
            chblAutoYes.OnSizeChanged(maxSize);
            chblManuallyYes.OnSizeChanged(maxSize);
            chblManuallyNo.OnSizeChanged(maxSize);
            chblAutoNo.OnSizeChanged(maxSize);
        }

        public void uc_Loaded(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
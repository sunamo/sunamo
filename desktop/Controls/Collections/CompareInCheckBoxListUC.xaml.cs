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
    /// Interaction logic for CompareInCheckBoxListUC.xaml
    /// </summary>
    public partial class CompareInCheckBoxListUC : UserControl, IUserControl, IUserControlInWindow
    {
        List<CheckBoxListUC> chbls = null;

        public CompareInCheckBoxListUC()
        {
            InitializeComponent();
        }

        private void CompareInCheckBoxListUC_SizeChanged(object sender, SizeChangedEventArgs e)
        {

            foreach (var item in chbls)
            {
                if (this.ActualHeight == 0)
                {
                    continue;
                }
                var size = item.ActualHeight;
                item.Height = this.ActualHeight - r0.ActualHeight;
            }
            //r1.Height. = this.ActualHeight - r0.ActualHeight; 
        }

        public bool? DialogResult { set => ChangeDialogResult(value); }

        public string Title => "Decide which to process";

        public event VoidBoolNullable ChangeDialogResult;

        public void Accept(object input)
        {
            
        }

        public void Init(List<string> autoYes, List<string> manuallyYes, List<string> manuallyNo, List<string> autoNo)
        {
            chbls = CA.ToList<CheckBoxListUC>(chblAutoYes, chblManuallyYes, chblManuallyNo, chblAutoNo);

            foreach (var item in chbls)
            {
                item.HideAllButtons();
            }

            // First must call Init due to create instance of NotifyChangesCollection
            chblAutoYes.Init( autoYes, true);
            chblManuallyYes.Init();
            chblManuallyNo.Init();
            chblAutoNo.Init(autoNo, false);


            chblAutoYes.EventOn(false, true, false, false, false);
            chblManuallyYes.EventOn(false, true, false, false, false);
            chblManuallyNo.EventOn(true, false, false, false, false);
            chblAutoNo.EventOn(true, false, false, false, false);

            chblAutoYes.HideAllButtons();
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

        string autoYes, manuallyYes, manuallyNo, autoNo = null;

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

        /// <summary>
        /// Only check
        /// </summary>
        /// <param name="o"></param>
        private void ChblAutoNo_CollectionChanged(object sender, string operation, object data)
        {
            MoveCheckBox(sender, chblAutoNo, chblManuallyYes);
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

        public void Init(List<string> c1, List<string> c2)
        {
            throw new NotImplementedException();
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

            if (moved % 5 == 0)
            {
                TF.SaveLines(chblAutoYes.AllContent(), autoYes);
                TF.SaveLines(chblManuallyYes.AllContent(), manuallyYes);
                TF.SaveLines(chblManuallyNo.AllContent(), manuallyNo);
                TF.SaveLines(chblAutoNo.AllContent(), autoNo);

            }
        }

        CheckBox ch(object o)
        {
            var casted = (CheckBoxListUC)o;
            var chb = (CheckBox)casted.Tag;
            return chb;
        }

        public List<string> ExcludeToProcess()
        {
            throw new NotImplementedException();
        }

        public List<string> IncludeToProcess()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Must be due to IUserControl
        /// </summary>
        public void Init()
        {
            
        }
    }
}

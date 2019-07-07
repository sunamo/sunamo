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
        public CompareInCheckBoxListUC()
        {
            InitializeComponent();
        }

        public bool? DialogResult { set => ChangeDialogResult(value); }

        public string Title => "Decide which to process";

        public event VoidBoolNullable ChangeDialogResult;

        public void Accept(object input)
        {
            
        }

        public void Init(List<string> c, List<string> notToTranslate)
        {
            chblAutoYes.EventOn(false, true, false, false, false);
            chblManuallyYes.EventOn(false, true, false, false, false);
            chblManuallyNo.EventOn(true, false, false, false, false);
            chblAutoNo.EventOn(true, false, false, false, false);

            chblAutoYes.HideAllButtons();
            chblManuallyYes.HideAllButtons();
            chblManuallyNo.HideAllButtons();
            chblAutoNo.HideAllButtons();

            chblAutoYes.Init(c);
            chblAutoNo.Init(notToTranslate);

            chblAutoYes.CollectionChanged += ChblAutoYes_CollectionChanged;
            chblManuallyYes.CollectionChanged += ChblManuallyYes_CollectionChanged;
            chblManuallyNo.CollectionChanged += ChblManuallyNo_CollectionChanged;
            chblAutoNo.CollectionChanged += ChblAutoNo_CollectionChanged;
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

        /// <summary>
        /// Only uncheck
        /// </summary>
        /// <param name="o"></param>
        private void ChblAutoYes_CollectionChanged(object sender, string operation, object data)
        {
            MoveCheckBox(sender, chblAutoYes, chblManuallyNo);
        }

        private void MoveCheckBox(object sender, CheckBoxListUC from, CheckBoxListUC to)
        {
            var c = ch(sender);
            from.l.l.Remove(c);

            // Switch of IsChecked will be perfomed after click
            to.l.l.Add(c);


        }

        CheckBox ch(object o)
        {
            var casted = (CheckBox)o;
            return casted;
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

using System;
using System.Collections;
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

namespace desktop.Controls.Input
{
    /// <summary>
    /// Select Value - more from selector
    /// EnterOneValueUC - single,  EnterOneValueUC - fwElemements 
    /// </summary>
    public partial class SelectTwoValues : UserControl, IUserControl, IControlWithResult
    {
        public ComboBoxHelper<string> cbEntered1Helper = null;
        public ComboBoxHelper<string> cbEntered2Helper = null;

        public bool? DialogResult {
            set
            {
                ChangeDialogResult(value);
            }
        }

        public void FocusOnMainElement()
        {
            cbEntered1.Focus();
        }

        public string Title => sess.i18n(XlfKeys.SelectTwoValues);

        public event VoidBoolNullable ChangeDialogResult;

        public SelectTwoValues()
        {
            InitializeComponent();

            cbEntered1Helper = new ComboBoxHelper<string>(cbEntered1);
            cbEntered2Helper = new ComboBoxHelper<string>(cbEntered2);

            cbEntered1Helper.SelectionChanged += CbEntered1_Selected;
            cbEntered2Helper.SelectionChanged += CbEntered2_Selected;

            btnEnter.IsEnabled = false;

            Loaded += uc_Loaded;
        }

        private void CbEntered1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                
            }
        }

        private void CbEntered2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                
            }
        }

        /// <summary>
        /// IN early time set up red border but this was useless, now not used
        /// </summary>
        /// <param name="cbEntered2"></param>
        private bool AfterEnteredValue(ComboBoxHelper<string> cbEntered2)
        {
            var cbEntered = cbEntered2.Cb;
            if (cbEntered2.Selected)
            {
                cbEntered.BorderThickness = new Thickness(0);
                return true;
            }
            // Dont set up border, only collapse size of element
            //cbEntered.BorderThickness = new Thickness(20);
            //cbEntered.BorderBrush = new SolidColorBrush(Colors.Red);
            return false;
        }

        private void CbEntered1_Selected(object sender, RoutedEventArgs e)
        {
            EnableBtn();
        }

        private void CbEntered2_Selected(object sender, RoutedEventArgs e)
        {
            EnableBtn();
        }

        private void EnableBtn()
        {
            var cb1 = cbEntered1Helper.Selected;
            var cb2 = cbEntered2Helper.Selected;
                if (cb1 && cb2)
                {
                    btnEnter.IsEnabled = true;
                }
                else
                {
                    btnEnter.IsEnabled = false;
                }   
        }

        public void Accept(object input)
        {
            
        }

        /// <summary>
        /// Nothing can be null
        /// </summary>
        /// <param name="whatEnter1"></param>
        /// <param name="values1"></param>
        /// <param name="whatEnter2"></param>
        /// <param name="values2"></param>
        /// <param name="hint"></param>
        public void Init(string whatEnter1, IEnumerable values1, string whatEnter2, IEnumerable values2, string hint)
        {
            TextBlockHelper.SetTextPostColon(tbWhatEnter1, whatEnter1);
            cbEntered1Helper.AddValuesOfEnumerableAsItems(values1);

            TextBlockHelper.SetTextPostColon(tbWhatEnter2, whatEnter2);
            cbEntered2Helper.AddValuesOfEnumerableAsItems(values2);

            TextBlockHelper.SetText(tbHint, hint);
        }

        /// <summary>
        /// Better use Init() with parameters
        /// </summary>
        public void Init()
        {
            
        }

        private void BtnEnter_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        public void uc_Loaded(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
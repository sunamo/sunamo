using System;
using System.Collections.Generic;
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
using desktop.Helpers;

namespace desktop.Controls.Input
{
    /// <summary>
    /// Use for variable name always longer, enterOneValue instead of eov
    /// Select Value - more from selector
    /// EnterOneValueUC - single,  EnterOneValueUC - fwElemements 
    /// </summary>
    public partial class EnterOneValueUC : UserControl, IControlWithResult, IUserControlWithMenuItemsList, IControlWithResultDebug
    {
        static Type type = typeof(EnterOneValueUC);
        public ValidateData validateData = null;
       
        
            
        //
        #region ctor
        /// <summary>
        /// Has button so dialogButtons is not needed to add
        /// </summary>
        public EnterOneValueUC()
        {
            InitializeComponent();

            dynLayout = new DynLayout(gridGrowable);

            fwElemements = CA.ToList<FrameworkElement>(txtEnteredText);

            Loaded += uc_Loaded;
        }

        public EnterOneValueUC(string whatEnter, Size size) : this()
        {
            Init(whatEnter);

            if (size != Size.Empty)
            {
                txtEnteredText.Width = size.Width;
                txtEnteredText.Height = size.Height;
            }
        }

        /// <summary>
        /// BY default use args-less ctor and then Init
        /// </summary>
        /// <param name="whatEnter"></param>
        public EnterOneValueUC(string whatEnter) : this()
        {
            Init(whatEnter);

            txtEnteredText.AcceptsReturn = true;

            Loaded += EnterOneValueUC_Loaded;
        } 
        #endregion

        public bool IsMultiline
        {
            set
            {
                if (value)
                {
                    txtEnteredText.AcceptsReturn = true;
                    txtEnteredText.Height = txtEnteredText.Height * 5;
                }
                else
                {
                    txtEnteredText.AcceptsReturn = false;
                    txtEnteredText.Height /= 5;
                }
            }
        }

        public void EnterOneValueUC_Loaded(object sender, RoutedEventArgs e)
        {
            PrintColumnsRows(gridGrowable);
            PrintColumnsRows(grid2);
        }

        private void PrintColumnsRows(Grid grid2)
        {
#if DEBUG
            //////DebugLogger.Instance.WriteLine(grid2.ColumnDefinitions.Count + "x" + grid2.RowDefinitions.Count);
#endif
        }

        public void Init(string whatEnter)
        {
            tbWhatEnter.Text = sess.i18n(XlfKeys.ToEnter) + " " + whatEnter + " " + sess.i18n(XlfKeys.andPressEnter) + ".";
            btnEnter.Content = sess.i18n(XlfKeys.Enter);
        }

        

        /// <summary>
        /// TextBlock.Text is take from Tag, which can be TWithName<object>
        /// Tag can be TWithName<object> or any object and its value is set to TextBlock
        /// </summary>
        /// <param name="uie"></param>
        public void Init(IEnumerable<FrameworkElement> uie, GridSize gs = GridSize.GetAutoSize)
        {
            //gridGrowable.
            txtEnteredText.Visibility = Visibility.Collapsed;
            //txtEnteredText.Parent.Chi

            fwElemements = CA.ToList<FrameworkElement>(uie);

            if (gs == GridSize.GetAutoSize)
            {
                GridHelper.GetAutoSize(gridGrowable, 2, uie.Count());
            }
            else if (gs == GridSize.Mine)
            {
                ThrowExceptions.NotImplementedCase(Exc.GetStackTrace(), type, Exc.CallingMethod(), gs);
            }
            else if (gs == GridSize.XamlDefined)
            {
                // is already in xaml
                GridHelper.GetAutoSize(gridGrowable, 0, uie.Count());
            }

            int i = 0;

            foreach (var item in uie)
            {
                string name = null;
                name = ExtractName(item);

                AddControl(i, name, item);

                i++;
            }
        }

        private static string ExtractName(FrameworkElement item)
        {
            string name;
            if (item.Tag is TWithName<object>)
            {
                var t = (TWithName<object>)item.Tag;
                name = t.name;
            }
            else
            {
                name = item.Tag.ToString();
            }

            return name;
        }

        

        

        private void btnEnter_Click_1(object sender, RoutedEventArgs e)
        {
            ButtonBase bb;
            
            if (AfterEnteredValue(fwElemements))
            {
                DialogResult = true;
            }
        }

        public bool? DialogResult
        {
            set
            {
                if (ChangeDialogResult != null)
                {
                    ChangeDialogResult(value);
                }

            }
        }//

        public string Title => sess.i18n(XlfKeys.EnteringData);

        public object GetContentByTag(object tag)
        {
            return dynLayout.GetContentByTag(tag);
        }

        public void AddControl(int row, string name, FrameworkElement ui)
        {
            dynLayout.AddControl(row, name, ui);
        }

        private bool AfterEnteredValue(List<FrameworkElement> txtEnteredText)
        {
            string methodName = "AfterEnteredValue";
            bool? previousValidate = true;

            bool allOk = true;

            foreach (var item in txtEnteredText)
            {
                // Always set to true
                item.SetValidated(previousValidate.Value);

                previousValidate = item.Validate2(ExtractName(item), validateData);

                if (previousValidate.HasValue)
                {
                    if (!previousValidate.Value)
                    {
                        if (RH.IsOrIsDeriveFromBaseClass(item.GetType(), TypesControls.tControl))
                        {
                            var c = (Control)item;
                            c.BorderThickness = new Thickness(2);
                            c.BorderBrush = new SolidColorBrush(Colors.Red);

                            if (validateData != null)
                            {
                                if (!string.IsNullOrEmpty(validateData.messageToReallyShow))
                                {
                                    tbHint.Text = validateData.messageToReallyShow;
                                }
                            }

                            allOk = false;
                        }
                    }
                }
                else
                {
                    allOk = false;
                    ThrowExceptions.Custom(Exc.GetStackTrace(),type, methodName, sess.i18n(XlfKeys.NotImplementedValidateForControl)+" " + item.GetType().FullName);
                }
            }
            //txtEnteredText.Text = txtEnteredText.Text.Trim();
            //if (txtEnteredText.Text != "")
            //{
            //    return true;
            //}

            if (allOk)
            {
                return true;
            }

            return false;
        }
        public List<FrameworkElement> fwElemements
        {
            get
            {
                return dynLayout.fwElements;
            }
            set
            {
                dynLayout.fwElements = value;
            }
        }
        public object this[int i]
        {
            get
            {
                return fwElemements[i].GetContent();
            }
        }

        public DynLayout dynLayout = null;

        private void txtEnteredText_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (AfterEnteredValue(fwElemements))
                {
                    DialogResult = true;
                }
            }
        }

        public void Accept(object input)
        {
            txtEnteredText.Text = input.ToString();
            ButtonHelper.PerformClick(btnEnter);
            // Cant be, window must be already showned as dialog
            //DialogResult = true;
        }

        public List<MenuItem> menuItems = new List<MenuItem>();
        //public TextBox txtEnteredText => new TextBox();

        public List<MenuItem> MenuItems()
        {
            return menuItems;
        }

        public void Init()
        {
            
        }

        public int CountOfHandlersChangeDialogResult()
        {
            var l = RuntimeHelper.GetInvocationList(ChangeDialogResult);
            return l.Count;
        }

        public void AttachChangeDialogResult(VoidBoolNullable a, bool throwException = true)
        {
            RuntimeHelper.AttachChangeDialogResult(this, a, throwException);
        }

        public void FocusOnMainElement()
        {
            txtEnteredText.Focus();
        }

        public void uc_Loaded(object sender, RoutedEventArgs e)
        {
            //ThrowExceptions.NotImplementedMethod(Exc.GetStackTrace(),type, Exc.CallingMethod());
        }

        public event VoidBoolNullable ChangeDialogResult;
    }
}
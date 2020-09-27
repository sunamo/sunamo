using sunamo.Data;
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


namespace desktop.Controls.Mouse
{
    /// <summary>
    /// 
    /// </summary>
    public partial class InsertLetterAfterMouseDownUC : UserControl, IControlWithResult,IShowSearchResults
    {
        public string ToInsert = AllStrings.bs;

        public void FocusOnMainElement()
        {

        }

        public bool? DialogResult {
            set
            {
                if (value.HasValue && value.Value)
                {
                    Data.Inserted = txt.Text;
                    ChangeDialogResult(value);
                }
            }
        }

        public event VoidBoolNullable ChangeDialogResult;

        public InsertLetterAfterMouseDownUC()
        {
            InitializeComponent();

            //txt.KeyDown += Txt_KeyDown;
        }

        private void DialogButtons_ChangeDialogResult(bool? b)
        {
            ChangeDialogResult(b);
        }

        InsertLetterAfterMouseDownData Data
        {
            get
            {
                return (InsertLetterAfterMouseDownData)Tag;
            }
        }


        public void Accept(object input)
        {
            txt.Text = input.ToString();
            ChangeDialogResult(true);
        }

        bool insertNow
        {
            get
            {
                return BTS.GetValueOfNullable(chbInsert.IsChecked);
            }
            set
            {
                chbInsert.IsChecked = value;
            }
        }

        private void Txt_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            
            if (e.ChangedButton == MouseButton.Right)
            {
                insertNow = false;
            }
            else if (e.ChangedButton == MouseButton.Left)
            {
                if (insertNow)
                {
                    txt.Text = txt.Text.Insert(txt.SelectionStart, ToInsert);
                }
                
            }
            
        }

        public TextBoxState state = new TextBoxState();

        public void SetTbSearchedResult(int actual, int count)
        {
            state.textSearchedResult = $"{actual}/{count}";
            SetTextBoxState();
        }

        public void SetTextBoxState(string s = null)
        {
            if (s == null)
            {
                s = state.textSearchedResult;
            }
            tbState.Text = s;
        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        Key lastKey = Key.Enter;

        private void Txt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != lastKey)
            {
                if (e.Key == Key.Enter)
                {
                    lastKey = e.Key;
                    DialogResult = true;
                }
            }
        }
    }
}
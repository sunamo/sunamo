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
    /// Interaction logic for InsertLetterAfterMouseDownUC.xaml
    /// </summary>
    public partial class InsertLetterAfterMouseDownUC : UserControl, IUserControlInWindow
    {
        public string ToInsert = "\\";

        public bool? DialogResult { set  {
                if (value.HasValue && value.Value)
                {
                    Data.Inserted = txt.Text;
                }
            } }

        public event VoidBoolNullable ChangeDialogResult;

        public InsertLetterAfterMouseDownUC()
        {
            InitializeComponent();
            
            dialogButtons.ChangeDialogResult += DialogButtons_ChangeDialogResult;
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

        private void Txt_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void Txt_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void Txt_MouseUp(object sender, MouseButtonEventArgs e)
        {
            
        }

        bool insertNow = true;

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
    }
}

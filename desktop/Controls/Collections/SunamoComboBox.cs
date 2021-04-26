using sunamo.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace desktop.Controls.Collections
{
    /// <summary>
    /// Doesnt be as UC because https://stackoverflow.com/a/35158783/9327173
    /// </summary>
    public class SunamoComboBox : ComboBox
    {
        static Type type = typeof(SunamoComboBox);

        public SunamoComboBox()
        {
            this.KeyUp += SunamoComboBox_KeyUp;
        }

        public ComboBox cb
        {
            get
            {
                return this;
            }
        }

        TextBox editableTextBox;

        public override void OnApplyTemplate()
        {
            SetEditableTextbox();
        }

        public void SetCaret(int position)
        {
            this.editableTextBox.SelectionStart = position;
            this.editableTextBox.SelectionLength = 0;
        }

        void SetEditableTextbox()
        {
            base.OnApplyTemplate();
            string nameChild = "PART_EditableTextBox";
            //var d = GetTemplateChild(nameChild);
            var d = Template.FindName(nameChild, this);
            if (d != null)
            {
                string type = d.GetType().FullName;
                var myTextBox = d as TextBox;
                if (myTextBox != null)
                {
                    this.editableTextBox = myTextBox;
                }
            }
        }

        private void SunamoComboBox_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                this.Text += AllStrings.space;
                SetCaret(editableTextBox.Text.Length);
            }
        }
    }
}
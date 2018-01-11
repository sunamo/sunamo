using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace F.WF
{
    public partial class SelectFrom3ActionsForm : Form
    {
        public SelectFrom3ActionsForm(string formTitle, string labelIntroduction, object[] lbitems, string buttonYes, string buttonNo, string buttonCancel)
        {
            InitializeComponent();

            Text = formTitle;
            label1.Text = labelIntroduction;
            listBox1.Items.AddRange(lbitems);
            button3.Text = buttonYes;
            button2.Text = buttonNo;
            button1.Text = buttonCancel;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Yes;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}

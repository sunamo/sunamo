using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace F.WF
{
    // Nemùže být generický protože bych to musel dát jak tu tak do Designer
    public partial class CHLBForm : Form
    {


        public CHLBForm(string label, params object[] items)
        {
            InitializeComponent();


            label1.Text = label;
            checkedListBox1.Items.AddRange(items);
        }

        public int CheckedCount
        {
            get
            {
                return checkedListBox1.CheckedIndices.Count;
            }
        }
    }
}
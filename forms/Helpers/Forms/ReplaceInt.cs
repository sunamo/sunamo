﻿using System.Windows.Forms;
namespace F.WF
{
    /// <summary>
    /// 
    /// </summary>
    public class ReplaceInt : Form
    {
        private Label label2;
        private Label label3;
        private NumberTextBoxMona numberTextBoxMona1;
        private NumberTextBoxMona numberTextBoxMona2;
        private Button button1;
        private Button button2;

        private Label label1;

        public int OldValue
        {
            get
            {
                return numberTextBoxMona1.Number;
            }
        }

        public int NewValue
        {
            get
            {
                return numberTextBoxMona2.Number;
            }
        }

        public ReplaceInt(ReplaceArgs ra, string whatSearchAndReplace, string old, string novy)
        {
            InitializeComponent();

            if (ra != null)
            {
                if (!string.IsNullOrEmpty(ra.initialMessage))
                {
                    label1.Text = ra.initialMessage;
                }
                if (!string.IsNullOrEmpty(ra.lblOldValue))
                {
                    label2.Text = ra.lblOldValue;
                }
                if (!string.IsNullOrEmpty(ra.lblNewValue))
                {
                    label3.Text = ra.lblNewValue;
                }
            }

            label1.Text += whatSearchAndReplace + AllStrings.colon;
            numberTextBoxMona1.Text = old.ToString();
            numberTextBoxMona2.Text = novy.ToString();
        }

        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.numberTextBoxMona1 = new NumberTextBoxMona();
            this.numberTextBoxMona2 = new NumberTextBoxMona();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(129, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Type values for replacing ";
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Old value:";
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "New value:";
            this.numberTextBoxMona1.Location = new System.Drawing.Point(93, 61);
            this.numberTextBoxMona1.Name = "numberTextBoxMona1";
            this.numberTextBoxMona1.Size = new System.Drawing.Size(100, 20);
            this.numberTextBoxMona1.TabIndex = 3;
            this.numberTextBoxMona1.Text = "0";
            this.numberTextBoxMona2.Location = new System.Drawing.Point(93, 35);
            this.numberTextBoxMona2.Name = "numberTextBoxMona2";
            this.numberTextBoxMona2.Size = new System.Drawing.Size(100, 20);
            this.numberTextBoxMona2.TabIndex = 4;
            this.numberTextBoxMona2.Text = "0";
            this.button1.Location = new System.Drawing.Point(118, 87);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Cancel";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            this.button2.Location = new System.Drawing.Point(37, 87);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 6;
            this.button2.Text = "OK";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            this.ClientSize = new System.Drawing.Size(210, 119);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.numberTextBoxMona2);
            this.Controls.Add(this.numberTextBoxMona1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "ResizeImages";
            this.Text = "Replace ";
            this.Load += new System.EventHandler(this.ResizeImages_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void ResizeImages_Load(object sender, System.EventArgs e)
        {

        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            if (NewValue != OldValue)
            {
                DialogResult = DialogResult.OK;
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Values are equals");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace F.WF
{
    /// <summary>
    /// Jednoduche okno s popiskem a text. polem. 
    /// </summary>
    public partial class EnterValueEventForm : Form
    {
        private Button button1;
        public TextBox TextBox1;
        private Label label1;

        #region wf
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.TextBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(264, 27);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(50, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "Enter";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // TextBox1
            // 
            this.TextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox1.Location = new System.Drawing.Point(15, 27);
            this.TextBox1.Name = "TextBox1";
            this.TextBox1.Size = new System.Drawing.Size(243, 20);
            this.TextBox1.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(172, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Zadejte hodnotu a stisknete. enter:";
            // 
            // EnterValueEventForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(326, 59);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.TextBox1);
            this.Controls.Add(this.label1);
            this.Name = "EnterValueEventForm";
            this.Text = "SMText";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        #endregion

        /// <summary>
        /// Po stisku entru.
        /// </summary>
        public event VoidObjectParamsObjects Zadani;

        /// <summary>
        /// IK, WF.
        /// </summary>
        public EnterValueEventForm()
        {
            InitializeComponent();
            this.KeyDown += new KeyEventHandler(VyvolejUdalostPoEntru);
            this.Closing += new CancelEventHandler(SMText_Closing);


        }

        /// <summary>
        /// Zakaze zavreni pres krizek.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void SMText_Closing(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
        }

        /// <summary>
        /// Pokud byl stisknut enter, vyvola udalost.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void VyvolejUdalostPoEntru(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                SkryjForm();

                if (Zadani != null)
                {
                    Zadani(TextBox1.Text);
                }
            }
        }

        private void SkryjForm()
        {
            DialogResult = DialogResult.OK;
            this.Visible = false;
        }

        /// <summary>
        /// Do A1 zadejte hondotu bez dvojtecky - pripoji se sama a zadejte na zacatku.
        /// </summary>
        /// <param name="label"></param>
        public EnterValueEventForm(string label)
            : this()
        {
            Text = "Enter " + label;
            label1.Text = "Enter " + label + " and press enter: ";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            SkryjForm();
        }
    }
}

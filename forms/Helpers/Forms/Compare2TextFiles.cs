using System.Windows.Forms;
using System;
namespace F.WF
{
    /// <summary>
    /// 
    /// </summary>
    public class Compare2TextFiles : Form
    {
        private Label label1;
        private Button button1;
        private Button button2;
        private Button button3;
        private TextBoxForms textBox1;
        private TextBoxForms textBox2;
        private SplitContainer splitContainer1;

        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.textBox1 = new TextBoxForms();
            this.textBox2 = new TextBoxForms();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Panel1.Controls.Add(this.textBox1);
            this.splitContainer1.Panel2.Controls.Add(this.textBox2);
            this.splitContainer1.Size = new System.Drawing.Size(528, 293);
            this.splitContainer1.SplitterDistance = 226;
            this.splitContainer1.TabIndex = 0;
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(41, 313);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(232, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "What do you want proceed?";
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(441, 310);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Keep right";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(279, 310);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "Keep left";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.Location = new System.Drawing.Point(360, 310);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 4;
            this.button3.Text = "Merge";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Location = new System.Drawing.Point(0, 0);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(226, 293);
            this.textBox1.TabIndex = 0;
            this.textBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox2.Location = new System.Drawing.Point(0, 0);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(298, 293);
            this.textBox2.TabIndex = 1;
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Compare2TextFiles";
            this.Size = new System.Drawing.Size(528, 351);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        /// <summary>
        /// Left - Yes
        /// Merge = OK
        /// Right - No
        /// </summary>
        public Compare2TextFiles(string textLeft, string textRight)
        {
            InitializeComponent();
            OnResize(EventArgs.Empty);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnResize(System.EventArgs e)
        {
            base.OnResize(e);

            splitContainer1.SplitterDistance = splitContainer1.Width - 4 / 2;


        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Yes;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
        }
    }
}

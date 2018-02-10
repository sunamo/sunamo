using System.Windows.Forms;
namespace F.WF
{
    public class EnterValueForm : Form
    {

        private Button button1;
        public TextBox TextBox1;
        private Label label1;

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
            this.button1.Location = new System.Drawing.Point(264, 28);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(50, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Enter";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // TextBox1
            // 
            this.TextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox1.Location = new System.Drawing.Point(15, 28);
            this.TextBox1.Name = "TextBox1";
            this.TextBox1.Size = new System.Drawing.Size(243, 20);
            this.TextBox1.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(172, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Zadejte hodnotu a stisknete. enter:";
            // 
            // EnterValueForm
            // 
            this.ClientSize = new System.Drawing.Size(326, 59);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.TextBox1);
            this.Controls.Add(this.label1);
            this.Name = "EnterValueForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        public EnterValueForm()
        {
            InitializeComponent();
            TextBox1.KeyDown += new KeyEventHandler(TextBox1_KeyDown);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);


        }

        void TextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DialogResult = DialogResult.OK;
            }
        }

        /// <summary>
        /// Do A1 zadejte hondotu bez dvojtecky - pripoji se sama a zadejte na zacatku.
        /// </summary>
        /// <param name="label"></param>
        public EnterValueForm(string label)
            : this()
        {
            Text = "Enter " + label;
            label1.Text = "Enter " + label + " and press enter: ";

        }

        private void button1_Click(object sender, System.EventArgs e)
        {

            DialogResult = DialogResult.OK;
        }
    }
}

partial class WebBrowserWF
    {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WebBrowserWF));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnBack = new System.Windows.Forms.ToolStripButton();
            this.btnNext = new System.Windows.Forms.ToolStripButton();
            this.btnReload = new System.Windows.Forms.ToolStripButton();
            this.btnHome = new System.Windows.Forms.ToolStripButton();
            this.btnClose = new System.Windows.Forms.ToolStripButton();
            this.txtAddress = new System.Windows.Forms.ToolStripTextBox();
            this.btnCustom = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.webView = new System.Windows.Forms.WebBrowser();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(284, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnBack,
            this.btnNext,
            this.btnReload,
            this.btnHome,
            this.btnClose,
            this.txtAddress,
            this.btnCustom});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(284, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnBack
            // 
            this.btnBack.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnBack.Image = ((System.Drawing.Image)(resources.GetObject("btnBack.Image")));
            this.btnBack.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(23, 22);
            this.btnBack.Text = "Back";
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click_1);
            // 
            // btnNext
            // 
            this.btnNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNext.Image = ((System.Drawing.Image)(resources.GetObject("btnNext.Image")));
            this.btnNext.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(23, 22);
            this.btnNext.Text = "Forward";
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click_1);
            // 
            // btnReload
            // 
            this.btnReload.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnReload.Image = ((System.Drawing.Image)(resources.GetObject("btnReload.Image")));
            this.btnReload.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnReload.Name = "btnReload";
            this.btnReload.Size = new System.Drawing.Size(23, 22);
            this.btnReload.Text = "Reload";
            this.btnReload.Click += new System.EventHandler(this.btnReload_Click_1);
            // 
            // btnHome
            // 
            this.btnHome.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnHome.Image = ((System.Drawing.Image)(resources.GetObject("btnHome.Image")));
            this.btnHome.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnHome.Name = "btnHome";
            this.btnHome.Size = new System.Drawing.Size(23, 22);
            this.btnHome.Text = "Home page";
            this.btnHome.Click += new System.EventHandler(this.btnHome_Click_1);
            // 
            // btnClose
            // 
            this.btnClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(23, 22);
            this.btnClose.Text = "Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click_1);
            // 
            // txtAddress
            // 
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(100, 25);
            this.txtAddress.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtAddress_KeyUp_1);
            // 
            // btnCustom
            // 
            this.btnCustom.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCustom.Image = ((System.Drawing.Image)(resources.GetObject("btnCustom.Image")));
            this.btnCustom.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCustom.Name = "btnCustom";
            this.btnCustom.Size = new System.Drawing.Size(23, 22);
            this.btnCustom.Click += new System.EventHandler(this.btnCustom_Click_1);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 239);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(284, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // webView
            // 
            this.webView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webView.Location = new System.Drawing.Point(0, 49);
            this.webView.MinimumSize = new System.Drawing.Size(20, 20);
            this.webView.Name = "webView";
            this.webView.Size = new System.Drawing.Size(284, 190);
            this.webView.TabIndex = 4;
            // 
            // WebBrowser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.webView);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "WebBrowser";
            this.Text = "WebBrowser";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnBack;
        private System.Windows.Forms.ToolStripButton btnNext;
        private System.Windows.Forms.ToolStripButton btnHome;
        private System.Windows.Forms.ToolStripButton btnReload;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.WebBrowser webView;
        private System.Windows.Forms.ToolStripTextBox txtAddress;
        private System.Windows.Forms.ToolStripButton btnCustom;
        private System.Windows.Forms.ToolStripButton btnClose;
    }
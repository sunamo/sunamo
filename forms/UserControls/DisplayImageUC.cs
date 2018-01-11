using System.Windows.Forms;
using System.Drawing;
using forms;
public class DisplayImageUC : UserControl
    {
        SplitContainer sc = new SplitContainer();
        TextBoxForms infoOmage = new TextBoxForms();
        PictureBox pbImage = new PictureBox();

        public DisplayImageUC(Bitmap image)
        {
            this.SuspendLayout();
            sc.SuspendLayout();
            sc.Dock = DockStyle.Fill;
            infoOmage.Dock = DockStyle.Fill;
            infoOmage.Multiline = true;
            pbImage.Width = image.Width;
            pbImage.Height = image.Height;
            sc.Panel2.Controls.Add(infoOmage);
            sc.Panel1.Controls.Add(pbImage);

            infoOmage.Text = forms.Pictures.InfoAbout(image);
            pbImage.Image = image;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(sc);
            sc.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        protected override void OnResize(System.EventArgs e)
        {
            base.OnResize(e);

            // Pro jistotu to je nastaven� na vy��� ne� 200
            if (ClientSize.Width > 210)
            {

                sc.SplitterDistance = ClientSize.Width - 200;
                sc.Panel2Collapsed = false;
            }
            else
            {
                sc.Panel2Collapsed = true;
            }
        }
    }
//}

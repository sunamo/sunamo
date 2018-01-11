using System.Windows.Forms;
using System.Drawing;
using forms;

public class DisplayTextUC : UserControl
{
    SplitContainer sc = new SplitContainer();
    TextBoxForms txt = new TextBoxForms();
    TextBoxForms txt2 = new TextBoxForms();

    /// <summary>
    /// Kdy� A2 will be null, Panel2 will be collapsed
    /// </summary>
    /// <param name="text"></param>
    /// <param name="text2"></param>
    public DisplayTextUC(string text, string text2)
    {
        this.SuspendLayout();
        if (text2 == null)
        {
            txt.Dock = DockStyle.Fill;
            txt.Multiline = true;
            txt.Text = text;
            Controls.Add(txt);
        }
        else
        {
            sc.SuspendLayout();
            sc.Dock = DockStyle.Fill;
            if (text2 == null)
            {
                sc.Panel2Collapsed = true;
            }
            else
            {
                txt2.Text = text2;
            }

            txt.Dock = DockStyle.Fill;
            txt.Multiline = true;
            sc.Panel1.Controls.Add(txt);

            txt2.Dock = DockStyle.Fill;
            txt2.Multiline = true;
            sc.Panel2.Controls.Add(txt2);

            txt.Text = text;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(sc);
            sc.ResumeLayout(false);
        }
        this.ResumeLayout(false);
    }

    /// <summary>
    /// Nastavuje text pomoc� Invoke
    /// </summary>
    public string Content
    {
        get
        {
            
            return this.Invoke( IH.delegateGetTextInTextBoxForms, txt).ToString();
        }
        set
        {
            this.Invoke(IH.delegateSetTextInTextBoxForms, txt, value);
        }
    } 

    protected override void OnResize(System.EventArgs e)
    {
        base.OnResize(e);
        if (true)
        {
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
}

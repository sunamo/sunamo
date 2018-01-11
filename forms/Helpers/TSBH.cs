using System.Drawing;
using System.Windows.Forms;
namespace forms
{
    public class TSBH
    {
        public static System.Windows.Forms.ToolStripButton CreateNew(Image image, string text)
        {
            System.Windows.Forms.ToolStripButton tsb = new System.Windows.Forms.ToolStripButton();
            tsb.Text = text;
            tsb.Image = image;
            tsb.DisplayStyle = ToolStripItemDisplayStyle.Image;
            return tsb;
        }
    }
}

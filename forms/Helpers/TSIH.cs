using System.Windows.Forms;
using System.Collections.Generic;
namespace forms
{
    public class TSIH
    {
        public static ToolStripMenuItem[] ConvertFromArrayStringToTSMI(List<string> v)
        {
            List<ToolStripMenuItem> tsmis = new List<ToolStripMenuItem>();
            foreach (string item in v)
            {
                tsmis.Add(TSMIH.CreateNew(item));
            }
            return tsmis.ToArray();
        }

        public static ToolStripItem[] ConvertFromArrayStringToTSI(List<string> v)
        {
            List<ToolStripItem> tsmis = new List<ToolStripItem>();
            foreach (string item in v)
            {
                tsmis.Add(TSMIH.CreateNew(item));
            }
            return tsmis.ToArray();
        }
    }
}
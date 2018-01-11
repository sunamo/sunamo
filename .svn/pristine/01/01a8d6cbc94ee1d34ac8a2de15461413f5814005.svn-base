using System.Windows.Forms;
namespace forms
{
    /// <summary>
    /// T��da pro snadn�j�� u�it� ToolStripProgressBar
    /// </summary>
    public class TSPBH
    {
        ToolStripProgressBar tscb = null;
        float onePercent = 0;
        float last = 0;
        Form f = null;

        /// <summary>
        /// 
        /// </summary>
        public TSPBH(ToolStripProgressBar tscb, int pocetCelkove, Form f)
        {
            this.f = f;
            tscb.Value = 0;
            this.tscb = tscb;
            onePercent = 100 / pocetCelkove;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Hotovo()
        {
            last += onePercent;
            f.Invoke(IH.delegatePbarUpdate, tscb, (int)last);
            //IHS.updateProgressBarValue(tscb, (int)predchozi);
        }

        public void HotovoUplne()
        {
            f.Invoke(IH.delegatePbarUpdate, tscb, 100);
            //IHS.updateProgressBarValue(tscb, 100);
        }
    }
}

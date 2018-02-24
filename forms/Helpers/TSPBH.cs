using sunamo.Helpers.Number;
using System.Windows.Forms;
namespace forms
{
    /// <summary>
    /// T��da pro snadn�j�� u�it� ToolStripProgressBar
    /// </summary>
    public class TSPBH
    {
        ToolStripProgressBar tscb = null;
        PercentCalculator percentCalculator;
        Form f = null;

        /// <summary>
        /// 
        /// </summary>
        public TSPBH(ToolStripProgressBar tscb, int pocetCelkove, Form f)
        {
            this.f = f;
            tscb.Value = 0;
            this.tscb = tscb;
            percentCalculator = new PercentCalculator(pocetCelkove);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Hotovo()
        {
            percentCalculator.last += percentCalculator.onePercent;
            f.Invoke(IH.delegatePbarUpdate, tscb, (int)percentCalculator.last);
            //IHS.updateProgressBarValue(tscb, (int)predchozi);
        }

        public void HotovoUplne()
        {
            f.Invoke(IH.delegatePbarUpdate, tscb, 100);
            //IHS.updateProgressBarValue(tscb, 100);
        }
    }
}

/// <summary>
/// 
/// </summary>
using System.Windows.Forms;
using System.Collections.Generic;
using sunamo;

namespace forms
{
    /// <summary>
    /// POmocna trida pro praci s vice checkboxy(ale ne CheckedListBox)
    /// </summary>
    public class CHBH
    {
        public CHBH()
        {

        }

        public static void SetChecked(Control control, bool p)
        {
            List<CheckBox> chbs = OPH.GetRecursiveAllSubControlsOfType<CheckBox>(control);
            foreach (CheckBox var in chbs)
            {
                var.Checked = p;
            }


        }

        public static CheckState GetCheckState(List<bool> create)
        {
            bool[] create2 = create.ToArray();
            if (BTS.IsAllEquals(true, create2))
            {
                return CheckState.Checked;
            }
            else
            {
                if (BTS.IsAllEquals(false, create2))
                {
                    return CheckState.Unchecked;
                }
                else
                {
                    return CheckState.Indeterminate;
                }
            }
        }
    }
}

/// <summary>
/// 
/// </summary>
using System.Windows.Forms;
using System.Collections.Generic;
using sunamo;

namespace forms
{
    /// <summary>
    /// POmocn� t��da pro pr�ci s v�ce checkboxy(ale ne CheckedListBox)
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
            if (BT.IsAllEquals(true, create2))
            {
                return CheckState.Checked;
            }
            else
            {
                if (BT.IsAllEquals(false, create2))
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

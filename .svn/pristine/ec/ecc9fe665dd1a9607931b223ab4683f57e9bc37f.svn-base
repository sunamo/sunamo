using System.Windows.Forms;
using System.Collections.Generic;
namespace forms
{
    public class OPH
    {
        public static List<Control> GetThisAndRecursiveAllSubControls(Control f)
        {
            List<Control> vr = new List<Control>();
            vr.Add(f);

            foreach (Control item in f.Controls)
            {
                GetThisAndRecursiveAllSubControls(f, vr);
            }
            return vr;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="f"></param>
        /// <param name="vr"></param>
        private static void GetThisAndRecursiveAllSubControls(Control f, List<Control> vr)
        {
            vr.Add(f);

            foreach (Control item in f.Controls)
            {
                GetThisAndRecursiveAllSubControls(item, vr);
            }
        }

        private static void GetThisAndRecursiveAllSubControls<T>(Control f, List<T> vr) where T : class
        {
            if (f.GetType() == typeof(T))
            {
                vr.Add(f as T);
            }

            foreach (Control item in f.Controls)
            {
                GetThisAndRecursiveAllSubControls(item, vr);
            }
        }

        public static List<T> GetRecursiveAllSubControlsOfType<T>(Control control) where T : class
        {
            List<T> vr = new List<T>();
            foreach (Control item in control.Controls)
            {
                GetThisAndRecursiveAllSubControls<T>(item, vr);
            }
            return vr;
        }

        /// <summary>
        /// Mus�m A1 vy�e�it takto, proto�e je poka�d� jinak typovan�: ListBox.ObjectCollection eg.
        /// </summary>
        /// <param name="countItems"></param>
        /// <param name="index"></param>
        public static bool IsInRange(int countItems, int index)
        {
            int od = 0;
            int to = countItems;
            return od <= index && to > index;
        }
    }
}

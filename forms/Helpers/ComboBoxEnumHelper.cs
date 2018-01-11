using System.Windows.Forms;
using System;
namespace forms
{
    public class ComboBoxEnumHelper<T>
    {
        ComboBox cb = null;

        #region base
        public ComboBoxEnumHelper(ComboBox cb)
        {
            this.cb = cb;
            PridejPolozky();
        }
        #endregion

        #region ICB<T> Members

        public T VratNastavene()
        {
            return (T)Enum.Parse(typeof(T), cb.SelectedItem.ToString());
        }

        public void NastavHodnotuVyctu(T sablonyProjektu)
        {
            for (int i = 0; i < cb.Items.Count; i++)
            {
                string gg = cb.Items[i].ToString();
                if (gg == sablonyProjektu.ToString())
                {
                    cb.SelectedIndex = i;
                    break;
                }
            }
        }

        void PridejPolozky()
        {
            cb.DropDownStyle = ComboBoxStyle.DropDownList;
            foreach (string item in Enum.GetNames(typeof(T)))
            {
                if (!cb.Items.Contains(item))
                {
                    cb.Items.Add(item);
                }
            }
            cb.SelectedIndex = 0;
            cb.Text = cb.Items[0].ToString();
            //}
        }

        #endregion

        public void RemoveEnumItem(T var)
        {
            string[] jmena = Enum.GetNames(typeof(T));
            for (int i = 0; i < jmena.Length; i++)
            {
                if (jmena[i] == var.ToString())
                {
                    cb.Items.RemoveAt(i);
                    return;
                }
            }
        }

        /// <summary>
        /// Nemus� volat tuto metodu, sta�� bohat� pou��t cb.SelectedIndex = p;
        /// </summary>
        /// <param name="p"></param>
        public void NastavIndexVyctu(int p)
        {
            cb.SelectedIndex = p;
        }
    }
}

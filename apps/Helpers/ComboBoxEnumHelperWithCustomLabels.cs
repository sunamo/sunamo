//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Windows.UI.Xaml.Controls;

//namespace apps
//{
//    public class ComboBoxEnumHelperWithCustomLabels<T> : ComboBoxEnumHelperBase<T>
//    {
//        Dictionary<string, string> d = null;

//        public ComboBoxEnumHelperWithCustomLabels(ComboBox cb, Dictionary<string, string> d) : base(cb)
//        {
//            this.d = d;
//            AddItems();
//        }

//        public override T GetSelected()
//        {
//            string cbi = cb.SelectedItem.ToString();
//            foreach (var item in d)
//            {
//                if (item.Value == cbi)
//                {
//                    return (T)Enum.Parse(typeof(T), item.Key);
//                }
//            }
//            return default(T);
//        }

//        public override void RemoveItem(T t)
//        {
//            string cbi = d[t.ToString()];
//            for (int i = 0; i < cb.Items.Count; i++)
//            {
//                string gg = cb.Items[i].ToString();
//                if (gg == cbi)
//                {
//                    cb.Items.RemoveAt(i);
//                    break;
//                }
//            }
//        }

//        public override void SetValue(string cbi)
//        {
            
//            for (int i = 0; i < cb.Items.Count; i++)
//            {
//                string gg = cb.Items[i].ToString();
//                if (gg == cbi)
//                {
//                    cb.SelectedIndex = i;
//                    break;
//                }
//            }
//        }

//        public override void SetValue(T sablonyProjektu)
//        {
//            string cbi = d[sablonyProjektu.ToString()];
//            SetValue(cbi);
//        }

//        protected override void AddItems()
//        {
//            foreach (string item in Enum.GetNames(typeof(T)))
//            {
//                    cb.Items.Add(d[ item.ToString()]);
//                //}
//            }
//            cb.SelectedIndex = 0;
//        }
//    }
//}

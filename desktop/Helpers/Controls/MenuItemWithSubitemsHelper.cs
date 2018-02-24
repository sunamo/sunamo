using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
/// <summary>
/// Třída s generickým typem je MenuItemWithSubitemsHelperT
/// Používá se pro automatické zaškrtávání posledního a zjištění která hodnota byla zaškrtnuta
/// </summary>
public class MenuItemWithSubitemsHelper
{
    protected MenuItem tsddb = null;
        protected MenuItem prev = new MenuItem();
        protected string originalToolTipText = "";
    public event EventHandler MenuItemChecked;
        object selectedO = null;
        bool mnoho = false;
        /// <summary>
        /// Objekt, ve kterém je vždy aktuální zda v tsddb něco je
        /// Takže se nelekni že to je promměná
        /// </summary>
        public object SelectedO
        {
            get
            {
                return selectedO;
            }
            set
            {
                selectedO = value;
                if (!mnoho)
                {
                    foreach (MenuItem item in tsddb.Items)
                    {
                        if (tagy)
                        {
                            if (item.Tag.ToString() == value.ToString())
                            {
                                item.IsChecked = true;
                            }
                        }
                        else
                        {
                            if (item == value)
                            {
                                item.IsChecked = true;
                            }
                        }
                    }
                }
            }
        }



    public void AddValuesOfEnumAsItems<T>(object defVal)
    {
        Dictionary<T, string> d = new Dictionary<T, string>();
        Type type = typeof(T);
        var en = Enum.GetValues(type);
        foreach (T item in en)
        {
            d.Add(item, item.ToString());
            //AddMenuItem(item);
        }

        AddValuesOfEnumAsItems<T>(d, defVal);

    }

    public void AddValuesOfEnumAsItems<T>(Dictionary<MenuItem, T> d, object defVal, MenuItem defMi)
    {
        Type type = typeof(T);
        if (defVal != null)
        {
            if (type.FullName != defVal.GetType().FullName)
            {
                throw new Exception("Parameter defVal in MenuItemWithSubitemsHelper.AddValuesOfEnumAsItems was not type of Enum.");
            }
        }

        T _def = (T)defVal;

        foreach (var item in d)
        {
            item.Key.Tag = item.Value;
            AddMenuItem(item.Key);
        }

        if (defVal != null)
        {
            SelectedO = defVal;
        }

        prev = defMi;
    }

    public void AddValuesOfEnumAsItems<T>(Dictionary<T, string> d, object defVal)
    {
        Type type = typeof(T);
        if (defVal != null)
        {
            if (type.FullName != defVal.GetType().FullName)
            {
                throw new Exception("Parameter defVal in MenuItemWithSubitemsHelper.AddValuesOfEnumAsItems was not type of Enum.");
            }
        }

        T _def = (T)defVal;

        foreach (var item in d)
        {
            AddMenuItem(item.Key, item.Value);
        }

        if (defVal != null)
        {
            SelectedO = defVal;
        }
    }

    private MenuItem AddMenuItem(object tag, string header)
    {
        MenuItem tsmi = new MenuItem();
        tsmi.Header = header;
        tsmi.Tag = tag;
        AddMenuItem(tsmi);
        return tsmi;
    }

    private void AddMenuItem(MenuItem tsmi)
    {
        tsmi.Click += new RoutedEventHandler(tsmi_Click);
        tsddb.Items.Add(tsmi);
    }

    /// <summary>
    /// Používá se pokud chci porovnávat rychleji na reference MenuItem ale chci zjistit Tag zvolené položky.
    /// </summary>
    /// <returns></returns>
    public object SelectedTag()
    {
        if (Selected)
        {
            return ((MenuItem)SelectedO).Tag;
        }
        return null;
    }

    public bool zaskrtavat = false;

        public bool Selected
        {
            get
            {
            if (SelectedO != null)
            {
                return SelectedO.ToString().Trim() != "";
            }
            return false;
            //return SelectedO != null;
        }
        }

        public string SelectedS
        {
            get
            {
                return SelectedO.ToString();
            }
        }

        public void AddValuesOfEnumAsItems(Array bs, bool zaskPrvni)
        {
            tsddb.Items.Clear();
            int i = 0;
            foreach (object item in bs)
            {
            MenuItem tsmi = AddMenuItem(item, item.ToString());
            if (zaskPrvni)
            {
                if (i == 0)
                {
                    tsmi.IsChecked = true;
                    prev = tsmi;
                }
            }
            i++;
            }
        }

        public void tsmi_Click(object sender, RoutedEventArgs e)
        {
            prev.IsChecked = false;
            MenuItem tsmi = (MenuItem)sender;
            if (zaskrtavat)
            {
                tsmi.IsChecked = true;
                prev = tsmi;
            }
            if (tagy)
            {
                selectedO = tsmi.Tag;
            }
            else
            {
                selectedO = tsmi;
            }
            tsddb.ToolTip = originalToolTipText + " " + SelectedO.ToString();

        if (MenuItemChecked != null)
        {
            MenuItemChecked(sender, e);
        }
        }

        public void AddValuesOfArrayAsItems(RoutedEventHandler eh,  object[] o)
        {
            tsddb.Items.Clear();
            int i = 0;
            foreach (object item in o)
            {
                MenuItem tsmi = AddMenuItem(item, item.ToString());
                tsmi.Click += eh;
                
                i++;
            }
        }

        public void AddValuesOfArrayAsItems(ICommand eh, object[] o)
        {
            tsddb.Items.Clear();
            int i = 0;
            foreach (object item in o)
            {
            MenuItem tsmi = AddMenuItem(item, item.ToString());

            tsmi.Command = eh;
                tsmi.CommandParameter = item;
                
                i++;
            }
        }

        public void AddValuesOfArrayAsItems(RoutedCommand cmd0, object[] p, RoutedCommand cmd1, List<StringBuilder> stovky, RoutedCommand cmd2, List<StringBuilder> desitky, RoutedCommand cmd3, List<StringBuilder> jednotky)
        {
            mnoho = true;
            int pristePokracovatDesitky = 0;
            tsddb.Items.Clear();
            int i = 0;
            foreach (object item in p)
            {
                pristePokracovatDesitky = 0;
                string category = item.ToString();
                string categoryPipe = category + "|";
                MenuItem tsmi = new MenuItem();
                tsmi.Header = category;

                string[] stovkyDivided = SH.Split(stovky[i].ToString(), '|');

                List< String> stovkyActual = new List<String>();
                StringBuilder stovkyActualTemp = new StringBuilder();
                for (int y = 0; y < stovkyDivided.Length; y++)
                {
                    if ((y) % 100 == 0 && y != 0)
                    {
                        stovkyActual.Add(stovkyActualTemp.ToString());
                        stovkyActualTemp.Clear();
                        stovkyActualTemp.Append(stovkyDivided[y] + ",");
                    }
                    else
                    {
                        stovkyActualTemp.Append(stovkyDivided[y] + ",");
                    }
                    //
                }
                int pristePokracovatJednotky = 0;
                
                int pristePokracovatStovky = 0;
                stovkyActual.Add(stovkyActualTemp.ToString());
                
                //int aktualniIndexStovky = 0;
                foreach (var idcka in stovkyActual)
                {

                    MenuItem tsmiStovky = new MenuItem();
                    tsmiStovky.Header = (pristePokracovatStovky + 1).ToString() + " - " + (pristePokracovatStovky + SH.Split(idcka, ",").Length).ToString() ;
                    
                    List< List<MenuItem>> kVlozeniDoDesitky = new List< List<MenuItem>>();
                    List<StringBuilder> idckaDesitky = new List<StringBuilder>();
                    kVlozeniDoDesitky.Add(new List<MenuItem>());
                    idckaDesitky.Add(new StringBuilder());
                    string[] jednotkyDivided = SH.Split(idcka, ",");
                    int indexNaKteryUkladatDesitky = 0;
                    foreach (var jednotka in jednotkyDivided)
                    {
                        MenuItem tsmiJednotky = new MenuItem();
                        tsmiJednotky.Header = (pristePokracovatJednotky + 1).ToString();
                        pristePokracovatJednotky++;
                        tsmiJednotky.Command = cmd3;
                        tsmiJednotky.CommandParameter = tsmiJednotky.Header.ToString() + "|" + categoryPipe + jednotka;

                        var  o = (pristePokracovatJednotky - 1);
                        if (o % 10 == 0 && o % 100 != 0 && o != 0)
                        {
                            indexNaKteryUkladatDesitky++;
                            kVlozeniDoDesitky.Add(new List<MenuItem>());
                            idckaDesitky.Add(new StringBuilder());        
                        }
                        kVlozeniDoDesitky[indexNaKteryUkladatDesitky].Add(tsmiJednotky);
                        idckaDesitky[indexNaKteryUkladatDesitky].Append(jednotka + ",");
                    }

                    for (int t = 0; t < kVlozeniDoDesitky.Count; t++)
                    {
                        if (kVlozeniDoDesitky[kVlozeniDoDesitky.Count- 1].Count == 0)
                        {
                            int rat = kVlozeniDoDesitky.Count- 1;
                            kVlozeniDoDesitky.RemoveAt(rat);
                            idckaDesitky.RemoveAt(rat);
                        }
                    }
                    
                    int e = 0;
                    foreach (var item3 in kVlozeniDoDesitky)
                    {
                        var u =idckaDesitky[e].ToString();
                        e++;
                        string[] desitkyPouze = SH.Split(u, ",");
                        MenuItem tsmiDesitky = new MenuItem();
                        tsmiDesitky.Header = (pristePokracovatDesitky + 1).ToString() + " - " + (pristePokracovatDesitky + desitkyPouze.Length).ToString();
                        foreach (var item4 in item3)
                        {
                            tsmiDesitky.Items.Add(item4);
                        }

                        MenuItem tsmiDesitky2 = new MenuItem();
                        tsmiDesitky2.Header = (pristePokracovatDesitky + 1).ToString() + " - " + (pristePokracovatDesitky + desitkyPouze.Length).ToString();
                        tsmiDesitky2.Command = cmd2;
                        //  
                        tsmiDesitky2.CommandParameter = tsmiDesitky2.Header.ToString() + "|" + categoryPipe + u;

                        tsmiStovky.Items.Add(tsmiDesitky2);

                        tsmiStovky.Items.Add(tsmiDesitky);
                        pristePokracovatDesitky+= 10;
                    }

                    MenuItem tsmiStovky2 = new MenuItem();
                    tsmiStovky2.Header = (pristePokracovatStovky + 1).ToString() + " - " + (pristePokracovatStovky + SH.Split(idcka, ",").Length).ToString();

                    pristePokracovatStovky += 100;
                    tsmiStovky2.Command = cmd1;
                    //
                    tsmiStovky2.CommandParameter =tsmiStovky2.Header.ToString() + "|" + categoryPipe  + idcka;
                    tsmi.Items.Add(tsmiStovky2);

                    tsmi.Items.Add(tsmiStovky);
                }

                MenuItem tsmi2 = new MenuItem();
                tsmi2.Header = category;
                tsmi2.Command = cmd0;
                tsmi2.CommandParameter = category;

                tsddb.Items.Add(tsmi2);

                tsmi.IsEnabled = true;
                tsddb.Items.Add(tsmi);
                i++;
            }
        }

        public void AddValuesOfIntAsItems(RoutedEventHandler eh, int initialValue, int resizeOf, int degrees)
        {
            tsddb.Items.Clear();
            int akt = initialValue;
            List<int> pred = new List<int>();
            for (int i = 0; i < degrees; i++)
            {
                akt -= resizeOf;
                pred.Add(akt);

            }
            pred.Reverse();
            akt = initialValue;
            List<int> po = new List<int>();
            for (int i = 0; i < degrees; i++)
            {
                akt += resizeOf;
                po.Add(akt);
            }
            List<int> o = new List<int>();
            o.AddRange(pred);
            o.Add(initialValue);
            o.AddRange(po);
            int y = 0;
            foreach (int item in o)
            {
                MenuItem tsmi = new MenuItem();
                tsmi.Header = item.ToString();
                tsmi.Tag = item;
                tsmi.Click += tsmi_Click;
                tsmi.Click += eh;
                tsddb.Items.Add(tsmi);
                y++;
            }
        }

        bool tagy = true;

    /// <summary>
        /// A2 zda se má do SelectedO uložit tsmi.Tag nebo jen tsmi
    /// </summary>
    /// <param name="tsddb"></param>
    /// <param name="tagy"></param>
        public MenuItemWithSubitemsHelper(MenuItem tsddb, bool tagy)
        {
            this.tsddb = tsddb;
            this.tagy = tagy;
        }

        public MenuItemWithSubitemsHelper(MenuItem tsddb)
        {
            this.tsddb = tsddb;
            tagy = true;
        }
}

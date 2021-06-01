using desktop.Helpers;
using sunamo.Data;
using sunamo.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace desktop.Controls.Visualization
{
    /// <summary>
    /// T je typ ve kterém se spravují data k UIElements nebo checkboxes
    /// Its is normal grid, in default state there is no padding / margin in cell
    /// Ways to show data in WPF - see where is used in D:\Sync\Develop of Future\UsedTableControls.txt
    /// TwoWayTable
    /// DataGrid
    /// ListView
    /// ListBox
    /// GridView - If you are looking for a GridView control in WPF, you will be disappointed. WPF does not have a GridView control. But the good news is you can achieve GridView like functionality with a ListView control available in WPF. 
    /// https://www.c-sharpcorner.com/UploadFile/mahesh/gridview-in-wpf/
    /// 
    /// Use in apps:
    /// IlCamminoManager - DataGridCamminoTracklist, IlCamminoManager,  SearchingIlCammino , ListenToIlCammino 
    /// 
    /// WpfApp1 - EncodingOfFiles
    /// LastFmClient/LastFmClient - artists/songs
    /// GeoCachingTool/SavedCaches
    /// SeznamCzReality/ListViewFlats
    /// SunamoYouTube
    /// 
    /// </summary>
    public partial class TwoWayTable : UserControl
    {
        const double marginInCell = 8;

        #region region for all code to easy transfer to another code
        /// <summary>
        /// Bez započítání top
        /// </summary>
        int rows = 0;
        /// <summary>
        /// Bez započítání left
        /// </summary>
        int columns = 0;
        /// <summary>
        /// První rozměr jsou řádky, druhý sloupce
        /// </summary>
        UIElement[,] controls = null;
        /// <summary>
        /// První rozměr jsou řádky, druhý sloupce
        /// Is initialized only when dataCellWrapper == AddBeforeControl.CheckBox
        /// </summary>
        CheckBox[,] checkBoxes = null;

        public void DoSave()
        {
            foreach (var item in leftChbs)
            {
                if (displayEntity != string.Empty)
                {
                    //if (Save != null)
                    //{
                        Twt_Save(this, displayEntity, item);
                    //}
                }
            }
        }

        /// <summary>
        /// In A1 is displayEntity
        /// </summary>
        /// <param name="obj"></param>
        private void Twt_Save(TwoWayTable sender, string site, string page)
        {
            var path = AppData.ci.GetFile(AppFolders.Controls, SH.Join(AllStrings.lowbar, sender.Name, site, page));

            #region Get isChecked from row

            var ele = sender.GetCheckBoxesInRow(page);
            var isChecked = ele.Select(d => d.IsChecked.Value);
            var ints = new List<int>(isChecked.Count());

            foreach (var item in isChecked)
            {
                ints.Add(BTS.BoolToInt(item));
            }
            #endregion

            if (site == "Lyr")
            {

            }

            var content = SH.Join(AllChars.comma, ints);

            //Set(sender, checkBoxes, content);
            //SaveControl(sender);

            // better is save simpli and no use adc
            TF.WriteAllText(path, content);
        }

        /// <summary>
        /// První rozměr jsou řádky, druhý sloupce
        /// Every cell data has own data
        /// </summary>
        object[,] data = null;

        public int Rows
        {
            get
            {
                return rows;
            }
        }

        public int Columns
        {
            get
            {
                return columns;
            }
        }

        public T GetDataAt<T>(int row, int column)
        {
            return (T)data[row, column];
        }

        public bool? IsCheckedAt(int row, int column)
        {
            return checkBoxes[row, column].IsChecked;
        }

        public TwoWayTable()
        {
            InitializeComponent();

            Loaded += TwoWayTable_Loaded;
        }

        private void TwoWayTable_Loaded(object sender, RoutedEventArgs e)
        {
#if DEBUG
            //if (grid.Children.Count > 0)
            //{
            //    var d = grid.Children[7];
            //    var fw = d as FrameworkElement;
            //    var s = fw.ActualHeight;
            //}
#endif
        }

        public void CreateGrid(int row, int column)
        {
            row++;
            column++;
            checkBoxes = null;
            data = null;
            if (dataCellWrapper == AddBeforeControl.CheckBox)
            {
                checkBoxes = new CheckBox[row, column];
            }

            controls = new UIElement[row, column];
            data = new object[row, column];
            rows = row;
            columns = column;
            ClearGridChildren();
            
            GridHelper.GetAutoSize(grid, column, row);
        }
        
        public void ClearGridChildren()
        {
            grid.Children.Clear();
        }

        /// <summary>
        /// key - twt_Geo etc
        /// value = key - name of aspx, value - checked
        /// </summary>
        /// <param name="twt"></param>
        /// <returns></returns>
        public static Dictionary<string, Dictionary<string, List<bool>>> GetRowsIsChecked(TwoWayTable twt)
        {
            var begin = twt.Name + "_";
            var folder = AppData.ci.GetFolder(AppFolders.Controls);
            var files = FS.GetFiles(folder, begin + "*", System.IO.SearchOption.TopDirectoryOnly);

            Dictionary<string, Dictionary<string, List<bool>>> s = new Dictionary<string, Dictionary<string, List<bool>>>();

            // In web will be window always null
            // See comments in IWindowWithSettingsManager
            var window = (IWindowWithSettingsManager)WpfApp.mp;
            var Data = window.Data;
            var data = Data.data;

            foreach (var item in files)
            {
                var fn = FS.GetFileName(item);

                fn = fn.Substring(begin.Length);

                string key = begin + fn;

                var webPage = SH.Split(fn, AllStrings.lowbar);
                var web = webPage[0];
                var page = webPage[1];
                var dKey = begin + web;

                var text = TF.ReadFile(item);

                #region MyRegion
                //ApplicationDataContainerList adcl = null;

                //// Automatically load
                //if (!data.ContainsKey(key))
                //{
                //    var v = new ApplicationDataContainerList(item);
                //    adcl = Data.AddFrameworkElement(key, v);
                //}
                //else
                //{
                //    adcl = data[key];
                //}

                // , for delimiting values in row, " " for entire new row
                //var text = adcl.GetString(ApplicationDataConsts.checkBoxes); 
                #endregion

                if (item.Contains("Lyr"))
                {

                }

                var cells = SH.Split(text, ",");
                var numbers = CA.ToNumber<int>(int.Parse, cells);

                var bools = CA.ToBool(numbers) ;

                Dictionary<string, List<bool>> dict = null;
                if (s.ContainsKey(dKey))
                {
                    dict = s[dKey];
                }
                else
                {
                    dict = new Dictionary<string, List<bool>>();
                    s.Add(dKey, dict);
                }

                DictionaryHelper.AddOrSet<string, List<bool>>(dict, page, bools);

                #region Adding into checkedCells
                //Dictionary<string, List<bool>> dict = null;
                //if (twt.checkedCells.ContainsKey(dKey))
                //{
                //    dict = twt.checkedCells[dKey];
                //}
                //else
                //{
                //    dict = new Dictionary<string, List<bool>>();
                //    twt.checkedCells.Add(dKey, dict);
                //}
                //dict.Add(page, bools.ToList()); 
                #endregion
            }

            return s;
        }

        public void ReRender()
        {
            base.OnRenderSizeChanged(new SizeChangedInfo(this, this.RenderSize, true, true));
        }

        /// <summary>
        /// Stupid, better is have everything in grid
        /// Is loaded during startup
        /// Is use when web is changed, load data from txt file
        /// In key is text in format nameFw.nameOfActualContent
        /// In value key is left column
        /// In value value is checked
        /// </summary>
        //public Dictionary<string, Dictionary<string, List<bool>>> checkedCells = new Dictionary<string, Dictionary<string, List<bool>>>();

        public List<CheckBox> GetCheckBoxesInRow(string dex)
        {
            return GetCheckBoxesInRow(leftChbs.IndexOf(dex));
        }

        public  List<CheckBox> GetCheckBoxesInRow(int dex)
        {
            var ele = GridHelper.GetControlsFrom<CheckBox>(grid, true, dex).ToList();
            ele.RemoveAt(ele.Count -1);
            return ele;
        }

        AddBeforeControl dataCellWrapper = AddBeforeControl.None;
        string displayEntity = string.Empty;
        //public event Action<TwoWayTable, string, string> Save;
        /// <summary>
        /// For saving data for every table
        /// </summary>
        public string DisplayEntity
        {
            get
            {
                return displayEntity;
            }
            set
            {
                displayEntity =  value;

                var checkedCells = TwoWayTable.GetRowsIsChecked(this);

                var key = this.Name + AllStrings.lowbar + displayEntity;

                if (checkedCells.ContainsKey(key))
                {
                    Dictionary<string, List<bool>> s = checkedCells[key];

                    foreach (var item in s)
                    {
                        int dex = GetIndexOfRow(item.Key);

                        if (dex != -1)
                        {
                            var ele = GetCheckBoxesInRow(dex);

                            for (int i = 0; i < item.Value.Count; i++)
                            {
                                var el = ele[i];
                                var isChecked = item.Value[i];
                                if (displayEntity == "Lyr")
                                {
                                    if (isChecked)
                                    {

                                    }
                                    el.IsChecked = isChecked;
                                }
                                else
                                {
                                    el.IsChecked = isChecked;
                                }
                            }
                        }

                    }
                }
            }
        }

        private int GetIndexOfRow(string key)
        {
            var result = leftChbs.IndexOf(key);
            return result;
        }

        public AddBeforeControl DataCellWrapper
        {
            get
            {
                return dataCellWrapper;
            }
            set
            {
                dataCellWrapper = value;
            }
        }

        public double maxHeightRowBorder = 30;

        /// <summary>
        /// Can be use AddColumn or AddRow in dependent how I have structured data
        /// A1 is columns in table
        /// In A2 is control and tick (if DataCellWrapper == AddBeforeControl.CheckBox). Can be null
        /// A3 = data to control, cant be null if element in A2 is not null
        /// </summary>
        /// <param name="dexCol"></param>
        /// <param name="uie"></param>
        /// <param name="o"></param>
        public void AddColumn(int dexCol, List<CheckBoxData<UIElement>> uie, List< object> o)
        {
            for (int i = 0; i < uie.Count; i++)
            {
                UIElement item = null;
                if (uie[i] != null)
                {
                    item = uie[i].t;
                }

                if (i % 2 == 1)
                {
                    //Control fw = item as Control;
                    //DebugLogger.Instance.WriteLine(item.GetType().Name);
                    //if (fw != null)
                    //{
                    //    //fw.Background = Brushes.LightGray;
                    //}

                }

                if (item != null)
                {
                    Border b = new Border();
                    b.Height = maxHeightRowBorder;
                    b.Padding = new Thickness(5);
                    b.Child = item;
                    // Must be transparent, it's only around inner control (which is often empty), not complex with checkbox
                    b.BorderBrush = Brushes.Transparent;
                    b.BorderThickness = new Thickness( 1);
                    item = b;
                }

                

                controls[i, dexCol] = item;

                if (item != null)
                {
                    data[i, dexCol] = o[i];
                    if (DataCellWrapper == AddBeforeControl.CheckBox)
                    {
                        CheckBox chb = new CheckBox();
                        chb.VerticalAlignment = VerticalAlignment.Center;
                        chb.VerticalContentAlignment = VerticalAlignment.Center;
                        chb.Content = item;
                        chb.IsChecked = uie[i].tick;
                        
                        

                        Grid.SetColumn(chb, dexCol + 1);
                        Grid.SetRow(chb, i + 1);
                        grid.Children.Add(chb);

                        checkBoxes[i, dexCol] = chb;
                    }
                    else
                    {
                        //item.VerticalAlignment = VerticalAlignment.Center;
                        //item.VerticalContentAlignment = VerticalAlignment.Center;
                        Grid.SetColumn(item, dexCol + 1);
                        Grid.SetRow(item, i + 1);
                        grid.Children.Add(item);
                    }
                }
            }
        }

        /// <summary>
        /// Can be use AddColumn or AddRow in dependent how I have structured data
        /// </summary>
        /// <param name="dexRow"></param>
        /// <param name="uie"></param>
        /// <param name="o"></param>
        public void AddRow(int dexRow, CheckBoxData<UIElement>[] uie, object[] o)
        {
            if (uie.Length + 1 != columns)
            {
                return;
            }

            for (int i = 0; i < uie.Length; i++)
            {
                UIElement item = null;
                if (uie[i] != null)
                {
                    item = uie[i].t;
                }

                controls[dexRow, i] = item;
                if (item != null)
                {
                    data[dexRow, i] = o[i];
                    if (DataCellWrapper == AddBeforeControl.CheckBox)
                    {
                        CheckBox chb = new CheckBox();
                        chb.Content = item;
                        chb.IsChecked = uie[i].tick;


                        FrameworkElementHelper.SetMargin(chb, marginInCell);

                        Grid.SetColumn(chb, i + 1);
                        Grid.SetRow(chb, dexRow + 1);
                        grid.Children.Add(chb);

                        checkBoxes[dexRow, i] = chb;
                    }
                    else
                    {
                        FrameworkElementHelper.SetMargin(item, marginInCell);

                        Grid.SetColumn(item, i + 1);
                        Grid.SetRow(item, dexRow + 1);
                        grid.Children.Add(item);

                        
                    }
                }
            }

        }

        public void AddTop(params CheckBoxData<UIElement>[] uie)
        {
            AddTop(uie.ToList());
        }

        public void AddTop(IEnumerable<CheckBoxData<UIElement>> uie)
        {
            int i = 0;
            foreach (var item2 in uie)
            {
                UIElement item = item2.t;
                if (dataCellWrapper == AddBeforeControl.CheckBox)
                {
                    CheckBox top = new CheckBox();
                    top.Tag = i;
                    top.Click += Top_Click;

                    top.Content = item;
                    item = top;
                }
                Grid.SetColumn(item, i + 1);
                Grid.SetRow(item, 0);
                grid.Children.Add(item);
                i++;
            }
        }

        private void Top_Click(object sender, RoutedEventArgs e)
        {
            CheckBox chb = (CheckBox)sender;
            int tagOfCheckBox = (int)chb.Tag;
            bool isChecked = ((bool)chb.IsChecked);

            for (int i = 0; i < checkBoxes.GetLength(0); i++)
            {
                var dr = checkBoxes[i, tagOfCheckBox];
                if (dr != null)
                {
                    dr.IsChecked = isChecked;
                }
            }
        }
        public void AddLeft( params CheckBoxData<UIElement>[] uie)
        {
            AddLeft( uie.ToList());
        }

        List<string> leftChbs = new List<string>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="separateGrid"></param>
        /// <param name="uie"></param>
        public void AddLeft( IEnumerable<CheckBoxData<UIElement>> uie)
        {
            leftChbs.Clear();

            int i = 0;
            foreach (var item2 in uie)
            {
                UIElement item = item2.t;

                var s = item.GetContent();

                if (s != null)
                {
                    leftChbs.Add(s.ToString());
                }
                else
                {
                    leftChbs.Add(string.Empty);
                }

                if (dataCellWrapper == AddBeforeControl.CheckBox)
                {
                    CheckBox left = new CheckBox();
                    left.Tag = i;
                    left.Click += Left_Click;

                    left.Content = item;
                    item = left;


                }

                Grid.SetColumn(item, 0);
                Grid.SetRow(item, i + 1);
               
                grid.Children.Add(item);
                
                i++;
            }
        }

        private void Left_Click(object sender, RoutedEventArgs e)
        {
            CheckBox chb = (CheckBox)sender;
            int tagOfCheckBox = (int)chb.Tag;
            bool isChecked = ((bool)chb.IsChecked);

            for (int i = 0; i < checkBoxes.GetLength(1); i++)
            {
                var dr = checkBoxes[tagOfCheckBox, i];
                if (dr != null)
                {
                    dr.IsChecked = isChecked;
                }
            }
        } 
        #endregion
    }
}
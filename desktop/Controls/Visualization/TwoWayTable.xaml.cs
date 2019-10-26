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

        private void ClearGridChildren()
        {
            grid.Children.Clear();
        }

        AddBeforeControl dataCellWrapper = AddBeforeControl.None;
        /// <summary>
        /// For saving data for every table
        /// </summary>
        public string DisplayEntity;

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

        /// <summary>
        /// Can be use AddColumn or AddRow in dependent how I have structured data
        /// A1 is columns in table
        /// In A2 is control and tick (if DataCellWrapper == AddBeforeControl.CheckBox). Can be null
        /// A3 = data to control, cant be null if element in A2 is not null
        /// </summary>
        /// <param name="dexCol"></param>
        /// <param name="uie"></param>
        /// <param name="o"></param>
        public void AddColumn(int dexCol, CheckBoxData<UIElement>[] uie, object[] o)
        {
            for (int i = 0; i < uie.Length; i++)
            {
                UIElement item = null;
                if (uie[i] != null)
                {
                    item = uie[i].t;
                }

                if (item != null)
                {
                    Border b = new Border();
                    b.Padding = new Thickness(5);
                    b.Child = item;
                    b.BorderBrush = Brushes.Transparent;
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="separateGrid"></param>
        /// <param name="uie"></param>
        public void AddLeft( IEnumerable<CheckBoxData<UIElement>> uie)
        {
            int i = 0;
            foreach (var item2 in uie)
            {
                UIElement item = item2.t;

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
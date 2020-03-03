using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Media;
namespace desktop
{
    public static class DataGridHelper
    {
        public static System.Windows.Controls.DataGridColumn NewTextColumn(string p, string bindingPath)
        {
            DataGridTextColumn c = new DataGridTextColumn();
            c.Header = p;
            c.Binding = new Binding(bindingPath);
            return c;
        }

        public static System.Windows.Controls.DataGridColumn NewCheckBoxColumn(string p, string bindingPath)
        {
            DataGridCheckBoxColumn c = new DataGridCheckBoxColumn();
            c.Header = p;
            c.Binding = new Binding(bindingPath);

            return c;
        }

        public static DataGridCell GetCell(DataGrid dataGrid1, int row, int column)
        {
            DataGridRow rowContainer = GetRow(dataGrid1, row);

            if (rowContainer != null)
            {
                DataGridCellsPresenter presenter = GetVisualChild<DataGridCellsPresenter>(rowContainer);

                DataGridCell cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(column);
                if (cell == null)
                {
                    dataGrid1.ScrollIntoView(rowContainer, dataGrid1.Columns[column]);
                    cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(column);
                }
                return cell;
            }
            return null;
        }

        public static DataGridRow GetRow(DataGrid dataGrid1, int index)
        {
            if (dataGrid1.ItemContainerGenerator.Status == GeneratorStatus.NotStarted)
            {
                dataGrid1.UpdateLayout();
            }

            DataGridRow row = (DataGridRow)dataGrid1.ItemContainerGenerator.ContainerFromIndex(index);
            if (row == null)
            {
                ;
                dataGrid1.ScrollIntoView(dataGrid1.Items[index]);
                row = (DataGridRow)dataGrid1.ItemContainerGenerator.ContainerFromIndex(index);
            }
            return row;
        }

        public static T GetVisualChild<T>(Visual parent) where T : Visual
        {
            T child = default(T);
            int numVisuals = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < numVisuals; i++)
            {
                Visual v = (Visual)VisualTreeHelper.GetChild(parent, i);
                child = v as T;
                if (child == null)
                {
                    child = GetVisualChild<T>(v);
                }
                if (child != null)
                {
                    break;
                }
            }
            return child;
        }

        /// <summary>
        /// To A1 is transmit eg. DataGridRow.CurrentCell
        /// </summary>
        /// <param name="cell"></param>
        public static object GetCellValue(DataGridCellInfo cell)
        {
            var boundItem = cell.Item;
            var binding = new Binding();
            if (cell.Column is DataGridTextColumn)
            {
                binding
                  = ((DataGridTextColumn)cell.Column).Binding
                        as Binding;
            }
            else if (cell.Column is DataGridCheckBoxColumn)
            {
                binding
                  = ((DataGridCheckBoxColumn)cell.Column).Binding
                        as Binding;
            }
            else if (cell.Column is DataGridComboBoxColumn)
            {
                binding
                    = ((DataGridComboBoxColumn)cell.Column).SelectedValueBinding
                         as Binding;

                if (binding == null)
                {
                    binding
                      = ((DataGridComboBoxColumn)cell.Column).SelectedItemBinding
                           as Binding;
                }
            }

            if (binding != null)
            {
                var propertyName = binding.Path.Path;
                var propInfo = boundItem.GetType().GetProperty(propertyName);
                return propInfo.GetValue(boundItem, new object[] { });
            }

            return null;
        }

        public static string GetCellValueAsString(DataGridCellInfo dataGridCellInfo)
        {
            object vr = GetCellValue(dataGridCellInfo);
            if (vr == null)
            {
                return "";
            }
            return vr.ToString();
        }

        public static List<DataGridRow> GetDataGridRows(DataGrid dataGrid)
        {
            if (dataGrid.ItemContainerGenerator.Status == GeneratorStatus.NotStarted)
            {
                dataGrid.UpdateLayout();
            }
            List<DataGridRow> vr = new List<DataGridRow>();
            for (int i = 0; i < dataGrid.Items.Count; i++)
            {
                var d = dataGrid.ItemContainerGenerator.ContainerFromIndex(i);

                

                if (d == null)
                {
                    // These 2 lines help get all DataGridRow but wont work if is working with templates (like checkbox)
                    //dataGrid.UpdateLayout();
                    //dataGrid.ScrollIntoView(dataGrid.Items[i]);
                    d = dataGrid.ItemContainerGenerator.ContainerFromIndex(i);
                }


                DataGridRow row = (DataGridRow)d;
                vr.Add(row);
            }
            return vr;
        }

        public static void AddRows(DataGrid dtGrid, List<object> list, List<IList> o, params string[] columns)
        {
            foreach (var item in columns)
            {
                dtGrid.Columns.Add(DataGridHelper.NewTextColumn(item, null));
            }

            dtGrid.ItemsSource = o;
        }

        public static void EnableSorting(DataGrid dtGrid, bool v)
        {
            dtGrid.CanUserSortColumns = v;
            foreach (var item in dtGrid.Columns)
            {
                item.CanUserSort = v;
            }
        }

        public static void SortDataGrid(DataGrid dataGrid, DataGridColumn column, ListSortDirection? sortDirection)
        {
            // Clear current sort descriptions
            dataGrid.Items.SortDescriptions.Clear();

            var sortDirection2 = ListSortDirection.Ascending;

            if (sortDirection.HasValue)
            {
                sortDirection2 = sortDirection.Value;
            }

            // Add the new sort description
            dataGrid.Items.SortDescriptions.Add(new SortDescription(column.SortMemberPath, sortDirection2));

            // Apply sort
            foreach (var col in dataGrid.Columns)
            {
                col.SortDirection = null;
            }
            column.SortDirection = sortDirection;

            // Refresh items to display sort
            dataGrid.Items.Refresh();
        }
    }
    
}
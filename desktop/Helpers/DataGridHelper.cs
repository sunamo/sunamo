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
            DataGridRow row = (DataGridRow)dataGrid1.ItemContainerGenerator.ContainerFromIndex(index);
            if (row == null)
            {
                dataGrid1.UpdateLayout();
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
        /// <returns></returns>
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
    }
}

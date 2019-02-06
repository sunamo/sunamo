using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace sunamo.Collections
{
    /// <summary>
    /// Allow make query to parallel collections as be one
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ValuesTableGrid<T> : List<List<T>>
    {
        /// <summary>
        /// Row - wrapper - files 2
        /// Column - inner - apps 4
        /// </summary>
        private List<List<T>> exists;

        public DataTable SwitchRowsAndColumn()
        {
            DataTable newTable = new DataTable();

            newTable.Columns.Add("Field Name");
            for (int i = 0; i < exists.Count; i++)
                newTable.Columns.Add();

            for (int i = 0; i < exists[0].Count; i++)
            {
                DataRow newRow = newTable.NewRow();

                newRow[0] = oldTable.Columns[i].Caption;
                for (int j = 0; j < oldTable.Rows.Count; j++)
                    newRow[j + 1] = oldTable.Rows[j][i];
                newTable.Rows.Add(newRow);
            }

            dataGridView.DataSource = newTable;
        }

        public ValuesTableGrid(List<List<T>> exists)
        {
            this.exists = exists;
        }

        public bool IsAllInColumn(int i, T value)
        {
            return CA.IsAllTheSame<T>(value, exists[i]);
        }

        public bool IsAllInRow(int i, T value)
        {
            foreach (var item in exists)
            {
                if (!EqualityComparer<T>.Default.Equals(item[i], value))
                {
                    return false;
                }
            }
            return true;
        }
    }
}

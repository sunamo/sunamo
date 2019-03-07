using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace sunamo.Collections
{
    /// <summary>
    /// Similar class with two dimension array is UniqueTableInWhole
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
        public List<string> captions;

        /// <summary>
        /// Must be initialized captions variable
        /// All rows must be trimmed from \r \n
        /// </summary>
        /// <returns></returns>
        public DataTable SwitchRowsAndColumn()
        {
            DataTable newTable = new DataTable();

            newTable.Columns.Add(string.Empty);
            for (int i = 0; i < exists.Count; i++)
                newTable.Columns.Add();

            var s = exists[0];
            for (int i = 0; i < s.Count; i++)
            {
                DataRow newRow = newTable.NewRow();

                newRow[0] = captions[i];
                for (int j = 0; j < exists.Count; j++)
                    newRow[j + 1] = exists[j][i];
                newTable.Rows.Add(newRow);
            }

            return newTable;
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

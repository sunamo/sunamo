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
        private List<List<T>> _exists;
        public List<string> captions;

        /// <summary>
        /// Must be initialized captions variable
        /// All rows must be trimmed from \r \n
        /// </summary>
        public DataTable SwitchRowsAndColumn()
        {
            DataTable newTable = new DataTable();

            if (_exists.Count > 0)
            {
                newTable.Columns.Add(string.Empty);
                for (int i = 0; i < _exists.Count; i++)
                    newTable.Columns.Add();

                var s = _exists[0];
                for (int i = 0; i < s.Count; i++)
                {
                    DataRow newRow = newTable.NewRow();

                    var caption = CA.GetIndex(captions, i);
                    newRow[0] = caption == null ? string.Empty : caption.ToString();

                    for (int j = 0; j < _exists.Count; j++)
                        newRow[j + 1] = _exists[j][i];
                    newTable.Rows.Add(newRow);
                }
            }

            return newTable;
        }

        public ValuesTableGrid(List<List<T>> exists)
        {
            _exists = exists;
        }

        public bool IsAllInColumn(int i, T value)
        {
            return CA.IsAllTheSame<T>(value, _exists[i]);
        }

        public bool IsAllInRow(int i, T value)
        {
            foreach (var item in _exists)
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
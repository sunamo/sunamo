using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sunamo.Collections
{
    /// <summary>
    /// Add one row with all columns
    /// Similar class with two dimension array is ValuesTableGrid<T>
    /// 
    /// Can be:
    /// Every column of row unique
    /// Ëvery row of column unique
    /// Every column as whole different
    /// Ëvery rows as whole different
    /// </summary>
    public class UniqueTableInWhole
    {
        private string[,] _rows = null;
        private int _actualRow = 0;
        private int _cells = 0;

        public UniqueTableInWhole(int c, int r)
        {
            _cells = c;
            _rows = new string[r, c];
        }

        public bool IsRowsInColumnUnique(int columnIndex)
        {
            return false;
        }

        private bool IsColumnUnique(int columnIndex, int rowsCount)
        {
            HashSet<string> hs = new HashSet<string>();
            for (int r = 0; r < rowsCount; r++)
            {
                hs.Add(_rows[r, columnIndex]);
            }

            return hs.Count == rowsCount;
        }

        private bool IsRowUnique(int rowIndex, int columnsCount)
        {
            HashSet<string> hs = new HashSet<string>();
            for (int c = 0; c < columnsCount; c++)
            {
                hs.Add(_rows[rowIndex, c]);
            }

            return hs.Count == columnsCount;
        }

        /// <summary>
        /// If A1, must be all columns in all rows unique
        /// Ïf A2, must be all rows in all columns unique
        /// </summary>
        /// <param name="columns"></param>
        /// <param name="rows"></param>
        public bool IsUniqueAsRowsOrColumns(bool columns, bool rows)
        {
            if (!columns && !rows)
            {
                ThrowExceptions.Custom(Exc.GetStackTrace(), type, Exc.CallingMethod(),sess.i18n(XlfKeys.BothColumnAndRowArgumentsInUniqueTableInWholeIsUniqueAsRowOrColumnWasFalse) + ".");
            }

            int rowsCount = _rows.GetLength(0);
            int columnsCount = _rows.GetLength(1);

            if (columns)
            {
                for (int r = 0; r < rowsCount; r++)
                {
                    if (!IsRowUnique(r, columnsCount))
                    {
                        return false;
                    }
                }
            }

            if (rows)
            {
                for (int c = 0; c < columnsCount; c++)
                {
                    if (!IsColumnUnique(c, rowsCount))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        static Type type = typeof(UniqueTableInWhole);
        public void AddCells(List<string> c)
        {
            if (c.Count != _cells)
            {
                ThrowExceptions.Custom(Exc.GetStackTrace(), type, Exc.CallingMethod(),sess.i18n(XlfKeys.DifferentCountInputElementsOfArrayInUniqueTableInWholeAddCells));
            }

            for (int i = 0; i < c.Count; i++)
            {
                _rows[_actualRow, i] = c[i];
            }

            _actualRow++;
        }
    }
}
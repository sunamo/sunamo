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
    /// Může být:
    /// Každý sloupec řádku unikátní
    /// Každý řádek sloupce unikátní
    /// Všechny sloupce jako celek odlišné
    /// Všechny řádky jako celkem odlišné
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

        /// <summary>
        /// Vrátí zda je každá hodnota v sloupci A1 unikátní
        /// Nekontroluje zda je index A1 správný, musí to dělat volající metoda
        /// </summary>
        /// <param name="columnIndex"></param>
        
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
        /// Pokud A1, musí být všechny sloupce jako celek zvlášť unikátní
        /// Pokud A2, musí být všechny řádky jako celek zvlášť unikátní
        /// </summary>
        /// <param name="columns"></param>
        /// <param name="rows"></param>
        public bool IsUniqueAsRowsOrColumns(bool columns, bool rows)
        {
            if (!columns && !rows)
            {
                throw new Exception("Both column and row arguments in UniqueTableInWhole.IsUniqueAsRowOrColumn() was false" + ".");
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

        public void AddCells(List<string> c)
        {
            if (c.Count != _cells)
            {
                throw new Exception("Different count input elements of array in UniqueTableInWhole.AddCells");
            }

            for (int i = 0; i < c.Count; i++)
            {
                _rows[_actualRow, i] = c[i];
            }

            _actualRow++;
        }
    }
}
